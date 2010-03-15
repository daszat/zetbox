
# Intro

This file shows how N:M relations are implemented with EntityFramwork here. To 
explain it this example is used: ObjectClass'es implement multiple Interface's 
without their knowledge. Interfaces can be implemented by multiple 
ObjectClass'es.


# Metadata

Confusingly, the next example uses the "real" ObjectClass from the database to describe the (different) ObjectClass of the example.

var interf = ctx.Create<ObjectClass>();
interf.Name = "Interface";
interf.TableName = "Interfaces";

var cls = ctx.Create<ObjectClass>();
cls.Name = "ObjectClass";
cls.TableName = "ObjectClasses";

var impl = ctx.Create<ObjectReferenceProperty>();
impl.Name = "ImplementsInterfaces";
impl.GetReferencedObjectClass() = interf;
impl.ObjectClass = cls;
impl.IsIndexed = false;
impl.Constraints.Add(ctx.Create<NotNullableConstraint>());


# Kistl.Objects Interface

	interface ObjectClass {
		ICollection<Interface> ImplementsInterfaces;
	}

	interface Interface {
	}


# Database

In the database this, of course, must be stored as a auxilary table:

	CREATE TABLE ObjectClasses ( ID int PRIMARY KEY );

	CREATE TABLE Interfaces ( ID int PRIMARY KEY );

	CREATE TABLE ObjectClasses_ImplementsInterfacesCollection (
		ID int PRIMARY KEY,
		fk_ObjectClass int REFERENCES ObjectClasses (ID),
		fk_ImplementsInterfaces int REFERENCES Interfaces (ID)
	);




# EF C# Implementation

Actual class names are postfixed "__Implementation__" to lessen possibilities 
of name-clashes.

	[EdmEntityType(NamespaceName="Model", Name="ObjectClass")]
	class ObjectClass__Implementation__
		: ObjectClass
	{

		// returns a EntityCollectionEntryValueWrapper
		ICollection<Interface> ImplementsInterfaces { get; }

		// this is the actual property which is seen by EF
		[EdmRelationshipNavigationProperty("Model",
			"FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_Parent", "B_ObjectClass_ImplementsInterfacesCollectionEntry")]
		EntityCollection<ObjectClass_ImplementsInterfacesCollectionEntry__Implementation__> ImplementsInterfaces__Implementation__ { get; }

	}

This class represents the ObjectClasses_ImplementsInterfacesCollection table.
The implementations of the ParentImpl and ValueImpl use EF's 
RelationshipManager to retrieve and set the referenced ObjectClass'es and 
Interface's

	[EdmEntityType(NamespaceName="Model", Name="ObjectClass_ImplementsInterfacesCollectionEntry")]
	class ObjectClass_ImplementsInterfacesCollectionEntry__Implementation__
	{

		[EdmRelationshipNavigationPropertyAttribute("Model",
			"FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_ImplementsInterfaces" ,
			"A_Interface")]
		public Interface__Implementation__ ValueImpl { get; set; }

		[EdmRelationshipNavigationPropertyAttribute("Model",
			"FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_Parent",
			"A_ObjectClass")]
		public Kistl.App.Base.ObjectClass__Implementation__ ParentImpl { get; set; }

	}


Additionally two EntityFramework assembly Attributes are needed to describe 
the underlying Relationship. The first defines the relation from the 
ObjectClass to the collection entry, the second from the collection entry to 
the Interface.

[assembly: EdmRelationship("Model", "FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_Parent",
	"A_ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(ObjectClass__Implementation__),
	"B_ObjectClass_ImplementsInterfacesCollectionEntry",
	                 RelationshipMultiplicity.Many,      typeof(ObjectClass_ImplementsInterfacesCollectionEntry__Implementation__))]

