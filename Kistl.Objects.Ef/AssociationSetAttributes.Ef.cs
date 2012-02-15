using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Xml;
using System.Xml.Serialization;

using Kistl.API;
using Kistl.DalProvider.Ef;


	/*
    Relation: FK_AbstractModuleMember_has_Module
    A: ZeroOrMore AbstractModuleMember as AbstractModuleMember
    B: One Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_AbstractModuleMember_has_Module",
    "AbstractModuleMember", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.AbstractModuleMemberEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ModuleEfImpl)
    )]


	/*
    Relation: FK_AbstractModuleMember_was_ChangedBy
    A: ZeroOrMore AbstractModuleMember as AbstractModuleMember
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_AbstractModuleMember_was_ChangedBy",
    "AbstractModuleMember", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.AbstractModuleMemberEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_AbstractModuleMember_was_CreatedBy
    A: ZeroOrMore AbstractModuleMember as AbstractModuleMember
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_AbstractModuleMember_was_CreatedBy",
    "AbstractModuleMember", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.AbstractModuleMemberEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_AccessControl_has_Module
    A: ZeroOrMore AccessControl as AccessControl
    B: One Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_AccessControl_has_Module",
    "AccessControl", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.AccessControlEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ModuleEfImpl)
    )]


	/*
    Relation: FK_AccessControl_was_ChangedBy
    A: ZeroOrMore AccessControl as AccessControl
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_AccessControl_was_ChangedBy",
    "AccessControl", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.AccessControlEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_AccessControl_was_CreatedBy
    A: ZeroOrMore AccessControl as AccessControl
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_AccessControl_was_CreatedBy",
    "AccessControl", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.AccessControlEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_Application_has_Module
    A: ZeroOrMore Application as Application
    B: ZeroOrOne Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Application_has_Module",
    "Application", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ApplicationEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ModuleEfImpl)
    )]


	/*
    Relation: FK_Application_has_RootScreen
    A: ZeroOrMore Application as Application
    B: ZeroOrOne NavigationScreen as RootScreen
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Application_has_RootScreen",
    "Application", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ApplicationEfImpl),
    "RootScreen", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.NavigationScreenEfImpl)
    )]


	/*
    Relation: FK_Application_opens_a_WorkspaceViewModel
    A: ZeroOrMore Application as Application
    B: ZeroOrOne ViewModelDescriptor as WorkspaceViewModel
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Application_opens_a_WorkspaceViewModel",
    "Application", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ApplicationEfImpl),
    "WorkspaceViewModel", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ViewModelDescriptorEfImpl)
    )]


	/*
    Relation: FK_Application_was_ChangedBy
    A: ZeroOrMore Application as Application
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Application_was_ChangedBy",
    "Application", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ApplicationEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_Application_was_CreatedBy
    A: ZeroOrMore Application as Application
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Application_was_CreatedBy",
    "Application", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ApplicationEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_ASide_connectsTo_BSide
    A: ZeroOrMore N_to_M_relations_A as ASide
    B: ZeroOrMore N_to_M_relations_B as BSide
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_ASide_connectsTo_BSide_A",
    "ASide", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Test.N_to_M_relations_AEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Test.N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_ASide_connectsTo_BSide_B",
    "BSide", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Test.N_to_M_relations_BEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Test.N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryEfImpl)
    )]

	/*
    Relation: FK_Assembly_was_ChangedBy
    A: ZeroOrMore Assembly as Assembly
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Assembly_was_ChangedBy",
    "Assembly", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.AssemblyEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
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
    "Assembly", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.AssemblyEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
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
    "Auftrag", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.AuftragEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
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
    "Auftrag", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.AuftragEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
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
    "Auftrag", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.AuftragEfImpl),
    "Kunde", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.KundeEfImpl)
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
    "Auftrag", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.AuftragEfImpl),
    "Mitarbeiter", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.MitarbeiterEfImpl)
    )]


	/*
    Relation: FK_BaseCalendar_has_ChildCalendar
    A: ZeroOrOne Calendar as BaseCalendar
    B: ZeroOrMore Calendar as ChildCalendar
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_BaseCalendar_has_ChildCalendar",
    "BaseCalendar", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Calendar.CalendarEfImpl),
    "ChildCalendar", RelationshipMultiplicity.Many, typeof(Kistl.App.Calendar.CalendarEfImpl)
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
    "BaseObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClassEfImpl),
    "SubClasses", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClassEfImpl)
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
    "BaseParameter", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.BaseParameterEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
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
    "BaseParameter", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.BaseParameterEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_BaseProperty_has_Module
    A: ZeroOrMore Property as BaseProperty
    B: One Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_BaseProperty_has_Module",
    "BaseProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.PropertyEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ModuleEfImpl)
    )]


	/*
    Relation: FK_BoolProperty_has_FalseIcon
    A: ZeroOrMore BoolProperty as BoolProperty
    B: ZeroOrOne Icon as FalseIcon
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_BoolProperty_has_FalseIcon",
    "BoolProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.BoolPropertyEfImpl),
    "FalseIcon", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.IconEfImpl)
    )]


	/*
    Relation: FK_BoolProperty_has_NullIcon
    A: ZeroOrMore BoolProperty as BoolProperty
    B: ZeroOrOne Icon as NullIcon
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_BoolProperty_has_NullIcon",
    "BoolProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.BoolPropertyEfImpl),
    "NullIcon", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.IconEfImpl)
    )]


	/*
    Relation: FK_BoolProperty_has_TrueIcon
    A: ZeroOrMore BoolProperty as BoolProperty
    B: ZeroOrOne Icon as TrueIcon
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_BoolProperty_has_TrueIcon",
    "BoolProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.BoolPropertyEfImpl),
    "TrueIcon", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.IconEfImpl)
    )]


	/*
    Relation: FK_CalculatedReference_dependsOn_InputProperties
    A: ZeroOrMore CalculatedObjectReferenceProperty as CalculatedReference
    B: ZeroOrMore Property as InputProperties
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_CalculatedReference_dependsOn_InputProperties_A",
    "CalculatedReference", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.CalculatedObjectReferencePropertyEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.CalculatedObjectReferenceProperty_dependsOn_Property_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_CalculatedReference_dependsOn_InputProperties_B",
    "InputProperties", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.PropertyEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.CalculatedObjectReferenceProperty_dependsOn_Property_RelationEntryEfImpl)
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
    "CalculatedReference", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.CalculatedObjectReferencePropertyEfImpl),
    "ReferencedClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClassEfImpl)
    )]


	/*
    Relation: FK_Calendar_has_CalendarRules
    A: One Calendar as Calendar
    B: ZeroOrMore CalendarRule as CalendarRules
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Calendar_has_CalendarRules",
    "Calendar", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Calendar.CalendarEfImpl),
    "CalendarRules", RelationshipMultiplicity.Many, typeof(Kistl.App.Calendar.CalendarRuleEfImpl)
    )]


	/*
    Relation: FK_Calendar_has_Module
    A: ZeroOrMore Calendar as Calendar
    B: ZeroOrOne Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Calendar_has_Module",
    "Calendar", RelationshipMultiplicity.Many, typeof(Kistl.App.Calendar.CalendarEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ModuleEfImpl)
    )]


	/*
    Relation: FK_Calendar_was_ChangedBy
    A: ZeroOrMore Calendar as Calendar
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Calendar_was_ChangedBy",
    "Calendar", RelationshipMultiplicity.Many, typeof(Kistl.App.Calendar.CalendarEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_Calendar_was_CreatedBy
    A: ZeroOrMore Calendar as Calendar
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Calendar_was_CreatedBy",
    "Calendar", RelationshipMultiplicity.Many, typeof(Kistl.App.Calendar.CalendarEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_CalendarRule_has_Module
    A: ZeroOrMore CalendarRule as CalendarRule
    B: ZeroOrOne Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_CalendarRule_has_Module",
    "CalendarRule", RelationshipMultiplicity.Many, typeof(Kistl.App.Calendar.CalendarRuleEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ModuleEfImpl)
    )]


	/*
    Relation: FK_CalendarRule_was_ChangedBy
    A: ZeroOrMore CalendarRule as CalendarRule
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_CalendarRule_was_ChangedBy",
    "CalendarRule", RelationshipMultiplicity.Many, typeof(Kistl.App.Calendar.CalendarRuleEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_CalendarRule_was_CreatedBy
    A: ZeroOrMore CalendarRule as CalendarRule
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_CalendarRule_was_CreatedBy",
    "CalendarRule", RelationshipMultiplicity.Many, typeof(Kistl.App.Calendar.CalendarRuleEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
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
    "Child", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.TypeRefEfImpl),
    "Parent", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRefEfImpl)
    )]


	/*
    Relation: FK_ChildControlKinds_have_a_Parent
    A: ZeroOrMore ControlKind as ChildControlKinds
    B: ZeroOrOne ControlKind as Parent
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ChildControlKinds_have_a_Parent",
    "ChildControlKinds", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ControlKindEfImpl),
    "Parent", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ControlKindEfImpl)
    )]


	/*
    Relation: FK_ClrObjectParameter_isOf_Type
    A: ZeroOrMore CLRObjectParameter as ClrObjectParameter
    B: One TypeRef as Type
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ClrObjectParameter_isOf_Type",
    "ClrObjectParameter", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.CLRObjectParameterEfImpl),
    "Type", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRefEfImpl)
    )]


	/*
    Relation: FK_CompoundObjectProperty_has_CompoundObjectDefinition
    A: ZeroOrMore CompoundObjectProperty as CompoundObjectProperty
    B: One CompoundObject as CompoundObjectDefinition
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_CompoundObjectProperty_has_CompoundObjectDefinition",
    "CompoundObjectProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.CompoundObjectPropertyEfImpl),
    "CompoundObjectDefinition", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.CompoundObjectEfImpl)
    )]


	/*
    Relation: FK_Configuration_of_Identity
    A: ZeroOrMore FileImportConfiguration as Configuration
    B: ZeroOrOne Identity as Identity
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Configuration_of_Identity",
    "Configuration", RelationshipMultiplicity.Many, typeof(at.dasz.DocumentManagement.FileImportConfigurationEfImpl),
    "Identity", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
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
    "ConstrainedProperty", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.PropertyEfImpl),
    "Constraints", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ConstraintEfImpl)
    )]


	/*
    Relation: FK_Constraint_invokes_GetErrorTextInvocation
    A: One InvokingConstraint as Constraint
    B: One ConstraintInvocation as GetErrorTextInvocation
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Constraint_invokes_GetErrorTextInvocation",
    "Constraint", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.InvokingConstraintEfImpl),
    "GetErrorTextInvocation", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ConstraintInvocationEfImpl)
    )]


	/*
    Relation: FK_Constraint_invokes_IsValidInvocation
    A: One InvokingConstraint as Constraint
    B: One ConstraintInvocation as IsValidInvocation
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Constraint_invokes_IsValidInvocation",
    "Constraint", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.InvokingConstraintEfImpl),
    "IsValidInvocation", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ConstraintInvocationEfImpl)
    )]


	/*
    Relation: FK_Constraint_on_Constrained
    A: ZeroOrMore InstanceConstraint as Constraint
    B: ZeroOrOne DataType as Constrained
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Constraint_on_Constrained",
    "Constraint", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.InstanceConstraintEfImpl),
    "Constrained", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DataTypeEfImpl)
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
    "Constraint", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ConstraintEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
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
    "Constraint", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ConstraintEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_ConstraintInvocation_has_TypeRef
    A: ZeroOrMore ConstraintInvocation as ConstraintInvocation
    B: ZeroOrOne TypeRef as TypeRef
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ConstraintInvocation_has_TypeRef",
    "ConstraintInvocation", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ConstraintInvocationEfImpl),
    "TypeRef", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRefEfImpl)
    )]


	/*
    Relation: FK_ControlKind_has_Module
    A: ZeroOrMore ControlKind as ControlKind
    B: One Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ControlKind_has_Module",
    "ControlKind", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ControlKindEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ModuleEfImpl)
    )]


	/*
    Relation: FK_CPParameter_has_CompoundObject
    A: ZeroOrMore CompoundObjectParameter as CPParameter
    B: One CompoundObject as CompoundObject
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_CPParameter_has_CompoundObject",
    "CPParameter", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.CompoundObjectParameterEfImpl),
    "CompoundObject", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.CompoundObjectEfImpl)
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
    "DataType", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.DataTypeEfImpl),
    "DefaultIcon", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.IconEfImpl)
    )]


	/*
    Relation: FK_DataType_implements_ImplementedInterfaces
    A: ZeroOrMore DataType as DataType
    B: ZeroOrMore Interface as ImplementedInterfaces
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_DataType_implements_ImplementedInterfaces_A",
    "DataType", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DataTypeEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.DataType_implements_Interface_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_DataType_implements_ImplementedInterfaces_B",
    "ImplementedInterfaces", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.InterfaceEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.DataType_implements_Interface_RelationEntryEfImpl)
    )]

	/*
    Relation: FK_DataType_may_request_ControlKind
    A: ZeroOrMore DataType as DataType
    B: ZeroOrOne ControlKind as ControlKind
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_DataType_may_request_ControlKind",
    "DataType", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.DataTypeEfImpl),
    "ControlKind", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ControlKindEfImpl)
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
    "DataType", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.DataTypeEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
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
    "DataType", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.DataTypeEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
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
    "DefaultPropertyValue", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.DefaultPropertyValueEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
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
    "DefaultPropertyValue", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.DefaultPropertyValueEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_Descriptor_has_ViewModelRef
    A: ZeroOrMore ViewModelDescriptor as Descriptor
    B: One TypeRef as ViewModelRef
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Descriptor_has_ViewModelRef",
    "Descriptor", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ViewModelDescriptorEfImpl),
    "ViewModelRef", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRefEfImpl)
    )]


	/*
    Relation: FK_Document_has_Revisions
    A: ZeroOrMore Document as Document
    B: ZeroOrMore Blob as Revisions
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Document_has_Revisions_A",
    "Document", RelationshipMultiplicity.ZeroOrOne, typeof(at.dasz.DocumentManagement.DocumentEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(at.dasz.DocumentManagement.Document_has_Blob_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Document_has_Revisions_B",
    "Revisions", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.BlobEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(at.dasz.DocumentManagement.Document_has_Blob_RelationEntryEfImpl)
    )]

	/*
    Relation: FK_Document_was_ChangedBy
    A: ZeroOrMore Blob as Document
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Document_was_ChangedBy",
    "Document", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.BlobEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_Document_was_CreatedBy
    A: ZeroOrMore Blob as Document
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Document_was_CreatedBy",
    "Document", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.BlobEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_Ein_Fragebogen_enthaelt_gute_Antworten
    A: One Fragebogen as Ein_Fragebogen
    B: ZeroOrMore Antwort as gute_Antworten
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Ein_Fragebogen_enthaelt_gute_Antworten",
    "Ein_Fragebogen", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Test.FragebogenEfImpl),
    "gute_Antworten", RelationshipMultiplicity.Many, typeof(Kistl.App.Test.AntwortEfImpl)
    )]


	/*
    Relation: FK_EnumDefaultValue_defaults_to_EnumValue
    A: ZeroOrMore EnumDefaultValue as EnumDefaultValue
    B: One EnumerationEntry as EnumValue
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_EnumDefaultValue_defaults_to_EnumValue",
    "EnumDefaultValue", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.EnumDefaultValueEfImpl),
    "EnumValue", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.EnumerationEntryEfImpl)
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
    "Enumeration", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.EnumerationEfImpl),
    "EnumerationEntries", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.EnumerationEntryEfImpl)
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
    "EnumerationEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.EnumerationEntryEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
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
    "EnumerationEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.EnumerationEntryEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_EnumerationProperty_has_Enumeration
    A: ZeroOrMore EnumerationProperty as EnumerationProperty
    B: One Enumeration as Enumeration
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_EnumerationProperty_has_Enumeration",
    "EnumerationProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.EnumerationPropertyEfImpl),
    "Enumeration", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.EnumerationEfImpl)
    )]


	/*
    Relation: FK_EnumParameter_has_Enumeration
    A: ZeroOrMore EnumParameter as EnumParameter
    B: One Enumeration as Enumeration
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_EnumParameter_has_Enumeration",
    "EnumParameter", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.EnumParameterEfImpl),
    "Enumeration", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.EnumerationEfImpl)
    )]


	/*
    Relation: FK_File_has_Blob
    A: ZeroOrMore File as File
    B: One Blob as Blob
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_File_has_Blob",
    "File", RelationshipMultiplicity.Many, typeof(at.dasz.DocumentManagement.FileEfImpl),
    "Blob", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.BlobEfImpl)
    )]


	/*
    Relation: FK_File_was_ChangedBy
    A: ZeroOrMore File as File
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_File_was_ChangedBy",
    "File", RelationshipMultiplicity.Many, typeof(at.dasz.DocumentManagement.FileEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_File_was_CreatedBy
    A: ZeroOrMore File as File
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_File_was_CreatedBy",
    "File", RelationshipMultiplicity.Many, typeof(at.dasz.DocumentManagement.FileEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_FileImportConfiguration_was_ChangedBy
    A: ZeroOrMore FileImportConfiguration as FileImportConfiguration
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_FileImportConfiguration_was_ChangedBy",
    "FileImportConfiguration", RelationshipMultiplicity.Many, typeof(at.dasz.DocumentManagement.FileImportConfigurationEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_FileImportConfiguration_was_CreatedBy
    A: ZeroOrMore FileImportConfiguration as FileImportConfiguration
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_FileImportConfiguration_was_CreatedBy",
    "FileImportConfiguration", RelationshipMultiplicity.Many, typeof(at.dasz.DocumentManagement.FileImportConfigurationEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_FilterConfiguration_has_Module
    A: ZeroOrMore FilterConfiguration as FilterConfiguration
    B: One Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_FilterConfiguration_has_Module",
    "FilterConfiguration", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.FilterConfigurationEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ModuleEfImpl)
    )]


	/*
    Relation: FK_FilterConfiguration_has_RequestedKind
    A: ZeroOrMore FilterConfiguration as FilterConfiguration
    B: ZeroOrOne ControlKind as RequestedKind
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_FilterConfiguration_has_RequestedKind",
    "FilterConfiguration", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.FilterConfigurationEfImpl),
    "RequestedKind", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ControlKindEfImpl)
    )]


	/*
    Relation: FK_FilterConfiguration_has_ViewModelDescriptor
    A: ZeroOrMore FilterConfiguration as FilterConfiguration
    B: One ViewModelDescriptor as ViewModelDescriptor
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_FilterConfiguration_has_ViewModelDescriptor",
    "FilterConfiguration", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.FilterConfigurationEfImpl),
    "ViewModelDescriptor", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ViewModelDescriptorEfImpl)
    )]


	/*
    Relation: FK_FilterConfiguration_was_ChangedBy
    A: ZeroOrMore FilterConfiguration as FilterConfiguration
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_FilterConfiguration_was_ChangedBy",
    "FilterConfiguration", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.FilterConfigurationEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_FilterConfiguration_was_CreatedBy
    A: ZeroOrMore FilterConfiguration as FilterConfiguration
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_FilterConfiguration_was_CreatedBy",
    "FilterConfiguration", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.FilterConfigurationEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_FK_Column_references_PK_Column
    A: ZeroOrMore SourceColumn as FK_Column
    B: ZeroOrOne SourceColumn as PK_Column
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_FK_Column_references_PK_Column",
    "FK_Column", RelationshipMultiplicity.Many, typeof(ZBox.App.SchemaMigration.SourceColumnEfImpl),
    "PK_Column", RelationshipMultiplicity.ZeroOrOne, typeof(ZBox.App.SchemaMigration.SourceColumnEfImpl)
    )]


	/*
    Relation: FK_Group_has_Module
    A: ZeroOrMore Group as Group
    B: One Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Group_has_Module",
    "Group", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.GroupEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ModuleEfImpl)
    )]


	/*
    Relation: FK_GroupMembership_has_Group
    A: ZeroOrMore GroupMembership as GroupMembership
    B: One Group as Group
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_GroupMembership_has_Group",
    "GroupMembership", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.GroupMembershipEfImpl),
    "Group", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.GroupEfImpl)
    )]


	/*
    Relation: FK_Icon_has_Blob
    A: ZeroOrOne Icon as Icon
    B: One Blob as Blob
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Icon_has_Blob",
    "Icon", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.IconEfImpl),
    "Blob", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.BlobEfImpl)
    )]


	/*
    Relation: FK_Icon_has_Module
    A: ZeroOrMore Icon as Icon
    B: One Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Icon_has_Module",
    "Icon", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.IconEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ModuleEfImpl)
    )]


	/*
    Relation: FK_Identities_memberOf_Groups
    A: ZeroOrMore Identity as Identities
    B: ZeroOrMore Group as Groups
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Identities_memberOf_Groups_A",
    "Identities", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Identity_memberOf_Group_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Identities_memberOf_Groups_B",
    "Groups", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.GroupEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.Identity_memberOf_Group_RelationEntryEfImpl)
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
    "Kunde", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.KundeEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
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
    "Kunde", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.KundeEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_MB_Lst_Role_hasOther_TCO_Role
    A: ZeroOrMore Muhblah as MB_Lst_Role
    B: ZeroOrOne TestCustomObject as TCO_Role
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_MB_Lst_Role_hasOther_TCO_Role",
    "MB_Lst_Role", RelationshipMultiplicity.Many, typeof(Kistl.App.Test.MuhblahEfImpl),
    "TCO_Role", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Test.TestCustomObjectEfImpl)
    )]


	/*
    Relation: FK_MB_Many_Role_has_TCO_ManyList_Role
    A: ZeroOrMore Muhblah as MB_Many_Role
    B: ZeroOrMore TestCustomObject as TCO_ManyList_Role
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_MB_Many_Role_has_TCO_ManyList_Role_A",
    "MB_Many_Role", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Test.MuhblahEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Test.Muhblah_has_TestCustomObject_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_MB_Many_Role_has_TCO_ManyList_Role_B",
    "TCO_ManyList_Role", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Test.TestCustomObjectEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Test.Muhblah_has_TestCustomObject_RelationEntryEfImpl)
    )]

	/*
    Relation: FK_MB_One_Role_loves_TCO_One_Role
    A: ZeroOrOne Muhblah as MB_One_Role
    B: ZeroOrOne TestCustomObject as TCO_One_Role
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_MB_One_Role_loves_TCO_One_Role",
    "MB_One_Role", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Test.MuhblahEfImpl),
    "TCO_One_Role", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Test.TestCustomObjectEfImpl)
    )]


	/*
    Relation: FK_MB_Role_has_TCO_Lst_Role
    A: ZeroOrOne Muhblah as MB_Role
    B: ZeroOrMore TestCustomObject as TCO_Lst_Role
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_MB_Role_has_TCO_Lst_Role",
    "MB_Role", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Test.MuhblahEfImpl),
    "TCO_Lst_Role", RelationshipMultiplicity.Many, typeof(Kistl.App.Test.TestCustomObjectEfImpl)
    )]


	/*
    Relation: FK_Method_has_Icon
    A: ZeroOrMore Method as Method
    B: ZeroOrOne Icon as Icon
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Method_has_Icon",
    "Method", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodEfImpl),
    "Icon", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.IconEfImpl)
    )]


	/*
    Relation: FK_Method_has_Module
    A: ZeroOrMore Method as Method
    B: One Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Method_has_Module",
    "Method", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ModuleEfImpl)
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
    "Method", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.MethodEfImpl),
    "Parameter", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.BaseParameterEfImpl)
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
    "Method", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
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
    "Method", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_MigrationProject_migrates_to_Module
    A: ZeroOrMore MigrationProject as MigrationProject
    B: ZeroOrOne Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_MigrationProject_migrates_to_Module",
    "MigrationProject", RelationshipMultiplicity.Many, typeof(ZBox.App.SchemaMigration.MigrationProjectEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ModuleEfImpl)
    )]


	/*
    Relation: FK_MigrationProject_reads_from_StagingDatabases
    A: One MigrationProject as MigrationProject
    B: ZeroOrMore StagingDatabase as StagingDatabases
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_MigrationProject_reads_from_StagingDatabases",
    "MigrationProject", RelationshipMultiplicity.ZeroOrOne, typeof(ZBox.App.SchemaMigration.MigrationProjectEfImpl),
    "StagingDatabases", RelationshipMultiplicity.Many, typeof(ZBox.App.SchemaMigration.StagingDatabaseEfImpl)
    )]


	/*
    Relation: FK_MigrationProject_was_ChangedBy
    A: ZeroOrMore MigrationProject as MigrationProject
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_MigrationProject_was_ChangedBy",
    "MigrationProject", RelationshipMultiplicity.Many, typeof(ZBox.App.SchemaMigration.MigrationProjectEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_MigrationProject_was_CreatedBy
    A: ZeroOrMore MigrationProject as MigrationProject
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_MigrationProject_was_CreatedBy",
    "MigrationProject", RelationshipMultiplicity.Many, typeof(ZBox.App.SchemaMigration.MigrationProjectEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_Mitarbeiter_is_a_Identity
    A: ZeroOrOne Mitarbeiter as Mitarbeiter
    B: ZeroOrOne Identity as Identity
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Mitarbeiter_is_a_Identity",
    "Mitarbeiter", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.MitarbeiterEfImpl),
    "Identity", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
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
    "Mitarbeiter", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.MitarbeiterEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
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
    "Mitarbeiter", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.MitarbeiterEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_Module_contains_Assemblies
    A: One Module as Module
    B: ZeroOrMore Assembly as Assemblies
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Module_contains_Assemblies",
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ModuleEfImpl),
    "Assemblies", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.AssemblyEfImpl)
    )]


	/*
    Relation: FK_Module_contains_DataTypes
    A: One Module as Module
    B: ZeroOrMore DataType as DataTypes
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Module_contains_DataTypes",
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ModuleEfImpl),
    "DataTypes", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.DataTypeEfImpl)
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
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ModuleEfImpl),
    "Relation", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.RelationEfImpl)
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
    "Module", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ModuleEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
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
    "Module", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ModuleEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_NavigationScreen_accessed_by_Groups
    A: ZeroOrMore NavigationEntry as NavigationScreen
    B: ZeroOrMore Group as Groups
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_NavigationScreen_accessed_by_Groups_A",
    "NavigationScreen", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.NavigationEntryEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.NavigationEntry_accessed_by_Group_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_NavigationScreen_accessed_by_Groups_B",
    "Groups", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.GroupEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.NavigationEntry_accessed_by_Group_RelationEntryEfImpl)
    )]

	/*
    Relation: FK_NavigationScreen_has_Module
    A: ZeroOrMore NavigationEntry as NavigationScreen
    B: One Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_NavigationScreen_has_Module",
    "NavigationScreen", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.NavigationEntryEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ModuleEfImpl)
    )]


	/*
    Relation: FK_NavigationScreen_was_ChangedBy
    A: ZeroOrMore NavigationEntry as NavigationScreen
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_NavigationScreen_was_ChangedBy",
    "NavigationScreen", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.NavigationEntryEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_NavigationScreen_was_CreatedBy
    A: ZeroOrMore NavigationEntry as NavigationScreen
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_NavigationScreen_was_CreatedBy",
    "NavigationScreen", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.NavigationEntryEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_ObjectClass_has_AccessControlList
    A: One ObjectClass as ObjectClass
    B: ZeroOrMore AccessControl as AccessControlList
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ObjectClass_has_AccessControlList",
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClassEfImpl),
    "AccessControlList", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.AccessControlEfImpl)
    )]


	/*
    Relation: FK_ObjectClass_Has_FilterConfigurations
    A: One ObjectClass as ObjectClass
    B: ZeroOrMore ObjectClassFilterConfiguration as FilterConfigurations
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ObjectClass_Has_FilterConfigurations",
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClassEfImpl),
    "FilterConfigurations", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ObjectClassFilterConfigurationEfImpl)
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
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DataTypeEfImpl),
    "Methods", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodEfImpl)
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
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DataTypeEfImpl),
    "Properties", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.PropertyEfImpl)
    )]


	/*
    Relation: FK_ObjectParameter_has_ObjectClass
    A: ZeroOrMore ObjectReferenceParameter as ObjectParameter
    B: One ObjectClass as ObjectClass
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ObjectParameter_has_ObjectClass",
    "ObjectParameter", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectReferenceParameterEfImpl),
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClassEfImpl)
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
    "ObjectReferencePlaceholderProperty", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectReferencePlaceholderPropertyEfImpl),
    "ReferencedObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClassEfImpl)
    )]


	/*
    Relation: FK_ObjRefProp_shows_Methods
    A: ZeroOrMore ObjectReferenceProperty as ObjRefProp
    B: ZeroOrMore Method as Methods
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_ObjRefProp_shows_Methods_A",
    "ObjRefProp", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectReferencePropertyEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectReferenceProperty_shows_Method_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_ObjRefProp_shows_Methods_B",
    "Methods", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.MethodEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectReferenceProperty_shows_Method_RelationEntryEfImpl)
    )]

	/*
    Relation: FK_OneEnd_hasMany_NEnds
    A: ZeroOrOne OrderedOneEnd as OneEnd
    B: ZeroOrMore OrderedNEnd as NEnds
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_OneEnd_hasMany_NEnds",
    "OneEnd", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Test.OrderedOneEndEfImpl),
    "NEnds", RelationshipMultiplicity.Many, typeof(Kistl.App.Test.OrderedNEndEfImpl)
    )]


	/*
    Relation: FK_OneSide_connectsTo_NSide
    A: ZeroOrOne One_to_N_relations_One as OneSide
    B: ZeroOrMore One_to_N_relations_N as NSide
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_OneSide_connectsTo_NSide",
    "OneSide", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Test.One_to_N_relations_OneEfImpl),
    "NSide", RelationshipMultiplicity.Many, typeof(Kistl.App.Test.One_to_N_relations_NEfImpl)
    )]


	/*
    Relation: FK_Parent_navigates_to_Children
    A: ZeroOrOne NavigationEntry as Parent
    B: ZeroOrMore NavigationEntry as Children
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Parent_navigates_to_Children",
    "Parent", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.NavigationEntryEfImpl),
    "Children", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.NavigationEntryEfImpl)
    )]


	/*
    Relation: FK_Parent_of_Children
    A: One RequiredParent as Parent
    B: ZeroOrMore RequiredParentChild as Children
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Parent_of_Children",
    "Parent", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Test.RequiredParentEfImpl),
    "Children", RelationshipMultiplicity.Many, typeof(Kistl.App.Test.RequiredParentChildEfImpl)
    )]


	/*
    Relation: FK_Presentable_displayedBy_SecondaryControlKinds
    A: ZeroOrMore ViewModelDescriptor as Presentable
    B: ZeroOrMore ControlKind as SecondaryControlKinds
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Presentable_displayedBy_SecondaryControlKinds_A",
    "Presentable", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ViewModelDescriptorEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ViewModelDescriptor_displayedBy_ControlKind_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Presentable_displayedBy_SecondaryControlKinds_B",
    "SecondaryControlKinds", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ControlKindEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ViewModelDescriptor_displayedBy_ControlKind_RelationEntryEfImpl)
    )]

	/*
    Relation: FK_Presentable_has_DefaultKind
    A: ZeroOrMore ViewModelDescriptor as Presentable
    B: ZeroOrOne ControlKind as DefaultKind
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Presentable_has_DefaultKind",
    "Presentable", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ViewModelDescriptorEfImpl),
    "DefaultKind", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ControlKindEfImpl)
    )]


	/*
    Relation: FK_Presentable_has_DefaultViewModelDescriptor
    A: ZeroOrMore ObjectClass as Presentable
    B: One ViewModelDescriptor as DefaultViewModelDescriptor
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Presentable_has_DefaultViewModelDescriptor",
    "Presentable", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClassEfImpl),
    "DefaultViewModelDescriptor", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ViewModelDescriptorEfImpl)
    )]


	/*
    Relation: FK_Presentable_may_has_DefaultPropViewModelDescriptor
    A: ZeroOrMore CompoundObject as Presentable
    B: ZeroOrOne ViewModelDescriptor as DefaultPropViewModelDescriptor
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Presentable_may_has_DefaultPropViewModelDescriptor",
    "Presentable", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.CompoundObjectEfImpl),
    "DefaultPropViewModelDescriptor", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ViewModelDescriptorEfImpl)
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
    "Projekt", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.ProjektEfImpl),
    "Auftraege", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.AuftragEfImpl)
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
    "Projekt", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.ProjektEfImpl),
    "Tasks", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.TaskEfImpl)
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
    "Projekt", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.ProjektEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
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
    "Projekt", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.ProjektEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_Projekte_haben_Mitarbeiter
    A: ZeroOrMore Projekt as Projekte
    B: ZeroOrMore Mitarbeiter as Mitarbeiter
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Projekte_haben_Mitarbeiter_A",
    "Projekte", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.ProjektEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Projekt_haben_Mitarbeiter_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Projekte_haben_Mitarbeiter_B",
    "Mitarbeiter", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.MitarbeiterEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Projekt_haben_Mitarbeiter_RelationEntryEfImpl)
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
    "Property", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.PropertyEfImpl),
    "DefaultValue", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DefaultPropertyValueEfImpl)
    )]


	/*
    Relation: FK_Property_Has_PropertyFilterConfiguration
    A: One Property as Property
    B: ZeroOrOne PropertyFilterConfiguration as PropertyFilterConfiguration
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Property_Has_PropertyFilterConfiguration",
    "Property", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.PropertyEfImpl),
    "PropertyFilterConfiguration", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.PropertyFilterConfigurationEfImpl)
    )]


	/*
    Relation: FK_Property_has_ValueModelDescriptor
    A: ZeroOrMore Property as Property
    B: One ViewModelDescriptor as ValueModelDescriptor
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Property_has_ValueModelDescriptor",
    "Property", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.PropertyEfImpl),
    "ValueModelDescriptor", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ViewModelDescriptorEfImpl)
    )]


	/*
    Relation: FK_Property_may_request_ControlKind
    A: ZeroOrMore Property as Property
    B: ZeroOrOne ControlKind as ControlKind
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Property_may_request_ControlKind",
    "Property", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.PropertyEfImpl),
    "ControlKind", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ControlKindEfImpl)
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
    "Property", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.PropertyEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
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
    "Property", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.PropertyEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_Relation_hasA_A
    A: One Relation as Relation
    B: One RelationEnd as A
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Relation_hasA_A",
    "Relation", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.RelationEfImpl),
    "A", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.RelationEndEfImpl)
    )]


	/*
    Relation: FK_Relation_hasB_B
    A: One Relation as Relation
    B: One RelationEnd as B
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Relation_hasB_B",
    "Relation", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.RelationEfImpl),
    "B", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.RelationEndEfImpl)
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
    "Relation", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.RelationEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
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
    "Relation", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.RelationEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_RelationEnd_has_Navigator
    A: ZeroOrOne RelationEnd as RelationEnd
    B: ZeroOrOne ObjectReferenceProperty as Navigator
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_RelationEnd_has_Navigator",
    "RelationEnd", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.RelationEndEfImpl),
    "Navigator", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectReferencePropertyEfImpl)
    )]


	/*
    Relation: FK_RelationEnd_has_Type
    A: ZeroOrMore RelationEnd as RelationEnd
    B: One ObjectClass as Type
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_RelationEnd_has_Type",
    "RelationEnd", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.RelationEndEfImpl),
    "Type", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClassEfImpl)
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
    "RelationEnd", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.RelationEndEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
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
    "RelationEnd", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.RelationEndEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_RoleMembership_resolves_Relations
    A: ZeroOrMore RoleMembership as RoleMembership
    B: ZeroOrMore Relation as Relations
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_RoleMembership_resolves_Relations_A",
    "RoleMembership", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.RoleMembershipEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.RoleMembership_resolves_Relation_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_RoleMembership_resolves_Relations_B",
    "Relations", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.RelationEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.RoleMembership_resolves_Relation_RelationEntryEfImpl)
    )]

	/*
    Relation: FK_Screen_modeled_by_ViewModelDescriptor
    A: ZeroOrMore NavigationEntry as Screen
    B: ZeroOrOne ViewModelDescriptor as ViewModelDescriptor
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Screen_modeled_by_ViewModelDescriptor",
    "Screen", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.NavigationEntryEfImpl),
    "ViewModelDescriptor", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ViewModelDescriptorEfImpl)
    )]


	/*
    Relation: FK_Search_has_RequestedEditorKind
    A: ZeroOrMore NavigationSearchScreen as Search
    B: ZeroOrOne ControlKind as RequestedEditorKind
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Search_has_RequestedEditorKind",
    "Search", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.NavigationSearchScreenEfImpl),
    "RequestedEditorKind", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ControlKindEfImpl)
    )]


	/*
    Relation: FK_Search_has_RequestedWorkspaceKind
    A: ZeroOrMore NavigationSearchScreen as Search
    B: ZeroOrOne ControlKind as RequestedWorkspaceKind
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Search_has_RequestedWorkspaceKind",
    "Search", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.NavigationSearchScreenEfImpl),
    "RequestedWorkspaceKind", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ControlKindEfImpl)
    )]


	/*
    Relation: FK_SearchScreen_of_Type
    A: ZeroOrMore NavigationSearchScreen as SearchScreen
    B: ZeroOrOne ObjectClass as Type
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_SearchScreen_of_Type",
    "SearchScreen", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.NavigationSearchScreenEfImpl),
    "Type", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClassEfImpl)
    )]


	/*
    Relation: FK_Sequence_has_Data
    A: One Sequence as Sequence
    B: ZeroOrOne SequenceData as Data
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Sequence_has_Data",
    "Sequence", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.SequenceEfImpl),
    "Data", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.SequenceDataEfImpl)
    )]


	/*
    Relation: FK_Sequence_has_Module
    A: ZeroOrMore Sequence as Sequence
    B: One Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Sequence_has_Module",
    "Sequence", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.SequenceEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ModuleEfImpl)
    )]


	/*
    Relation: FK_Sequence_was_ChangedBy
    A: ZeroOrMore Sequence as Sequence
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Sequence_was_ChangedBy",
    "Sequence", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.SequenceEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_Sequence_was_CreatedBy
    A: ZeroOrMore Sequence as Sequence
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Sequence_was_CreatedBy",
    "Sequence", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.SequenceEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_ServiceDescriptor_describes_a_TypeRef
    A: ZeroOrMore ServiceDescriptor as ServiceDescriptor
    B: One TypeRef as TypeRef
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ServiceDescriptor_describes_a_TypeRef",
    "ServiceDescriptor", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ServiceDescriptorEfImpl),
    "TypeRef", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRefEfImpl)
    )]


	/*
    Relation: FK_ServiceDescriptor_has_Module
    A: ZeroOrMore ServiceDescriptor as ServiceDescriptor
    B: One Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ServiceDescriptor_has_Module",
    "ServiceDescriptor", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ServiceDescriptorEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ModuleEfImpl)
    )]


	/*
    Relation: FK_ServiceDescriptor_was_ChangedBy
    A: ZeroOrMore ServiceDescriptor as ServiceDescriptor
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ServiceDescriptor_was_ChangedBy",
    "ServiceDescriptor", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ServiceDescriptorEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_ServiceDescriptor_was_CreatedBy
    A: ZeroOrMore ServiceDescriptor as ServiceDescriptor
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ServiceDescriptor_was_CreatedBy",
    "ServiceDescriptor", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ServiceDescriptorEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_SourceColumn_belongs_to_SourceTable
    A: ZeroOrMore SourceColumn as SourceColumn
    B: One SourceTable as SourceTable
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_SourceColumn_belongs_to_SourceTable",
    "SourceColumn", RelationshipMultiplicity.Many, typeof(ZBox.App.SchemaMigration.SourceColumnEfImpl),
    "SourceTable", RelationshipMultiplicity.ZeroOrOne, typeof(ZBox.App.SchemaMigration.SourceTableEfImpl)
    )]


	/*
    Relation: FK_SourceColumn_created_Property
    A: ZeroOrMore SourceColumn as SourceColumn
    B: ZeroOrMore Property as Property
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_SourceColumn_created_Property_A",
    "SourceColumn", RelationshipMultiplicity.ZeroOrOne, typeof(ZBox.App.SchemaMigration.SourceColumnEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(ZBox.App.SchemaMigration.SourceColumn_created_Property_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_SourceColumn_created_Property_B",
    "Property", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.PropertyEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(ZBox.App.SchemaMigration.SourceColumn_created_Property_RelationEntryEfImpl)
    )]

	/*
    Relation: FK_SourceColumn_may_have_EnumEntries
    A: One SourceColumn as SourceColumn
    B: ZeroOrMore SourceEnum as EnumEntries
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_SourceColumn_may_have_EnumEntries",
    "SourceColumn", RelationshipMultiplicity.ZeroOrOne, typeof(ZBox.App.SchemaMigration.SourceColumnEfImpl),
    "EnumEntries", RelationshipMultiplicity.Many, typeof(ZBox.App.SchemaMigration.SourceEnumEfImpl)
    )]


	/*
    Relation: FK_SourceColumn_was_ChangedBy
    A: ZeroOrMore SourceColumn as SourceColumn
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_SourceColumn_was_ChangedBy",
    "SourceColumn", RelationshipMultiplicity.Many, typeof(ZBox.App.SchemaMigration.SourceColumnEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_SourceColumn_was_CreatedBy
    A: ZeroOrMore SourceColumn as SourceColumn
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_SourceColumn_was_CreatedBy",
    "SourceColumn", RelationshipMultiplicity.Many, typeof(ZBox.App.SchemaMigration.SourceColumnEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_SourceEnum_mapps_to_DestinationValue
    A: ZeroOrMore SourceEnum as SourceEnum
    B: One EnumerationEntry as DestinationValue
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_SourceEnum_mapps_to_DestinationValue",
    "SourceEnum", RelationshipMultiplicity.Many, typeof(ZBox.App.SchemaMigration.SourceEnumEfImpl),
    "DestinationValue", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.EnumerationEntryEfImpl)
    )]


	/*
    Relation: FK_SourceEnum_was_ChangedBy
    A: ZeroOrMore SourceEnum as SourceEnum
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_SourceEnum_was_ChangedBy",
    "SourceEnum", RelationshipMultiplicity.Many, typeof(ZBox.App.SchemaMigration.SourceEnumEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_SourceEnum_was_CreatedBy
    A: ZeroOrMore SourceEnum as SourceEnum
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_SourceEnum_was_CreatedBy",
    "SourceEnum", RelationshipMultiplicity.Many, typeof(ZBox.App.SchemaMigration.SourceEnumEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_SourceTable_created_ObjectClass
    A: ZeroOrOne SourceTable as SourceTable
    B: ZeroOrOne ObjectClass as ObjectClass
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_SourceTable_created_ObjectClass",
    "SourceTable", RelationshipMultiplicity.ZeroOrOne, typeof(ZBox.App.SchemaMigration.SourceTableEfImpl),
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClassEfImpl)
    )]


	/*
    Relation: FK_SourceTable_was_ChangedBy
    A: ZeroOrMore SourceTable as SourceTable
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_SourceTable_was_ChangedBy",
    "SourceTable", RelationshipMultiplicity.Many, typeof(ZBox.App.SchemaMigration.SourceTableEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_SourceTable_was_CreatedBy
    A: ZeroOrMore SourceTable as SourceTable
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_SourceTable_was_CreatedBy",
    "SourceTable", RelationshipMultiplicity.Many, typeof(ZBox.App.SchemaMigration.SourceTableEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_SourceTables_are_contained_in_StagingDatabase
    A: ZeroOrMore SourceTable as SourceTables
    B: One StagingDatabase as StagingDatabase
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_SourceTables_are_contained_in_StagingDatabase",
    "SourceTables", RelationshipMultiplicity.Many, typeof(ZBox.App.SchemaMigration.SourceTableEfImpl),
    "StagingDatabase", RelationshipMultiplicity.ZeroOrOne, typeof(ZBox.App.SchemaMigration.StagingDatabaseEfImpl)
    )]


	/*
    Relation: FK_StagingDatabase_was_ChangedBy
    A: ZeroOrMore StagingDatabase as StagingDatabase
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_StagingDatabase_was_ChangedBy",
    "StagingDatabase", RelationshipMultiplicity.Many, typeof(ZBox.App.SchemaMigration.StagingDatabaseEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_StagingDatabase_was_CreatedBy
    A: ZeroOrMore StagingDatabase as StagingDatabase
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_StagingDatabase_was_CreatedBy",
    "StagingDatabase", RelationshipMultiplicity.Many, typeof(ZBox.App.SchemaMigration.StagingDatabaseEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_Student_füllt_aus_Testbogen
    A: ZeroOrMore TestStudent as Student
    B: ZeroOrMore Fragebogen as Testbogen
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Student_füllt_aus_Testbogen_A",
    "Student", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Test.TestStudentEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Test.TestStudent_füllt_aus_Fragebogen_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Student_füllt_aus_Testbogen_B",
    "Testbogen", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Test.FragebogenEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Test.TestStudent_füllt_aus_Fragebogen_RelationEntryEfImpl)
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
    "Task", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.TaskEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
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
    "Task", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.TaskEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
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
    "Template", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.TemplateEfImpl),
    "DisplayedTypeAssembly", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.AssemblyEfImpl)
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
    "Template", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.TemplateEfImpl),
    "VisualTree", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.VisualEfImpl)
    )]


	/*
    Relation: FK_Template_hasMenu_Menu
    A: ZeroOrMore Template as Template
    B: ZeroOrMore Visual as Menu
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Template_hasMenu_Menu_A",
    "Template", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.TemplateEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Template_hasMenu_Visual_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Template_hasMenu_Menu_B",
    "Menu", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.VisualEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Template_hasMenu_Visual_RelationEntryEfImpl)
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
    "TestObjClass", RelationshipMultiplicity.Many, typeof(Kistl.App.Test.TestObjClassEfImpl),
    "ObjectProp", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.KundeEfImpl)
    )]


	/*
    Relation: FK_TypeRef_has_Assembly
    A: ZeroOrMore TypeRef as TypeRef
    B: One Assembly as Assembly
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_TypeRef_has_Assembly",
    "TypeRef", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.TypeRefEfImpl),
    "Assembly", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.AssemblyEfImpl)
    )]


	/*
    Relation: FK_TypeRef_hasGenericArguments_GenericArguments
    A: ZeroOrMore TypeRef as TypeRef
    B: ZeroOrMore TypeRef as GenericArguments
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_TypeRef_hasGenericArguments_GenericArguments_A",
    "TypeRef", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRefEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.TypeRef_hasGenericArguments_TypeRef_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_TypeRef_hasGenericArguments_GenericArguments_B",
    "GenericArguments", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRefEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.TypeRef_hasGenericArguments_TypeRef_RelationEntryEfImpl)
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
    "TypeRef", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.TypeRefEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
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
    "TypeRef", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.TypeRefEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_UniqueContraints_ensures_unique_on_Properties
    A: ZeroOrMore IndexConstraint as UniqueContraints
    B: ZeroOrMore Property as Properties
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_UniqueContraints_ensures_unique_on_Properties_A",
    "UniqueContraints", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.IndexConstraintEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.IndexConstraint_ensures_unique_on_Property_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_UniqueContraints_ensures_unique_on_Properties_B",
    "Properties", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.PropertyEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Base.IndexConstraint_ensures_unique_on_Property_RelationEntryEfImpl)
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
    "View", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ViewDescriptorEfImpl),
    "ControlRef", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRefEfImpl)
    )]


	/*
    Relation: FK_ViewDescriptor_has_Module
    A: ZeroOrMore ViewDescriptor as ViewDescriptor
    B: One Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ViewDescriptor_has_Module",
    "ViewDescriptor", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ViewDescriptorEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ModuleEfImpl)
    )]


	/*
    Relation: FK_ViewDescriptor_is_a_ControlKind
    A: ZeroOrMore ViewDescriptor as ViewDescriptor
    B: ZeroOrOne ControlKind as ControlKind
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ViewDescriptor_is_a_ControlKind",
    "ViewDescriptor", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ViewDescriptorEfImpl),
    "ControlKind", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ControlKindEfImpl)
    )]


	/*
    Relation: FK_ViewDescriptor_supports_ViewModelTypeRefs
    A: ZeroOrMore ViewDescriptor as ViewDescriptor
    B: ZeroOrMore TypeRef as ViewModelTypeRefs
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_ViewDescriptor_supports_ViewModelTypeRefs_A",
    "ViewDescriptor", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ViewDescriptorEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ViewDescriptor_supports_TypeRef_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_ViewDescriptor_supports_ViewModelTypeRefs_B",
    "ViewModelTypeRefs", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.TypeRefEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ViewDescriptor_supports_TypeRef_RelationEntryEfImpl)
    )]

	/*
    Relation: FK_ViewModel_displayed_by_DefaultDisplayKind
    A: ZeroOrMore ViewModelDescriptor as ViewModel
    B: ZeroOrOne ControlKind as DefaultDisplayKind
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ViewModel_displayed_by_DefaultDisplayKind",
    "ViewModel", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ViewModelDescriptorEfImpl),
    "DefaultDisplayKind", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ControlKindEfImpl)
    )]


	/*
    Relation: FK_ViewModel_displayed_by_DefaultGridCellEditorKind
    A: ZeroOrMore ViewModelDescriptor as ViewModel
    B: ZeroOrOne ControlKind as DefaultGridCellEditorKind
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ViewModel_displayed_by_DefaultGridCellEditorKind",
    "ViewModel", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ViewModelDescriptorEfImpl),
    "DefaultGridCellEditorKind", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ControlKindEfImpl)
    )]


	/*
    Relation: FK_ViewModel_displayed_by_DefaultGridDisplayKind
    A: ZeroOrMore ViewModelDescriptor as ViewModel
    B: ZeroOrOne ControlKind as DefaultGridDisplayKind
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ViewModel_displayed_by_DefaultGridDisplayKind",
    "ViewModel", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ViewModelDescriptorEfImpl),
    "DefaultGridDisplayKind", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ControlKindEfImpl)
    )]


	/*
    Relation: FK_ViewModelDescriptor_displayedInGridBy_DefaultGridCellKind
    A: ZeroOrMore ViewModelDescriptor as ViewModelDescriptor
    B: ZeroOrOne ControlKind as DefaultGridCellKind
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ViewModelDescriptor_displayedInGridBy_DefaultGridCellKind",
    "ViewModelDescriptor", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ViewModelDescriptorEfImpl),
    "DefaultGridCellKind", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.ControlKindEfImpl)
    )]


	/*
    Relation: FK_ViewModelDescriptor_has_Module
    A: ZeroOrMore ViewModelDescriptor as ViewModelDescriptor
    B: One Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ViewModelDescriptor_has_Module",
    "ViewModelDescriptor", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.ViewModelDescriptorEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ModuleEfImpl)
    )]


	/*
    Relation: FK_Visual_contains_Children
    A: ZeroOrMore Visual as Visual
    B: ZeroOrMore Visual as Children
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Visual_contains_Children_A",
    "Visual", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.VisualEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual_contains_Visual_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Visual_contains_Children_B",
    "Children", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.VisualEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual_contains_Visual_RelationEntryEfImpl)
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
    "Visual", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.VisualEfImpl),
    "Method", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.MethodEfImpl)
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
    "Visual", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.VisualEfImpl),
    "Property", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.PropertyEfImpl)
    )]


	/*
    Relation: FK_Visual_hasContextMenu_ContextMenu
    A: ZeroOrMore Visual as Visual
    B: ZeroOrMore Visual as ContextMenu
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Visual_hasContextMenu_ContextMenu_A",
    "Visual", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.VisualEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual_hasContextMenu_Visual_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Visual_hasContextMenu_ContextMenu_B",
    "ContextMenu", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.GUI.VisualEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.GUI.Visual_hasContextMenu_Visual_RelationEntryEfImpl)
    )]


// object-value association
[assembly: EdmRelationship(
    "Model", "FK_Kunde_value_EMails",
    "Kunde", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.KundeEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Kunde_EMails_CollectionEntryEfImpl)
    )]



// object-struct association
[assembly: EdmRelationship(
    "Model", "FK_TestCustomObject_value_PhoneNumbersOther",
    "TestCustomObject", RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Test.TestCustomObjectEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Kistl.App.Test.TestCustomObject_PhoneNumbersOther_CollectionEntryEfImpl)
    )]


[assembly: global::System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_Auftraege_Rights", 
	"Auftrag", 
	global::System.Data.Metadata.Edm.RelationshipMultiplicity.One, 
	typeof(Kistl.App.Projekte.AuftragEfImpl), 
	"Auftrag_Rights", 
	global::System.Data.Metadata.Edm.RelationshipMultiplicity.Many, 
	typeof(Kistl.App.Projekte.Auftrag_RightsEfImpl))]
[assembly: global::System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_Projekte_Rights", 
	"Projekt", 
	global::System.Data.Metadata.Edm.RelationshipMultiplicity.One, 
	typeof(Kistl.App.Projekte.ProjektEfImpl), 
	"Projekt_Rights", 
	global::System.Data.Metadata.Edm.RelationshipMultiplicity.Many, 
	typeof(Kistl.App.Projekte.Projekt_RightsEfImpl))]
[assembly: global::System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_Tasks_Rights", 
	"Task", 
	global::System.Data.Metadata.Edm.RelationshipMultiplicity.One, 
	typeof(Kistl.App.Projekte.TaskEfImpl), 
	"Task_Rights", 
	global::System.Data.Metadata.Edm.RelationshipMultiplicity.Many, 
	typeof(Kistl.App.Projekte.Task_RightsEfImpl))]
