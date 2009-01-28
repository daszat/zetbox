using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;


namespace Kistl.Server.Generators.EntityFramework.Implementation.EfModel
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst")]
    public partial class ModelSsdl : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;


        public ModelSsdl(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }
        
        public override void Generate()
        {
#line 16 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Schema xmlns=\"http://schemas.microsoft.com/ado/2006/04/edm/ssdl\"\r\n");
this.WriteObjects("        Namespace=\"Model.Store\"\r\n");
this.WriteObjects("        Alias=\"Self\"\r\n");
this.WriteObjects("        Provider=\"System.Data.SqlClient\"\r\n");
this.WriteObjects("        ProviderManifestToken=\"2005\" >\r\n");
this.WriteObjects("  <EntityContainer Name=\"dbo\">\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets for all Base Classes -->\r\n");
#line 26 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var cls in ctx.GetBaseClasses())
	{

#line 29 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  cls.ClassName , "\" EntityType=\"Model.Store.",  cls.ClassName , "\" Table=\"",  cls.TableName , "\"/>\r\n");
#line 30 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 32 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets for all derived classes and their inheritance AssociationSets -->\r\n");
#line 35 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var cls in ctx.GetDerivedClasses())
	{
		var info = new InheritanceStorageAssociationInfo(cls);

#line 39 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  cls.ClassName , "\" EntityType=\"Model.Store.",  cls.ClassName , "\" Table=\"",  cls.TableName , "\"/>\r\n");
this.WriteObjects("    <!-- inherits from ",  info.ParentEntitySetName , " -->\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  info.AssociationName , "\" Association=\"Model.Store.",  info.AssociationName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  info.ParentRoleName , "\" EntitySet=\"",  info.ParentEntitySetName , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  info.ChildRoleName , "\" EntitySet=\"",  info.ChildEntitySetName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
this.WriteObjects("    \r\n");
#line 46 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 48 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets and AssociationSet for all object-object CollectionEntrys -->\r\n");
#line 51 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var rel in NewRelation.GetAll(ctx)
		.Where(r => r.GetPreferredStorage() == StorageHint.Separate)
		.OrderBy(r => r.GetAssociationName()))
	{
		string assocNameA = rel.GetCollectionEntryAssociationName(rel.A);
		string assocNameB = rel.GetCollectionEntryAssociationName(rel.B);
		string esName = rel.GetCollectionEntryClassName();
		

#line 60 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <!-- \r\n");
#line 62 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
CallTemplate("Implementation.RelationDebugTemplate", ctx, rel);

#line 64 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    -->\r\n");
this.WriteObjects("    <EntitySet Name=\"",  esName , "\" EntityType=\"Model.Store.",  esName , "\" Table=\"",  esName , "\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocNameA , "\" Association=\"Model.Store.",  assocNameA , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.A.RoleName , "\" EntitySet=\"",  rel.A.Type.ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  esName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocNameB , "\" Association=\"Model.Store.",  assocNameB , "\" >\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  esName , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  rel.B.RoleName , "\" EntitySet=\"",  rel.B.Type.ClassName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
this.WriteObjects("    \r\n");
#line 76 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 78 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- AssociationSets for all object-object relations which do not need CollectionEntrys -->\r\n");
#line 82 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var rel in NewRelation.GetAll(ctx)
		.Where(r => r.GetPreferredStorage() != StorageHint.Separate)
		.OrderBy(r => r.GetAssociationName()))
	{
		string assocName = rel.GetAssociationName();
		

#line 89 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.Store.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.A.RoleName , "\" EntitySet=\"",  rel.A.Type.ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  rel.B.RoleName , "\" EntitySet=\"",  rel.B.Type.ClassName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 94 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 96 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets and AssociationSet for all object-value CollectionEntrys -->\r\n");
#line 99 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var prop in ctx.GetQuery<ValueTypeProperty>()
		.Where(p => p.IsList)
		.OrderBy(p => p.ObjectClass.ClassName)
		.OrderBy(p => p.PropertyName))
	{
		string assocName = prop.GetAssociationName();
		string esName = prop.GetCollectionEntryClassName();

#line 107 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  esName , "\" EntityType=\"Model.Store.",  esName , "\" Table=\"",  ((ObjectClass)prop.ObjectClass).TableName , "_",  prop.PropertyName , "Collection\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.Store.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  prop.ObjectClass.ClassName , "\" EntitySet=\"",  prop.ObjectClass.ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  esName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
this.WriteObjects("    \r\n");
#line 114 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 116 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  </EntityContainer>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes for all classes -->\r\n");
#line 122 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var cls in ctx.GetQuery<ObjectClass>())
	{

#line 124 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("  <EntityType Name=\"",  cls.ClassName , "\">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"int\" Nullable=\"false\" ",  (cls.BaseObjectClass == null) ? "StoreGeneratedPattern=\"Identity\" " : "" , "/>\r\n");
#line 131 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
ApplyEntityTypeColumnDefs(cls.Properties.OfType<Property>().ToList().Where(p => p.IsList == false && p.HasStorage()));

#line 133 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 134 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 136 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes for all object-object CollectionEntrys with their associations -->\r\n");
#line 139 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var rel in NewRelation.GetAll(ctx)
		.Where(r => r.GetPreferredStorage() == StorageHint.Separate)
		.OrderBy(r => r.GetAssociationName()))
	{
		string ceName = rel.GetCollectionEntryClassName();

#line 144 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("  <EntityType Name=\"",  ceName , "\">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"int\" Nullable=\"false\" StoreGeneratedPattern=\"Identity\" />\r\n");
this.WriteObjects("    <Property Name=\"fk_A\" Type=\"int\" Nullable=\"true\" />\r\n");
#line 152 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
if (rel.A.HasPersistentOrder)
		{

#line 155 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"AIndex\" Type=\"int\" Nullable=\"true\" />\r\n");
#line 157 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 159 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"fk_B\" Type=\"int\" Nullable=\"true\" />\r\n");
#line 161 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
if (rel.B.HasPersistentOrder)
		{

#line 164 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"BIndex\" Type=\"int\" Nullable=\"true\" />\r\n");
#line 166 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 168 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("  </EntityType>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- A to CollectionEntry -->\r\n");
this.WriteObjects("  <Association Name=\"",  rel.GetCollectionEntryAssociationName(rel.A) , "\">\r\n");
this.WriteObjects("    <End Role=\"",  rel.A.RoleName , "\" Type=\"Model.Store.",  rel.A.Type.ClassName , "\" Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"CollectionEntry\" Type=\"Model.Store.",  ceName , "\" Multiplicity=\"*\" />\r\n");
this.WriteObjects("    <ReferentialConstraint>\r\n");
this.WriteObjects("      <Principal Role=\"",  rel.A.RoleName , "\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Principal>\r\n");
this.WriteObjects("      <Dependent Role=\"CollectionEntry\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"fk_A\" />\r\n");
this.WriteObjects("      </Dependent>\r\n");
this.WriteObjects("    </ReferentialConstraint>\r\n");
this.WriteObjects("  </Association>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- B to CollectionEntry -->\r\n");
this.WriteObjects("  <Association Name=\"",  rel.GetCollectionEntryAssociationName(rel.B) , "\">\r\n");
this.WriteObjects("    <End Role=\"",  rel.B.RoleName , "\" Type=\"Model.Store.",  rel.B.Type.ClassName , "\" Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"CollectionEntry\" Type=\"Model.Store.",  ceName , "\" Multiplicity=\"*\" />\r\n");
this.WriteObjects("    <ReferentialConstraint>\r\n");
this.WriteObjects("      <Principal Role=\"",  rel.B.RoleName , "\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Principal>\r\n");
this.WriteObjects("      <Dependent Role=\"CollectionEntry\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"fk_B\" />\r\n");
this.WriteObjects("      </Dependent>\r\n");
this.WriteObjects("    </ReferentialConstraint>\r\n");
this.WriteObjects("  </Association>\r\n");
this.WriteObjects("\r\n");
#line 199 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 201 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- Associations for all object-object relations without CollectionEntry (1:1, 1:N) -->\r\n");
#line 205 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var rel in NewRelation.GetAll(ctx)
		.Where(r => r.GetPreferredStorage() != StorageHint.Separate)
		.OrderBy(r => r.GetAssociationName()))
	{
		RelationEnd principal, dependent;
	
		switch(rel.GetPreferredStorage())
		{
			case StorageHint.MergeA:
				principal = rel.B;
				dependent = rel.A;
				break;
			case StorageHint.MergeB:
				principal = rel.A;
				dependent = rel.B;
				break;
			default:
				throw new NotImplementedException();
		}

#line 225 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <Association Name=\"",  rel.GetAssociationName() , "\">\r\n");
this.WriteObjects("    <End Role=\"",  principal.RoleName , "\" Type=\"Model.Store.",  principal.Type.ClassName , "\" Multiplicity=\"",  principal.Multiplicity.ToSsdlMultiplicity().ToXmlValue() , "\" />\r\n");
this.WriteObjects("    <!-- the dependent role always has Multiplicity=\"*\" since 1:1 relations cannot be expressed by EF -->\r\n");
this.WriteObjects("    <End Role=\"",  dependent.RoleName , "\" Type=\"Model.Store.",  dependent.Type.ClassName , "\" Multiplicity=\"*\" />\r\n");
this.WriteObjects("    <ReferentialConstraint>\r\n");
this.WriteObjects("      <Principal Role=\"",  principal.RoleName , "\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Principal>\r\n");
this.WriteObjects("      <Dependent Role=\"",  dependent.RoleName , "\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"fk_",  principal.RoleName , "\" />\r\n");
this.WriteObjects("      </Dependent>\r\n");
this.WriteObjects("    </ReferentialConstraint>\r\n");
this.WriteObjects("  </Association>\r\n");
this.WriteObjects("\r\n");
#line 241 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 243 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- derived->base ObjectClass references -->\r\n");
#line 247 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var cls in ctx.GetDerivedClasses())
	{
		TypeMoniker parentType = cls.BaseObjectClass.GetTypeMoniker();
		TypeMoniker childType = cls.GetTypeMoniker();
		
		string parentRoleName = Construct.AssociationParentRoleName(parentType);
		string childRoleName = Construct.AssociationChildRoleName(childType);

#line 255 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("  <Association Name=\"",  Construct.AssociationName(parentType, childType, "ID") , "\">\r\n");
this.WriteObjects("    <End Role=\"",  parentRoleName , "\" Type=\"Model.Store.",  parentType.ClassName , "\" Multiplicity=\"1\" />\r\n");
this.WriteObjects("    <End Role=\"",  childRoleName , "\" Type=\"Model.Store.",  childType.ClassName , "\" Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <ReferentialConstraint>\r\n");
this.WriteObjects("      <Principal Role=\"",  parentRoleName , "\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Principal>\r\n");
this.WriteObjects("      <Dependent Role=\"",  childRoleName , "\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Dependent>\r\n");
this.WriteObjects("    </ReferentialConstraint>\r\n");
this.WriteObjects("  </Association>\r\n");
#line 267 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 269 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntityTypes and Associations for all object-value CollectionEntrys -->\r\n");
#line 273 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var prop in ctx.GetQuery<ValueTypeProperty>()
		.Where(p => p.IsList)
		.OrderBy(p => p.ObjectClass.ClassName)
		.OrderBy(p => p.PropertyName))
	{
		string assocName = prop.GetAssociationName();
		
		// the name of the class containing this list
		string containerTypeName = prop.ObjectClass.ClassName;
		// the name of the CollectionEntry class
		string entryTypeName = prop.GetCollectionEntryClassName();
		// the name of the contained type
		string itemTypeName = prop.ToDbType();
		
		string constraint = "";
		if (prop is StringProperty) {
			var sProp = (StringProperty)prop;
			constraint += String.Format("MaxLength=\"{0}\" ", sProp.Length);
		}


#line 294 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntityType Name=\"",  entryTypeName , "\" >\r\n");
this.WriteObjects("      <Key>\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Key>\r\n");
this.WriteObjects("      <Property Name=\"ID\" Type=\"int\" Nullable=\"false\" StoreGeneratedPattern=\"Identity\" />\r\n");
this.WriteObjects("      <Property Name=\"fk_",  containerTypeName , "\" Type=\"int\" Nullable=\"true\" />\r\n");
this.WriteObjects("      <Property Name=\"",  prop.PropertyName , "\" Type=\"",  itemTypeName , "\" ",  constraint , "/>\r\n");
#line 302 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
if (prop.IsIndexed)
		{

#line 305 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  prop.PropertyName , "Index\" Type=\"int\" Nullable=\"true\" />\r\n");
#line 307 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 309 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    </EntityType>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <Association Name=\"",  prop.GetAssociationName() , "\">\r\n");
this.WriteObjects("      <End Role=\"",  containerTypeName , "\" Type=\"Model.Store.",  containerTypeName , "\" Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" Type=\"Model.Store.",  entryTypeName , "\" Multiplicity=\"*\" />\r\n");
this.WriteObjects("      <ReferentialConstraint>\r\n");
this.WriteObjects("        <Principal Role=\"",  containerTypeName , "\">\r\n");
this.WriteObjects("          <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("        </Principal>\r\n");
this.WriteObjects("        <Dependent Role=\"CollectionEntry\">\r\n");
this.WriteObjects("          <PropertyRef Name=\"fk_",  containerTypeName , "\" />\r\n");
this.WriteObjects("        </Dependent>\r\n");
this.WriteObjects("      </ReferentialConstraint>\r\n");
this.WriteObjects("    </Association>\r\n");
this.WriteObjects("\r\n");
#line 325 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 327 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("</Schema>");

        }



    }
}