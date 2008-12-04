using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom.Compiler;
using Arebis.Utils;
using Arebis.CodeGeneration;

namespace Arebis.CodeGenerator.Templated
{
	[System.Diagnostics.DebuggerStepThrough]
	public class CSCodeBuilder : BaseCodeBuilder
	{
		protected override void AppendConstructorParam(StringBuilder constructorparams, string name, string type)
		{
			constructorparams.Append(", ");
			constructorparams.Append(type);
			constructorparams.Append(" ");
			constructorparams.Append(name);
		}

		protected override void AppendField(StringBuilder fields, string name, string type)
		{
			fields.Append("private ");
			fields.Append(type);
			fields.Append(' ');
			fields.Append(name);
			fields.AppendLine(";");
		}

		protected override void AppendFieldInitializer(StringBuilder fieldinits, string name, string type)
		{
			fieldinits.Append("this.");
			fieldinits.Append(name);
			fieldinits.Append(" = ");
			fieldinits.Append(name);
			fieldinits.AppendLine(";");
		}

		protected override void AppendImport(StringBuilder imports, string nameSpace, string alias)
		{
			imports.Append("using ");
			if (alias != null)
			{
				imports.Append(alias);
				imports.Append("=");
			}
			imports.Append(nameSpace);
			imports.AppendLine(";");
		}

		protected override void AppendLinePragmaBegin(StringBuilder code, string filename, int line)
		{
			code.AppendLine(String.Format("#line {1} \"{0}\"", filename, line));
		}

		protected override void AppendLinePragmaEnd(StringBuilder code)
		{ }
		
		protected override CodeDomProvider GetCodeDomProvider()
		{
			return CodeDomProvider.CreateProvider("c#");
		}

		protected override string GetCodeTemplate()
		{
			return Properties.Resources.CSCodeTemplate;
		}

		protected override string LineToWriteCall(string line)
		{
			StringBuilder result = new StringBuilder(line.Length * 2);

			string[] parts = StringUtils.SplitByTokens(line, GenerationLanguage.ExpressionTokens, false, true, StringComparison.InvariantCulture);

			result.Append("this.WriteObjects(\"");
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
					str = str.Replace("\\", "\\\\");
					str = str.Replace("\"", "\\\"");
					str = str.Replace("\r", "\\\r");
					str = str.Replace("\n", "\\\n");
					result.Append(str);
				}
				isExpression = !isExpression;
			}
			result.Append("\");");

			return result.ToString();
		}
	}
}
