/****** Object:  ForeignKey [FK_Assembly_Module_fk_Module]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Assembly_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[Assemblies]'))
ALTER TABLE [dbo].[Assemblies] DROP CONSTRAINT [FK_Assembly_Module_fk_Module]
GO
/****** Object:  ForeignKey [FK_Auftrag_Kunde_fk_Kunde]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Auftrag_Kunde_fk_Kunde]') AND parent_object_id = OBJECT_ID(N'[dbo].[Auftraege]'))
ALTER TABLE [dbo].[Auftraege] DROP CONSTRAINT [FK_Auftrag_Kunde_fk_Kunde]
GO
/****** Object:  ForeignKey [FK_Auftrag_Mitarbeiter_fk_Mitarbeiter]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Auftrag_Mitarbeiter_fk_Mitarbeiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[Auftraege]'))
ALTER TABLE [dbo].[Auftraege] DROP CONSTRAINT [FK_Auftrag_Mitarbeiter_fk_Mitarbeiter]
GO
/****** Object:  ForeignKey [FK_Auftrag_Projekt_fk_Projekt]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Auftrag_Projekt_fk_Projekt]') AND parent_object_id = OBJECT_ID(N'[dbo].[Auftraege]'))
ALTER TABLE [dbo].[Auftraege] DROP CONSTRAINT [FK_Auftrag_Projekt_fk_Projekt]
GO
/****** Object:  ForeignKey [FK_BackReferenceProperty_BaseProperty_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BackReferenceProperty_BaseProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[BackReferenceProperties]'))
ALTER TABLE [dbo].[BackReferenceProperties] DROP CONSTRAINT [FK_BackReferenceProperty_BaseProperty_ID]
GO
/****** Object:  ForeignKey [FK_BackReferenceProperty_ObjectReferenceProperty_fk_ReferenceProperty]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BackReferenceProperty_ObjectReferenceProperty_fk_ReferenceProperty]') AND parent_object_id = OBJECT_ID(N'[dbo].[BackReferenceProperties]'))
ALTER TABLE [dbo].[BackReferenceProperties] DROP CONSTRAINT [FK_BackReferenceProperty_ObjectReferenceProperty_fk_ReferenceProperty]
GO
/****** Object:  ForeignKey [FK_BaseParameter_Method_fk_Method]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BaseParameter_Method_fk_Method]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseParameters]'))
ALTER TABLE [dbo].[BaseParameters] DROP CONSTRAINT [FK_BaseParameter_Method_fk_Method]
GO
/****** Object:  ForeignKey [FK_BaseParameter_Module_fk_Module]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BaseParameter_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseParameters]'))
ALTER TABLE [dbo].[BaseParameters] DROP CONSTRAINT [FK_BaseParameter_Module_fk_Module]
GO
/****** Object:  ForeignKey [FK_BaseProperty_DataType_fk_ObjectClass]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BaseProperty_DataType_fk_ObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseProperties]'))
ALTER TABLE [dbo].[BaseProperties] DROP CONSTRAINT [FK_BaseProperty_DataType_fk_ObjectClass]
GO
/****** Object:  ForeignKey [FK_BaseProperty_Module_fk_Module]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BaseProperty_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseProperties]'))
ALTER TABLE [dbo].[BaseProperties] DROP CONSTRAINT [FK_BaseProperty_Module_fk_Module]
GO
/****** Object:  ForeignKey [FK_BaseProperty_ConstraintsCollectionEntry_BaseProperty_fk_BaseProperty]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BaseProperty_ConstraintsCollectionEntry_BaseProperty_fk_BaseProperty]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseProperties_ConstraintsCollection]'))
ALTER TABLE [dbo].[BaseProperties_ConstraintsCollection] DROP CONSTRAINT [FK_BaseProperty_ConstraintsCollectionEntry_BaseProperty_fk_BaseProperty]
GO
/****** Object:  ForeignKey [FK_BaseProperty_ConstraintsCollectionEntry_Constraint_fk_Constraints]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BaseProperty_ConstraintsCollectionEntry_Constraint_fk_Constraints]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseProperties_ConstraintsCollection]'))
ALTER TABLE [dbo].[BaseProperties_ConstraintsCollection] DROP CONSTRAINT [FK_BaseProperty_ConstraintsCollectionEntry_Constraint_fk_Constraints]
GO
/****** Object:  ForeignKey [FK_BoolParameter_BaseParameter_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BoolParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[BoolParameters]'))
ALTER TABLE [dbo].[BoolParameters] DROP CONSTRAINT [FK_BoolParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_BoolProperty_ValueTypeProperty_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BoolProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[BoolProperties]'))
ALTER TABLE [dbo].[BoolProperties] DROP CONSTRAINT [FK_BoolProperty_ValueTypeProperty_ID]
GO
/****** Object:  ForeignKey [FK_CLRObjectParameter_Assembly_fk_Assembly]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLRObjectParameter_Assembly_fk_Assembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLRObjectParameters]'))
ALTER TABLE [dbo].[CLRObjectParameters] DROP CONSTRAINT [FK_CLRObjectParameter_Assembly_fk_Assembly]
GO
/****** Object:  ForeignKey [FK_CLRObjectParameter_BaseParameter_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLRObjectParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLRObjectParameters]'))
ALTER TABLE [dbo].[CLRObjectParameters] DROP CONSTRAINT [FK_CLRObjectParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_Constraint_BaseProperty_fk_ConstrainedProperty]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Constraint_BaseProperty_fk_ConstrainedProperty]') AND parent_object_id = OBJECT_ID(N'[dbo].[Constraints]'))
ALTER TABLE [dbo].[Constraints] DROP CONSTRAINT [FK_Constraint_BaseProperty_fk_ConstrainedProperty]
GO
/****** Object:  ForeignKey [FK_ControlInfo_Assembly_fk_Assembly]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ControlInfo_Assembly_fk_Assembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[ControlInfos]'))
ALTER TABLE [dbo].[ControlInfos] DROP CONSTRAINT [FK_ControlInfo_Assembly_fk_Assembly]
GO
/****** Object:  ForeignKey [FK_DataType_Icon_fk_DefaultIcon]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DataType_Icon_fk_DefaultIcon]') AND parent_object_id = OBJECT_ID(N'[dbo].[DataTypes]'))
ALTER TABLE [dbo].[DataTypes] DROP CONSTRAINT [FK_DataType_Icon_fk_DefaultIcon]
GO
/****** Object:  ForeignKey [FK_DataType_Module_fk_Module]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DataType_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[DataTypes]'))
ALTER TABLE [dbo].[DataTypes] DROP CONSTRAINT [FK_DataType_Module_fk_Module]
GO
/****** Object:  ForeignKey [FK_DateTimeParameter_BaseParameter_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DateTimeParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[DateTimeParameters]'))
ALTER TABLE [dbo].[DateTimeParameters] DROP CONSTRAINT [FK_DateTimeParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_DateTimeProperty_ValueTypeProperty_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DateTimeProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[DateTimeProperties]'))
ALTER TABLE [dbo].[DateTimeProperties] DROP CONSTRAINT [FK_DateTimeProperty_ValueTypeProperty_ID]
GO
/****** Object:  ForeignKey [FK_DoubleParameter_BaseParameter_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoubleParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoubleParameters]'))
ALTER TABLE [dbo].[DoubleParameters] DROP CONSTRAINT [FK_DoubleParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_DoubleProperty_ValueTypeProperty_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoubleProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoubleProperties]'))
ALTER TABLE [dbo].[DoubleProperties] DROP CONSTRAINT [FK_DoubleProperty_ValueTypeProperty_ID]
GO
/****** Object:  ForeignKey [FK_EnumerationEntry_Enumeration_fk_Enumeration]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EnumerationEntry_Enumeration_fk_Enumeration]') AND parent_object_id = OBJECT_ID(N'[dbo].[EnumerationEntries]'))
ALTER TABLE [dbo].[EnumerationEntries] DROP CONSTRAINT [FK_EnumerationEntry_Enumeration_fk_Enumeration]
GO
/****** Object:  ForeignKey [FK_EnumerationProperty_Enumeration_fk_Enumeration]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EnumerationProperty_Enumeration_fk_Enumeration]') AND parent_object_id = OBJECT_ID(N'[dbo].[EnumerationProperties]'))
ALTER TABLE [dbo].[EnumerationProperties] DROP CONSTRAINT [FK_EnumerationProperty_Enumeration_fk_Enumeration]
GO
/****** Object:  ForeignKey [FK_EnumerationProperty_ValueTypeProperty_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EnumerationProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[EnumerationProperties]'))
ALTER TABLE [dbo].[EnumerationProperties] DROP CONSTRAINT [FK_EnumerationProperty_ValueTypeProperty_ID]
GO
/****** Object:  ForeignKey [FK_Enumeration_DataType_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Enumeration_DataType_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Enumerations]'))
ALTER TABLE [dbo].[Enumerations] DROP CONSTRAINT [FK_Enumeration_DataType_ID]
GO
/****** Object:  ForeignKey [FK_IntegerRangeConstraint_Constraint_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_IntegerRangeConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[IntegerRangeConstraints]'))
ALTER TABLE [dbo].[IntegerRangeConstraints] DROP CONSTRAINT [FK_IntegerRangeConstraint_Constraint_ID]
GO
/****** Object:  ForeignKey [FK_Interface_DataType_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Interface_DataType_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Interfaces]'))
ALTER TABLE [dbo].[Interfaces] DROP CONSTRAINT [FK_Interface_DataType_ID]
GO
/****** Object:  ForeignKey [FK_IntParameter_BaseParameter_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_IntParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[IntParameters]'))
ALTER TABLE [dbo].[IntParameters] DROP CONSTRAINT [FK_IntParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_IntProperty_ValueTypeProperty_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_IntProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[IntProperties]'))
ALTER TABLE [dbo].[IntProperties] DROP CONSTRAINT [FK_IntProperty_ValueTypeProperty_ID]
GO
/****** Object:  ForeignKey [FK_Kostenstelle_Zeitkonto_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Kostenstelle_Zeitkonto_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Kostenstellen]'))
ALTER TABLE [dbo].[Kostenstellen] DROP CONSTRAINT [FK_Kostenstelle_Zeitkonto_ID]
GO
/****** Object:  ForeignKey [FK_Kostentraeger_Projekt_fk_Projekt]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Kostentraeger_Projekt_fk_Projekt]') AND parent_object_id = OBJECT_ID(N'[dbo].[Kostentraeger]'))
ALTER TABLE [dbo].[Kostentraeger] DROP CONSTRAINT [FK_Kostentraeger_Projekt_fk_Projekt]
GO
/****** Object:  ForeignKey [FK_Kostentraeger_Zeitkonto_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Kostentraeger_Zeitkonto_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Kostentraeger]'))
ALTER TABLE [dbo].[Kostentraeger] DROP CONSTRAINT [FK_Kostentraeger_Zeitkonto_ID]
GO
/****** Object:  ForeignKey [FK_Kunde_EMailsCollectionEntry_Kunde_fk_Kunde]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Kunde_EMailsCollectionEntry_Kunde_fk_Kunde]') AND parent_object_id = OBJECT_ID(N'[dbo].[Kunden_EMailsCollection]'))
ALTER TABLE [dbo].[Kunden_EMailsCollection] DROP CONSTRAINT [FK_Kunde_EMailsCollectionEntry_Kunde_fk_Kunde]
GO
/****** Object:  ForeignKey [FK_MethodInvocationConstraint_Constraint_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocationConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocationConstraints]'))
ALTER TABLE [dbo].[MethodInvocationConstraints] DROP CONSTRAINT [FK_MethodInvocationConstraint_Constraint_ID]
GO
/****** Object:  ForeignKey [FK_MethodInvocation_Assembly_fk_Assembly]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocation_Assembly_fk_Assembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocations]'))
ALTER TABLE [dbo].[MethodInvocations] DROP CONSTRAINT [FK_MethodInvocation_Assembly_fk_Assembly]
GO
/****** Object:  ForeignKey [FK_MethodInvocation_DataType_fk_InvokeOnObjectClass]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocation_DataType_fk_InvokeOnObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocations]'))
ALTER TABLE [dbo].[MethodInvocations] DROP CONSTRAINT [FK_MethodInvocation_DataType_fk_InvokeOnObjectClass]
GO
/****** Object:  ForeignKey [FK_MethodInvocation_Method_fk_Method]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocation_Method_fk_Method]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocations]'))
ALTER TABLE [dbo].[MethodInvocations] DROP CONSTRAINT [FK_MethodInvocation_Method_fk_Method]
GO
/****** Object:  ForeignKey [FK_MethodInvocation_Module_fk_Module]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocation_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocations]'))
ALTER TABLE [dbo].[MethodInvocations] DROP CONSTRAINT [FK_MethodInvocation_Module_fk_Module]
GO
/****** Object:  ForeignKey [FK_Method_DataType_fk_ObjectClass]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Method_DataType_fk_ObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[Methods]'))
ALTER TABLE [dbo].[Methods] DROP CONSTRAINT [FK_Method_DataType_fk_ObjectClass]
GO
/****** Object:  ForeignKey [FK_Method_Module_fk_Module]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Method_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[Methods]'))
ALTER TABLE [dbo].[Methods] DROP CONSTRAINT [FK_Method_Module_fk_Module]
GO
/****** Object:  ForeignKey [FK_NotNullableConstraint_Constraint_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NotNullableConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[NotNullableConstraints]'))
ALTER TABLE [dbo].[NotNullableConstraints] DROP CONSTRAINT [FK_NotNullableConstraint_Constraint_ID]
GO
/****** Object:  ForeignKey [FK_ObjectClass_DataType_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_DataType_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses]'))
ALTER TABLE [dbo].[ObjectClasses] DROP CONSTRAINT [FK_ObjectClass_DataType_ID]
GO
/****** Object:  ForeignKey [FK_ObjectClass_ObjectClass_fk_BaseObjectClass]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_ObjectClass_fk_BaseObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses]'))
ALTER TABLE [dbo].[ObjectClasses] DROP CONSTRAINT [FK_ObjectClass_ObjectClass_fk_BaseObjectClass]
GO
/****** Object:  ForeignKey [FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_fk_ImplementsInterfaces]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_fk_ImplementsInterfaces]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses_ImplementsInterfacesCollection]'))
ALTER TABLE [dbo].[ObjectClasses_ImplementsInterfacesCollection] DROP CONSTRAINT [FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_fk_ImplementsInterfaces]
GO
/****** Object:  ForeignKey [FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_ObjectClass]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_ObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses_ImplementsInterfacesCollection]'))
ALTER TABLE [dbo].[ObjectClasses_ImplementsInterfacesCollection] DROP CONSTRAINT [FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_ObjectClass]
GO
/****** Object:  ForeignKey [FK_ObjectParameter_BaseParameter_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectParameters]'))
ALTER TABLE [dbo].[ObjectParameters] DROP CONSTRAINT [FK_ObjectParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_ObjectParameter_DataType_fk_DataType]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectParameter_DataType_fk_DataType]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectParameters]'))
ALTER TABLE [dbo].[ObjectParameters] DROP CONSTRAINT [FK_ObjectParameter_DataType_fk_DataType]
GO
/****** Object:  ForeignKey [FK_ObjectReferenceProperty_ObjectClass_fk_ReferenceObjectClass]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectReferenceProperty_ObjectClass_fk_ReferenceObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectReferenceProperties]'))
ALTER TABLE [dbo].[ObjectReferenceProperties] DROP CONSTRAINT [FK_ObjectReferenceProperty_ObjectClass_fk_ReferenceObjectClass]
GO
/****** Object:  ForeignKey [FK_ObjectReferenceProperty_Property_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectReferenceProperty_Property_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectReferenceProperties]'))
ALTER TABLE [dbo].[ObjectReferenceProperties] DROP CONSTRAINT [FK_ObjectReferenceProperty_Property_ID]
GO
/****** Object:  ForeignKey [FK_PresenterInfo_Assembly_fk_DataAssembly]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PresenterInfo_Assembly_fk_DataAssembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[PresenterInfos]'))
ALTER TABLE [dbo].[PresenterInfos] DROP CONSTRAINT [FK_PresenterInfo_Assembly_fk_DataAssembly]
GO
/****** Object:  ForeignKey [FK_PresenterInfo_Assembly_fk_PresenterAssembly]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PresenterInfo_Assembly_fk_PresenterAssembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[PresenterInfos]'))
ALTER TABLE [dbo].[PresenterInfos] DROP CONSTRAINT [FK_PresenterInfo_Assembly_fk_PresenterAssembly]
GO
/****** Object:  ForeignKey [FK_Projekt_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projekt_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projekte_MitarbeiterCollection]'))
ALTER TABLE [dbo].[Projekte_MitarbeiterCollection] DROP CONSTRAINT [FK_Projekt_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]
GO
/****** Object:  ForeignKey [FK_Projekt_MitarbeiterCollectionEntry_Projekt_fk_Projekt]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projekt_MitarbeiterCollectionEntry_Projekt_fk_Projekt]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projekte_MitarbeiterCollection]'))
ALTER TABLE [dbo].[Projekte_MitarbeiterCollection] DROP CONSTRAINT [FK_Projekt_MitarbeiterCollectionEntry_Projekt_fk_Projekt]
GO
/****** Object:  ForeignKey [FK_Property_BaseProperty_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Property_BaseProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Properties]'))
ALTER TABLE [dbo].[Properties] DROP CONSTRAINT [FK_Property_BaseProperty_ID]
GO
/****** Object:  ForeignKey [FK_StringConstraint_Constraint_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StringConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StringConstraints]'))
ALTER TABLE [dbo].[StringConstraints] DROP CONSTRAINT [FK_StringConstraint_Constraint_ID]
GO
/****** Object:  ForeignKey [FK_StringParameter_BaseParameter_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StringParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StringParameters]'))
ALTER TABLE [dbo].[StringParameters] DROP CONSTRAINT [FK_StringParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_StringProperty_ValueTypeProperty_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StringProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StringProperties]'))
ALTER TABLE [dbo].[StringProperties] DROP CONSTRAINT [FK_StringProperty_ValueTypeProperty_ID]
GO
/****** Object:  ForeignKey [FK_StringRangeConstraint_Constraint_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StringRangeConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StringRangeConstraints]'))
ALTER TABLE [dbo].[StringRangeConstraints] DROP CONSTRAINT [FK_StringRangeConstraint_Constraint_ID]
GO
/****** Object:  ForeignKey [FK_StructProperty_Property_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StructProperty_Property_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StructProperties]'))
ALTER TABLE [dbo].[StructProperties] DROP CONSTRAINT [FK_StructProperty_Property_ID]
GO
/****** Object:  ForeignKey [FK_StructProperty_Struct_fk_StructDefinition]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StructProperty_Struct_fk_StructDefinition]') AND parent_object_id = OBJECT_ID(N'[dbo].[StructProperties]'))
ALTER TABLE [dbo].[StructProperties] DROP CONSTRAINT [FK_StructProperty_Struct_fk_StructDefinition]
GO
/****** Object:  ForeignKey [FK_Struct_DataType_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Struct_DataType_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Structs]'))
ALTER TABLE [dbo].[Structs] DROP CONSTRAINT [FK_Struct_DataType_ID]
GO
/****** Object:  ForeignKey [FK_Taetigkeit_Mitarbeiter_fk_Mitarbeiter]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Taetigkeit_Mitarbeiter_fk_Mitarbeiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[Taetigkeiten]'))
ALTER TABLE [dbo].[Taetigkeiten] DROP CONSTRAINT [FK_Taetigkeit_Mitarbeiter_fk_Mitarbeiter]
GO
/****** Object:  ForeignKey [FK_Taetigkeit_TaetigkeitsArt_fk_TaetigkeitsArt]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Taetigkeit_TaetigkeitsArt_fk_TaetigkeitsArt]') AND parent_object_id = OBJECT_ID(N'[dbo].[Taetigkeiten]'))
ALTER TABLE [dbo].[Taetigkeiten] DROP CONSTRAINT [FK_Taetigkeit_TaetigkeitsArt_fk_TaetigkeitsArt]
GO
/****** Object:  ForeignKey [FK_Taetigkeit_Zeitkonto_fk_Zeitkonto]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Taetigkeit_Zeitkonto_fk_Zeitkonto]') AND parent_object_id = OBJECT_ID(N'[dbo].[Taetigkeiten]'))
ALTER TABLE [dbo].[Taetigkeiten] DROP CONSTRAINT [FK_Taetigkeit_Zeitkonto_fk_Zeitkonto]
GO
/****** Object:  ForeignKey [FK_Task_Projekt_fk_Projekt]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Task_Projekt_fk_Projekt]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tasks]'))
ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_Task_Projekt_fk_Projekt]
GO
/****** Object:  ForeignKey [FK_Template_Assembly_fk_DisplayedTypeAssembly]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_Assembly_fk_DisplayedTypeAssembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[Templates]'))
ALTER TABLE [dbo].[Templates] DROP CONSTRAINT [FK_Template_Assembly_fk_DisplayedTypeAssembly]
GO
/****** Object:  ForeignKey [FK_Template_Visual_fk_VisualTree]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_Visual_fk_VisualTree]') AND parent_object_id = OBJECT_ID(N'[dbo].[Templates]'))
ALTER TABLE [dbo].[Templates] DROP CONSTRAINT [FK_Template_Visual_fk_VisualTree]
GO
/****** Object:  ForeignKey [FK_Template_MenuCollectionEntry_Template_fk_Template]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_MenuCollectionEntry_Template_fk_Template]') AND parent_object_id = OBJECT_ID(N'[dbo].[Templates_MenuCollection]'))
ALTER TABLE [dbo].[Templates_MenuCollection] DROP CONSTRAINT [FK_Template_MenuCollectionEntry_Template_fk_Template]
GO
/****** Object:  ForeignKey [FK_Template_MenuCollectionEntry_Visual_fk_Menu]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_MenuCollectionEntry_Visual_fk_Menu]') AND parent_object_id = OBJECT_ID(N'[dbo].[Templates_MenuCollection]'))
ALTER TABLE [dbo].[Templates_MenuCollection] DROP CONSTRAINT [FK_Template_MenuCollectionEntry_Visual_fk_Menu]
GO
/****** Object:  ForeignKey [FK_TestObjClass_Kunde_fk_ObjectProp]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TestObjClass_Kunde_fk_ObjectProp]') AND parent_object_id = OBJECT_ID(N'[dbo].[TestObjClasses]'))
ALTER TABLE [dbo].[TestObjClasses] DROP CONSTRAINT [FK_TestObjClass_Kunde_fk_ObjectProp]
GO
/****** Object:  ForeignKey [FK_ValueTypeProperty_Property_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ValueTypeProperty_Property_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ValueTypeProperties]'))
ALTER TABLE [dbo].[ValueTypeProperties] DROP CONSTRAINT [FK_ValueTypeProperty_Property_ID]
GO
/****** Object:  ForeignKey [FK_Visual_BaseProperty_fk_Property]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_BaseProperty_fk_Property]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals]'))
ALTER TABLE [dbo].[Visuals] DROP CONSTRAINT [FK_Visual_BaseProperty_fk_Property]
GO
/****** Object:  ForeignKey [FK_Visual_Method_fk_Method]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_Method_fk_Method]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals]'))
ALTER TABLE [dbo].[Visuals] DROP CONSTRAINT [FK_Visual_Method_fk_Method]
GO
/****** Object:  ForeignKey [FK_Visual_ChildrenCollectionEntry_Visual_fk_Children]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_ChildrenCollectionEntry_Visual_fk_Children]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_ChildrenCollection]'))
ALTER TABLE [dbo].[Visuals_ChildrenCollection] DROP CONSTRAINT [FK_Visual_ChildrenCollectionEntry_Visual_fk_Children]
GO
/****** Object:  ForeignKey [FK_Visual_ChildrenCollectionEntry_Visual_fk_Visual]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_ChildrenCollectionEntry_Visual_fk_Visual]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_ChildrenCollection]'))
ALTER TABLE [dbo].[Visuals_ChildrenCollection] DROP CONSTRAINT [FK_Visual_ChildrenCollectionEntry_Visual_fk_Visual]
GO
/****** Object:  ForeignKey [FK_Visual_ContextMenuCollectionEntry_Visual_fk_ContextMenu]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_ContextMenuCollectionEntry_Visual_fk_ContextMenu]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_ContextMenuCollection]'))
ALTER TABLE [dbo].[Visuals_ContextMenuCollection] DROP CONSTRAINT [FK_Visual_ContextMenuCollectionEntry_Visual_fk_ContextMenu]
GO
/****** Object:  ForeignKey [FK_Visual_ContextMenuCollectionEntry_Visual_fk_Visual]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_ContextMenuCollectionEntry_Visual_fk_Visual]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_ContextMenuCollection]'))
ALTER TABLE [dbo].[Visuals_ContextMenuCollection] DROP CONSTRAINT [FK_Visual_ContextMenuCollectionEntry_Visual_fk_Visual]
GO
/****** Object:  ForeignKey [FK_Visual_MenuTreeCollectionEntry_Visual_fk_MenuTree]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_MenuTreeCollectionEntry_Visual_fk_MenuTree]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_MenuTreeCollection]'))
ALTER TABLE [dbo].[Visuals_MenuTreeCollection] DROP CONSTRAINT [FK_Visual_MenuTreeCollectionEntry_Visual_fk_MenuTree]
GO
/****** Object:  ForeignKey [FK_Visual_MenuTreeCollectionEntry_Visual_fk_Visual]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_MenuTreeCollectionEntry_Visual_fk_Visual]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_MenuTreeCollection]'))
ALTER TABLE [dbo].[Visuals_MenuTreeCollection] DROP CONSTRAINT [FK_Visual_MenuTreeCollectionEntry_Visual_fk_Visual]
GO
/****** Object:  ForeignKey [FK_Zeitkonto_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Zeitkonto_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[Zeitkonten_MitarbeiterCollection]'))
ALTER TABLE [dbo].[Zeitkonten_MitarbeiterCollection] DROP CONSTRAINT [FK_Zeitkonto_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]
GO
/****** Object:  ForeignKey [FK_Zeitkonto_MitarbeiterCollectionEntry_Zeitkonto_fk_Zeitkonto]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Zeitkonto_MitarbeiterCollectionEntry_Zeitkonto_fk_Zeitkonto]') AND parent_object_id = OBJECT_ID(N'[dbo].[Zeitkonten_MitarbeiterCollection]'))
ALTER TABLE [dbo].[Zeitkonten_MitarbeiterCollection] DROP CONSTRAINT [FK_Zeitkonto_MitarbeiterCollectionEntry_Zeitkonto_fk_Zeitkonto]
GO
/****** Object:  StoredProcedure [dbo].[sp_DropBaseTables]    Script Date: 11/03/2008 15:18:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_DropBaseTables]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_DropBaseTables]
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckBaseTables]    Script Date: 11/03/2008 15:18:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CheckBaseTables]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_CheckBaseTables]
GO
/****** Object:  Table [dbo].[Templates_MenuCollection]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Templates_MenuCollection]') AND type in (N'U'))
DROP TABLE [dbo].[Templates_MenuCollection]
GO
/****** Object:  Table [dbo].[Visuals_ChildrenCollection]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Visuals_ChildrenCollection]') AND type in (N'U'))
DROP TABLE [dbo].[Visuals_ChildrenCollection]
GO
/****** Object:  Table [dbo].[Visuals_ContextMenuCollection]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Visuals_ContextMenuCollection]') AND type in (N'U'))
DROP TABLE [dbo].[Visuals_ContextMenuCollection]
GO
/****** Object:  Table [dbo].[Visuals_MenuTreeCollection]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Visuals_MenuTreeCollection]') AND type in (N'U'))
DROP TABLE [dbo].[Visuals_MenuTreeCollection]
GO
/****** Object:  Table [dbo].[StringConstraints]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StringConstraints]') AND type in (N'U'))
DROP TABLE [dbo].[StringConstraints]
GO
/****** Object:  Table [dbo].[IntegerRangeConstraints]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IntegerRangeConstraints]') AND type in (N'U'))
DROP TABLE [dbo].[IntegerRangeConstraints]
GO
/****** Object:  Table [dbo].[NotNullableConstraints]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NotNullableConstraints]') AND type in (N'U'))
DROP TABLE [dbo].[NotNullableConstraints]
GO
/****** Object:  Table [dbo].[MethodInvocationConstraints]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MethodInvocationConstraints]') AND type in (N'U'))
DROP TABLE [dbo].[MethodInvocationConstraints]
GO
/****** Object:  Table [dbo].[StringRangeConstraints]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StringRangeConstraints]') AND type in (N'U'))
DROP TABLE [dbo].[StringRangeConstraints]
GO
/****** Object:  Table [dbo].[ObjectClasses_ImplementsInterfacesCollection]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObjectClasses_ImplementsInterfacesCollection]') AND type in (N'U'))
DROP TABLE [dbo].[ObjectClasses_ImplementsInterfacesCollection]
GO
/****** Object:  Table [dbo].[DateTimeParameters]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DateTimeParameters]') AND type in (N'U'))
DROP TABLE [dbo].[DateTimeParameters]
GO
/****** Object:  Table [dbo].[BoolParameters]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BoolParameters]') AND type in (N'U'))
DROP TABLE [dbo].[BoolParameters]
GO
/****** Object:  Table [dbo].[DoubleParameters]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoubleParameters]') AND type in (N'U'))
DROP TABLE [dbo].[DoubleParameters]
GO
/****** Object:  Table [dbo].[IntParameters]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IntParameters]') AND type in (N'U'))
DROP TABLE [dbo].[IntParameters]
GO
/****** Object:  Table [dbo].[StringParameters]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StringParameters]') AND type in (N'U'))
DROP TABLE [dbo].[StringParameters]
GO
/****** Object:  Table [dbo].[Templates]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Templates]') AND type in (N'U'))
DROP TABLE [dbo].[Templates]
GO
/****** Object:  Table [dbo].[CLRObjectParameters]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLRObjectParameters]') AND type in (N'U'))
DROP TABLE [dbo].[CLRObjectParameters]
GO
/****** Object:  Table [dbo].[ControlInfos]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ControlInfos]') AND type in (N'U'))
DROP TABLE [dbo].[ControlInfos]
GO
/****** Object:  Table [dbo].[PresenterInfos]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PresenterInfos]') AND type in (N'U'))
DROP TABLE [dbo].[PresenterInfos]
GO
/****** Object:  Table [dbo].[EnumerationEntries]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EnumerationEntries]') AND type in (N'U'))
DROP TABLE [dbo].[EnumerationEntries]
GO
/****** Object:  Table [dbo].[BoolProperties]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BoolProperties]') AND type in (N'U'))
DROP TABLE [dbo].[BoolProperties]
GO
/****** Object:  Table [dbo].[DoubleProperties]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoubleProperties]') AND type in (N'U'))
DROP TABLE [dbo].[DoubleProperties]
GO
/****** Object:  Table [dbo].[IntProperties]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IntProperties]') AND type in (N'U'))
DROP TABLE [dbo].[IntProperties]
GO
/****** Object:  Table [dbo].[DateTimeProperties]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DateTimeProperties]') AND type in (N'U'))
DROP TABLE [dbo].[DateTimeProperties]
GO
/****** Object:  Table [dbo].[StringProperties]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StringProperties]') AND type in (N'U'))
DROP TABLE [dbo].[StringProperties]
GO
/****** Object:  Table [dbo].[EnumerationProperties]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EnumerationProperties]') AND type in (N'U'))
DROP TABLE [dbo].[EnumerationProperties]
GO
/****** Object:  Table [dbo].[ValueTypeProperties]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ValueTypeProperties]') AND type in (N'U'))
DROP TABLE [dbo].[ValueTypeProperties]
GO
/****** Object:  Table [dbo].[StructProperties]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StructProperties]') AND type in (N'U'))
DROP TABLE [dbo].[StructProperties]
GO
/****** Object:  Table [dbo].[Visuals]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Visuals]') AND type in (N'U'))
DROP TABLE [dbo].[Visuals]
GO
/****** Object:  Table [dbo].[BackReferenceProperties]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BackReferenceProperties]') AND type in (N'U'))
DROP TABLE [dbo].[BackReferenceProperties]
GO
/****** Object:  Table [dbo].[ObjectReferenceProperties]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObjectReferenceProperties]') AND type in (N'U'))
DROP TABLE [dbo].[ObjectReferenceProperties]
GO
/****** Object:  Table [dbo].[Properties]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Properties]') AND type in (N'U'))
DROP TABLE [dbo].[Properties]
GO
/****** Object:  Table [dbo].[BaseProperties_ConstraintsCollection]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BaseProperties_ConstraintsCollection]') AND type in (N'U'))
DROP TABLE [dbo].[BaseProperties_ConstraintsCollection]
GO
/****** Object:  Table [dbo].[Constraints]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Constraints]') AND type in (N'U'))
DROP TABLE [dbo].[Constraints]
GO
/****** Object:  Table [dbo].[ObjectParameters]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObjectParameters]') AND type in (N'U'))
DROP TABLE [dbo].[ObjectParameters]
GO
/****** Object:  Table [dbo].[Structs]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Structs]') AND type in (N'U'))
DROP TABLE [dbo].[Structs]
GO
/****** Object:  Table [dbo].[ObjectClasses]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObjectClasses]') AND type in (N'U'))
DROP TABLE [dbo].[ObjectClasses]
GO
/****** Object:  Table [dbo].[Interfaces]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Interfaces]') AND type in (N'U'))
DROP TABLE [dbo].[Interfaces]
GO
/****** Object:  Table [dbo].[Enumerations]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Enumerations]') AND type in (N'U'))
DROP TABLE [dbo].[Enumerations]
GO
/****** Object:  Table [dbo].[TestObjClasses]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TestObjClasses]') AND type in (N'U'))
DROP TABLE [dbo].[TestObjClasses]
GO
/****** Object:  Table [dbo].[Kunden_EMailsCollection]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Kunden_EMailsCollection]') AND type in (N'U'))
DROP TABLE [dbo].[Kunden_EMailsCollection]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tasks]') AND type in (N'U'))
DROP TABLE [dbo].[Tasks]
GO
/****** Object:  Table [dbo].[Auftraege]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auftraege]') AND type in (N'U'))
DROP TABLE [dbo].[Auftraege]
GO
/****** Object:  Table [dbo].[Projekte_MitarbeiterCollection]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Projekte_MitarbeiterCollection]') AND type in (N'U'))
DROP TABLE [dbo].[Projekte_MitarbeiterCollection]
GO
/****** Object:  Table [dbo].[Zeitkonten_MitarbeiterCollection]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Zeitkonten_MitarbeiterCollection]') AND type in (N'U'))
DROP TABLE [dbo].[Zeitkonten_MitarbeiterCollection]
GO
/****** Object:  Table [dbo].[Kostentraeger]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Kostentraeger]') AND type in (N'U'))
DROP TABLE [dbo].[Kostentraeger]
GO
/****** Object:  Table [dbo].[Kostenstellen]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Kostenstellen]') AND type in (N'U'))
DROP TABLE [dbo].[Kostenstellen]
GO
/****** Object:  Table [dbo].[BaseProperties]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BaseProperties]') AND type in (N'U'))
DROP TABLE [dbo].[BaseProperties]
GO
/****** Object:  Table [dbo].[BaseParameters]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BaseParameters]') AND type in (N'U'))
DROP TABLE [dbo].[BaseParameters]
GO
/****** Object:  Table [dbo].[MethodInvocations]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MethodInvocations]') AND type in (N'U'))
DROP TABLE [dbo].[MethodInvocations]
GO
/****** Object:  Table [dbo].[Methods]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Methods]') AND type in (N'U'))
DROP TABLE [dbo].[Methods]
GO
/****** Object:  Table [dbo].[DataTypes]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DataTypes]') AND type in (N'U'))
DROP TABLE [dbo].[DataTypes]
GO
/****** Object:  Table [dbo].[Assemblies]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Assemblies]') AND type in (N'U'))
DROP TABLE [dbo].[Assemblies]
GO
/****** Object:  Table [dbo].[Taetigkeiten]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Taetigkeiten]') AND type in (N'U'))
DROP TABLE [dbo].[Taetigkeiten]
GO
/****** Object:  Table [dbo].[TestCustomObjects]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TestCustomObjects]') AND type in (N'U'))
DROP TABLE [dbo].[TestCustomObjects]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_TableExists]    Script Date: 11/03/2008 15:18:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_TableExists]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_TableExists]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_ColumnExists]    Script Date: 11/03/2008 15:18:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_ColumnExists]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_ColumnExists]
GO
/****** Object:  StoredProcedure [dbo].[sp_DropObjectProperty]    Script Date: 11/03/2008 15:18:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_DropObjectProperty]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_DropObjectProperty]
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckObjectProperty]    Script Date: 11/03/2008 15:18:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CheckObjectProperty]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_CheckObjectProperty]
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckObjectClass]    Script Date: 11/03/2008 15:18:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CheckObjectClass]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_CheckObjectClass]
GO
/****** Object:  Table [dbo].[Icons]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Icons]') AND type in (N'U'))
DROP TABLE [dbo].[Icons]
GO
/****** Object:  Table [dbo].[Kunden]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Kunden]') AND type in (N'U'))
DROP TABLE [dbo].[Kunden]
GO
/****** Object:  Table [dbo].[Projekte]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Projekte]') AND type in (N'U'))
DROP TABLE [dbo].[Projekte]
GO
/****** Object:  Table [dbo].[Mitarbeiter]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Mitarbeiter]') AND type in (N'U'))
DROP TABLE [dbo].[Mitarbeiter]
GO
/****** Object:  Table [dbo].[Zeitkonten]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Zeitkonten]') AND type in (N'U'))
DROP TABLE [dbo].[Zeitkonten]
GO
/****** Object:  Table [dbo].[Modules]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Modules]') AND type in (N'U'))
DROP TABLE [dbo].[Modules]
GO
/****** Object:  Table [dbo].[ObjectForDeletedProperties]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObjectForDeletedProperties]') AND type in (N'U'))
DROP TABLE [dbo].[ObjectForDeletedProperties]
GO
/****** Object:  Table [dbo].[TaetigkeitsArten]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaetigkeitsArten]') AND type in (N'U'))
DROP TABLE [dbo].[TaetigkeitsArten]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_FKConstraintExists]    Script Date: 11/03/2008 15:18:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_FKConstraintExists]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_FKConstraintExists]
GO
/****** Object:  Table [dbo].[LastTests]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastTests]') AND type in (N'U'))
DROP TABLE [dbo].[LastTests]
GO
/****** Object:  Table [dbo].[AnotherTests]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AnotherTests]') AND type in (N'U'))
DROP TABLE [dbo].[AnotherTests]
GO
/****** Object:  Table [dbo].[Muhblas]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Muhblas]') AND type in (N'U'))
DROP TABLE [dbo].[Muhblas]
GO
/****** Object:  Default [DF__ObjectCla__Table__0F975522]    Script Date: 11/03/2008 15:18:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__ObjectCla__Table__0F975522]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__ObjectCla__Table__0F975522]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ObjectClasses] DROP CONSTRAINT [DF__ObjectCla__Table__0F975522]
END


End
GO
/****** Object:  Table [dbo].[Muhblas]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Muhblas]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Muhblas](
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Muhblas] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[AnotherTests]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AnotherTests]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AnotherTests](
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_AnotherTests] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[LastTests]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastTests]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LastTests](
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_LastTests] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_FKConstraintExists]    Script Date: 11/03/2008 15:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_FKConstraintExists]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE function [dbo].[fn_FKConstraintExists] (@fk_constraint nvarchar(500)) returns bit
begin
declare @result int
SELECT @result = count(*) FROM sys.objects 
	WHERE object_id = OBJECT_ID(@fk_constraint) AND type in (N''F'')
return convert(bit, @result)
end

' 
END
GO
/****** Object:  Table [dbo].[TaetigkeitsArten]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaetigkeitsArten]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TaetigkeitsArten](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) COLLATE Latin1_General_CI_AS NOT NULL,
 CONSTRAINT [PK_TaetigkeitsArten] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[TaetigkeitsArten] ON
INSERT [dbo].[TaetigkeitsArten] ([ID], [Name]) VALUES (1, N'Entwicklung')
INSERT [dbo].[TaetigkeitsArten] ([ID], [Name]) VALUES (2, N'Meeting')
INSERT [dbo].[TaetigkeitsArten] ([ID], [Name]) VALUES (3, N'Planung')
SET IDENTITY_INSERT [dbo].[TaetigkeitsArten] OFF
/****** Object:  Table [dbo].[ObjectForDeletedProperties]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObjectForDeletedProperties]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ObjectForDeletedProperties](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[test] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[DataType] [nvarchar](100) COLLATE Latin1_General_CI_AS NULL,
	[IsAssociation] [bit] NULL,
	[Namespace] [nvarchar](100) COLLATE Latin1_General_CI_AS NULL,
	[ReferenceObjectClassName] [nvarchar](200) COLLATE Latin1_General_CI_AS NULL,
	[ReferencePropertyName] [nvarchar](200) COLLATE Latin1_General_CI_AS NULL,
 CONSTRAINT [PK_ObjectForDeletedProperties] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Modules]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Modules]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Modules](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Namespace] [nvarchar](200) COLLATE Latin1_General_CI_AS NOT NULL,
	[ModuleName] [nvarchar](200) COLLATE Latin1_General_CI_AS NOT NULL,
 CONSTRAINT [PK_Module] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Modules] ON
INSERT [dbo].[Modules] ([ID], [Namespace], [ModuleName]) VALUES (1, N'Kistl.App.Base', N'KistlBase')
INSERT [dbo].[Modules] ([ID], [Namespace], [ModuleName]) VALUES (2, N'Kistl.App.Projekte', N'Projekte')
INSERT [dbo].[Modules] ([ID], [Namespace], [ModuleName]) VALUES (3, N'Kistl.App.Zeiterfassung', N'Zeiterfassung')
INSERT [dbo].[Modules] ([ID], [Namespace], [ModuleName]) VALUES (4, N'Kistl.App.GUI', N'GUI')
INSERT [dbo].[Modules] ([ID], [Namespace], [ModuleName]) VALUES (5, N'Kistl.App.Test', N'TestModule')
SET IDENTITY_INSERT [dbo].[Modules] OFF
/****** Object:  Table [dbo].[Zeitkonten]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Zeitkonten]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Zeitkonten](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Kontoname] [nvarchar](200) COLLATE Latin1_General_CI_AS NOT NULL,
	[MaxStunden] [float] NULL,
	[AktuelleStunden] [float] NULL,
 CONSTRAINT [PK_Zeitkonto] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Zeitkonten] ON
INSERT [dbo].[Zeitkonten] ([ID], [Kontoname], [MaxStunden], [AktuelleStunden]) VALUES (2, N'Kistlkostenträger', 58, 41.5)
INSERT [dbo].[Zeitkonten] ([ID], [Kontoname], [MaxStunden], [AktuelleStunden]) VALUES (3, N'Urlaube', NULL, 8)
INSERT [dbo].[Zeitkonten] ([ID], [Kontoname], [MaxStunden], [AktuelleStunden]) VALUES (4, N'Neuer Kostenträger für Ziviltechniker', 40, 0)
INSERT [dbo].[Zeitkonten] ([ID], [Kontoname], [MaxStunden], [AktuelleStunden]) VALUES (8, N'test', 100, 0)
INSERT [dbo].[Zeitkonten] ([ID], [Kontoname], [MaxStunden], [AktuelleStunden]) VALUES (9, N'test3333', 111, 0)
INSERT [dbo].[Zeitkonten] ([ID], [Kontoname], [MaxStunden], [AktuelleStunden]) VALUES (10, N'Test für''s rechnungswesen', 1, 0)
INSERT [dbo].[Zeitkonten] ([ID], [Kontoname], [MaxStunden], [AktuelleStunden]) VALUES (11, N'asdf', 1, 0)
SET IDENTITY_INSERT [dbo].[Zeitkonten] OFF
/****** Object:  Table [dbo].[Mitarbeiter]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Mitarbeiter]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Mitarbeiter](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) COLLATE Latin1_General_CI_AS NULL,
	[Geburtstag] [datetime] NULL,
	[SVNr] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL,
	[TelefonNummer] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
 CONSTRAINT [PK_Mitarbeiter] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Mitarbeiter] ON
INSERT [dbo].[Mitarbeiter] ([ID], [Name], [Geburtstag], [SVNr], [TelefonNummer]) VALUES (1, N'DI David Schmitt', NULL, N'asdf', NULL)
INSERT [dbo].[Mitarbeiter] ([ID], [Name], [Geburtstag], [SVNr], [TelefonNummer]) VALUES (2, N'Arthur Zaczek', CAST(0x00006F9700000000 AS DateTime), N'1234', N'')
INSERT [dbo].[Mitarbeiter] ([ID], [Name], [Geburtstag], [SVNr], [TelefonNummer]) VALUES (3, N'Susanne Dobler', NULL, NULL, NULL)
INSERT [dbo].[Mitarbeiter] ([ID], [Name], [Geburtstag], [SVNr], [TelefonNummer]) VALUES (4, N'Josef Pfleger', NULL, N'123', NULL)
INSERT [dbo].[Mitarbeiter] ([ID], [Name], [Geburtstag], [SVNr], [TelefonNummer]) VALUES (5, N'Peter Mayer', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Mitarbeiter] OFF
/****** Object:  Table [dbo].[Projekte]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Projekte]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Projekte](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) COLLATE Latin1_General_CI_AS NULL,
	[AufwandGes] [float] NULL,
	[Kundenname] [nvarchar](100) COLLATE Latin1_General_CI_AS NULL,
 CONSTRAINT [PK_Projekte] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Projekte] ON
INSERT [dbo].[Projekte] ([ID], [Name], [AufwandGes], [Kundenname]) VALUES (1, N'Kistl', 0, N'Wir selbst')
INSERT [dbo].[Projekte] ([ID], [Name], [AufwandGes], [Kundenname]) VALUES (2, N'Ziviltechniker', 40, N'test')
INSERT [dbo].[Projekte] ([ID], [Name], [AufwandGes], [Kundenname]) VALUES (3, N'WebCMS.net V1.0', 0, NULL)
INSERT [dbo].[Projekte] ([ID], [Name], [AufwandGes], [Kundenname]) VALUES (4, N'Rechnungswesen', 2, NULL)
INSERT [dbo].[Projekte] ([ID], [Name], [AufwandGes], [Kundenname]) VALUES (5, N'Neues Projekt test', 22, NULL)
INSERT [dbo].[Projekte] ([ID], [Name], [AufwandGes], [Kundenname]) VALUES (6, N'test_abc2', 20, NULL)
INSERT [dbo].[Projekte] ([ID], [Name], [AufwandGes], [Kundenname]) VALUES (7, N'Neues Objekt', 0, N'')
INSERT [dbo].[Projekte] ([ID], [Name], [AufwandGes], [Kundenname]) VALUES (12, N'Neues Projekt im Context', 103, NULL)
INSERT [dbo].[Projekte] ([ID], [Name], [AufwandGes], [Kundenname]) VALUES (17, N'Another Project', NULL, NULL)
INSERT [dbo].[Projekte] ([ID], [Name], [AufwandGes], [Kundenname]) VALUES (18, N'Muhblah', NULL, N'asdf')
INSERT [dbo].[Projekte] ([ID], [Name], [AufwandGes], [Kundenname]) VALUES (19, N'WebProjekt', 1000, N'test')
SET IDENTITY_INSERT [dbo].[Projekte] OFF
/****** Object:  Table [dbo].[Kunden]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Kunden]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Kunden](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Kundenname] [nvarchar](200) COLLATE Latin1_General_CI_AS NOT NULL,
	[Adresse] [nvarchar](200) COLLATE Latin1_General_CI_AS NULL,
	[PLZ] [nvarchar](10) COLLATE Latin1_General_CI_AS NOT NULL,
	[Ort] [nvarchar](100) COLLATE Latin1_General_CI_AS NULL,
	[Land] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
 CONSTRAINT [PK_Kunde] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Kunden] ON
INSERT [dbo].[Kunden] ([ID], [Kundenname], [Adresse], [PLZ], [Ort], [Land]) VALUES (1, N'Accent2', N'Kremserstraße 1', N'2000', N'Krems', N'AT')
INSERT [dbo].[Kunden] ([ID], [Kundenname], [Adresse], [PLZ], [Ort], [Land]) VALUES (2, N'Susanne Dobler', N'Leopoldgasse', N'3400', N'Klosterneuburg', N'AT')
INSERT [dbo].[Kunden] ([ID], [Kundenname], [Adresse], [PLZ], [Ort], [Land]) VALUES (3, N'Bernhard', N'Bernhardstraße 1', N'1190', N'Wien', N'AT')
SET IDENTITY_INSERT [dbo].[Kunden] OFF
/****** Object:  Table [dbo].[Icons]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Icons]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Icons](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IconFile] [nvarchar](200) COLLATE Latin1_General_CI_AS NOT NULL,
 CONSTRAINT [PK_Icon] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Icons] ON
INSERT [dbo].[Icons] ([ID], [IconFile]) VALUES (1, N'app.ico')
INSERT [dbo].[Icons] ([ID], [IconFile]) VALUES (2, N'Code_ClassCS.ico')
INSERT [dbo].[Icons] ([ID], [IconFile]) VALUES (3, N'VSProject_genericproject.ico')
INSERT [dbo].[Icons] ([ID], [IconFile]) VALUES (4, N'Resource_Bitmap.ico')
INSERT [dbo].[Icons] ([ID], [IconFile]) VALUES (5, N'user.ico')
INSERT [dbo].[Icons] ([ID], [IconFile]) VALUES (6, N'users.ico')
INSERT [dbo].[Icons] ([ID], [IconFile]) VALUES (7, N'propertiesORoptions.ico')
INSERT [dbo].[Icons] ([ID], [IconFile]) VALUES (8, N'UtilityText.ico')
INSERT [dbo].[Icons] ([ID], [IconFile]) VALUES (9, N'otheroptions.ico')
INSERT [dbo].[Icons] ([ID], [IconFile]) VALUES (10, N'cab.ico')
INSERT [dbo].[Icons] ([ID], [IconFile]) VALUES (11, N'Code_Component.ico')
INSERT [dbo].[Icons] ([ID], [IconFile]) VALUES (12, N'document.ico')
INSERT [dbo].[Icons] ([ID], [IconFile]) VALUES (13, N'idr_dll.ico')
SET IDENTITY_INSERT [dbo].[Icons] OFF
/****** Object:  StoredProcedure [dbo].[sp_CheckObjectClass]    Script Date: 11/03/2008 15:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CheckObjectClass]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sp_CheckObjectClass]
	(@namespace nvarchar(100)
	,@classname nvarchar(50)
	,@tablename nvarchar(50)
	)  
as
IF NOT EXISTS (select * from dbo.ObjectClasses where 
	[Namespace] = @namespace and [ClassName] = @classname)
BEGIN
	PRINT N''Inserting '' + @namespace + ''.'' + @classname + '' Class''
	INSERT INTO [dbo].[ObjectClasses]
			   ([ClassName]
				,[Namespace]
				,[TableName])
		 VALUES
			   (''ObjectClass''
				,@namespace
				,@tablename)
END
ELSE
BEGIN
	PRINT ''Updateing Kistl.App.Base.ObjectClass Class''
	UPDATE dbo.ObjectClasses SET 
		[TableName] = @tablename
	WHERE [Namespace] = @namespace and [ClassName] = @classname
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckObjectProperty]    Script Date: 11/03/2008 15:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CheckObjectProperty]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sp_CheckObjectProperty]
	(@namespace nvarchar(100)
	,@classname nvarchar(50)
	,@propertyname nvarchar(50)
	,@datatype nvarchar(150)
	,@IsList bit
	,@IsAssociation bit
	)
as
declare @fk_ObjClass int

select @fk_ObjClass = [ID] from dbo.ObjectClasses where 
	[Namespace] = @namespace and [ClassName] = @classname

IF NOT EXISTS (select * from dbo.ObjectProperties where 
	[fk_ObjectClass] = @fk_ObjClass and [PropertyName] = @propertyname)
BEGIN
	PRINT N''Inserting '' + @namespace + N''.'' + @classname + N''.'' + @propertyname + N'' Property''
	INSERT INTO [dbo].[ObjectProperties]
			([fk_ObjectClass], [PropertyName], [DataType], [IsList], [IsAssociation])
     VALUES (@fk_ObjClass, @propertyname, @datatype, @IsList, @IsAssociation)
END
ELSE
BEGIN
	PRINT N''Updateting '' + @namespace + N''.'' + @classname + N''.'' + @propertyname + N'' Property''
	UPDATE [dbo].[ObjectProperties]
			SET [DataType] = @datatype, [IsList] = @IsList, [IsAssociation] = @IsAssociation
     WHERE [fk_ObjectClass] = @fk_ObjClass and [PropertyName] = @propertyname
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DropObjectProperty]    Script Date: 11/03/2008 15:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_DropObjectProperty]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create procedure [dbo].[sp_DropObjectProperty]
	(@namespace nvarchar(100)
	,@classname nvarchar(50)
	,@propertyname nvarchar(50)
	)
as
declare @fk_ObjClass int

select @fk_ObjClass = [ID] from dbo.ObjectClasses where 
	[Namespace] = @namespace and [ClassName] = @classname

IF EXISTS (select * from dbo.ObjectProperties where 
	[fk_ObjectClass] = @fk_ObjClass and [PropertyName] = @propertyname)
BEGIN
	PRINT N''Dropping '' + @namespace + N''.'' + @classname + N''.'' + @propertyname + N'' Property''
	delete from [dbo].[ObjectProperties]
     WHERE [fk_ObjectClass] = @fk_ObjClass and [PropertyName] = @propertyname
END

' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_ColumnExists]    Script Date: 11/03/2008 15:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_ColumnExists]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE function [dbo].[fn_ColumnExists] (@table nvarchar(500), @column nvarchar(500)) 
returns bit
begin
declare @result int
SELECT @result = count(*) FROM sys.objects o inner join sys.columns c on c.object_id=o.object_id
	WHERE o.object_id = OBJECT_ID(@table) 
		AND o.type in (N''U'')
		AND c.Name = @column
return convert(bit, @result)
end

' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_TableExists]    Script Date: 11/03/2008 15:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_TableExists]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE function [dbo].[fn_TableExists] (@table nvarchar(500)) returns bit
begin
declare @result int
SELECT @result = count(*) FROM sys.objects 
	WHERE object_id = OBJECT_ID(@table) AND type in (N''U'')
return convert(bit, @result)
end

' 
END
GO
/****** Object:  Table [dbo].[TestCustomObjects]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TestCustomObjects]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TestCustomObjects](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PersonName] [nvarchar](200) COLLATE Latin1_General_CI_AS NOT NULL,
	[PhoneNumberMobile_Number] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[PhoneNumberMobile_AreaCode] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[PhoneNumberOffice_Number] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[PhoneNumberOffice_AreaCode] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[Birthday] [datetime] NULL,
 CONSTRAINT [PK_TestCustomObjects] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[TestCustomObjects] ON
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (1, N'TestPerson 1631444502', NULL, NULL, NULL, NULL, CAST(0x00009AF200F5684B AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (2, N'TestPerson 668408229', N'1017303345', N'1', N'1017303345', N'1', CAST(0x00009AF20102CAD5 AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (3, N'TestPerson 612671113', N'449166542', N'1', N'449166542', N'1', CAST(0x00009AF20103AFED AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (4, N'TestPerson 1303888886', N'1653762989', N'1', N'1653762989', N'1', CAST(0x00009AF201043B6E AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (5, N'TestPerson 1114544050', N'1908441677', N'1', N'1908441677', N'1', CAST(0x00009AF20104DD2F AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (6, N'TestPerson 2056499088', N'1951616029', N'1', N'1951616029', N'1', CAST(0x00009AF2010FA16D AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (7, N'TestPerson 1059938944', N'910906200', N'1', N'910906200', N'1', CAST(0x00009AF20111B514 AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (8, N'TestPerson 303235821', N'1563468126', N'1', N'1563468126', N'1', CAST(0x00009AF2011257B6 AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (9, N'TestPerson 1402003243', N'119241189', N'1', N'119241189', N'1', CAST(0x00009AF20112F1B0 AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (10, N'TestPerson 20921760', N'188615653', N'1', N'188615653', N'1', CAST(0x00009AF201184258 AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (11, N'TestPerson 965300374', N'1654044216', N'1', N'1654044216', N'1', CAST(0x00009AF201188739 AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (12, N'TestPerson 1175150861', N'1928070763', N'1', N'1928070763', N'1', CAST(0x00009B4601153898 AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (13, N'TestPerson 1698574166', N'1009005532', N'1', N'1009005532', N'1', CAST(0x00009B4601157431 AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (14, N'TestPerson 1791266096', N'959286373', N'1', N'959286373', N'1', CAST(0x00009B460126C2A2 AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (15, N'TestPerson 957226129', N'457327195', N'1', N'457327195', N'1', CAST(0x00009B4900916CAC AS DateTime))
SET IDENTITY_INSERT [dbo].[TestCustomObjects] OFF
/****** Object:  Table [dbo].[Taetigkeiten]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Taetigkeiten]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Taetigkeiten](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_Mitarbeiter] [int] NOT NULL,
	[fk_Zeitkonto] [int] NOT NULL,
	[Datum] [datetime] NOT NULL,
	[Dauer] [float] NOT NULL,
	[fk_TaetigkeitsArt] [int] NULL,
 CONSTRAINT [PK_Taetigkeit] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Taetigkeiten] ON
INSERT [dbo].[Taetigkeiten] ([ID], [fk_Mitarbeiter], [fk_Zeitkonto], [Datum], [Dauer], [fk_TaetigkeitsArt]) VALUES (1, 2, 2, CAST(0x00009A1E00000000 AS DateTime), 7.5, 1)
INSERT [dbo].[Taetigkeiten] ([ID], [fk_Mitarbeiter], [fk_Zeitkonto], [Datum], [Dauer], [fk_TaetigkeitsArt]) VALUES (2, 1, 2, CAST(0x00009A1F00000000 AS DateTime), 4, NULL)
INSERT [dbo].[Taetigkeiten] ([ID], [fk_Mitarbeiter], [fk_Zeitkonto], [Datum], [Dauer], [fk_TaetigkeitsArt]) VALUES (5, 2, 2, CAST(0x00009A1C00000000 AS DateTime), 1, NULL)
INSERT [dbo].[Taetigkeiten] ([ID], [fk_Mitarbeiter], [fk_Zeitkonto], [Datum], [Dauer], [fk_TaetigkeitsArt]) VALUES (6, 2, 2, CAST(0x00009A1F00000000 AS DateTime), 1, NULL)
INSERT [dbo].[Taetigkeiten] ([ID], [fk_Mitarbeiter], [fk_Zeitkonto], [Datum], [Dauer], [fk_TaetigkeitsArt]) VALUES (8, 2, 2, CAST(0x00009A1F00000000 AS DateTime), 1, NULL)
INSERT [dbo].[Taetigkeiten] ([ID], [fk_Mitarbeiter], [fk_Zeitkonto], [Datum], [Dauer], [fk_TaetigkeitsArt]) VALUES (9, 2, 2, CAST(0x00009A1F00000000 AS DateTime), 1.5, NULL)
INSERT [dbo].[Taetigkeiten] ([ID], [fk_Mitarbeiter], [fk_Zeitkonto], [Datum], [Dauer], [fk_TaetigkeitsArt]) VALUES (10, 2, 2, CAST(0x00009A1F00000000 AS DateTime), 2.5, NULL)
INSERT [dbo].[Taetigkeiten] ([ID], [fk_Mitarbeiter], [fk_Zeitkonto], [Datum], [Dauer], [fk_TaetigkeitsArt]) VALUES (11, 2, 2, CAST(0x00009A1600000000 AS DateTime), 2, NULL)
INSERT [dbo].[Taetigkeiten] ([ID], [fk_Mitarbeiter], [fk_Zeitkonto], [Datum], [Dauer], [fk_TaetigkeitsArt]) VALUES (12, 2, 2, CAST(0x00009A1600000000 AS DateTime), 3, NULL)
INSERT [dbo].[Taetigkeiten] ([ID], [fk_Mitarbeiter], [fk_Zeitkonto], [Datum], [Dauer], [fk_TaetigkeitsArt]) VALUES (13, 3, 2, CAST(0x00009A1600000000 AS DateTime), 3, NULL)
INSERT [dbo].[Taetigkeiten] ([ID], [fk_Mitarbeiter], [fk_Zeitkonto], [Datum], [Dauer], [fk_TaetigkeitsArt]) VALUES (14, 2, 2, CAST(0x00009A1600000000 AS DateTime), 10, 1)
INSERT [dbo].[Taetigkeiten] ([ID], [fk_Mitarbeiter], [fk_Zeitkonto], [Datum], [Dauer], [fk_TaetigkeitsArt]) VALUES (15, 2, 2, CAST(0x00009A5F00000000 AS DateTime), 2, 3)
INSERT [dbo].[Taetigkeiten] ([ID], [fk_Mitarbeiter], [fk_Zeitkonto], [Datum], [Dauer], [fk_TaetigkeitsArt]) VALUES (16, 3, 3, CAST(0x00009A1600000000 AS DateTime), 8, NULL)
INSERT [dbo].[Taetigkeiten] ([ID], [fk_Mitarbeiter], [fk_Zeitkonto], [Datum], [Dauer], [fk_TaetigkeitsArt]) VALUES (17, 1, 2, CAST(0x00009A6E00E56589 AS DateTime), 1, 1)
INSERT [dbo].[Taetigkeiten] ([ID], [fk_Mitarbeiter], [fk_Zeitkonto], [Datum], [Dauer], [fk_TaetigkeitsArt]) VALUES (18, 2, 2, CAST(0x00009AE000000000 AS DateTime), 2, 1)
SET IDENTITY_INSERT [dbo].[Taetigkeiten] OFF
/****** Object:  Table [dbo].[Assemblies]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Assemblies]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Assemblies](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_Module] [int] NOT NULL,
	[AssemblyName] [nvarchar](200) COLLATE Latin1_General_CI_AS NOT NULL,
	[IsClientAssembly] [bit] NOT NULL,
 CONSTRAINT [PK_Assembly] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Assemblies] ON
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly]) VALUES (1, 1, N'Kistl.App.Projekte.Client', 1)
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly]) VALUES (2, 1, N'Kistl.App.Projekte.Server', 0)
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly]) VALUES (3, 4, N'Kistl.Client.ASPNET.Toolkit, Version=1.0.0.0', 0)
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly]) VALUES (4, 4, N'Kistl.Client.WPF, Version=1.0.0.0', 0)
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly]) VALUES (13, 4, N'Kistl.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 0)
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly]) VALUES (14, 4, N'Kistl.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 0)
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly]) VALUES (15, 4, N'Kistl.API, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 0)
SET IDENTITY_INSERT [dbo].[Assemblies] OFF
/****** Object:  Table [dbo].[DataTypes]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DataTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DataTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](51) COLLATE Latin1_General_CI_AS NOT NULL,
	[fk_DefaultIcon] [int] NULL,
	[fk_Module] [int] NOT NULL,
 CONSTRAINT [PK_DataTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[DataTypes] ON
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (2, N'ObjectClass', 11, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (3, N'Projekt', 3, 2)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (4, N'Task', 7, 2)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (5, N'BaseProperty', 9, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (6, N'Mitarbeiter', 5, 2)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (7, N'Property', 9, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (8, N'ValueTypeProperty', 9, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (9, N'StringProperty', 9, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (10, N'Method', 2, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (11, N'IntProperty', 9, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (12, N'BoolProperty', 9, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (13, N'DoubleProperty', 9, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (14, N'ObjectReferenceProperty', 9, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (15, N'DateTimeProperty', 9, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (16, N'BackReferenceProperty', 9, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (18, N'Module', 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (19, N'Auftrag', 8, 2)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (20, N'Zeitkonto', 12, 3)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (21, N'Kostenstelle', 10, 3)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (23, N'Kostentraeger', 3, 3)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (25, N'Taetigkeit', 7, 3)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (26, N'Kunde', 6, 2)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (27, N'Icon', 4, 4)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (29, N'Assembly', 13, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (30, N'MethodInvocation', 2, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (31, N'TaetigkeitsArt', 8, 3)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (33, N'DataType', 11, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (36, N'BaseParameter', 12, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (37, N'StringParameter', 12, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (38, N'IntParameter', 12, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (39, N'DoubleParameter', 12, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (40, N'BoolParameter', 12, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (41, N'DateTimeParameter', 12, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (42, N'ObjectParameter', 12, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (43, N'CLRObjectParameter', 12, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (44, N'Interface', 11, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (45, N'Enumeration', 8, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (46, N'EnumerationEntry', 8, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (47, N'EnumerationProperty', 9, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (48, N'ITestInterface', 1, 5)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (50, N'TestEnum', NULL, 5)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (51, N'TestObjClass', 5, 5)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (52, N'IRenderer', 2, 4)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (53, N'Toolkit', 4, 4)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (54, N'ControlInfo', NULL, 4)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (55, N'VisualType', NULL, 4)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (58, N'TestCustomObject', NULL, 5)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (59, N'Muhblah', NULL, 5)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (60, N'AnotherTest', NULL, 5)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (61, N'LastTest', NULL, 5)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (62, N'Struct', 11, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (63, N'TestPhoneStruct', NULL, 5)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (64, N'StructProperty', 9, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (66, N'PresenterInfo', NULL, 4)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (67, N'Visual', NULL, 4)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (68, N'Template', NULL, 4)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (69, N'Constraint', NULL, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (70, N'NotNullableConstraint', NULL, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (71, N'IntegerRangeConstraint', NULL, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (73, N'StringRangeConstraint', NULL, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module]) VALUES (74, N'MethodInvocationConstraint', NULL, 1)
SET IDENTITY_INSERT [dbo].[DataTypes] OFF
/****** Object:  Table [dbo].[Methods]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Methods]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Methods](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_ObjectClass] [int] NOT NULL,
	[MethodName] [nvarchar](100) COLLATE Latin1_General_CI_AS NOT NULL,
	[fk_Module] [int] NOT NULL,
	[IsDisplayable] [bit] NOT NULL,
 CONSTRAINT [PK_Methods] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Methods] ON
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (1, 5, N'GetPropertyTypeString', 1, 1)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (3, 19, N'RechnungErstellen', 2, 1)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (4, 2, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (5, 2, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (6, 2, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (7, 3, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (8, 3, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (9, 3, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (10, 4, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (11, 4, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (12, 4, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (13, 5, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (14, 5, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (15, 5, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (16, 6, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (17, 6, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (18, 6, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (19, 10, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (20, 10, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (21, 10, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (22, 18, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (23, 18, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (24, 18, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (25, 19, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (26, 19, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (27, 19, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (28, 20, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (29, 20, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (30, 20, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (31, 25, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (32, 25, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (33, 25, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (34, 26, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (35, 26, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (36, 26, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (37, 27, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (38, 27, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (39, 27, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (40, 29, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (41, 29, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (42, 29, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (43, 30, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (44, 30, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (45, 30, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (46, 5, N'GetGUIRepresentation', 4, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (71, 31, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (72, 31, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (73, 31, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (74, 33, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (75, 33, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (76, 33, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (79, 36, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (80, 36, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (81, 36, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (82, 36, N'GetParameterTypeString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (83, 6, N'TestMethodForParameter', 2, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (84, 45, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (85, 45, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (86, 45, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (87, 46, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (88, 46, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (89, 46, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (90, 48, N'TestMethod', 5, 1)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (91, 51, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (92, 51, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (93, 51, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (95, 51, N'TestMethod', 5, 1)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (96, 52, N'ShowMessage', 4, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (97, 52, N'ShowObject', 4, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (98, 52, N'ChooseObject', 4, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (106, 58, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (107, 58, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (108, 58, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (109, 59, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (110, 59, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (111, 59, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (112, 60, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (113, 60, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (114, 60, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (115, 61, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (116, 61, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (117, 61, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (118, 5, N'GetPropertyType', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (120, 33, N'GetDataTypeString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (121, 33, N'GetDataType', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (123, 36, N'GetParameterType', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (124, 10, N'GetReturnParameter', 1, 1)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (125, 2, N'GetInheritedMethods', 1, 1)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (126, 66, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (127, 66, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (128, 66, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (129, 67, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (130, 67, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (131, 67, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (132, 68, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (133, 68, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (134, 68, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (135, 69, N'IsValid', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (136, 69, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (137, 69, N'PreSave', 1, 0)
GO
print 'Processed 100 total records'
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (138, 69, N'PostSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (139, 69, N'GetErrorText', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (141, 54, N'ToString', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (142, 54, N'PreSave', 1, 0)
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable]) VALUES (143, 54, N'PostSave', 1, 0)
SET IDENTITY_INSERT [dbo].[Methods] OFF
/****** Object:  Table [dbo].[MethodInvocations]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MethodInvocations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MethodInvocations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_Method] [int] NOT NULL,
	[fk_Assembly] [int] NOT NULL,
	[FullTypeName] [nvarchar](200) COLLATE Latin1_General_CI_AS NOT NULL,
	[MemberName] [nvarchar](200) COLLATE Latin1_General_CI_AS NOT NULL,
	[fk_Module] [int] NOT NULL,
	[fk_InvokeOnObjectClass] [int] NOT NULL,
 CONSTRAINT [PK_MethodInvocation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[MethodInvocations] ON
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (1, 8, 1, N'Kistl.App.Projekte.CustomClientActions_Projekte', N'OnToString_Projekt', 2, 3)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (2, 17, 1, N'Kistl.App.Projekte.CustomClientActions_Projekte', N'OnToString_Mitarbeiter', 2, 6)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (4, 11, 1, N'Kistl.App.Projekte.CustomClientActions_Projekte', N'OnToString_Task', 2, 4)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (5, 5, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnToString_DataType', 1, 33)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (6, 44, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnToString_MethodInvokation', 1, 30)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (8, 14, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnToString_BaseProperty', 1, 5)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (9, 20, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnToString_Method', 1, 10)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (10, 23, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnToString_Module', 1, 18)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (11, 26, 1, N'Kistl.App.Projekte.CustomClientActions_Projekte', N'OnToString_Auftrag', 2, 19)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (12, 29, 1, N'Kistl.App.Zeiterfassung.CustomClientActions_Zeiterfassung', N'OnToString_Zeitkonto', 3, 20)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (13, 32, 1, N'Kistl.App.Zeiterfassung.CustomClientActions_Zeiterfassung', N'OnToString_Taetigkeit', 3, 25)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (14, 35, 1, N'Kistl.App.Projekte.CustomClientActions_Projekte', N'OnToString_Kunde', 2, 26)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (15, 38, 1, N'Kistl.App.GUI.CustomClientActions_GUI', N'OnToString_Icon', 4, 27)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (16, 41, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnToString_Assembly', 1, 29)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (17, 14, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnToString_ObjectReferenceProperty', 1, 14)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (18, 14, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnToString_BackReferenceProperty', 1, 16)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (19, 3, 1, N'Kistl.App.Projekte.CustomClientActions_Projekte', N'OnRechnungErstellen_Auftrag', 2, 19)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (20, 1, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetPropertyTypeString_StringProperty', 1, 9)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (21, 1, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetPropertyTypeString_IntProperty', 1, 11)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (22, 1, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetPropertyTypeString_BoolProperty', 1, 12)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (23, 1, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetPropertyTypeString_DoubleProperty', 1, 13)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (24, 1, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetPropertyTypeString_DateTimeProperty', 1, 15)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (25, 1, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetPropertyTypeString_BaseProperty', 1, 5)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (26, 1, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetPropertyTypeString_ObjectReferenceProperty', 1, 14)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (27, 1, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetPropertyTypeString_BackReferenceProperty', 1, 16)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (28, 6, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnPreSave_ObjectClass', 1, 2)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (29, 9, 2, N'Kistl.App.Projekte.CustomServerActions_Projekte', N'OnPreSetObject_Projekt', 2, 3)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (30, 12, 2, N'Kistl.App.Projekte.CustomServerActions_Projekte', N'OnPreSetObject_Task', 2, 4)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (31, 1, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnGetPropertyTypeString_StringProperty', 1, 9)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (32, 1, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnGetPropertyTypeString_IntProperty', 1, 11)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (33, 1, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnGetPropertyTypeString_BoolProperty', 1, 12)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (34, 1, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnGetPropertyTypeString_DoubleProperty', 1, 13)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (35, 1, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnGetPropertyTypeString_DateTimeProperty', 1, 15)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (36, 1, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnGetPropertyTypeString_BaseProperty', 1, 5)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (37, 1, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnGetPropertyTypeString_ObjectReferenceProperty', 1, 14)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (38, 1, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnGetPropertyTypeString_BackReferenceProperty', 1, 16)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (40, 46, 1, N'Kistl.App.GUI.CustomClientActions_GUI', N'OnGetGUIRepresentation_BaseProperty', 4, 5)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (41, 46, 1, N'Kistl.App.GUI.CustomClientActions_GUI', N'OnGetGUIRepresentation_BoolProperty', 4, 12)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (42, 46, 1, N'Kistl.App.GUI.CustomClientActions_GUI', N'OnGetGUIRepresentation_ObjectReferenceProperty', 4, 14)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (43, 46, 1, N'Kistl.App.GUI.CustomClientActions_GUI', N'OnGetGUIRepresentation_DateTimeProperty', 4, 15)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (44, 71, 1, N'Kistl.App.Zeiterfassung.CustomClientActions_Zeiterfassung', N'OnToString_TaetigkeitsArt', 3, 31)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (45, 30, 2, N'Kistl.App.Zeiterfassung.CustomServerActions_Zeiterfassung', N'OnPreSave_Zeitkonto', 3, 20)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (46, 33, 2, N'Kistl.App.Zeiterfassung.CustomServerActions_Zeiterfassung', N'OnPreSave_Taetigkeit', 3, 25)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (47, 82, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnGetParameterTypeString_StringParameter', 1, 37)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (48, 82, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnGetParameterTypeString_IntParameter', 1, 38)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (49, 80, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnToString_BaseParameter', 1, 36)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (50, 82, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetParameterTypeString_StringParameter', 1, 37)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (51, 82, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetParameterTypeString_IntParameter', 1, 38)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (52, 82, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetParameterTypeString_DoubleParameter', 1, 39)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (53, 82, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnGetParameterTypeString_DoubleParameter', 1, 39)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (54, 82, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnGetParameterTypeString_DateTimeParameter', 1, 41)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (55, 82, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnGetParameterTypeString_BoolParameter', 1, 40)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (56, 82, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetParameterTypeString_BoolParameter', 1, 40)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (57, 82, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetParameterTypeString_DateTimeParameter', 1, 41)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (58, 82, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetParameterTypeString_ObjectParameter', 1, 42)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (59, 82, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnGetParameterTypeString_ObjectParameter', 1, 42)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (60, 82, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetParameterTypeString_CLRObjectParameter', 1, 43)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (61, 82, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnGetParameterTypeString_CLRObjectParameter', 1, 43)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (62, 79, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnPreSave_BaseParameter', 1, 36)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (63, 21, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnPreSave_Method', 1, 10)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (64, 1, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnGetPropertyTypeString_EnumerationProperty', 1, 47)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (65, 1, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetPropertyTypeString_EnumerationProperty', 5, 47)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (66, 85, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnToString_Enumeration', 1, 45)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (67, 88, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnToString_EnumerationEntry', 1, 46)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (70, 118, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetPropertyType_BaseProperty', 1, 5)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (71, 118, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnGetPropertyType_BaseProperty', 1, 5)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (72, 121, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetDataType_DataType', 1, 33)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (73, 121, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnGetDataType_DataType', 1, 33)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (74, 120, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnGetDataTypeString_DataType', 1, 33)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (75, 120, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetDataTypeString_DataType', 1, 33)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (76, 123, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnGetParameterType_BaseParameter', 1, 36)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (77, 123, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetParameterType_BaseParameter', 1, 36)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (78, 123, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetParameterType_ObjectParameter', 1, 42)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (79, 123, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnGetParameterType_ObjectParameter', 1, 42)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (80, 124, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetReturnParameter_Method', 1, 10)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (81, 125, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetInheritedMethods_ObjectClass', 1, 2)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (82, 1, 2, N'Kistl.App.Base.CustomServerActions_KistlBase', N'OnGetPropertyTypeString_StructProperty', 1, 64)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (83, 1, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetPropertyTypeString_StructProperty', 1, 64)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (87, 135, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnIsValid_Constraint', 1, 69)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (88, 135, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnIsValid_NotNullableConstraint', 1, 70)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (89, 139, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetErrorText_NotNullableConstraint', 1, 70)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (90, 136, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnToString_NotNullableConstraint', 1, 70)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (91, 136, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnToString_IntegerRangeConstraint', 1, 71)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (92, 139, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetErrorText_IntegerRangeConstraint', 1, 71)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (93, 135, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnIsValid_IntegerRangeConstraint', 1, 71)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (94, 141, 1, N'Kistl.App.GUI.CustomClientActions_GUI', N'OnToString_ControlInfo', 4, 54)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (95, 136, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnToString_StringRangeConstraint', 1, 73)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (96, 139, 1, N'Kistl.App.Base.CistlClientActions_Base', N'OnGetErrorText_StringRangeConstraint', 1, 73)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (97, 135, 1, N'Kistl.App.Base.CistlClientActions_Base', N'OnIsValid_StringRangeConstraint', 1, 73)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (98, 139, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnGetErrorText_MethodInvocationConstraint', 1, 74)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (99, 136, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnToString_MethodInvocationConstraint', 1, 74)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (100, 135, 1, N'Kistl.App.Base.CustomClientActions_KistlBase', N'OnIsValid_MethodInvocationConstraint', 1, 74)
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [fk_Assembly], [FullTypeName], [MemberName], [fk_Module], [fk_InvokeOnObjectClass]) VALUES (101, 126, 1, N'Kistl.App.GUI.CustomClientActions_GUI', N'OnToString_PresenterInfo', 4, 66)
SET IDENTITY_INSERT [dbo].[MethodInvocations] OFF
/****** Object:  Table [dbo].[BaseParameters]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BaseParameters]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BaseParameters](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParameterName] [nvarchar](100) COLLATE Latin1_General_CI_AS NOT NULL,
	[fk_Method] [int] NOT NULL,
	[fk_Module] [int] NOT NULL,
	[IsList] [bit] NOT NULL,
	[IsReturnParameter] [bit] NOT NULL,
 CONSTRAINT [PK_BaseParameters] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[BaseParameters] ON
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (1, N'TestString', 83, 2, 0, 0)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (2, N'TestInt', 83, 2, 0, 0)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (3, N'ReturnParameter', 1, 1, 0, 1)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (4, N'ReturnParameter', 46, 1, 0, 1)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (5, N'ReturnParameter', 82, 1, 0, 1)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (6, N'TestDouble', 83, 2, 0, 0)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (7, N'TestBool', 83, 2, 0, 0)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (8, N'TestDateTime', 83, 2, 0, 0)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (9, N'TestDateTimeReturn', 83, 2, 0, 1)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (10, N'TestObjectParameter', 83, 2, 0, 0)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (11, N'TestCLRObjectParameter', 83, 2, 0, 0)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (12, N'DateTimeParam', 90, 5, 0, 0)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (13, N'DateTimeParamForTestMethod', 95, 5, 0, 0)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (14, N'message', 96, 4, 0, 0)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (16, N'obj', 97, 4, 0, 0)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (18, N'ctx', 98, 4, 0, 0)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (19, N'objectType', 98, 4, 0, 0)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (20, N'result', 98, 4, 0, 1)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (21, N'ReturnParameter', 118, 1, 0, 1)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (23, N'ReturnParameter', 120, 1, 0, 1)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (24, N'ReturnParameter', 121, 1, 0, 1)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (25, N'ReturnParameter', 123, 1, 0, 1)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (26, N'Result', 124, 1, 0, 1)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (27, N'Result', 125, 1, 1, 1)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (28, N'constrainedValue', 135, 1, 0, 0)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (29, N'return', 135, 1, 0, 1)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (30, N'result', 139, 1, 0, 1)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (31, N'constrainedValue', 139, 1, 0, 0)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (33, N'result', 141, 4, 0, 1)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (34, N'constrainedObj', 135, 1, 0, 0)
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Module], [IsList], [IsReturnParameter]) VALUES (35, N'constrainedObject', 139, 1, 0, 0)
SET IDENTITY_INSERT [dbo].[BaseParameters] OFF
/****** Object:  Table [dbo].[BaseProperties]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BaseProperties]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BaseProperties](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_ObjectClass] [int] NOT NULL,
	[PropertyName] [nvarchar](100) COLLATE Latin1_General_CI_AS NULL,
	[AltText] [nvarchar](200) COLLATE Latin1_General_CI_AS NULL,
	[fk_Module] [int] NOT NULL,
 CONSTRAINT [PK_ObjectProperties] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[BaseProperties] ON
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (1, 33, N'ClassName', N'Der Name der Objektklasse', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (3, 2, N'TableName', N'Tabellenname in der Datenbank', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (7, 33, N'Properties', N'Eigenschaften der Objektklasse', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (8, 5, N'ObjectClass', NULL, 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (9, 5, N'PropertyName', NULL, 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (11, 7, N'IsList', NULL, 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (13, 3, N'Name', N'Projektname', 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (14, 3, N'Tasks', NULL, 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (15, 4, N'Name', N'Taskname', 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (16, 4, N'DatumVon', N'Start Datum', 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (17, 4, N'DatumBis', N'Enddatum', 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (18, 4, N'Aufwand', N'Aufwand in Stunden', 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (19, 4, N'Projekt', N'Verknüpfung zum Projekt', 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (20, 6, N'Name', N'Vorname Nachname', 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (21, 6, N'Projekte', N'Projekte des Mitarbeiters für die er Verantwortlich ist', 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (22, 3, N'Mitarbeiter', NULL, 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (23, 3, N'AufwandGes', NULL, 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (25, 2, N'BaseObjectClass', N'Pointer auf die Basisklasse', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (26, 7, N'IsNullable', NULL, 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (27, 2, N'SubClasses', N'Liste der vererbten Klassen', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (28, 9, N'Length', NULL, 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (29, 10, N'ObjectClass', NULL, 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (30, 10, N'MethodName', NULL, 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (31, 33, N'Methods', N'Liste aller Methoden der Objektklasse.', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (38, 6, N'Geburtstag', N'Herzlichen Glückwunsch zum Geburtstag', 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (39, 6, N'SVNr', N'NNNN TTMMYY', 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (40, 6, N'TelefonNummer', N'+43 123 12345678', 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (41, 5, N'AltText', NULL, 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (42, 18, N'Namespace', N'CLR Namespace des Moduls', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (43, 18, N'ModuleName', N'Name des Moduls', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (44, 18, N'DataTypes', N'Datentypendes Modules', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (45, 33, N'Module', N'Modul der Objektklasse', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (46, 14, N'ReferenceObjectClass', N'Pointer zur Objektklasse', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (47, 16, N'ReferenceProperty', N'Das Property, welches auf diese Klasse zeigt', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (48, 3, N'Kundenname', N'Bitte geben Sie den Kundennamen ein', 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (49, 19, N'Mitarbeiter', NULL, 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (50, 19, N'Auftragsname', N'Bitte füllen Sie einen sprechenden Auftragsnamen aus', 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (51, 19, N'Projekt', N'Projekt zum Auftrag', 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (52, 20, N'Kontoname', N'Name des Zeiterfassungskontos', 3)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (53, 23, N'Projekt', N'Projekt des Kostenträgers', 3)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (54, 25, N'Mitarbeiter', N'Mitarbeiter', 3)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (55, 25, N'Zeitkonto', N'Zeitkonto', 3)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (56, 25, N'Datum', N'Datum', 3)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (57, 25, N'Dauer', N'Dauer in Stunden', 3)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (58, 20, N'Taetigkeiten', N'Tätigkeiten', 3)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (59, 26, N'Kundenname', N'Name des Kunden', 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (60, 26, N'Adresse', N'Adresse & Hausnummer', 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (61, 26, N'PLZ', N'Postleitzahl', 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (62, 26, N'Ort', N'Ort', 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (63, 26, N'Land', N'Land', 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (64, 19, N'Kunde', N'Kunde des Projektes', 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (65, 19, N'Auftragswert', N'Wert in EUR des Auftrages', 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (66, 3, N'Kostentraeger', N'Kostenträger', 3)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (67, 3, N'Auftraege', N'Aufträge', 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (68, 27, N'IconFile', N'Filename of the Icon', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (69, 33, N'DefaultIcon', N'Standard Icon wenn IIcon nicht implementiert ist', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (70, 29, N'Module', N'Module', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (71, 29, N'AssemblyName', N'Full Assemblyname eg. MyActions, Version=1.0.0.0', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (72, 5, N'Module', N'Zugehörig zum Modul', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (73, 10, N'Module', N'Zugehörig zum Modul', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (74, 30, N'Method', N'Methode, die Aufgerufen wird', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (75, 30, N'Assembly', N'Assembly, dass die Methode beinhaltet', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (76, 30, N'FullTypeName', N'Voller Name des .NET Types des implementierenden Members', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (77, 30, N'MemberName', N'Name des implementierenden Members', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (78, 30, N'Module', N'Zugehörig zum Modul', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (79, 30, N'InvokeOnObjectClass', N'In dieser Objektklasse implementieren', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (80, 33, N'MethodInvocations', N'all implemented Methods in this DataType', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (81, 10, N'MethodInvokations', N'Methodenaufrufe implementiert in dieser Objekt Klasse', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (82, 18, N'Assemblies', N'Assemblies des Moduls', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (83, 29, N'IsClientAssembly', N'Legt fest, ob es sich um ein Client-Assembly handelt.', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (84, 16, N'PreFetchToClient', N'Serialisierung der Liste zum Client', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (85, 26, N'EMails', N'EMails des Kunden - können mehrere sein', 2)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (86, 20, N'Mitarbeiter', N'Zugeordnete Mitarbeiter', 3)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (87, 31, N'Name', N'Name der Tätigkeitsart', 3)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (88, 25, N'TaetigkeitsArt', N'Art der Tätigkeit', 3)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (89, 20, N'MaxStunden', N'Maximal erlaubte Stundenanzahl', 3)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (90, 20, N'AktuelleStunden', N'Aktuell gebuchte Stunden', 3)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (91, 36, N'ParameterName', N'Name des Parameter', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (92, 36, N'Method', N'Methode des Parameters', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (93, 36, N'Module', N'Module', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (94, 36, N'IsList', N'Parameter wird als List<> generiert', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (95, 36, N'IsReturnParameter', N'Es darf nur ein Return Parameter angegeben werden', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (96, 10, N'Parameter', N'Parameter der Methode', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (97, 42, N'DataType', N'Kistl-Typ des Parameters', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (98, 43, N'Assembly', N'Assembly des CLR Objektes, NULL für Default Assemblies', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (99, 43, N'FullTypeName', N'Name des CLR Datentypen', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (100, 46, N'Enumeration', N'Übergeordnete Enumeration', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (103, 45, N'EnumerationEntries', N'Einträge der Enumeration', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (104, 47, N'Enumeration', N'Enumeration der Eigenschaft', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (105, 2, N'ImplementsInterfaces', N'Interfaces der Objektklasse', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (107, 48, N'StringProp', N'String Property für das Testinterface', 5)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (108, 48, N'ObjectProp', N'Objektpointer für das Testinterface', 5)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (109, 51, N'StringProp', N'String Property', 5)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (110, 51, N'TestEnumProp', N'Test Enumeration Property', 5)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (111, 48, N'TestEnumProp', N'Test Enum Property', 5)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (112, 51, N'ObjectProp', N'testtest', 5)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (113, 52, N'Platform', N'The Toolkit used by this Renderer', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (114, 54, N'Assembly', N'The assembly containing the Control', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (115, 54, N'ClassName', N'The name of the class implementing this Control', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (116, 54, N'IsContainer', N'Whether or not this Control can contain other Controls', 4)
GO
print 'Processed 100 total records'
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (117, 54, N'Platform', N'The toolkit of this Control.', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (118, 54, N'ControlType', N'The type of Control of this implementation', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (119, 2, N'IsSimpleObject', N'Setting this to true marks the instances of this class as "simple." At first this will only mean that they''ll be displayed inline.', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (124, 10, N'IsDisplayable', N'Shows this Method in th GUI', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (126, 51, N'MyIntProperty', N'test', 5)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (127, 63, N'Number', N'Enter a Number', 5)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (128, 63, N'AreaCode', N'Enter Area Code', 5)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (129, 64, N'StructDefinition', N'Definition of this Struct', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (130, 58, N'PersonName', N'Persons Name', 5)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (131, 58, N'PhoneNumberMobile', N'Mobile Phone Number', 5)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (132, 58, N'PhoneNumberOffice', N'Office Phone Number', 5)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (133, 58, N'Birthday', N'Happy Birthday!', 5)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (135, 46, N'Value', N'The CLR value of this entry', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (136, 46, N'Name', N'CLR name of this entry', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (137, 66, N'ControlType', N'which controls are handled by this Presenter', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (138, 66, N'PresenterAssembly', N'Where to find the implementation of the Presenter', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (139, 66, N'PresenterTypeName', N'The CLR namespace and class name of the Presenter', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (147, 66, N'DataAssembly', N'The Assembly of the Data Type', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (148, 66, N'DataTypeName', N'The CLR namespace and class name of the Data Type', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (149, 67, N'Description', N'A short description of the utility of this visual', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (150, 67, N'ControlType', N'Which visual is represented here', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (151, 67, N'Children', N'if this is a container, here are the visually contained/controlled children of this Visual', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (152, 67, N'Property', N'The Property to display', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (153, 67, N'Method', N'The Method whose return value shoud be displayed', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (154, 68, N'DisplayName', N'a short name to identify this Template to the user', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (155, 68, N'VisualTree', N'The visual representation of this Template', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (162, 68, N'DisplayedTypeFullName', N'FullName of the Type that is displayed with this Template', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (163, 68, N'DisplayedTypeAssembly', N'Assembly of the Type that is displayed with this Template', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (164, 67, N'ContextMenu', N'The context menu for this Visual', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (165, 68, N'Menu', N'The main menu for this Template', 4)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (167, 69, N'Reason', N'The reason of this constraint', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (168, 71, N'Max', N'The biggest value accepted by this constraint', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (169, 71, N'Min', N'The smallest value accepted by this constraint', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (170, 5, N'Constraints', N'The list of constraints applying to this Property', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (171, 69, N'ConstrainedProperty', N'The property to be constrained', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (172, 73, N'MaxLength', N'The maximal length of this StringProperty', 1)
INSERT [dbo].[BaseProperties] ([ID], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module]) VALUES (173, 73, N'MinLength', N'The minimal length of this StringProperty', 1)
SET IDENTITY_INSERT [dbo].[BaseProperties] OFF
/****** Object:  Table [dbo].[Kostenstellen]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Kostenstellen]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Kostenstellen](
	[ID] [int] NOT NULL,
 CONSTRAINT [PK_Kostenstelle] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[Kostenstellen] ([ID]) VALUES (3)
/****** Object:  Table [dbo].[Kostentraeger]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Kostentraeger]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Kostentraeger](
	[ID] [int] NOT NULL,
	[fk_Projekt] [int] NOT NULL,
 CONSTRAINT [PK_Kostentraeger] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[Kostentraeger] ([ID], [fk_Projekt]) VALUES (2, 1)
INSERT [dbo].[Kostentraeger] ([ID], [fk_Projekt]) VALUES (4, 2)
INSERT [dbo].[Kostentraeger] ([ID], [fk_Projekt]) VALUES (8, 4)
INSERT [dbo].[Kostentraeger] ([ID], [fk_Projekt]) VALUES (9, 19)
INSERT [dbo].[Kostentraeger] ([ID], [fk_Projekt]) VALUES (10, 4)
INSERT [dbo].[Kostentraeger] ([ID], [fk_Projekt]) VALUES (11, 4)
/****** Object:  Table [dbo].[Zeitkonten_MitarbeiterCollection]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Zeitkonten_MitarbeiterCollection]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Zeitkonten_MitarbeiterCollection](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_Zeitkonto] [int] NOT NULL,
	[fk_Mitarbeiter] [int] NOT NULL,
 CONSTRAINT [PK_Zeitkonten_MitarbeiterCollection] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Zeitkonten_MitarbeiterCollection] ON
INSERT [dbo].[Zeitkonten_MitarbeiterCollection] ([ID], [fk_Zeitkonto], [fk_Mitarbeiter]) VALUES (3, 3, 3)
INSERT [dbo].[Zeitkonten_MitarbeiterCollection] ([ID], [fk_Zeitkonto], [fk_Mitarbeiter]) VALUES (4, 3, 4)
INSERT [dbo].[Zeitkonten_MitarbeiterCollection] ([ID], [fk_Zeitkonto], [fk_Mitarbeiter]) VALUES (5, 2, 2)
INSERT [dbo].[Zeitkonten_MitarbeiterCollection] ([ID], [fk_Zeitkonto], [fk_Mitarbeiter]) VALUES (6, 2, 1)
INSERT [dbo].[Zeitkonten_MitarbeiterCollection] ([ID], [fk_Zeitkonto], [fk_Mitarbeiter]) VALUES (14, 2, 4)
INSERT [dbo].[Zeitkonten_MitarbeiterCollection] ([ID], [fk_Zeitkonto], [fk_Mitarbeiter]) VALUES (15, 2, 3)
SET IDENTITY_INSERT [dbo].[Zeitkonten_MitarbeiterCollection] OFF
/****** Object:  Table [dbo].[Projekte_MitarbeiterCollection]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Projekte_MitarbeiterCollection]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Projekte_MitarbeiterCollection](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_Projekt] [int] NOT NULL,
	[fk_Mitarbeiter] [int] NOT NULL,
 CONSTRAINT [PK_Projekte_MitarbeiterCollection] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Projekte_MitarbeiterCollection] ON
INSERT [dbo].[Projekte_MitarbeiterCollection] ([ID], [fk_Projekt], [fk_Mitarbeiter]) VALUES (8, 4, 2)
INSERT [dbo].[Projekte_MitarbeiterCollection] ([ID], [fk_Projekt], [fk_Mitarbeiter]) VALUES (9, 3, 3)
INSERT [dbo].[Projekte_MitarbeiterCollection] ([ID], [fk_Projekt], [fk_Mitarbeiter]) VALUES (11, 2, 1)
INSERT [dbo].[Projekte_MitarbeiterCollection] ([ID], [fk_Projekt], [fk_Mitarbeiter]) VALUES (12, 5, 1)
INSERT [dbo].[Projekte_MitarbeiterCollection] ([ID], [fk_Projekt], [fk_Mitarbeiter]) VALUES (15, 2, 2)
INSERT [dbo].[Projekte_MitarbeiterCollection] ([ID], [fk_Projekt], [fk_Mitarbeiter]) VALUES (20, 17, 4)
INSERT [dbo].[Projekte_MitarbeiterCollection] ([ID], [fk_Projekt], [fk_Mitarbeiter]) VALUES (21, 18, 1)
INSERT [dbo].[Projekte_MitarbeiterCollection] ([ID], [fk_Projekt], [fk_Mitarbeiter]) VALUES (22, 2, 5)
SET IDENTITY_INSERT [dbo].[Projekte_MitarbeiterCollection] OFF
/****** Object:  Table [dbo].[Auftraege]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auftraege]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Auftraege](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_Mitarbeiter] [int] NULL,
	[Auftragsname] [nvarchar](200) COLLATE Latin1_General_CI_AS NULL,
	[fk_Projekt] [int] NULL,
	[fk_Kunde] [int] NULL,
	[Auftragswert] [float] NULL,
 CONSTRAINT [PK_Auftrag] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Auftraege] ON
INSERT [dbo].[Auftraege] ([ID], [fk_Mitarbeiter], [Auftragsname], [fk_Projekt], [fk_Kunde], [Auftragswert]) VALUES (1, 1, N'Auftrag für den Businessplan', NULL, 1, 10)
INSERT [dbo].[Auftraege] ([ID], [fk_Mitarbeiter], [Auftragsname], [fk_Projekt], [fk_Kunde], [Auftragswert]) VALUES (2, 2, N'Auftrag für Marketingunterlagen', NULL, 2, 100)
INSERT [dbo].[Auftraege] ([ID], [fk_Mitarbeiter], [Auftragsname], [fk_Projekt], [fk_Kunde], [Auftragswert]) VALUES (3, 2, N'Kistl Implementierungsprojekt', NULL, 1, 100000)
INSERT [dbo].[Auftraege] ([ID], [fk_Mitarbeiter], [Auftragsname], [fk_Projekt], [fk_Kunde], [Auftragswert]) VALUES (4, NULL, N'testauftrag', NULL, 2, 100)
INSERT [dbo].[Auftraege] ([ID], [fk_Mitarbeiter], [Auftragsname], [fk_Projekt], [fk_Kunde], [Auftragswert]) VALUES (5, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Auftraege] ([ID], [fk_Mitarbeiter], [Auftragsname], [fk_Projekt], [fk_Kunde], [Auftragswert]) VALUES (6, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Auftraege] ([ID], [fk_Mitarbeiter], [Auftragsname], [fk_Projekt], [fk_Kunde], [Auftragswert]) VALUES (7, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Auftraege] ([ID], [fk_Mitarbeiter], [Auftragsname], [fk_Projekt], [fk_Kunde], [Auftragswert]) VALUES (8, 5, N'Auftrag Ziviltechniker', 2, 3, 111111)
SET IDENTITY_INSERT [dbo].[Auftraege] OFF
/****** Object:  Table [dbo].[Tasks]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tasks]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tasks](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) COLLATE Latin1_General_CI_AS NULL,
	[DatumVon] [datetime] NULL,
	[DatumBis] [datetime] NULL,
	[Aufwand] [float] NULL,
	[fk_Projekt] [int] NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Tasks] ON
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (1, N'Planung Ziviltechniker', CAST(0x000098A900000000 AS DateTime), CAST(0x000098C800000000 AS DateTime), 27, 2)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (2, N'Umsetzung Ziviltechniker', CAST(0x000098A900000000 AS DateTime), CAST(0x000098C800000000 AS DateTime), 10, 2)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (3, N'Planung Kistl', CAST(0x0000988F00000000 AS DateTime), CAST(0x000098E400000000 AS DateTime), 10, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (5, N'Ein Test2', CAST(0x0000901A00000000 AS DateTime), CAST(0x0000901A00000000 AS DateTime), 10, 5)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (7, N'Neue Aufgabe', CAST(0x000098A900000000 AS DateTime), CAST(0x000098E400000000 AS DateTime), 10, 6)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (8, N'Zweite Aufgabe', CAST(0x000098A900000000 AS DateTime), CAST(0x000098AC00000000 AS DateTime), 10, 6)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (9, N'Buchhaltungsprogramm schreiben', NULL, NULL, 10, 17)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (10, N'Neuer Task', CAST(0x00009A0400000000 AS DateTime), CAST(0x00009A0500000000 AS DateTime), 1, 4)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (11, N'test', NULL, NULL, 1, 12)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (12, N'test2', NULL, NULL, 101, 12)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (13, N'NUnit Test Task', CAST(0x00009A6E00EAD54C AS DateTime), CAST(0x00009A6F00EAD54C AS DateTime), 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (14, N'NUnit Test Task', CAST(0x00009A6E00EB35CE AS DateTime), CAST(0x00009A6F00EB35CE AS DateTime), 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (15, N'NUnit Test Task', CAST(0x00009A6E00F414C2 AS DateTime), CAST(0x00009A6F00F414C2 AS DateTime), 1, 4)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (16, N'NUnit Test Task', CAST(0x00009A6E00F4712C AS DateTime), CAST(0x00009A6F00F4712C AS DateTime), 1, 5)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (17, N'NUnit Test Task', CAST(0x00009A6E00F49D28 AS DateTime), CAST(0x00009A6F00F49D28 AS DateTime), 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (18, N'NUnit Test Task', CAST(0x00009A6E00F4BBF6 AS DateTime), CAST(0x00009A6F00F4BBF6 AS DateTime), 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (19, N'TestTask', NULL, NULL, 1, 17)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (21, N'asdf', NULL, NULL, 2, 17)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (22, N'asdf', NULL, NULL, NULL, 17)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (23, N'test', NULL, NULL, 11, 18)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (24, N'New Task for Testing', NULL, NULL, 11, 2)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (25, N'NUnit Test Task', CAST(0x00009AE20123BCE6 AS DateTime), CAST(0x00009AE30123BCE6 AS DateTime), 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (26, N'NUnit Test Task', CAST(0x00009AE201257648 AS DateTime), CAST(0x00009AE301257648 AS DateTime), 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (27, N'NUnit Test Task', CAST(0x00009AE2012B3EC7 AS DateTime), CAST(0x00009AE3012B3EC7 AS DateTime), 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (28, N'NUnit Test Task', CAST(0x00009AE2013563AA AS DateTime), CAST(0x00009AE3013563AA AS DateTime), 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (29, N'NUnit Test Task', CAST(0x00009AE20135A0BD AS DateTime), CAST(0x00009AE30135A0BD AS DateTime), 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (30, N'NUnit Test Task', CAST(0x00009AE400B09584 AS DateTime), CAST(0x00009AE500B09584 AS DateTime), 1, 1)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (31, N'NUnit Test Task', CAST(0x00009AE400B10129 AS DateTime), CAST(0x00009AE500B10129 AS DateTime), 1, 1)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (32, N'NUnit Test Task', CAST(0x00009AE400CBC965 AS DateTime), CAST(0x00009AE500CBC965 AS DateTime), 1, 1)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (33, N'NUnit Test Task', CAST(0x00009AE40132AF38 AS DateTime), CAST(0x00009AE50132AF38 AS DateTime), 1, 1)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (34, N'NUnit Test Task', CAST(0x00009B4601153802 AS DateTime), CAST(0x00009B4701153802 AS DateTime), 1, 1)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (35, N'NUnit Test Task', CAST(0x00009B4601157396 AS DateTime), CAST(0x00009B4701157396 AS DateTime), 1, 1)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (36, N'NUnit Test Task', CAST(0x00009B460126C20C AS DateTime), CAST(0x00009B470126C20C AS DateTime), 1, 1)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt]) VALUES (37, N'NUnit Test Task', CAST(0x00009B4900916C00 AS DateTime), CAST(0x00009B4A00916C00 AS DateTime), 1, 1)
SET IDENTITY_INSERT [dbo].[Tasks] OFF
/****** Object:  Table [dbo].[Kunden_EMailsCollection]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Kunden_EMailsCollection]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Kunden_EMailsCollection](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_Kunde] [int] NOT NULL,
	[EMails] [nvarchar](200) COLLATE Latin1_General_CI_AS NOT NULL,
 CONSTRAINT [PK_Kunden_EMailsCollection] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Kunden_EMailsCollection] ON
INSERT [dbo].[Kunden_EMailsCollection] ([ID], [fk_Kunde], [EMails]) VALUES (6, 2, N'susanne.dobler@mail.com')
INSERT [dbo].[Kunden_EMailsCollection] ([ID], [fk_Kunde], [EMails]) VALUES (24, 1, N'UnitTest03.11.2008 08:49:27@dasz.at')
INSERT [dbo].[Kunden_EMailsCollection] ([ID], [fk_Kunde], [EMails]) VALUES (25, 1, N'UnitTest31.10.2008 17:53:09@dasz.at')
INSERT [dbo].[Kunden_EMailsCollection] ([ID], [fk_Kunde], [EMails]) VALUES (26, 1, N'UnitTest03.11.2008 08:49:26@dasz.at')
SET IDENTITY_INSERT [dbo].[Kunden_EMailsCollection] OFF
/****** Object:  Table [dbo].[TestObjClasses]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TestObjClasses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TestObjClasses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StringProp] [nvarchar](200) COLLATE Latin1_General_CI_AS NOT NULL,
	[TestEnumProp] [int] NOT NULL,
	[fk_ObjectProp] [int] NOT NULL,
	[MyIntProperty] [int] NULL,
 CONSTRAINT [PK_TestObjClasses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[TestObjClasses] ON
INSERT [dbo].[TestObjClasses] ([ID], [StringProp], [TestEnumProp], [fk_ObjectProp], [MyIntProperty]) VALUES (1, N'test', 1, 2, NULL)
SET IDENTITY_INSERT [dbo].[TestObjClasses] OFF
/****** Object:  Table [dbo].[Enumerations]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Enumerations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Enumerations](
	[ID] [int] NOT NULL,
 CONSTRAINT [PK_Enumerations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[Enumerations] ([ID]) VALUES (50)
INSERT [dbo].[Enumerations] ([ID]) VALUES (53)
INSERT [dbo].[Enumerations] ([ID]) VALUES (55)
/****** Object:  Table [dbo].[Interfaces]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Interfaces]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Interfaces](
	[ID] [int] NOT NULL,
 CONSTRAINT [PK_Interfaces] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[Interfaces] ([ID]) VALUES (48)
INSERT [dbo].[Interfaces] ([ID]) VALUES (52)
/****** Object:  Table [dbo].[ObjectClasses]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObjectClasses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ObjectClasses](
	[ID] [int] NOT NULL,
	[TableName] [nvarchar](100) COLLATE Latin1_General_CI_AS NOT NULL,
	[fk_BaseObjectClass] [int] NULL,
	[IsSimpleObject] [bit] NOT NULL,
 CONSTRAINT [PK_ObjectClasses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (2, N'ObjectClasses', 33, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (3, N'Projekte', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (4, N'Tasks', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (5, N'BaseProperties', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (6, N'Mitarbeiter', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (7, N'Properties', 5, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (8, N'ValueTypeProperties', 7, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (9, N'StringProperties', 8, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (10, N'Methods', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (11, N'IntProperties', 8, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (12, N'BoolProperties', 8, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (13, N'DoubleProperties', 8, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (14, N'ObjectReferenceProperties', 7, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (15, N'DateTimeProperties', 8, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (16, N'BackReferenceProperties', 5, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (18, N'Modules', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (19, N'Auftraege', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (20, N'Zeitkonten', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (21, N'Kostenstellen', 20, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (23, N'Kostentraeger', 20, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (25, N'Taetigkeiten', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (26, N'Kunden', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (27, N'Icons', NULL, 1)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (29, N'Assemblies', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (30, N'MethodInvocations', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (31, N'TaetigkeitsArten', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (33, N'DataTypes', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (36, N'BaseParameters', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (37, N'StringParameters', 36, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (38, N'IntParameters', 36, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (39, N'DoubleParameters', 36, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (40, N'BoolParameters', 36, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (41, N'DateTimeParameters', 36, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (42, N'ObjectParameters', 36, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (43, N'CLRObjectParameters', 36, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (44, N'Interfaces', 33, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (45, N'Enumerations', 33, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (46, N'EnumerationEntries', NULL, 1)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (47, N'EnumerationProperties', 8, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (51, N'TestObjClasses', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (54, N'ControlInfos', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (58, N'TestCustomObjects', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (59, N'Muhblas', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (60, N'AnotherTests', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (61, N'LastTests', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (62, N'Structs', 33, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (64, N'StructProperties', 7, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (66, N'PresenterInfos', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (67, N'Visuals', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (68, N'Templates', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (69, N'Constraints', NULL, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (70, N'NotNullableConstraints', 69, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (71, N'IntegerRangeConstraints', 69, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (73, N'StringRangeConstraints', 69, 0)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject]) VALUES (74, N'MethodInvocationConstraints', 69, 0)
/****** Object:  Table [dbo].[Structs]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Structs]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Structs](
	[ID] [int] NOT NULL,
 CONSTRAINT [PK_Structs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[Structs] ([ID]) VALUES (63)
/****** Object:  Table [dbo].[ObjectParameters]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObjectParameters]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ObjectParameters](
	[ID] [int] NOT NULL,
	[fk_DataType] [int] NOT NULL,
 CONSTRAINT [PK_ObjectParameters] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[ObjectParameters] ([ID], [fk_DataType]) VALUES (10, 19)
INSERT [dbo].[ObjectParameters] ([ID], [fk_DataType]) VALUES (26, 36)
INSERT [dbo].[ObjectParameters] ([ID], [fk_DataType]) VALUES (27, 10)
/****** Object:  Table [dbo].[Constraints]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Constraints]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Constraints](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Reason] [nvarchar](400) COLLATE Latin1_General_CI_AS NULL,
	[fk_ConstrainedProperty] [int] NOT NULL,
 CONSTRAINT [PK_Constraints] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Constraints] ON
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (74, NULL, 171)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (75, NULL, 169)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (76, NULL, 168)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (77, NULL, 163)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (78, NULL, 162)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (79, NULL, 155)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (80, NULL, 154)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (81, NULL, 153)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (82, NULL, 150)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (83, NULL, 149)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (84, NULL, 148)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (85, NULL, 139)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (86, NULL, 138)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (87, NULL, 137)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (88, NULL, 136)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (89, NULL, 135)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (90, NULL, 130)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (91, NULL, 129)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (92, NULL, 124)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (93, NULL, 119)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (94, NULL, 118)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (95, NULL, 117)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (96, NULL, 116)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (97, NULL, 115)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (98, NULL, 114)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (99, NULL, 113)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (100, NULL, 112)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (101, NULL, 111)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (102, NULL, 110)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (103, NULL, 109)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (104, NULL, 108)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (105, NULL, 107)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (106, NULL, 104)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (107, NULL, 100)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (108, NULL, 99)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (109, NULL, 97)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (110, NULL, 95)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (111, NULL, 94)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (112, NULL, 93)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (113, NULL, 92)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (114, NULL, 91)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (115, NULL, 87)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (116, NULL, 84)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (117, NULL, 83)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (118, NULL, 79)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (119, NULL, 78)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (120, NULL, 77)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (121, NULL, 76)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (122, NULL, 75)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (123, NULL, 74)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (124, NULL, 73)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (125, NULL, 72)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (126, NULL, 71)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (127, NULL, 70)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (128, NULL, 68)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (129, NULL, 61)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (130, NULL, 59)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (131, NULL, 57)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (132, NULL, 56)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (133, NULL, 55)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (134, NULL, 54)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (135, NULL, 53)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (136, NULL, 52)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (137, NULL, 45)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (138, NULL, 43)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (139, NULL, 42)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (140, NULL, 30)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (141, NULL, 29)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (142, NULL, 26)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (143, NULL, 11)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (144, NULL, 8)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (145, NULL, 3)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (146, NULL, 1)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (147, NULL, 28)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (148, N'strings in the database should not be longer than 4k', 172)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (149, N'strings in the database should not be longer than 4k', 173)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (150, NULL, 173)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (151, NULL, 172)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (152, NULL, 167)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (153, NULL, 162)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (154, NULL, 154)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (155, NULL, 149)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (156, NULL, 148)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (157, NULL, 139)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (158, NULL, 136)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (159, NULL, 130)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (160, NULL, 128)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (161, NULL, 127)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (162, N'', 115)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (163, NULL, 109)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (164, NULL, 107)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (165, NULL, 99)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (166, NULL, 91)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (167, NULL, 87)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (168, NULL, 85)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (169, NULL, 77)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (170, NULL, 76)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (171, NULL, 71)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (172, NULL, 68)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (173, NULL, 63)
GO
print 'Processed 100 total records'
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (174, NULL, 62)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (175, NULL, 61)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (176, NULL, 60)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (177, NULL, 59)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (178, NULL, 52)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (179, NULL, 50)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (180, NULL, 48)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (181, NULL, 43)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (182, NULL, 42)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (183, NULL, 41)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (184, NULL, 40)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (185, NULL, 39)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (186, NULL, 30)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (187, NULL, 20)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (188, NULL, 15)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (189, NULL, 13)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (190, NULL, 9)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (191, NULL, 3)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (192, N'Classes should have a non-empty name. Names longer than 51 become unwieldy.', 1)
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty]) VALUES (193, N'Method.ObjectClass and InvokeOnObjectClass have to match.', 74)
SET IDENTITY_INSERT [dbo].[Constraints] OFF
/****** Object:  Table [dbo].[BaseProperties_ConstraintsCollection]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BaseProperties_ConstraintsCollection]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BaseProperties_ConstraintsCollection](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_BaseProperty] [int] NOT NULL,
	[fk_Constraints] [int] NOT NULL,
 CONSTRAINT [PK_BaseProperties_ConstraintsCollection] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Properties]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Properties]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Properties](
	[ID] [int] NOT NULL,
	[IsNullable] [bit] NOT NULL,
	[IsList] [bit] NOT NULL,
 CONSTRAINT [PK_Properties] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (1, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (3, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (8, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (9, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (11, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (13, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (15, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (16, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (17, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (18, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (19, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (20, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (22, 1, 1)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (23, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (25, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (26, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (28, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (29, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (30, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (38, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (39, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (40, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (41, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (42, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (43, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (45, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (46, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (47, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (48, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (49, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (50, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (51, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (52, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (53, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (54, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (55, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (56, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (57, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (59, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (60, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (61, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (62, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (63, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (64, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (65, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (68, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (69, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (70, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (71, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (72, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (73, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (74, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (75, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (76, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (77, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (78, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (79, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (83, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (84, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (85, 1, 1)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (86, 1, 1)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (87, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (88, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (89, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (90, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (91, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (92, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (93, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (94, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (95, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (97, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (98, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (99, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (100, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (104, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (105, 1, 1)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (107, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (108, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (109, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (110, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (111, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (112, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (113, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (114, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (115, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (116, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (117, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (118, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (119, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (124, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (126, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (127, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (128, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (129, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (130, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (131, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (132, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (133, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (135, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (136, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (137, 0, 0)
GO
print 'Processed 100 total records'
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (138, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (139, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (147, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (148, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (149, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (150, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (151, 1, 1)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (152, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (153, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (154, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (155, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (162, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (163, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (164, 1, 1)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (165, 1, 1)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (167, 1, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (168, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (169, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (171, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (172, 0, 0)
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList]) VALUES (173, 0, 0)
/****** Object:  Table [dbo].[ObjectReferenceProperties]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObjectReferenceProperties]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ObjectReferenceProperties](
	[ID] [int] NOT NULL,
	[ReferenceObjectClassName] [nvarchar](200) COLLATE Latin1_General_CI_AS NULL,
	[fk_ReferenceObjectClass] [int] NULL,
 CONSTRAINT [PK_ObjectReferenceProperties] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (8, N'Kistl.App.Base.ObjectClass', 33)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (19, N'Kistl.App.Projekte.Projekt', 3)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (22, N'Kistl.App.Projekte.Mitarbeiter', 6)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (25, N'Kistl.App.Base.ObjectClass', 2)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (29, N'Kistl.App.Base.ObjectClass', 33)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (45, N'Kistl.App.Base.Module', 18)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (46, N'Kistl.App.Base.ObjectClass', 2)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (47, NULL, 14)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (49, NULL, 6)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (51, NULL, 3)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (53, NULL, 3)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (54, NULL, 6)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (55, NULL, 20)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (64, NULL, 26)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (69, NULL, 27)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (70, NULL, 18)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (72, NULL, 18)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (73, NULL, 18)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (74, NULL, 10)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (75, NULL, 29)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (78, NULL, 18)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (79, NULL, 33)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (86, NULL, 6)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (88, NULL, 31)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (92, NULL, 10)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (93, NULL, 18)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (97, NULL, 33)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (98, NULL, 29)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (100, NULL, 45)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (104, NULL, 45)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (105, NULL, 44)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (108, NULL, 26)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (112, NULL, 26)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (114, NULL, 29)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (129, NULL, 62)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (138, NULL, 29)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (147, NULL, 29)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (151, NULL, 67)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (152, NULL, 5)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (153, NULL, 10)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (155, NULL, 67)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (163, NULL, 29)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (164, NULL, 67)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (165, NULL, 67)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [ReferenceObjectClassName], [fk_ReferenceObjectClass]) VALUES (171, NULL, 5)
/****** Object:  Table [dbo].[BackReferenceProperties]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BackReferenceProperties]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BackReferenceProperties](
	[ID] [int] NOT NULL,
	[ReferenceObjectClassName] [nvarchar](200) COLLATE Latin1_General_CI_AS NULL,
	[ReferencePropertyName] [nvarchar](200) COLLATE Latin1_General_CI_AS NULL,
	[fk_ReferenceProperty] [int] NULL,
	[PreFetchToClient] [bit] NOT NULL,
 CONSTRAINT [PK_BackReferenceProperty] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[BackReferenceProperties] ([ID], [ReferenceObjectClassName], [ReferencePropertyName], [fk_ReferenceProperty], [PreFetchToClient]) VALUES (7, N'Kistl.App.Base.BaseProperty', N'ObjectClass', 8, 1)
INSERT [dbo].[BackReferenceProperties] ([ID], [ReferenceObjectClassName], [ReferencePropertyName], [fk_ReferenceProperty], [PreFetchToClient]) VALUES (14, N'Kistl.App.Projekte.Task', N'Projekt', 19, 0)
INSERT [dbo].[BackReferenceProperties] ([ID], [ReferenceObjectClassName], [ReferencePropertyName], [fk_ReferenceProperty], [PreFetchToClient]) VALUES (21, N'Kistl.App.Projekte.Projekt', N'Mitarbeiter', 22, 0)
INSERT [dbo].[BackReferenceProperties] ([ID], [ReferenceObjectClassName], [ReferencePropertyName], [fk_ReferenceProperty], [PreFetchToClient]) VALUES (27, N'Kistl.App.Base.ObjectClass', N'BaseObjectClass', 25, 0)
INSERT [dbo].[BackReferenceProperties] ([ID], [ReferenceObjectClassName], [ReferencePropertyName], [fk_ReferenceProperty], [PreFetchToClient]) VALUES (31, N'Kistl.App.Base.Method', N'ObjectClass', 29, 1)
INSERT [dbo].[BackReferenceProperties] ([ID], [ReferenceObjectClassName], [ReferencePropertyName], [fk_ReferenceProperty], [PreFetchToClient]) VALUES (44, N'Kistl.App.Base.ObjectClass', N'Module', 45, 0)
INSERT [dbo].[BackReferenceProperties] ([ID], [ReferenceObjectClassName], [ReferencePropertyName], [fk_ReferenceProperty], [PreFetchToClient]) VALUES (58, NULL, NULL, 55, 0)
INSERT [dbo].[BackReferenceProperties] ([ID], [ReferenceObjectClassName], [ReferencePropertyName], [fk_ReferenceProperty], [PreFetchToClient]) VALUES (66, NULL, NULL, 53, 0)
INSERT [dbo].[BackReferenceProperties] ([ID], [ReferenceObjectClassName], [ReferencePropertyName], [fk_ReferenceProperty], [PreFetchToClient]) VALUES (67, NULL, NULL, 51, 0)
INSERT [dbo].[BackReferenceProperties] ([ID], [ReferenceObjectClassName], [ReferencePropertyName], [fk_ReferenceProperty], [PreFetchToClient]) VALUES (80, NULL, NULL, 79, 1)
INSERT [dbo].[BackReferenceProperties] ([ID], [ReferenceObjectClassName], [ReferencePropertyName], [fk_ReferenceProperty], [PreFetchToClient]) VALUES (81, NULL, NULL, 74, 0)
INSERT [dbo].[BackReferenceProperties] ([ID], [ReferenceObjectClassName], [ReferencePropertyName], [fk_ReferenceProperty], [PreFetchToClient]) VALUES (82, NULL, NULL, 70, 0)
INSERT [dbo].[BackReferenceProperties] ([ID], [ReferenceObjectClassName], [ReferencePropertyName], [fk_ReferenceProperty], [PreFetchToClient]) VALUES (96, NULL, NULL, 92, 1)
INSERT [dbo].[BackReferenceProperties] ([ID], [ReferenceObjectClassName], [ReferencePropertyName], [fk_ReferenceProperty], [PreFetchToClient]) VALUES (103, NULL, NULL, 100, 1)
INSERT [dbo].[BackReferenceProperties] ([ID], [ReferenceObjectClassName], [ReferencePropertyName], [fk_ReferenceProperty], [PreFetchToClient]) VALUES (170, NULL, NULL, 171, 1)
/****** Object:  Table [dbo].[Visuals]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Visuals]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Visuals](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](200) COLLATE Latin1_General_CI_AS NOT NULL,
	[ControlType] [int] NOT NULL,
	[fk_Property] [int] NULL,
	[fk_Method] [int] NOT NULL,
 CONSTRAINT [PK_Visuals] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[StructProperties]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StructProperties]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StructProperties](
	[ID] [int] NOT NULL,
	[fk_StructDefinition] [int] NOT NULL,
 CONSTRAINT [PK_StructProperties] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[StructProperties] ([ID], [fk_StructDefinition]) VALUES (131, 63)
INSERT [dbo].[StructProperties] ([ID], [fk_StructDefinition]) VALUES (132, 63)
/****** Object:  Table [dbo].[ValueTypeProperties]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ValueTypeProperties]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ValueTypeProperties](
	[ID] [int] NOT NULL,
 CONSTRAINT [PK_ValueTypeProperties] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (1)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (3)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (9)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (11)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (13)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (15)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (16)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (17)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (18)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (20)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (23)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (26)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (28)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (30)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (38)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (39)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (40)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (41)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (42)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (43)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (48)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (50)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (52)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (56)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (57)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (59)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (60)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (61)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (62)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (63)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (65)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (68)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (71)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (76)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (77)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (83)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (84)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (85)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (87)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (89)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (90)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (91)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (94)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (95)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (99)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (107)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (109)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (110)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (111)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (113)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (115)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (116)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (117)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (118)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (119)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (124)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (126)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (127)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (128)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (130)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (133)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (135)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (136)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (137)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (139)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (148)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (149)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (150)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (154)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (162)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (167)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (168)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (169)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (172)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (173)
/****** Object:  Table [dbo].[EnumerationProperties]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EnumerationProperties]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EnumerationProperties](
	[ID] [int] NOT NULL,
	[fk_Enumeration] [int] NOT NULL,
 CONSTRAINT [PK_EnumerationProperties] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[EnumerationProperties] ([ID], [fk_Enumeration]) VALUES (110, 50)
INSERT [dbo].[EnumerationProperties] ([ID], [fk_Enumeration]) VALUES (111, 50)
INSERT [dbo].[EnumerationProperties] ([ID], [fk_Enumeration]) VALUES (113, 53)
INSERT [dbo].[EnumerationProperties] ([ID], [fk_Enumeration]) VALUES (117, 53)
INSERT [dbo].[EnumerationProperties] ([ID], [fk_Enumeration]) VALUES (118, 55)
INSERT [dbo].[EnumerationProperties] ([ID], [fk_Enumeration]) VALUES (137, 55)
INSERT [dbo].[EnumerationProperties] ([ID], [fk_Enumeration]) VALUES (150, 55)
/****** Object:  Table [dbo].[StringProperties]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StringProperties]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StringProperties](
	[ID] [int] NOT NULL,
	[Length] [int] NULL,
 CONSTRAINT [PK_StringProperties] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (1, 51)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (3, 100)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (9, 100)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (13, 100)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (15, 100)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (20, 100)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (30, 100)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (39, 20)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (40, 50)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (41, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (42, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (43, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (48, 100)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (50, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (52, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (59, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (60, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (61, 10)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (62, 100)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (63, 50)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (68, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (71, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (76, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (77, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (85, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (87, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (91, 100)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (99, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (107, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (109, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (115, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (127, 50)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (128, 50)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (130, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (136, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (139, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (148, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (149, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (154, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (162, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (167, 400)
/****** Object:  Table [dbo].[DateTimeProperties]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DateTimeProperties]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DateTimeProperties](
	[ID] [int] NOT NULL,
 CONSTRAINT [PK_DateTimeProperties] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[DateTimeProperties] ([ID]) VALUES (16)
INSERT [dbo].[DateTimeProperties] ([ID]) VALUES (17)
INSERT [dbo].[DateTimeProperties] ([ID]) VALUES (38)
INSERT [dbo].[DateTimeProperties] ([ID]) VALUES (56)
INSERT [dbo].[DateTimeProperties] ([ID]) VALUES (133)
/****** Object:  Table [dbo].[IntProperties]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IntProperties]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[IntProperties](
	[ID] [int] NOT NULL,
 CONSTRAINT [PK_IntPropertes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[IntProperties] ([ID]) VALUES (28)
INSERT [dbo].[IntProperties] ([ID]) VALUES (126)
INSERT [dbo].[IntProperties] ([ID]) VALUES (135)
INSERT [dbo].[IntProperties] ([ID]) VALUES (168)
INSERT [dbo].[IntProperties] ([ID]) VALUES (169)
INSERT [dbo].[IntProperties] ([ID]) VALUES (172)
INSERT [dbo].[IntProperties] ([ID]) VALUES (173)
/****** Object:  Table [dbo].[DoubleProperties]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoubleProperties]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DoubleProperties](
	[ID] [int] NOT NULL,
 CONSTRAINT [PK_DoubleProperties] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[DoubleProperties] ([ID]) VALUES (18)
INSERT [dbo].[DoubleProperties] ([ID]) VALUES (23)
INSERT [dbo].[DoubleProperties] ([ID]) VALUES (57)
INSERT [dbo].[DoubleProperties] ([ID]) VALUES (65)
INSERT [dbo].[DoubleProperties] ([ID]) VALUES (89)
INSERT [dbo].[DoubleProperties] ([ID]) VALUES (90)
/****** Object:  Table [dbo].[BoolProperties]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BoolProperties]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BoolProperties](
	[ID] [int] NOT NULL,
 CONSTRAINT [PK_BoolProperties] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[BoolProperties] ([ID]) VALUES (11)
INSERT [dbo].[BoolProperties] ([ID]) VALUES (26)
INSERT [dbo].[BoolProperties] ([ID]) VALUES (83)
INSERT [dbo].[BoolProperties] ([ID]) VALUES (84)
INSERT [dbo].[BoolProperties] ([ID]) VALUES (94)
INSERT [dbo].[BoolProperties] ([ID]) VALUES (95)
INSERT [dbo].[BoolProperties] ([ID]) VALUES (116)
INSERT [dbo].[BoolProperties] ([ID]) VALUES (119)
INSERT [dbo].[BoolProperties] ([ID]) VALUES (124)
/****** Object:  Table [dbo].[EnumerationEntries]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EnumerationEntries]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EnumerationEntries](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_Enumeration] [int] NOT NULL,
	[Value] [int] NOT NULL,
	[Name] [nvarchar](200) COLLATE Latin1_General_CI_AS NOT NULL,
 CONSTRAINT [PK_EnumerationEntries] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[EnumerationEntries] ON
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (2, 50, 0, N'First')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (3, 50, 1, N'Second')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (5, 53, 0, N'WPF')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (6, 53, 1, N'ASPNET')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (7, 53, 2, N'TEST')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (40, 55, 16, N'SimpleObjectList')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (41, 55, 15, N'Enumeration')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (42, 55, 14, N'StringList')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (43, 55, 13, N'String')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (44, 55, 12, N'IntegerList')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (45, 55, 11, N'Integer')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (46, 55, 10, N'DoubleList')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (47, 55, 9, N'Double')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (48, 55, 8, N'DateTimeList')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (49, 55, 7, N'DateTime')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (50, 55, 6, N'BooleanList')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (51, 55, 5, N'Boolean')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (52, 55, 4, N'ObjectReference')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (53, 55, 3, N'ObjectList')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (54, 55, 2, N'PropertyGroup')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (55, 55, 1, N'Object')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (56, 55, 0, N'Renderer')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (57, 55, 18, N'MenuGroup')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (58, 55, 17, N'MenuItem')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name]) VALUES (59, 55, 19, N'TemplateEditor')
SET IDENTITY_INSERT [dbo].[EnumerationEntries] OFF
/****** Object:  Table [dbo].[PresenterInfos]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PresenterInfos]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PresenterInfos](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_PresenterAssembly] [int] NOT NULL,
	[PresenterTypeName] [nvarchar](200) COLLATE Latin1_General_CI_AS NOT NULL,
	[fk_DataAssembly] [int] NULL,
	[DataTypeName] [nvarchar](200) COLLATE Latin1_General_CI_AS NOT NULL,
	[ControlType] [int] NOT NULL,
 CONSTRAINT [PK_PresenterInfos] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[PresenterInfos] ON
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (3, 14, N'Kistl.GUI.DefaultMethodPresenter`1', 13, N'Kistl.App.Base.Method', 14)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (4, 14, N'Kistl.GUI.DefaultMethodPresenter`1', 13, N'Kistl.App.Base.Method', 13)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (5, 14, N'Kistl.GUI.DefaultMethodPresenter`1', 13, N'Kistl.App.Base.Method', 12)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (6, 14, N'Kistl.GUI.DefaultMethodPresenter`1', 13, N'Kistl.App.Base.Method', 11)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (7, 14, N'Kistl.GUI.DefaultMethodPresenter`1', 13, N'Kistl.App.Base.Method', 10)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (8, 14, N'Kistl.GUI.DefaultMethodPresenter`1', 13, N'Kistl.App.Base.Method', 9)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (9, 14, N'Kistl.GUI.DefaultMethodPresenter`1', 13, N'Kistl.App.Base.Method', 8)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (10, 14, N'Kistl.GUI.DefaultMethodPresenter`1', 13, N'Kistl.App.Base.Method', 7)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (11, 14, N'Kistl.GUI.DefaultMethodPresenter`1', 13, N'Kistl.App.Base.Method', 6)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (12, 14, N'Kistl.GUI.DefaultMethodPresenter`1', 13, N'Kistl.App.Base.Method', 5)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (13, 14, N'Kistl.GUI.ObjectListMethodPresenter', 13, N'Kistl.App.Base.Method', 3)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (14, 14, N'Kistl.GUI.ObjectMethodPresenter', 13, N'Kistl.App.Base.Method', 4)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (15, 14, N'Kistl.GUI.DefaultListPresenter`1', NULL, N'System.Collections.Generic.IList`1[[Kistl.App.Base.EnumerationEntry, Kistl.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]', 16)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (16, 14, N'Kistl.GUI.EnumerationPresenter`1', 13, N'Kistl.App.Base.EnumerationProperty', 15)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (17, 14, N'Kistl.GUI.DefaultListPresenter`1', 13, N'Kistl.App.Base.StringProperty', 14)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (18, 14, N'Kistl.GUI.DefaultValuePresenter`1', 13, N'Kistl.App.Base.StringProperty', 13)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (19, 14, N'Kistl.GUI.DefaultListPresenter`1', 13, N'Kistl.App.Base.IntProperty', 12)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (20, 14, N'Kistl.GUI.DefaultStructPresenter`1', 13, N'Kistl.App.Base.IntProperty', 11)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (21, 14, N'Kistl.GUI.DefaultListPresenter`1', 13, N'Kistl.App.Base.DoubleProperty', 10)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (22, 14, N'Kistl.GUI.DefaultStructPresenter`1', 13, N'Kistl.App.Base.DoubleProperty', 9)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (23, 14, N'Kistl.GUI.DefaultListPresenter`1', 13, N'Kistl.App.Base.DateTimeProperty', 8)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (24, 14, N'Kistl.GUI.DefaultStructPresenter`1', 13, N'Kistl.App.Base.DateTimeProperty', 7)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (25, 14, N'Kistl.GUI.DefaultListPresenter`1', 13, N'Kistl.App.Base.BoolProperty', 6)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (26, 14, N'Kistl.GUI.DefaultStructPresenter`1', 13, N'Kistl.App.Base.BoolProperty', 5)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (27, 14, N'Kistl.GUI.BackReferencePresenter`1', 13, N'Kistl.App.Base.BackReferenceProperty', 3)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (28, 14, N'Kistl.GUI.ObjectListPresenter`1', 13, N'Kistl.App.Base.ObjectReferenceProperty', 3)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (29, 14, N'Kistl.GUI.ObjectReferencePresenter`1', 13, N'Kistl.App.Base.ObjectReferenceProperty', 4)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (30, 14, N'Kistl.GUI.GroupPresenter', 14, N'Kistl.GUI.GroupPresenter', 2)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (31, 14, N'Kistl.GUI.ObjectPresenter', 15, N'Kistl.API.IDataObject', 1)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (32, 14, N'Kistl.GUI.ActionPresenter', 13, N'Kistl.App.Base.Method', 17)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType]) VALUES (33, 4, N'Kistl.GUI.Renderer.WPF.TemplateEditorPresenter', 15, N'Kistl.API.IDataObject', 19)
SET IDENTITY_INSERT [dbo].[PresenterInfos] OFF
/****** Object:  Table [dbo].[ControlInfos]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ControlInfos]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ControlInfos](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_Assembly] [int] NOT NULL,
	[ClassName] [nvarchar](200) COLLATE Latin1_General_CI_AS NOT NULL,
	[IsContainer] [bit] NOT NULL,
	[Platform] [int] NOT NULL,
	[ControlType] [int] NOT NULL,
 CONSTRAINT [PK_ControlInfos] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[ControlInfos] ON
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (285, 4, N'Kistl.GUI.Renderer.WPF.EnumControl', 0, 0, 15)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (286, 4, N'Kistl.GUI.Renderer.WPF.StringListControl', 0, 0, 14)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (287, 4, N'Kistl.GUI.Renderer.WPF.EditSimpleProperty', 0, 0, 13)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (288, 4, N'Kistl.GUI.Renderer.WPF.IntegerListControl', 0, 0, 12)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (289, 4, N'Kistl.GUI.Renderer.WPF.EditIntProperty', 0, 0, 11)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (290, 4, N'Kistl.GUI.Renderer.WPF.DoubleListControl', 0, 0, 10)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (291, 4, N'Kistl.GUI.Renderer.WPF.EditDoubleProperty', 0, 0, 9)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (292, 4, N'Kistl.GUI.Renderer.WPF.DateTimeListControl', 0, 0, 8)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (293, 4, N'Kistl.GUI.Renderer.WPF.EditDateTimeProperty', 0, 0, 7)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (294, 4, N'Kistl.GUI.Renderer.WPF.BooleanListControl', 0, 0, 6)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (295, 4, N'Kistl.GUI.Renderer.WPF.EditBoolProperty', 0, 0, 5)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (296, 4, N'Kistl.GUI.Renderer.WPF.EnumEntryListControl', 0, 0, 16)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (297, 4, N'Kistl.GUI.Renderer.WPF.ObjectListControl', 0, 0, 3)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (298, 4, N'Kistl.GUI.Renderer.WPF.ObjectReferenceControl', 0, 0, 4)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (299, 4, N'Kistl.GUI.Renderer.WPF.GroupBoxWrapper', 1, 0, 2)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (300, 4, N'Kistl.GUI.Renderer.WPF.ObjectTabItem', 1, 0, 1)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (301, 4, N'Kistl.GUI.Renderer.WPF.Renderer', 1, 0, 0)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (302, 3, N'Kistl.Client.ASPNET.Toolkit.StringPropertyControlLoader', 0, 1, 13)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (303, 3, N'Kistl.Client.ASPNET.Toolkit.ObjectReferencePropertyControlLoader', 0, 1, 4)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (304, 3, N'Kistl.Client.ASPNET.Toolkit.ObjectListControlLoader', 0, 1, 3)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (305, 3, N'Kistl.Client.ASPNET.Toolkit.IntPropertyControlLoader', 0, 1, 11)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (306, 3, N'Kistl.Client.ASPNET.Toolkit.DoublePropertyControlLoader', 0, 1, 9)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (307, 3, N'Kistl.Client.ASPNET.Toolkit.DateTimePropertyControlLoader', 0, 1, 7)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (308, 3, N'Kistl.Client.ASPNET.Toolkit.BoolPropertyControlLoader', 0, 1, 5)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (309, 3, N'Kistl.Client.ASPNET.Toolkit.ObjectPanelLoader', 1, 1, 1)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (310, 3, N'Kistl.Client.ASPNET.Toolkit.Renderer', 1, 1, 0)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (311, 4, N'Kistl.GUI.Renderer.WPF.ActionControl', 0, 0, 17)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (312, 3, N'Kistl.Client.ASPNET.Toolkit.Controls.PropertyGroup', 1, 1, 2)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType]) VALUES (313, 4, N'Kistl.GUI.Renderer.WPF.TemplateEditor', 0, 0, 19)
SET IDENTITY_INSERT [dbo].[ControlInfos] OFF
/****** Object:  Table [dbo].[CLRObjectParameters]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLRObjectParameters]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CLRObjectParameters](
	[ID] [int] NOT NULL,
	[fk_Assembly] [int] NULL,
	[FullTypeName] [nvarchar](200) COLLATE Latin1_General_CI_AS NOT NULL,
 CONSTRAINT [PK_CLRObjectParameters] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName]) VALUES (11, NULL, N'System.Guid')
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName]) VALUES (16, NULL, N'Kistl.API.IDataObject')
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName]) VALUES (18, NULL, N'Kistl.API.IKistlContext')
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName]) VALUES (19, NULL, N'System.Type')
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName]) VALUES (20, NULL, N'Kistl.API.IDataObject')
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName]) VALUES (21, NULL, N'System.Type')
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName]) VALUES (24, NULL, N'System.Type')
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName]) VALUES (25, NULL, N'System.Type')
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName]) VALUES (28, NULL, N'System.Object')
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName]) VALUES (31, NULL, N'System.Object')
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName]) VALUES (34, NULL, N'System.Object')
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName]) VALUES (35, NULL, N'System.Object')
/****** Object:  Table [dbo].[Templates]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Templates]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Templates](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DisplayName] [nvarchar](200) COLLATE Latin1_General_CI_AS NOT NULL,
	[fk_VisualTree] [int] NOT NULL,
	[DisplayedTypeFullName] [nvarchar](200) COLLATE Latin1_General_CI_AS NOT NULL,
	[fk_DisplayedTypeAssembly] [int] NOT NULL,
 CONSTRAINT [PK_Templates] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[StringParameters]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StringParameters]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StringParameters](
	[ID] [int] NOT NULL,
 CONSTRAINT [PK_StringParameters] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[StringParameters] ([ID]) VALUES (1)
INSERT [dbo].[StringParameters] ([ID]) VALUES (3)
INSERT [dbo].[StringParameters] ([ID]) VALUES (4)
INSERT [dbo].[StringParameters] ([ID]) VALUES (5)
INSERT [dbo].[StringParameters] ([ID]) VALUES (14)
INSERT [dbo].[StringParameters] ([ID]) VALUES (23)
INSERT [dbo].[StringParameters] ([ID]) VALUES (30)
INSERT [dbo].[StringParameters] ([ID]) VALUES (33)
/****** Object:  Table [dbo].[IntParameters]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IntParameters]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[IntParameters](
	[ID] [int] NOT NULL,
 CONSTRAINT [PK_IntParameters] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[IntParameters] ([ID]) VALUES (2)
/****** Object:  Table [dbo].[DoubleParameters]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoubleParameters]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DoubleParameters](
	[ID] [int] NOT NULL,
 CONSTRAINT [PK_DoubleParameters] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[DoubleParameters] ([ID]) VALUES (6)
/****** Object:  Table [dbo].[BoolParameters]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BoolParameters]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BoolParameters](
	[ID] [int] NOT NULL,
 CONSTRAINT [PK_BoolParameters] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[BoolParameters] ([ID]) VALUES (7)
INSERT [dbo].[BoolParameters] ([ID]) VALUES (29)
/****** Object:  Table [dbo].[DateTimeParameters]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DateTimeParameters]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DateTimeParameters](
	[ID] [int] NOT NULL,
 CONSTRAINT [PK_DateTimeParameters] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[DateTimeParameters] ([ID]) VALUES (8)
INSERT [dbo].[DateTimeParameters] ([ID]) VALUES (9)
INSERT [dbo].[DateTimeParameters] ([ID]) VALUES (12)
INSERT [dbo].[DateTimeParameters] ([ID]) VALUES (13)
/****** Object:  Table [dbo].[ObjectClasses_ImplementsInterfacesCollection]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObjectClasses_ImplementsInterfacesCollection]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ObjectClasses_ImplementsInterfacesCollection](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_ObjectClass] [int] NOT NULL,
	[fk_ImplementsInterfaces] [int] NOT NULL,
 CONSTRAINT [PK_ObjectClasses_ImplementsInterfacesCollection] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[ObjectClasses_ImplementsInterfacesCollection] ON
INSERT [dbo].[ObjectClasses_ImplementsInterfacesCollection] ([ID], [fk_ObjectClass], [fk_ImplementsInterfaces]) VALUES (1, 51, 48)
SET IDENTITY_INSERT [dbo].[ObjectClasses_ImplementsInterfacesCollection] OFF
/****** Object:  Table [dbo].[StringRangeConstraints]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StringRangeConstraints]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StringRangeConstraints](
	[ID] [int] NOT NULL,
	[MaxLength] [int] NOT NULL,
	[MinLength] [int] NOT NULL,
 CONSTRAINT [PK_StringRangeConstraints] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (152, 400, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (153, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (154, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (155, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (156, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (157, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (158, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (159, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (160, 50, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (161, 50, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (162, 200, 1)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (163, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (164, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (165, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (166, 100, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (167, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (168, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (169, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (170, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (171, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (172, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (173, 50, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (174, 100, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (175, 10, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (176, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (177, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (178, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (179, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (180, 100, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (181, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (182, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (183, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (184, 50, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (185, 20, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (186, 100, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (187, 100, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (188, 100, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (189, 100, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (190, 100, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (191, 100, 1)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (192, 51, 1)
/****** Object:  Table [dbo].[MethodInvocationConstraints]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MethodInvocationConstraints]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MethodInvocationConstraints](
	[ID] [int] NOT NULL,
 CONSTRAINT [PK_MethodInvocationConstraints] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[MethodInvocationConstraints] ([ID]) VALUES (193)
/****** Object:  Table [dbo].[NotNullableConstraints]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NotNullableConstraints]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NotNullableConstraints](
	[ID] [int] NOT NULL,
	[Reason] [nvarchar](400) COLLATE Latin1_General_CI_AS NULL,
 CONSTRAINT [PK_NotNullableConstraints] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (74, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (75, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (76, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (77, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (78, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (79, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (80, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (81, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (82, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (83, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (84, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (85, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (86, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (87, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (88, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (89, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (90, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (91, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (92, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (93, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (94, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (95, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (96, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (97, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (98, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (99, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (100, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (101, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (102, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (103, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (104, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (105, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (106, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (107, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (108, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (109, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (110, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (111, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (112, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (113, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (114, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (115, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (116, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (117, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (118, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (119, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (120, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (121, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (122, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (123, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (124, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (125, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (126, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (127, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (128, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (129, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (130, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (131, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (132, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (133, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (134, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (135, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (136, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (137, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (138, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (139, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (140, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (141, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (142, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (143, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (144, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (145, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (146, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (150, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (151, NULL)
/****** Object:  Table [dbo].[IntegerRangeConstraints]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IntegerRangeConstraints]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[IntegerRangeConstraints](
	[ID] [int] NOT NULL,
	[Max] [int] NOT NULL,
	[Min] [int] NOT NULL,
 CONSTRAINT [PK_IntegerRangeConstraints] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[IntegerRangeConstraints] ([ID], [Max], [Min]) VALUES (147, 4000, 0)
INSERT [dbo].[IntegerRangeConstraints] ([ID], [Max], [Min]) VALUES (148, 4000, 0)
INSERT [dbo].[IntegerRangeConstraints] ([ID], [Max], [Min]) VALUES (149, 4000, 0)
/****** Object:  Table [dbo].[StringConstraints]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StringConstraints]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StringConstraints](
	[ID] [int] NOT NULL,
 CONSTRAINT [PK_StringConstraints] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Visuals_MenuTreeCollection]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Visuals_MenuTreeCollection]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Visuals_MenuTreeCollection](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_Visual] [int] NOT NULL,
	[fk_MenuTree] [int] NOT NULL,
 CONSTRAINT [PK_Visuals_MenuTreeCollection] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Visuals_ContextMenuCollection]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Visuals_ContextMenuCollection]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Visuals_ContextMenuCollection](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_Visual] [int] NOT NULL,
	[fk_ContextMenu] [int] NOT NULL,
 CONSTRAINT [PK_Visuals_ContextMenuCollection] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Visuals_ChildrenCollection]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Visuals_ChildrenCollection]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Visuals_ChildrenCollection](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_Visual] [int] NOT NULL,
	[fk_Children] [int] NOT NULL,
 CONSTRAINT [PK_Visuals_ChildrenCollection] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Templates_MenuCollection]    Script Date: 11/03/2008 15:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Templates_MenuCollection]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Templates_MenuCollection](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_Template] [int] NOT NULL,
	[fk_Menu] [int] NOT NULL,
 CONSTRAINT [PK_Templates_MenuCollection] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckBaseTables]    Script Date: 11/03/2008 15:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CheckBaseTables]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sp_CheckBaseTables] as

/******************* Create Tables if they dont exist *******************/
IF dbo.fn_TableExists(N''[dbo].[ObjectClasses]'') = 0
BEGIN
PRINT ''Creating table [ObjectClasses]''
CREATE TABLE [dbo].[ObjectClasses] (
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](50) NOT NULL,
	[Namespace] nvarchar(100) not null,
	[TableName] nvarchar(50) not null,
	CONSTRAINT [PK_ObjectClasses] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)
) 
END

