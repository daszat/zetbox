
namespace Zetbox.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Arebis.CodeGeneration;
    using Arebis.CodeGenerator.Templated;

    using Zetbox.API.Utils;

    /// <summary>
    /// a <see cref="IGenerationHost"/> to use the pre-compiled Templates from the current assembly
    /// </summary>
    /// Copied and adapted from Arebis.CodeGenerator.Templated.GenerationHost

#if RELEASE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class ResourceBasedGenerationHost
        : IGenerationHost, IDisposable
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Generator.Host");

        private Dictionary<string, TemplateInfo> templates;
        private List<IFileWriter> fileWriters;
        private NameValueCollection settings;

        private Stack<TextWriter> contextWriter;
        private string outputDirectory;

        private static readonly object _lock = new object();
        private static bool _arebisInitialized = false;

        public ResourceBasedGenerationHost()
        {
        }

        public void Initialize(NameValueCollection settings)
        {
            if (settings == null) { throw new ArgumentNullException("settings"); }

            // Store settings:
            this.settings = settings;

            if (!_arebisInitialized)
            {
                lock (_lock)
                {
                    if (!_arebisInitialized)
                    {
                        // Setup generation language settings:
                        GenerationLanguage.DefaultNameSpace = "Arebis.DynamicAssembly";
                        GenerationLanguage.DefaultBaseClass = "Arebis.CodeGeneration.CodeTemplate";
                        GenerationLanguage.CodeBuilders["vb"] = typeof(VBCodeBuilder);
                        GenerationLanguage.CodeBuilders["c#"] = typeof(CSCodeBuilder);
                        GenerationLanguage.DefaultTemplateLanguage = "c#";
                        _arebisInitialized = true;
                    }
                }
            }

            // Default to T3 syntax:
            new Arebis.CodeGenerator.Templated.Syntax.T3Syntax().Setup(this.settings);

            // Run GenerationSetup if any:
            foreach (string generationSetupTypeName in settings.GetValues("generatorsetup") ?? new string[0])
            {
                try
                {
                    ((IGenerationSetup)Activator.CreateInstance(Type.GetType(generationSetupTypeName)))
                    .Setup(this.settings);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(String.Format("Failed to setup generator by '{0}': {1}", generationSetupTypeName, ex.Message));
                }
            }

            // Build initial context writer:
            this.contextWriter = new Stack<TextWriter>();
            this.contextWriter.Push(new StringWriter());

            // Set output directory:
            this.outputDirectory = Path.Combine(Environment.CurrentDirectory, (this.Settings["targetdir"] ?? "."));

            // Initialize compiled templates:
            this.templates = new Dictionary<string, TemplateInfo>();

            // Initialize filewriters:
            this.fileWriters = new List<IFileWriter>();
            foreach (string writer in settings.GetValues("filewriter") ?? new string[0])
            {
                try
                {
                    Type writerType = Type.GetType(writer);
                    IFileWriter instance = (IFileWriter)Activator.CreateInstance(writerType);
                    this.fileWriters.Add(instance);
                }
                catch (ArgumentNullException)
                {
                    throw new ApplicationException(String.Format("Failed to instantiate the filewriter \"{0}\". Check classname and referencepath.", writer));
                }
            }

            // Add default filewriter:
            this.fileWriters.Add(new DefaultFileWriter());

            // Link & initialize file writers:
            for (int i = 0; i < this.fileWriters.Count; i++)
            {
                // Set host:
                this.fileWriters[i].Host = this;
                // Connect to previous (if any):
                if (i > 0)
                {
                    this.fileWriters[i - 1].NextWriter = this.fileWriters[i];
                }
            }
        }

        public void CallTemplate(string templateClass, params object[] parameters)
        {
            // Call template:
            this.CallTemplateToContext(templateClass, parameters);
        }

        public void CallTemplateToFile(string templateClass, string outputfile, params object[] parameters)
        {
            try
            {
                // Push new writer as contexts writer:
                this.contextWriter.Push(new StringWriter());

                this.CallTemplateToContext(templateClass, parameters);
            }
            finally
            {
                // Restore previous writer context:
                TextWriter writer = this.contextWriter.Pop();

                // Write the file:
                if (outputfile != null)
                    this.WriteFile(Path.Combine(this.outputDirectory, outputfile), writer.ToString());
            }
        }

        private void CallTemplateToContext(string templateClass, params object[] parameters)
        {
            var providerName = String.Format(
                "{0}.{1}, {2}",
                this.Settings["providertemplatenamespace"],
                templateClass,
                this.Settings["providertemplateassembly"]);

            Type t = Type.GetType(providerName);
            if (t == null)
            {
                var defaultName = String.Format("{0}.{1}", this.Settings["basetemplatepath"], templateClass);
                t = Type.GetType(defaultName);

                if (t == null)
                {
                    Log.InfoFormat("provided template [{0}] not found", providerName);
                    throw new ArgumentOutOfRangeException("templateClass", String.Format("No class found for {0}", templateClass));
                }
                else
                {
                    Log.InfoFormat("provided template [{0}] not found, using [{1}] instead", providerName, defaultName);
                }
            }

            var fullParams = new object[] { this }.Concat(parameters).ToArray();
            ResourceTemplate template = null;
            try
            {
                template = (ResourceTemplate)Activator.CreateInstance(t, fullParams);
            }
            catch (MissingMethodException ex)
            {
                var msg = String.Format("Failed to find Constructor for {0} with signature:\n\t{1}",
                        t.FullName,
                        String.Join(",\n\t", fullParams.Select(p => p == null ? "(null)" : p.GetType().FullName).ToArray()));
                Log.Error(msg, ex);
                throw new ApplicationException(msg, ex);
            }
            template.Generate();
        }

        public virtual void WriteFile(string filename, string content)
        {
            this.fileWriters[0].WriteFile(filename, content);
        }

        public NameValueCollection Settings
        {
            get { return this.settings; }
        }

        public void WriteOutput(string str)
        {
            this.contextWriter.Peek().Write(str);
        }

        public string NewLineString
        {
            get { return Environment.NewLine; }
        }

        void IGenerationHost.Log(string fmt, params object[] args)
        {
            Log.InfoFormat(fmt, args);
        }

        public void Dispose()
        {
            // Dispose templates:
            if (this.templates != null)
            {
                foreach (TemplateInfo item in this.templates.Values)
                {
                    item.Dispose();
                }
                this.templates = null;
            }
        }
    }
}