using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Xml;
using System.Xml.Serialization;

using Zetbox.API;
using Zetbox.DalProvider.Ef;


	/*
    Relation: FK_AbstractModuleMember_has_Module
    A: ZeroOrMore AbstractModuleMember as AbstractModuleMember
    B: One Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_AbstractModuleMember_has_Module",
    "AbstractModuleMember", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.AbstractModuleMemberEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ModuleEfImpl)
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
    "AbstractModuleMember", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.AbstractModuleMemberEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "AbstractModuleMember", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.AbstractModuleMemberEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "AccessControl", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.AccessControlEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ModuleEfImpl)
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
    "AccessControl", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.AccessControlEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "AccessControl", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.AccessControlEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_App_has_Icon
    A: ZeroOrMore Application as App
    B: ZeroOrOne Icon as Icon
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_App_has_Icon",
    "App", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.ApplicationEfImpl),
    "Icon", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.IconEfImpl)
    )]


	/*
    Relation: FK_Application_has_Module
    A: ZeroOrMore Application as Application
    B: One Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Application_has_Module",
    "Application", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.ApplicationEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ModuleEfImpl)
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
    "Application", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.ApplicationEfImpl),
    "RootScreen", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.NavigationScreenEfImpl)
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
    "Application", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.ApplicationEfImpl),
    "WorkspaceViewModel", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.ViewModelDescriptorEfImpl)
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
    "Application", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.ApplicationEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Application", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.ApplicationEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_ASide_connectsTo_BSide
    A: ZeroOrMore N_to_M_relations_A as ASide
    B: ZeroOrMore N_to_M_relations_B as BSide
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_ASide_connectsTo_BSide_A",
    "ASide", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Test.N_to_M_relations_AEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.ASide_connectsTo_BSide_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_ASide_connectsTo_BSide_B",
    "BSide", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Test.N_to_M_relations_BEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.ASide_connectsTo_BSide_RelationEntryEfImpl)
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
    "Assembly", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.AssemblyEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Assembly", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.AssemblyEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Auftrag", RelationshipMultiplicity.Many, typeof(Zetbox.App.Projekte.AuftragEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Auftrag", RelationshipMultiplicity.Many, typeof(Zetbox.App.Projekte.AuftragEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Auftrag", RelationshipMultiplicity.Many, typeof(Zetbox.App.Projekte.AuftragEfImpl),
    "Kunde", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Projekte.KundeEfImpl)
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
    "Auftrag", RelationshipMultiplicity.Many, typeof(Zetbox.App.Projekte.AuftragEfImpl),
    "Mitarbeiter", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Projekte.MitarbeiterEfImpl)
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
    "BaseObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ObjectClassEfImpl),
    "SubClasses", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.ObjectClassEfImpl)
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
    "BaseParameter", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.BaseParameterEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "BaseParameter", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.BaseParameterEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "BaseProperty", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.PropertyEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ModuleEfImpl)
    )]


	/*
    Relation: FK_BaseWorkSchedule_has_ChildWorkSchedules
    A: ZeroOrOne WorkSchedule as BaseWorkSchedule
    B: ZeroOrMore WorkSchedule as ChildWorkSchedules
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_BaseWorkSchedule_has_ChildWorkSchedules",
    "BaseWorkSchedule", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Calendar.WorkScheduleEfImpl),
    "ChildWorkSchedules", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.WorkScheduleEfImpl)
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
    "BoolProperty", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.BoolPropertyEfImpl),
    "FalseIcon", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.IconEfImpl)
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
    "BoolProperty", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.BoolPropertyEfImpl),
    "NullIcon", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.IconEfImpl)
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
    "BoolProperty", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.BoolPropertyEfImpl),
    "TrueIcon", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.IconEfImpl)
    )]


	/*
    Relation: FK_CalculatedReference_dependsOn_InputProperties
    A: ZeroOrMore CalculatedObjectReferenceProperty as CalculatedReference
    B: ZeroOrMore Property as InputProperties
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_CalculatedReference_dependsOn_InputProperties_A",
    "CalculatedReference", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.CalculatedObjectReferencePropertyEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.CalculatedReference_dependsOn_InputProperties_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_CalculatedReference_dependsOn_InputProperties_B",
    "InputProperties", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.PropertyEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.CalculatedReference_dependsOn_InputProperties_RelationEntryEfImpl)
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
    "CalculatedReference", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.CalculatedObjectReferencePropertyEfImpl),
    "ReferencedClass", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ObjectClassEfImpl)
    )]


	/*
    Relation: FK_Calendar_has_Owner
    A: ZeroOrMore CalendarBook as Calendar
    B: One Identity as Owner
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Calendar_has_Owner",
    "Calendar", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.CalendarBookEfImpl),
    "Owner", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_Calendar_shared_r_GroupReaders
    A: ZeroOrMore CalendarBook as Calendar
    B: ZeroOrMore Group as GroupReaders
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Calendar_shared_r_GroupReaders_A",
    "Calendar", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Calendar.CalendarBookEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.Calendar_shared_r_GroupReaders_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Calendar_shared_r_GroupReaders_B",
    "GroupReaders", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.GroupEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.Calendar_shared_r_GroupReaders_RelationEntryEfImpl)
    )]

	/*
    Relation: FK_Calendar_shared_r_Readers
    A: ZeroOrMore CalendarBook as Calendar
    B: ZeroOrMore Identity as Readers
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Calendar_shared_r_Readers_A",
    "Calendar", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Calendar.CalendarBookEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.Calendar_shared_r_Readers_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Calendar_shared_r_Readers_B",
    "Readers", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.Calendar_shared_r_Readers_RelationEntryEfImpl)
    )]

	/*
    Relation: FK_Calendar_shared_w_GroupWriters
    A: ZeroOrMore CalendarBook as Calendar
    B: ZeroOrMore Group as GroupWriters
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Calendar_shared_w_GroupWriters_A",
    "Calendar", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Calendar.CalendarBookEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.Calendar_shared_w_GroupWriters_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Calendar_shared_w_GroupWriters_B",
    "GroupWriters", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.GroupEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.Calendar_shared_w_GroupWriters_RelationEntryEfImpl)
    )]

	/*
    Relation: FK_Calendar_shared_w_Writers
    A: ZeroOrMore CalendarBook as Calendar
    B: ZeroOrMore Identity as Writers
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Calendar_shared_w_Writers_A",
    "Calendar", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Calendar.CalendarBookEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.Calendar_shared_w_Writers_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Calendar_shared_w_Writers_B",
    "Writers", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.Calendar_shared_w_Writers_RelationEntryEfImpl)
    )]

	/*
    Relation: FK_Calendar_was_ChangedBy
    A: ZeroOrMore CalendarBook as Calendar
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Calendar_was_ChangedBy",
    "Calendar", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.CalendarBookEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_Calendar_was_CreatedBy
    A: ZeroOrMore CalendarBook as Calendar
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Calendar_was_CreatedBy",
    "Calendar", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.CalendarBookEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_Child_allow_Identity
    A: ZeroOrMore SecurityTestChild as Child
    B: One Identity as Identity
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Child_allow_Identity",
    "Child", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.SecurityTestChildEfImpl),
    "Identity", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "ChildControlKinds", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.ControlKindEfImpl),
    "Parent", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.ControlKindEfImpl)
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
    "CompoundObjectProperty", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.CompoundObjectPropertyEfImpl),
    "CompoundObjectDefinition", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.CompoundObjectEfImpl)
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
    "Identity", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "ConstrainedProperty", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.PropertyEfImpl),
    "Constraints", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.ConstraintEfImpl)
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
    "Constraint", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.InstanceConstraintEfImpl),
    "Constrained", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.DataTypeEfImpl)
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
    "Constraint", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.ConstraintEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Constraint", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.ConstraintEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "ControlKind", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.ControlKindEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ModuleEfImpl)
    )]


	/*
    Relation: FK_CPObj_has_DefaultViewModelDescriptor
    A: ZeroOrMore CompoundObject as CPObj
    B: ZeroOrOne ViewModelDescriptor as DefaultViewModelDescriptor
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_CPObj_has_DefaultViewModelDescriptor",
    "CPObj", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.CompoundObjectEfImpl),
    "DefaultViewModelDescriptor", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.ViewModelDescriptorEfImpl)
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
    "CPParameter", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.CompoundObjectParameterEfImpl),
    "CompoundObject", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.CompoundObjectEfImpl)
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
    "DataType", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.DataTypeEfImpl),
    "DefaultIcon", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.IconEfImpl)
    )]


	/*
    Relation: FK_DataType_implements_ImplementedInterfaces
    A: ZeroOrMore DataType as DataType
    B: ZeroOrMore Interface as ImplementedInterfaces
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_DataType_implements_ImplementedInterfaces_A",
    "DataType", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.DataTypeEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.DataType_implements_ImplementedInterfaces_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_DataType_implements_ImplementedInterfaces_B",
    "ImplementedInterfaces", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.InterfaceEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.DataType_implements_ImplementedInterfaces_RelationEntryEfImpl)
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
    "DataType", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.DataTypeEfImpl),
    "ControlKind", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.ControlKindEfImpl)
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
    "DataType", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.DataTypeEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "DataType", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.DataTypeEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "DefaultPropertyValue", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.DefaultPropertyValueEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "DefaultPropertyValue", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.DefaultPropertyValueEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_Document_has_Revisions
    A: ZeroOrMore File as Document
    B: ZeroOrMore Blob as Revisions
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Document_has_Revisions_A",
    "Document", RelationshipMultiplicity.ZeroOrOne, typeof(at.dasz.DocumentManagement.FileEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(at.dasz.DocumentManagement.Document_has_Revisions_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Document_has_Revisions_B",
    "Revisions", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.BlobEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(at.dasz.DocumentManagement.Document_has_Revisions_RelationEntryEfImpl)
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
    "Document", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.BlobEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Document", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.BlobEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Ein_Fragebogen", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Test.FragebogenEfImpl),
    "gute_Antworten", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.AntwortEfImpl)
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
    "EnumDefaultValue", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.EnumDefaultValueEfImpl),
    "EnumValue", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.EnumerationEntryEfImpl)
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
    "Enumeration", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.EnumerationEfImpl),
    "EnumerationEntries", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.EnumerationEntryEfImpl)
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
    "EnumerationEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.EnumerationEntryEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "EnumerationEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.EnumerationEntryEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "EnumerationProperty", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.EnumerationPropertyEfImpl),
    "Enumeration", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.EnumerationEfImpl)
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
    "EnumParameter", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.EnumParameterEfImpl),
    "Enumeration", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.EnumerationEfImpl)
    )]


	/*
    Relation: FK_Event_of_Calendar
    A: ZeroOrMore Event as Event
    B: One CalendarBook as Calendar
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Event_of_Calendar",
    "Event", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.EventEfImpl),
    "Calendar", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Calendar.CalendarBookEfImpl)
    )]


	/*
    Relation: FK_Event_was_ChangedBy
    A: ZeroOrMore Event as Event
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Event_was_ChangedBy",
    "Event", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.EventEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_Event_was_CreatedBy
    A: ZeroOrMore Event as Event
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Event_was_CreatedBy",
    "Event", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.EventEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Blob", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.BlobEfImpl)
    )]


	/*
    Relation: FK_File_has_Excerpt
    A: One File as File
    B: ZeroOrOne Excerpt as Excerpt
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_File_has_Excerpt",
    "File", RelationshipMultiplicity.ZeroOrOne, typeof(at.dasz.DocumentManagement.FileEfImpl),
    "Excerpt", RelationshipMultiplicity.ZeroOrOne, typeof(at.dasz.DocumentManagement.ExcerptEfImpl)
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
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "FilterConfiguration", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.FilterConfigurationEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ModuleEfImpl)
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
    "FilterConfiguration", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.FilterConfigurationEfImpl),
    "RequestedKind", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.ControlKindEfImpl)
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
    "FilterConfiguration", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.FilterConfigurationEfImpl),
    "ViewModelDescriptor", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.ViewModelDescriptorEfImpl)
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
    "FilterConfiguration", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.FilterConfigurationEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "FilterConfiguration", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.FilterConfigurationEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "FK_Column", RelationshipMultiplicity.Many, typeof(Zetbox.App.SchemaMigration.SourceColumnEfImpl),
    "PK_Column", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.SchemaMigration.SourceColumnEfImpl)
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
    "Group", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.GroupEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ModuleEfImpl)
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
    "GroupMembership", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.GroupMembershipEfImpl),
    "Group", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.GroupEfImpl)
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
    "Icon", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.IconEfImpl),
    "Blob", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.BlobEfImpl)
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
    "Icon", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.IconEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ModuleEfImpl)
    )]


	/*
    Relation: FK_Identities_memberOf_Groups
    A: ZeroOrMore Identity as Identities
    B: ZeroOrMore Group as Groups
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Identities_memberOf_Groups_A",
    "Identities", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.Identities_memberOf_Groups_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Identities_memberOf_Groups_B",
    "Groups", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.GroupEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.Identities_memberOf_Groups_RelationEntryEfImpl)
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
    "Kunde", RelationshipMultiplicity.Many, typeof(Zetbox.App.Projekte.KundeEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Kunde", RelationshipMultiplicity.Many, typeof(Zetbox.App.Projekte.KundeEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_License_was_ChangedBy
    A: ZeroOrMore License as License
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_License_was_ChangedBy",
    "License", RelationshipMultiplicity.Many, typeof(Zetbox.App.LicenseManagement.LicenseEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_License_was_CreatedBy
    A: ZeroOrMore License as License
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_License_was_CreatedBy",
    "License", RelationshipMultiplicity.Many, typeof(Zetbox.App.LicenseManagement.LicenseEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_LstCfg_has_Type
    A: ZeroOrMore SavedListConfiguration as LstCfg
    B: One ObjectClass as Type
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_LstCfg_has_Type",
    "LstCfg", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.SavedListConfigurationEfImpl),
    "Type", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ObjectClassEfImpl)
    )]


	/*
    Relation: FK_LstCfg_of_Owner
    A: ZeroOrMore SavedListConfiguration as LstCfg
    B: ZeroOrOne Identity as Owner
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_LstCfg_of_Owner",
    "LstCfg", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.SavedListConfigurationEfImpl),
    "Owner", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "MB_Lst_Role", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.MuhblahEfImpl),
    "TCO_Role", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Test.TestCustomObjectEfImpl)
    )]


	/*
    Relation: FK_MB_Many_Role_has_TCO_ManyList_Role
    A: ZeroOrMore Muhblah as MB_Many_Role
    B: ZeroOrMore TestCustomObject as TCO_ManyList_Role
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_MB_Many_Role_has_TCO_ManyList_Role_A",
    "MB_Many_Role", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Test.MuhblahEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.MB_Many_Role_has_TCO_ManyList_Role_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_MB_Many_Role_has_TCO_ManyList_Role_B",
    "TCO_ManyList_Role", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Test.TestCustomObjectEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.MB_Many_Role_has_TCO_ManyList_Role_RelationEntryEfImpl)
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
    "MB_One_Role", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Test.MuhblahEfImpl),
    "TCO_One_Role", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Test.TestCustomObjectEfImpl)
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
    "MB_Role", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Test.MuhblahEfImpl),
    "TCO_Lst_Role", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.TestCustomObjectEfImpl)
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
    "Method", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.MethodEfImpl),
    "Icon", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.IconEfImpl)
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
    "Method", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.MethodEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ModuleEfImpl)
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
    "Method", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.MethodEfImpl),
    "Parameter", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.BaseParameterEfImpl)
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
    "Method", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.MethodEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Method", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.MethodEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "MigrationProject", RelationshipMultiplicity.Many, typeof(Zetbox.App.SchemaMigration.MigrationProjectEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ModuleEfImpl)
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
    "MigrationProject", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.SchemaMigration.MigrationProjectEfImpl),
    "StagingDatabases", RelationshipMultiplicity.Many, typeof(Zetbox.App.SchemaMigration.StagingDatabaseEfImpl)
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
    "MigrationProject", RelationshipMultiplicity.Many, typeof(Zetbox.App.SchemaMigration.MigrationProjectEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "MigrationProject", RelationshipMultiplicity.Many, typeof(Zetbox.App.SchemaMigration.MigrationProjectEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Mitarbeiter", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Projekte.MitarbeiterEfImpl),
    "Identity", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Mitarbeiter", RelationshipMultiplicity.Many, typeof(Zetbox.App.Projekte.MitarbeiterEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Mitarbeiter", RelationshipMultiplicity.Many, typeof(Zetbox.App.Projekte.MitarbeiterEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ModuleEfImpl),
    "Assemblies", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.AssemblyEfImpl)
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
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ModuleEfImpl),
    "DataTypes", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.DataTypeEfImpl)
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
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ModuleEfImpl),
    "Relation", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.RelationEfImpl)
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
    "Module", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.ModuleEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Module", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.ModuleEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_NavEntry_have_RequestedKind
    A: ZeroOrMore NavigationEntry as NavEntry
    B: ZeroOrOne ControlKind as RequestedKind
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_NavEntry_have_RequestedKind",
    "NavEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.NavigationEntryEfImpl),
    "RequestedKind", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.ControlKindEfImpl)
    )]


	/*
    Relation: FK_NavigationScreen_accessed_by_Groups
    A: ZeroOrMore NavigationEntry as NavigationScreen
    B: ZeroOrMore Group as Groups
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_NavigationScreen_accessed_by_Groups_A",
    "NavigationScreen", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.NavigationEntryEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.NavigationScreen_accessed_by_Groups_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_NavigationScreen_accessed_by_Groups_B",
    "Groups", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.GroupEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.NavigationScreen_accessed_by_Groups_RelationEntryEfImpl)
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
    "NavigationScreen", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.NavigationEntryEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ModuleEfImpl)
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
    "NavigationScreen", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.NavigationEntryEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "NavigationScreen", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.NavigationEntryEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ObjectClassEfImpl),
    "AccessControlList", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.AccessControlEfImpl)
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
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ObjectClassEfImpl),
    "FilterConfigurations", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.ObjectClassFilterConfigurationEfImpl)
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
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.DataTypeEfImpl),
    "Methods", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.MethodEfImpl)
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
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.DataTypeEfImpl),
    "Properties", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.PropertyEfImpl)
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
    "ObjectParameter", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.ObjectReferenceParameterEfImpl),
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ObjectClassEfImpl)
    )]


	/*
    Relation: FK_ObjRefPlaceholderProp_ofType_ReferencedClass
    A: ZeroOrMore ObjectReferencePlaceholderProperty as ObjRefPlaceholderProp
    B: One ObjectClass as ReferencedClass
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_ObjRefPlaceholderProp_ofType_ReferencedClass",
    "ObjRefPlaceholderProp", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.ObjectReferencePlaceholderPropertyEfImpl),
    "ReferencedClass", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ObjectClassEfImpl)
    )]


	/*
    Relation: FK_ObjRefProp_shows_Methods
    A: ZeroOrMore ObjectReferenceProperty as ObjRefProp
    B: ZeroOrMore Method as Methods
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_ObjRefProp_shows_Methods_A",
    "ObjRefProp", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ObjectReferencePropertyEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.ObjRefProp_shows_Methods_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_ObjRefProp_shows_Methods_B",
    "Methods", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.MethodEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.ObjRefProp_shows_Methods_RelationEntryEfImpl)
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
    "OneEnd", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Test.OrderedOneEndEfImpl),
    "NEnds", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.OrderedNEndEfImpl)
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
    "OneSide", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Test.One_to_N_relations_OneEfImpl),
    "NSide", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.One_to_N_relations_NEfImpl)
    )]


	/*
    Relation: FK_OneSide_connectsTo_OrderedNSide
    A: ZeroOrOne One_to_N_relations_One as OneSide
    B: ZeroOrMore One_to_N_relations_OrderedN as OrderedNSide
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_OneSide_connectsTo_OrderedNSide",
    "OneSide", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Test.One_to_N_relations_OneEfImpl),
    "OrderedNSide", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.One_to_N_relations_OrderedNEfImpl)
    )]


	/*
    Relation: FK_Parent_has_Children
    A: ZeroOrOne MethodTest as Parent
    B: ZeroOrMore MethodTest as Children
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Parent_has_Children",
    "Parent", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Test.MethodTestEfImpl),
    "Children", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.MethodTestEfImpl)
    )]


	/*
    Relation: FK_Parent_has_Children2
    A: ZeroOrOne SecurityTestParent as Parent
    B: ZeroOrMore SecurityTestChild as Children2
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Parent_has_Children2",
    "Parent", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Test.SecurityTestParentEfImpl),
    "Children2", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.SecurityTestChildEfImpl)
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
    "Parent", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.NavigationEntryEfImpl),
    "Children", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.NavigationEntryEfImpl)
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
    "Parent", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Test.RequiredParentEfImpl),
    "Children", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.RequiredParentChildEfImpl)
    )]


	/*
    Relation: FK_Presentable_displayedBy_SecondaryControlKinds
    A: ZeroOrMore ViewModelDescriptor as Presentable
    B: ZeroOrMore ControlKind as SecondaryControlKinds
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Presentable_displayedBy_SecondaryControlKinds_A",
    "Presentable", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.ViewModelDescriptorEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.Presentable_displayedBy_SecondaryControlKinds_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Presentable_displayedBy_SecondaryControlKinds_B",
    "SecondaryControlKinds", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.ControlKindEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.Presentable_displayedBy_SecondaryControlKinds_RelationEntryEfImpl)
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
    "Presentable", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.ViewModelDescriptorEfImpl),
    "DefaultKind", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.ControlKindEfImpl)
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
    "Presentable", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.ObjectClassEfImpl),
    "DefaultViewModelDescriptor", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.ViewModelDescriptorEfImpl)
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
    "Presentable", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.CompoundObjectEfImpl),
    "DefaultPropViewModelDescriptor", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.ViewModelDescriptorEfImpl)
    )]


	/*
    Relation: FK_PrivateKey_was_ChangedBy
    A: ZeroOrMore PrivateKey as PrivateKey
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_PrivateKey_was_ChangedBy",
    "PrivateKey", RelationshipMultiplicity.Many, typeof(Zetbox.App.LicenseManagement.PrivateKeyEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_PrivateKey_was_CreatedBy
    A: ZeroOrMore PrivateKey as PrivateKey
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_PrivateKey_was_CreatedBy",
    "PrivateKey", RelationshipMultiplicity.Many, typeof(Zetbox.App.LicenseManagement.PrivateKeyEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Projekt", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Projekte.ProjektEfImpl),
    "Auftraege", RelationshipMultiplicity.Many, typeof(Zetbox.App.Projekte.AuftragEfImpl)
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
    "Projekt", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Projekte.ProjektEfImpl),
    "Tasks", RelationshipMultiplicity.Many, typeof(Zetbox.App.Projekte.TaskEfImpl)
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
    "Projekt", RelationshipMultiplicity.Many, typeof(Zetbox.App.Projekte.ProjektEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Projekt", RelationshipMultiplicity.Many, typeof(Zetbox.App.Projekte.ProjektEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_Projekte_haben_Mitarbeiter
    A: ZeroOrMore Projekt as Projekte
    B: ZeroOrMore Mitarbeiter as Mitarbeiter
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Projekte_haben_Mitarbeiter_A",
    "Projekte", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Projekte.ProjektEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Projekte.Projekte_haben_Mitarbeiter_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Projekte_haben_Mitarbeiter_B",
    "Mitarbeiter", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Projekte.MitarbeiterEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Projekte.Projekte_haben_Mitarbeiter_RelationEntryEfImpl)
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
    "Property", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.PropertyEfImpl),
    "DefaultValue", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.DefaultPropertyValueEfImpl)
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
    "Property", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.PropertyEfImpl),
    "PropertyFilterConfiguration", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.PropertyFilterConfigurationEfImpl)
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
    "Property", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.PropertyEfImpl),
    "ValueModelDescriptor", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.ViewModelDescriptorEfImpl)
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
    "Property", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.PropertyEfImpl),
    "ControlKind", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.ControlKindEfImpl)
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
    "Property", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.PropertyEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Property", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.PropertyEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Relation", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.RelationEfImpl),
    "A", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.RelationEndEfImpl)
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
    "Relation", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.RelationEfImpl),
    "B", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.RelationEndEfImpl)
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
    "Relation", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.RelationEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Relation", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.RelationEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "RelationEnd", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.RelationEndEfImpl),
    "Navigator", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ObjectReferencePropertyEfImpl)
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
    "RelationEnd", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.RelationEndEfImpl),
    "Type", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ObjectClassEfImpl)
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
    "RelationEnd", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.RelationEndEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "RelationEnd", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.RelationEndEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_RoleMembership_resolves_Relations
    A: ZeroOrMore RoleMembership as RoleMembership
    B: ZeroOrMore Relation as Relations
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_RoleMembership_resolves_Relations_A",
    "RoleMembership", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.RoleMembershipEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.RoleMembership_resolves_Relations_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_RoleMembership_resolves_Relations_B",
    "Relations", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.RelationEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.RoleMembership_resolves_Relations_RelationEntryEfImpl)
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
    "Screen", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.NavigationEntryEfImpl),
    "ViewModelDescriptor", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.ViewModelDescriptorEfImpl)
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
    "Search", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.NavigationSearchScreenEfImpl),
    "RequestedEditorKind", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.ControlKindEfImpl)
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
    "Search", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.NavigationSearchScreenEfImpl),
    "RequestedWorkspaceKind", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.ControlKindEfImpl)
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
    "SearchScreen", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.NavigationSearchScreenEfImpl),
    "Type", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ObjectClassEfImpl)
    )]


	/*
    Relation: FK_SecurityTestChild_was_ChangedBy
    A: ZeroOrMore SecurityTestChild as SecurityTestChild
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_SecurityTestChild_was_ChangedBy",
    "SecurityTestChild", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.SecurityTestChildEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_SecurityTestChild_was_CreatedBy
    A: ZeroOrMore SecurityTestChild as SecurityTestChild
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_SecurityTestChild_was_CreatedBy",
    "SecurityTestChild", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.SecurityTestChildEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Sequence", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.SequenceEfImpl),
    "Data", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.SequenceDataEfImpl)
    )]


	/*
    Relation: FK_Sequence_has_Module
    A: ZeroOrMore Sequence as Sequence
    B: ZeroOrOne Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Sequence_has_Module",
    "Sequence", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.SequenceEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ModuleEfImpl)
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
    "Sequence", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.SequenceEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Sequence", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.SequenceEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "ServiceDescriptor", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.ServiceDescriptorEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ModuleEfImpl)
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
    "ServiceDescriptor", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.ServiceDescriptorEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "ServiceDescriptor", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.ServiceDescriptorEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "SourceColumn", RelationshipMultiplicity.Many, typeof(Zetbox.App.SchemaMigration.SourceColumnEfImpl),
    "SourceTable", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.SchemaMigration.SourceTableEfImpl)
    )]


	/*
    Relation: FK_SourceColumn_created_Property
    A: ZeroOrMore SourceColumn as SourceColumn
    B: ZeroOrMore Property as Property
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_SourceColumn_created_Property_A",
    "SourceColumn", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.SchemaMigration.SourceColumnEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.SchemaMigration.SourceColumn_created_Property_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_SourceColumn_created_Property_B",
    "Property", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.PropertyEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.SchemaMigration.SourceColumn_created_Property_RelationEntryEfImpl)
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
    "SourceColumn", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.SchemaMigration.SourceColumnEfImpl),
    "EnumEntries", RelationshipMultiplicity.Many, typeof(Zetbox.App.SchemaMigration.SourceEnumEfImpl)
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
    "SourceColumn", RelationshipMultiplicity.Many, typeof(Zetbox.App.SchemaMigration.SourceColumnEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "SourceColumn", RelationshipMultiplicity.Many, typeof(Zetbox.App.SchemaMigration.SourceColumnEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "SourceEnum", RelationshipMultiplicity.Many, typeof(Zetbox.App.SchemaMigration.SourceEnumEfImpl),
    "DestinationValue", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.EnumerationEntryEfImpl)
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
    "SourceEnum", RelationshipMultiplicity.Many, typeof(Zetbox.App.SchemaMigration.SourceEnumEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "SourceEnum", RelationshipMultiplicity.Many, typeof(Zetbox.App.SchemaMigration.SourceEnumEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "SourceTable", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.SchemaMigration.SourceTableEfImpl),
    "ObjectClass", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ObjectClassEfImpl)
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
    "SourceTable", RelationshipMultiplicity.Many, typeof(Zetbox.App.SchemaMigration.SourceTableEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "SourceTable", RelationshipMultiplicity.Many, typeof(Zetbox.App.SchemaMigration.SourceTableEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "SourceTables", RelationshipMultiplicity.Many, typeof(Zetbox.App.SchemaMigration.SourceTableEfImpl),
    "StagingDatabase", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.SchemaMigration.StagingDatabaseEfImpl)
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
    "StagingDatabase", RelationshipMultiplicity.Many, typeof(Zetbox.App.SchemaMigration.StagingDatabaseEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "StagingDatabase", RelationshipMultiplicity.Many, typeof(Zetbox.App.SchemaMigration.StagingDatabaseEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_Student_füllt_aus_Testbogen
    A: ZeroOrMore TestStudent as Student
    B: ZeroOrMore Fragebogen as Testbogen
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Student_füllt_aus_Testbogen_A",
    "Student", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Test.TestStudentEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.Student_füllt_aus_Testbogen_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_Student_füllt_aus_Testbogen_B",
    "Testbogen", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Test.FragebogenEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.Student_füllt_aus_Testbogen_RelationEntryEfImpl)
    )]

	/*
    Relation: FK_SyncAccount_for_Calendar
    A: ZeroOrMore WorkScheduleSyncProvider as SyncAccount
    B: One CalendarBook as Calendar
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_SyncAccount_for_Calendar",
    "SyncAccount", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.WorkScheduleSyncProviderEfImpl),
    "Calendar", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Calendar.CalendarBookEfImpl)
    )]


	/*
    Relation: FK_SyncAccount_of_WorkSchedule
    A: ZeroOrMore WorkScheduleSyncProvider as SyncAccount
    B: One WorkSchedule as WorkSchedule
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_SyncAccount_of_WorkSchedule",
    "SyncAccount", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.WorkScheduleSyncProviderEfImpl),
    "WorkSchedule", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Calendar.WorkScheduleEfImpl)
    )]


	/*
    Relation: FK_SyncAccount_was_ChangedBy
    A: ZeroOrMore SyncProvider as SyncAccount
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_SyncAccount_was_ChangedBy",
    "SyncAccount", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.SyncProviderEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_SyncAccount_was_CreatedBy
    A: ZeroOrMore SyncProvider as SyncAccount
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_SyncAccount_was_CreatedBy",
    "SyncAccount", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.SyncProviderEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Task", RelationshipMultiplicity.Many, typeof(Zetbox.App.Projekte.TaskEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
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
    "Task", RelationshipMultiplicity.Many, typeof(Zetbox.App.Projekte.TaskEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_Test_of_Event
    A: ZeroOrMore EventTestObject as Test
    B: One Event as Event
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_Test_of_Event",
    "Test", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.EventTestObjectEfImpl),
    "Event", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Calendar.EventEfImpl)
    )]


	/*
    Relation: FK_TestObj_has_AnotherFile
    A: ZeroOrMore DocumentTestObject as TestObj
    B: ZeroOrOne File as AnotherFile
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_TestObj_has_AnotherFile",
    "TestObj", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.DocumentTestObjectEfImpl),
    "AnotherFile", RelationshipMultiplicity.ZeroOrOne, typeof(at.dasz.DocumentManagement.FileEfImpl)
    )]


	/*
    Relation: FK_TestObj_has_AnyFile
    A: ZeroOrMore DocumentTestObject as TestObj
    B: ZeroOrOne File as AnyFile
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_TestObj_has_AnyFile",
    "TestObj", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.DocumentTestObjectEfImpl),
    "AnyFile", RelationshipMultiplicity.ZeroOrOne, typeof(at.dasz.DocumentManagement.FileEfImpl)
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
    "TestObjClass", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.TestObjClassEfImpl),
    "ObjectProp", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Projekte.KundeEfImpl)
    )]


	/*
    Relation: FK_TPHBaseObj_was_ChangedBy
    A: ZeroOrMore TPHBaseObj as TPHBaseObj
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_TPHBaseObj_was_ChangedBy",
    "TPHBaseObj", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.TPHBaseObjEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_TPHBaseObj_was_CreatedBy
    A: ZeroOrMore TPHBaseObj as TPHBaseObj
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_TPHBaseObj_was_CreatedBy",
    "TPHBaseObj", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.TPHBaseObjEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_UniqueContraints_ensures_unique_on_Properties
    A: ZeroOrMore IndexConstraint as UniqueContraints
    B: ZeroOrMore Property as Properties
    Preferred Storage: Separate
	*/

