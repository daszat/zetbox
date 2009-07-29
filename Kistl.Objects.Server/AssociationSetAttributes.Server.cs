using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Xml;
using System.Xml.Serialization;

using Kistl.API;
using Kistl.DALProvider.EF;


	/*
    Relation: FK_Auftrag_has_Kunde
    A: ZeroOrMore Auftrag as Auftrag
    B: ZeroOrOne Kunde as Kunde
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Auftrag_has_Kunde",
    "Auftrag", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Auftrag__Implementation__),
    "Kunde", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Kunde__Implementation__)
    )]


	/*
    Relation: FK_Auftrag_has_Mitarbeiter
    A: ZeroOrMore Auftrag as Auftrag
    B: ZeroOrOne Mitarbeiter as Mitarbeiter
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Auftrag_has_Mitarbeiter",
    "Auftrag", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Auftrag__Implementation__),
    "Mitarbeiter", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Mitarbeiter__Implementation__)
    )]


	/*
    Relation: FK_CLRObjectParameter_has_Assembly
    A: ZeroOrMore CLRObjectParameter as CLRObjectParameter
    B: ZeroOrOne Assembly as Assembly
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_CLRObjectParameter_has_Assembly",
    "CLRObjectParameter", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.CLRObjectParameter__Implementation__),
    "Assembly", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    Relation: FK_DataType_has_Icon
    A: ZeroOrMore DataType as DataType
    B: ZeroOrOne Icon as DefaultIcon
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_DataType_has_Icon",
    "DataType", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.DataType__Implementation__),
    "DefaultIcon", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Icon__Implementation__)
    )]


	/*
    Relation: FK_DataType_has_Method
    A: One DataType as ObjectClass
    B: ZeroOrMore Method as Methods
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_DataType_has_Method",
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DataType__Implementation__),
    "Methods", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Method__Implementation__)
    )]


	/*
    Relation: FK_DataType_has_MethodInvocation
    A: One DataType as InvokeOnObjectClass
    B: ZeroOrMore MethodInvocation as MethodInvocations
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_DataType_has_MethodInvocation",
    "InvokeOnObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DataType__Implementation__),
    "MethodInvocations", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation__Implementation__)
    )]


	/*
    Relation: FK_DataType_has_Property
    A: One DataType as ObjectClass
    B: ZeroOrMore Property as Properties
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_DataType_has_Property",
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DataType__Implementation__),
    "Properties", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Property__Implementation__)
    )]


	/*
    Relation: FK_Enumeration_has_EnumerationEntry
    A: One Enumeration as Enumeration
    B: ZeroOrMore EnumerationEntry as EnumerationEntries
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Enumeration_has_EnumerationEntry",
    "Enumeration", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Enumeration__Implementation__),
    "EnumerationEntries", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.EnumerationEntry__Implementation__)
    )]


	/*
    Relation: FK_EnumerationProperty_has_Enumeration
    A: ZeroOrMore EnumerationProperty as EnumerationProperty
    B: ZeroOrOne Enumeration as Enumeration
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_EnumerationProperty_has_Enumeration",
    "EnumerationProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.EnumerationProperty__Implementation__),
    "Enumeration", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Enumeration__Implementation__)
    )]


	/*
    Relation: FK_Icon_has_Module
    A: ZeroOrMore Icon as Icon
    B: ZeroOrOne Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Icon_has_Module",
    "Icon", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Icon__Implementation__),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__)
    )]


	/*
    Relation: FK_Method_has_BaseParameter
    A: One Method as Method
    B: ZeroOrMore BaseParameter as Parameter
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Method_has_BaseParameter",
    "Method", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Method__Implementation__),
    "Parameter", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.BaseParameter__Implementation__)
    )]


	/*
    Relation: FK_Method_has_MethodInvocation
    A: One Method as Method
    B: ZeroOrMore MethodInvocation as MethodInvokations
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Method_has_MethodInvocation",
    "Method", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Method__Implementation__),
    "MethodInvokations", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation__Implementation__)
    )]


	/*
    Relation: FK_Method_has_Module
    A: ZeroOrMore Method as Method
    B: ZeroOrOne Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Method_has_Module",
    "Method", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Method__Implementation__),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__)
    )]


	/*
    Relation: FK_MethodInvocation_has_Module
    A: ZeroOrMore MethodInvocation as MethodInvocation
    B: ZeroOrOne Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_MethodInvocation_has_Module",
    "MethodInvocation", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation__Implementation__),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__)
    )]


	/*
    Relation: FK_MethodInvocation_has_TypeRef
    A: ZeroOrMore MethodInvocation as MethodInvocation
    B: ZeroOrOne TypeRef as Implementor
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_MethodInvocation_has_TypeRef",
    "MethodInvocation", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation__Implementation__),
    "Implementor", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__)
    )]


	/*
    Relation: FK_Module_has_Assembly
    A: One Module as Module
    B: ZeroOrMore Assembly as Assemblies
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Module_has_Assembly",
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__),
    "Assemblies", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    Relation: FK_Module_has_DataType
    A: One Module as Module
    B: ZeroOrMore DataType as DataTypes
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Module_has_DataType",
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__),
    "DataTypes", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.DataType__Implementation__)
    )]


	/*
    Relation: FK_Module_has_Relation
    A: One Module as Module
    B: ZeroOrMore Relation as Relation
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Module_has_Relation",
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__),
    "Relation", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Relation__Implementation__)
    )]


	/*
    Relation: FK_ObjectClass_has_ObjectClass
    A: ZeroOrOne ObjectClass as BaseObjectClass
    B: ZeroOrMore ObjectClass as SubClasses
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ObjectClass_has_ObjectClass",
    "BaseObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClass__Implementation__),
    "SubClasses", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass__Implementation__)
    )]


	/*
    Relation: FK_ObjectClass_has_PresentableModelDescriptor
    A: ZeroOrMore ObjectClass as Presentable
    B: One PresentableModelDescriptor as DefaultPresentableModelDescriptor
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ObjectClass_has_PresentableModelDescriptor",
    "Presentable", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass__Implementation__),
    "DefaultPresentableModelDescriptor", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.PresentableModelDescriptor__Implementation__)
    )]


	/*
    Relation: FK_ObjectClass_implements_Interface
    A: ZeroOrMore ObjectClass as ObjectClass
    B: ZeroOrMore Interface as ImplementsInterfaces
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_ObjectClass_implements_Interface_ObjectClass",
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClass__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass_implements_Interface_RelationEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_ObjectClass_implements_Interface_ImplementsInterfaces",
    "ImplementsInterfaces", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Interface__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass_implements_Interface_RelationEntry__Implementation__)
    )]

	/*
    Relation: FK_ObjectParameter_has_DataType
    A: ZeroOrMore ObjectParameter as ObjectParameter
    B: ZeroOrOne DataType as DataType
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ObjectParameter_has_DataType",
    "ObjectParameter", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectParameter__Implementation__),
    "DataType", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DataType__Implementation__)
    )]


	/*
    Relation: FK_ObjectReferenceProperty_has_ObjectClass
    A: ZeroOrMore ObjectReferenceProperty as ObjectReferenceProperty
    B: ZeroOrOne ObjectClass as ReferenceObjectClass
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ObjectReferenceProperty_has_ObjectClass",
    "ObjectReferenceProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectReferenceProperty__Implementation__),
    "ReferenceObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClass__Implementation__)
    )]


	/*
    Relation: FK_PresenceRecord_has_Mitarbeiter
    A: ZeroOrMore PresenceRecord as PresenceRecord
    B: One Mitarbeiter as Mitarbeiter
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_PresenceRecord_has_Mitarbeiter",
    "PresenceRecord", RelationshipMultiplicity.Many, typeof(Kistl.App.TimeRecords.PresenceRecord__Implementation__),
    "Mitarbeiter", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Mitarbeiter__Implementation__)
    )]


	/*
    Relation: FK_PresentableModelDescriptor_has_ControlKind
    A: ZeroOrOne PresentableModelDescriptor as Presentable
    B: ZeroOrOne ControlKind as DefaultKind
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_PresentableModelDescriptor_has_ControlKind",
    "Presentable", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.PresentableModelDescriptor__Implementation__),
    "DefaultKind", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ControlKind__Implementation__)
    )]


	/*
    Relation: FK_PresentableModelDescriptor_has_Module
    A: ZeroOrMore PresentableModelDescriptor as PresentableModelDescriptor
    B: ZeroOrOne Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_PresentableModelDescriptor_has_Module",
    "PresentableModelDescriptor", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.PresentableModelDescriptor__Implementation__),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__)
    )]


	/*
    Relation: FK_PresentableModelDescriptor_has_TypeRef
    A: ZeroOrMore PresentableModelDescriptor as Descriptor
    B: One TypeRef as PresentableModelRef
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_PresentableModelDescriptor_has_TypeRef",
    "Descriptor", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.PresentableModelDescriptor__Implementation__),
    "PresentableModelRef", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__)
    )]


	/*
    Relation: FK_Projekt_haben_Mitarbeiter
    A: ZeroOrMore Projekt as Projekte
    B: ZeroOrMore Mitarbeiter as Mitarbeiter
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Projekt_haben_Mitarbeiter_Projekte",
    "Projekte", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Projekt__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Projekt_haben_Mitarbeiter_RelationEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Projekt_haben_Mitarbeiter_Mitarbeiter",
    "Mitarbeiter", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Mitarbeiter__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Projekt_haben_Mitarbeiter_RelationEntry__Implementation__)
    )]

	/*
    Relation: FK_Projekt_has_Auftrag
    A: ZeroOrOne Projekt as Projekt
    B: ZeroOrMore Auftrag as Auftraege
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Projekt_has_Auftrag",
    "Projekt", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Projekt__Implementation__),
    "Auftraege", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Auftrag__Implementation__)
    )]


	/*
    Relation: FK_Projekt_has_Task
    A: ZeroOrOne Projekt as Projekt
    B: ZeroOrMore Task as Tasks
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Projekt_has_Task",
    "Projekt", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Projekt__Implementation__),
    "Tasks", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Task__Implementation__)
    )]


	/*
    Relation: FK_Property_has_Constraint
    A: One Property as ConstrainedProperty
    B: ZeroOrMore Constraint as Constraints
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Property_has_Constraint",
    "ConstrainedProperty", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Property__Implementation__),
    "Constraints", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Constraint__Implementation__)
    )]


	/*
    Relation: FK_Property_has_DefaultPropertyValue
    A: One Property as Property
    B: ZeroOrOne DefaultPropertyValue as DefaultValue
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Property_has_DefaultPropertyValue",
    "Property", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Property__Implementation__),
    "DefaultValue", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DefaultPropertyValue__Implementation__)
    )]


	/*
    Relation: FK_Property_has_Module
    A: ZeroOrMore Property as BaseProperty
    B: ZeroOrOne Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Property_has_Module",
    "BaseProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Property__Implementation__),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__)
    )]


	/*
    Relation: FK_Property_has_PresentableModelDescriptor
    A: ZeroOrMore Property as Property
    B: One PresentableModelDescriptor as ValueModelDescriptor
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Property_has_PresentableModelDescriptor",
    "Property", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Property__Implementation__),
    "ValueModelDescriptor", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.PresentableModelDescriptor__Implementation__)
    )]


	/*
    Relation: FK_Property_has_PropertyInvocation
    A: One Property as InvokeOnProperty
    B: ZeroOrMore PropertyInvocation as Invocations
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Property_has_PropertyInvocation",
    "InvokeOnProperty", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Property__Implementation__),
    "Invocations", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.PropertyInvocation__Implementation__)
    )]


	/*
    Relation: FK_PropertyInvocation_has_TypeRef
    A: ZeroOrMore PropertyInvocation as PropertyInvocation
    B: One TypeRef as Implementor
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_PropertyInvocation_has_TypeRef",
    "PropertyInvocation", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.PropertyInvocation__Implementation__),
    "Implementor", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__)
    )]


	/*
    Relation: FK_Relation_hasA_RelationEnd
    A: ZeroOrOne Relation as Relation
    B: ZeroOrOne RelationEnd as A
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Relation_hasA_RelationEnd",
    "Relation", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Relation__Implementation__),
    "A", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.RelationEnd__Implementation__)
    )]


	/*
    Relation: FK_Relation_hasB_RelationEnd
    A: ZeroOrOne Relation as Relation
    B: ZeroOrOne RelationEnd as B
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Relation_hasB_RelationEnd",
    "Relation", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Relation__Implementation__),
    "B", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.RelationEnd__Implementation__)
    )]


	/*
    Relation: FK_RelationEnd_has_ObjectClass
    A: ZeroOrMore RelationEnd as RelationEnd
    B: ZeroOrOne ObjectClass as Type
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_RelationEnd_has_ObjectClass",
    "RelationEnd", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.RelationEnd__Implementation__),
    "Type", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClass__Implementation__)
    )]


	/*
    Relation: FK_RelationEnd_has_ObjectReferenceProperty
    A: ZeroOrOne RelationEnd as RelationEnd
    B: ZeroOrOne ObjectReferenceProperty as Navigator
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_RelationEnd_has_ObjectReferenceProperty",
    "RelationEnd", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.RelationEnd__Implementation__),
    "Navigator", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectReferenceProperty__Implementation__)
    )]


	/*
    Relation: FK_StructProperty_has_Struct
    A: ZeroOrMore StructProperty as StructProperty
    B: ZeroOrOne Struct as StructDefinition
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_StructProperty_has_Struct",
    "StructProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.StructProperty__Implementation__),
    "StructDefinition", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Struct__Implementation__)
    )]


	/*
    Relation: FK_Template_has_Assembly
    A: ZeroOrMore Template as Template
    B: ZeroOrOne Assembly as DisplayedTypeAssembly
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Template_has_Assembly",
    "Template", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Template__Implementation__),
    "DisplayedTypeAssembly", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    Relation: FK_Template_has_Visual
    A: ZeroOrMore Template as Template
    B: ZeroOrOne Visual as VisualTree
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Template_has_Visual",
    "Template", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Template__Implementation__),
    "VisualTree", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Visual__Implementation__)
    )]


	/*
    Relation: FK_Template_hasMenu_Visual
    A: ZeroOrMore Template as Template
    B: ZeroOrMore Visual as Menu
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Template_hasMenu_Visual_Template",
    "Template", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Template__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Template_hasMenu_Visual_RelationEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Template_hasMenu_Visual_Menu",
    "Menu", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Visual__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Template_hasMenu_Visual_RelationEntry__Implementation__)
    )]

	/*
    Relation: FK_TestObjClass_has_Kunde
    A: ZeroOrMore TestObjClass as TestObjClass
    B: ZeroOrOne Kunde as ObjectProp
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_TestObjClass_has_Kunde",
    "TestObjClass", RelationshipMultiplicity.Many, typeof(Kistl.App.Test.TestObjClass__Implementation__),
    "ObjectProp", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Kunde__Implementation__)
    )]


	/*
    Relation: FK_TypeRef_has_Assembly
    A: ZeroOrMore TypeRef as TypeRef
    B: ZeroOrOne Assembly as Assembly
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_TypeRef_has_Assembly",
    "TypeRef", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.TypeRef__Implementation__),
    "Assembly", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    Relation: FK_TypeRef_has_TypeRef
    A: ZeroOrMore TypeRef as Child
    B: One TypeRef as Parent
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_TypeRef_has_TypeRef",
    "Child", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.TypeRef__Implementation__),
    "Parent", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__)
    )]


	/*
    Relation: FK_TypeRef_hasGenericArguments_TypeRef
    A: ZeroOrMore TypeRef as TypeRef
    B: ZeroOrMore TypeRef as GenericArguments
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_TypeRef_hasGenericArguments_TypeRef_TypeRef",
    "TypeRef", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.TypeRef_hasGenericArguments_TypeRef_RelationEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_TypeRef_hasGenericArguments_TypeRef_GenericArguments",
    "GenericArguments", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.TypeRef_hasGenericArguments_TypeRef_RelationEntry__Implementation__)
    )]

	/*
    Relation: FK_ViewDescriptor_has_Module
    A: ZeroOrMore ViewDescriptor as ViewDescriptor
    B: ZeroOrOne Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ViewDescriptor_has_Module",
    "ViewDescriptor", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ViewDescriptor__Implementation__),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__)
    )]


	/*
    Relation: FK_ViewDescriptor_has_PresentableModelDescriptor
    A: ZeroOrMore ViewDescriptor as View
    B: One PresentableModelDescriptor as PresentedModelDescriptor
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ViewDescriptor_has_PresentableModelDescriptor",
    "View", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ViewDescriptor__Implementation__),
    "PresentedModelDescriptor", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.PresentableModelDescriptor__Implementation__)
    )]


	/*
    Relation: FK_ViewDescriptor_has_TypeRef
    A: ZeroOrMore ViewDescriptor as View
    B: One TypeRef as ControlRef
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ViewDescriptor_has_TypeRef",
    "View", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ViewDescriptor__Implementation__),
    "ControlRef", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__)
    )]


	/*
    Relation: FK_ViewDescriptor_isof_ControlKind
    A: ZeroOrOne ViewDescriptor as Control
    B: ZeroOrOne ControlKind as Kind
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ViewDescriptor_isof_ControlKind",
    "Control", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ViewDescriptor__Implementation__),
    "Kind", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ControlKind__Implementation__)
    )]


	/*
    Relation: FK_Visual_contains_Visual
    A: ZeroOrMore Visual as Visual
    B: ZeroOrMore Visual as Children
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Visual_contains_Visual_Visual",
    "Visual", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Visual__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual_contains_Visual_RelationEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Visual_contains_Visual_Children",
    "Children", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Visual__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual_contains_Visual_RelationEntry__Implementation__)
    )]

	/*
    Relation: FK_Visual_has_Method
    A: ZeroOrMore Visual as Visual
    B: ZeroOrOne Method as Method
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Visual_has_Method",
    "Visual", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual__Implementation__),
    "Method", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Method__Implementation__)
    )]


	/*
    Relation: FK_Visual_has_Property
    A: ZeroOrMore Visual as Visual
    B: ZeroOrOne Property as Property
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Visual_has_Property",
    "Visual", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual__Implementation__),
    "Property", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Property__Implementation__)
    )]


	/*
    Relation: FK_Visual_hasContextMenu_Visual
    A: ZeroOrMore Visual as Visual
    B: ZeroOrMore Visual as ContextMenu
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Visual_hasContextMenu_Visual_Visual",
    "Visual", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Visual__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual_hasContextMenu_Visual_RelationEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Visual_hasContextMenu_Visual_ContextMenu",
    "ContextMenu", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Visual__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual_hasContextMenu_Visual_RelationEntry__Implementation__)
    )]

	/*
    Relation: FK_WorkEffort_has_Mitarbeiter
    A: ZeroOrMore WorkEffort as WorkEffort
    B: One Mitarbeiter as Mitarbeiter
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_WorkEffort_has_Mitarbeiter",
    "WorkEffort", RelationshipMultiplicity.Many, typeof(Kistl.App.TimeRecords.WorkEffort__Implementation__),
    "Mitarbeiter", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Mitarbeiter__Implementation__)
    )]


	/*
    Relation: FK_WorkEffortAccount_has_Mitarbeiter
    A: ZeroOrMore WorkEffortAccount as WorkEffortAccount
    B: ZeroOrMore Mitarbeiter as Mitarbeiter
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_WorkEffortAccount_has_Mitarbeiter_WorkEffortAccount",
    "WorkEffortAccount", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.TimeRecords.WorkEffortAccount__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.TimeRecords.WorkEffortAccount_has_Mitarbeiter_RelationEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_WorkEffortAccount_has_Mitarbeiter_Mitarbeiter",
    "Mitarbeiter", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Mitarbeiter__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.TimeRecords.WorkEffortAccount_has_Mitarbeiter_RelationEntry__Implementation__)
    )]


// object-value association
[assembly: EdmRelationship(
    "Model", "FK_Kunde_value_EMails",
    "Kunde", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Kunde__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Kunde_EMails_CollectionEntry__Implementation__)
    )]

