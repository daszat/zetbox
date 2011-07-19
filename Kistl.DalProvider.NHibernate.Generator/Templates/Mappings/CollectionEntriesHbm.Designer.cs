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
this.WriteObjects("\r\n");
#line 37 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
IdGeneratorHbm.Call(Host, "id", tableName); 
#line 38 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        <many-to-one name=\"A\"\r\n");
this.WriteObjects("                     column=\"`",  rel.GetRelationFkColumnName(RelationEndRole.A) , "`\" />\r\n");
this.WriteObjects("        <many-to-one name=\"B\"\r\n");
this.WriteObjects("                     column=\"`",  rel.GetRelationFkColumnName(RelationEndRole.B) , "`\" />\r\n");
#line 43 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (rel.NeedsPositionStorage(RelationEndRole.A)) { 
#line 44 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"A",  Kistl.API.Helper.PositionSuffix , "\"\r\n");
this.WriteObjects("                    column=\"`",  Construct.ListPositionColumnName(rel.B) , "`\" />\r\n");
#line 46 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 47 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (rel.NeedsPositionStorage(RelationEndRole.B)) { 
#line 48 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"B",  Kistl.API.Helper.PositionSuffix , "\"\r\n");
this.WriteObjects("                  column=\"`",  Construct.ListPositionColumnName(rel.A) , "`\" />\r\n");
#line 50 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 51 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (rel.A.Type.ImplementsIExportable() && rel.B.Type.ImplementsIExportable()) { 
#line 52 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"ExportGuid\" column=\"`ExportGuid`\" type=\"Guid\" />\r\n");
#line 53 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 54 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("    </class>\r\n");
#line 55 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 56 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- ValueCollectionEntries are defined directly on use -->\r\n");
#line 59 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
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
        var tableName = prop.GetCollectionEntryTable();
        var ceReverseKeyColumnName = prop.GetCollectionEntryReverseKeyColumnName();

#line 72 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("    <class name=\"",  collectionEntryNamespace , ".",  collectionEntryClassName , "+",  proxyClassName , "\"\r\n");
this.WriteObjects("           proxy=\"",  collectionEntryNamespace , ".",  collectionEntryClassName , "+",  proxyClassName , "\"\r\n");
this.WriteObjects("           table=\"`",  tableName , "`\">\r\n");
this.WriteObjects("\r\n");
#line 76 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
IdGeneratorHbm.Call(Host, "id", tableName); 
#line 77 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        <many-to-one name=\"Parent\"\r\n");
this.WriteObjects("                     column=\"`",  ceReverseKeyColumnName , "`\" />\r\n");
#line 80 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
ValueTypePropertyHbm.Call(Host, String.Empty, prop, "Value", prop.Name, true, ImplementationSuffix, false); 
#line 81 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (prop.HasPersistentOrder) { 
#line 82 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"Index\"\r\n");
this.WriteObjects("                  column=\"`Index`\" />\r\n");
#line 84 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 85 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (((ObjectClass)prop.ObjectClass).ImplementsIExportable()) { 
#line 86 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <!-- export guid is not needed since serialization is always \"in-place\"\r\n");
this.WriteObjects("        <property name=\"ExportGuid\" column=\"`ExportGuid`\" type=\"Guid\" />\r\n");
this.WriteObjects("        -->\r\n");
#line 89 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 90 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("    </class>\r\n");
this.WriteObjects("\r\n");
#line 92 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 93 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("    <!-- CompoundObjectCollectionEntries -->\r\n");
#line 95 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
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
        var tableName = prop.GetCollectionEntryTable();
        var ceReverseKeyColumnName = prop.GetCollectionEntryReverseKeyColumnName();

#line 108 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("    <class name=\"",  collectionEntryNamespace , ".",  collectionEntryClassName , "+",  proxyClassName , "\"\r\n");
this.WriteObjects("           proxy=\"",  collectionEntryNamespace , ".",  collectionEntryClassName , "+",  proxyClassName , "\"\r\n");
this.WriteObjects("           table=\"`",  tableName , "`\">\r\n");
this.WriteObjects("\r\n");
#line 112 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
IdGeneratorHbm.Call(Host, "id", tableName); 
#line 113 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        <many-to-one name=\"Parent\"\r\n");
this.WriteObjects("                     column=\"`",  ceReverseKeyColumnName , "`\" />\r\n");
#line 116 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
CompoundObjectPropertyHbm.Call(Host, ctx, String.Empty, prop, "Value", prop.Name, true, ImplementationSuffix); 
#line 117 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (prop.HasPersistentOrder) { 
#line 118 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <property name=\"Index\"\r\n");
this.WriteObjects("                  column=\"`Index`\" />\r\n");
#line 120 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 121 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
if (((ObjectClass)prop.ObjectClass).ImplementsIExportable()) { 
#line 122 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("        <!-- export guid is not needed since serialization is always \"in-place\"\r\n");
this.WriteObjects("        <property name=\"ExportGuid\" column=\"`ExportGuid`\" type=\"Guid\" />\r\n");
this.WriteObjects("        -->\r\n");
#line 125 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 126 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("    </class>\r\n");
this.WriteObjects("\r\n");
#line 128 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
} 
#line 129 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\CollectionEntriesHbm.cst"
this.WriteObjects("</hibernate-mapping>\r\n");

        }

    }
}