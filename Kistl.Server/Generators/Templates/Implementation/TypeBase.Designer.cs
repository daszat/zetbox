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
		private IKistlContext ctx;
		private DataType DataType;


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
this.WriteObjects("\r\n");
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
#line 33 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
foreach(string ns in GetAdditionalImports())
    {

#line 36 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
this.WriteObjects("    using ",  ns , ";\r\n");
#line 38 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
}
    
    ApplyNamespacePreambleTemplate();

#line 42 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    /// <summary>\r\n");
this.WriteObjects("    /// ",  DataType.Description , "\r\n");
this.WriteObjects("    /// </summary>\r\n");
#line 48 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
var mungedClassName = MungeClassName(DataType.ClassName);

    ApplyClassAttributeTemplate();

#line 52 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
this.WriteObjects("    [System.Diagnostics.DebuggerDisplay(\"",  DataType.ClassName , "\")]\r\n");
this.WriteObjects("    public class ",  mungedClassName , " ",  GetInheritance() , "\r\n");
this.WriteObjects("    {\r\n");
this.WriteObjects("\r\n");
#line 57 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
ApplyIDPropertyTemplate();

        foreach(Property p in DataType.Properties)
        {

#line 62 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// ",  p.Description , "\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 67 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
ApplyPropertyTemplate(p);
        }

        foreach(var m in MethodsToGenerate())
        {

#line 73 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// ",  m.Description , "\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 78 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
ApplyMethodTemplate(m);
        }

        ApplyClassTailTemplate();

#line 83 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("\r\n");
#line 87 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
ApplyNamespaceTailTemplate();

#line 89 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("}");

        }



    }
}