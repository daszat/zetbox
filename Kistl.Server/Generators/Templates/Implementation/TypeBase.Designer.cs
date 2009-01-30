using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Implementation
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst")]
    public partial class TypeBase : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected DataType DataType;


        public TypeBase(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, DataType DataType)
            : base(_host)
        {
			this.ctx = ctx;
			this.DataType = DataType;

        }
        
        public override void Generate()
        {
#line 15 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
ApplyGlobalPreambleTemplate();


#line 18 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
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
this.WriteObjects("\r\n");
this.WriteObjects("    using Kistl.API;\r\n");
this.WriteObjects("\r\n");
#line 32 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
foreach(string ns in GetAdditionalImports())
	{

#line 35 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
this.WriteObjects("	using ",  ns , ";\r\n");
#line 37 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
}
	
	ApplyNamespacePreambleTemplate();

#line 41 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    /// <summary>\r\n");
this.WriteObjects("    /// ",  DataType.Description , "\r\n");
this.WriteObjects("    /// </summary>\r\n");
#line 47 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
var mungedClassName = MungeClassName(DataType.ClassName);

	ApplyClassAttributeTemplate();

#line 51 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
this.WriteObjects("    [System.Diagnostics.DebuggerDisplay(\"",  DataType.ClassName , "\")]\r\n");
this.WriteObjects("    public class ",  mungedClassName , " ",  GetInheritance() , "\r\n");
this.WriteObjects("    {\r\n");
this.WriteObjects("\r\n");
#line 56 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
ApplyIDPropertyTemplate();

		foreach(Property p in DataType.Properties)
		{

#line 61 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// ",  p.Description , "\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 66 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
ApplyPropertyTemplate(p);
		}

		foreach(var m in MethodsToGenerate())
		{

#line 72 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// ",  m.Description , "\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 77 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
ApplyMethodTemplate(m);
		}

#line 80 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("	}\r\n");
this.WriteObjects("\r\n");
#line 84 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
ApplyNamespaceTailTemplate();

#line 86 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
this.WriteObjects("}");

        }



    }
}