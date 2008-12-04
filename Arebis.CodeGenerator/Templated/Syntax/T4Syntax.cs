using System;
using System.Collections.Generic;
using System.Text;
using Arebis.CodeGeneration;
using System.Text.RegularExpressions;
using System.Collections.Specialized;

namespace Arebis.CodeGenerator.Templated.Syntax
{
	public class T4Syntax : IGenerationSetup
	{
		public void Setup(NameValueCollection settings)
		{
			GenerationLanguage.CodeTemplateElementName = "template";
			GenerationLanguage.RxComments = new Regex("<#--( |\\t)*\\r?\\n?(?<comment>(?s:.*?))( |\\t)*\\r?\\n?--#>(\\w*\\r?\\n)?");
			GenerationLanguage.RxIncludes = new Regex("<#@(?i:\\s*include\\s+file\\s*=\\s*(?<quote>[\"']?)(?<filename>[^\"']*?)\\k<quote>\\s*)#>(\\w*\\r?\\n)?");
			GenerationLanguage.RxDirectives = new Regex("(<#@\\s*(?<elementName>\\w+)(\\s+(?<name>\\w+)=(?<quote>[\"']?)(?<value>.*?)\\k<quote>)*\\s*#>(\\w*\\r?\\n)?)+");
			GenerationLanguage.RxScriptlets = new Regex("<#(?![@=$#%])\\s*\\r?\\n?(?<body>[^=]((.|\\r|\\n)*?(<#=(.|\\r|\\n)*?#>)?)*)#>(\\w*\\r?\\n)?");
			GenerationLanguage.RxScripts = new Regex("<#+\\s*\\r?\\n?(?<body>((.|\\r|\\n)*?(\\w*@[^\\r\\n]*\\r?\\n)*)*)#>(\\w*\\r?\\n)?");
			GenerationLanguage.RxEmbeddedBody = new Regex("(?<=(^|\\r?\\n))(([ \\t])*@(?<body>[^\\r\\n]*?\\r?\\n))+");
			GenerationLanguage.ExpressionTokens = new string[] { "<#=", "#>" };
		}
	}
}
