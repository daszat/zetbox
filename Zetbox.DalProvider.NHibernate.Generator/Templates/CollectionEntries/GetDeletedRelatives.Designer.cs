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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\GetDeletedRelatives.cst")]
    public partial class GetDeletedRelatives : Zetbox.Generator.ResourceTemplate
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
#line 17 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\GetDeletedRelatives.cst"
this.WriteObjects("");
#line 32 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\GetDeletedRelatives.cst"
this.WriteObjects("\n");
this.WriteObjects("        public override List<NHibernatePersistenceObject> GetParentsToDelete()\n");
this.WriteObjects("        {\n");
this.WriteObjects("            var result = base.GetParentsToDelete();\n");
this.WriteObjects("\n");
#line 37 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\GetDeletedRelatives.cst"
if (!string.IsNullOrEmpty(aName)) { 
#line 38 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\GetDeletedRelatives.cst"
this.WriteObjects("            // Follow ",  aName , "\n");
this.WriteObjects("            if (this.",  aName , " != null && this.",  aName , ".ObjectState == DataObjectState.Deleted)\n");
this.WriteObjects("                result.Add((NHibernatePersistenceObject)this.",  aName , ");\n");
#line 41 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\GetDeletedRelatives.cst"
} 
#line 42 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\GetDeletedRelatives.cst"
this.WriteObjects("\n");
#line 43 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\GetDeletedRelatives.cst"
if (!string.IsNullOrEmpty(bName)) { 
#line 44 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\GetDeletedRelatives.cst"
this.WriteObjects("            // Follow ",  bName , "\n");
this.WriteObjects("            if (this.",  bName , " != null && this.",  bName , ".ObjectState == DataObjectState.Deleted)\n");
this.WriteObjects("                result.Add((NHibernatePersistenceObject)this.",  bName , ");\n");
this.WriteObjects("\n");
#line 48 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\GetDeletedRelatives.cst"
} 
#line 49 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CollectionEntries\GetDeletedRelatives.cst"
this.WriteObjects("            return result;\n");
this.WriteObjects("        }\n");

        }

    }
}