IF dbo.fn_TableExists(N''[dbo].[ObjectProperties]'') = 0
BEGIN
PRINT ''Creating table [ObjectProperties]''
CREATE TABLE [dbo].[ObjectProperties] (
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_ObjectClass] [int] NOT NULL,
	[PropertyName] [nvarchar](50) NOT NULL,
	[DataType] [nvarchar](150) NOT NULL,
	[IsList] [bit] NOT NULL default(0),
	[IsAssociation] [bit] NOT NULL default(0),
	CONSTRAINT [PK_ObjectProperties] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)
)
END

/******************* Check each Column *******************/
/******************* Only missing Columns are supported yet *******************/
IF dbo.fn_ColumnExists(N''[dbo].[ObjectClasses]'', N''ID'') = 0
BEGIN
	PRINT ''Adding Column [ObjectClasses].[ID]''
	alter table [dbo].[ObjectClasses] add
		[ID] [int] IDENTITY(1,1) NOT NULL,
		CONSTRAINT [PK_ObjectClasses] PRIMARY KEY CLUSTERED 
		(
			[ID] ASC
		)	
END

IF dbo.fn_ColumnExists(N''[dbo].[ObjectClasses]'', N''ClassName'') = 0
BEGIN
	PRINT ''Adding Column [ObjectClasses].[ClassName]''
	alter table [dbo].[ObjectClasses] add
		[ClassName] [nvarchar](50) NOT NULL
