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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst")]
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
#line 15 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("<?xml version=\"1.0\"?>\r\n");
this.WriteObjects("<hibernate-mapping xmlns=\"urn:nhibernate-mapping-2.2\" \r\n");
this.WriteObjects("                   schema=\"`dbo`\"\r\n");
this.WriteObjects("                   default-cascade=\"save-update\"\r\n");
this.WriteObjects("                   assembly=\"Kistl.Objects.NHibernateImpl\">\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- RelationCollectionEntries -->\r\n");
#line 23 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
foreach (var rel in ctx.GetQuery<Relation>()
        .Where(r => r.Storage == StorageType.Separate)
        .ToList()
        .OrderBy(r => r.GetRelationClassName()))
    {
        var collectionEntryNamespace = rel.A.Type.Module.Namespace;
        var collectionEntryClassName = rel.GetRelationClassName() + ImplementationSuffix;
        var proxyClassName = rel.GetRelationClassName() + "Proxy";
        var tableName = rel.GetRelationTableName();

#line 33 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
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
#line 49 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (rel.NeedsPositionStorage(RelationEndRole.A)) { 
#line 50 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"A",  Kistl.API.Helper.PositionSuffix , "\"\r\n");
this.WriteObjects("                    column=\"`",  Construct.ListPositionColumnName(rel.B) , "`\" />\r\n");
#line 52 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 53 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (rel.NeedsPositionStorage(RelationEndRole.B)) { 
#line 54 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"B",  Kistl.API.Helper.PositionSuffix , "\"\r\n");
this.WriteObjects("                  column=\"`",  Construct.ListPositionColumnName(rel.A) , "`\" />\r\n");
#line 56 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 57 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (rel.A.Type.ImplementsIExportable() && rel.B.Type.ImplementsIExportable()) { 
#line 58 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"ExportGuid\" column=\"`ExportGuid`\" type=\"Guid\" />\r\n");
#line 59 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 60 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("    </class>\r\n");
#line 61 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 62 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- ValueCollectionEntries -->\r\n");
#line 65 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
foreach (var vtp in ctx.GetQuery<ValueTypeProperty>()
        .Where(p => p.IsList)
        .OrderBy(p => p.Name))
    {
        var collectionEntryNamespace = vtp.Module.Namespace;
        var collectionEntryClassName = vtp.GetCollectionEntryClassName() + ImplementationSuffix;
        var proxyClassName = vtp.GetCollectionEntryClassName() + "Proxy";
        var tableName = vtp.GetCollectionEntryTable();

#line 74 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
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
this.WriteObjects("        <many-to-one name=\"Parent\"\r\n");
this.WriteObjects("                     column=\"`",  vtp.GetCollectionEntryReverseKeyColumnName() , "`\" />\r\n");
this.WriteObjects("        <property name=\"Value\"\r\n");
this.WriteObjects("                  column=\"`",  vtp.Name , "`\" />\r\n");
#line 90 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (vtp.HasPersistentOrder) { 
#line 91 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"",  vtp.Name , "Index\"\r\n");
this.WriteObjects("                  column=\"`",  vtp.Name , "Index`\" />\r\n");
#line 94 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
throw new NotImplementedException();
} 
#line 96 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (((ObjectClass)vtp.ObjectClass).ImplementsIExportable()) { 
#line 97 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"ExportGuid\" column=\"`ExportGuid`\" type=\"Guid\" />\r\n");
#line 98 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 99 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("    </class>\r\n");
#line 100 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 101 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- CompoundObjectCollectionEntries -->\r\n");
#line 104 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
foreach (var cop in ctx.GetQuery<CompoundObjectProperty>()
        .Where(p => p.IsList)
        .OrderBy(p => p.Name))
    {
        var collectionEntryNamespace = cop.Module.Namespace;
        var collectionEntryClassName = cop.GetCollectionEntryClassName() + ImplementationSuffix;
        var proxyClassName = cop.GetCollectionEntryClassName() + "Proxy";
        var tableName = cop.GetCollectionEntryTable();

#line 113 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
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
this.WriteObjects("        <many-to-one name=\"Parent\"\r\n");
this.WriteObjects("                     column=\"`",  cop.GetCollectionEntryReverseKeyColumnName() , "`\" />\r\n");
this.WriteObjects("        <component name=\"Value\"\r\n");
this.WriteObjects("                   class=\"",  cop.CompoundObjectDefinition.Module.Namespace , ".",  cop.CompoundObjectDefinition.Name , "",  ImplementationSuffix , ",Kistl.Objects.NHibernateImpl\">\r\n");
#line 129 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
PropertiesHbm.Call(Host, ctx, cop.Name + "_", cop.CompoundObjectDefinition.Properties); 
#line 130 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        </component>\r\n");
this.WriteObjects("\r\n");
#line 132 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (cop.HasPersistentOrder) { 
#line 133 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"",  cop.Name , "Index\"\r\n");
this.WriteObjects("                    column=\"`",  cop.Name , "Index`\" />\r\n");
#line 136 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
throw new NotImplementedException();
} 
#line 138 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (((ObjectClass)cop.ObjectClass).ImplementsIExportable()) { 
#line 139 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"ExportGuid\" column=\"`ExportGuid`\" type=\"Guid\" />\r\n");
#line 140 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 141 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("    </class>\r\n");
#line 142 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 143 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("</hibernate-mapping>");

        }

    }
}