using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.API;
using Zetbox.API.SchemaManagement;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Mappings
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst")]
    public partial class CollectionEntriesHbm : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Mappings.CollectionEntriesHbm", ctx);
        }

        public CollectionEntriesHbm(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }

        public override void Generate()
        {
#line 32 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("<?xml version=\"1.0\"?>\r\n");
this.WriteObjects("<hibernate-mapping xmlns=\"urn:nhibernate-mapping-2.2\" \r\n");
this.WriteObjects("                   default-cascade=\"save-update\"\r\n");
this.WriteObjects("                   assembly=\"Zetbox.Objects.NHibernateImpl\">\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- RelationCollectionEntries -->\r\n");
#line 39 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
foreach (var rel in GetRelations(ctx)
        .Where(r => r.Storage == StorageType.Separate)
        .ToList()
        .OrderBy(r => r.GetRelationClassName()))
    {
        var collectionEntryNamespace = rel.Module.Namespace;
        var collectionEntryClassName = rel.GetRelationClassName() + ImplementationSuffix;
        var proxyClassName = rel.GetRelationClassName() + "Proxy";
        var schemaName = rel.Module.SchemaName;
        var tableName = rel.GetRelationTableName();

#line 50 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("    <class name=\"",  collectionEntryNamespace , ".",  collectionEntryClassName , "+",  proxyClassName , "\"\r\n");
this.WriteObjects("           proxy=\"",  collectionEntryNamespace , ".",  collectionEntryClassName , "+",  proxyClassName , "\"\r\n");
this.WriteObjects("           table=\"`",  tableName , "`\"\r\n");
this.WriteObjects("           schema=\"`",  schemaName , "`\" >\r\n");
this.WriteObjects("\r\n");
#line 55 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
IdGeneratorHbm.Call(Host, "id", schemaName, tableName); 
#line 56 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        <many-to-one name=\"A\"\r\n");
this.WriteObjects("                     column=\"`",  Construct.ForeignKeyColumnName(rel.A) , "`\" />\r\n");
this.WriteObjects("        <many-to-one name=\"B\"\r\n");
this.WriteObjects("                     column=\"`",  Construct.ForeignKeyColumnName(rel.B) , "`\" />\r\n");
#line 61 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (rel.NeedsPositionStorage(RelationEndRole.A)) { 
#line 62 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"A",  Zetbox.API.Helper.PositionSuffix , "\"\r\n");
this.WriteObjects("                    column=\"`",  Construct.ListPositionColumnName(rel.B) , "`\" />\r\n");
#line 64 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 65 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (rel.NeedsPositionStorage(RelationEndRole.B)) { 
#line 66 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"B",  Zetbox.API.Helper.PositionSuffix , "\"\r\n");
this.WriteObjects("                  column=\"`",  Construct.ListPositionColumnName(rel.A) , "`\" />\r\n");
#line 68 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 69 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (rel.A.Type.ImplementsIExportable() && rel.B.Type.ImplementsIExportable()) { 
#line 70 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"ExportGuid\" column=\"`ExportGuid`\" type=\"Guid\" />\r\n");
#line 71 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 72 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("    </class>\r\n");
#line 73 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 74 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- ValueCollectionEntries are defined directly on use -->\r\n");
#line 77 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
foreach (var prop in GetValueTypeProperties(ctx)
                            .Where(p => p.IsList && !p.IsCalculated)
                            .Where(p => p.ObjectClass is ObjectClass)
                            .ToList()
                            .OrderBy(p => p.ObjectClass.Name)
                            .ThenBy(p => p.Name))
   {
        var collectionEntryNamespace = prop.GetCollectionEntryNamespace();
        var collectionEntryClassName = prop.GetCollectionEntryClassName() + ImplementationSuffix;
        var proxyClassName = prop.GetCollectionEntryClassName() + "Proxy";
        var schemaName = prop.Module.SchemaName;
        var tableName = prop.GetCollectionEntryTable();
        var ceReverseKeyColumnName = Construct.ForeignKeyColumnName(prop);

#line 91 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("    <class name=\"",  collectionEntryNamespace , ".",  collectionEntryClassName , "+",  proxyClassName , "\"\r\n");
this.WriteObjects("           proxy=\"",  collectionEntryNamespace , ".",  collectionEntryClassName , "+",  proxyClassName , "\"\r\n");
this.WriteObjects("           table=\"`",  tableName , "`\"\r\n");
this.WriteObjects("           schema=\"`",  schemaName , "`\" >\r\n");
this.WriteObjects("\r\n");
#line 96 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
IdGeneratorHbm.Call(Host, "id", schemaName, tableName); 
#line 97 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        <many-to-one name=\"Parent\"\r\n");
this.WriteObjects("                     column=\"`",  ceReverseKeyColumnName , "`\" />\r\n");
#line 100 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
ValueTypePropertyHbm.Call(Host, String.Empty, prop, "Value", prop.Name, true, ImplementationSuffix, false); 
#line 101 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (prop.HasPersistentOrder) { 
#line 102 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"Value_pos\"\r\n");
this.WriteObjects("                  column=\"`Index`\" />\r\n");
#line 104 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 105 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (((ObjectClass)prop.ObjectClass).ImplementsIExportable()) { 
#line 106 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <!-- export guid is not needed since serialization is always \"in-place\"\r\n");
this.WriteObjects("        <property name=\"ExportGuid\" column=\"`ExportGuid`\" type=\"Guid\" />\r\n");
this.WriteObjects("        -->\r\n");
#line 109 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 110 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("    </class>\r\n");
this.WriteObjects("\r\n");
#line 112 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 113 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("    <!-- CompoundObjectCollectionEntries -->\r\n");
#line 115 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
foreach (var prop in GetCompoundObjectProperties(ctx)
                            .Where(p => p.IsList /* && !p.IsCalculated */)
                            .Where(p => p.ObjectClass is ObjectClass)
                            .ToList()
                            .OrderBy(p => p.ObjectClass.Name)
                            .ThenBy(p => p.Name))
   {
        var collectionEntryNamespace = prop.GetCollectionEntryNamespace();
        var collectionEntryClassName = prop.GetCollectionEntryClassName() + ImplementationSuffix;
        var proxyClassName = prop.GetCollectionEntryClassName() + "Proxy";
        var schemaName = prop.Module.SchemaName;
        var tableName = prop.GetCollectionEntryTable();
        var ceReverseKeyColumnName = Construct.ForeignKeyColumnName(prop);

#line 129 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("    <class name=\"",  collectionEntryNamespace , ".",  collectionEntryClassName , "+",  proxyClassName , "\"\r\n");
this.WriteObjects("           proxy=\"",  collectionEntryNamespace , ".",  collectionEntryClassName , "+",  proxyClassName , "\"\r\n");
this.WriteObjects("           table=\"`",  tableName , "`\"\r\n");
this.WriteObjects("           schema=\"`",  schemaName , "`\" >\r\n");
this.WriteObjects("\r\n");
#line 134 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
IdGeneratorHbm.Call(Host, "id", schemaName, tableName); 
#line 135 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        <many-to-one name=\"Parent\"\r\n");
this.WriteObjects("                     column=\"`",  ceReverseKeyColumnName , "`\" />\r\n");
#line 138 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
CompoundObjectPropertyHbm.Call(Host, ctx, String.Empty, prop, "Value", prop.Name, true, ImplementationSuffix); 
#line 139 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (prop.HasPersistentOrder) { 
#line 140 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"Value_pos\"\r\n");
this.WriteObjects("                  column=\"`Index`\" />\r\n");
#line 142 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 143 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (((ObjectClass)prop.ObjectClass).ImplementsIExportable()) { 
#line 144 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <!-- export guid is not needed since serialization is always \"in-place\"\r\n");
this.WriteObjects("        <property name=\"ExportGuid\" column=\"`ExportGuid`\" type=\"Guid\" />\r\n");
this.WriteObjects("        -->\r\n");
#line 147 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 148 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("    </class>\r\n");
this.WriteObjects("\r\n");
#line 150 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 151 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("</hibernate-mapping>\r\n");

        }

    }
}