// The association from A to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_UniqueContraints_ensures_unique_on_Properties_A",
    "UniqueContraints", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IndexConstraintEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.UniqueContraints_ensures_unique_on_Properties_RelationEntryEfImpl)
    )]
// The association from B to the CollectionEntry
[assembly: EdmRelationship("Model", "FK_UniqueContraints_ensures_unique_on_Properties_B",
    "Properties", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.PropertyEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Base.UniqueContraints_ensures_unique_on_Properties_RelationEntryEfImpl)
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
    "ViewDescriptor", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.ViewDescriptorEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ModuleEfImpl)
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
    "ViewDescriptor", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.ViewDescriptorEfImpl),
    "ControlKind", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.ControlKindEfImpl)
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
    "ViewModel", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.ViewModelDescriptorEfImpl),
    "DefaultDisplayKind", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.ControlKindEfImpl)
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
    "ViewModel", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.ViewModelDescriptorEfImpl),
    "DefaultGridCellEditorKind", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.ControlKindEfImpl)
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
    "ViewModel", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.ViewModelDescriptorEfImpl),
    "DefaultGridDisplayKind", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.ControlKindEfImpl)
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
    "ViewModelDescriptor", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.ViewModelDescriptorEfImpl),
    "DefaultGridCellKind", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.GUI.ControlKindEfImpl)
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
    "ViewModelDescriptor", RelationshipMultiplicity.Many, typeof(Zetbox.App.GUI.ViewModelDescriptorEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ModuleEfImpl)
    )]


	/*
    Relation: FK_WorkSchedule_has_Module
    A: ZeroOrMore WorkSchedule as WorkSchedule
    B: One Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_WorkSchedule_has_Module",
    "WorkSchedule", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.WorkScheduleEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ModuleEfImpl)
    )]


	/*
    Relation: FK_WorkSchedule_has_WorkScheduleRules
    A: One WorkSchedule as WorkSchedule
    B: ZeroOrMore WorkScheduleRule as WorkScheduleRules
    Preferred Storage: MergeIntoB
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_WorkSchedule_has_WorkScheduleRules",
    "WorkSchedule", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Calendar.WorkScheduleEfImpl),
    "WorkScheduleRules", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.WorkScheduleRuleEfImpl)
    )]


	/*
    Relation: FK_WorkSchedule_was_ChangedBy
    A: ZeroOrMore WorkSchedule as WorkSchedule
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_WorkSchedule_was_ChangedBy",
    "WorkSchedule", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.WorkScheduleEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_WorkSchedule_was_CreatedBy
    A: ZeroOrMore WorkSchedule as WorkSchedule
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_WorkSchedule_was_CreatedBy",
    "WorkSchedule", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.WorkScheduleEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_WorkScheduleRule_has_Module
    A: ZeroOrMore WorkScheduleRule as WorkScheduleRule
    B: One Module as Module
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_WorkScheduleRule_has_Module",
    "WorkScheduleRule", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.WorkScheduleRuleEfImpl),
    "Module", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.ModuleEfImpl)
    )]


	/*
    Relation: FK_WorkScheduleRule_was_ChangedBy
    A: ZeroOrMore WorkScheduleRule as WorkScheduleRule
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_WorkScheduleRule_was_ChangedBy",
    "WorkScheduleRule", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.WorkScheduleRuleEfImpl),
    "ChangedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
    )]


	/*
    Relation: FK_WorkScheduleRule_was_CreatedBy
    A: ZeroOrMore WorkScheduleRule as WorkScheduleRule
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
	*/

