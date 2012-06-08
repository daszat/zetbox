using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.NHibernate.Generator.Templates.CollectionEntries
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\CollectionEntries\GetDeletedRelatives.cst")]
    public partial class GetDeletedRelatives : Kistl.Generator.ResourceTemplate
    {
		protected string aName;
		protected string bName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, string aName, string bName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("CollectionEntries.GetDeletedRelatives", aName, bName);
        }

        public GetDeletedRelatives(Arebis.CodeGeneration.IGenerationHost _host, string aName, string bName)
            : base(_host)
        {
			this.aName = aName;
			this.bName = bName;

        }

        public override void Generate()
        {
#line 16 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\CollectionEntries\GetDeletedRelatives.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public override List<NHibernatePersistenceObject> GetParentsToDelete()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            var result = base.GetParentsToDelete();\r\n");
this.WriteObjects("\r\n");
#line 21 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\CollectionEntries\GetDeletedRelatives.cst"
if (!string.IsNullOrEmpty(aName)) { 
#line 22 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\CollectionEntries\GetDeletedRelatives.cst"
this.WriteObjects("            // Follow ",  aName , "\r\n");
this.WriteObjects("            if (this.",  aName , " != null && this.",  aName , ".ObjectState == DataObjectState.Deleted)\r\n");
this.WriteObjects("                result.Add((NHibernatePersistenceObject)this.",  aName , ");\r\n");
#line 25 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\CollectionEntries\GetDeletedRelatives.cst"
} 
#line 26 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\CollectionEntries\GetDeletedRelatives.cst"
this.WriteObjects("\r\n");
#line 27 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\CollectionEntries\GetDeletedRelatives.cst"
if (!string.IsNullOrEmpty(bName)) { 
#line 28 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\CollectionEntries\GetDeletedRelatives.cst"
this.WriteObjects("            // Follow ",  bName , "\r\n");
this.WriteObjects("            if (this.",  bName , " != null && this.",  bName , ".ObjectState == DataObjectState.Deleted)\r\n");
this.WriteObjects("                result.Add((NHibernatePersistenceObject)this.",  bName , ");\r\n");
this.WriteObjects("\r\n");
#line 32 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\CollectionEntries\GetDeletedRelatives.cst"
} 
#line 33 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\CollectionEntries\GetDeletedRelatives.cst"
this.WriteObjects("            return result;\r\n");
this.WriteObjects("        }\r\n");

        }

    }
}