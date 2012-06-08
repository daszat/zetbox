// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Generator
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Specialized;
    using System.IO;
    using System.Reflection;
    using Arebis.CodeGenerator.Templated;

    /// <summary>
    /// http://www.codeproject.com/KB/cs/T4BasedCodeGenerator.aspx
    /// A Generator object creates a GenerationHost and executes the initial template on it.
    /// This implementation will host the GenerationHost in a separate AppDomain to allow unload of assemblies.
    /// </summary>
    public class TemplateExecutor
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Generator.Templates");

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
#if USE_APP_DOMAIN
            // Execute the (initial) template by GenerationHost in separate AppDomain:
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
            // execute the generationhost locally
            using (ResourceBasedGenerationHost genHost = new ResourceBasedGenerationHost())
            {
                genHost.Initialize(this.Settings);
                return this.ExecuteTemplate(genHost);
            }
#endif
        }

        public virtual int ExecuteTemplate(ResourceBasedGenerationHost genHost)
        {
            return this.ExecuteTemplate(genHost, this.Settings["template"], this.Settings["output"], this.TemplateParameters);
        }

        public virtual int ExecuteTemplate(ResourceBasedGenerationHost genHost, string templateFilename, string outputFilename, object[] templateParameters)
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
