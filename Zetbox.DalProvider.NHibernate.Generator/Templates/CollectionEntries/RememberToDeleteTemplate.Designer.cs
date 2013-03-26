using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.CollectionEntries
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst")]
    public partial class RememberToDeleteTemplate : Zetbox.Generator.ResourceTemplate
    {
		protected string aName;
		protected string bName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, string aName, string bName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("CollectionEntries.RememberToDeleteTemplate", aName, bName);
        }

        public RememberToDeleteTemplate(Arebis.CodeGeneration.IGenerationHost _host, string aName, string bName)
            : base(_host)
        {
			this.aName = aName;
			this.bName = bName;

        }

        public override void Generate()
        {
#line 32 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public override void NotifyDeleting()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.NotifyDeleting();\r\n");
#line 36 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
if (!string.IsNullOrEmpty(aName)) { 
#line 37 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("            // Follow ",  aName , "\r\n");
this.WriteObjects("            if (this.",  aName , " != null && this.",  aName , ".ObjectState == DataObjectState.Deleted)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                ParentsToDelete.Add((NHibernatePersistenceObject)this.",  aName , ");\r\n");
this.WriteObjects("                ((NHibernatePersistenceObject)this.",  aName , ").ChildrenToDelete.Add(this);\r\n");
this.WriteObjects("            }\r\n");
#line 44 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
} 
#line 45 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
if (!string.IsNullOrEmpty(bName)) { 
#line 46 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("            // Follow ",  bName , "\r\n");
this.WriteObjects("            if (this.",  bName , " != null && this.",  bName , ".ObjectState == DataObjectState.Deleted)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                ParentsToDelete.Add((NHibernatePersistenceObject)this.",  bName , ");\r\n");
this.WriteObjects("                ((NHibernatePersistenceObject)this.",  bName , ").ChildrenToDelete.Add(this);\r\n");
this.WriteObjects("            }\r\n");
#line 53 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
} 
#line 54 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
if (!string.IsNullOrEmpty(aName)) { 
#line 55 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("            // reset pointers on being deleted\r\n");
this.WriteObjects("            // this must happen after registering them above in ParentsToDelete/ChildrenToDelete to avoid interference from a second notification round\r\n");
this.WriteObjects("            this.",  aName , " = null;\r\n");
#line 59 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
} 
#line 60 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
if (!string.IsNullOrEmpty(bName)) { 
#line 61 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
this.WriteObjects("            this.",  bName , " = null;\r\n");
#line 62 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
} 
#line 63 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
this.WriteObjects("        }\r\n");

        }

    }
}