END

IF dbo.fn_ColumnExists(N''[dbo].[ObjectClasses]'', N''ServerObject'') = 1
BEGIN
	PRINT ''Dropping Column [ObjectClasses].[ServerObject]''
	alter table [dbo].[ObjectClasses] drop column
		[ServerObject]
END

IF dbo.fn_ColumnExists(N''[dbo].[ObjectClasses]'', N''ClientObject'') = 1
BEGIN
	PRINT ''Dropping Column [ObjectClasses].[ClientObject]''
	alter table [dbo].[ObjectClasses] drop column
		[ClientObject]
END

IF dbo.fn_ColumnExists(N''[dbo].[ObjectClasses]'', N''DataObject'') = 1
BEGIN
	PRINT ''Dropping Column [ObjectClasses].[DataObject]''
	alter table [dbo].[ObjectClasses] drop column
		[DataObject]
END

IF dbo.fn_ColumnExists(N''[dbo].[ObjectClasses]'', N''Namespace'') = 0
BEGIN
	PRINT ''Adding Column [ObjectClasses].[Namespace]''
	alter table [dbo].[ObjectClasses] add
		[Namespace] nvarchar(100) not null
END

IF dbo.fn_ColumnExists(N''[dbo].[ObjectClasses]'', N''TableName'') = 0
BEGIN
	PRINT ''Adding Column [ObjectClasses].[TableName]''
	alter table [dbo].[ObjectClasses] add
		[TableName] nvarchar(50) not null
