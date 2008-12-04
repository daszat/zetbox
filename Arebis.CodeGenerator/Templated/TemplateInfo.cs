using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Arebis.CodeGeneration;
using Arebis.Parsing.MultiContent;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Diagnostics;
using System.CodeDom.Compiler;

namespace Arebis.CodeGenerator.Templated
{
	[System.Diagnostics.DebuggerStepThrough]
	public class TemplateInfo : ITemplateInfo, IDisposable
	{
		#region Static fields


		#endregion Static fields

		#region Instance fields

		// Main fields:
		private GenerationHost host;
		private FileInfo templatefileinfo;
		private MixedContentFile fileContent;
		private ICodeBuilder codeBuilder = null;

		// Properties retrieved from template directives:
		private Dictionary<string, IList<NameValueCollection>> directives;

		#endregion Instance fields

		public TemplateInfo(string filename, GenerationHost host)
		{
			this.templatefileinfo = new FileInfo(filename);
			this.host = host;
		}
		
		public void Invoke(object[] parameters)
		{
			// Create compiled type:
			if (this.codeBuilder == null)
			{
				this.host.Log("Parsing template: \"{0}\".", this.templatefileinfo.FullName);
				this.fileContent = this.ReadAndParseFile();
				this.ReadDirectives(this.fileContent);

				this.host.Log("Compiling template: \"{0}\".", this.templatefileinfo.FullName);
				this.codeBuilder = this.CreateCodeBuilder(this.GetCodeTemplateDirective()["Language"] ?? GenerationLanguage.DefaultTemplateLanguage);
				this.codeBuilder.TemplateInfo = this;
				bool succeeded = this.codeBuilder.Compile();
				foreach (CompilerError err in this.codeBuilder.CompilerErrors)
				{ 
					this.host.Log("Compile {0} {1}: {2}\r\n  File \"{3}\", line {4}",
						err.IsWarning ? "warning" : "error",
						err.ErrorNumber,
						err.ErrorText,
						err.FileName,
						err.Line);
				}
				if (succeeded == false)
				{
					throw new CompilationFailedException(this.templatefileinfo.FullName, this.codeBuilder.CompilerErrors);
				}
			}

			// Construct full parameters (add host):
			List<object> allparameters = new List<object>();
			allparameters.Add(this.host);
			allparameters.AddRange(parameters);

			// Create instance and invoke:
			this.host.Log("Invoking template: \"{0}\".", this.templatefileinfo.FullName);
			CodeTemplate instance = (CodeTemplate)Activator.CreateInstance(this.codeBuilder.CompiledType, allparameters.ToArray());
			try
			{
				instance.Generate();
			}
			catch (RuntimeException)
			{
				throw;
			}
			catch (CompilationFailedException)
			{
				throw;
			}
			catch (Exception ex)
			{
				this.host.Log(ex.ToString());
				throw new RuntimeException(ex);
			}
		}

		/// <summary>
		/// Returns the full filename given a relative filename.
		/// Used to resolve codeBehind and additionally compiled files.
		/// </summary>
		public string FindFile(string relativeName)
		{
		    if (relativeName == null) return null;
		    return Path.Combine(Path.GetDirectoryName(templatefileinfo.FullName), relativeName);
		}

		#region ITemplateInfo Members

		public IGenerationHost Host
		{
			get { return this.host; }
		}

		public NameValueCollection Settings
		{
			get { return this.host.Settings; }
		}

		public FileInfo TemplateFileInfo
		{
			get { return this.templatefileinfo; }
		}

		public MixedContentFile FileContent
		{
			get { return this.fileContent; }
		}

		public IList<NameValueCollection> GetDirectives(string directiveName)
		{
			if (this.directives.ContainsKey(directiveName))
				return this.directives[directiveName];
			else
				return new List<NameValueCollection>();
		}

		public NameValueCollection GetCodeTemplateDirective()
		{
			IList<NameValueCollection> ctdirs = this.GetDirectives(GenerationLanguage.CodeTemplateElementName);
			if (ctdirs.Count > 0)
				return ctdirs[0];
			else
				return new NameValueCollection();
		}

