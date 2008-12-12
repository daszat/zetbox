using System;
using System.Collections.Generic;
using System.Text;
using Arebis.Utils;
using Arebis.CodeGeneration;
using System.Collections.Specialized;
using System.Reflection;
using System.IO;
using System.Globalization;

namespace Arebis.CodeGenerator.Templated
{
	public class GenerationHost : IGenerationHost, IDisposable
	{
		private static Dictionary<string, TemplateInfo> templates = new Dictionary<string,TemplateInfo>();
		private List<IFileWriter> fileWriters;
		private NameValueCollection settings;
		private List<string> referencepath;

		private Stack<string> contextDirectory;
		private Stack<TextWriter> contextWriter;
		private string outputDirectory;

		private string logfile;

		public GenerationHost()
		{
			// Install assembly resolver:
#if USE_APP_DOMAIN
			AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(this.AssemblyResolve);
#endif
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

			// Build base referencepath:
			this.referencepath = new List<string>();
			foreach (string item in this.Settings.GetValues("referencepath") ?? new string[0])
				this.referencepath.Add(item);

			// Build initial context directory:
			this.contextDirectory = new Stack<string>();
			this.contextDirectory.Push(Path.Combine(Environment.CurrentDirectory, (this.Settings["sourcedir"] ?? ".")));

			// Build initial context writer:
			this.contextWriter = new Stack<TextWriter>();
			this.contextWriter.Push(new StringWriter());

			// Set output directory:
			this.outputDirectory = Path.Combine(Environment.CurrentDirectory, (this.Settings["targetdir"] ?? "."));

			// Initialize compiled templates:
			// this.templates = new Dictionary<string, TemplateInfo>();

			// Initialize filewriters:
			this.fileWriters = new List<IFileWriter>();
			foreach(string writer in settings.GetValues("filewriter") ?? new string[0])
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

		/// <summary>
		/// Returns a writable copy of the base reference path.
		/// </summary>
        public List<string> ReferencePath
		{
            [System.Diagnostics.DebuggerStepThrough]
            get { return new List<string>(this.referencepath); }
		}

		public void CallTemplate(string templatefile, params object[] parameters)
		{
			// Get full template filename:
            string fulltemplatefile = templatefile.IsResourceFile() ? templatefile : Path.Combine(this.contextDirectory.Peek(), templatefile);

			// Call template:
			this.CallTemplateToContext(fulltemplatefile, parameters);
		}

		public void CallTemplateToFile(string templatefile, string outputfile, params object[] parameters)
		{
			// Get full template filename:
            string fulltemplatefile = templatefile.IsResourceFile() ? templatefile : Path.Combine(this.contextDirectory.Peek(), templatefile);

			try
			{
				// Push new writer as contexts writer:
				this.contextWriter.Push(new StringWriter());

				this.CallTemplateToContext(fulltemplatefile, parameters);
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

		private void CallTemplateToContext(string fulltemplatefile, params object[] parameters)
		{
			try
			{
				// Push template's directory as current directory context:
                if(!fulltemplatefile.IsResourceFile())
				    this.contextDirectory.Push(new FileInfo(fulltemplatefile).Directory.FullName);

				// Create & cache template:
                if (templates.ContainsKey(fulltemplatefile) == false)
                {
                    templates[fulltemplatefile] = new TemplateInfo();
                }
                templates[fulltemplatefile].Initialize(fulltemplatefile, this);

				// Invoke template:
				templates[fulltemplatefile].Invoke(parameters);
			}
			finally
			{
				// Restore previous directory context:
                if (!fulltemplatefile.IsResourceFile())
				    this.contextDirectory.Pop();
			}
		}

        [System.Diagnostics.DebuggerStepThrough]
        public virtual void WriteFile(string filename, string content)
		{
			this.fileWriters[0].WriteFile(filename, content);
		}

        public NameValueCollection Settings
		{
            [System.Diagnostics.DebuggerStepThrough]
            get { return this.settings; }
		}

        [System.Diagnostics.DebuggerStepThrough]
        public void WriteOutput(string str)
		{
			this.contextWriter.Peek().Write(str);
		}

        public string NewLineString
		{
            [System.Diagnostics.DebuggerStepThrough]
            get { return Environment.NewLine; }
		}

        [System.Diagnostics.DebuggerStepThrough]
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
			// Uninstall assembly resolver:
#if USE_APP_DOMAIN
			AppDomain.CurrentDomain.AssemblyResolve -=new ResolveEventHandler(this.AssemblyResolve);
#endif

			// Dispose templates:
            //if (this.templates != null)
            //{
            //    foreach (TemplateInfo item in this.templates.Values)
            //    {
            //        item.Dispose();
            //    }
            //    this.templates = null;
            //}
		}

#if USE_APP_DOMAIN
		/// <summary>
		/// Attempts to resolve assemblies.
		/// </summary>
		Assembly AssemblyResolve(object sender, ResolveEventArgs args)
		{
			if (this.referencepath != null)
			{
				string filename = args.Name.Split(',')[0];
				foreach (string path in this.referencepath)
				{
					if (File.Exists(Path.Combine(path, filename + ".dll")))
					{
						return Assembly.LoadFile(Path.Combine(path, filename + ".dll"));
					}
					else if (File.Exists(Path.Combine(path, filename + ".exe")))
					{
						return Assembly.LoadFile(Path.Combine(path, filename + ".exe"));
					}
				}
			}
			return null;
		}
#endif
	}
}