END

-------------

IF dbo.fn_ColumnExists(N''[dbo].[ObjectProperties]'', N''ID'') = 0
BEGIN
	PRINT ''Adding Column [ObjectProperties].[ID]''
	alter table [dbo].[ObjectProperties] add
		[ID] [int] IDENTITY(1,1) NOT NULL,
		CONSTRAINT [PK_ObjectProperties] PRIMARY KEY CLUSTERED 
		(
			[ID] ASC
		)
END

IF dbo.fn_ColumnExists(N''[dbo].[ObjectProperties]'', N''fk_ObjectClass'') = 0
BEGIN
	PRINT ''Adding Column [ObjectProperties].[fk_ObjectClass]''
	alter table [dbo].[ObjectProperties] add
		[fk_ObjectClass] [int] NOT NULL
END

IF dbo.fn_ColumnExists(N''[dbo].[ObjectProperties]'', N''PropertyName'') = 0
BEGIN
	PRINT ''Adding Column [ObjectProperties].[PropertyName]''
	alter table [dbo].[ObjectProperties] add
		[PropertyName] [nvarchar](50) NOT NULL
END

IF dbo.fn_ColumnExists(N''[dbo].[ObjectProperties]'', N''DataType'') = 0
BEGIN
	PRINT ''Adding Column [ObjectProperties].[DataType]''
	alter table [dbo].[ObjectProperties] add
		[DataType] [nvarchar](150) NOT NULL