		#endregion ITemplateInfo Members

		#region IDisposable Members

		public void Dispose()
		{
			if (this.codeBuilder != null)
			{
				this.codeBuilder.Dispose();
				this.codeBuilder = null;
			}
		}

		#endregion

		#region Private implementation

		private MixedContentFile ReadAndParseFile()
		{
			// Read the file:
			MixedContentFile file = new MixedContentFile(this.templatefileinfo.FullName, File.ReadAllText(this.templatefileinfo.FullName), TemplatePartTypes.TemplateBody);

			// Process comments:
			file.ApplyParserRegex(TemplatePartTypes.TemplateBody, TemplatePartTypes.Comment, GenerationLanguage.RxComments);
			file.ExtractPartsGroup(TemplatePartTypes.Comment, "comment", TemplatePartTypes.Comment);

			// Pre-processor 'includes':
			while (file.ApplyParserRegex(TemplatePartTypes.TemplateBody, TemplatePartTypes.IncludePragma, GenerationLanguage.RxIncludes))
			{
				foreach (ContentPart part in file.Parts)
				{
					if ((TemplatePartTypes)part.Type == TemplatePartTypes.IncludePragma)
					{
						string fn = part.Data["filename"];
						fn = Path.Combine(Path.GetDirectoryName(part.File.Filename), fn);
						part.Substitute(new MixedContentFile(fn, File.ReadAllText(fn), TemplatePartTypes.TemplateBody), TemplatePartTypes.TemplateBody);
					}
				}
			}

			// Process declarations:
			file.ApplyParserRegex(TemplatePartTypes.TemplateBody, TemplatePartTypes.Declaration, GenerationLanguage.RxDirectives);

			// Process scripts:
			file.ApplyParserRegex(TemplatePartTypes.TemplateBody, TemplatePartTypes.Script, GenerationLanguage.RxScripts);
			file.ExtractPartsGroup(TemplatePartTypes.Script, "body", TemplatePartTypes.Script);

			// Process scriptlets:
			file.ApplyParserRegex(TemplatePartTypes.TemplateBody, TemplatePartTypes.Scriptlet, GenerationLanguage.RxScriptlets);
			file.ExtractPartsGroup(TemplatePartTypes.Scriptlet, "body", TemplatePartTypes.Scriptlet);

			// Process embedded body:
			file.ApplyParserRegex(TemplatePartTypes.Scriptlet, TemplatePartTypes.EmbeddedBody, GenerationLanguage.RxEmbeddedBody);
			file.ExtractPartsGroup(TemplatePartTypes.EmbeddedBody, "body", TemplatePartTypes.TemplateBody);

			return file;
		}


		private static Regex directiveParser = new Regex("<%@\\s*(?<elementName>\\w+)(\\s+(?<name>\\w+)=\"(?<value>[^\"]*)\")*\\s*%>( |\\t)*\\r?\\n?");

		private void ReadDirectives(MixedContentFile file)
		{
			// Initialize directives:
			this.directives = new Dictionary<string, IList<NameValueCollection>>();

			// Collect directives:
			foreach (ContentPart part in file.FindPartsOfType(TemplatePartTypes.Declaration))
			{
				foreach (Match match in directiveParser.Matches(part.Content))
				{
					string name = match.Groups["elementName"].Value;
					NameValueCollection attributes = new NameValueCollection();
					for (int i = 0; i < match.Groups["name"].Captures.Count; i++)
					{
						attributes[match.Groups["name"].Captures[i].Value]
							= match.Groups["value"].Captures[i].Value;
					}
					if (this.directives.ContainsKey(name) == false)
						this.directives[name] = new List<NameValueCollection>();
					this.directives[name].Add(attributes);
				}
			}
		}

		private ICodeBuilder CreateCodeBuilder(string language)
		{
			try
			{
				return (ICodeBuilder)Activator.CreateInstance(GenerationLanguage.CodeBuilders[language.ToLower()]);
			}
			catch (KeyNotFoundException)
			{
				throw new ApplicationException(String.Format("Unknown template language '{0}'.", language));
			}
		}

		#endregion Private implementation

	}
}
