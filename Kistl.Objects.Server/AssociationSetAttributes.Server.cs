using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Xml;
using System.Xml.Serialization;

using Kistl.API;
using Kistl.DALProvider.EF;


	/*
    Relation: FK_Auftrag_Kunde_Auftrag_34
    A: 3 Auftrag as Auftrag
    B: 1 Kunde as Kunde
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Auftrag_Kunde_Auftrag_34",
    "Auftrag", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Auftrag__Implementation__),
    "Kunde", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Kunde__Implementation__)
    )]


	/*
    Relation: FK_Auftrag_Mitarbeiter_Auftrag_29
    A: 3 Auftrag as Auftrag
    B: 1 Mitarbeiter as Mitarbeiter
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Auftrag_Mitarbeiter_Auftrag_29",
    "Auftrag", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Auftrag__Implementation__),
    "Mitarbeiter", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Mitarbeiter__Implementation__)
    )]


	/*
    Relation: FK_BackReferenceProperty_ObjectReferenceProperty_BackReferenceProperty_28
    A: 3 BackReferenceProperty as BackReferenceProperty
    B: 1 ObjectReferenceProperty as ReferenceProperty
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_BackReferenceProperty_ObjectReferenceProperty_BackReferenceProperty_28",
    "BackReferenceProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.BackReferenceProperty__Implementation__),
    "ReferenceProperty", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectReferenceProperty__Implementation__)
    )]


	/*
    Relation: FK_BaseProperty_Constraint_ConstrainedProperty_62
    A: 2 BaseProperty as ConstrainedProperty
    B: 3 Constraint as Constraints
    Preferred Storage: 2
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_BaseProperty_Constraint_ConstrainedProperty_62",
    "ConstrainedProperty", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.BaseProperty__Implementation__),
    "Constraints", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Constraint__Implementation__)
    )]


	/*
    Relation: FK_BaseProperty_Module_BaseProperty_37
    A: 3 BaseProperty as BaseProperty
    B: 1 Module as Module
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_BaseProperty_Module_BaseProperty_37",
    "BaseProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.BaseProperty__Implementation__),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__)
    )]


	/*
    Relation: FK_CLRObjectParameter_Assembly_CLRObjectParameter_46
    A: 3 CLRObjectParameter as CLRObjectParameter
    B: 1 Assembly as Assembly
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_CLRObjectParameter_Assembly_CLRObjectParameter_46",
    "CLRObjectParameter", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.CLRObjectParameter__Implementation__),
    "Assembly", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    Relation: FK_ControlInfo_Assembly_ControlInfo_51
    A: 3 ControlInfo as ControlInfo
    B: 1 Assembly as Assembly
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ControlInfo_Assembly_ControlInfo_51",
    "ControlInfo", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ControlInfo__Implementation__),
    "Assembly", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    Relation: FK_DataType_BaseProperty_ObjectClass_19
    A: 2 DataType as ObjectClass
    B: 3 BaseProperty as Properties
    Preferred Storage: 2
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_DataType_BaseProperty_ObjectClass_19",
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DataType__Implementation__),
    "Properties", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.BaseProperty__Implementation__)
    )]


	/*
    Relation: FK_DataType_Icon_DataType_35
    A: 3 DataType as DataType
    B: 1 Icon as DefaultIcon
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_DataType_Icon_DataType_35",
    "DataType", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.DataType__Implementation__),
    "DefaultIcon", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Icon__Implementation__)
    )]


	/*
    Relation: FK_DataType_Method_ObjectClass_25
    A: 2 DataType as ObjectClass
    B: 3 Method as Methods
    Preferred Storage: 2
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_DataType_Method_ObjectClass_25",
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DataType__Implementation__),
    "Methods", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Method__Implementation__)
    )]


	/*
    Relation: FK_DataType_MethodInvocation_InvokeOnObjectClass_41
    A: 2 DataType as InvokeOnObjectClass
    B: 3 MethodInvocation as MethodInvocations
    Preferred Storage: 2
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_DataType_MethodInvocation_InvokeOnObjectClass_41",
    "InvokeOnObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DataType__Implementation__),
    "MethodInvocations", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation__Implementation__)
    )]


	/*
    Relation: FK_Enumeration_EnumerationEntry_Enumeration_47
    A: 2 Enumeration as Enumeration
    B: 3 EnumerationEntry as EnumerationEntries
    Preferred Storage: 2
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Enumeration_EnumerationEntry_Enumeration_47",
    "Enumeration", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Enumeration__Implementation__),
    "EnumerationEntries", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.EnumerationEntry__Implementation__)
    )]


	/*
    Relation: FK_EnumerationProperty_Enumeration_EnumerationProperty_48
    A: 3 EnumerationProperty as EnumerationProperty
    B: 1 Enumeration as Enumeration
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_EnumerationProperty_Enumeration_EnumerationProperty_48",
    "EnumerationProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.EnumerationProperty__Implementation__),
    "Enumeration", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Enumeration__Implementation__)
    )]


	/*
    Relation: FK_Method_BaseParameter_Method_44
    A: 2 Method as Method
    B: 3 BaseParameter as Parameter
    Preferred Storage: 2
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Method_BaseParameter_Method_44",
    "Method", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Method__Implementation__),
    "Parameter", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.BaseParameter__Implementation__)
    )]


	/*
    Relation: FK_Method_MethodInvocation_Method_39
    A: 2 Method as Method
    B: 3 MethodInvocation as MethodInvokations
    Preferred Storage: 2
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Method_MethodInvocation_Method_39",
    "Method", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Method__Implementation__),
    "MethodInvokations", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation__Implementation__)
    )]


	/*
    Relation: FK_Method_Module_Method_38
    A: 3 Method as Method
    B: 1 Module as Module
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Method_Module_Method_38",
    "Method", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Method__Implementation__),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__)
    )]


	/*
    Relation: FK_MethodInvocation_Module_MethodInvocation_40
    A: 3 MethodInvocation as MethodInvocation
    B: 1 Module as Module
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_MethodInvocation_Module_MethodInvocation_40",
    "MethodInvocation", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation__Implementation__),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__)
    )]


	/*
    Relation: FK_MethodInvocation_TypeRef_MethodInvocation_67
    A: 3 MethodInvocation as MethodInvocation
    B: 1 TypeRef as Implementor
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_MethodInvocation_TypeRef_MethodInvocation_67",
    "MethodInvocation", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation__Implementation__),
    "Implementor", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__)
    )]


	/*
    Relation: FK_Module_Assembly_Module_36
    A: 2 Module as Module
    B: 3 Assembly as Assemblies
    Preferred Storage: 2
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Module_Assembly_Module_36",
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__),
    "Assemblies", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    Relation: FK_Module_DataType_Module_26
    A: 2 Module as Module
    B: 3 DataType as DataTypes
    Preferred Storage: 2
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Module_DataType_Module_26",
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__),
    "DataTypes", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.DataType__Implementation__)
    )]


	/*
    Relation: FK_ObjectClass_Interface_ObjectClass_49
    A: 3 ObjectClass as ObjectClass
    B: 3 Interface as ImplementsInterfaces
    Preferred Storage: 4
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_ObjectClass_Interface_ObjectClass_49",
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClass__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass_ImplementsInterfaces49CollectionEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_ObjectClass_Interface_ImplementsInterfaces_49",
    "ImplementsInterfaces", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Interface__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass_ImplementsInterfaces49CollectionEntry__Implementation__)
    )]

	/*
    Relation: FK_ObjectClass_ObjectClass_BaseObjectClass_24
    A: 1 ObjectClass as BaseObjectClass
    B: 3 ObjectClass as SubClasses
    Preferred Storage: 2
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ObjectClass_ObjectClass_BaseObjectClass_24",
    "BaseObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClass__Implementation__),
    "SubClasses", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass__Implementation__)
    )]


	/*
    Relation: FK_ObjectClass_TypeRef_ObjectClass_70
    A: 3 ObjectClass as ObjectClass
    B: 1 TypeRef as DefaultModel
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ObjectClass_TypeRef_ObjectClass_70",
    "ObjectClass", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass__Implementation__),
    "DefaultModel", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__)
    )]


	/*
    Relation: FK_ObjectParameter_DataType_ObjectParameter_45
    A: 3 ObjectParameter as ObjectParameter
    B: 1 DataType as DataType
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ObjectParameter_DataType_ObjectParameter_45",
    "ObjectParameter", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectParameter__Implementation__),
    "DataType", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DataType__Implementation__)
    )]


	/*
    Relation: FK_ObjectReferenceProperty_ObjectClass_ObjectReferenceProperty_27
    A: 3 ObjectReferenceProperty as ObjectReferenceProperty
    B: 1 ObjectClass as ReferenceObjectClass
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ObjectReferenceProperty_ObjectClass_ObjectReferenceProperty_27",
    "ObjectReferenceProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectReferenceProperty__Implementation__),
    "ReferenceObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClass__Implementation__)
    )]


	/*
    Relation: FK_PresenterInfo_Assembly_PresenterInfo_53
    A: 3 PresenterInfo as PresenterInfo
    B: 1 Assembly as PresenterAssembly
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_PresenterInfo_Assembly_PresenterInfo_53",
    "PresenterInfo", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.PresenterInfo__Implementation__),
    "PresenterAssembly", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    Relation: FK_PresenterInfo_Assembly_PresenterInfo_54
    A: 3 PresenterInfo as PresenterInfo
    B: 1 Assembly as DataAssembly
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_PresenterInfo_Assembly_PresenterInfo_54",
    "PresenterInfo", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.PresenterInfo__Implementation__),
    "DataAssembly", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    Relation: FK_Projekt_Auftrag_Projekt_30
    A: 1 Projekt as Projekt
    B: 3 Auftrag as Auftraege
    Preferred Storage: 2
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Projekt_Auftrag_Projekt_30",
    "Projekt", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Projekt__Implementation__),
    "Auftraege", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Auftrag__Implementation__)
    )]


	/*
    Relation: FK_Projekt_Kostentraeger_Projekt_31
    A: 2 Projekt as Projekt
    B: 3 Kostentraeger as Kostentraeger
    Preferred Storage: 2
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Projekt_Kostentraeger_Projekt_31",
    "Projekt", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Projekt__Implementation__),
    "Kostentraeger", RelationshipMultiplicity.Many, typeof(Kistl.App.Zeiterfassung.Kostentraeger__Implementation__)
    )]


	/*
    Relation: FK_Projekt_Mitarbeiter_Projekte_23
    A: 3 Projekt as Projekte
    B: 3 Mitarbeiter as Mitarbeiter
    Preferred Storage: 4
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Projekt_Mitarbeiter_Projekte_23",
    "Projekte", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Projekt__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Projekt_Mitarbeiter23CollectionEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Projekt_Mitarbeiter_Mitarbeiter_23",
    "Mitarbeiter", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Mitarbeiter__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Projekt_Mitarbeiter23CollectionEntry__Implementation__)
    )]

	/*
    Relation: FK_Projekt_Task_Projekt_22
    A: 1 Projekt as Projekt
    B: 3 Task as Tasks
    Preferred Storage: 2
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Projekt_Task_Projekt_22",
    "Projekt", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Projekt__Implementation__),
    "Tasks", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Task__Implementation__)
    )]


	/*
    Relation: FK_Relation_ObjectReferenceProperty_LeftOf_63
    A: 1 Relation as LeftOf
    B: 2 ObjectReferenceProperty as LeftPart
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Relation_ObjectReferenceProperty_LeftOf_63",
    "LeftOf", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Relation__Implementation__),
    "LeftPart", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectReferenceProperty__Implementation__)
    )]


	/*
    Relation: FK_Relation_ObjectReferenceProperty_RightOf_64
    A: 1 Relation as RightOf
    B: 2 ObjectReferenceProperty as RightPart
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Relation_ObjectReferenceProperty_RightOf_64",
    "RightOf", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Relation__Implementation__),
    "RightPart", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectReferenceProperty__Implementation__)
    )]


	/*
    Relation: FK_Relation_RelationEnd_Relation_71
    A: 3 Relation as Relation
    B: 1 RelationEnd as A
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Relation_RelationEnd_Relation_71",
    "Relation", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Relation__Implementation__),
    "A", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.RelationEnd__Implementation__)
    )]


	/*
    Relation: FK_Relation_RelationEnd_Relation_72
    A: 3 Relation as Relation
    B: 1 RelationEnd as B
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Relation_RelationEnd_Relation_72",
    "Relation", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Relation__Implementation__),
    "B", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.RelationEnd__Implementation__)
    )]


	/*
    Relation: FK_RelationEnd_ObjectClass_RelationEnd_73
    A: 3 RelationEnd as RelationEnd
    B: 1 ObjectClass as Type
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_RelationEnd_ObjectClass_RelationEnd_73",
    "RelationEnd", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.RelationEnd__Implementation__),
    "Type", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClass__Implementation__)
    )]


	/*
    Relation: FK_RelationEnd_Property_RelationEnd_74
    A: 3 RelationEnd as RelationEnd
    B: 1 Property as Navigator
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_RelationEnd_Property_RelationEnd_74",
    "RelationEnd", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.RelationEnd__Implementation__),
    "Navigator", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Property__Implementation__)
    )]


	/*
    Relation: FK_StructProperty_Struct_StructProperty_52
    A: 3 StructProperty as StructProperty
    B: 1 Struct as StructDefinition
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_StructProperty_Struct_StructProperty_52",
    "StructProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.StructProperty__Implementation__),
    "StructDefinition", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Struct__Implementation__)
    )]


	/*
    Relation: FK_Taetigkeit_Mitarbeiter_Taetigkeit_32
    A: 3 Taetigkeit as Taetigkeit
    B: 1 Mitarbeiter as Mitarbeiter
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Taetigkeit_Mitarbeiter_Taetigkeit_32",
    "Taetigkeit", RelationshipMultiplicity.Many, typeof(Kistl.App.Zeiterfassung.Taetigkeit__Implementation__),
    "Mitarbeiter", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Mitarbeiter__Implementation__)
    )]


	/*
    Relation: FK_Taetigkeit_TaetigkeitsArt_Taetigkeit_43
    A: 3 Taetigkeit as Taetigkeit
    B: 1 TaetigkeitsArt as TaetigkeitsArt
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Taetigkeit_TaetigkeitsArt_Taetigkeit_43",
    "Taetigkeit", RelationshipMultiplicity.Many, typeof(Kistl.App.Zeiterfassung.Taetigkeit__Implementation__),
    "TaetigkeitsArt", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Zeiterfassung.TaetigkeitsArt__Implementation__)
    )]


	/*
    Relation: FK_Template_Assembly_Template_59
    A: 3 Template as Template
    B: 1 Assembly as DisplayedTypeAssembly
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Template_Assembly_Template_59",
    "Template", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Template__Implementation__),
    "DisplayedTypeAssembly", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    Relation: FK_Template_Visual_Template_58
    A: 3 Template as Template
    B: 1 Visual as VisualTree
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Template_Visual_Template_58",
    "Template", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Template__Implementation__),
    "VisualTree", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Visual__Implementation__)
    )]


	/*
    Relation: FK_Template_Visual_Template_61
    A: 3 Template as Template
    B: 3 Visual as Menu
    Preferred Storage: 4
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Template_Visual_Template_61",
    "Template", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Template__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Template_Menu61CollectionEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Template_Visual_Menu_61",
    "Menu", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Visual__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Template_Menu61CollectionEntry__Implementation__)
    )]

	/*
    Relation: FK_TestObjClass_Kunde_TestObjClass_50
    A: 3 TestObjClass as TestObjClass
    B: 1 Kunde as ObjectProp
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_TestObjClass_Kunde_TestObjClass_50",
    "TestObjClass", RelationshipMultiplicity.Many, typeof(Kistl.App.Test.TestObjClass__Implementation__),
    "ObjectProp", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Kunde__Implementation__)
    )]


	/*
    Relation: FK_TypeRef_Assembly_TypeRef_65
    A: 3 TypeRef as TypeRef
    B: 1 Assembly as Assembly
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_TypeRef_Assembly_TypeRef_65",
    "TypeRef", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.TypeRef__Implementation__),
    "Assembly", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    Relation: FK_TypeRef_TypeRef_TypeRef_66
    A: 3 TypeRef as TypeRef
    B: 3 TypeRef as GenericArguments
    Preferred Storage: 4
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_TypeRef_TypeRef_TypeRef_66",
    "TypeRef", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.TypeRef_GenericArguments66CollectionEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_TypeRef_TypeRef_GenericArguments_66",
    "GenericArguments", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.TypeRef_GenericArguments66CollectionEntry__Implementation__)
    )]

	/*
    Relation: FK_ViewDescriptor_TypeRef_ViewDescriptor_68
    A: 3 ViewDescriptor as ViewDescriptor
    B: 1 TypeRef as LayoutRef
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ViewDescriptor_TypeRef_ViewDescriptor_68",
    "ViewDescriptor", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ViewDescriptor__Implementation__),
    "LayoutRef", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__)
    )]


	/*
    Relation: FK_ViewDescriptor_TypeRef_ViewDescriptor_69
    A: 3 ViewDescriptor as ViewDescriptor
    B: 1 TypeRef as ViewRef
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ViewDescriptor_TypeRef_ViewDescriptor_69",
    "ViewDescriptor", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ViewDescriptor__Implementation__),
    "ViewRef", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__)
    )]


	/*
    Relation: FK_Visual_BaseProperty_Visual_56
    A: 3 Visual as Visual
    B: 1 BaseProperty as Property
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Visual_BaseProperty_Visual_56",
    "Visual", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual__Implementation__),
    "Property", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.BaseProperty__Implementation__)
    )]


	/*
    Relation: FK_Visual_Method_Visual_57
    A: 3 Visual as Visual
    B: 1 Method as Method
    Preferred Storage: 1
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Visual_Method_Visual_57",
    "Visual", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual__Implementation__),
    "Method", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Method__Implementation__)
    )]


	/*
    Relation: FK_Visual_Visual_Visual_55
    A: 3 Visual as Visual
    B: 3 Visual as Children
    Preferred Storage: 4
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Visual_Visual_Visual_55",
    "Visual", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Visual__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual_Children55CollectionEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Visual_Visual_Children_55",
    "Children", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Visual__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual_Children55CollectionEntry__Implementation__)
    )]

	/*
    Relation: FK_Visual_Visual_Visual_60
    A: 3 Visual as Visual
    B: 3 Visual as ContextMenu
    Preferred Storage: 4
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Visual_Visual_Visual_60",
    "Visual", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Visual__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual_ContextMenu60CollectionEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Visual_Visual_ContextMenu_60",
    "ContextMenu", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Visual__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual_ContextMenu60CollectionEntry__Implementation__)
    )]

	/*
    Relation: FK_Zeitkonto_Mitarbeiter_Zeitkonto_42
    A: 3 Zeitkonto as Zeitkonto
    B: 3 Mitarbeiter as Mitarbeiter
    Preferred Storage: 4
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Zeitkonto_Mitarbeiter_Zeitkonto_42",
    "Zeitkonto", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Zeiterfassung.Zeitkonto__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Zeiterfassung.Zeitkonto_Mitarbeiter42CollectionEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Zeitkonto_Mitarbeiter_Mitarbeiter_42",
    "Mitarbeiter", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Mitarbeiter__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Zeiterfassung.Zeitkonto_Mitarbeiter42CollectionEntry__Implementation__)
    )]

	/*
    Relation: FK_Zeitkonto_Taetigkeit_Zeitkonto_33
    A: 2 Zeitkonto as Zeitkonto
    B: 3 Taetigkeit as Taetigkeiten
    Preferred Storage: 2
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Zeitkonto_Taetigkeit_Zeitkonto_33",
    "Zeitkonto", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Zeiterfassung.Zeitkonto__Implementation__),
    "Taetigkeiten", RelationshipMultiplicity.Many, typeof(Kistl.App.Zeiterfassung.Taetigkeit__Implementation__)
    )]



// object-value association
[assembly: EdmRelationship(
    "Model", "FK_Kunde_String_EMails",
    "Kunde", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Kunde__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Kunde_EMailsCollectionEntry__Implementation__)
    )]

