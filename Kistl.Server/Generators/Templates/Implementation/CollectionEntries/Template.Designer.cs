using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Implementation.CollectionEntries
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl_clean\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst")]
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
#line 14 "P:\Kistl_clean\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
ApplyClassAttributeTemplate();

#line 16 "P:\Kistl_clean\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("    [System.Diagnostics.DebuggerDisplay(\"",  GetCeClassName() , "\")]\r\n");
this.WriteObjects("    public class ",  GetCeClassName() , " ",  GetInheritance() , "\r\n");
this.WriteObjects("    {\r\n");
this.WriteObjects("    \r\n");
#line 21 "P:\Kistl_clean\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
ApplyIdPropertyTemplate();

#line 23 "P:\Kistl_clean\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// Reference to the A-Side member of this CollectionEntry\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 28 "P:\Kistl_clean\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
ApplyAPropertyTemplate();

#line 30 "P:\Kistl_clean\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// the B-side value of this CollectionEntry\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 34 "P:\Kistl_clean\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
ApplyBPropertyTemplate();



    if (IsOrdered())
    {

#line 41 "P:\Kistl_clean\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// Index into the A-side list of this relation\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 48 "P:\Kistl_clean\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
ApplyAIndexPropertyTemplate();

#line 50 "P:\Kistl_clean\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// Index into the B-side list of this relation\r\n");
this.WriteObjects("        /// </summary>\r\n");
#line 55 "P:\Kistl_clean\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
ApplyBIndexPropertyTemplate();
    }

#line 58 "P:\Kistl_clean\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("#region Serializer\r\n");
this.WriteObjects("\r\n");
#line 62 "P:\Kistl_clean\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
CallTemplate("Implementation.ObjectClasses.SerializerTemplate", ctx,
        Templates.Implementation.SerializerDirection.ToStream, this.MembersToSerialize);
    
    CallTemplate("Implementation.ObjectClasses.SerializerTemplate", ctx,
        Templates.Implementation.SerializerDirection.FromStream, this.MembersToSerialize);

#line 68 "P:\Kistl_clean\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("#endregion\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("	public override InterfaceType GetInterfaceType()\r\n");
this.WriteObjects("	{\r\n");
this.WriteObjects("		return new InterfaceType(typeof(",  GetCeInterface() , "));\r\n");
this.WriteObjects("	}\r\n");
this.WriteObjects("\r\n");
#line 77 "P:\Kistl_clean\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
ApplyClassTailTemplate();

#line 79 "P:\Kistl_clean\Kistl.Server\Generators\Templates\Implementation\CollectionEntries\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    }\r\n");

        }



    }
}