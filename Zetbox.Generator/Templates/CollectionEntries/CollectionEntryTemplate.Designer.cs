using System;
using System.Collections.Generic;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.CollectionEntries
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst")]
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
#line 17 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("");
#line 29 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("    // BEGIN ",  this.GetType().FullName , "\n");
#line 31 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyClassAttributeTemplate();

#line 33 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("    [System.Diagnostics.DebuggerDisplay(\"",  GetCeClassName() , "\")]\n");
this.WriteObjects("    public class ",  GetCeClassName() , " ",  GetInheritance() , "\n");
this.WriteObjects("    {\n");
#line 36 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyConstructorTemplate(); 
#line 37 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyClassHeadTemplate(); 
#line 38 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\n");
this.WriteObjects("        /// <summary>\n");
this.WriteObjects("        /// the A-side value of this CollectionEntry\n");
this.WriteObjects("        /// </summary>\n");
#line 43 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyAPropertyTemplate();

#line 45 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\n");
this.WriteObjects("        /// <summary>\n");
this.WriteObjects("        /// the B-side value of this CollectionEntry\n");
this.WriteObjects("        /// </summary>\n");
#line 50 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyBPropertyTemplate();


    if (IsOrdered())
    {

#line 56 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\n");
this.WriteObjects("        /// <summary>\n");
this.WriteObjects("        /// Index into the A-side list of this relation\n");
this.WriteObjects("        /// </summary>\n");
#line 61 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyAIndexPropertyTemplate();

#line 63 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\n");
this.WriteObjects("        /// <summary>\n");
this.WriteObjects("        /// Index into the B-side list of this relation\n");
this.WriteObjects("        /// </summary>\n");
#line 68 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyBIndexPropertyTemplate();
    }

#line 71 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\n");
this.WriteObjects("        #region Serializer\n");
this.WriteObjects("\n");
#line 75 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
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

#line 90 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\n");
this.WriteObjects("        #endregion\n");
this.WriteObjects("\n");
this.WriteObjects("        public override Type GetImplementedInterface()\n");
this.WriteObjects("        {\n");
this.WriteObjects("            return typeof(",  GetCeInterface() , ");\n");
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("        public override void ApplyChangesFrom(IPersistenceObject obj)\n");
this.WriteObjects("        {\n");
this.WriteObjects("            base.ApplyChangesFrom(obj);\n");
this.WriteObjects("            var other = (",  GetCeClassName() , ")obj;\n");
this.WriteObjects("            var me = (",  GetCeClassName() , ")this;\n");
this.WriteObjects("\n");
#line 105 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyChangesFromBody();

#line 107 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("\n");
this.WriteObjects("        public override void ReloadReferences()\n");
this.WriteObjects("        {\n");
this.WriteObjects("            // Do not reload references if the current object has been deleted.\n");
this.WriteObjects("            // TODO: enable when MemoryContext uses MemoryDataObjects\n");
this.WriteObjects("            //if (this.ObjectState == DataObjectState.Deleted) return;\n");
#line 116 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyReloadReferenceBody();

#line 118 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("\n");
this.WriteObjects("        }\n");
this.WriteObjects("\n");
#line 122 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
ApplyClassTailTemplate();

#line 124 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\CollectionEntryTemplate.cst"
this.WriteObjects("    }\n");
this.WriteObjects("    // END ",  this.GetType().FullName , "\n");

        }

    }
}