// basic association
[assembly: EdmRelationship(
    "Model", "FK_WorkScheduleRule_was_CreatedBy",
    "WorkScheduleRule", RelationshipMultiplicity.Many, typeof(Zetbox.App.Calendar.WorkScheduleRuleEfImpl),
    "CreatedBy", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Base.IdentityEfImpl)
    )]



// object-value association
[assembly: EdmRelationship(
    "Model", "FK_Kunde_value_EMails",
    "Kunde", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Projekte.KundeEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Projekte.Kunde_EMails_CollectionEntryEfImpl)
    )]


// object-value association
[assembly: EdmRelationship(
    "Model", "FK_Muhblah_value_StringCollection",
    "Muhblah", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Test.MuhblahEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.Muhblah_StringCollection_CollectionEntryEfImpl)
    )]



// object-struct association
[assembly: EdmRelationship(
    "Model", "FK_Projekt_value_AuditJournal",
    "Projekt", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Projekte.ProjektEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Projekte.Projekt_AuditJournal_CollectionEntryEfImpl)
    )]


// object-struct association
[assembly: EdmRelationship(
    "Model", "FK_TestCustomObject_value_PhoneNumbersOther",
    "TestCustomObject", RelationshipMultiplicity.ZeroOrOne, typeof(Zetbox.App.Test.TestCustomObjectEfImpl),
    "CollectionEntry", RelationshipMultiplicity.Many, typeof(Zetbox.App.Test.TestCustomObject_PhoneNumbersOther_CollectionEntryEfImpl)
    )]