END

IF dbo.fn_ColumnExists(N''[dbo].[ObjectProperties]'', N''IsList'') = 0
BEGIN
	PRINT ''Adding Column [ObjectProperties].[IsList]''
	alter table [dbo].[ObjectProperties] add
		[IsList] [bit] NOT NULL default(0)
END

IF dbo.fn_ColumnExists(N''[dbo].[ObjectProperties]'', N''IsAssociation'') = 0
BEGIN
	PRINT ''Adding Column [ObjectProperties].[IsAssociation]''
	alter table [dbo].[ObjectProperties] add
		[IsAssociation] [bit] NOT NULL default(0)
END

IF dbo.fn_ColumnExists(N''[dbo].[ObjectProperties]'', N''AssociationClass'') = 1
BEGIN
	PRINT ''Dropping Column [ObjectProperties].[AssociationClass]''
	alter table [dbo].[ObjectProperties] drop column
		AssociationClass
END


/******************* Check Content of ObjectClass *******************/
exec sp_CheckObjectClass N''Kistl.App.Base'', N''ObjectClass'', N''ObjectClasses''
exec sp_CheckObjectClass N''Kistl.App.Base'', N''ObjectProperty'', N''ObjectProperties''

/******************* Check Content of ObjectProperty *******************/
exec sp_CheckObjectProperty N''Kistl.App.Base'', N''ObjectClass'', N''ClassName'', N''System.String'', 0, 0
exec sp_CheckObjectProperty N''Kistl.App.Base'', N''ObjectClass'', N''Namespace'', N''System.String'', 0, 0
exec sp_CheckObjectProperty N''Kistl.App.Base'', N''ObjectClass'', N''TableName'', N''System.String'', 0, 0
exec sp_CheckObjectProperty N''Kistl.App.Base'', N''ObjectClass'', N''Properties'', N''Kistl.App.Base.ObjectProperty'', 1, 1

