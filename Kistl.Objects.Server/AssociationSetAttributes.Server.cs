using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Xml;
using System.Xml.Serialization;

using Kistl.API;
using Kistl.DALProvider.EF;


	/*
    Relation: FK_Assembly_was_ChangedBy
    A: ZeroOrMore Assembly as Assembly
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Assembly_was_ChangedBy",
    "Assembly", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Assembly__Implementation__),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_Assembly_was_CreatedBy
    A: ZeroOrMore Assembly as Assembly
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Assembly_was_CreatedBy",
    "Assembly", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Assembly__Implementation__),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_Auftrag_ChangedBy_ChangedBy
    A: ZeroOrMore Auftrag as Auftrag
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Auftrag_ChangedBy_ChangedBy",
    "Auftrag", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Auftrag__Implementation__),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_Auftrag_CreatedBy_CreatedBy
    A: ZeroOrMore Auftrag as Auftrag
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Auftrag_CreatedBy_CreatedBy",
    "Auftrag", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Auftrag__Implementation__),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


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
    Relation: FK_BaseObjectClass_has_SubClasses
    A: ZeroOrOne ObjectClass as BaseObjectClass
    B: ZeroOrMore ObjectClass as SubClasses
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_BaseObjectClass_has_SubClasses",
    "BaseObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClass__Implementation__),
    "SubClasses", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass__Implementation__)
    )]


	/*
    Relation: FK_BaseParameter_was_ChangedBy
    A: ZeroOrMore BaseParameter as BaseParameter
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_BaseParameter_was_ChangedBy",
    "BaseParameter", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.BaseParameter__Implementation__),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_BaseParameter_was_CreatedBy
    A: ZeroOrMore BaseParameter as BaseParameter
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_BaseParameter_was_CreatedBy",
    "BaseParameter", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.BaseParameter__Implementation__),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_BaseProperty_has_Module
    A: ZeroOrMore Property as BaseProperty
    B: ZeroOrOne Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_BaseProperty_has_Module",
    "BaseProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Property__Implementation__),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__)
    )]


	/*
    Relation: FK_CalculatedReference_dependsOn_InputProperties
    A: ZeroOrMore CalculatedObjectReferenceProperty as CalculatedReference
    B: ZeroOrMore Property as InputProperties
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_CalculatedReference_dependsOn_InputProperties_A",
    "CalculatedReference", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.CalculatedObjectReferenceProperty__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.CalculatedObjectReferenceProperty_dependsOn_Property_RelationEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_CalculatedReference_dependsOn_InputProperties_B",
    "InputProperties", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Property__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.CalculatedObjectReferenceProperty_dependsOn_Property_RelationEntry__Implementation__)
    )]

	/*
    Relation: FK_CalculatedReference_references_ReferencedClass
    A: ZeroOrMore CalculatedObjectReferenceProperty as CalculatedReference
    B: One ObjectClass as ReferencedClass
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_CalculatedReference_references_ReferencedClass",
    "CalculatedReference", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.CalculatedObjectReferenceProperty__Implementation__),
    "ReferencedClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClass__Implementation__)
    )]


	/*
    Relation: FK_Child_has_Parent
    A: ZeroOrMore TypeRef as Child
    B: ZeroOrOne TypeRef as Parent
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Child_has_Parent",
    "Child", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.TypeRef__Implementation__),
    "Parent", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__)
    )]


	/*
    Relation: FK_ClrObjectParameter_isOf_Type
    A: ZeroOrMore CLRObjectParameter as ClrObjectParameter
    B: ZeroOrOne TypeRef as Type
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ClrObjectParameter_isOf_Type",
    "ClrObjectParameter", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.CLRObjectParameter__Implementation__),
    "Type", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__)
    )]


	/*
    Relation: FK_ConstrainedProperty_has_Constraints
    A: One Property as ConstrainedProperty
    B: ZeroOrMore Constraint as Constraints
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ConstrainedProperty_has_Constraints",
    "ConstrainedProperty", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Property__Implementation__),
    "Constraints", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Constraint__Implementation__)
    )]


	/*
    Relation: FK_Constraint_on_Constrained
    A: ZeroOrMore InstanceConstraint as Constraint
    B: ZeroOrOne ObjectClass as Constrained
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Constraint_on_Constrained",
    "Constraint", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.InstanceConstraint__Implementation__),
    "Constrained", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClass__Implementation__)
    )]


	/*
    Relation: FK_Constraint_was_ChangedBy
    A: ZeroOrMore Constraint as Constraint
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Constraint_was_ChangedBy",
    "Constraint", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Constraint__Implementation__),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_Constraint_was_CreatedBy
    A: ZeroOrMore Constraint as Constraint
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Constraint_was_CreatedBy",
    "Constraint", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Constraint__Implementation__),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_Control_isof_Kind
    A: ZeroOrMore ViewDescriptor as Control
    B: ZeroOrOne ControlKindClass as Kind
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Control_isof_Kind",
    "Control", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ViewDescriptor__Implementation__),
    "Kind", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ControlKindClass__Implementation__)
    )]


	/*
    Relation: FK_ControlKindClass_supports_SupportedInterfaces
    A: ZeroOrMore ControlKindClass as ControlKindClass
    B: ZeroOrMore TypeRef as SupportedInterfaces
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_ControlKindClass_supports_SupportedInterfaces_A",
    "ControlKindClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ControlKindClass__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ControlKindClass_supports_TypeRef_RelationEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_ControlKindClass_supports_SupportedInterfaces_B",
    "SupportedInterfaces", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ControlKindClass_supports_TypeRef_RelationEntry__Implementation__)
    )]

	/*
    Relation: FK_DataType_has_DefaultIcon
    A: ZeroOrMore DataType as DataType
    B: ZeroOrOne Icon as DefaultIcon
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_DataType_has_DefaultIcon",
    "DataType", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.DataType__Implementation__),
    "DefaultIcon", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Icon__Implementation__)
    )]


	/*
    Relation: FK_DataType_was_ChangedBy
    A: ZeroOrMore DataType as DataType
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_DataType_was_ChangedBy",
    "DataType", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.DataType__Implementation__),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_DataType_was_CreatedBy
    A: ZeroOrMore DataType as DataType
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_DataType_was_CreatedBy",
    "DataType", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.DataType__Implementation__),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_DefaultPropertyValue_was_ChangedBy
    A: ZeroOrMore DefaultPropertyValue as DefaultPropertyValue
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_DefaultPropertyValue_was_ChangedBy",
    "DefaultPropertyValue", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.DefaultPropertyValue__Implementation__),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_DefaultPropertyValue_was_CreatedBy
    A: ZeroOrMore DefaultPropertyValue as DefaultPropertyValue
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_DefaultPropertyValue_was_CreatedBy",
    "DefaultPropertyValue", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.DefaultPropertyValue__Implementation__),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_Descriptor_has_PresentableModelRef
    A: ZeroOrMore PresentableModelDescriptor as Descriptor
    B: One TypeRef as PresentableModelRef
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Descriptor_has_PresentableModelRef",
    "Descriptor", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.PresentableModelDescriptor__Implementation__),
    "PresentableModelRef", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__)
    )]


	/*
    Relation: FK_Enumeration_has_EnumerationEntries
    A: One Enumeration as Enumeration
    B: ZeroOrMore EnumerationEntry as EnumerationEntries
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Enumeration_has_EnumerationEntries",
    "Enumeration", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Enumeration__Implementation__),
    "EnumerationEntries", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.EnumerationEntry__Implementation__)
    )]


	/*
    Relation: FK_EnumerationEntry_was_ChangedBy
    A: ZeroOrMore EnumerationEntry as EnumerationEntry
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_EnumerationEntry_was_ChangedBy",
    "EnumerationEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.EnumerationEntry__Implementation__),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_EnumerationEntry_was_CreatedBy
    A: ZeroOrMore EnumerationEntry as EnumerationEntry
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_EnumerationEntry_was_CreatedBy",
    "EnumerationEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.EnumerationEntry__Implementation__),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
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
    Relation: FK_InvokeOnObjectClass_has_MethodInvocations
    A: One DataType as InvokeOnObjectClass
    B: ZeroOrMore MethodInvocation as MethodInvocations
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_InvokeOnObjectClass_has_MethodInvocations",
    "InvokeOnObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DataType__Implementation__),
    "MethodInvocations", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation__Implementation__)
    )]


	/*
    Relation: FK_InvokeOnProperty_has_Invocations
    A: One Property as InvokeOnProperty
    B: ZeroOrMore PropertyInvocation as Invocations
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_InvokeOnProperty_has_Invocations",
    "InvokeOnProperty", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Property__Implementation__),
    "Invocations", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.PropertyInvocation__Implementation__)
    )]


	/*
    Relation: FK_Kunde_was_ChangedBy
    A: ZeroOrMore Kunde as Kunde
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Kunde_was_ChangedBy",
    "Kunde", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Kunde__Implementation__),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_Kunde_was_CreatedBy
    A: ZeroOrMore Kunde as Kunde
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Kunde_was_CreatedBy",
    "Kunde", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Kunde__Implementation__),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_List_has_ItemKind
    A: ZeroOrMore StringListKind as List
    B: One ControlKindClass as ItemKind
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_List_has_ItemKind",
    "List", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.StringListKind__Implementation__),
    "ItemKind", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ControlKindClass__Implementation__)
    )]


	/*
    Relation: FK_Method_has_MethodInvocations
    A: One Method as Method
    B: ZeroOrMore MethodInvocation as MethodInvocations
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Method_has_MethodInvocations",
    "Method", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Method__Implementation__),
    "MethodInvocations", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation__Implementation__)
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
    Relation: FK_Method_has_Parameter
    A: One Method as Method
    B: ZeroOrMore BaseParameter as Parameter
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Method_has_Parameter",
    "Method", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Method__Implementation__),
    "Parameter", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.BaseParameter__Implementation__)
    )]


	/*
    Relation: FK_Method_was_ChangedBy
    A: ZeroOrMore Method as Method
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Method_was_ChangedBy",
    "Method", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Method__Implementation__),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_Method_was_CreatedBy
    A: ZeroOrMore Method as Method
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Method_was_CreatedBy",
    "Method", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Method__Implementation__),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_MethodInvocation_has_Implementor
    A: ZeroOrMore MethodInvocation as MethodInvocation
    B: ZeroOrOne TypeRef as Implementor
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_MethodInvocation_has_Implementor",
    "MethodInvocation", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation__Implementation__),
    "Implementor", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__)
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
    Relation: FK_MethodInvocation_was_ChangedBy
    A: ZeroOrMore MethodInvocation as MethodInvocation
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_MethodInvocation_was_ChangedBy",
    "MethodInvocation", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation__Implementation__),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_MethodInvocation_was_CreatedBy
    A: ZeroOrMore MethodInvocation as MethodInvocation
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_MethodInvocation_was_CreatedBy",
    "MethodInvocation", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation__Implementation__),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_Mitarbeiter_was_ChangedBy
    A: ZeroOrMore Mitarbeiter as Mitarbeiter
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Mitarbeiter_was_ChangedBy",
    "Mitarbeiter", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Mitarbeiter__Implementation__),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_Mitarbeiter_was_CreatedBy
    A: ZeroOrMore Mitarbeiter as Mitarbeiter
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Mitarbeiter_was_CreatedBy",
    "Mitarbeiter", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Mitarbeiter__Implementation__),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_Module_has_Assemblies
    A: One Module as Module
    B: ZeroOrMore Assembly as Assemblies
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Module_has_Assemblies",
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module__Implementation__),
    "Assemblies", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    Relation: FK_Module_has_DataTypes
    A: One Module as Module
    B: ZeroOrMore DataType as DataTypes
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Module_has_DataTypes",
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
    Relation: FK_Module_was_ChangedBy
    A: ZeroOrMore Module as Module
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Module_was_ChangedBy",
    "Module", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Module__Implementation__),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_Module_was_CreatedBy
    A: ZeroOrMore Module as Module
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Module_was_CreatedBy",
    "Module", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Module__Implementation__),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_MuhBlah_List_Role_hasOther_TestCustomObjects_Role
    A: ZeroOrMore Muhblah as MuhBlah_List_Role
    B: ZeroOrOne TestCustomObject as TestCustomObjects_Role
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_MuhBlah_List_Role_hasOther_TestCustomObjects_Role",
    "MuhBlah_List_Role", RelationshipMultiplicity.Many, typeof(Kistl.App.Test.Muhblah__Implementation__),
    "TestCustomObjects_Role", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Test.TestCustomObject__Implementation__)
    )]


	/*
    Relation: FK_MuhBlah_ManyList_Role_has_TestCustomObjects_ManyList_Role
    A: ZeroOrMore Muhblah as MuhBlah_ManyList_Role
    B: ZeroOrMore TestCustomObject as TestCustomObjects_ManyList_Role
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_MuhBlah_ManyList_Role_has_TestCustomObjects_ManyList_Role_A",
    "MuhBlah_ManyList_Role", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Test.Muhblah__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Test.Muhblah_has_TestCustomObject_RelationEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_MuhBlah_ManyList_Role_has_TestCustomObjects_ManyList_Role_B",
    "TestCustomObjects_ManyList_Role", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Test.TestCustomObject__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Test.Muhblah_has_TestCustomObject_RelationEntry__Implementation__)
    )]

	/*
    Relation: FK_MuhBlah_One_Role_loves_TestCustomObjects_One_Role
    A: ZeroOrOne Muhblah as MuhBlah_One_Role
    B: ZeroOrOne TestCustomObject as TestCustomObjects_One_Role
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_MuhBlah_One_Role_loves_TestCustomObjects_One_Role",
    "MuhBlah_One_Role", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Test.Muhblah__Implementation__),
    "TestCustomObjects_One_Role", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Test.TestCustomObject__Implementation__)
    )]


	/*
    Relation: FK_MuhBlah_Role_has_TestCustomObjects_List_Role
    A: ZeroOrOne Muhblah as MuhBlah_Role
    B: ZeroOrMore TestCustomObject as TestCustomObjects_List_Role
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_MuhBlah_Role_has_TestCustomObjects_List_Role",
    "MuhBlah_Role", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Test.Muhblah__Implementation__),
    "TestCustomObjects_List_Role", RelationshipMultiplicity.Many, typeof(Kistl.App.Test.TestCustomObject__Implementation__)
    )]


	/*
    Relation: FK_ObjectClass_has_Methods
    A: One DataType as ObjectClass
    B: ZeroOrMore Method as Methods
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ObjectClass_has_Methods",
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DataType__Implementation__),
    "Methods", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Method__Implementation__)
    )]


	/*
    Relation: FK_ObjectClass_has_Properties
    A: One DataType as ObjectClass
    B: ZeroOrMore Property as Properties
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ObjectClass_has_Properties",
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DataType__Implementation__),
    "Properties", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Property__Implementation__)
    )]


	/*
    Relation: FK_ObjectClass_implements_ImplementsInterfaces
    A: ZeroOrMore ObjectClass as ObjectClass
    B: ZeroOrMore Interface as ImplementsInterfaces
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_ObjectClass_implements_ImplementsInterfaces_A",
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClass__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass_implements_Interface_RelationEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_ObjectClass_implements_ImplementsInterfaces_B",
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
    Relation: FK_ObjectReferencePlaceholderProperty_ofType_ReferencedObjectClass
    A: ZeroOrMore ObjectReferencePlaceholderProperty as ObjectReferencePlaceholderProperty
    B: One ObjectClass as ReferencedObjectClass
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ObjectReferencePlaceholderProperty_ofType_ReferencedObjectClass",
    "ObjectReferencePlaceholderProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectReferencePlaceholderProperty__Implementation__),
    "ReferencedObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClass__Implementation__)
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
    Relation: FK_Presentable_displayedBy_SecondaryControlKinds
    A: ZeroOrMore PresentableModelDescriptor as Presentable
    B: ZeroOrMore ControlKind as SecondaryControlKinds
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Presentable_displayedBy_SecondaryControlKinds_A",
    "Presentable", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.PresentableModelDescriptor__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.PresentableModelDescriptor_displayedBy_ControlKind_RelationEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Presentable_displayedBy_SecondaryControlKinds_B",
    "SecondaryControlKinds", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ControlKind__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.PresentableModelDescriptor_displayedBy_ControlKind_RelationEntry__Implementation__)
    )]

	/*
    Relation: FK_Presentable_has_DefaultKind
    A: ZeroOrOne PresentableModelDescriptor as Presentable
    B: ZeroOrOne ControlKind as DefaultKind
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Presentable_has_DefaultKind",
    "Presentable", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.PresentableModelDescriptor__Implementation__),
    "DefaultKind", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ControlKind__Implementation__)
    )]


	/*
    Relation: FK_Presentable_has_DefaultPresentableModelDescriptor
    A: ZeroOrMore ObjectClass as Presentable
    B: One PresentableModelDescriptor as DefaultPresentableModelDescriptor
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Presentable_has_DefaultPresentableModelDescriptor",
    "Presentable", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass__Implementation__),
    "DefaultPresentableModelDescriptor", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.PresentableModelDescriptor__Implementation__)
    )]


	/*
    Relation: FK_PresentableModelDescriptor_displayedInGridBy_DefaultGridCellKind
    A: ZeroOrMore PresentableModelDescriptor as PresentableModelDescriptor
    B: ZeroOrOne ControlKind as DefaultGridCellKind
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_PresentableModelDescriptor_displayedInGridBy_DefaultGridCellKind",
    "PresentableModelDescriptor", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.PresentableModelDescriptor__Implementation__),
    "DefaultGridCellKind", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ControlKind__Implementation__)
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
    Relation: FK_Projekt_has_Auftraege
    A: ZeroOrOne Projekt as Projekt
    B: ZeroOrMore Auftrag as Auftraege
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Projekt_has_Auftraege",
    "Projekt", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Projekt__Implementation__),
    "Auftraege", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Auftrag__Implementation__)
    )]


	/*
    Relation: FK_Projekt_has_Tasks
    A: One Projekt as Projekt
    B: ZeroOrMore Task as Tasks
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Projekt_has_Tasks",
    "Projekt", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Projekt__Implementation__),
    "Tasks", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Task__Implementation__)
    )]


	/*
    Relation: FK_Projekt_was_ChangedBy
    A: ZeroOrMore Projekt as Projekt
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Projekt_was_ChangedBy",
    "Projekt", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Projekt__Implementation__),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_Projekt_was_CreatedBy
    A: ZeroOrMore Projekt as Projekt
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Projekt_was_CreatedBy",
    "Projekt", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Projekt__Implementation__),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_Projekte_haben_Mitarbeiter
    A: ZeroOrMore Projekt as Projekte
    B: ZeroOrMore Mitarbeiter as Mitarbeiter
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Projekte_haben_Mitarbeiter_A",
    "Projekte", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Projekt__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Projekt_haben_Mitarbeiter_RelationEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Projekte_haben_Mitarbeiter_B",
    "Mitarbeiter", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Mitarbeiter__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Projekt_haben_Mitarbeiter_RelationEntry__Implementation__)
    )]

	/*
    Relation: FK_Property_has_DefaultValue
    A: One Property as Property
    B: ZeroOrOne DefaultPropertyValue as DefaultValue
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Property_has_DefaultValue",
    "Property", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Property__Implementation__),
    "DefaultValue", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DefaultPropertyValue__Implementation__)
    )]


	/*
    Relation: FK_Property_has_ValueModelDescriptor
    A: ZeroOrMore Property as Property
    B: One PresentableModelDescriptor as ValueModelDescriptor
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Property_has_ValueModelDescriptor",
    "Property", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Property__Implementation__),
    "ValueModelDescriptor", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.PresentableModelDescriptor__Implementation__)
    )]


	/*
    Relation: FK_Property_was_ChangedBy
    A: ZeroOrMore Property as Property
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Property_was_ChangedBy",
    "Property", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Property__Implementation__),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_Property_was_CreatedBy
    A: ZeroOrMore Property as Property
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Property_was_CreatedBy",
    "Property", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Property__Implementation__),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_PropertyInvocation_has_Implementor
    A: ZeroOrMore PropertyInvocation as PropertyInvocation
    B: One TypeRef as Implementor
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_PropertyInvocation_has_Implementor",
    "PropertyInvocation", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.PropertyInvocation__Implementation__),
    "Implementor", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__)
    )]


	/*
    Relation: FK_PropertyInvocation_was_ChangedBy
    A: ZeroOrMore PropertyInvocation as PropertyInvocation
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_PropertyInvocation_was_ChangedBy",
    "PropertyInvocation", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.PropertyInvocation__Implementation__),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_PropertyInvocation_was_CreatedBy
    A: ZeroOrMore PropertyInvocation as PropertyInvocation
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_PropertyInvocation_was_CreatedBy",
    "PropertyInvocation", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.PropertyInvocation__Implementation__),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_Relation_hasA_A
    A: ZeroOrOne Relation as Relation
    B: ZeroOrOne RelationEnd as A
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Relation_hasA_A",
    "Relation", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Relation__Implementation__),
    "A", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.RelationEnd__Implementation__)
    )]


	/*
    Relation: FK_Relation_hasB_B
    A: ZeroOrOne Relation as Relation
    B: ZeroOrOne RelationEnd as B
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Relation_hasB_B",
    "Relation", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Relation__Implementation__),
    "B", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.RelationEnd__Implementation__)
    )]


	/*
    Relation: FK_Relation_was_ChangedBy
    A: ZeroOrMore Relation as Relation
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Relation_was_ChangedBy",
    "Relation", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Relation__Implementation__),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_Relation_was_CreatedBy
    A: ZeroOrMore Relation as Relation
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Relation_was_CreatedBy",
    "Relation", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Relation__Implementation__),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_RelationEnd_has_Navigator
    A: ZeroOrOne RelationEnd as RelationEnd
    B: ZeroOrOne ObjectReferenceProperty as Navigator
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_RelationEnd_has_Navigator",
    "RelationEnd", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.RelationEnd__Implementation__),
    "Navigator", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectReferenceProperty__Implementation__)
    )]


	/*
    Relation: FK_RelationEnd_has_Type
    A: ZeroOrMore RelationEnd as RelationEnd
    B: ZeroOrOne ObjectClass as Type
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_RelationEnd_has_Type",
    "RelationEnd", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.RelationEnd__Implementation__),
    "Type", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClass__Implementation__)
    )]


	/*
    Relation: FK_RelationEnd_was_ChangedBy
    A: ZeroOrMore RelationEnd as RelationEnd
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_RelationEnd_was_ChangedBy",
    "RelationEnd", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.RelationEnd__Implementation__),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_RelationEnd_was_CreatedBy
    A: ZeroOrMore RelationEnd as RelationEnd
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_RelationEnd_was_CreatedBy",
    "RelationEnd", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.RelationEnd__Implementation__),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_StructProperty_has_StructDefinition
    A: ZeroOrMore StructProperty as StructProperty
    B: ZeroOrOne Struct as StructDefinition
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_StructProperty_has_StructDefinition",
    "StructProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.StructProperty__Implementation__),
    "StructDefinition", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Struct__Implementation__)
    )]


	/*
    Relation: FK_Task_was_ChangedBy
    A: ZeroOrMore Task as Task
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Task_was_ChangedBy",
    "Task", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Task__Implementation__),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_Task_was_CreatedBy
    A: ZeroOrMore Task as Task
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Task_was_CreatedBy",
    "Task", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Task__Implementation__),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_Template_has_DisplayedTypeAssembly
    A: ZeroOrMore Template as Template
    B: ZeroOrOne Assembly as DisplayedTypeAssembly
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Template_has_DisplayedTypeAssembly",
    "Template", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Template__Implementation__),
    "DisplayedTypeAssembly", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Assembly__Implementation__)
    )]


	/*
    Relation: FK_Template_has_VisualTree
    A: ZeroOrMore Template as Template
    B: ZeroOrOne Visual as VisualTree
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Template_has_VisualTree",
    "Template", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Template__Implementation__),
    "VisualTree", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Visual__Implementation__)
    )]


	/*
    Relation: FK_Template_hasMenu_Menu
    A: ZeroOrMore Template as Template
    B: ZeroOrMore Visual as Menu
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Template_hasMenu_Menu_A",
    "Template", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Template__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Template_hasMenu_Visual_RelationEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Template_hasMenu_Menu_B",
    "Menu", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Visual__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Template_hasMenu_Visual_RelationEntry__Implementation__)
    )]

	/*
    Relation: FK_TestObjClass_has_ObjectProp
    A: ZeroOrMore TestObjClass as TestObjClass
    B: ZeroOrOne Kunde as ObjectProp
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_TestObjClass_has_ObjectProp",
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
    Relation: FK_TypeRef_hasGenericArguments_GenericArguments
    A: ZeroOrMore TypeRef as TypeRef
    B: ZeroOrMore TypeRef as GenericArguments
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_TypeRef_hasGenericArguments_GenericArguments_A",
    "TypeRef", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.TypeRef_hasGenericArguments_TypeRef_RelationEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_TypeRef_hasGenericArguments_GenericArguments_B",
    "GenericArguments", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.TypeRef_hasGenericArguments_TypeRef_RelationEntry__Implementation__)
    )]

	/*
    Relation: FK_TypeRef_was_ChangedBy
    A: ZeroOrMore TypeRef as TypeRef
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_TypeRef_was_ChangedBy",
    "TypeRef", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.TypeRef__Implementation__),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_TypeRef_was_CreatedBy
    A: ZeroOrMore TypeRef as TypeRef
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_TypeRef_was_CreatedBy",
    "TypeRef", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.TypeRef__Implementation__),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Identity__Implementation__)
    )]


	/*
    Relation: FK_View_has_ControlRef
    A: ZeroOrMore ViewDescriptor as View
    B: One TypeRef as ControlRef
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_View_has_ControlRef",
    "View", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ViewDescriptor__Implementation__),
    "ControlRef", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRef__Implementation__)
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
    Relation: FK_Visual_contains_Children
    A: ZeroOrMore Visual as Visual
    B: ZeroOrMore Visual as Children
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Visual_contains_Children_A",
    "Visual", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Visual__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual_contains_Visual_RelationEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Visual_contains_Children_B",
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
    Relation: FK_Visual_hasContextMenu_ContextMenu
    A: ZeroOrMore Visual as Visual
    B: ZeroOrMore Visual as ContextMenu
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Visual_hasContextMenu_ContextMenu_A",
    "Visual", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.Visual__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual_hasContextMenu_Visual_RelationEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Visual_hasContextMenu_ContextMenu_B",
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
[assembly: EdmRelationship("Model", "FK_WorkEffortAccount_has_Mitarbeiter_A",
    "WorkEffortAccount", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.TimeRecords.WorkEffortAccount__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.TimeRecords.WorkEffortAccount_has_Mitarbeiter_RelationEntry__Implementation__)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_WorkEffortAccount_has_Mitarbeiter_B",
    "Mitarbeiter", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Mitarbeiter__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.TimeRecords.WorkEffortAccount_has_Mitarbeiter_RelationEntry__Implementation__)
    )]


// object-value association
[assembly: EdmRelationship(
    "Model", "FK_Kunde_value_EMails",
    "Kunde", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Kunde__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Kunde_EMails_CollectionEntry__Implementation__)
    )]


// object-value association
[assembly: EdmRelationship(
    "Model", "FK_ObjectClass_value_SecurityRules",
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClass__Implementation__),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass_SecurityRules_CollectionEntry__Implementation__)
    )]

