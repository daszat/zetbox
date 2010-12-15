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
#line 19 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyConstructorTemplate(); 
#line 20 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyClassHeadTemplate(); 
#line 21 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// the A-side value of this CollectionEntry\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 26 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyAPropertyTemplate();

#line 28 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// the B-side value of this CollectionEntry\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 33 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyBPropertyTemplate();


    if (IsOrdered())
    {

#line 39 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// Index into the A-side list of this relation\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 44 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyAIndexPropertyTemplate();

#line 46 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// Index into the B-side list of this relation\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 51 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyBIndexPropertyTemplate();
    }

#line 54 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        #region Serializer\r\n");
this.WriteObjects("\r\n");
#line 58 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
Serialization.SerializerTemplate.Call(Host, ctx,
        Serialization.SerializerDirection.ToStream, this.MembersToSerialize, true, null);

    Serialization.SerializerTemplate.Call(Host, ctx,
        Serialization.SerializerDirection.FromStream, this.MembersToSerialize, true, null);

    Serialization.SerializerTemplate.Call(Host, ctx,
        Serialization.SerializerDirection.ToXmlStream, this.MembersToSerialize, true, null);

    Serialization.SerializerTemplate.Call(Host, ctx,
        Serialization.SerializerDirection.FromXmlStream, this.MembersToSerialize, true, null);

    if(IsExportable())
    {
        Serialization.SerializerTemplate.Call(Host, ctx,
            Serialization.SerializerDirection.Export, this.MembersToSerialize, false, GetExportGuidBackingStoreReference());

        Serialization.SerializerTemplate.Call(Host, ctx,
            Serialization.SerializerDirection.MergeImport, this.MembersToSerialize, false, GetExportGuidBackingStoreReference());
    }

#line 79 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
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
#line 94 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyChangesFromBody();

#line 96 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public override void ReloadReferences()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            // Do not reload references if the current object has been deleted.\r\n");
this.WriteObjects("            // TODO: enable when MemoryContext uses MemoryDataObjects\r\n");
this.WriteObjects("            //if (this.ObjectState == DataObjectState.Deleted) return;\r\n");
#line 105 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyReloadReferenceBody();

#line 107 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
#line 111 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyClassTailTemplate();

#line 113 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("    }\r\n");

        }



    }
}