exec sp_DropObjectProperty N''Kistl.App.Base'', N''ObjectClass'', N''ServerObject''
exec sp_DropObjectProperty N''Kistl.App.Base'', N''ObjectClass'', N''ClientObject''
exec sp_DropObjectProperty N''Kistl.App.Base'', N''ObjectClass'', N''DataObject''


exec sp_CheckObjectProperty N''Kistl.App.Base'', N''ObjectProperty'', N''fk_ObjectClass'', N''System.Int32'', 0, 1
exec sp_CheckObjectProperty N''Kistl.App.Base'', N''ObjectProperty'', N''PropertyName'', N''System.String'', 0, 0
exec sp_CheckObjectProperty N''Kistl.App.Base'', N''ObjectProperty'', N''DataType'', N''System.String'', 0, 0
exec sp_CheckObjectProperty N''Kistl.App.Base'', N''ObjectProperty'', N''IsList'', N''System.Boolean'', 0, 0
exec sp_CheckObjectProperty N''Kistl.App.Base'', N''ObjectProperty'', N''IsAssociation'', N''System.Boolean'', 0, 0

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DropBaseTables]    Script Date: 11/03/2008 15:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_DropBaseTables]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[sp_DropBaseTables] 
as

IF dbo.fn_TableExists(N''[dbo].[ObjectClasses]'') = 1
BEGIN
	drop table dbo.ObjectProperties
END


IF dbo.fn_TableExists(N''[dbo].[ObjectProperties]'') = 1
BEGIN
	drop table dbo.ObjectClasses
