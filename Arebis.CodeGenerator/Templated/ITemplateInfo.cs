using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using Arebis.CodeGeneration;
using Arebis.Parsing.MultiContent;

namespace Arebis.CodeGenerator.Templated
{
	public interface ITemplateInfo
	{
		IGenerationHost Host { get; }
		NameValueCollection Settings { get; }
		FileInfo TemplateFileInfo { get; }
		MixedContentFile FileContent { get; }
		IList<NameValueCollection> GetDirectives(string directiveName);
		NameValueCollection GetCodeTemplateDirective();

		string FindFile(string relativeName);
	}
}
