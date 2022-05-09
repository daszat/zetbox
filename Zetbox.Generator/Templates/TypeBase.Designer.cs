using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates
{
    [Arebis.CodeGeneration.TemplateInfo(@"C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst")]
    public partial class TypeBase : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected DataType DataType;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, DataType DataType)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("TypeBase", ctx, DataType);
        }

        public TypeBase(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, DataType DataType)
            : base(_host)
        {
			this.ctx = ctx;
			this.DataType = DataType;

        }

        public override void Generate()
        {
#line 32 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
this.WriteObjects("// <autogenerated/>\r\n");
this.WriteObjects("\r\n");
#line 34 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
ApplyGlobalPreambleTemplate(); 
#line 35 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
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
this.WriteObjects("    using Zetbox.API;\r\n");
this.WriteObjects("    using Zetbox.DalProvider.Base.RelationWrappers;\r\n");
this.WriteObjects("\r\n");
#line 49 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
foreach(string ns in GetAdditionalImports().Distinct().OrderBy(s => s)) { 
#line 50 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
this.WriteObjects("    using ",  ns , ";\r\n");
#line 51 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
} 
#line 52 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
this.WriteObjects("\r\n");
#line 53 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
ApplyNamespacePreambleTemplate(); 
#line 54 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
this.WriteObjects("    /// <summary>\r\n");
this.WriteObjects("    /// ",  UglyXmlEncode(DataType.Description) , "\r\n");
this.WriteObjects("    /// </summary>\r\n");
#line 58 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
var mungedClassName = GetTypeName();

    ApplyClassAttributeTemplate();

#line 62 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
this.WriteObjects("    [System.Diagnostics.DebuggerDisplay(\"",  DataType.Name , "\")]\r\n");
this.WriteObjects("    public",  GetClassModifiers() , " class ",  mungedClassName , " ",  GetInheritance() , "\r\n");
this.WriteObjects("    {\r\n");
#line 65 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
ApplyClassHeadTemplate(); 
#line 66 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
ApplyConstructorTemplate(); 
#line 68 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
// TODO: decouple serializing format from Name order
        foreach(Property p in DataType.Properties.OrderBy(p => p.Name))
        {

#line 72 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// ",  UglyXmlEncode(p.Description) , "\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 77 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
ApplyPropertyTemplate(p);
        }

        foreach(var mg in MethodsToGenerate().GroupBy(m => m.Name).OrderBy(mg => mg.Key))
        {
            int index = 0;
            foreach(var m in mg.OrderByDefault())
            {

#line 86 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// ",  UglyXmlEncode(m.Description) , "\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 91 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
ApplyMethodTemplate(m, index++);
            }
        }

#line 95 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public override Type GetImplementedInterface()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            return typeof(",  DataType.Name , ");\r\n");
this.WriteObjects("        }\r\n");
#line 101 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
ApplyApplyChangesFromMethod();
        ApplyAttachToContextMethod();
		ApplySetNewMethod();
        ApplyClassTailTemplate();

#line 106 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        #region Serializer\r\n");
this.WriteObjects("\r\n");
#line 110 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
Serialization.SerializerTemplate.Call(Host, ctx,
            Serialization.SerializerDirection.ToStream, this.MembersToSerialize, true, null);
        
        Serialization.SerializerTemplate.Call(Host, ctx,
            Serialization.SerializerDirection.FromStream, this.MembersToSerialize, true, null);

        if ((DataType is ObjectClass) && ((ObjectClass)DataType).ImplementsIExportable().Result)
        {
            ObjectClass cls = (ObjectClass)DataType;            
            Serialization.SerializerTemplate.Call(Host, ctx,
                Serialization.SerializerDirection.Export, this.MembersToSerialize, cls.BaseObjectClass != null, GetExportGuidBackingStoreReference());
            
            Serialization.SerializerTemplate.Call(Host, ctx,
                Serialization.SerializerDirection.MergeImport, this.MembersToSerialize, cls.BaseObjectClass != null, GetExportGuidBackingStoreReference());
        } 
        else if (DataType is CompoundObject)
        {
            Serialization.SerializerTemplate.Call(Host, ctx,
                Serialization.SerializerDirection.Export, this.MembersToSerialize, true, null);
            
            Serialization.SerializerTemplate.Call(Host, ctx,
                Serialization.SerializerDirection.MergeImport, this.MembersToSerialize, true, null);
        }

#line 134 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        #endregion\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    }\r\n");
#line 138 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
ApplyNamespaceTailTemplate(); 
#line 139 "C:\projects\zetbox\Zetbox.Generator\Templates\TypeBase.cst"
this.WriteObjects("}");

        }

    }
}