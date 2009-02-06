using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Interface.DataTypes
{
    [Arebis.CodeGeneration.TemplateInfo(@"C:\temp\Kistl2\Kistl.Server\Generators\Templates\Interface\DataTypes\Template.cst")]
    public partial class Template : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Kistl.App.Base.DataType dataType;


        public Template(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.App.Base.DataType dataType)
            : base(_host)
        {
			this.ctx = ctx;
			this.dataType = dataType;

        }
        
        public override void Generate()
        {
#line 13 "C:\temp\Kistl2\Kistl.Server\Generators\Templates\Interface\DataTypes\Template.cst"
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
this.WriteObjects("    public interface ",  dataType.ClassName , " ",  GetInheritance() , " \r\n");
this.WriteObjects("    {\r\n");
#line 27 "C:\temp\Kistl2\Kistl.Server\Generators\Templates\Interface\DataTypes\Template.cst"
foreach(Property p in dataType.Properties)
    {

#line 30 "C:\temp\Kistl2\Kistl.Server\Generators\Templates\Interface\DataTypes\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// ",  p.Description , "\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 35 "C:\temp\Kistl2\Kistl.Server\Generators\Templates\Interface\DataTypes\Template.cst"
ApplyPropertyTemplate(p);
    }



    foreach(var m in MethodsToGenerate())
    {

#line 43 "C:\temp\Kistl2\Kistl.Server\Generators\Templates\Interface\DataTypes\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// ",  m.Description , "\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 48 "C:\temp\Kistl2\Kistl.Server\Generators\Templates\Interface\DataTypes\Template.cst"
ApplyMethodTemplate(m);
    }

#line 51 "C:\temp\Kistl2\Kistl.Server\Generators\Templates\Interface\DataTypes\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("}");

        }



    }
}