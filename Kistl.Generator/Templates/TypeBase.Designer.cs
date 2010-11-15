using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\TypeBase.cst")]
    public partial class TypeBase : Kistl.Generator.ResourceTemplate
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
#line 16 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
this.WriteObjects("// <autogenerated/>\r\n");
this.WriteObjects("\r\n");
#line 20 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
ApplyGlobalPreambleTemplate();


#line 23 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
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
#line 37 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
foreach(string ns in GetAdditionalImports().Distinct().OrderBy(s => s))
    {

#line 40 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
this.WriteObjects("    using ",  ns , ";\r\n");
#line 42 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
}
    
    ApplyNamespacePreambleTemplate();

#line 46 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    /// <summary>\r\n");
this.WriteObjects("    /// ",  DataType.Description , "\r\n");
this.WriteObjects("    /// </summary>\r\n");
#line 51 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
var mungedClassName = GetTypeName();

    ApplyClassAttributeTemplate();

#line 55 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
this.WriteObjects("    [System.Diagnostics.DebuggerDisplay(\"",  DataType.Name , "\")]\r\n");
this.WriteObjects("    public",  GetClassModifiers() , " class ",  mungedClassName , " ",  GetInheritance() , "\r\n");
this.WriteObjects("    {\r\n");
this.WriteObjects("        [Obsolete]\r\n");
this.WriteObjects("        public ",  mungedClassName , "()\r\n");
this.WriteObjects("            : base(null)\r\n");
this.WriteObjects("        {\r\n");
#line 62 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
ApplyConstructorBodyTemplate(); 
#line 63 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public ",  mungedClassName , "(Func<IFrozenContext> lazyCtx)\r\n");
this.WriteObjects("            : base(lazyCtx)\r\n");
this.WriteObjects("        {\r\n");
#line 68 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
ApplyConstructorBodyTemplate(); 
#line 69 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
#line 71 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
ApplyExtraConstructorTemplate(); 
#line 72 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
this.WriteObjects("\r\n");
#line 74 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
// TODO: decouple serializing format from Name order
        foreach(Property p in DataType.Properties.OrderBy(p => p.Name))
        {

#line 78 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// ",  p.Description , "\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 83 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
ApplyPropertyTemplate(p);
        }

        foreach(var mg in MethodsToGenerate().GroupBy(m => m.Name).OrderBy(mg => mg.Key))
        {
            int index = 0;
            foreach(var m in mg.OrderByDefault())
            {

#line 92 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// ",  m.Description , "\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 97 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
ApplyMethodTemplate(m, index++);
            }
        }

#line 101 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public override Type GetImplementedInterface()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            return typeof(",  DataType.Name , ");\r\n");
this.WriteObjects("        }\r\n");
#line 107 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
ApplyApplyChangesFromMethod();
        ApplyAttachToContextMethod();
        ApplyClassTailTemplate();

#line 111 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        #region Serializer\r\n");
this.WriteObjects("\r\n");
#line 115 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
Serialization.SerializerTemplate.Call(Host, ctx,
            Serialization.SerializerDirection.ToStream, this.MembersToSerialize, true, false);
        
        Serialization.SerializerTemplate.Call(Host, ctx,
            Serialization.SerializerDirection.FromStream, this.MembersToSerialize, true, false);

        Serialization.SerializerTemplate.Call(Host, ctx,
            Serialization.SerializerDirection.ToXmlStream, this.MembersToSerialize, true, false);
        
        Serialization.SerializerTemplate.Call(Host, ctx,
            Serialization.SerializerDirection.FromXmlStream, this.MembersToSerialize, true, false);

        if((DataType is ObjectClass) && ((ObjectClass)DataType).ImplementsIExportable())
        {
            ObjectClass cls = (ObjectClass)DataType;            
            Serialization.SerializerTemplate.Call(Host, ctx,
                Serialization.SerializerDirection.Export, this.MembersToSerialize, cls.BaseObjectClass != null, true);
            
            Serialization.SerializerTemplate.Call(Host, ctx,
                Serialization.SerializerDirection.MergeImport, this.MembersToSerialize, cls.BaseObjectClass != null, true);
        }

#line 137 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        #endregion\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    }\r\n");
#line 142 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
ApplyNamespaceTailTemplate();

#line 144 "P:\Kistl\Kistl.Generator\Templates\TypeBase.cst"
this.WriteObjects("}");

        }



    }
}