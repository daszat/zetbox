using System;
using System.Collections.Generic;
using System.Linq;
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
#line 17 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
ApplyGlobalPreambleTemplate();


#line 20 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
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
#line 35 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
foreach(string ns in GetAdditionalImports())
    {

#line 38 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
this.WriteObjects("    using ",  ns , ";\r\n");
#line 40 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
}
    
    ApplyNamespacePreambleTemplate();

#line 44 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    /// <summary>\r\n");
this.WriteObjects("    /// ",  DataType.Description , "\r\n");
this.WriteObjects("    /// </summary>\r\n");
#line 50 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
var mungedClassName = GetTypeName();

    ApplyClassAttributeTemplate();

#line 54 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
this.WriteObjects("    [System.Diagnostics.DebuggerDisplay(\"",  DataType.ClassName , "\")]\r\n");
this.WriteObjects("    public class ",  mungedClassName , " ",  GetInheritance() , "\r\n");
this.WriteObjects("    {\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("		public ",  mungedClassName , "()\r\n");
this.WriteObjects("		{\r\n");
#line 61 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
ApplyConstructorTemplate();

#line 63 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
#line 66 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
ApplyIDPropertyTemplate();

		// TODO: decouple serializing format from PropertyName order
        foreach(Property p in DataType.Properties.OrderBy(p => p.PropertyName))
        {

#line 72 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// ",  p.Description , "\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 77 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
ApplyPropertyTemplate(p);
        }

        foreach(var m in MethodsToGenerate().OrderBy(m => m.MethodName))
        {

#line 83 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// ",  m.Description , "\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 88 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
ApplyMethodTemplate(m);
        }

#line 91 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("		public override InterfaceType GetInterfaceType()\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			return new InterfaceType(typeof(",  DataType.ClassName , "));\r\n");
this.WriteObjects("		}\r\n");
#line 97 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
ApplyClassTailTemplate();

#line 99 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("#region Serializer\r\n");
this.WriteObjects("\r\n");
#line 104 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
CallTemplate("Implementation.ObjectClasses.SerializerTemplate", ctx,
			SerializerDirection.ToStream, this.MembersToSerialize);
		
		CallTemplate("Implementation.ObjectClasses.SerializerTemplate", ctx,
			SerializerDirection.FromStream, this.MembersToSerialize);

#line 110 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("#endregion\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("\r\n");
#line 116 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
ApplyNamespaceTailTemplate();

#line 118 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("}");

        }



    }
}