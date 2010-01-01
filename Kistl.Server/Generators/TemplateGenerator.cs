using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using Arebis.CodeGenerator.Templated;
using Kistl.API.Utils;

namespace Kistl.Server.Generators
{
    /// <summary>
    /// http://www.codeproject.com/KB/cs/T4BasedCodeGenerator.aspx
    /// A Generator object creates a GenerationHost and executes the initial template on it.
    /// This implementation will host the GenerationHost in a separate AppDomain to allow unload of assemblies.
    /// </summary>
    public class TemplateGenerator
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Generator.Templates");

        private NameValueCollection settings = new NameValueCollection();
        private object[] templateParameters = new object[0];

        public NameValueCollection Settings
        {
            get { return this.settings; }
            set { this.settings = value; }
        }

        public object[] TemplateParameters
        {
            get { return this.templateParameters; }
            set { this.templateParameters = value; }
        }

        public virtual int ExecuteTemplate()
        {
            // Execute the (initial) template by GenerationHost in separate AppDomain:
#if USE_APP_DOMAIN
			AppDomain appDomain = null;
			GenerationHost genHost = null;
			try
			{
				AppDomainSetup info = new AppDomainSetup();
				info.ApplicationBase = new FileInfo(Assembly.GetEntryAssembly().Location).Directory.FullName;
				info.ShadowCopyFiles = "true";
				appDomain = AppDomain.CreateDomain("CodeGenSpace", AppDomain.CurrentDomain.Evidence, info);
				genHost = (GenerationHost)appDomain.CreateInstance(typeof(GenerationHost).Assembly.FullName, typeof(GenerationHost).FullName).Unwrap();
				genHost.Initialize(this.Settings);
				return this.ExecuteTemplate(genHost);
			}
			finally
			{
				if (genHost != null) genHost.Dispose();
				if (appDomain != null) AppDomain.Unload(appDomain);
			}
#else
            using (GenerationHost genHost = new GenerationHost())
            {
                genHost.Initialize(this.Settings);
                return this.ExecuteTemplate(genHost);
            }
#endif
        }

        public virtual int ExecuteTemplate(GenerationHost genHost)
        {
            return this.ExecuteTemplate(genHost, this.Settings["template"], this.settings["output"], this.TemplateParameters);
        }

        public virtual int ExecuteTemplate(GenerationHost genHost, string templateFilename, string outputFilename, object[] templateParameters)
        {
            if (genHost == null) { throw new ArgumentNullException("genHost"); }

            using (log4net.NDC.Push(templateFilename))
            {
                try
                {
                    if (Log.IsDebugEnabled)
                    {
                        Log.DebugFormat("Executing template");
                    }
                    genHost.Initialize(this.Settings);
                    genHost.CallTemplateToFile(templateFilename, outputFilename, templateParameters);
                    return 0;
                }
                catch (CompilationFailedException ex)
                {
                    Log.Error("Template compilation failed", ex);
                    foreach (CompilerError err in ex.Errors)
                    {
                        Log.WarnFormat("{0} {1}: {2}\r\n  \"{3}\", line #{4}",
                            err.IsWarning ? "Warning" : "Error",
                            err.ErrorNumber,
                            err.ErrorText,
                            err.FileName,
                            err.Line);
                    }
                    throw;
                }
            }
        }
    }
}
