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
this.WriteObjects("                   default-cascade=\"save-update\"\r\n");
this.WriteObjects("                   assembly=\"Kistl.Objects.NHibernateImpl\">\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- RelationCollectionEntries -->\r\n");
#line 22 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
foreach (var rel in ctx.GetQuery<Relation>()
        .Where(r => r.Storage == StorageType.Separate)
        .ToList()
        .OrderBy(r => r.GetRelationClassName()))
    {
        var collectionEntryNamespace = rel.A.Type.Module.Namespace;
        var collectionEntryClassName = rel.GetRelationClassName() + ImplementationSuffix;
        var proxyClassName = rel.GetRelationClassName() + "Proxy";
        var schemaName = rel.Module.SchemaName;
        var tableName = rel.GetRelationTableName();

#line 33 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("    <class name=\"",  collectionEntryNamespace , ".",  collectionEntryClassName , "+",  proxyClassName , "\"\r\n");
this.WriteObjects("           proxy=\"",  collectionEntryNamespace , ".",  collectionEntryClassName , "+",  proxyClassName , "\"\r\n");
this.WriteObjects("           table=\"`",  tableName , "`\"\r\n");
this.WriteObjects("           schema=\"`",  schemaName , "`\" >\r\n");
this.WriteObjects("\r\n");
#line 38 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
IdGeneratorHbm.Call(Host, "id", schemaName, tableName); 
#line 39 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        <many-to-one name=\"A\"\r\n");
this.WriteObjects("                     column=\"`",  rel.GetRelationFkColumnName(RelationEndRole.A) , "`\" />\r\n");
this.WriteObjects("        <many-to-one name=\"B\"\r\n");
this.WriteObjects("                     column=\"`",  rel.GetRelationFkColumnName(RelationEndRole.B) , "`\" />\r\n");
#line 44 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (rel.NeedsPositionStorage(RelationEndRole.A)) { 
#line 45 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"A",  Kistl.API.Helper.PositionSuffix , "\"\r\n");
this.WriteObjects("                    column=\"`",  Construct.ListPositionColumnName(rel.B) , "`\" />\r\n");
#line 47 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 48 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (rel.NeedsPositionStorage(RelationEndRole.B)) { 
#line 49 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"B",  Kistl.API.Helper.PositionSuffix , "\"\r\n");
this.WriteObjects("                  column=\"`",  Construct.ListPositionColumnName(rel.A) , "`\" />\r\n");
#line 51 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 52 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (rel.A.Type.ImplementsIExportable() && rel.B.Type.ImplementsIExportable()) { 
#line 53 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"ExportGuid\" column=\"`ExportGuid`\" type=\"Guid\" />\r\n");
#line 54 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 55 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("    </class>\r\n");
#line 56 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 57 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- ValueCollectionEntries are defined directly on use -->\r\n");
#line 60 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
foreach (var prop in ctx.GetQuery<ValueTypeProperty>()
                            .Where(p => p.IsList && !p.IsCalculated)
                            .Where(p => p.ObjectClass is ObjectClass)
                            .ToList()
                            .OrderBy(p => p.ObjectClass.Name)
                            .ThenBy(p => p.Name))
   {
        var collectionEntryNamespace = prop.Module.Namespace;
        var collectionEntryClassName = prop.GetCollectionEntryClassName() + ImplementationSuffix;
        var proxyClassName = prop.GetCollectionEntryClassName() + "Proxy";
        var schemaName = prop.Module.SchemaName;
        var tableName = prop.GetCollectionEntryTable();
        var ceReverseKeyColumnName = prop.GetCollectionEntryReverseKeyColumnName();

#line 74 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("    <class name=\"",  collectionEntryNamespace , ".",  collectionEntryClassName , "+",  proxyClassName , "\"\r\n");
this.WriteObjects("           proxy=\"",  collectionEntryNamespace , ".",  collectionEntryClassName , "+",  proxyClassName , "\"\r\n");
this.WriteObjects("           table=\"`",  tableName , "`\"\r\n");
this.WriteObjects("           schema=\"`",  schemaName , "`\" >\r\n");
this.WriteObjects("\r\n");
#line 79 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
IdGeneratorHbm.Call(Host, "id", schemaName, tableName); 
#line 80 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        <many-to-one name=\"Parent\"\r\n");
this.WriteObjects("                     column=\"`",  ceReverseKeyColumnName , "`\" />\r\n");
#line 83 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
ValueTypePropertyHbm.Call(Host, String.Empty, prop, "Value", prop.Name, true, ImplementationSuffix, false); 
#line 84 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (prop.HasPersistentOrder) { 
#line 85 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"Index\"\r\n");
this.WriteObjects("                  column=\"`Index`\" />\r\n");
#line 87 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 88 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (((ObjectClass)prop.ObjectClass).ImplementsIExportable()) { 
#line 89 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <!-- export guid is not needed since serialization is always \"in-place\"\r\n");
this.WriteObjects("        <property name=\"ExportGuid\" column=\"`ExportGuid`\" type=\"Guid\" />\r\n");
this.WriteObjects("        -->\r\n");
#line 92 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 93 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("    </class>\r\n");
this.WriteObjects("\r\n");
#line 95 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 96 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("    <!-- CompoundObjectCollectionEntries -->\r\n");
#line 98 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
foreach (var prop in ctx.GetQuery<CompoundObjectProperty>()
                            .Where(p => p.IsList /* && !p.IsCalculated */)
                            .Where(p => p.ObjectClass is ObjectClass)
                            .ToList()
                            .OrderBy(p => p.ObjectClass.Name)
                            .ThenBy(p => p.Name))
   {
        var collectionEntryNamespace = prop.Module.Namespace;
        var collectionEntryClassName = prop.GetCollectionEntryClassName() + ImplementationSuffix;
        var proxyClassName = prop.GetCollectionEntryClassName() + "Proxy";
        var schemaName = prop.Module.SchemaName;
        var tableName = prop.GetCollectionEntryTable();
        var ceReverseKeyColumnName = prop.GetCollectionEntryReverseKeyColumnName();

#line 112 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("    <class name=\"",  collectionEntryNamespace , ".",  collectionEntryClassName , "+",  proxyClassName , "\"\r\n");
this.WriteObjects("           proxy=\"",  collectionEntryNamespace , ".",  collectionEntryClassName , "+",  proxyClassName , "\"\r\n");
this.WriteObjects("           table=\"`",  tableName , "`\"\r\n");
this.WriteObjects("           schema=\"`",  schemaName , "`\" >\r\n");
this.WriteObjects("\r\n");
#line 117 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
IdGeneratorHbm.Call(Host, "id", schemaName, tableName); 
#line 118 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        <many-to-one name=\"Parent\"\r\n");
this.WriteObjects("                     column=\"`",  ceReverseKeyColumnName , "`\" />\r\n");
#line 121 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
CompoundObjectPropertyHbm.Call(Host, ctx, String.Empty, prop, "Value", prop.Name, true, ImplementationSuffix); 
#line 122 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (prop.HasPersistentOrder) { 
#line 123 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"Index\"\r\n");
this.WriteObjects("                  column=\"`Index`\" />\r\n");
#line 125 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 126 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (((ObjectClass)prop.ObjectClass).ImplementsIExportable()) { 
#line 127 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <!-- export guid is not needed since serialization is always \"in-place\"\r\n");
this.WriteObjects("        <property name=\"ExportGuid\" column=\"`ExportGuid`\" type=\"Guid\" />\r\n");
this.WriteObjects("        -->\r\n");
#line 130 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 131 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("    </class>\r\n");
this.WriteObjects("\r\n");
#line 133 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 134 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("</hibernate-mapping>\r\n");

        }

    }
}