[assembly: EdmRelationship("Model", "FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_ImplementsInterfaces",
	"A_Interface", RelationshipMultiplicity.ZeroOrOne, typeof(Interface__Implementation__),
	"B_ObjectClass_ImplementsInterfacesCollectionEntry",
	               RelationshipMultiplicity.Many,      typeof(ObjectClass_ImplementsInterfacesCollectionEntry__Implementation__))]



# EDMX

Finally the various parts have to be defined in the EDMX:

## CSDL

	<EntityContainer Name="Entities">
		<EntitySet Name="ObjectClass" EntityType="Model.ObjectClass" />
		<EntitySet Name="Interface" EntityType="Model.Interface" />
		<EntitySet Name="ObjectClass_ImplementsInterfacesCollectionEntry" EntityType="Model.ObjectClass_ImplementsInterfacesCollectionEntry" />
		<AssociationSet Name="FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_Parent" Association="Model.FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_Parent">
			<End Role="A_ObjectClass" EntitySet="ObjectClass" />
			<End Role="B_ObjectClass_ImplementsInterfacesCollectionEntry" EntitySet="ObjectClass_ImplementsInterfacesCollectionEntry" />
		</AssociationSet>
		<AssociationSet Name="FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_ImplementsInterfaces" Association="Model.FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_ImplementsInterfaces">
			<End Role="A_Interface" EntitySet="Interface" />
			<End Role="B_ObjectClass_ImplementsInterfacesCollectionEntry" EntitySet="ObjectClass_ImplementsInterfacesCollectionEntry" />
		</AssociationSet>
	</EntityContainer>

	<EntityType Name="ObjectClass">
		<Key>
			<PropertyRef Name="ID" />
		</Key>
		<Property Name="ID" Type="Int32" Nullable="false" />
		<NavigationProperty Name="ImplementsInterfaces__Implementation__"
			Relationship="Model.FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_Parent"
			FromRole="A_ObjectClass"
			ToRole="B_ObjectClass_ImplementsInterfacesCollectionEntry" />
	</EntityType>

	<EntityType Name="ObjectClass_ImplementsInterfacesCollectionEntry">
		<Key>
			<PropertyRef Name="ID" />
		</Key>
		<Property Name="ID" Type="Int32" Nullable="false" />
		<NavigationProperty Name="ValueImpl"
			Relationship="Model.FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_ImplementsInterfaces"
			FromRole="B_ObjectClass_ImplementsInterfacesCollectionEntry"
			ToRole="A_Interface" />
		<NavigationProperty Name="ParentImpl"
			Relationship="Model.FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_Parent"
			FromRole="B_ObjectClass_ImplementsInterfacesCollectionEntry"
			ToRole="A_ObjectClass" />
	</EntityType>

	<EntityType Name="Interface">
		<Key>
			<PropertyRef Name="ID" />
		</Key>
		<Property Name="ID" Type="Int32" Nullable="false" />
	</EntityType>

	<Association Name="FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_ImplementsInterfaces">
		<End Role="A_Interface" Type="Model.Interface" Multiplicity="0..1" />
		<End Role="B_ObjectClass_ImplementsInterfacesCollectionEntry" Type="Model.ObjectClass_ImplementsInterfacesCollectionEntry" Multiplicity="*" />
	</Association>

	<Association Name="FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_Parent">
		<End Role="A_ObjectClass" Type="Model.ObjectClass" Multiplicity="0..1" />
		<End Role="B_ObjectClass_ImplementsInterfacesCollectionEntry" Type="Model.ObjectClass_ImplementsInterfacesCollectionEntry" Multiplicity="*" />
	</Association>



