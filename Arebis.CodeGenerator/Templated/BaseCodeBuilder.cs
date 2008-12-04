using System;
using System.Collections.Generic;
using System.Text;
using Arebis.Parsing.MultiContent;
using System.IO;
using Arebis.Parsing;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Text.RegularExpressions;
using System.Reflection;
using Arebis.Reflection;
using System.Collections.Specialized;
using Arebis.Utils;
using Arebis.CodeGeneration;

namespace Arebis.CodeGenerator.Templated
{
	[System.Diagnostics.DebuggerStepThrough]
	public abstract class BaseCodeBuilder : ICodeBuilder
	{
		private static Regex RxLinesWithLinebreaks = new Regex("[^\\r\\n]*\\r?\\n?");

		private ITemplateInfo templateInfo;
		private string code;
		private Type compiledType;
		private List<CompilerError> compilerErrors;

		private List<string> filesToDelete = new List<string>();

		#region ICodeBuilder Members

		public ITemplateInfo TemplateInfo
		{
			get { return this.templateInfo; }
			set { this.templateInfo = value; }
		}

		public Type CompiledType
		{
			get { return this.compiledType; }
		}

		public IList<CompilerError> CompilerErrors
		{
			get { return this.compilerErrors; }
		}

		public string Code
		{
			get { return this.code; }
		}

		public bool Compile()
		{
			// Check precondition: templateInfo must be known:
			if (this.templateInfo == null)
				throw new InvalidOperationException("TemplateInfo property must be set before calling Compile method.");

			// Template substitution fields:
			string ns;
			string classname;
			string baseclassname;
			StringBuilder imports = new StringBuilder();
			StringBuilder fields = new StringBuilder();
			StringBuilder constructorparams = new StringBuilder();
			StringBuilder fieldinits = new StringBuilder();
			StringBuilder generatebody = new StringBuilder();
			StringBuilder scripts = new StringBuilder();
			string codeBehindFile = null;

			// Retrieve codeTemplate directive:
			NameValueCollection ctdir = templateInfo.GetCodeTemplateDirective();
			// Retrieve class name information:
			ClassName cnb = new ClassName(ctdir["ClassName"] ?? StringUtils.ToIdentifier(templateInfo.TemplateFileInfo.Name));
			if (cnb.NameSpace == String.Empty)
				cnb.NameSpace = GenerationLanguage.DefaultNameSpace;

			// Retrieve base information:
			ns = cnb.NameSpace;
			classname = cnb.Name;
			baseclassname = ctdir["Inherits"] ?? GenerationLanguage.DefaultBaseClass;
			codeBehindFile = templateInfo.FindFile(ctdir["CodeFile"]);

			foreach (NameValueCollection import in templateInfo.GetDirectives("Import"))
			{
				this.AppendImport(imports, import["Namespace"], import["Alias"]);
			}

			// Fill fields, fieldinits, constructorparams:
			foreach (NameValueCollection param in templateInfo.GetDirectives("Parameter"))
			{
				string n = param["Name"];
				string t = param["Type"] ?? "System.Object";

				fields.Append("\t\t");
				this.AppendField(fields, n, t);
				fieldinits.Append("\t\t\t");
				this.AppendFieldInitializer(fieldinits, n, t);
				this.AppendConstructorParam(constructorparams, n, t);
			}

			// Fill generatebody:
			bool writeLinePragma = Convert.ToBoolean(ctdir["LinePragmas"] ?? "True");
			foreach (ContentPart part in templateInfo.FileContent.Parts)
			{
				// Check for TemplateBody or Scriptlet:
				if (((TemplatePartTypes)part.Type != TemplatePartTypes.TemplateBody) && ((TemplatePartTypes)part.Type != TemplatePartTypes.Scriptlet))
					continue;

				// Add line pragma begin:
				if (writeLinePragma)
					this.AppendLinePragmaBegin(generatebody, part.File.Filename, part.StartLine);

				// Generate:
				if ((TemplatePartTypes)part.Type == TemplatePartTypes.TemplateBody)
				{
					foreach (string line in LinesWithBreaks(part.Content))
					{
						string content = ToContent(this.LineToWriteCall(line));
						//generatebody.Append("\t\t\t");
						generatebody.Append(content);
						generatebody.AppendLine();
					}
				}
				else if ((TemplatePartTypes)part.Type == TemplatePartTypes.Scriptlet)
				{
					foreach (string line in LinesWithBreaks(part.Content))
					{
						//generatebody.Append("\t\t\t");
						generatebody.Append(line);
					}
					generatebody.AppendLine();
				}

				// Add line pragma end:
				if (writeLinePragma)
					this.AppendLinePragmaEnd(generatebody);
			}

			// Fill scripts:
			foreach (ContentPart part in templateInfo.FileContent.FindPartsOfType(TemplatePartTypes.Script))
			{
				// Add line pragma begin:
				if (writeLinePragma)
					this.AppendLinePragmaBegin(scripts, part.File.Filename, part.StartLine);

				// Append script:
				scripts.AppendLine(part.Content);
				scripts.AppendLine();

				// Add line pragma end:
				if (writeLinePragma)
					this.AppendLinePragmaEnd(scripts);
			}

			// Build code:
			this.code = GetCodeTemplate();
			this.code = this.code.Replace("<%=templatefilename%>", templateInfo.TemplateFileInfo.FullName);
			this.code = this.code.Replace("<%=imports%>", imports.ToString());
			this.code = this.code.Replace("<%=namespace%>", ns);
			this.code = this.code.Replace("<%=classname%>", classname);
			this.code = this.code.Replace("<%=baseclassname%>", baseclassname);
			this.code = this.code.Replace("<%=fields%>", fields.ToString());
			this.code = this.code.Replace("<%=constructorparameters%>", constructorparams.ToString());
			this.code = this.code.Replace("<%=fieldinitialisations%>", fieldinits.ToString());
			this.code = this.code.Replace("<%=generatebody%>", generatebody.ToString());
			this.code = this.code.Replace("<%=scripts%>", scripts.ToString());

			// Save code file:
			string codeFile = templateInfo.TemplateFileInfo.FullName + ".gen";
			File.WriteAllText(codeFile, this.code);

			// Build assembly resolution path:
			List<string> referencepathlist = ((GenerationHost)templateInfo.Host).ReferencePath;
			referencepathlist.Add(templateInfo.TemplateFileInfo.Directory.FullName);
			referencepathlist.Add(templateInfo.TemplateFileInfo.Directory.FullName + "\\bin");
			string[] referencepath = referencepathlist.ToArray();

			// Collect references:
			List<string> references = new List<string>();
			references.Add(FileUtils.FindInPath(new FileInfo(typeof(Arebis.CodeGeneration.CodeTemplate).Assembly.Location).Name, referencepath));
			references.Add("System.dll");

			// Find references from settings:
			foreach (string path in templateInfo.Settings.GetValues("referenceassembly") ?? new string[0])
			{
				// Search for references:
				string fullpath = FileUtils.FindInPath(path, referencepath) ?? path;
				if (fullpath == null) continue;

				// Add reference to references:
				if (references.Contains(fullpath) == false) references.Add(fullpath);
			}

			// Find references from directives:
			foreach (NameValueCollection refer in templateInfo.GetDirectives("ReferenceAssembly"))
			{
				// Search for references:
				string path = refer["Path"];
				string fullpath = FileUtils.FindInPath(path, referencepath) ?? path;
				if (fullpath == null) continue;

				// Add reference to references:
				if (references.Contains(fullpath) == false) references.Add(fullpath);
			}

			// Log references:
			foreach (string item in references)
			{
				templateInfo.Host.Log("Added reference \"{0}\".", item);
			}

			// Generate assembly filename:
			string assemblyFile = 
				ctdir["AssemblyFile"] ??
				Path.Combine(Path.GetTempPath(), StringUtils.ToIdentifier(templateInfo.TemplateFileInfo.FullName) + ".dll");

			// Collect files to compile:
			List<string> codefiles = new List<string>();
			codefiles.Add(codeFile);
			if (codeBehindFile != null) codefiles.Add(codeBehindFile);
			foreach (NameValueCollection compileFileAttrs in templateInfo.GetDirectives("CompileFile"))
				codefiles.Add(templateInfo.FindFile(compileFileAttrs["Path"]));

			// Compile the template code:
			CodeDomProvider provider = this.GetCodeDomProvider();
			CompilerParameters pars = new CompilerParameters(references.ToArray(), assemblyFile, true);
			CompilerResults results;
			pars.GenerateExecutable = false;
			pars.GenerateInMemory = false;
			results = provider.CompileAssemblyFromFile(pars, codefiles.ToArray());

			// Delete the generated sourcefile:
			this.filesToDelete.Add(assemblyFile);
			this.filesToDelete.Add(assemblyFile.Replace(".dll", ".pdb"));
			if (Convert.ToBoolean(templateInfo.Settings["deletegeneratedsource"] ?? "True") == true)
				this.filesToDelete.Add(codeFile);

			// Check for compile errors & warnings:
			this.compilerErrors = new List<CompilerError>();
			foreach (CompilerError error in results.Errors)
			{
				compilerErrors.Add(error);
			}

			// Store compiled type & return success:
			if (results.Errors.HasErrors)
			{
				this.compiledType = null;
				return false;
			}
			else
			{
				this.compiledType = results.CompiledAssembly.GetType(ns + "." + classname);
				return true;
			}
		}

