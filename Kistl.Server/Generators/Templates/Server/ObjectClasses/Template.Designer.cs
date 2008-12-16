using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Server.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\Template.cst")]
    public partial class Template : Kistl.Server.Generators.KistlCodeTemplate
    {
		private Kistl.App.Base.ObjectClass dataType;


        public Template(Arebis.CodeGeneration.IGenerationHost _host, Kistl.App.Base.ObjectClass dataType)
            : base(_host)
        {
			this.dataType = dataType;

        }
        
        public override void Generate()
        {
#line 12 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\Template.cst"
this.WriteObjects("\r\n");
#line 14 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\Template.cst"
this.WriteObjects("namespace ",  DataType.Module.Namespace , "\r\n");
this.WriteObjects("{\r\n");
this.WriteObjects("    using System;\r\n");
this.WriteObjects("    using System.Collections;\r\n");
this.WriteObjects("    using System.Collections.Generic;\r\n");
this.WriteObjects("    using System.Collections.ObjectModel;\r\n");
this.WriteObjects("    using System.Linq;\r\n");
this.WriteObjects("    using System.Text;\r\n");
this.WriteObjects("    using System.Xml;\r\n");
this.WriteObjects("    using System.Xml.Serialization;\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("    using Kistl.API;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("",  GetAdditionalImports() , "\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("    /// <summary>\r\n");
this.WriteObjects("    /// ",  dataType.Description , "\r\n");
this.WriteObjects("    /// </summary>\r\n");
#line 34 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\Template.cst"
var mungedClassName = MungeClassName(dataType.ClassName);

ApplyClassAttributeTemplate();


#line 39 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\Template.cst"
this.WriteObjects("    public class ",  mungedClassName , " ",  GetInheritance() , "\r\n");
this.WriteObjects("    {	\r\n");
this.WriteObjects("    \r\n");
#line 42 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\Template.cst"
ApplyIDPropertyTemplate(); 
#line 43 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\Template.cst"
this.WriteObjects("        \r\n");
this.WriteObjects("        ");
#line 44 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\Template.cst"
foreach(Property p in dataType.Properties)
		{ 
#line 46 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\Template.cst"
this.WriteObjects("		\r\n");
this.WriteObjects("		\r\n");
this.WriteObjects("		/// <summary>\r\n");
this.WriteObjects("        /// ",  p.Description , "\r\n");
this.WriteObjects("        /// </summary> ");
#line 51 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\Template.cst"
ApplyPropertyTemplate(p);
		}
		
		foreach(var m in MethodsToGenerate())
		{ 
#line 56 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\Template.cst"
this.WriteObjects("		\r\n");
this.WriteObjects("		\r\n");
this.WriteObjects("		/// <summary>\r\n");
this.WriteObjects("        /// ",  m.Description , "\r\n");
this.WriteObjects("        /// </summary> ");
#line 61 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\Template.cst"
ApplyMethodTemplate(m);
		}
		
#line 63 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\Template.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("	}\r\n");
this.WriteObjects("}");

        }



    }
}