using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Xml;
using System.Xml.Serialization;

using Kistl.API;
using Kistl.DALProvider.EF;


	/*
    Relation: FK_ArbeitszeitEintrag_Mitarbeiter_Arbeitszeit_81
    A: ZeroOrMore ArbeitszeitEintrag as Arbeitszeit
    B: One Mitarbeiter as Mitarbeiter
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ArbeitszeitEintrag_Mitarbeiter_Arbeitszeit_81",
    "Arbeitszeit", RelationshipMultiplicity.Many, typeof(Kistl.App.Zeiterfassung.ArbeitszeitEintrag__Implementation__),
    "Mitarbeiter", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Mitarbeiter__Implementation__)
    )]


	/*
    Relation: FK_Auftrag_Kunde_Auftrag_34
    A: ZeroOrMore Auftrag as Auftrag
    B: ZeroOrOne Kunde as Kunde
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Auftrag_Kunde_Auftrag_34",
    "Auftrag", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Auftrag__Implementation__),
    "Kunde", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Kunde__Implementation__)
    )]


	/*
    Relation: FK_Auftrag_Mitarbeiter_Auftrag_29
    A: ZeroOrMore Auftrag as Auftrag
    B: ZeroOrOne Mitarbeiter as Mitarbeiter
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Auftrag_Mitarbeiter_Auftrag_29",
    "Auftrag", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Auftrag__Implementation__),
    "Mitarbeiter", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Mitarbeiter__Implementation__)
    )]


	/*
    Relation: FK_CLRObjectParameter_Assembly_CLRObjectParameter_46
    A: ZeroOrMore CLRObjectParameter as CLRObjectParameter
    B: ZeroOrOne Assembly as Assembly
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_CLRObjectParameter_Assembly_CLRObjectParameter_46",
    "CLRObjectParameter", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.CLRObjectParameter__Implementation__),
    "Assembly", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    Relation: FK_ControlInfo_Assembly_ControlInfo_51
    A: ZeroOrMore ControlInfo as ControlInfo
    B: ZeroOrOne Assembly as Assembly
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ControlInfo_Assembly_ControlInfo_51",
    "ControlInfo", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ControlInfo__Implementation__),
    "Assembly", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    Relation: FK_DataType_Icon_DataType_35
    A: ZeroOrMore DataType as DataType
    B: ZeroOrOne Icon as DefaultIcon
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_DataType_Icon_DataType_35",
    "DataType", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.DataType__Implementation__),
    "DefaultIcon", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Icon__Implementation__)
    )]


	/*
    Relation: FK_DataType_Method_ObjectClass_25
    A: One DataType as ObjectClass
    B: ZeroOrMore Method as Methods
    Preferred Storage: Right
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_DataType_Method_ObjectClass_25",
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DataType__Implementation__),
    "Methods", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Method__Implementation__)
    )]


	/*
    Relation: FK_DataType_MethodInvocation_InvokeOnObjectClass_41
    A: One DataType as InvokeOnObjectClass
    B: ZeroOrMore MethodInvocation as MethodInvocations
    Preferred Storage: Right
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_DataType_MethodInvocation_InvokeOnObjectClass_41",
    "InvokeOnObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DataType__Implementation__),
    "MethodInvocations", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation__Implementation__)
    )]


	/*
    Relation: FK_DataType_Property_ObjectClass_19
    A: One DataType as ObjectClass
    B: ZeroOrMore Property as Properties
    Preferred Storage: Right
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_DataType_Property_ObjectClass_19",
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DataType__Implementation__),
    "Properties", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Property__Implementation__)
    )]


	/*
    Relation: FK_Enumeration_EnumerationEntry_Enumeration_47
    A: One Enumeration as Enumeration
    B: ZeroOrMore EnumerationEntry as EnumerationEntries
    Preferred Storage: Right
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Enumeration_EnumerationEntry_Enumeration_47",
    "Enumeration", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Enumeration__Implementation__),
    "EnumerationEntries", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.EnumerationEntry__Implementation__)
    )]


	/*
    Relation: FK_EnumerationProperty_Enumeration_EnumerationProperty_48
    A: ZeroOrMore EnumerationProperty as EnumerationProperty
    B: ZeroOrOne Enumeration as Enumeration
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_EnumerationProperty_Enumeration_EnumerationProperty_48",
    "EnumerationProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.EnumerationProperty__Implementation__),
    "Enumeration", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Enumeration__Implementation__)
    )]


	/*
    Relation: FK_Method_BaseParameter_Method_44
    A: One Method as Method
    B: ZeroOrMore BaseParameter as Parameter
    Preferred Storage: Right
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Method_BaseParameter_Method_44",
    "Method", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Method__Implementation__),
    "Parameter", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.BaseParameter__Implementation__)
    )]


	/*
    Relation: FK_Method_MethodInvocation_Method_39
    A: One Method as Method
    B: ZeroOrMore MethodInvocation as MethodInvokations
    Preferred Storage: Right
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Method_MethodInvocation_Method_39",
    "Method", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Method__Implementation__),
    "MethodInvokations", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation__Implementation__)
    )]


	/*
    Relation: FK_Method_Module_Method_38
    A: ZeroOrMore Method as Method
    B: ZeroOrOne Module as Module
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Method_Module_Method_38",
    "Method", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Method__Implementation__),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__)
    )]


	/*
    Relation: FK_MethodInvocation_Module_MethodInvocation_40
    A: ZeroOrMore MethodInvocation as MethodInvocation
    B: ZeroOrOne Module as Module
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_MethodInvocation_Module_MethodInvocation_40",
    "MethodInvocation", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation__Implementation__),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__)
    )]


	/*
    Relation: FK_MethodInvocation_TypeRef_MethodInvocation_67
    A: ZeroOrMore MethodInvocation as MethodInvocation
    B: ZeroOrOne TypeRef as Implementor
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_MethodInvocation_TypeRef_MethodInvocation_67",
    "MethodInvocation", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation__Implementation__),
    "Implementor", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__)
    )]


	/*
    Relation: FK_Module_Assembly_Module_36
    A: One Module as Module
    B: ZeroOrMore Assembly as Assemblies
    Preferred Storage: Right
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Module_Assembly_Module_36",
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__),
    "Assemblies", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    Relation: FK_Module_DataType_Module_26
    A: One Module as Module
    B: ZeroOrMore DataType as DataTypes
    Preferred Storage: Right
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Module_DataType_Module_26",
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__),
    "DataTypes", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.DataType__Implementation__)
    )]


	/*
    Relation: FK_ObjectClass_Interface_ObjectClass_49
    A: ZeroOrMore ObjectClass as ObjectClass
    B: ZeroOrMore Interface as ImplementsInterfaces
    Preferred Storage: Separate
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
    A: ZeroOrOne ObjectClass as BaseObjectClass
    B: ZeroOrMore ObjectClass as SubClasses
    Preferred Storage: Right
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ObjectClass_ObjectClass_BaseObjectClass_24",
    "BaseObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClass__Implementation__),
    "SubClasses", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass__Implementation__)
    )]


	/*
    Relation: FK_ObjectClass_PresentableModelDescriptor_Presentable_78
    A: ZeroOrMore ObjectClass as Presentable
    B: One PresentableModelDescriptor as DefaultPresentableModelDescriptor
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ObjectClass_PresentableModelDescriptor_Presentable_78",
    "Presentable", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass__Implementation__),
    "DefaultPresentableModelDescriptor", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.PresentableModelDescriptor__Implementation__)
    )]


	/*
    Relation: FK_ObjectClass_TypeRef_ObjectClass_70
    A: ZeroOrMore ObjectClass as ObjectClass
    B: ZeroOrOne TypeRef as DefaultModel
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ObjectClass_TypeRef_ObjectClass_70",
    "ObjectClass", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass__Implementation__),
    "DefaultModel", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__)
    )]


	/*
    Relation: FK_ObjectParameter_DataType_ObjectParameter_45
    A: ZeroOrMore ObjectParameter as ObjectParameter
    B: ZeroOrOne DataType as DataType
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ObjectParameter_DataType_ObjectParameter_45",
    "ObjectParameter", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectParameter__Implementation__),
    "DataType", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DataType__Implementation__)
    )]


	/*
    Relation: FK_ObjectReferenceProperty_ObjectClass_ObjectReferenceProperty_27
    A: ZeroOrMore ObjectReferenceProperty as ObjectReferenceProperty
    B: ZeroOrOne ObjectClass as ReferenceObjectClass
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ObjectReferenceProperty_ObjectClass_ObjectReferenceProperty_27",
    "ObjectReferenceProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectReferenceProperty__Implementation__),
    "ReferenceObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClass__Implementation__)
    )]


	/*
    Relation: FK_PresentableModelDescriptor_TypeRef_Descriptor_77
    A: ZeroOrMore PresentableModelDescriptor as Descriptor
    B: One TypeRef as PresentableModelRef
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_PresentableModelDescriptor_TypeRef_Descriptor_77",
    "Descriptor", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.PresentableModelDescriptor__Implementation__),
    "PresentableModelRef", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__)
    )]


	/*
    Relation: FK_PresenterInfo_Assembly_PresenterInfo_53
    A: ZeroOrMore PresenterInfo as PresenterInfo
    B: ZeroOrOne Assembly as PresenterAssembly
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_PresenterInfo_Assembly_PresenterInfo_53",
    "PresenterInfo", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.PresenterInfo__Implementation__),
    "PresenterAssembly", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    Relation: FK_PresenterInfo_Assembly_PresenterInfo_54
    A: ZeroOrMore PresenterInfo as PresenterInfo
    B: ZeroOrOne Assembly as DataAssembly
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_PresenterInfo_Assembly_PresenterInfo_54",
    "PresenterInfo", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.PresenterInfo__Implementation__),
    "DataAssembly", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    Relation: FK_Projekt_Auftrag_Projekt_30
    A: ZeroOrOne Projekt as Projekt
    B: ZeroOrMore Auftrag as Auftraege
    Preferred Storage: Right
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Projekt_Auftrag_Projekt_30",
    "Projekt", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Projekt__Implementation__),
    "Auftraege", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Auftrag__Implementation__)
    )]


	/*
    Relation: FK_Projekt_Mitarbeiter_Projekte_23
    A: ZeroOrMore Projekt as Projekte
    B: ZeroOrMore Mitarbeiter as Mitarbeiter
    Preferred Storage: Separate
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
    A: ZeroOrOne Projekt as Projekt
    B: ZeroOrMore Task as Tasks
    Preferred Storage: Right
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Projekt_Task_Projekt_22",
    "Projekt", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Projekt__Implementation__),
    "Tasks", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Task__Implementation__)
    )]


	/*
    Relation: FK_Property_Constraint_ConstrainedProperty_62
    A: One Property as ConstrainedProperty
    B: ZeroOrMore Constraint as Constraints
    Preferred Storage: Right
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Property_Constraint_ConstrainedProperty_62",
    "ConstrainedProperty", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Property__Implementation__),
    "Constraints", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Constraint__Implementation__)
    )]


	/*
    Relation: FK_Property_Module_BaseProperty_37
    A: ZeroOrMore Property as BaseProperty
    B: ZeroOrOne Module as Module
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Property_Module_BaseProperty_37",
    "BaseProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Property__Implementation__),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__)
    )]


	/*
    Relation: FK_Property_PresentableModelDescriptor_Property_80
    A: ZeroOrMore Property as Property
    B: One PresentableModelDescriptor as ValueModelDescriptor
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Property_PresentableModelDescriptor_Property_80",
    "Property", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Property__Implementation__),
    "ValueModelDescriptor", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.PresentableModelDescriptor__Implementation__)
    )]


	/*
    Relation: FK_Relation_RelationEnd_Relation_71
    A: ZeroOrOne Relation as Relation
    B: ZeroOrOne RelationEnd as A
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Relation_RelationEnd_Relation_71",
    "Relation", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Relation__Implementation__),
    "A", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.RelationEnd__Implementation__)
    )]


	/*
    Relation: FK_Relation_RelationEnd_Relation_72
    A: ZeroOrOne Relation as Relation
    B: ZeroOrOne RelationEnd as B
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Relation_RelationEnd_Relation_72",
    "Relation", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Relation__Implementation__),
    "B", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.RelationEnd__Implementation__)
    )]


	/*
    Relation: FK_RelationEnd_ObjectClass_RelationEnd_73
    A: ZeroOrMore RelationEnd as RelationEnd
    B: ZeroOrOne ObjectClass as Type
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_RelationEnd_ObjectClass_RelationEnd_73",
    "RelationEnd", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.RelationEnd__Implementation__),
    "Type", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClass__Implementation__)
    )]


	/*
    Relation: FK_RelationEnd_ObjectReferenceProperty_RelationEnd_74
    A: ZeroOrOne RelationEnd as RelationEnd
    B: ZeroOrOne ObjectReferenceProperty as Navigator
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_RelationEnd_ObjectReferenceProperty_RelationEnd_74",
    "RelationEnd", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.RelationEnd__Implementation__),
    "Navigator", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectReferenceProperty__Implementation__)
    )]


	/*
    Relation: FK_StructProperty_Struct_StructProperty_52
    A: ZeroOrMore StructProperty as StructProperty
    B: ZeroOrOne Struct as StructDefinition
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_StructProperty_Struct_StructProperty_52",
    "StructProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.StructProperty__Implementation__),
    "StructDefinition", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Struct__Implementation__)
    )]


	/*
    Relation: FK_Template_Assembly_Template_59
    A: ZeroOrMore Template as Template
    B: ZeroOrOne Assembly as DisplayedTypeAssembly
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Template_Assembly_Template_59",
    "Template", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Template__Implementation__),
    "DisplayedTypeAssembly", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    Relation: FK_Template_Visual_Template_58
    A: ZeroOrMore Template as Template
    B: ZeroOrOne Visual as VisualTree
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Template_Visual_Template_58",
    "Template", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Template__Implementation__),
    "VisualTree", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Visual__Implementation__)
    )]


	/*
    Relation: FK_Template_Visual_Template_61
    A: ZeroOrMore Template as Template
    B: ZeroOrMore Visual as Menu
    Preferred Storage: Separate
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
    A: ZeroOrMore TestObjClass as TestObjClass
    B: ZeroOrOne Kunde as ObjectProp
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_TestObjClass_Kunde_TestObjClass_50",
    "TestObjClass", RelationshipMultiplicity.Many, typeof(Kistl.App.Test.TestObjClass__Implementation__),
    "ObjectProp", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Kunde__Implementation__)
    )]


	/*
    Relation: FK_TypeRef_Assembly_TypeRef_65
    A: ZeroOrMore TypeRef as TypeRef
    B: ZeroOrOne Assembly as Assembly
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_TypeRef_Assembly_TypeRef_65",
    "TypeRef", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.TypeRef__Implementation__),
    "Assembly", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    Relation: FK_TypeRef_TypeRef_Child_79
    A: ZeroOrMore TypeRef as Child
    B: One TypeRef as Parent
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_TypeRef_TypeRef_Child_79",
    "Child", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.TypeRef__Implementation__),
    "Parent", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__)
    )]


	/*
    Relation: FK_TypeRef_TypeRef_TypeRef_66
    A: ZeroOrMore TypeRef as TypeRef
    B: ZeroOrMore TypeRef as GenericArguments
    Preferred Storage: Separate
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
    Relation: FK_ViewDescriptor_PresentableModelDescriptor_View_75
    A: ZeroOrMore ViewDescriptor as View
    B: One PresentableModelDescriptor as PresentedModelDescriptor
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ViewDescriptor_PresentableModelDescriptor_View_75",
    "View", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ViewDescriptor__Implementation__),
    "PresentedModelDescriptor", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.PresentableModelDescriptor__Implementation__)
    )]


	/*
    Relation: FK_ViewDescriptor_TypeRef_View_76
    A: ZeroOrMore ViewDescriptor as View
    B: One TypeRef as ControlRef
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ViewDescriptor_TypeRef_View_76",
    "View", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ViewDescriptor__Implementation__),
    "ControlRef", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__)
    )]


	/*
    Relation: FK_Visual_Method_Visual_57
    A: ZeroOrMore Visual as Visual
    B: ZeroOrOne Method as Method
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Visual_Method_Visual_57",
    "Visual", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual__Implementation__),
    "Method", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Method__Implementation__)
    )]


	/*
    Relation: FK_Visual_Property_Visual_56
    A: ZeroOrMore Visual as Visual
    B: ZeroOrOne Property as Property
    Preferred Storage: Left
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Visual_Property_Visual_56",
    "Visual", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual__Implementation__),
    "Property", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Property__Implementation__)
    )]


	/*
    Relation: FK_Visual_Visual_Visual_55
    A: ZeroOrMore Visual as Visual
    B: ZeroOrMore Visual as Children
    Preferred Storage: Separate
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
    A: ZeroOrMore Visual as Visual
    B: ZeroOrMore Visual as ContextMenu
    Preferred Storage: Separate
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
    A: ZeroOrMore Zeitkonto as Zeitkonto
    B: ZeroOrMore Mitarbeiter as Mitarbeiter
    Preferred Storage: Separate
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


// object-value association
[assembly: EdmRelationship(
    "Model", "FK_Kunde_String_EMails",
    "Kunde", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Kunde__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Kunde_EMailsCollectionEntry__Implementation__)
    )]