		#endregion

		public void Dispose()
		{
			if (this.filesToDelete != null)
			{
				foreach (string filename in this.filesToDelete)
				{
					try
					{
						if (File.Exists(filename)) File.Delete(filename);
					}
					catch (IOException) { 
					}
				}
				this.filesToDelete = null;
			}
		}

		protected abstract string GetCodeTemplate();

		protected abstract void AppendImport(StringBuilder imports, string nameSpace, string alias);

		protected abstract void AppendField(StringBuilder fields, string name, string type);

		protected abstract void AppendFieldInitializer(StringBuilder fieldinits, string name, string type);

		protected abstract void AppendConstructorParam(StringBuilder constructorparams, string name, string type);

		protected abstract void AppendLinePragmaBegin(StringBuilder code, string filename, int line);

		protected abstract void AppendLinePragmaEnd(StringBuilder code);

		/// <summary>
		/// Translates the string into a method call to render the line.
		/// </summary>
		protected abstract string LineToWriteCall(string line);

		protected abstract CodeDomProvider GetCodeDomProvider();

		private static IEnumerable<string> LinesWithBreaks(string text)
		{
			List<string> lines = new List<string>();
			foreach (Match match in RxLinesWithLinebreaks.Matches(text))
			{
				string line = match.Value;
				if (line == String.Empty) break;
				lines.Add(line);
			}
			return lines;
		}

		private static string ToContent(string text)
		{
			text = text.Replace("\\\r", "\\r");
			text = text.Replace("\\\n", "\\n");
			return text;
		}
	}
}