[assembly: global::System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_Auftraege_Rights", 
	"Auftrag", 
	global::System.Data.Metadata.Edm.RelationshipMultiplicity.One, 
	typeof(Zetbox.App.Projekte.AuftragEfImpl), 
	"Auftrag_Rights", 
	global::System.Data.Metadata.Edm.RelationshipMultiplicity.Many, 
	typeof(Zetbox.App.Projekte.Auftrag_RightsEfImpl))]
[assembly: global::System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_CalendarBooks_Rights", 
	"CalendarBook", 
	global::System.Data.Metadata.Edm.RelationshipMultiplicity.One, 
	typeof(Zetbox.App.Calendar.CalendarBookEfImpl), 
	"CalendarBook_Rights", 
	global::System.Data.Metadata.Edm.RelationshipMultiplicity.Many, 
	typeof(Zetbox.App.Calendar.CalendarBook_RightsEfImpl))]
[assembly: global::System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_Events_Rights", 
	"Event", 
	global::System.Data.Metadata.Edm.RelationshipMultiplicity.One, 
	typeof(Zetbox.App.Calendar.EventEfImpl), 
	"Event_Rights", 
	global::System.Data.Metadata.Edm.RelationshipMultiplicity.Many, 
	typeof(Zetbox.App.Calendar.Event_RightsEfImpl))]
