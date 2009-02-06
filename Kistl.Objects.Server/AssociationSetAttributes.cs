using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Xml;
using System.Xml.Serialization;

using Kistl.API;
using Kistl.DALProvider.EF;


	/*
    NewRelation: FK_Auftrag_Kunde_Auftrag_14 
    A: ZeroOrMore Auftrag as Auftrag (site: A, no Relation, prop ID=64)
    B: ZeroOrOne Kunde as Kunde (site: B, no Relation, prop ID=64)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Auftrag_Kunde_Auftrag_14",
    "Auftrag", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Auftrag__Implementation__),
    "Kunde", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Kunde__Implementation__)
    )]


	/*
    NewRelation: FK_Auftrag_Mitarbeiter_Auftrag_9 
    A: ZeroOrMore Auftrag as Auftrag (site: A, no Relation, prop ID=49)
    B: ZeroOrOne Mitarbeiter as Mitarbeiter (site: B, no Relation, prop ID=49)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Auftrag_Mitarbeiter_Auftrag_9",
    "Auftrag", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Auftrag__Implementation__),
    "Mitarbeiter", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Mitarbeiter__Implementation__)
    )]


	/*
    NewRelation: FK_BackReferenceProperty_ObjectReferenceProperty_BackReferenceProperty_8 
    A: ZeroOrMore BackReferenceProperty as BackReferenceProperty (site: A, no Relation, prop ID=47)
    B: ZeroOrOne ObjectReferenceProperty as ReferenceProperty (site: B, no Relation, prop ID=47)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_BackReferenceProperty_ObjectReferenceProperty_BackReferenceProperty_8",
    "BackReferenceProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.BackReferenceProperty__Implementation__),
    "ReferenceProperty", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectReferenceProperty__Implementation__)
    )]


	/*
    NewRelation: FK_BaseProperty_Constraint_ConstrainedProperty_42 
    A: One BaseProperty as ConstrainedProperty (site: A, from relation ID = 16)
    B: ZeroOrMore Constraint as Constraints (site: B, from relation ID = 16)
    Preferred Storage: MergeB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_BaseProperty_Constraint_ConstrainedProperty_42",
    "ConstrainedProperty", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.BaseProperty__Implementation__),
    "Constraints", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Constraint__Implementation__)
    )]


	/*
    NewRelation: FK_BaseProperty_Module_BaseProperty_17 
    A: ZeroOrMore BaseProperty as BaseProperty (site: A, no Relation, prop ID=72)
    B: ZeroOrOne Module as Module (site: B, no Relation, prop ID=72)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_BaseProperty_Module_BaseProperty_17",
    "BaseProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.BaseProperty__Implementation__),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__)
    )]


	/*
    NewRelation: FK_CLRObjectParameter_Assembly_CLRObjectParameter_26 
    A: ZeroOrMore CLRObjectParameter as CLRObjectParameter (site: A, no Relation, prop ID=98)
    B: ZeroOrOne Assembly as Assembly (site: B, no Relation, prop ID=98)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_CLRObjectParameter_Assembly_CLRObjectParameter_26",
    "CLRObjectParameter", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.CLRObjectParameter__Implementation__),
    "Assembly", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    NewRelation: FK_ControlInfo_Assembly_ControlInfo_31 
    A: ZeroOrMore ControlInfo as ControlInfo (site: A, no Relation, prop ID=114)
    B: ZeroOrOne Assembly as Assembly (site: B, no Relation, prop ID=114)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ControlInfo_Assembly_ControlInfo_31",
    "ControlInfo", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ControlInfo__Implementation__),
    "Assembly", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    NewRelation: FK_DataType_BaseProperty_ObjectClass_1 
    A: One DataType as ObjectClass (site: A, from relation ID = 1)
    B: ZeroOrMore BaseProperty as Properties (site: B, from relation ID = 1)
    Preferred Storage: MergeB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_DataType_BaseProperty_ObjectClass_1",
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DataType__Implementation__),
    "Properties", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.BaseProperty__Implementation__)
    )]


	/*
    NewRelation: FK_DataType_Icon_DataType_15 
    A: ZeroOrMore DataType as DataType (site: A, no Relation, prop ID=69)
    B: ZeroOrOne Icon as DefaultIcon (site: B, no Relation, prop ID=69)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_DataType_Icon_DataType_15",
    "DataType", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.DataType__Implementation__),
    "DefaultIcon", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Icon__Implementation__)
    )]


	/*
    NewRelation: FK_DataType_Method_ObjectClass_5 
    A: One DataType as ObjectClass (site: A, from relation ID = 5)
    B: ZeroOrMore Method as Methods (site: B, from relation ID = 5)
    Preferred Storage: MergeB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_DataType_Method_ObjectClass_5",
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DataType__Implementation__),
    "Methods", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Method__Implementation__)
    )]


	/*
    NewRelation: FK_DataType_MethodInvocation_InvokeOnObjectClass_21 
    A: One DataType as InvokeOnObjectClass (site: A, from relation ID = 11)
    B: ZeroOrMore MethodInvocation as MethodInvocations (site: B, from relation ID = 11)
    Preferred Storage: MergeB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_DataType_MethodInvocation_InvokeOnObjectClass_21",
    "InvokeOnObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DataType__Implementation__),
    "MethodInvocations", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation__Implementation__)
    )]


	/*
    NewRelation: FK_Enumeration_EnumerationEntry_Enumeration_27 
    A: One Enumeration as Enumeration (site: A, from relation ID = 15)
    B: ZeroOrMore EnumerationEntry as EnumerationEntries (site: B, from relation ID = 15)
    Preferred Storage: MergeB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Enumeration_EnumerationEntry_Enumeration_27",
    "Enumeration", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Enumeration__Implementation__),
    "EnumerationEntries", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.EnumerationEntry__Implementation__)
    )]


	/*
    NewRelation: FK_EnumerationProperty_Enumeration_EnumerationProperty_28 
    A: ZeroOrMore EnumerationProperty as EnumerationProperty (site: A, no Relation, prop ID=104)
    B: ZeroOrOne Enumeration as Enumeration (site: B, no Relation, prop ID=104)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_EnumerationProperty_Enumeration_EnumerationProperty_28",
    "EnumerationProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.EnumerationProperty__Implementation__),
    "Enumeration", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Enumeration__Implementation__)
    )]


	/*
    NewRelation: FK_Method_BaseParameter_Method_24 
    A: One Method as Method (site: A, from relation ID = 14)
    B: ZeroOrMore BaseParameter as Parameter (site: B, from relation ID = 14)
    Preferred Storage: MergeB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Method_BaseParameter_Method_24",
    "Method", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Method__Implementation__),
    "Parameter", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.BaseParameter__Implementation__)
    )]


	/*
    NewRelation: FK_Method_MethodInvocation_Method_19 
    A: One Method as Method (site: A, from relation ID = 12)
    B: ZeroOrMore MethodInvocation as MethodInvokations (site: B, from relation ID = 12)
    Preferred Storage: MergeB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Method_MethodInvocation_Method_19",
    "Method", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Method__Implementation__),
    "MethodInvokations", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation__Implementation__)
    )]


	/*
    NewRelation: FK_Method_Module_Method_18 
    A: ZeroOrMore Method as Method (site: A, no Relation, prop ID=73)
    B: ZeroOrOne Module as Module (site: B, no Relation, prop ID=73)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Method_Module_Method_18",
    "Method", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Method__Implementation__),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__)
    )]


	/*
    NewRelation: FK_MethodInvocation_Module_MethodInvocation_20 
    A: ZeroOrMore MethodInvocation as MethodInvocation (site: A, no Relation, prop ID=78)
    B: ZeroOrOne Module as Module (site: B, no Relation, prop ID=78)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_MethodInvocation_Module_MethodInvocation_20",
    "MethodInvocation", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation__Implementation__),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__)
    )]


	/*
    NewRelation: FK_MethodInvocation_TypeRef_MethodInvocation_47 
    A: ZeroOrMore MethodInvocation as MethodInvocation (site: A, no Relation, prop ID=208)
    B: ZeroOrOne TypeRef as Implementor (site: B, no Relation, prop ID=208)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_MethodInvocation_TypeRef_MethodInvocation_47",
    "MethodInvocation", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation__Implementation__),
    "Implementor", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__)
    )]


	/*
    NewRelation: FK_Module_Assembly_Module_16 
    A: One Module as Module (site: A, from relation ID = 13)
    B: ZeroOrMore Assembly as Assemblies (site: B, from relation ID = 13)
    Preferred Storage: MergeB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Module_Assembly_Module_16",
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__),
    "Assemblies", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    NewRelation: FK_Module_DataType_Module_6 
    A: One Module as Module (site: A, from relation ID = 6)
    B: ZeroOrMore DataType as DataTypes (site: B, from relation ID = 6)
    Preferred Storage: MergeB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Module_DataType_Module_6",
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__),
    "DataTypes", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.DataType__Implementation__)
    )]


	/*
    NewRelation: FK_ObjectClass_Interface_ObjectClass_29 
    A: ZeroOrMore ObjectClass as ObjectClass (site: A, no Relation, prop ID=105)
    B: ZeroOrMore Interface as ImplementsInterfaces (site: B, no Relation, prop ID=105)
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_ObjectClass_Interface_ObjectClass_29",
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClass__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass_ImplementsInterfaces29CollectionEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_ObjectClass_Interface_ImplementsInterfaces_29",
    "ImplementsInterfaces", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Interface__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass_ImplementsInterfaces29CollectionEntry__Implementation__)
    )]

	/*
    NewRelation: FK_ObjectClass_ObjectClass_BaseObjectClass_4 
    A: ZeroOrOne ObjectClass as BaseObjectClass (site: A, from relation ID = 4)
    B: ZeroOrMore ObjectClass as SubClasses (site: B, from relation ID = 4)
    Preferred Storage: MergeB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ObjectClass_ObjectClass_BaseObjectClass_4",
    "BaseObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClass__Implementation__),
    "SubClasses", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass__Implementation__)
    )]


	/*
    NewRelation: FK_ObjectClass_TypeRef_ObjectClass_50 
    A: ZeroOrMore ObjectClass as ObjectClass (site: A, no Relation, prop ID=212)
    B: ZeroOrOne TypeRef as DefaultModel (site: B, no Relation, prop ID=212)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ObjectClass_TypeRef_ObjectClass_50",
    "ObjectClass", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass__Implementation__),
    "DefaultModel", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__)
    )]


	/*
    NewRelation: FK_ObjectParameter_DataType_ObjectParameter_25 
    A: ZeroOrMore ObjectParameter as ObjectParameter (site: A, no Relation, prop ID=97)
    B: ZeroOrOne DataType as DataType (site: B, no Relation, prop ID=97)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ObjectParameter_DataType_ObjectParameter_25",
    "ObjectParameter", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectParameter__Implementation__),
    "DataType", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DataType__Implementation__)
    )]


	/*
    NewRelation: FK_ObjectReferenceProperty_ObjectClass_ObjectReferenceProperty_7 
    A: ZeroOrMore ObjectReferenceProperty as ObjectReferenceProperty (site: A, no Relation, prop ID=46)
    B: ZeroOrOne ObjectClass as ReferenceObjectClass (site: B, no Relation, prop ID=46)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ObjectReferenceProperty_ObjectClass_ObjectReferenceProperty_7",
    "ObjectReferenceProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectReferenceProperty__Implementation__),
    "ReferenceObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClass__Implementation__)
    )]


	/*
    NewRelation: FK_PresenterInfo_Assembly_PresenterInfo_33 
    A: ZeroOrMore PresenterInfo as PresenterInfo (site: A, no Relation, prop ID=138)
    B: ZeroOrOne Assembly as PresenterAssembly (site: B, no Relation, prop ID=138)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_PresenterInfo_Assembly_PresenterInfo_33",
    "PresenterInfo", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.PresenterInfo__Implementation__),
    "PresenterAssembly", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    NewRelation: FK_PresenterInfo_Assembly_PresenterInfo_34 
    A: ZeroOrMore PresenterInfo as PresenterInfo (site: A, no Relation, prop ID=147)
    B: ZeroOrOne Assembly as DataAssembly (site: B, no Relation, prop ID=147)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_PresenterInfo_Assembly_PresenterInfo_34",
    "PresenterInfo", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.PresenterInfo__Implementation__),
    "DataAssembly", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    NewRelation: FK_Projekt_Auftrag_Projekt_10 
    A: ZeroOrOne Projekt as Projekt (site: A, from relation ID = 10)
    B: ZeroOrMore Auftrag as Auftraege (site: B, from relation ID = 10)
    Preferred Storage: MergeB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Projekt_Auftrag_Projekt_10",
    "Projekt", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Projekt__Implementation__),
    "Auftraege", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Auftrag__Implementation__)
    )]


	/*
    NewRelation: FK_Projekt_Kostentraeger_Projekt_11 
    A: One Projekt as Projekt (site: A, from relation ID = 9)
    B: ZeroOrMore Kostentraeger as Kostentraeger (site: B, from relation ID = 9)
    Preferred Storage: MergeB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Projekt_Kostentraeger_Projekt_11",
    "Projekt", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Projekt__Implementation__),
    "Kostentraeger", RelationshipMultiplicity.Many, typeof(Kistl.App.Zeiterfassung.Kostentraeger__Implementation__)
    )]


	/*
    NewRelation: FK_Projekt_Mitarbeiter_Projekte_3 
    A: ZeroOrMore Projekt as Projekte (site: A, from relation ID = 3)
    B: ZeroOrMore Mitarbeiter as Mitarbeiter (site: B, from relation ID = 3)
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Projekt_Mitarbeiter_Projekte_3",
    "Projekte", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Projekt__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Projekt_Mitarbeiter3CollectionEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Projekt_Mitarbeiter_Mitarbeiter_3",
    "Mitarbeiter", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Mitarbeiter__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Projekt_Mitarbeiter3CollectionEntry__Implementation__)
    )]

	/*
    NewRelation: FK_Projekt_Task_Projekt_2 
    A: ZeroOrOne Projekt as Projekt (site: A, from relation ID = 2)
    B: ZeroOrMore Task as Tasks (site: B, from relation ID = 2)
    Preferred Storage: MergeB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Projekt_Task_Projekt_2",
    "Projekt", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Projekt__Implementation__),
    "Tasks", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Task__Implementation__)
    )]


	/*
    NewRelation: FK_Relation_ObjectReferenceProperty_LeftOf_43 
    A: ZeroOrOne Relation as LeftOf (site: A, from relation ID = 17)
    B: One ObjectReferenceProperty as LeftPart (site: B, from relation ID = 17)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Relation_ObjectReferenceProperty_LeftOf_43",
    "LeftOf", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Relation__Implementation__),
    "LeftPart", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectReferenceProperty__Implementation__)
    )]


	/*
    NewRelation: FK_Relation_ObjectReferenceProperty_RightOf_44 
    A: ZeroOrOne Relation as RightOf (site: A, from relation ID = 18)
    B: One ObjectReferenceProperty as RightPart (site: B, from relation ID = 18)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Relation_ObjectReferenceProperty_RightOf_44",
    "RightOf", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Relation__Implementation__),
    "RightPart", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectReferenceProperty__Implementation__)
    )]


	/*
    NewRelation: FK_StructProperty_Struct_StructProperty_32 
    A: ZeroOrMore StructProperty as StructProperty (site: A, no Relation, prop ID=129)
    B: ZeroOrOne Struct as StructDefinition (site: B, no Relation, prop ID=129)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_StructProperty_Struct_StructProperty_32",
    "StructProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.StructProperty__Implementation__),
    "StructDefinition", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Struct__Implementation__)
    )]


	/*
    NewRelation: FK_Taetigkeit_Mitarbeiter_Taetigkeit_12 
    A: ZeroOrMore Taetigkeit as Taetigkeit (site: A, no Relation, prop ID=54)
    B: ZeroOrOne Mitarbeiter as Mitarbeiter (site: B, no Relation, prop ID=54)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Taetigkeit_Mitarbeiter_Taetigkeit_12",
    "Taetigkeit", RelationshipMultiplicity.Many, typeof(Kistl.App.Zeiterfassung.Taetigkeit__Implementation__),
    "Mitarbeiter", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Mitarbeiter__Implementation__)
    )]


	/*
    NewRelation: FK_Taetigkeit_TaetigkeitsArt_Taetigkeit_23 
    A: ZeroOrMore Taetigkeit as Taetigkeit (site: A, no Relation, prop ID=88)
    B: ZeroOrOne TaetigkeitsArt as TaetigkeitsArt (site: B, no Relation, prop ID=88)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Taetigkeit_TaetigkeitsArt_Taetigkeit_23",
    "Taetigkeit", RelationshipMultiplicity.Many, typeof(Kistl.App.Zeiterfassung.Taetigkeit__Implementation__),
    "TaetigkeitsArt", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Zeiterfassung.TaetigkeitsArt__Implementation__)
    )]


	/*
    NewRelation: FK_Template_Assembly_Template_39 
    A: ZeroOrMore Template as Template (site: A, no Relation, prop ID=163)
    B: ZeroOrOne Assembly as DisplayedTypeAssembly (site: B, no Relation, prop ID=163)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Template_Assembly_Template_39",
    "Template", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Template__Implementation__),
    "DisplayedTypeAssembly", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    NewRelation: FK_Template_Visual_Template_38 
    A: ZeroOrMore Template as Template (site: A, no Relation, prop ID=155)
    B: ZeroOrOne Visual as VisualTree (site: B, no Relation, prop ID=155)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Template_Visual_Template_38",
    "Template", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Template__Implementation__),
    "VisualTree", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Visual__Implementation__)
    )]


	/*
    NewRelation: FK_Template_Visual_Template_41 
    A: ZeroOrMore Template as Template (site: A, no Relation, prop ID=165)
    B: ZeroOrMore Visual as Menu (site: B, no Relation, prop ID=165)
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Template_Visual_Template_41",
    "Template", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Template__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Template_Menu41CollectionEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Template_Visual_Menu_41",
    "Menu", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Visual__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Template_Menu41CollectionEntry__Implementation__)
    )]

	/*
    NewRelation: FK_TestObjClass_Kunde_TestObjClass_30 
    A: ZeroOrMore TestObjClass as TestObjClass (site: A, no Relation, prop ID=112)
    B: ZeroOrOne Kunde as ObjectProp (site: B, no Relation, prop ID=112)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_TestObjClass_Kunde_TestObjClass_30",
    "TestObjClass", RelationshipMultiplicity.Many, typeof(Kistl.App.Test.TestObjClass__Implementation__),
    "ObjectProp", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Kunde__Implementation__)
    )]


	/*
    NewRelation: FK_TypeRef_Assembly_TypeRef_45 
    A: ZeroOrMore TypeRef as TypeRef (site: A, no Relation, prop ID=206)
    B: ZeroOrOne Assembly as Assembly (site: B, no Relation, prop ID=206)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_TypeRef_Assembly_TypeRef_45",
    "TypeRef", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.TypeRef__Implementation__),
    "Assembly", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    NewRelation: FK_TypeRef_TypeRef_TypeRef_46 
    A: ZeroOrMore TypeRef as TypeRef (site: A, no Relation, prop ID=207)
    B: ZeroOrMore TypeRef as GenericArguments (site: B, no Relation, prop ID=207)
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_TypeRef_TypeRef_TypeRef_46",
    "TypeRef", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.TypeRef_GenericArguments46CollectionEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_TypeRef_TypeRef_GenericArguments_46",
    "GenericArguments", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.TypeRef_GenericArguments46CollectionEntry__Implementation__)
    )]

	/*
    NewRelation: FK_ViewDescriptor_TypeRef_ViewDescriptor_48 
    A: ZeroOrMore ViewDescriptor as ViewDescriptor (site: A, no Relation, prop ID=209)
    B: ZeroOrOne TypeRef as LayoutRef (site: B, no Relation, prop ID=209)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ViewDescriptor_TypeRef_ViewDescriptor_48",
    "ViewDescriptor", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ViewDescriptor__Implementation__),
    "LayoutRef", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__)
    )]


	/*
    NewRelation: FK_ViewDescriptor_TypeRef_ViewDescriptor_49 
    A: ZeroOrMore ViewDescriptor as ViewDescriptor (site: A, no Relation, prop ID=211)
    B: ZeroOrOne TypeRef as ViewRef (site: B, no Relation, prop ID=211)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ViewDescriptor_TypeRef_ViewDescriptor_49",
    "ViewDescriptor", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ViewDescriptor__Implementation__),
    "ViewRef", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__)
    )]


	/*
    NewRelation: FK_Visual_BaseProperty_Visual_36 
    A: ZeroOrMore Visual as Visual (site: A, no Relation, prop ID=152)
    B: ZeroOrOne BaseProperty as Property (site: B, no Relation, prop ID=152)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Visual_BaseProperty_Visual_36",
    "Visual", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual__Implementation__),
    "Property", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.BaseProperty__Implementation__)
    )]


	/*
    NewRelation: FK_Visual_Method_Visual_37 
    A: ZeroOrMore Visual as Visual (site: A, no Relation, prop ID=153)
    B: ZeroOrOne Method as Method (site: B, no Relation, prop ID=153)
    Preferred Storage: MergeA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Visual_Method_Visual_37",
    "Visual", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual__Implementation__),
    "Method", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Method__Implementation__)
    )]


	/*
    NewRelation: FK_Visual_Visual_Visual_35 
    A: ZeroOrMore Visual as Visual (site: A, no Relation, prop ID=151)
    B: ZeroOrMore Visual as Children (site: B, no Relation, prop ID=151)
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Visual_Visual_Visual_35",
    "Visual", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Visual__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual_Children35CollectionEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Visual_Visual_Children_35",
    "Children", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Visual__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual_Children35CollectionEntry__Implementation__)
    )]

	/*
    NewRelation: FK_Visual_Visual_Visual_40 
    A: ZeroOrMore Visual as Visual (site: A, no Relation, prop ID=164)
    B: ZeroOrMore Visual as ContextMenu (site: B, no Relation, prop ID=164)
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Visual_Visual_Visual_40",
    "Visual", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Visual__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual_ContextMenu40CollectionEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Visual_Visual_ContextMenu_40",
    "ContextMenu", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Visual__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual_ContextMenu40CollectionEntry__Implementation__)
    )]

	/*
    NewRelation: FK_Zeitkonto_Mitarbeiter_Zeitkonto_22 
    A: ZeroOrMore Zeitkonto as Zeitkonto (site: A, no Relation, prop ID=86)
    B: ZeroOrMore Mitarbeiter as Mitarbeiter (site: B, no Relation, prop ID=86)
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Zeitkonto_Mitarbeiter_Zeitkonto_22",
    "Zeitkonto", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Zeiterfassung.Zeitkonto__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Zeiterfassung.Zeitkonto_Mitarbeiter22CollectionEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Zeitkonto_Mitarbeiter_Mitarbeiter_22",
    "Mitarbeiter", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Mitarbeiter__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Zeiterfassung.Zeitkonto_Mitarbeiter22CollectionEntry__Implementation__)
    )]

	/*
    NewRelation: FK_Zeitkonto_Taetigkeit_Zeitkonto_13 
    A: One Zeitkonto as Zeitkonto (site: A, from relation ID = 8)
    B: ZeroOrMore Taetigkeit as Taetigkeiten (site: B, from relation ID = 8)
    Preferred Storage: MergeB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Zeitkonto_Taetigkeit_Zeitkonto_13",
    "Zeitkonto", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Zeiterfassung.Zeitkonto__Implementation__),
    "Taetigkeiten", RelationshipMultiplicity.Many, typeof(Kistl.App.Zeiterfassung.Taetigkeit__Implementation__)
    )]



// object-value association
[assembly: EdmRelationship(
    "Model", "FK_Kunde_String_EMails",
    "Kunde", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Kunde__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Kunde_EMailsCollectionEntry__Implementation__)
    )]

