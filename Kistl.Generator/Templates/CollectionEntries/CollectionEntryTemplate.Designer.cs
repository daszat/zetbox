using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.CollectionEntries
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst")]
    public partial class CollectionEntryTemplate : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;


        public CollectionEntryTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }
        
        public override void Generate()
        {
#line 14 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyClassAttributeTemplate();

#line 16 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("    [System.Diagnostics.DebuggerDisplay(\"",  GetCeClassName() , "\")]\r\n");
this.WriteObjects("    public class ",  GetCeClassName() , " ",  GetInheritance() , "\r\n");
this.WriteObjects("    {\r\n");
this.WriteObjects("        [Obsolete]\r\n");
this.WriteObjects("        public ",  GetCeClassName() , "() : base(null) { }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public ",  GetCeClassName() , "(Func<IFrozenContext> lazyCtx)\r\n");
this.WriteObjects("            : base(lazyCtx)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
#line 28 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyClassHeadTemplate();

#line 30 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// the A-side value of this CollectionEntry\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 35 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyAPropertyTemplate();

#line 37 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// the B-side value of this CollectionEntry\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 42 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyBPropertyTemplate();


    if (IsOrdered())
    {

#line 48 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// Index into the A-side list of this relation\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 53 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyAIndexPropertyTemplate();

#line 55 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// Index into the B-side list of this relation\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 60 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyBIndexPropertyTemplate();
    }

#line 63 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        #region Serializer\r\n");
this.WriteObjects("\r\n");
#line 67 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
Serialization.SerializerTemplate.Call(Host, ctx,
        Serialization.SerializerDirection.ToStream, this.MembersToSerialize, true, false);

    Serialization.SerializerTemplate.Call(Host, ctx,
        Serialization.SerializerDirection.FromStream, this.MembersToSerialize, true, false);

    Serialization.SerializerTemplate.Call(Host, ctx,
        Serialization.SerializerDirection.ToXmlStream, this.MembersToSerialize, true, false);

    Serialization.SerializerTemplate.Call(Host, ctx,
        Serialization.SerializerDirection.FromXmlStream, this.MembersToSerialize, true, false);

    if(IsExportable())
    {
        Serialization.SerializerTemplate.Call(Host, ctx,
            Serialization.SerializerDirection.Export, this.MembersToSerialize, false, true);

        Serialization.SerializerTemplate.Call(Host, ctx,
            Serialization.SerializerDirection.MergeImport, this.MembersToSerialize, false, true);
    }

#line 88 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        #endregion\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public override Type GetImplementedInterface()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            return typeof(",  GetCeInterface() , ");\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public override void ApplyChangesFrom(IPersistenceObject obj)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.ApplyChangesFrom(obj);\r\n");
this.WriteObjects("            var other = (",  GetCeClassName() , ")obj;\r\n");
this.WriteObjects("            var me = (",  GetCeClassName() , ")this;\r\n");
this.WriteObjects("\r\n");
#line 103 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyChangesFromBody();

#line 105 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public override void ReloadReferences()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            // Do not reload references if the current object has been deleted.\r\n");
this.WriteObjects("            // TODO: enable when MemoryContext uses MemoryDataObjects\r\n");
this.WriteObjects("            //if (this.ObjectState == DataObjectState.Deleted) return;\r\n");
#line 114 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyReloadReferenceBody();

#line 116 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
#line 120 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyClassTailTemplate();

#line 122 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("    }\r\n");

        }



    }
}