[assembly: global::System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_Projekte_Rights", 
	"Projekt", 
	global::System.Data.Metadata.Edm.RelationshipMultiplicity.One, 
	typeof(Zetbox.App.Projekte.ProjektEfImpl), 
	"Projekt_Rights", 
	global::System.Data.Metadata.Edm.RelationshipMultiplicity.Many, 
	typeof(Zetbox.App.Projekte.Projekt_RightsEfImpl))]
[assembly: global::System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_SecurityTestChildren_Rights", 
	"SecurityTestChild", 
	global::System.Data.Metadata.Edm.RelationshipMultiplicity.One, 
	typeof(Zetbox.App.Test.SecurityTestChildEfImpl), 
	"SecurityTestChild_Rights", 
	global::System.Data.Metadata.Edm.RelationshipMultiplicity.Many, 
	typeof(Zetbox.App.Test.SecurityTestChild_RightsEfImpl))]
[assembly: global::System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_Tasks_Rights", 
	"Task", 
	global::System.Data.Metadata.Edm.RelationshipMultiplicity.One, 
	typeof(Zetbox.App.Projekte.TaskEfImpl), 
	"Task_Rights", 
	global::System.Data.Metadata.Edm.RelationshipMultiplicity.Many, 
	typeof(Zetbox.App.Projekte.Task_RightsEfImpl))]
