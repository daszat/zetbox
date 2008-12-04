using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom.Compiler;

namespace Arebis.CodeGenerator.Templated
{
	public interface ICodeBuilder : IDisposable
	{
		ITemplateInfo TemplateInfo { get; set; }
		bool Compile();
		Type CompiledType { get; }
		IList<CompilerError> CompilerErrors { get; }
	}
}
