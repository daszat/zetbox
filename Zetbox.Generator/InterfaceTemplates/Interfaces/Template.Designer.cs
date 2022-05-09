using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.InterfaceTemplates.Interfaces
{
    [Arebis.CodeGeneration.TemplateInfo(@"C:\projects\zetbox\Zetbox.Generator\InterfaceTemplates\Interfaces\Template.cst")]
    public partial class Template : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected DataType dataType;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, DataType dataType)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Interfaces.Template", ctx, dataType);
        }

        public Template(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, DataType dataType)
            : base(_host)
        {
			this.ctx = ctx;
			this.dataType = dataType;

        }

        public override void Generate()
        {
#line 30 "C:\projects\zetbox\Zetbox.Generator\InterfaceTemplates\Interfaces\Template.cst"
this.WriteObjects("// <autogenerated/>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("namespace ",  dataType.Module.Namespace , "\r\n");
this.WriteObjects("{\r\n");
this.WriteObjects("    using System;\r\n");
this.WriteObjects("    using System.Collections.Generic;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    using Zetbox.API;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    /// <summary>\r\n");
this.WriteObjects("    /// ",  UglyXmlEncode(dataType.Description) , "\r\n");
this.WriteObjects("    /// </summary>\r\n");
this.WriteObjects("    [Zetbox.API.DefinitionGuid(\"",  dataType.ExportGuid , "\")]\r\n");
this.WriteObjects("    public interface ",  dataType.Name , " ",  GetInheritance() , " \r\n");
this.WriteObjects("    {\r\n");
#line 46 "C:\projects\zetbox\Zetbox.Generator\InterfaceTemplates\Interfaces\Template.cst"
foreach(Property p in dataType.Properties.OrderBy(p => p.Name))
    {
        if (!IsDeclaredInImplementsInterface(p))
        {

#line 51 "C:\projects\zetbox\Zetbox.Generator\InterfaceTemplates\Interfaces\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// ",  UglyXmlEncode(p.Description) , "\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 56 "C:\projects\zetbox\Zetbox.Generator\InterfaceTemplates\Interfaces\Template.cst"
ApplyPropertyTemplate(p);
        }
    }

    foreach(var mg in MethodsToGenerate().GroupBy(m => m.Name).OrderBy(mg => mg.Key))
    {
        int index = 0;
        foreach(var m in mg.OrderByDefault())
        {
            if (!IsDeclaredInImplementsInterface(m))
            {

#line 68 "C:\projects\zetbox\Zetbox.Generator\InterfaceTemplates\Interfaces\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// ",  UglyXmlEncode(m.Description) , "\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 73 "C:\projects\zetbox\Zetbox.Generator\InterfaceTemplates\Interfaces\Template.cst"
ApplyMethodTemplate(m, index++);
            }
        }
    }

#line 78 "C:\projects\zetbox\Zetbox.Generator\InterfaceTemplates\Interfaces\Template.cst"
this.WriteObjects("    }\r\n");
this.WriteObjects("}\r\n");

        }

    }
}