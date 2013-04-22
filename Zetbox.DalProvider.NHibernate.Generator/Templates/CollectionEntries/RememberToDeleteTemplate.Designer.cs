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
		protected bool manageCollection;
		protected bool aRefsObj;
		protected string aName;
		protected string aCollectionName;
		protected bool bRefsObj;
		protected string bName;
		protected string bCollectionName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, bool manageCollection, bool aRefsObj, string aName, string aCollectionName, bool bRefsObj, string bName, string bCollectionName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("CollectionEntries.RememberToDeleteTemplate", manageCollection, aRefsObj, aName, aCollectionName, bRefsObj, bName, bCollectionName);
        }

        public RememberToDeleteTemplate(Arebis.CodeGeneration.IGenerationHost _host, bool manageCollection, bool aRefsObj, string aName, string aCollectionName, bool bRefsObj, string bName, string bCollectionName)
            : base(_host)
        {
			this.manageCollection = manageCollection;
			this.aRefsObj = aRefsObj;
			this.aName = aName;
			this.aCollectionName = aCollectionName;
			this.bRefsObj = bRefsObj;
			this.bName = bName;
			this.bCollectionName = bCollectionName;

        }

        public override void Generate()
        {
#line 37 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public override void NotifyDeleting()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.NotifyDeleting();\r\n");
#line 41 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
if (aRefsObj) { 
#line 42 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("            // Follow ",  aName , "\r\n");
this.WriteObjects("            if (this.",  aName , " != null && this.",  aName , ".ObjectState == DataObjectState.Deleted)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                ParentsToDelete.Add((NHibernatePersistenceObject)this.",  aName , ");\r\n");
this.WriteObjects("                ((NHibernatePersistenceObject)this.",  aName , ").ChildrenToDelete.Add(this);\r\n");
this.WriteObjects("            }\r\n");
#line 49 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
} 
#line 50 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
if (bRefsObj) { 
#line 51 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("            // Follow ",  bName , "\r\n");
this.WriteObjects("            if (this.",  bName , " != null && this.",  bName , ".ObjectState == DataObjectState.Deleted)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                ParentsToDelete.Add((NHibernatePersistenceObject)this.",  bName , ");\r\n");
this.WriteObjects("                ((NHibernatePersistenceObject)this.",  bName , ").ChildrenToDelete.Add(this);\r\n");
this.WriteObjects("            }\r\n");
#line 58 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
} 
#line 59 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
var manageA = manageCollection && !string.IsNullOrEmpty(aName) && !string.IsNullOrEmpty(aCollectionName); 
#line 60 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
var manageB = manageCollection && !string.IsNullOrEmpty(bName) && !string.IsNullOrEmpty(bCollectionName); 
#line 61 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
if (manageA) { 
#line 62 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("            // remove from collections manually to notify NHibernate if necessary\r\n");
this.WriteObjects("            if (this.",  aName , " != null && this.",  aName , ".",  aCollectionName , ".Contains(this.",  bName , ")) this.",  aName , ".",  aCollectionName , ".Remove(this.",  bName , ");\r\n");
#line 65 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
} 
#line 66 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
if (manageA && manageB) { 
#line 67 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
this.WriteObjects("            else\r\n");
#line 68 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
} 
#line 69 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
if (manageB) { 
#line 70 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
this.WriteObjects("            // The other direction is handled by the infrastructure (but ",  aName , " might be null)\r\n");
this.WriteObjects("            if (this.",  bName , " != null && this.",  bName , ".",  bCollectionName , ".Contains(this.",  aName , ")) this.",  bName , ".",  bCollectionName , ".Remove(this.",  aName , ");\r\n");
#line 72 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
} 
#line 73 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
if (aRefsObj) { 
#line 74 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("            // reset pointers on being deleted\r\n");
this.WriteObjects("            // this must happen after registering them above in ParentsToDelete/ChildrenToDelete to avoid interference from a second notification round\r\n");
this.WriteObjects("            this.",  aName , " = null;\r\n");
#line 78 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
} 
#line 79 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
if (bRefsObj) { 
#line 80 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
this.WriteObjects("            this.",  bName , " = null;\r\n");
#line 81 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
} 
#line 82 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\RememberToDeleteTemplate.cst"
this.WriteObjects("        }\r\n");

        }

    }
}