## SSDL

	<EntityContainer Name="dbo">
		<EntitySet Name="ObjectClass" EntityType="Model.Store.ObjectClass" Table="ObjectClasses" />
		<EntitySet Name="Interface" EntityType="Model.Store.Interface" Table="Interfaces" />
		<EntitySet Name="ObjectClass_ImplementsInterfacesCollectionEntry" EntityType="Model.Store.ObjectClass_ImplementsInterfacesCollectionEntry" Table="ObjectClasses_ImplementsInterfacesCollection" />
		<AssociationSet Name="FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_ImplementsInterfaces" Association="Model.Store.FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_ImplementsInterfaces">
			<End Role="A_Interface" EntitySet="Interface" />
			<End Role="B_ObjectClass_ImplementsInterfacesCollectionEntry" EntitySet="ObjectClass_ImplementsInterfacesCollectionEntry" />
		</AssociationSet>
		<AssociationSet Name="FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_Parent" Association="Model.Store.FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_Parent">
			<End Role="A_ObjectClass" EntitySet="ObjectClass" />
			<End Role="B_ObjectClass_ImplementsInterfacesCollectionEntry" EntitySet="ObjectClass_ImplementsInterfacesCollectionEntry" />
		</AssociationSet>
	</EntityContainer>

	<EntityType Name="ObjectClass">
		<Key>
			<PropertyRef Name="ID" />
		</Key>
		<Property Name="ID" Type="int" Nullable="false" />
	</EntityType>

	<EntityType Name="Interface">
		<Key>
			<PropertyRef Name="ID" />
		</Key>
		<Property Name="ID" Type="int" Nullable="false" />
	</EntityType>
	
	<EntityType Name="ObjectClass_ImplementsInterfacesCollectionEntry">
		<Key>
			<PropertyRef Name="ID" />
		</Key>
		<Property Name="ID" Type="int" Nullable="false" StoreGeneratedPattern="Identity" />
		<Property Name="fk_ObjectClass" Type="int" Nullable="true" />
		<Property Name="fk_ImplementsInterfaces" Type="int" />
	</EntityType>

	<Association Name="FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_ImplementsInterfaces">
		<End Role="A_Interface" Multiplicity="0..1" Type="Model.Store.Interface" />
		<End Role="B_ObjectClass_ImplementsInterfacesCollectionEntry" Multiplicity="*" Type="Model.Store.ObjectClass_ImplementsInterfacesCollectionEntry" />
		<ReferentialConstraint>
			<Principal Role="A_Interface">
				<PropertyRef Name="ID" />
			</Principal>
			<Dependent Role="B_ObjectClass_ImplementsInterfacesCollectionEntry">
				<PropertyRef Name="fk_ImplementsInterfaces" />
			</Dependent>
		</ReferentialConstraint>
	</Association>

	<Association Name="FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_Parent">
		<End Role="A_ObjectClass" Multiplicity="0..1" Type="Model.Store.ObjectClass" />
		<End Role="B_ObjectClass_ImplementsInterfacesCollectionEntry"Multiplicity="*"  Type="Model.Store.ObjectClass_ImplementsInterfacesCollectionEntry" />
		<ReferentialConstraint>
			<Principal Role="A_ObjectClass">
				<PropertyRef Name="ID" />
			</Principal>
			<Dependent Role="B_ObjectClass_ImplementsInterfacesCollectionEntry">
				<PropertyRef Name="fk_ObjectClass" />
			</Dependent>
		</ReferentialConstraint>
	</Association>

