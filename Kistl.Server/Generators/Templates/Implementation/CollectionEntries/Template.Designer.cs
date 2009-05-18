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
this.WriteObjects("    \r\n");
#line 21 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
ApplyIdPropertyTemplate();
    ApplyRelationIdPropertyTemplate();
    ApplyObjectGetterTemplate();

#line 25 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// Reference to the A-Side member of this CollectionEntry\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 30 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
ApplyAPropertyTemplate();

#line 32 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// the B-side value of this CollectionEntry\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 36 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
ApplyBPropertyTemplate();



    if (IsOrdered())
    {

#line 43 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// Index into the A-side list of this relation\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 50 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
ApplyAIndexPropertyTemplate();

#line 52 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// Index into the B-side list of this relation\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 57 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
ApplyBIndexPropertyTemplate();
    }

#line 60 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("#region Serializer\r\n");
this.WriteObjects("\r\n");
#line 64 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
Implementation.ObjectClasses.SerializerTemplate.Call(Host, ctx,
        Templates.Implementation.SerializerDirection.ToStream, this.MembersToSerialize);
    
    Implementation.ObjectClasses.SerializerTemplate.Call(Host, ctx,
        Templates.Implementation.SerializerDirection.FromStream, this.MembersToSerialize);

    Implementation.ObjectClasses.SerializerTemplate.Call(Host, ctx,
        Templates.Implementation.SerializerDirection.ToXmlStream, this.MembersToSerialize);
    
    Implementation.ObjectClasses.SerializerTemplate.Call(Host, ctx,
        Templates.Implementation.SerializerDirection.FromXmlStream, this.MembersToSerialize);

#line 76 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("#endregion\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		public override InterfaceType GetInterfaceType()\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			return new InterfaceType(typeof(",  GetCeInterface() , "));\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("	\r\n");
this.WriteObjects("		public override void ReloadReferences()\r\n");
this.WriteObjects("		{\r\n");
#line 87 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
ApplyReloadReferenceBody();

#line 89 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("		\r\n");
this.WriteObjects("		public override void ApplyChangesFrom(IPersistenceObject obj)\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			base.ApplyChangesFrom(obj);\r\n");
this.WriteObjects("			var other = (",  GetCeClassName() , ")obj;\r\n");
this.WriteObjects("			var me = (",  GetCeClassName() , ")this;\r\n");
this.WriteObjects("			\r\n");
#line 99 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
ApplyChangesFromBody();

#line 101 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("		}		\r\n");
this.WriteObjects("		\r\n");
this.WriteObjects("		\r\n");
#line 105 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
ApplyClassTailTemplate();

#line 107 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    }\r\n");

        }



    }
}