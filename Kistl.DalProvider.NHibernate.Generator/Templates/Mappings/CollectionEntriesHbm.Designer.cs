using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.NHibernate.Generator.Templates.Mappings
{
    [Arebis.CodeGeneration.TemplateInfo(@"/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Mappings/CollectionEntriesHbm.cst")]
    public partial class CollectionEntriesHbm : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Mappings.CollectionEntriesHbm", ctx);
        }

        public CollectionEntriesHbm(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }

        public override void Generate()
        {
#line 15 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Mappings/CollectionEntriesHbm.cst"
this.WriteObjects("<?xml version=\"1.0\"?>\r\n");
this.WriteObjects("<hibernate-mapping xmlns=\"urn:nhibernate-mapping-2.2\" \r\n");
this.WriteObjects("                   schema=\"`dbo`\"\r\n");
this.WriteObjects("                   default-cascade=\"save-update\"\r\n");
this.WriteObjects("                   assembly=\"Kistl.Objects.NHibernateImpl\">\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- RelationCollectionEntries -->\r\n");
#line 23 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Mappings/CollectionEntriesHbm.cst"
foreach (var rel in ctx.GetQuery<Relation>()
        .Where(r => r.Storage == StorageType.Separate)
        .ToList()
        .OrderBy(r => r.GetRelationClassName()))
    {
        var collectionEntryNamespace = rel.A.Type.Module.Namespace;
        var collectionEntryClassName = rel.GetRelationClassName() + ImplementationSuffix;
        var proxyClassName = rel.GetRelationClassName() + "Proxy";
        var tableName = rel.GetRelationTableName();

#line 33 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Mappings/CollectionEntriesHbm.cst"
this.WriteObjects("    <class name=\"",  collectionEntryNamespace , ".",  collectionEntryClassName , "+",  proxyClassName , "\"\r\n");
this.WriteObjects("           proxy=\"",  collectionEntryNamespace , ".",  collectionEntryClassName , "+",  proxyClassName , "\"\r\n");
this.WriteObjects("           table=\"`",  tableName , "`\">\r\n");
this.WriteObjects("        <id name=\"ID\"\r\n");
this.WriteObjects("            column=\"`ID`\"\r\n");
this.WriteObjects("            type=\"Int32\">\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            <generator class=\"native\">\r\n");
this.WriteObjects("                <param name=\"sequence\">`",  tableName , "_ID_seq`</param>\r\n");
this.WriteObjects("            </generator>\r\n");
this.WriteObjects("        </id>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        <many-to-one name=\"A\"\r\n");
this.WriteObjects("                     column=\"`",  rel.GetRelationFkColumnName(RelationEndRole.A) , "`\" />\r\n");
this.WriteObjects("        <many-to-one name=\"B\"\r\n");
this.WriteObjects("                     column=\"`",  rel.GetRelationFkColumnName(RelationEndRole.B) , "`\" />\r\n");
#line 49 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Mappings/CollectionEntriesHbm.cst"
if (rel.NeedsPositionStorage(RelationEndRole.A)) { 
#line 50 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Mappings/CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"A",  Kistl.API.Helper.PositionSuffix , "\"\r\n");
this.WriteObjects("                    column=\"`",  Construct.ListPositionColumnName(rel.B) , "`\" />\r\n");
#line 52 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Mappings/CollectionEntriesHbm.cst"
} 
#line 53 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Mappings/CollectionEntriesHbm.cst"
if (rel.NeedsPositionStorage(RelationEndRole.B)) { 
#line 54 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Mappings/CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"B",  Kistl.API.Helper.PositionSuffix , "\"\r\n");
this.WriteObjects("                  column=\"`",  Construct.ListPositionColumnName(rel.A) , "`\" />\r\n");
#line 56 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Mappings/CollectionEntriesHbm.cst"
} 
#line 57 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Mappings/CollectionEntriesHbm.cst"
if (rel.A.Type.ImplementsIExportable() && rel.B.Type.ImplementsIExportable()) { 
#line 58 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Mappings/CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"ExportGuid\" column=\"`ExportGuid`\" type=\"Guid\" />\r\n");
#line 59 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Mappings/CollectionEntriesHbm.cst"
} 
#line 60 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Mappings/CollectionEntriesHbm.cst"
this.WriteObjects("    </class>\r\n");
#line 61 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Mappings/CollectionEntriesHbm.cst"
} 
#line 62 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Mappings/CollectionEntriesHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- ValueCollectionEntries are defined directly on use -->\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- CompoundObjectCollectionEntries are defined directly on use -->\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("</hibernate-mapping>");

        }

    }
}