using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Arebis.CodeGeneration;
using Arebis.CodeGenerator.Templated;

namespace Kistl.Server.Generators
{

    /// <summary>
    /// a <see cref="IGenerationHost"/> to use the pre-compiled Templates from the current assembly
    /// </summary>
    /// Copied and adapted from Arebis.CodeGenerator.Templated.GenerationHost

#if RELEASE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class GenerationHost : IGenerationHost, IDisposable
    {
        private Dictionary<string, TemplateInfo> templates;
        private List<IFileWriter> fileWriters;
        private NameValueCollection settings;

        private Stack<TextWriter> contextWriter;
        private string outputDirectory;

        private string logfile;

        public GenerationHost()
        {
        }

        public void Initialize(NameValueCollection settings)
        {
            // Store settings:
            this.settings = settings;

            // Setup generation language settings:
            GenerationLanguage.DefaultNameSpace = "Arebis.DynamicAssembly";
            GenerationLanguage.DefaultBaseClass = "Arebis.CodeGeneration.CodeTemplate";
            GenerationLanguage.CodeBuilders["vb"] = typeof(VBCodeBuilder);
            GenerationLanguage.CodeBuilders["c#"] = typeof(CSCodeBuilder);
            GenerationLanguage.DefaultTemplateLanguage = "c#";

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

            // Initialize logfile:
            if (settings["logfile"] != null)
            {
                this.logfile = Path.Combine(Environment.CurrentDirectory, settings["logfile"]);
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
            Type t = Type.GetType(String.Format("{0}.{1}", this.Settings["providertemplatepath"], templateClass));
            t = t ?? Type.GetType(String.Format("{0}.{1}", this.Settings["basetemplatepath"], templateClass));
            
            if (t == null)
                throw new ArgumentException("templateClass", String.Format("No class found for {0}", templateClass));

            var template = (KistlCodeTemplate)Activator.CreateInstance(t, new object[] { this }.Concat(parameters).ToArray());
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

        public void Log(string fmt, params object[] args)
        {
            string timestamp = DateTime.Now.ToString();
            try
            {
                if (logfile == null)
                    Console.WriteLine(String.Format(timestamp + " " + fmt, args));
                else
                {
                    StreamWriter writer = File.AppendText(this.logfile);
                    try
                    {
                        writer.WriteLine(String.Format(timestamp + " " + fmt, args));
                    }
                    finally
                    {
                        writer.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    Console.WriteLine(timestamp + " Attempt to log message \"" + fmt + "\" failed: " + ex.Message);
                }
                catch (Exception) { }
            }
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