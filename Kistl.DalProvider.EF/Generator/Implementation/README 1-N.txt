
# Intro

This file shows how 1:N relations are implemented with EntityFramwork here. To 
explain it this example is used: A `Module` has many `Assembly`s, but an 
`Assembly` can only be part of a single `Module`.

# Metadata

//29
var assemblyCls = ctx.Create<ObjectClass>();
assemblyCls.Name = "Assembly";
assemblyCls.TableName = "Assemblies";

// 18
var moduleCls = ctx.Create<ObjectClass>();
moduleCls.Name = "Module";
moduleCls.TableName = "Modules";

// 82
var assemblies = ctx.Create<ObjectReferenceProperty>();
assemblies.Name = "Assemblies";
assemblies.IsIndexed = false;
assemblies.Constraints.Add(ctx.Create<NotNullableConstraint>());
assemblies.ObjectClass = moduleCls;
assemblies.GetReferencedObjectClass() = assemblyCls;

// 70       
var module = ctx.Create<ObjectReferenceProperty>();
module.IsIndexed = false;
module.Name = "Module";
assemblies.ObjectClass = assemblyCls;
assemblies.GetReferencedObjectClass() = moduleCls;

var rel = ctx.Create<Relation>();
rel.Storage = StorageType.Right; // See Case 765, why this is just bollocks.
rel.LeftPart = assemblies;
rel.RightPart = module;

# Kistl.Objects Interface

	interface Assembly {
		Module Module { get; set; }
	}

	interface Module {
		ICollection<Assembly> Assemblies { get; }
	}

# Database

In the database this can be stored within the Assembly table:

	CREATE TABLE Modules ( ID int PRIMARY KEY );

	CREATE TABLE Assemblies (
		ID int PRIMARY KEY,
		fk_Module int REFERENCES Module (ID)
	);


# EF C# Implementation

Actual class names are postfixed "__Implementation__" to lessen possibilities 
of name-clashes.


	[EdmEntityType(NamespaceName="Model", Name="Assembly")]
	class Assembly__Implementation__ : Assembly
	{

		// implement the interface
		public Kistl.App.Base.Module Module { get; set; }

		// this is the actual property which is seen by EF
		[EdmRelationshipNavigationPropertyAttribute("Model", "FK_Assembly_Module_Module", "A_Module")]
		public Kistl.App.Base.Module__Implementation__ Module__Implementation__ { get; set; }

	}


	[EdmEntityType(NamespaceName="Model", Name="Module")]
	class Module__Implementation__ : Module
	{

		// returns a EntityCollectionWrapper
		ICollection<Assembly> Assemblies { get; }

		// this is the actual property which is seen by EF
		[EdmRelationshipNavigationProperty("Model", "FK_Assembly_Module_Module", "B_Assembly")]
		EntityCollection<Assembly__Implementation__> Assemblies__Implementation__ { get; }

	}


Additionally a EntityFramework assembly Attribute is needed to describe 
the underlying Relationship:

	[assembly: EdmRelationship("Model", "FK_Assembly_Module_Module",
		"A_Module",   RelationshipMultiplicity.ZeroOrOne, typeof(Module__Implementation__),
		"B_Assembly", RelationshipMultiplicity.Many,      typeof(Assembly__Implementation__))]


# EDMX

Finally the various parts have to be defined in the EDMX:

## CSDL

	<EntityContainer Name="Entities">
		<EntitySet Name="Assembly" EntityType="Model.Assembly" />
		<EntitySet Name="Module" EntityType="Model.Module" />
		<AssociationSet Name="FK_Assembly_Module_Module" Association="Model.FK_Assembly_Module_Module">
			<End Role="A_Module" EntitySet="Module" />
			<End Role="B_Assembly" EntitySet="Assembly" />
		</AssociationSet>
	</EntityContainer>
	<EntityType Name="Module">
		<Key>
			<PropertyRef Name="ID" />
		</Key>
		<Property Name="ID" Type="Int32" Nullable="false" />
		<NavigationProperty Name="Assemblies__Implementation__"
			Relationship="Model.FK_Assembly_Module_Module"
			FromRole="A_Module"
			ToRole="B_Assembly" />
	</EntityType>
	<EntityType Name="Assembly">
		<Key>
			<PropertyRef Name="ID" />
		</Key>
		<Property Name="ID" Type="Int32" Nullable="false" />
		<NavigationProperty Name="Module__Implementation__"
			Relationship="Model.FK_Assembly_Module_Module"
			FromRole="B_Assembly"
			ToRole="A_Module" />
	</EntityType>

## SSDL

	<EntityContainer Name="dbo">
		<EntitySet Name="Module" EntityType="Model.Store.Module" Table="Modules" />
		<EntitySet Name="Assembly" EntityType="Model.Store.Assembly" Table="Assemblies" />
		<AssociationSet Name="FK_Assembly_Module_Module" Association="Model.Store.FK_Assembly_Module_Module">
			<End Role="A_Module" EntitySet="Module" />
			<End Role="B_Assembly" EntitySet="Assembly" />
		</AssociationSet>
	</EntityContainer>
	<EntityType Name="Module">
		<Key>
			<PropertyRef Name="ID" />
		</Key>
		<Property Name="ID" Type="int" Nullable="false" />
	</EntityType>
	<EntityType Name="Assembly">
		<Key>
			<PropertyRef Name="ID" />
		</Key>
		<Property Name="ID" Type="int" Nullable="false" />
		<Property Name="fk_Module" Type="int" />
	</EntityType>


## MSL

	<EntityContainerMapping StorageEntityContainer="dbo" CdmEntityContainer="Entities">
		<EntitySetMapping Name="Module">
			<EntityTypeMapping TypeName="IsTypeOf(Model.Module)">
				<MappingFragment StoreEntitySet="Module">
					<ScalarProperty Name="ID" ColumnName="ID" />
					<ScalarProperty Name="Namespace" ColumnName="Namespace" />
					<ScalarProperty Name="Name" ColumnName="Name" />
					<ScalarProperty Name="Description" ColumnName="Description" />
				</MappingFragment>
			</EntityTypeMapping>
		</EntitySetMapping>
		<EntitySetMapping Name="Assembly">
			<EntityTypeMapping TypeName="IsTypeOf(Model.Assembly)">
				<MappingFragment StoreEntitySet="Assembly">
					<ScalarProperty Name="ID" ColumnName="ID" />
					<ScalarProperty Name="Name" ColumnName="Name" />
					<ScalarProperty Name="IsClientAssembly" ColumnName="IsClientAssembly" />
				</MappingFragment>
			</EntityTypeMapping>
		</EntitySetMapping>
		<AssociationSetMapping Name="FK_Assembly_Module_Module" TypeName="Model.FK_Assembly_Module_Module" StoreEntitySet="Assembly">
			<EndProperty Name="A_Module">
				<ScalarProperty Name="ID" ColumnName="fk_Module" />
			</EndProperty>
			<EndProperty Name="B_Assembly">
				<ScalarProperty Name="ID" ColumnName="ID" />
			</EndProperty>
			<Condition ColumnName="fk_Module" IsNull="false" />
		</AssociationSetMapping>
	</EntityContainerMapping>




