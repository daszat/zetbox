using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.InterfaceTemplates.Interfaces
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\InterfaceTemplates\Interfaces\Template.cst")]
    public partial class Template : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected DataType dataType;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, DataType dataType)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Interfaces.Template", ctx, dataType);
        }

        public Template(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, DataType dataType)
            : base(_host)
        {
			this.ctx = ctx;
			this.dataType = dataType;

        }

        public override void Generate()
        {
#line 14 "P:\Kistl\Kistl.Generator\InterfaceTemplates\Interfaces\Template.cst"
this.WriteObjects("// <autogenerated/>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("namespace ",  dataType.Module.Namespace , "\r\n");
this.WriteObjects("{\r\n");
this.WriteObjects("    using System;\r\n");
this.WriteObjects("    using System.Collections.Generic;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    using Kistl.API;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    /// <summary>\r\n");
this.WriteObjects("    /// ",  dataType.Description , "\r\n");
this.WriteObjects("    /// </summary>\r\n");
this.WriteObjects("    public interface ",  dataType.Name , " ",  GetInheritance() , " \r\n");
this.WriteObjects("    {\r\n");
#line 29 "P:\Kistl\Kistl.Generator\InterfaceTemplates\Interfaces\Template.cst"
foreach(Property p in dataType.Properties.OrderBy(p => p.Name))
    {
        if(!IsDeclaredInImplementsInterface(p))
        {

#line 34 "P:\Kistl\Kistl.Generator\InterfaceTemplates\Interfaces\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// ",  p.Description , "\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 39 "P:\Kistl\Kistl.Generator\InterfaceTemplates\Interfaces\Template.cst"
ApplyPropertyTemplate(p);
        }
    }

    foreach(var mg in MethodsToGenerate().GroupBy(m => m.Name).OrderBy(mg => mg.Key))
    {
        int index = 0;
        foreach(var m in mg.OrderByDefault())
        {
            if(!IsDeclaredInImplementsInterface(m))
            {

#line 51 "P:\Kistl\Kistl.Generator\InterfaceTemplates\Interfaces\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// ",  m.Description , "\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 56 "P:\Kistl\Kistl.Generator\InterfaceTemplates\Interfaces\Template.cst"
ApplyMethodTemplate(m, index++);
            }
        }
    }

#line 61 "P:\Kistl\Kistl.Generator\InterfaceTemplates\Interfaces\Template.cst"
this.WriteObjects("    }\r\n");
this.WriteObjects("}\r\n");

        }

    }
}