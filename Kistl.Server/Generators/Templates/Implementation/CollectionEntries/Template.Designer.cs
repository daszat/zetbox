using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Implementation.CollectionEntries
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst")]
    public partial class Template : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;


        public Template(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }
        
        public override void Generate()
        {
#line 14 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
ApplyClassAttributeTemplate();

#line 16 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("    [System.Diagnostics.DebuggerDisplay(\"",  GetCeClassName() , "\")]\r\n");
this.WriteObjects("    public class ",  GetCeClassName() , " ",  GetInheritance() , "\r\n");
this.WriteObjects("    {\r\n");
this.WriteObjects("		[Obsolete]\r\n");
this.WriteObjects("		public ",  GetCeClassName() , "() : base(null) {}\r\n");
this.WriteObjects("		\r\n");
this.WriteObjects("		public ",  GetCeClassName() , "(Func<IReadOnlyKistlContext> lazyCtx)\r\n");
this.WriteObjects("			: base(lazyCtx)\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("    \r\n");
#line 28 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
ApplyIdPropertyTemplate();
	if(ImplementsIExportable())
	{
		ApplyExportGuidPropertyTemplate();
    }
    ApplyRelationIdPropertyTemplate();
    ApplyObjectGetterTemplate();

#line 36 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// Reference to the A-Side member of this CollectionEntry\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 41 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
ApplyAPropertyTemplate();

#line 43 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// the B-side value of this CollectionEntry\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 48 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
ApplyBPropertyTemplate();



    if (IsOrdered())
    {

#line 55 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// Index into the A-side list of this relation\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 60 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
ApplyAIndexPropertyTemplate();

#line 62 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// Index into the B-side list of this relation\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 67 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
ApplyBIndexPropertyTemplate();
    }

#line 70 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("#region Serializer\r\n");
this.WriteObjects("\r\n");
#line 74 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
Implementation.ObjectClasses.SerializerTemplate.Call(Host, ctx,
        Templates.Implementation.SerializerDirection.ToStream, this.MembersToSerialize, true, false);
    
    Implementation.ObjectClasses.SerializerTemplate.Call(Host, ctx,
        Templates.Implementation.SerializerDirection.FromStream, this.MembersToSerialize, true, false);

    Implementation.ObjectClasses.SerializerTemplate.Call(Host, ctx,
        Templates.Implementation.SerializerDirection.ToXmlStream, this.MembersToSerialize, true, false);
    
    Implementation.ObjectClasses.SerializerTemplate.Call(Host, ctx,
        Templates.Implementation.SerializerDirection.FromXmlStream, this.MembersToSerialize, true, false);

	if(ImplementsIExportable())
	{
		Implementation.ObjectClasses.SerializerTemplate.Call(Host, ctx,
			Templates.Implementation.SerializerDirection.Export, this.MembersToSerialize, false, HasExportGuid());
	    
		Implementation.ObjectClasses.SerializerTemplate.Call(Host, ctx,
			Templates.Implementation.SerializerDirection.MergeImport, this.MembersToSerialize, false, HasExportGuid());
	}

#line 95 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("#endregion\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		public override Type GetImplementedInterface()\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			return typeof(",  GetCeInterface() , ");\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("	\r\n");
this.WriteObjects("		public override void ReloadReferences()\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			// Do not reload references if the current object has been deleted.\r\n");
this.WriteObjects("			// TODO: enable when MemoryContext uses MemoryDataObjects\r\n");
this.WriteObjects("			//if (this.ObjectState == DataObjectState.Deleted) return;\r\n");
#line 109 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
ApplyReloadReferenceBody();

#line 111 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("		\r\n");
this.WriteObjects("		public override void ApplyChangesFrom(IPersistenceObject obj)\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			base.ApplyChangesFrom(obj);\r\n");
this.WriteObjects("			var other = (",  GetCeClassName() , ")obj;\r\n");
this.WriteObjects("			var me = (",  GetCeClassName() , ")this;\r\n");
this.WriteObjects("			\r\n");
#line 121 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
ApplyChangesFromBody();

#line 123 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("		}		\r\n");
this.WriteObjects("		\r\n");
this.WriteObjects("		\r\n");
#line 127 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
ApplyClassTailTemplate();

#line 129 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    }\r\n");

        }



    }
}