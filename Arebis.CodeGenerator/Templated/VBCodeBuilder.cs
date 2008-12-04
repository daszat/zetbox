using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom.Compiler;
using Arebis.Utils;
using Arebis.CodeGeneration;

namespace Arebis.CodeGenerator.Templated
{
	[System.Diagnostics.DebuggerStepThrough]
	public class VBCodeBuilder : BaseCodeBuilder
	{
		protected override void AppendConstructorParam(StringBuilder constructorparams, string name, string type)
		{
			constructorparams.Append(", ByVal ");
			constructorparams.Append(name);
			constructorparams.Append(" As ");
			constructorparams.Append(type);
		}

		protected override void AppendField(StringBuilder fields, string name, string type)
		{
			fields.Append("Private ");
			fields.Append(name);
			fields.Append(" As ");
			fields.Append(type);
			fields.AppendLine();
		}

		protected override void AppendFieldInitializer(StringBuilder fieldinits, string name, string type)
		{
			fieldinits.Append("Me.");
			fieldinits.Append(name);
			fieldinits.Append(" = ");
			fieldinits.Append(name);
			fieldinits.AppendLine();
		}

		protected override void AppendImport(StringBuilder imports, string nameSpace, string alias)
		{
			imports.Append("Imports ");
			if (alias != null)
			{
				imports.Append(alias);
				imports.Append(" = ");
			}
			imports.Append(nameSpace);
			imports.AppendLine();
		}

		protected override void AppendLinePragmaBegin(StringBuilder code, string filename, int line)
		{
			code.AppendLine(String.Format("#ExternalSource(\"{0}\", {1})", filename, line));
		}

		protected override void AppendLinePragmaEnd(StringBuilder code)
		{
			code.AppendLine("#End ExternalSource");
		}

		protected override CodeDomProvider GetCodeDomProvider()
		{
			return CodeDomProvider.CreateProvider("vb");
		}

		protected override string GetCodeTemplate()
		{
			string code = Properties.Resources.VBCodeTemplate;
			code = code.Replace("<%=expliciton%>", this.TemplateInfo.GetCodeTemplateDirective()["Explicit"] ?? "Off");
			code = code.Replace("<%=stricton%>", this.TemplateInfo.GetCodeTemplateDirective()["Strict"] ?? "Off");
			return code;
		}

		protected override string LineToWriteCall(string line)
		{
			StringBuilder result = new StringBuilder(line.Length * 2);

			string[] parts = StringUtils.SplitByTokens(line, GenerationLanguage.ExpressionTokens, false, true, StringComparison.InvariantCulture);

			result.Append("Me.WriteObjects(\"");
			bool isExpression = false;
			foreach (string part in parts)
			{
				if (isExpression)
				{
					result.Append("\", ");
					result.Append(part);
					result.Append(", \"");
				}
				else
				{
					string str = part;
					str = str.Replace("\"", "\"\"");
					str = str.Replace("\r", "");
					str = str.Replace("\n", "\" & Environment.NewLine & \"");
					result.Append(str);
				}
				isExpression = !isExpression;
			}
			result.Append("\")");

			return result.ToString();
		}
	}
}