## MSL

	<EntityContainerMapping StorageEntityContainer="dbo" CdmEntityContainer="Entities">
		<EntitySetMapping Name="ObjectClass">
			<EntityTypeMapping TypeName="IsTypeOf(Model.ObjectClass)">
				<MappingFragment StoreEntitySet="ObjectClass">
					<ScalarProperty Name="ID" ColumnName="ID" />
				</MappingFragment>
			</EntityTypeMapping>
		</EntitySetMapping>
		<EntitySetMapping Name="Interface">
			<EntityTypeMapping TypeName="IsTypeOf(Model.Interface)">
				<MappingFragment StoreEntitySet="Interface">
					<ScalarProperty Name="ID" ColumnName="ID" />
				</MappingFragment>
			</EntityTypeMapping>
		</EntitySetMapping>
		<EntitySetMapping Name="ObjectClass_ImplementsInterfacesCollectionEntry">
			<EntityTypeMapping TypeName="IsTypeOf(Model.ObjectClass_ImplementsInterfacesCollectionEntry)">
				<MappingFragment StoreEntitySet="ObjectClass_ImplementsInterfacesCollectionEntry">
					<ScalarProperty Name="ID" ColumnName="ID" />
				</MappingFragment>
			</EntityTypeMapping>
		</EntitySetMapping>
		<AssociationSetMapping Name="FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_ImplementsInterfaces" TypeName="Model.FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_ImplementsInterfaces" StoreEntitySet="ObjectClass_ImplementsInterfacesCollectionEntry">
			<EndProperty Name="A_Interface">
				<ScalarProperty Name="ID" ColumnName="fk_ImplementsInterfaces" />
			</EndProperty>
			<EndProperty Name="B_ObjectClass_ImplementsInterfacesCollectionEntry">
				<ScalarProperty Name="ID" ColumnName="ID" />
			</EndProperty>
			<Condition ColumnName="fk_ImplementsInterfaces" IsNull="false" />
		</AssociationSetMapping>
		<AssociationSetMapping Name="FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_Parent" TypeName="Model.FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_Parent" StoreEntitySet="ObjectClass_ImplementsInterfacesCollectionEntry">
			<EndProperty Name="A_ObjectClass">
				<ScalarProperty Name="ID" ColumnName="fk_ObjectClass" />
			</EndProperty>
			<EndProperty Name="B_ObjectClass_ImplementsInterfacesCollectionEntry">
				<ScalarProperty Name="ID" ColumnName="ID" />
			</EndProperty>
			<Condition ColumnName="fk_ObjectClass" IsNull="false" />
		</AssociationSetMapping>
	</EntityContainerMapping>



# Reversing the Relationship

In some cases the reverse relationship ("Interface.ImplementedBy") should be 
exposed to the consumer too. In this case, a few modifications have to be 
made to enable this.

## Metadata

To create the reverse `ImplementedBy` property on the `Interface`, another 
ObjectReferenceProperty with a Relation to the existing ORP has to be created.

## Kistl.Objects Interface

Add a `ICollection<ObjectClass> ImplementedBy { get; }` property to the `Interface` class.

## Database

Being set based and not property based, nothing has to be modified here.

## EF C# Implementation

The reverse relationship reuses the 
`ObjectClass_ImplementsInterfacesCollectionEntry__Implementation__` class. 
Only the returned wrapper is different to implement the appropriate semantics.

	class Interface {
	
		// returns a EntityCollectionEntryValueWrapper
		ICollection<ObjectClass> ImplementedBy { get; }
		
		// again, this is the actual property which is seen by EF
		[EdmRelationshipNavigationProperty("Model", "FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_ImplementsInterfaces",
			"B_ObjectClass_ImplementsInterfacesCollectionEntry")]
		EntityCollection<ObjectClass_ImplementsInterfacesCollectionEntry__Implementation__> ImplementedBy__Implementation__ { get; }
		
	}

Since the reverse Property doesn't add any "new" relationships, the existing `EdmRelationship` attributes suffice.

### EDMX, CSDL

Only the new `NavigationProperty` has to be added:

	<NavigationProperty Name="ImplementedBy__Implementation__"
		Relationship="Model.FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_ImplementsInterfaces"
		FromRole="A_Interface"
		ToRole="B_ObjectClass_ImplementsInterfacesCollectionEntry" />

### EDMX, SSDL

Since the database is unchanged, the SSDL stays untouched.

### EDMX, MSL

Interestingly enough the MSL, too, contains already enough information for the 
reverse relationship to work.


# Hidden Details

For details to the various wrapping classes and implementations of properties, 
please look directly into the generated code or the Kistl.API.Server.

