using System;
using System.Collections.Generic;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.CollectionEntries
{
    [Arebis.CodeGeneration.TemplateInfo(@"C:\projects\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst")]
    public partial class CollectionEntryTemplate : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("CollectionEntries.CollectionEntryTemplate", ctx);
        }

        public CollectionEntryTemplate(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }

        public override void Generate()
        {
#line 29 "C:\projects\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("    // BEGIN ",  this.GetType().FullName , "\r\n");
#line 31 "C:\projects\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyClassAttributeTemplate();

#line 33 "C:\projects\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("    [System.Diagnostics.DebuggerDisplay(\"",  GetCeClassName() , "\")]\r\n");
this.WriteObjects("    public class ",  GetCeClassName() , " ",  GetInheritance() , "\r\n");
this.WriteObjects("    {\r\n");
#line 36 "C:\projects\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyConstructorTemplate(); 
#line 37 "C:\projects\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyClassHeadTemplate(); 
#line 38 "C:\projects\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// the A-side value of this CollectionEntry\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 43 "C:\projects\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyAPropertyTemplate();

#line 45 "C:\projects\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// the B-side value of this CollectionEntry\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 50 "C:\projects\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyBPropertyTemplate();


    if (IsOrdered())
    {

#line 56 "C:\projects\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// Index into the A-side list of this relation\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 61 "C:\projects\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyAIndexPropertyTemplate();

#line 63 "C:\projects\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// Index into the B-side list of this relation\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 68 "C:\projects\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyBIndexPropertyTemplate();
    }

#line 71 "C:\projects\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        #region Serializer\r\n");
this.WriteObjects("\r\n");
#line 75 "C:\projects\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
Serialization.SerializerTemplate.Call(Host, ctx,
        Serialization.SerializerDirection.ToStream, this.MembersToSerialize, true, null);

    Serialization.SerializerTemplate.Call(Host, ctx,
        Serialization.SerializerDirection.FromStream, this.MembersToSerialize, true, null);

    if (IsExportable())
    {
        Serialization.SerializerTemplate.Call(Host, ctx,
            Serialization.SerializerDirection.Export, this.MembersToSerialize, false, GetExportGuidBackingStoreReference());

        Serialization.SerializerTemplate.Call(Host, ctx,
            Serialization.SerializerDirection.MergeImport, this.MembersToSerialize, false, GetExportGuidBackingStoreReference());
    }

#line 90 "C:\projects\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
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
#line 105 "C:\projects\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyChangesFromBody();

#line 107 "C:\projects\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public override async System.Threading.Tasks.Task ReloadReferences()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            await await base.ReloadReferences();\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            // Do not reload references if the current object has been deleted.\r\n");
this.WriteObjects("            // TODO: enable when MemoryContext uses MemoryDataObjects\r\n");
this.WriteObjects("            //if (this.ObjectState == DataObjectState.Deleted) return;\r\n");
#line 118 "C:\projects\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyReloadReferenceBody();

#line 120 "C:\projects\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
#line 124 "C:\projects\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyClassTailTemplate();

#line 126 "C:\projects\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("    }\r\n");
this.WriteObjects("    // END ",  this.GetType().FullName , "\r\n");

        }

    }
}