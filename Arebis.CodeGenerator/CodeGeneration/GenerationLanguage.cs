using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Arebis.CodeGeneration
{
	public static class GenerationLanguage
	{
		public static Dictionary<string, Type> CodeBuilders = new Dictionary<string,Type>();

		public static string CodeTemplateElementName = "Template";

		public static Regex RxComments = null;
		public static Regex RxIncludes = null;
		public static Regex RxDirectives = null;
		public static Regex RxScriptlets = null;
		public static Regex RxScripts = null;
		public static Regex RxEmbeddedBody = null;
		public static string[] ExpressionTokens = new string[] { null, null };

		public static string DefaultNameSpace = "Arebis.DynamicAssembly";
		public static string DefaultBaseClass = "Arebis.CodeGeneration.CodeTemplate";

		public static string DefaultTemplateLanguage = "c#";
	}
}