END
' 
END
GO
/****** Object:  Default [DF__ObjectCla__Table__0F975522]    Script Date: 11/03/2008 15:18:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__ObjectCla__Table__0F975522]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__ObjectCla__Table__0F975522]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ObjectClasses] ADD  CONSTRAINT [DF__ObjectCla__Table__0F975522]  DEFAULT ('NewTable') FOR [TableName]
END


End
GO
/****** Object:  ForeignKey [FK_Assembly_Module_fk_Module]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Assembly_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[Assemblies]'))
ALTER TABLE [dbo].[Assemblies]  WITH CHECK ADD  CONSTRAINT [FK_Assembly_Module_fk_Module] FOREIGN KEY([fk_Module])
REFERENCES [dbo].[Modules] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Assembly_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[Assemblies]'))
ALTER TABLE [dbo].[Assemblies] CHECK CONSTRAINT [FK_Assembly_Module_fk_Module]
GO
/****** Object:  ForeignKey [FK_Auftrag_Kunde_fk_Kunde]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Auftrag_Kunde_fk_Kunde]') AND parent_object_id = OBJECT_ID(N'[dbo].[Auftraege]'))
ALTER TABLE [dbo].[Auftraege]  WITH CHECK ADD  CONSTRAINT [FK_Auftrag_Kunde_fk_Kunde] FOREIGN KEY([fk_Kunde])
REFERENCES [dbo].[Kunden] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Auftrag_Kunde_fk_Kunde]') AND parent_object_id = OBJECT_ID(N'[dbo].[Auftraege]'))
ALTER TABLE [dbo].[Auftraege] CHECK CONSTRAINT [FK_Auftrag_Kunde_fk_Kunde]
GO
/****** Object:  ForeignKey [FK_Auftrag_Mitarbeiter_fk_Mitarbeiter]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Auftrag_Mitarbeiter_fk_Mitarbeiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[Auftraege]'))
ALTER TABLE [dbo].[Auftraege]  WITH CHECK ADD  CONSTRAINT [FK_Auftrag_Mitarbeiter_fk_Mitarbeiter] FOREIGN KEY([fk_Mitarbeiter])
REFERENCES [dbo].[Mitarbeiter] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Auftrag_Mitarbeiter_fk_Mitarbeiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[Auftraege]'))
ALTER TABLE [dbo].[Auftraege] CHECK CONSTRAINT [FK_Auftrag_Mitarbeiter_fk_Mitarbeiter]
GO
/****** Object:  ForeignKey [FK_Auftrag_Projekt_fk_Projekt]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Auftrag_Projekt_fk_Projekt]') AND parent_object_id = OBJECT_ID(N'[dbo].[Auftraege]'))
ALTER TABLE [dbo].[Auftraege]  WITH CHECK ADD  CONSTRAINT [FK_Auftrag_Projekt_fk_Projekt] FOREIGN KEY([fk_Projekt])
REFERENCES [dbo].[Projekte] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Auftrag_Projekt_fk_Projekt]') AND parent_object_id = OBJECT_ID(N'[dbo].[Auftraege]'))
ALTER TABLE [dbo].[Auftraege] CHECK CONSTRAINT [FK_Auftrag_Projekt_fk_Projekt]
GO
/****** Object:  ForeignKey [FK_BackReferenceProperty_BaseProperty_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BackReferenceProperty_BaseProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[BackReferenceProperties]'))
ALTER TABLE [dbo].[BackReferenceProperties]  WITH CHECK ADD  CONSTRAINT [FK_BackReferenceProperty_BaseProperty_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[BaseProperties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BackReferenceProperty_BaseProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[BackReferenceProperties]'))
ALTER TABLE [dbo].[BackReferenceProperties] CHECK CONSTRAINT [FK_BackReferenceProperty_BaseProperty_ID]
GO
/****** Object:  ForeignKey [FK_BackReferenceProperty_ObjectReferenceProperty_fk_ReferenceProperty]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BackReferenceProperty_ObjectReferenceProperty_fk_ReferenceProperty]') AND parent_object_id = OBJECT_ID(N'[dbo].[BackReferenceProperties]'))
ALTER TABLE [dbo].[BackReferenceProperties]  WITH CHECK ADD  CONSTRAINT [FK_BackReferenceProperty_ObjectReferenceProperty_fk_ReferenceProperty] FOREIGN KEY([fk_ReferenceProperty])
REFERENCES [dbo].[ObjectReferenceProperties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BackReferenceProperty_ObjectReferenceProperty_fk_ReferenceProperty]') AND parent_object_id = OBJECT_ID(N'[dbo].[BackReferenceProperties]'))
ALTER TABLE [dbo].[BackReferenceProperties] CHECK CONSTRAINT [FK_BackReferenceProperty_ObjectReferenceProperty_fk_ReferenceProperty]
GO
/****** Object:  ForeignKey [FK_BaseParameter_Method_fk_Method]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BaseParameter_Method_fk_Method]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseParameters]'))
ALTER TABLE [dbo].[BaseParameters]  WITH CHECK ADD  CONSTRAINT [FK_BaseParameter_Method_fk_Method] FOREIGN KEY([fk_Method])
REFERENCES [dbo].[Methods] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BaseParameter_Method_fk_Method]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseParameters]'))
ALTER TABLE [dbo].[BaseParameters] CHECK CONSTRAINT [FK_BaseParameter_Method_fk_Method]
GO
/****** Object:  ForeignKey [FK_BaseParameter_Module_fk_Module]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BaseParameter_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseParameters]'))
ALTER TABLE [dbo].[BaseParameters]  WITH CHECK ADD  CONSTRAINT [FK_BaseParameter_Module_fk_Module] FOREIGN KEY([fk_Module])
REFERENCES [dbo].[Modules] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BaseParameter_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseParameters]'))
ALTER TABLE [dbo].[BaseParameters] CHECK CONSTRAINT [FK_BaseParameter_Module_fk_Module]
GO
/****** Object:  ForeignKey [FK_BaseProperty_DataType_fk_ObjectClass]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BaseProperty_DataType_fk_ObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseProperties]'))
ALTER TABLE [dbo].[BaseProperties]  WITH CHECK ADD  CONSTRAINT [FK_BaseProperty_DataType_fk_ObjectClass] FOREIGN KEY([fk_ObjectClass])
REFERENCES [dbo].[DataTypes] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BaseProperty_DataType_fk_ObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseProperties]'))
ALTER TABLE [dbo].[BaseProperties] CHECK CONSTRAINT [FK_BaseProperty_DataType_fk_ObjectClass]
GO
/****** Object:  ForeignKey [FK_BaseProperty_Module_fk_Module]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BaseProperty_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseProperties]'))
ALTER TABLE [dbo].[BaseProperties]  WITH CHECK ADD  CONSTRAINT [FK_BaseProperty_Module_fk_Module] FOREIGN KEY([fk_Module])
REFERENCES [dbo].[Modules] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BaseProperty_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseProperties]'))
ALTER TABLE [dbo].[BaseProperties] CHECK CONSTRAINT [FK_BaseProperty_Module_fk_Module]
GO
/****** Object:  ForeignKey [FK_BaseProperty_ConstraintsCollectionEntry_BaseProperty_fk_BaseProperty]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BaseProperty_ConstraintsCollectionEntry_BaseProperty_fk_BaseProperty]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseProperties_ConstraintsCollection]'))
ALTER TABLE [dbo].[BaseProperties_ConstraintsCollection]  WITH CHECK ADD  CONSTRAINT [FK_BaseProperty_ConstraintsCollectionEntry_BaseProperty_fk_BaseProperty] FOREIGN KEY([fk_BaseProperty])
REFERENCES [dbo].[BaseProperties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BaseProperty_ConstraintsCollectionEntry_BaseProperty_fk_BaseProperty]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseProperties_ConstraintsCollection]'))
ALTER TABLE [dbo].[BaseProperties_ConstraintsCollection] CHECK CONSTRAINT [FK_BaseProperty_ConstraintsCollectionEntry_BaseProperty_fk_BaseProperty]
GO
/****** Object:  ForeignKey [FK_BaseProperty_ConstraintsCollectionEntry_Constraint_fk_Constraints]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BaseProperty_ConstraintsCollectionEntry_Constraint_fk_Constraints]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseProperties_ConstraintsCollection]'))
ALTER TABLE [dbo].[BaseProperties_ConstraintsCollection]  WITH CHECK ADD  CONSTRAINT [FK_BaseProperty_ConstraintsCollectionEntry_Constraint_fk_Constraints] FOREIGN KEY([fk_Constraints])
REFERENCES [dbo].[Constraints] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BaseProperty_ConstraintsCollectionEntry_Constraint_fk_Constraints]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseProperties_ConstraintsCollection]'))
ALTER TABLE [dbo].[BaseProperties_ConstraintsCollection] CHECK CONSTRAINT [FK_BaseProperty_ConstraintsCollectionEntry_Constraint_fk_Constraints]
GO
/****** Object:  ForeignKey [FK_BoolParameter_BaseParameter_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BoolParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[BoolParameters]'))
ALTER TABLE [dbo].[BoolParameters]  WITH CHECK ADD  CONSTRAINT [FK_BoolParameter_BaseParameter_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[BaseParameters] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BoolParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[BoolParameters]'))
ALTER TABLE [dbo].[BoolParameters] CHECK CONSTRAINT [FK_BoolParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_BoolProperty_ValueTypeProperty_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BoolProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[BoolProperties]'))
ALTER TABLE [dbo].[BoolProperties]  WITH CHECK ADD  CONSTRAINT [FK_BoolProperty_ValueTypeProperty_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[ValueTypeProperties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BoolProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[BoolProperties]'))
ALTER TABLE [dbo].[BoolProperties] CHECK CONSTRAINT [FK_BoolProperty_ValueTypeProperty_ID]
GO
/****** Object:  ForeignKey [FK_CLRObjectParameter_Assembly_fk_Assembly]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLRObjectParameter_Assembly_fk_Assembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLRObjectParameters]'))
ALTER TABLE [dbo].[CLRObjectParameters]  WITH CHECK ADD  CONSTRAINT [FK_CLRObjectParameter_Assembly_fk_Assembly] FOREIGN KEY([fk_Assembly])
REFERENCES [dbo].[Assemblies] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLRObjectParameter_Assembly_fk_Assembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLRObjectParameters]'))
ALTER TABLE [dbo].[CLRObjectParameters] CHECK CONSTRAINT [FK_CLRObjectParameter_Assembly_fk_Assembly]
GO
/****** Object:  ForeignKey [FK_CLRObjectParameter_BaseParameter_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLRObjectParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLRObjectParameters]'))
ALTER TABLE [dbo].[CLRObjectParameters]  WITH CHECK ADD  CONSTRAINT [FK_CLRObjectParameter_BaseParameter_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[BaseParameters] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLRObjectParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLRObjectParameters]'))
ALTER TABLE [dbo].[CLRObjectParameters] CHECK CONSTRAINT [FK_CLRObjectParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_Constraint_BaseProperty_fk_ConstrainedProperty]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Constraint_BaseProperty_fk_ConstrainedProperty]') AND parent_object_id = OBJECT_ID(N'[dbo].[Constraints]'))
ALTER TABLE [dbo].[Constraints]  WITH CHECK ADD  CONSTRAINT [FK_Constraint_BaseProperty_fk_ConstrainedProperty] FOREIGN KEY([fk_ConstrainedProperty])
REFERENCES [dbo].[BaseProperties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Constraint_BaseProperty_fk_ConstrainedProperty]') AND parent_object_id = OBJECT_ID(N'[dbo].[Constraints]'))
ALTER TABLE [dbo].[Constraints] CHECK CONSTRAINT [FK_Constraint_BaseProperty_fk_ConstrainedProperty]
GO
/****** Object:  ForeignKey [FK_ControlInfo_Assembly_fk_Assembly]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ControlInfo_Assembly_fk_Assembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[ControlInfos]'))
ALTER TABLE [dbo].[ControlInfos]  WITH CHECK ADD  CONSTRAINT [FK_ControlInfo_Assembly_fk_Assembly] FOREIGN KEY([fk_Assembly])
REFERENCES [dbo].[Assemblies] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ControlInfo_Assembly_fk_Assembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[ControlInfos]'))
ALTER TABLE [dbo].[ControlInfos] CHECK CONSTRAINT [FK_ControlInfo_Assembly_fk_Assembly]
GO
/****** Object:  ForeignKey [FK_DataType_Icon_fk_DefaultIcon]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DataType_Icon_fk_DefaultIcon]') AND parent_object_id = OBJECT_ID(N'[dbo].[DataTypes]'))
ALTER TABLE [dbo].[DataTypes]  WITH CHECK ADD  CONSTRAINT [FK_DataType_Icon_fk_DefaultIcon] FOREIGN KEY([fk_DefaultIcon])
REFERENCES [dbo].[Icons] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DataType_Icon_fk_DefaultIcon]') AND parent_object_id = OBJECT_ID(N'[dbo].[DataTypes]'))
ALTER TABLE [dbo].[DataTypes] CHECK CONSTRAINT [FK_DataType_Icon_fk_DefaultIcon]
GO
/****** Object:  ForeignKey [FK_DataType_Module_fk_Module]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DataType_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[DataTypes]'))
ALTER TABLE [dbo].[DataTypes]  WITH CHECK ADD  CONSTRAINT [FK_DataType_Module_fk_Module] FOREIGN KEY([fk_Module])
REFERENCES [dbo].[Modules] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DataType_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[DataTypes]'))
ALTER TABLE [dbo].[DataTypes] CHECK CONSTRAINT [FK_DataType_Module_fk_Module]
GO
/****** Object:  ForeignKey [FK_DateTimeParameter_BaseParameter_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DateTimeParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[DateTimeParameters]'))
ALTER TABLE [dbo].[DateTimeParameters]  WITH CHECK ADD  CONSTRAINT [FK_DateTimeParameter_BaseParameter_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[BaseParameters] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DateTimeParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[DateTimeParameters]'))
ALTER TABLE [dbo].[DateTimeParameters] CHECK CONSTRAINT [FK_DateTimeParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_DateTimeProperty_ValueTypeProperty_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DateTimeProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[DateTimeProperties]'))
ALTER TABLE [dbo].[DateTimeProperties]  WITH CHECK ADD  CONSTRAINT [FK_DateTimeProperty_ValueTypeProperty_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[ValueTypeProperties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DateTimeProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[DateTimeProperties]'))
ALTER TABLE [dbo].[DateTimeProperties] CHECK CONSTRAINT [FK_DateTimeProperty_ValueTypeProperty_ID]
GO
/****** Object:  ForeignKey [FK_DoubleParameter_BaseParameter_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoubleParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoubleParameters]'))
ALTER TABLE [dbo].[DoubleParameters]  WITH CHECK ADD  CONSTRAINT [FK_DoubleParameter_BaseParameter_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[BaseParameters] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoubleParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoubleParameters]'))
ALTER TABLE [dbo].[DoubleParameters] CHECK CONSTRAINT [FK_DoubleParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_DoubleProperty_ValueTypeProperty_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoubleProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoubleProperties]'))
ALTER TABLE [dbo].[DoubleProperties]  WITH CHECK ADD  CONSTRAINT [FK_DoubleProperty_ValueTypeProperty_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[ValueTypeProperties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoubleProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoubleProperties]'))
ALTER TABLE [dbo].[DoubleProperties] CHECK CONSTRAINT [FK_DoubleProperty_ValueTypeProperty_ID]
GO
/****** Object:  ForeignKey [FK_EnumerationEntry_Enumeration_fk_Enumeration]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EnumerationEntry_Enumeration_fk_Enumeration]') AND parent_object_id = OBJECT_ID(N'[dbo].[EnumerationEntries]'))
ALTER TABLE [dbo].[EnumerationEntries]  WITH CHECK ADD  CONSTRAINT [FK_EnumerationEntry_Enumeration_fk_Enumeration] FOREIGN KEY([fk_Enumeration])
REFERENCES [dbo].[Enumerations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EnumerationEntry_Enumeration_fk_Enumeration]') AND parent_object_id = OBJECT_ID(N'[dbo].[EnumerationEntries]'))
ALTER TABLE [dbo].[EnumerationEntries] CHECK CONSTRAINT [FK_EnumerationEntry_Enumeration_fk_Enumeration]
GO
/****** Object:  ForeignKey [FK_EnumerationProperty_Enumeration_fk_Enumeration]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EnumerationProperty_Enumeration_fk_Enumeration]') AND parent_object_id = OBJECT_ID(N'[dbo].[EnumerationProperties]'))
ALTER TABLE [dbo].[EnumerationProperties]  WITH CHECK ADD  CONSTRAINT [FK_EnumerationProperty_Enumeration_fk_Enumeration] FOREIGN KEY([fk_Enumeration])
REFERENCES [dbo].[Enumerations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EnumerationProperty_Enumeration_fk_Enumeration]') AND parent_object_id = OBJECT_ID(N'[dbo].[EnumerationProperties]'))
ALTER TABLE [dbo].[EnumerationProperties] CHECK CONSTRAINT [FK_EnumerationProperty_Enumeration_fk_Enumeration]
GO
/****** Object:  ForeignKey [FK_EnumerationProperty_ValueTypeProperty_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EnumerationProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[EnumerationProperties]'))
ALTER TABLE [dbo].[EnumerationProperties]  WITH CHECK ADD  CONSTRAINT [FK_EnumerationProperty_ValueTypeProperty_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[ValueTypeProperties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EnumerationProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[EnumerationProperties]'))
ALTER TABLE [dbo].[EnumerationProperties] CHECK CONSTRAINT [FK_EnumerationProperty_ValueTypeProperty_ID]
GO
/****** Object:  ForeignKey [FK_Enumeration_DataType_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Enumeration_DataType_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Enumerations]'))
ALTER TABLE [dbo].[Enumerations]  WITH CHECK ADD  CONSTRAINT [FK_Enumeration_DataType_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[DataTypes] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Enumeration_DataType_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Enumerations]'))
ALTER TABLE [dbo].[Enumerations] CHECK CONSTRAINT [FK_Enumeration_DataType_ID]
GO
/****** Object:  ForeignKey [FK_IntegerRangeConstraint_Constraint_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_IntegerRangeConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[IntegerRangeConstraints]'))
ALTER TABLE [dbo].[IntegerRangeConstraints]  WITH CHECK ADD  CONSTRAINT [FK_IntegerRangeConstraint_Constraint_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[Constraints] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_IntegerRangeConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[IntegerRangeConstraints]'))
ALTER TABLE [dbo].[IntegerRangeConstraints] CHECK CONSTRAINT [FK_IntegerRangeConstraint_Constraint_ID]
GO
/****** Object:  ForeignKey [FK_Interface_DataType_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Interface_DataType_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Interfaces]'))
ALTER TABLE [dbo].[Interfaces]  WITH CHECK ADD  CONSTRAINT [FK_Interface_DataType_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[DataTypes] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Interface_DataType_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Interfaces]'))
ALTER TABLE [dbo].[Interfaces] CHECK CONSTRAINT [FK_Interface_DataType_ID]
GO
/****** Object:  ForeignKey [FK_IntParameter_BaseParameter_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_IntParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[IntParameters]'))
ALTER TABLE [dbo].[IntParameters]  WITH CHECK ADD  CONSTRAINT [FK_IntParameter_BaseParameter_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[BaseParameters] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_IntParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[IntParameters]'))
ALTER TABLE [dbo].[IntParameters] CHECK CONSTRAINT [FK_IntParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_IntProperty_ValueTypeProperty_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_IntProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[IntProperties]'))
ALTER TABLE [dbo].[IntProperties]  WITH CHECK ADD  CONSTRAINT [FK_IntProperty_ValueTypeProperty_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[ValueTypeProperties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_IntProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[IntProperties]'))
ALTER TABLE [dbo].[IntProperties] CHECK CONSTRAINT [FK_IntProperty_ValueTypeProperty_ID]
GO
/****** Object:  ForeignKey [FK_Kostenstelle_Zeitkonto_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Kostenstelle_Zeitkonto_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Kostenstellen]'))
ALTER TABLE [dbo].[Kostenstellen]  WITH CHECK ADD  CONSTRAINT [FK_Kostenstelle_Zeitkonto_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[Zeitkonten] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Kostenstelle_Zeitkonto_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Kostenstellen]'))
ALTER TABLE [dbo].[Kostenstellen] CHECK CONSTRAINT [FK_Kostenstelle_Zeitkonto_ID]
GO
/****** Object:  ForeignKey [FK_Kostentraeger_Projekt_fk_Projekt]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Kostentraeger_Projekt_fk_Projekt]') AND parent_object_id = OBJECT_ID(N'[dbo].[Kostentraeger]'))
ALTER TABLE [dbo].[Kostentraeger]  WITH CHECK ADD  CONSTRAINT [FK_Kostentraeger_Projekt_fk_Projekt] FOREIGN KEY([fk_Projekt])
REFERENCES [dbo].[Projekte] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Kostentraeger_Projekt_fk_Projekt]') AND parent_object_id = OBJECT_ID(N'[dbo].[Kostentraeger]'))
ALTER TABLE [dbo].[Kostentraeger] CHECK CONSTRAINT [FK_Kostentraeger_Projekt_fk_Projekt]
GO
/****** Object:  ForeignKey [FK_Kostentraeger_Zeitkonto_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Kostentraeger_Zeitkonto_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Kostentraeger]'))
ALTER TABLE [dbo].[Kostentraeger]  WITH CHECK ADD  CONSTRAINT [FK_Kostentraeger_Zeitkonto_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[Zeitkonten] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Kostentraeger_Zeitkonto_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Kostentraeger]'))
ALTER TABLE [dbo].[Kostentraeger] CHECK CONSTRAINT [FK_Kostentraeger_Zeitkonto_ID]
GO
/****** Object:  ForeignKey [FK_Kunde_EMailsCollectionEntry_Kunde_fk_Kunde]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Kunde_EMailsCollectionEntry_Kunde_fk_Kunde]') AND parent_object_id = OBJECT_ID(N'[dbo].[Kunden_EMailsCollection]'))
ALTER TABLE [dbo].[Kunden_EMailsCollection]  WITH CHECK ADD  CONSTRAINT [FK_Kunde_EMailsCollectionEntry_Kunde_fk_Kunde] FOREIGN KEY([fk_Kunde])
REFERENCES [dbo].[Kunden] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Kunde_EMailsCollectionEntry_Kunde_fk_Kunde]') AND parent_object_id = OBJECT_ID(N'[dbo].[Kunden_EMailsCollection]'))
ALTER TABLE [dbo].[Kunden_EMailsCollection] CHECK CONSTRAINT [FK_Kunde_EMailsCollectionEntry_Kunde_fk_Kunde]
GO
/****** Object:  ForeignKey [FK_MethodInvocationConstraint_Constraint_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocationConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocationConstraints]'))
ALTER TABLE [dbo].[MethodInvocationConstraints]  WITH CHECK ADD  CONSTRAINT [FK_MethodInvocationConstraint_Constraint_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[Constraints] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocationConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocationConstraints]'))
ALTER TABLE [dbo].[MethodInvocationConstraints] CHECK CONSTRAINT [FK_MethodInvocationConstraint_Constraint_ID]
GO
/****** Object:  ForeignKey [FK_MethodInvocation_Assembly_fk_Assembly]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocation_Assembly_fk_Assembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocations]'))
ALTER TABLE [dbo].[MethodInvocations]  WITH CHECK ADD  CONSTRAINT [FK_MethodInvocation_Assembly_fk_Assembly] FOREIGN KEY([fk_Assembly])
REFERENCES [dbo].[Assemblies] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocation_Assembly_fk_Assembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocations]'))
ALTER TABLE [dbo].[MethodInvocations] CHECK CONSTRAINT [FK_MethodInvocation_Assembly_fk_Assembly]
GO
/****** Object:  ForeignKey [FK_MethodInvocation_DataType_fk_InvokeOnObjectClass]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocation_DataType_fk_InvokeOnObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocations]'))
ALTER TABLE [dbo].[MethodInvocations]  WITH CHECK ADD  CONSTRAINT [FK_MethodInvocation_DataType_fk_InvokeOnObjectClass] FOREIGN KEY([fk_InvokeOnObjectClass])
REFERENCES [dbo].[DataTypes] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocation_DataType_fk_InvokeOnObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocations]'))
ALTER TABLE [dbo].[MethodInvocations] CHECK CONSTRAINT [FK_MethodInvocation_DataType_fk_InvokeOnObjectClass]
GO
/****** Object:  ForeignKey [FK_MethodInvocation_Method_fk_Method]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocation_Method_fk_Method]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocations]'))
ALTER TABLE [dbo].[MethodInvocations]  WITH CHECK ADD  CONSTRAINT [FK_MethodInvocation_Method_fk_Method] FOREIGN KEY([fk_Method])
REFERENCES [dbo].[Methods] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocation_Method_fk_Method]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocations]'))
ALTER TABLE [dbo].[MethodInvocations] CHECK CONSTRAINT [FK_MethodInvocation_Method_fk_Method]
GO
/****** Object:  ForeignKey [FK_MethodInvocation_Module_fk_Module]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocation_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocations]'))
ALTER TABLE [dbo].[MethodInvocations]  WITH CHECK ADD  CONSTRAINT [FK_MethodInvocation_Module_fk_Module] FOREIGN KEY([fk_Module])
REFERENCES [dbo].[Modules] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocation_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocations]'))
ALTER TABLE [dbo].[MethodInvocations] CHECK CONSTRAINT [FK_MethodInvocation_Module_fk_Module]
GO
/****** Object:  ForeignKey [FK_Method_DataType_fk_ObjectClass]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Method_DataType_fk_ObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[Methods]'))
ALTER TABLE [dbo].[Methods]  WITH CHECK ADD  CONSTRAINT [FK_Method_DataType_fk_ObjectClass] FOREIGN KEY([fk_ObjectClass])
REFERENCES [dbo].[DataTypes] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Method_DataType_fk_ObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[Methods]'))
ALTER TABLE [dbo].[Methods] CHECK CONSTRAINT [FK_Method_DataType_fk_ObjectClass]
GO
/****** Object:  ForeignKey [FK_Method_Module_fk_Module]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Method_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[Methods]'))
ALTER TABLE [dbo].[Methods]  WITH CHECK ADD  CONSTRAINT [FK_Method_Module_fk_Module] FOREIGN KEY([fk_Module])
REFERENCES [dbo].[Modules] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Method_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[Methods]'))
ALTER TABLE [dbo].[Methods] CHECK CONSTRAINT [FK_Method_Module_fk_Module]
GO
/****** Object:  ForeignKey [FK_NotNullableConstraint_Constraint_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NotNullableConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[NotNullableConstraints]'))
ALTER TABLE [dbo].[NotNullableConstraints]  WITH CHECK ADD  CONSTRAINT [FK_NotNullableConstraint_Constraint_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[Constraints] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NotNullableConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[NotNullableConstraints]'))
ALTER TABLE [dbo].[NotNullableConstraints] CHECK CONSTRAINT [FK_NotNullableConstraint_Constraint_ID]
GO
/****** Object:  ForeignKey [FK_ObjectClass_DataType_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_DataType_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses]'))
ALTER TABLE [dbo].[ObjectClasses]  WITH CHECK ADD  CONSTRAINT [FK_ObjectClass_DataType_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[DataTypes] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_DataType_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses]'))
ALTER TABLE [dbo].[ObjectClasses] CHECK CONSTRAINT [FK_ObjectClass_DataType_ID]
GO
/****** Object:  ForeignKey [FK_ObjectClass_ObjectClass_fk_BaseObjectClass]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_ObjectClass_fk_BaseObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses]'))
ALTER TABLE [dbo].[ObjectClasses]  WITH CHECK ADD  CONSTRAINT [FK_ObjectClass_ObjectClass_fk_BaseObjectClass] FOREIGN KEY([fk_BaseObjectClass])
REFERENCES [dbo].[ObjectClasses] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_ObjectClass_fk_BaseObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses]'))
ALTER TABLE [dbo].[ObjectClasses] CHECK CONSTRAINT [FK_ObjectClass_ObjectClass_fk_BaseObjectClass]
GO
/****** Object:  ForeignKey [FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_fk_ImplementsInterfaces]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_fk_ImplementsInterfaces]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses_ImplementsInterfacesCollection]'))
ALTER TABLE [dbo].[ObjectClasses_ImplementsInterfacesCollection]  WITH CHECK ADD  CONSTRAINT [FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_fk_ImplementsInterfaces] FOREIGN KEY([fk_ImplementsInterfaces])
REFERENCES [dbo].[Interfaces] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_fk_ImplementsInterfaces]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses_ImplementsInterfacesCollection]'))
ALTER TABLE [dbo].[ObjectClasses_ImplementsInterfacesCollection] CHECK CONSTRAINT [FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_fk_ImplementsInterfaces]
GO
/****** Object:  ForeignKey [FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_ObjectClass]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_ObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses_ImplementsInterfacesCollection]'))
ALTER TABLE [dbo].[ObjectClasses_ImplementsInterfacesCollection]  WITH CHECK ADD  CONSTRAINT [FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_ObjectClass] FOREIGN KEY([fk_ObjectClass])
REFERENCES [dbo].[ObjectClasses] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_ObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses_ImplementsInterfacesCollection]'))
ALTER TABLE [dbo].[ObjectClasses_ImplementsInterfacesCollection] CHECK CONSTRAINT [FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_ObjectClass]
GO
/****** Object:  ForeignKey [FK_ObjectParameter_BaseParameter_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectParameters]'))
ALTER TABLE [dbo].[ObjectParameters]  WITH CHECK ADD  CONSTRAINT [FK_ObjectParameter_BaseParameter_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[BaseParameters] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectParameters]'))
ALTER TABLE [dbo].[ObjectParameters] CHECK CONSTRAINT [FK_ObjectParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_ObjectParameter_DataType_fk_DataType]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectParameter_DataType_fk_DataType]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectParameters]'))
ALTER TABLE [dbo].[ObjectParameters]  WITH CHECK ADD  CONSTRAINT [FK_ObjectParameter_DataType_fk_DataType] FOREIGN KEY([fk_DataType])
REFERENCES [dbo].[DataTypes] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectParameter_DataType_fk_DataType]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectParameters]'))
ALTER TABLE [dbo].[ObjectParameters] CHECK CONSTRAINT [FK_ObjectParameter_DataType_fk_DataType]
GO
/****** Object:  ForeignKey [FK_ObjectReferenceProperty_ObjectClass_fk_ReferenceObjectClass]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectReferenceProperty_ObjectClass_fk_ReferenceObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectReferenceProperties]'))
ALTER TABLE [dbo].[ObjectReferenceProperties]  WITH CHECK ADD  CONSTRAINT [FK_ObjectReferenceProperty_ObjectClass_fk_ReferenceObjectClass] FOREIGN KEY([fk_ReferenceObjectClass])
REFERENCES [dbo].[ObjectClasses] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectReferenceProperty_ObjectClass_fk_ReferenceObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectReferenceProperties]'))
ALTER TABLE [dbo].[ObjectReferenceProperties] CHECK CONSTRAINT [FK_ObjectReferenceProperty_ObjectClass_fk_ReferenceObjectClass]
GO
/****** Object:  ForeignKey [FK_ObjectReferenceProperty_Property_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectReferenceProperty_Property_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectReferenceProperties]'))
ALTER TABLE [dbo].[ObjectReferenceProperties]  WITH CHECK ADD  CONSTRAINT [FK_ObjectReferenceProperty_Property_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[Properties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectReferenceProperty_Property_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectReferenceProperties]'))
ALTER TABLE [dbo].[ObjectReferenceProperties] CHECK CONSTRAINT [FK_ObjectReferenceProperty_Property_ID]
GO
/****** Object:  ForeignKey [FK_PresenterInfo_Assembly_fk_DataAssembly]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PresenterInfo_Assembly_fk_DataAssembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[PresenterInfos]'))
ALTER TABLE [dbo].[PresenterInfos]  WITH CHECK ADD  CONSTRAINT [FK_PresenterInfo_Assembly_fk_DataAssembly] FOREIGN KEY([fk_DataAssembly])
REFERENCES [dbo].[Assemblies] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PresenterInfo_Assembly_fk_DataAssembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[PresenterInfos]'))
ALTER TABLE [dbo].[PresenterInfos] CHECK CONSTRAINT [FK_PresenterInfo_Assembly_fk_DataAssembly]
GO
/****** Object:  ForeignKey [FK_PresenterInfo_Assembly_fk_PresenterAssembly]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PresenterInfo_Assembly_fk_PresenterAssembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[PresenterInfos]'))
ALTER TABLE [dbo].[PresenterInfos]  WITH CHECK ADD  CONSTRAINT [FK_PresenterInfo_Assembly_fk_PresenterAssembly] FOREIGN KEY([fk_PresenterAssembly])
REFERENCES [dbo].[Assemblies] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PresenterInfo_Assembly_fk_PresenterAssembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[PresenterInfos]'))
ALTER TABLE [dbo].[PresenterInfos] CHECK CONSTRAINT [FK_PresenterInfo_Assembly_fk_PresenterAssembly]
GO
/****** Object:  ForeignKey [FK_Projekt_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projekt_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projekte_MitarbeiterCollection]'))
ALTER TABLE [dbo].[Projekte_MitarbeiterCollection]  WITH CHECK ADD  CONSTRAINT [FK_Projekt_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter] FOREIGN KEY([fk_Mitarbeiter])
REFERENCES [dbo].[Mitarbeiter] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projekt_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projekte_MitarbeiterCollection]'))
ALTER TABLE [dbo].[Projekte_MitarbeiterCollection] CHECK CONSTRAINT [FK_Projekt_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]
GO
/****** Object:  ForeignKey [FK_Projekt_MitarbeiterCollectionEntry_Projekt_fk_Projekt]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projekt_MitarbeiterCollectionEntry_Projekt_fk_Projekt]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projekte_MitarbeiterCollection]'))
ALTER TABLE [dbo].[Projekte_MitarbeiterCollection]  WITH CHECK ADD  CONSTRAINT [FK_Projekt_MitarbeiterCollectionEntry_Projekt_fk_Projekt] FOREIGN KEY([fk_Projekt])
REFERENCES [dbo].[Projekte] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projekt_MitarbeiterCollectionEntry_Projekt_fk_Projekt]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projekte_MitarbeiterCollection]'))
ALTER TABLE [dbo].[Projekte_MitarbeiterCollection] CHECK CONSTRAINT [FK_Projekt_MitarbeiterCollectionEntry_Projekt_fk_Projekt]
GO
/****** Object:  ForeignKey [FK_Property_BaseProperty_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Property_BaseProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Properties]'))
ALTER TABLE [dbo].[Properties]  WITH CHECK ADD  CONSTRAINT [FK_Property_BaseProperty_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[BaseProperties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Property_BaseProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Properties]'))
ALTER TABLE [dbo].[Properties] CHECK CONSTRAINT [FK_Property_BaseProperty_ID]
GO
/****** Object:  ForeignKey [FK_StringConstraint_Constraint_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StringConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StringConstraints]'))
ALTER TABLE [dbo].[StringConstraints]  WITH CHECK ADD  CONSTRAINT [FK_StringConstraint_Constraint_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[Constraints] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StringConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StringConstraints]'))
ALTER TABLE [dbo].[StringConstraints] CHECK CONSTRAINT [FK_StringConstraint_Constraint_ID]
GO
/****** Object:  ForeignKey [FK_StringParameter_BaseParameter_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StringParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StringParameters]'))
ALTER TABLE [dbo].[StringParameters]  WITH CHECK ADD  CONSTRAINT [FK_StringParameter_BaseParameter_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[BaseParameters] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StringParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StringParameters]'))
ALTER TABLE [dbo].[StringParameters] CHECK CONSTRAINT [FK_StringParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_StringProperty_ValueTypeProperty_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StringProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StringProperties]'))
ALTER TABLE [dbo].[StringProperties]  WITH CHECK ADD  CONSTRAINT [FK_StringProperty_ValueTypeProperty_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[ValueTypeProperties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StringProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StringProperties]'))
ALTER TABLE [dbo].[StringProperties] CHECK CONSTRAINT [FK_StringProperty_ValueTypeProperty_ID]
GO
/****** Object:  ForeignKey [FK_StringRangeConstraint_Constraint_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StringRangeConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StringRangeConstraints]'))
ALTER TABLE [dbo].[StringRangeConstraints]  WITH CHECK ADD  CONSTRAINT [FK_StringRangeConstraint_Constraint_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[Constraints] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StringRangeConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StringRangeConstraints]'))
ALTER TABLE [dbo].[StringRangeConstraints] CHECK CONSTRAINT [FK_StringRangeConstraint_Constraint_ID]
GO
/****** Object:  ForeignKey [FK_StructProperty_Property_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StructProperty_Property_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StructProperties]'))
ALTER TABLE [dbo].[StructProperties]  WITH CHECK ADD  CONSTRAINT [FK_StructProperty_Property_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[Properties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StructProperty_Property_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StructProperties]'))
ALTER TABLE [dbo].[StructProperties] CHECK CONSTRAINT [FK_StructProperty_Property_ID]
GO
/****** Object:  ForeignKey [FK_StructProperty_Struct_fk_StructDefinition]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StructProperty_Struct_fk_StructDefinition]') AND parent_object_id = OBJECT_ID(N'[dbo].[StructProperties]'))
ALTER TABLE [dbo].[StructProperties]  WITH CHECK ADD  CONSTRAINT [FK_StructProperty_Struct_fk_StructDefinition] FOREIGN KEY([fk_StructDefinition])
REFERENCES [dbo].[Structs] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StructProperty_Struct_fk_StructDefinition]') AND parent_object_id = OBJECT_ID(N'[dbo].[StructProperties]'))
ALTER TABLE [dbo].[StructProperties] CHECK CONSTRAINT [FK_StructProperty_Struct_fk_StructDefinition]
GO
/****** Object:  ForeignKey [FK_Struct_DataType_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Struct_DataType_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Structs]'))
ALTER TABLE [dbo].[Structs]  WITH CHECK ADD  CONSTRAINT [FK_Struct_DataType_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[DataTypes] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Struct_DataType_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Structs]'))
ALTER TABLE [dbo].[Structs] CHECK CONSTRAINT [FK_Struct_DataType_ID]
GO
/****** Object:  ForeignKey [FK_Taetigkeit_Mitarbeiter_fk_Mitarbeiter]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Taetigkeit_Mitarbeiter_fk_Mitarbeiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[Taetigkeiten]'))
ALTER TABLE [dbo].[Taetigkeiten]  WITH CHECK ADD  CONSTRAINT [FK_Taetigkeit_Mitarbeiter_fk_Mitarbeiter] FOREIGN KEY([fk_Mitarbeiter])
REFERENCES [dbo].[Mitarbeiter] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Taetigkeit_Mitarbeiter_fk_Mitarbeiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[Taetigkeiten]'))
ALTER TABLE [dbo].[Taetigkeiten] CHECK CONSTRAINT [FK_Taetigkeit_Mitarbeiter_fk_Mitarbeiter]
GO
/****** Object:  ForeignKey [FK_Taetigkeit_TaetigkeitsArt_fk_TaetigkeitsArt]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Taetigkeit_TaetigkeitsArt_fk_TaetigkeitsArt]') AND parent_object_id = OBJECT_ID(N'[dbo].[Taetigkeiten]'))
ALTER TABLE [dbo].[Taetigkeiten]  WITH CHECK ADD  CONSTRAINT [FK_Taetigkeit_TaetigkeitsArt_fk_TaetigkeitsArt] FOREIGN KEY([fk_TaetigkeitsArt])
REFERENCES [dbo].[TaetigkeitsArten] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Taetigkeit_TaetigkeitsArt_fk_TaetigkeitsArt]') AND parent_object_id = OBJECT_ID(N'[dbo].[Taetigkeiten]'))
ALTER TABLE [dbo].[Taetigkeiten] CHECK CONSTRAINT [FK_Taetigkeit_TaetigkeitsArt_fk_TaetigkeitsArt]
GO
/****** Object:  ForeignKey [FK_Taetigkeit_Zeitkonto_fk_Zeitkonto]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Taetigkeit_Zeitkonto_fk_Zeitkonto]') AND parent_object_id = OBJECT_ID(N'[dbo].[Taetigkeiten]'))
ALTER TABLE [dbo].[Taetigkeiten]  WITH CHECK ADD  CONSTRAINT [FK_Taetigkeit_Zeitkonto_fk_Zeitkonto] FOREIGN KEY([fk_Zeitkonto])
REFERENCES [dbo].[Zeitkonten] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Taetigkeit_Zeitkonto_fk_Zeitkonto]') AND parent_object_id = OBJECT_ID(N'[dbo].[Taetigkeiten]'))
ALTER TABLE [dbo].[Taetigkeiten] CHECK CONSTRAINT [FK_Taetigkeit_Zeitkonto_fk_Zeitkonto]
GO
/****** Object:  ForeignKey [FK_Task_Projekt_fk_Projekt]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Task_Projekt_fk_Projekt]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tasks]'))
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Task_Projekt_fk_Projekt] FOREIGN KEY([fk_Projekt])
REFERENCES [dbo].[Projekte] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Task_Projekt_fk_Projekt]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tasks]'))
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Task_Projekt_fk_Projekt]
GO
/****** Object:  ForeignKey [FK_Template_Assembly_fk_DisplayedTypeAssembly]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_Assembly_fk_DisplayedTypeAssembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[Templates]'))
ALTER TABLE [dbo].[Templates]  WITH CHECK ADD  CONSTRAINT [FK_Template_Assembly_fk_DisplayedTypeAssembly] FOREIGN KEY([fk_DisplayedTypeAssembly])
REFERENCES [dbo].[Assemblies] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_Assembly_fk_DisplayedTypeAssembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[Templates]'))
ALTER TABLE [dbo].[Templates] CHECK CONSTRAINT [FK_Template_Assembly_fk_DisplayedTypeAssembly]
GO
/****** Object:  ForeignKey [FK_Template_Visual_fk_VisualTree]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_Visual_fk_VisualTree]') AND parent_object_id = OBJECT_ID(N'[dbo].[Templates]'))
ALTER TABLE [dbo].[Templates]  WITH CHECK ADD  CONSTRAINT [FK_Template_Visual_fk_VisualTree] FOREIGN KEY([fk_VisualTree])
REFERENCES [dbo].[Visuals] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_Visual_fk_VisualTree]') AND parent_object_id = OBJECT_ID(N'[dbo].[Templates]'))
ALTER TABLE [dbo].[Templates] CHECK CONSTRAINT [FK_Template_Visual_fk_VisualTree]
GO
/****** Object:  ForeignKey [FK_Template_MenuCollectionEntry_Template_fk_Template]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_MenuCollectionEntry_Template_fk_Template]') AND parent_object_id = OBJECT_ID(N'[dbo].[Templates_MenuCollection]'))
ALTER TABLE [dbo].[Templates_MenuCollection]  WITH CHECK ADD  CONSTRAINT [FK_Template_MenuCollectionEntry_Template_fk_Template] FOREIGN KEY([fk_Template])
REFERENCES [dbo].[Templates] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_MenuCollectionEntry_Template_fk_Template]') AND parent_object_id = OBJECT_ID(N'[dbo].[Templates_MenuCollection]'))
ALTER TABLE [dbo].[Templates_MenuCollection] CHECK CONSTRAINT [FK_Template_MenuCollectionEntry_Template_fk_Template]
GO
/****** Object:  ForeignKey [FK_Template_MenuCollectionEntry_Visual_fk_Menu]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_MenuCollectionEntry_Visual_fk_Menu]') AND parent_object_id = OBJECT_ID(N'[dbo].[Templates_MenuCollection]'))
ALTER TABLE [dbo].[Templates_MenuCollection]  WITH CHECK ADD  CONSTRAINT [FK_Template_MenuCollectionEntry_Visual_fk_Menu] FOREIGN KEY([fk_Menu])
REFERENCES [dbo].[Visuals] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_MenuCollectionEntry_Visual_fk_Menu]') AND parent_object_id = OBJECT_ID(N'[dbo].[Templates_MenuCollection]'))
ALTER TABLE [dbo].[Templates_MenuCollection] CHECK CONSTRAINT [FK_Template_MenuCollectionEntry_Visual_fk_Menu]
GO
/****** Object:  ForeignKey [FK_TestObjClass_Kunde_fk_ObjectProp]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TestObjClass_Kunde_fk_ObjectProp]') AND parent_object_id = OBJECT_ID(N'[dbo].[TestObjClasses]'))
ALTER TABLE [dbo].[TestObjClasses]  WITH CHECK ADD  CONSTRAINT [FK_TestObjClass_Kunde_fk_ObjectProp] FOREIGN KEY([fk_ObjectProp])
REFERENCES [dbo].[Kunden] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TestObjClass_Kunde_fk_ObjectProp]') AND parent_object_id = OBJECT_ID(N'[dbo].[TestObjClasses]'))
ALTER TABLE [dbo].[TestObjClasses] CHECK CONSTRAINT [FK_TestObjClass_Kunde_fk_ObjectProp]
GO
/****** Object:  ForeignKey [FK_ValueTypeProperty_Property_ID]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ValueTypeProperty_Property_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ValueTypeProperties]'))
ALTER TABLE [dbo].[ValueTypeProperties]  WITH CHECK ADD  CONSTRAINT [FK_ValueTypeProperty_Property_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[Properties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ValueTypeProperty_Property_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ValueTypeProperties]'))
ALTER TABLE [dbo].[ValueTypeProperties] CHECK CONSTRAINT [FK_ValueTypeProperty_Property_ID]
GO
/****** Object:  ForeignKey [FK_Visual_BaseProperty_fk_Property]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_BaseProperty_fk_Property]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals]'))
ALTER TABLE [dbo].[Visuals]  WITH CHECK ADD  CONSTRAINT [FK_Visual_BaseProperty_fk_Property] FOREIGN KEY([fk_Property])
REFERENCES [dbo].[BaseProperties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_BaseProperty_fk_Property]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals]'))
ALTER TABLE [dbo].[Visuals] CHECK CONSTRAINT [FK_Visual_BaseProperty_fk_Property]
GO
/****** Object:  ForeignKey [FK_Visual_Method_fk_Method]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_Method_fk_Method]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals]'))
ALTER TABLE [dbo].[Visuals]  WITH CHECK ADD  CONSTRAINT [FK_Visual_Method_fk_Method] FOREIGN KEY([fk_Method])
REFERENCES [dbo].[Methods] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_Method_fk_Method]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals]'))
ALTER TABLE [dbo].[Visuals] CHECK CONSTRAINT [FK_Visual_Method_fk_Method]
GO
/****** Object:  ForeignKey [FK_Visual_ChildrenCollectionEntry_Visual_fk_Children]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_ChildrenCollectionEntry_Visual_fk_Children]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_ChildrenCollection]'))
ALTER TABLE [dbo].[Visuals_ChildrenCollection]  WITH CHECK ADD  CONSTRAINT [FK_Visual_ChildrenCollectionEntry_Visual_fk_Children] FOREIGN KEY([fk_Children])
REFERENCES [dbo].[Visuals] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_ChildrenCollectionEntry_Visual_fk_Children]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_ChildrenCollection]'))
ALTER TABLE [dbo].[Visuals_ChildrenCollection] CHECK CONSTRAINT [FK_Visual_ChildrenCollectionEntry_Visual_fk_Children]
GO
/****** Object:  ForeignKey [FK_Visual_ChildrenCollectionEntry_Visual_fk_Visual]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_ChildrenCollectionEntry_Visual_fk_Visual]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_ChildrenCollection]'))
ALTER TABLE [dbo].[Visuals_ChildrenCollection]  WITH CHECK ADD  CONSTRAINT [FK_Visual_ChildrenCollectionEntry_Visual_fk_Visual] FOREIGN KEY([fk_Visual])
REFERENCES [dbo].[Visuals] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_ChildrenCollectionEntry_Visual_fk_Visual]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_ChildrenCollection]'))
ALTER TABLE [dbo].[Visuals_ChildrenCollection] CHECK CONSTRAINT [FK_Visual_ChildrenCollectionEntry_Visual_fk_Visual]
GO
/****** Object:  ForeignKey [FK_Visual_ContextMenuCollectionEntry_Visual_fk_ContextMenu]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_ContextMenuCollectionEntry_Visual_fk_ContextMenu]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_ContextMenuCollection]'))
ALTER TABLE [dbo].[Visuals_ContextMenuCollection]  WITH CHECK ADD  CONSTRAINT [FK_Visual_ContextMenuCollectionEntry_Visual_fk_ContextMenu] FOREIGN KEY([fk_ContextMenu])
REFERENCES [dbo].[Visuals] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_ContextMenuCollectionEntry_Visual_fk_ContextMenu]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_ContextMenuCollection]'))
ALTER TABLE [dbo].[Visuals_ContextMenuCollection] CHECK CONSTRAINT [FK_Visual_ContextMenuCollectionEntry_Visual_fk_ContextMenu]
GO
/****** Object:  ForeignKey [FK_Visual_ContextMenuCollectionEntry_Visual_fk_Visual]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_ContextMenuCollectionEntry_Visual_fk_Visual]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_ContextMenuCollection]'))
ALTER TABLE [dbo].[Visuals_ContextMenuCollection]  WITH CHECK ADD  CONSTRAINT [FK_Visual_ContextMenuCollectionEntry_Visual_fk_Visual] FOREIGN KEY([fk_Visual])
REFERENCES [dbo].[Visuals] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_ContextMenuCollectionEntry_Visual_fk_Visual]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_ContextMenuCollection]'))
ALTER TABLE [dbo].[Visuals_ContextMenuCollection] CHECK CONSTRAINT [FK_Visual_ContextMenuCollectionEntry_Visual_fk_Visual]
GO
/****** Object:  ForeignKey [FK_Visual_MenuTreeCollectionEntry_Visual_fk_MenuTree]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_MenuTreeCollectionEntry_Visual_fk_MenuTree]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_MenuTreeCollection]'))
ALTER TABLE [dbo].[Visuals_MenuTreeCollection]  WITH CHECK ADD  CONSTRAINT [FK_Visual_MenuTreeCollectionEntry_Visual_fk_MenuTree] FOREIGN KEY([fk_MenuTree])
REFERENCES [dbo].[Visuals] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_MenuTreeCollectionEntry_Visual_fk_MenuTree]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_MenuTreeCollection]'))
ALTER TABLE [dbo].[Visuals_MenuTreeCollection] CHECK CONSTRAINT [FK_Visual_MenuTreeCollectionEntry_Visual_fk_MenuTree]
GO
/****** Object:  ForeignKey [FK_Visual_MenuTreeCollectionEntry_Visual_fk_Visual]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_MenuTreeCollectionEntry_Visual_fk_Visual]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_MenuTreeCollection]'))
ALTER TABLE [dbo].[Visuals_MenuTreeCollection]  WITH CHECK ADD  CONSTRAINT [FK_Visual_MenuTreeCollectionEntry_Visual_fk_Visual] FOREIGN KEY([fk_Visual])
REFERENCES [dbo].[Visuals] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_MenuTreeCollectionEntry_Visual_fk_Visual]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_MenuTreeCollection]'))
ALTER TABLE [dbo].[Visuals_MenuTreeCollection] CHECK CONSTRAINT [FK_Visual_MenuTreeCollectionEntry_Visual_fk_Visual]
GO
/****** Object:  ForeignKey [FK_Zeitkonto_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Zeitkonto_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[Zeitkonten_MitarbeiterCollection]'))
ALTER TABLE [dbo].[Zeitkonten_MitarbeiterCollection]  WITH CHECK ADD  CONSTRAINT [FK_Zeitkonto_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter] FOREIGN KEY([fk_Mitarbeiter])
REFERENCES [dbo].[Mitarbeiter] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Zeitkonto_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[Zeitkonten_MitarbeiterCollection]'))
ALTER TABLE [dbo].[Zeitkonten_MitarbeiterCollection] CHECK CONSTRAINT [FK_Zeitkonto_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]
GO
/****** Object:  ForeignKey [FK_Zeitkonto_MitarbeiterCollectionEntry_Zeitkonto_fk_Zeitkonto]    Script Date: 11/03/2008 15:18:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Zeitkonto_MitarbeiterCollectionEntry_Zeitkonto_fk_Zeitkonto]') AND parent_object_id = OBJECT_ID(N'[dbo].[Zeitkonten_MitarbeiterCollection]'))
ALTER TABLE [dbo].[Zeitkonten_MitarbeiterCollection]  WITH CHECK ADD  CONSTRAINT [FK_Zeitkonto_MitarbeiterCollectionEntry_Zeitkonto_fk_Zeitkonto] FOREIGN KEY([fk_Zeitkonto])
REFERENCES [dbo].[Zeitkonten] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Zeitkonto_MitarbeiterCollectionEntry_Zeitkonto_fk_Zeitkonto]') AND parent_object_id = OBJECT_ID(N'[dbo].[Zeitkonten_MitarbeiterCollection]'))
ALTER TABLE [dbo].[Zeitkonten_MitarbeiterCollection] CHECK CONSTRAINT [FK_Zeitkonto_MitarbeiterCollectionEntry_Zeitkonto_fk_Zeitkonto]
GO
