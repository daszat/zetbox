/****** Object:  ForeignKey [FK_Kunde_EMailsCollectionEntry_Kunde_fk_Kunde]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Kunde_EMailsCollectionEntry_Kunde_fk_Kunde]') AND parent_object_id = OBJECT_ID(N'[dbo].[Kunden_EMailsCollection]'))
ALTER TABLE [dbo].[Kunden_EMailsCollection] DROP CONSTRAINT [FK_Kunde_EMailsCollectionEntry_Kunde_fk_Kunde]
GO
/****** Object:  ForeignKey [FK_Projekt_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projekt_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projekte_MitarbeiterCollection]'))
ALTER TABLE [dbo].[Projekte_MitarbeiterCollection] DROP CONSTRAINT [FK_Projekt_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]
GO
/****** Object:  ForeignKey [FK_Projekt_MitarbeiterCollectionEntry_Projekt_fk_Projekt]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projekt_MitarbeiterCollectionEntry_Projekt_fk_Projekt]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projekte_MitarbeiterCollection]'))
ALTER TABLE [dbo].[Projekte_MitarbeiterCollection] DROP CONSTRAINT [FK_Projekt_MitarbeiterCollectionEntry_Projekt_fk_Projekt]
GO
/****** Object:  ForeignKey [FK_Task_Projekt_fk_Projekt]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Task_Projekt_fk_Projekt]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tasks]'))
ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_Task_Projekt_fk_Projekt]
GO
/****** Object:  ForeignKey [FK_WorkEffortAccount_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkEffortAccount_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkEffortAccounts_MitarbeiterCollection]'))
ALTER TABLE [dbo].[WorkEffortAccounts_MitarbeiterCollection] DROP CONSTRAINT [FK_WorkEffortAccount_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]
GO
/****** Object:  ForeignKey [FK_WorkEffortAccount_MitarbeiterCollectionEntry_WorkEffortAccount_fk_WorkEffortAccount]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkEffortAccount_MitarbeiterCollectionEntry_WorkEffortAccount_fk_WorkEffortAccount]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkEffortAccounts_MitarbeiterCollection]'))
ALTER TABLE [dbo].[WorkEffortAccounts_MitarbeiterCollection] DROP CONSTRAINT [FK_WorkEffortAccount_MitarbeiterCollectionEntry_WorkEffortAccount_fk_WorkEffortAccount]
GO
/****** Object:  ForeignKey [FK_TestObjClass_Kunde_fk_ObjectProp]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TestObjClass_Kunde_fk_ObjectProp]') AND parent_object_id = OBJECT_ID(N'[dbo].[TestObjClasses]'))
ALTER TABLE [dbo].[TestObjClasses] DROP CONSTRAINT [FK_TestObjClass_Kunde_fk_ObjectProp]
GO
/****** Object:  ForeignKey [FK_Auftrag_Kunde_fk_Kunde]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Auftrag_Kunde_fk_Kunde]') AND parent_object_id = OBJECT_ID(N'[dbo].[Auftraege]'))
ALTER TABLE [dbo].[Auftraege] DROP CONSTRAINT [FK_Auftrag_Kunde_fk_Kunde]
GO
/****** Object:  ForeignKey [FK_Auftrag_Mitarbeiter_fk_Mitarbeiter]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Auftrag_Mitarbeiter_fk_Mitarbeiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[Auftraege]'))
ALTER TABLE [dbo].[Auftraege] DROP CONSTRAINT [FK_Auftrag_Mitarbeiter_fk_Mitarbeiter]
GO
/****** Object:  ForeignKey [FK_Auftrag_Projekt_fk_Projekt]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Auftrag_Projekt_fk_Projekt]') AND parent_object_id = OBJECT_ID(N'[dbo].[Auftraege]'))
ALTER TABLE [dbo].[Auftraege] DROP CONSTRAINT [FK_Auftrag_Projekt_fk_Projekt]
GO
/****** Object:  ForeignKey [FK_Assembly_Module_fk_Module]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Assembly_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[Assemblies]'))
ALTER TABLE [dbo].[Assemblies] DROP CONSTRAINT [FK_Assembly_Module_fk_Module]
GO
/****** Object:  ForeignKey [FK_DataType_Icon_fk_DefaultIcon]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DataType_Icon_fk_DefaultIcon]') AND parent_object_id = OBJECT_ID(N'[dbo].[DataTypes]'))
ALTER TABLE [dbo].[DataTypes] DROP CONSTRAINT [FK_DataType_Icon_fk_DefaultIcon]
GO
/****** Object:  ForeignKey [FK_DataType_Module_fk_Module]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DataType_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[DataTypes]'))
ALTER TABLE [dbo].[DataTypes] DROP CONSTRAINT [FK_DataType_Module_fk_Module]
GO
/****** Object:  ForeignKey [FK_Interface_DataType_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Interface_DataType_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Interfaces]'))
ALTER TABLE [dbo].[Interfaces] DROP CONSTRAINT [FK_Interface_DataType_ID]
GO
/****** Object:  ForeignKey [FK_Enumeration_DataType_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Enumeration_DataType_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Enumerations]'))
ALTER TABLE [dbo].[Enumerations] DROP CONSTRAINT [FK_Enumeration_DataType_ID]
GO
/****** Object:  ForeignKey [FK_Method_DataType_fk_ObjectClass]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Method_DataType_fk_ObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[Methods]'))
ALTER TABLE [dbo].[Methods] DROP CONSTRAINT [FK_Method_DataType_fk_ObjectClass]
GO
/****** Object:  ForeignKey [FK_Method_Module_fk_Module]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Method_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[Methods]'))
ALTER TABLE [dbo].[Methods] DROP CONSTRAINT [FK_Method_Module_fk_Module]
GO
/****** Object:  ForeignKey [FK_ControlInfo_Assembly_fk_Assembly]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ControlInfo_Assembly_fk_Assembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[ControlInfos]'))
ALTER TABLE [dbo].[ControlInfos] DROP CONSTRAINT [FK_ControlInfo_Assembly_fk_Assembly]
GO
/****** Object:  ForeignKey [FK_TypeRef_Assembly_fk_Assembly]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TypeRef_Assembly_fk_Assembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[TypeRefs]'))
ALTER TABLE [dbo].[TypeRefs] DROP CONSTRAINT [FK_TypeRef_Assembly_fk_Assembly]
GO
/****** Object:  ForeignKey [FK_TypeRef_TypeRef_fk_Parent]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TypeRef_TypeRef_fk_Parent]') AND parent_object_id = OBJECT_ID(N'[dbo].[TypeRefs]'))
ALTER TABLE [dbo].[TypeRefs] DROP CONSTRAINT [FK_TypeRef_TypeRef_fk_Parent]
GO
/****** Object:  ForeignKey [FK_Struct_DataType_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Struct_DataType_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Structs]'))
ALTER TABLE [dbo].[Structs] DROP CONSTRAINT [FK_Struct_DataType_ID]
GO
/****** Object:  ForeignKey [FK_PresenterInfo_Assembly_fk_DataAssembly]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PresenterInfo_Assembly_fk_DataAssembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[PresenterInfos]'))
ALTER TABLE [dbo].[PresenterInfos] DROP CONSTRAINT [FK_PresenterInfo_Assembly_fk_DataAssembly]
GO
/****** Object:  ForeignKey [FK_PresenterInfo_Assembly_fk_PresenterAssembly]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PresenterInfo_Assembly_fk_PresenterAssembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[PresenterInfos]'))
ALTER TABLE [dbo].[PresenterInfos] DROP CONSTRAINT [FK_PresenterInfo_Assembly_fk_PresenterAssembly]
GO
/****** Object:  ForeignKey [FK_PresentableModelDescriptors_TypeRef_fk_PresentableModelRef]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PresentableModelDescriptors_TypeRef_fk_PresentableModelRef]') AND parent_object_id = OBJECT_ID(N'[dbo].[PresentableModelDescriptors]'))
ALTER TABLE [dbo].[PresentableModelDescriptors] DROP CONSTRAINT [FK_PresentableModelDescriptors_TypeRef_fk_PresentableModelRef]
GO
/****** Object:  ForeignKey [FK_Visual_Method_fk_Method]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_Method_fk_Method]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals]'))
ALTER TABLE [dbo].[Visuals] DROP CONSTRAINT [FK_Visual_Method_fk_Method]
GO
/****** Object:  ForeignKey [FK_TypeRef_GenericArgumentsCollectionEntry_TypeRef_fk_GenericArguments]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TypeRef_GenericArgumentsCollectionEntry_TypeRef_fk_GenericArguments]') AND parent_object_id = OBJECT_ID(N'[dbo].[TypeRefs_GenericArgumentsCollection]'))
ALTER TABLE [dbo].[TypeRefs_GenericArgumentsCollection] DROP CONSTRAINT [FK_TypeRef_GenericArgumentsCollectionEntry_TypeRef_fk_GenericArguments]
GO
/****** Object:  ForeignKey [FK_TypeRef_GenericArgumentsCollectionEntry_TypeRef_fk_TypeRef]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TypeRef_GenericArgumentsCollectionEntry_TypeRef_fk_TypeRef]') AND parent_object_id = OBJECT_ID(N'[dbo].[TypeRefs_GenericArgumentsCollection]'))
ALTER TABLE [dbo].[TypeRefs_GenericArgumentsCollection] DROP CONSTRAINT [FK_TypeRef_GenericArgumentsCollectionEntry_TypeRef_fk_TypeRef]
GO
/****** Object:  ForeignKey [FK_BaseParameter_Method_fk_Method]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BaseParameter_Method_fk_Method]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseParameters]'))
ALTER TABLE [dbo].[BaseParameters] DROP CONSTRAINT [FK_BaseParameter_Method_fk_Method]
GO
/****** Object:  ForeignKey [FK_BaseParameter_Module_fk_Module]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BaseParameter_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseParameters]'))
ALTER TABLE [dbo].[BaseParameters] DROP CONSTRAINT [FK_BaseParameter_Module_fk_Module]
GO
/****** Object:  ForeignKey [FK_MethodInvocation_DataType_fk_InvokeOnObjectClass]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocation_DataType_fk_InvokeOnObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocations]'))
ALTER TABLE [dbo].[MethodInvocations] DROP CONSTRAINT [FK_MethodInvocation_DataType_fk_InvokeOnObjectClass]
GO
/****** Object:  ForeignKey [FK_MethodInvocation_Method_fk_Method]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocation_Method_fk_Method]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocations]'))
ALTER TABLE [dbo].[MethodInvocations] DROP CONSTRAINT [FK_MethodInvocation_Method_fk_Method]
GO
/****** Object:  ForeignKey [FK_MethodInvocation_Module_fk_Module]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocation_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocations]'))
ALTER TABLE [dbo].[MethodInvocations] DROP CONSTRAINT [FK_MethodInvocation_Module_fk_Module]
GO
/****** Object:  ForeignKey [FK_MethodInvocation_TypeRef_fk_Implementor]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocation_TypeRef_fk_Implementor]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocations]'))
ALTER TABLE [dbo].[MethodInvocations] DROP CONSTRAINT [FK_MethodInvocation_TypeRef_fk_Implementor]
GO
/****** Object:  ForeignKey [FK_EnumerationEntry_Enumeration_fk_Enumeration]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EnumerationEntry_Enumeration_fk_Enumeration]') AND parent_object_id = OBJECT_ID(N'[dbo].[EnumerationEntries]'))
ALTER TABLE [dbo].[EnumerationEntries] DROP CONSTRAINT [FK_EnumerationEntry_Enumeration_fk_Enumeration]
GO
/****** Object:  ForeignKey [FK_DoubleParameter_BaseParameter_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoubleParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoubleParameters]'))
ALTER TABLE [dbo].[DoubleParameters] DROP CONSTRAINT [FK_DoubleParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_IntParameter_BaseParameter_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_IntParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[IntParameters]'))
ALTER TABLE [dbo].[IntParameters] DROP CONSTRAINT [FK_IntParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_DateTimeParameter_BaseParameter_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DateTimeParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[DateTimeParameters]'))
ALTER TABLE [dbo].[DateTimeParameters] DROP CONSTRAINT [FK_DateTimeParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_BoolParameter_BaseParameter_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BoolParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[BoolParameters]'))
ALTER TABLE [dbo].[BoolParameters] DROP CONSTRAINT [FK_BoolParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_CLRObjectParameter_Assembly_fk_Assembly]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLRObjectParameter_Assembly_fk_Assembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLRObjectParameters]'))
ALTER TABLE [dbo].[CLRObjectParameters] DROP CONSTRAINT [FK_CLRObjectParameter_Assembly_fk_Assembly]
GO
/****** Object:  ForeignKey [FK_CLRObjectParameter_BaseParameter_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLRObjectParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLRObjectParameters]'))
ALTER TABLE [dbo].[CLRObjectParameters] DROP CONSTRAINT [FK_CLRObjectParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_Visual_MenuTreeCollectionEntry_Visual_fk_MenuTree]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_MenuTreeCollectionEntry_Visual_fk_MenuTree]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_MenuTreeCollection]'))
ALTER TABLE [dbo].[Visuals_MenuTreeCollection] DROP CONSTRAINT [FK_Visual_MenuTreeCollectionEntry_Visual_fk_MenuTree]
GO
/****** Object:  ForeignKey [FK_Visual_MenuTreeCollectionEntry_Visual_fk_Visual]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_MenuTreeCollectionEntry_Visual_fk_Visual]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_MenuTreeCollection]'))
ALTER TABLE [dbo].[Visuals_MenuTreeCollection] DROP CONSTRAINT [FK_Visual_MenuTreeCollectionEntry_Visual_fk_Visual]
GO
/****** Object:  ForeignKey [FK_Visual_ContextMenuCollectionEntry_Visual_fk_ContextMenu]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_ContextMenuCollectionEntry_Visual_fk_ContextMenu]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_ContextMenuCollection]'))
ALTER TABLE [dbo].[Visuals_ContextMenuCollection] DROP CONSTRAINT [FK_Visual_ContextMenuCollectionEntry_Visual_fk_ContextMenu]
GO
/****** Object:  ForeignKey [FK_Visual_ContextMenuCollectionEntry_Visual_fk_Visual]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_ContextMenuCollectionEntry_Visual_fk_Visual]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_ContextMenuCollection]'))
ALTER TABLE [dbo].[Visuals_ContextMenuCollection] DROP CONSTRAINT [FK_Visual_ContextMenuCollectionEntry_Visual_fk_Visual]
GO
/****** Object:  ForeignKey [FK_Visual_ChildrenCollectionEntry_Visual_fk_Children]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_ChildrenCollectionEntry_Visual_fk_Children]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_ChildrenCollection]'))
ALTER TABLE [dbo].[Visuals_ChildrenCollection] DROP CONSTRAINT [FK_Visual_ChildrenCollectionEntry_Visual_fk_Children]
GO
/****** Object:  ForeignKey [FK_Visual_ChildrenCollectionEntry_Visual_fk_Visual]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_ChildrenCollectionEntry_Visual_fk_Visual]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_ChildrenCollection]'))
ALTER TABLE [dbo].[Visuals_ChildrenCollection] DROP CONSTRAINT [FK_Visual_ChildrenCollectionEntry_Visual_fk_Visual]
GO
/****** Object:  ForeignKey [FK_Template_Assembly_fk_DisplayedTypeAssembly]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_Assembly_fk_DisplayedTypeAssembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[Templates]'))
ALTER TABLE [dbo].[Templates] DROP CONSTRAINT [FK_Template_Assembly_fk_DisplayedTypeAssembly]
GO
/****** Object:  ForeignKey [FK_Template_Visual_fk_VisualTree]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_Visual_fk_VisualTree]') AND parent_object_id = OBJECT_ID(N'[dbo].[Templates]'))
ALTER TABLE [dbo].[Templates] DROP CONSTRAINT [FK_Template_Visual_fk_VisualTree]
GO
/****** Object:  ForeignKey [FK_ViewDescriptor_TypeRef_fk_ControlRef]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ViewDescriptor_TypeRef_fk_ControlRef]') AND parent_object_id = OBJECT_ID(N'[dbo].[ViewDescriptors]'))
ALTER TABLE [dbo].[ViewDescriptors] DROP CONSTRAINT [FK_ViewDescriptor_TypeRef_fk_ControlRef]
GO
/****** Object:  ForeignKey [FK_ViewDescriptor_TypeRef_fk_PresentedModelDescriptor]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ViewDescriptor_TypeRef_fk_PresentedModelDescriptor]') AND parent_object_id = OBJECT_ID(N'[dbo].[ViewDescriptors]'))
ALTER TABLE [dbo].[ViewDescriptors] DROP CONSTRAINT [FK_ViewDescriptor_TypeRef_fk_PresentedModelDescriptor]
GO
/****** Object:  ForeignKey [FK_ObjectClass_DataType_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_DataType_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses]'))
ALTER TABLE [dbo].[ObjectClasses] DROP CONSTRAINT [FK_ObjectClass_DataType_ID]
GO
/****** Object:  ForeignKey [FK_ObjectClass_ObjectClass_fk_BaseObjectClass]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_ObjectClass_fk_BaseObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses]'))
ALTER TABLE [dbo].[ObjectClasses] DROP CONSTRAINT [FK_ObjectClass_ObjectClass_fk_BaseObjectClass]
GO
/****** Object:  ForeignKey [FK_ObjectClass_PresentableModelDescriptor_fk_DefaultPresentableModelDescriptor]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_PresentableModelDescriptor_fk_DefaultPresentableModelDescriptor]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses]'))
ALTER TABLE [dbo].[ObjectClasses] DROP CONSTRAINT [FK_ObjectClass_PresentableModelDescriptor_fk_DefaultPresentableModelDescriptor]
GO
/****** Object:  ForeignKey [FK_ObjectClass_TypeRef_fk_DefaultModel]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_TypeRef_fk_DefaultModel]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses]'))
ALTER TABLE [dbo].[ObjectClasses] DROP CONSTRAINT [FK_ObjectClass_TypeRef_fk_DefaultModel]
GO
/****** Object:  ForeignKey [FK_Properties_DataTypes]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Properties_DataTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[Properties]'))
ALTER TABLE [dbo].[Properties] DROP CONSTRAINT [FK_Properties_DataTypes]
GO
/****** Object:  ForeignKey [FK_Property_PresentableModelDescriptor_fk_ValueModelDescriptor]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Property_PresentableModelDescriptor_fk_ValueModelDescriptor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Properties]'))
ALTER TABLE [dbo].[Properties] DROP CONSTRAINT [FK_Property_PresentableModelDescriptor_fk_ValueModelDescriptor]
GO
/****** Object:  ForeignKey [FK_StringParameter_BaseParameter_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StringParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StringParameters]'))
ALTER TABLE [dbo].[StringParameters] DROP CONSTRAINT [FK_StringParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_ObjectParameter_BaseParameter_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectParameters]'))
ALTER TABLE [dbo].[ObjectParameters] DROP CONSTRAINT [FK_ObjectParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_ObjectParameter_DataType_fk_DataType]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectParameter_DataType_fk_DataType]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectParameters]'))
ALTER TABLE [dbo].[ObjectParameters] DROP CONSTRAINT [FK_ObjectParameter_DataType_fk_DataType]
GO
/****** Object:  ForeignKey [FK_ObjectReferenceProperty_ObjectClass_fk_ReferenceObjectClass]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectReferenceProperty_ObjectClass_fk_ReferenceObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectReferenceProperties]'))
ALTER TABLE [dbo].[ObjectReferenceProperties] DROP CONSTRAINT [FK_ObjectReferenceProperty_ObjectClass_fk_ReferenceObjectClass]
GO
/****** Object:  ForeignKey [FK_ObjectReferenceProperty_Property_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectReferenceProperty_Property_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectReferenceProperties]'))
ALTER TABLE [dbo].[ObjectReferenceProperties] DROP CONSTRAINT [FK_ObjectReferenceProperty_Property_ID]
GO
/****** Object:  ForeignKey [FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_fk_ImplementsInterfaces]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_fk_ImplementsInterfaces]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses_ImplementsInterfacesCollection]'))
ALTER TABLE [dbo].[ObjectClasses_ImplementsInterfacesCollection] DROP CONSTRAINT [FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_fk_ImplementsInterfaces]
GO
/****** Object:  ForeignKey [FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_ObjectClass]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_ObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses_ImplementsInterfacesCollection]'))
ALTER TABLE [dbo].[ObjectClasses_ImplementsInterfacesCollection] DROP CONSTRAINT [FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_ObjectClass]
GO
/****** Object:  ForeignKey [FK_RelationEnd_ObjectClass_fk_Type]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RelationEnd_ObjectClass_fk_Type]') AND parent_object_id = OBJECT_ID(N'[dbo].[RelationEnds]'))
ALTER TABLE [dbo].[RelationEnds] DROP CONSTRAINT [FK_RelationEnd_ObjectClass_fk_Type]
GO
/****** Object:  ForeignKey [FK_RelationEnd_Property_fk_Navigator]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RelationEnd_Property_fk_Navigator]') AND parent_object_id = OBJECT_ID(N'[dbo].[RelationEnds]'))
ALTER TABLE [dbo].[RelationEnds] DROP CONSTRAINT [FK_RelationEnd_Property_fk_Navigator]
GO
/****** Object:  ForeignKey [FK_ValueTypeProperty_Property_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ValueTypeProperty_Property_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ValueTypeProperties]'))
ALTER TABLE [dbo].[ValueTypeProperties] DROP CONSTRAINT [FK_ValueTypeProperty_Property_ID]
GO
/****** Object:  ForeignKey [FK_Template_MenuCollectionEntry_Template_fk_Template]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_MenuCollectionEntry_Template_fk_Template]') AND parent_object_id = OBJECT_ID(N'[dbo].[Templates_MenuCollection]'))
ALTER TABLE [dbo].[Templates_MenuCollection] DROP CONSTRAINT [FK_Template_MenuCollectionEntry_Template_fk_Template]
GO
/****** Object:  ForeignKey [FK_Template_MenuCollectionEntry_Visual_fk_Menu]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_MenuCollectionEntry_Visual_fk_Menu]') AND parent_object_id = OBJECT_ID(N'[dbo].[Templates_MenuCollection]'))
ALTER TABLE [dbo].[Templates_MenuCollection] DROP CONSTRAINT [FK_Template_MenuCollectionEntry_Visual_fk_Menu]
GO
/****** Object:  ForeignKey [FK_StructProperty_Property_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StructProperty_Property_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StructProperties]'))
ALTER TABLE [dbo].[StructProperties] DROP CONSTRAINT [FK_StructProperty_Property_ID]
GO
/****** Object:  ForeignKey [FK_StructProperty_Struct_fk_StructDefinition]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StructProperty_Struct_fk_StructDefinition]') AND parent_object_id = OBJECT_ID(N'[dbo].[StructProperties]'))
ALTER TABLE [dbo].[StructProperties] DROP CONSTRAINT [FK_StructProperty_Struct_fk_StructDefinition]
GO
/****** Object:  ForeignKey [FK_Constraints_Properties]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Constraints_Properties]') AND parent_object_id = OBJECT_ID(N'[dbo].[Constraints]'))
ALTER TABLE [dbo].[Constraints] DROP CONSTRAINT [FK_Constraints_Properties]
GO
/****** Object:  ForeignKey [FK_MethodInvocationConstraint_Constraint_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocationConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocationConstraints]'))
ALTER TABLE [dbo].[MethodInvocationConstraints] DROP CONSTRAINT [FK_MethodInvocationConstraint_Constraint_ID]
GO
/****** Object:  ForeignKey [FK_IsValidIdentifierConstraint_Constraint_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_IsValidIdentifierConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[IsValidIdentifierConstraints]'))
ALTER TABLE [dbo].[IsValidIdentifierConstraints] DROP CONSTRAINT [FK_IsValidIdentifierConstraint_Constraint_ID]
GO
/****** Object:  ForeignKey [FK_IntProperty_ValueTypeProperty_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_IntProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[IntProperties]'))
ALTER TABLE [dbo].[IntProperties] DROP CONSTRAINT [FK_IntProperty_ValueTypeProperty_ID]
GO
/****** Object:  ForeignKey [FK_IntegerRangeConstraint_Constraint_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_IntegerRangeConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[IntegerRangeConstraints]'))
ALTER TABLE [dbo].[IntegerRangeConstraints] DROP CONSTRAINT [FK_IntegerRangeConstraint_Constraint_ID]
GO
/****** Object:  ForeignKey [FK_GuidProperties_ValueTypeProperties]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GuidProperties_ValueTypeProperties]') AND parent_object_id = OBJECT_ID(N'[dbo].[GuidProperties]'))
ALTER TABLE [dbo].[GuidProperties] DROP CONSTRAINT [FK_GuidProperties_ValueTypeProperties]
GO
/****** Object:  ForeignKey [FK_EnumerationProperty_Enumeration_fk_Enumeration]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EnumerationProperty_Enumeration_fk_Enumeration]') AND parent_object_id = OBJECT_ID(N'[dbo].[EnumerationProperties]'))
ALTER TABLE [dbo].[EnumerationProperties] DROP CONSTRAINT [FK_EnumerationProperty_Enumeration_fk_Enumeration]
GO
/****** Object:  ForeignKey [FK_EnumerationProperty_ValueTypeProperty_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EnumerationProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[EnumerationProperties]'))
ALTER TABLE [dbo].[EnumerationProperties] DROP CONSTRAINT [FK_EnumerationProperty_ValueTypeProperty_ID]
GO
/****** Object:  ForeignKey [FK_DoubleProperty_ValueTypeProperty_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoubleProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoubleProperties]'))
ALTER TABLE [dbo].[DoubleProperties] DROP CONSTRAINT [FK_DoubleProperty_ValueTypeProperty_ID]
GO
/****** Object:  ForeignKey [FK_DateTimeProperty_ValueTypeProperty_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DateTimeProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[DateTimeProperties]'))
ALTER TABLE [dbo].[DateTimeProperties] DROP CONSTRAINT [FK_DateTimeProperty_ValueTypeProperty_ID]
GO
/****** Object:  ForeignKey [FK_BoolProperty_ValueTypeProperty_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BoolProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[BoolProperties]'))
ALTER TABLE [dbo].[BoolProperties] DROP CONSTRAINT [FK_BoolProperty_ValueTypeProperty_ID]
GO
/****** Object:  ForeignKey [FK_StringRangeConstraint_Constraint_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StringRangeConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StringRangeConstraints]'))
ALTER TABLE [dbo].[StringRangeConstraints] DROP CONSTRAINT [FK_StringRangeConstraint_Constraint_ID]
GO
/****** Object:  ForeignKey [FK_StringProperty_ValueTypeProperty_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StringProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StringProperties]'))
ALTER TABLE [dbo].[StringProperties] DROP CONSTRAINT [FK_StringProperty_ValueTypeProperty_ID]
GO
/****** Object:  ForeignKey [FK_StringConstraint_Constraint_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StringConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StringConstraints]'))
ALTER TABLE [dbo].[StringConstraints] DROP CONSTRAINT [FK_StringConstraint_Constraint_ID]
GO
/****** Object:  ForeignKey [FK_Relation_RelationEnd_fk_A]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Relation_RelationEnd_fk_A]') AND parent_object_id = OBJECT_ID(N'[dbo].[Relations]'))
ALTER TABLE [dbo].[Relations] DROP CONSTRAINT [FK_Relation_RelationEnd_fk_A]
GO
/****** Object:  ForeignKey [FK_Relation_RelationEnd_fk_B]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Relation_RelationEnd_fk_B]') AND parent_object_id = OBJECT_ID(N'[dbo].[Relations]'))
ALTER TABLE [dbo].[Relations] DROP CONSTRAINT [FK_Relation_RelationEnd_fk_B]
GO
/****** Object:  ForeignKey [FK_NotNullableConstraint_Constraint_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NotNullableConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[NotNullableConstraints]'))
ALTER TABLE [dbo].[NotNullableConstraints] DROP CONSTRAINT [FK_NotNullableConstraint_Constraint_ID]
GO
/****** Object:  ForeignKey [FK_IsValidNamespaceConstraint_IsValidIdentifierConstraint_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_IsValidNamespaceConstraint_IsValidIdentifierConstraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[IsValidNamespaceConstraints]'))
ALTER TABLE [dbo].[IsValidNamespaceConstraints] DROP CONSTRAINT [FK_IsValidNamespaceConstraint_IsValidIdentifierConstraint_ID]
GO
/****** Object:  Table [dbo].[IsValidNamespaceConstraints]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IsValidNamespaceConstraints]') AND type in (N'U'))
DROP TABLE [dbo].[IsValidNamespaceConstraints]
GO
/****** Object:  Table [dbo].[NotNullableConstraints]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NotNullableConstraints]') AND type in (N'U'))
DROP TABLE [dbo].[NotNullableConstraints]
GO
/****** Object:  Table [dbo].[Relations]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Relations]') AND type in (N'U'))
DROP TABLE [dbo].[Relations]
GO
/****** Object:  Table [dbo].[StringConstraints]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StringConstraints]') AND type in (N'U'))
DROP TABLE [dbo].[StringConstraints]
GO
/****** Object:  Table [dbo].[StringProperties]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StringProperties]') AND type in (N'U'))
DROP TABLE [dbo].[StringProperties]
GO
/****** Object:  Table [dbo].[StringRangeConstraints]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StringRangeConstraints]') AND type in (N'U'))
DROP TABLE [dbo].[StringRangeConstraints]
GO
/****** Object:  Table [dbo].[BoolProperties]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BoolProperties]') AND type in (N'U'))
DROP TABLE [dbo].[BoolProperties]
GO
/****** Object:  Table [dbo].[DateTimeProperties]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DateTimeProperties]') AND type in (N'U'))
DROP TABLE [dbo].[DateTimeProperties]
GO
/****** Object:  Table [dbo].[DoubleProperties]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoubleProperties]') AND type in (N'U'))
DROP TABLE [dbo].[DoubleProperties]
GO
/****** Object:  Table [dbo].[EnumerationProperties]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EnumerationProperties]') AND type in (N'U'))
DROP TABLE [dbo].[EnumerationProperties]
GO
/****** Object:  Table [dbo].[GuidProperties]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GuidProperties]') AND type in (N'U'))
DROP TABLE [dbo].[GuidProperties]
GO
/****** Object:  Table [dbo].[IntegerRangeConstraints]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IntegerRangeConstraints]') AND type in (N'U'))
DROP TABLE [dbo].[IntegerRangeConstraints]
GO
/****** Object:  Table [dbo].[IntProperties]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IntProperties]') AND type in (N'U'))
DROP TABLE [dbo].[IntProperties]
GO
/****** Object:  Table [dbo].[IsValidIdentifierConstraints]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IsValidIdentifierConstraints]') AND type in (N'U'))
DROP TABLE [dbo].[IsValidIdentifierConstraints]
GO
/****** Object:  Table [dbo].[MethodInvocationConstraints]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MethodInvocationConstraints]') AND type in (N'U'))
DROP TABLE [dbo].[MethodInvocationConstraints]
GO
/****** Object:  Table [dbo].[Constraints]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Constraints]') AND type in (N'U'))
DROP TABLE [dbo].[Constraints]
GO
/****** Object:  Table [dbo].[StructProperties]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StructProperties]') AND type in (N'U'))
DROP TABLE [dbo].[StructProperties]
GO
/****** Object:  Table [dbo].[Templates_MenuCollection]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Templates_MenuCollection]') AND type in (N'U'))
DROP TABLE [dbo].[Templates_MenuCollection]
GO
/****** Object:  Table [dbo].[ValueTypeProperties]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ValueTypeProperties]') AND type in (N'U'))
DROP TABLE [dbo].[ValueTypeProperties]
GO
/****** Object:  Table [dbo].[RelationEnds]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RelationEnds]') AND type in (N'U'))
DROP TABLE [dbo].[RelationEnds]
GO
/****** Object:  Table [dbo].[ObjectClasses_ImplementsInterfacesCollection]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObjectClasses_ImplementsInterfacesCollection]') AND type in (N'U'))
DROP TABLE [dbo].[ObjectClasses_ImplementsInterfacesCollection]
GO
/****** Object:  Table [dbo].[ObjectReferenceProperties]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObjectReferenceProperties]') AND type in (N'U'))
DROP TABLE [dbo].[ObjectReferenceProperties]
GO
/****** Object:  Table [dbo].[ObjectParameters]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObjectParameters]') AND type in (N'U'))
DROP TABLE [dbo].[ObjectParameters]
GO
/****** Object:  Table [dbo].[StringParameters]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StringParameters]') AND type in (N'U'))
DROP TABLE [dbo].[StringParameters]
GO
/****** Object:  Table [dbo].[Properties]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Properties]') AND type in (N'U'))
DROP TABLE [dbo].[Properties]
GO
/****** Object:  Table [dbo].[ObjectClasses]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObjectClasses]') AND type in (N'U'))
DROP TABLE [dbo].[ObjectClasses]
GO
/****** Object:  Table [dbo].[ViewDescriptors]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ViewDescriptors]') AND type in (N'U'))
DROP TABLE [dbo].[ViewDescriptors]
GO
/****** Object:  Table [dbo].[Templates]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Templates]') AND type in (N'U'))
DROP TABLE [dbo].[Templates]
GO
/****** Object:  Table [dbo].[Visuals_ChildrenCollection]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Visuals_ChildrenCollection]') AND type in (N'U'))
DROP TABLE [dbo].[Visuals_ChildrenCollection]
GO
/****** Object:  Table [dbo].[Visuals_ContextMenuCollection]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Visuals_ContextMenuCollection]') AND type in (N'U'))
DROP TABLE [dbo].[Visuals_ContextMenuCollection]
GO
/****** Object:  Table [dbo].[Visuals_MenuTreeCollection]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Visuals_MenuTreeCollection]') AND type in (N'U'))
DROP TABLE [dbo].[Visuals_MenuTreeCollection]
GO
/****** Object:  Table [dbo].[CLRObjectParameters]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLRObjectParameters]') AND type in (N'U'))
DROP TABLE [dbo].[CLRObjectParameters]
GO
/****** Object:  Table [dbo].[BoolParameters]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BoolParameters]') AND type in (N'U'))
DROP TABLE [dbo].[BoolParameters]
GO
/****** Object:  Table [dbo].[DateTimeParameters]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DateTimeParameters]') AND type in (N'U'))
DROP TABLE [dbo].[DateTimeParameters]
GO
/****** Object:  Table [dbo].[IntParameters]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IntParameters]') AND type in (N'U'))
DROP TABLE [dbo].[IntParameters]
GO
/****** Object:  Table [dbo].[DoubleParameters]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoubleParameters]') AND type in (N'U'))
DROP TABLE [dbo].[DoubleParameters]
GO
/****** Object:  Table [dbo].[EnumerationEntries]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EnumerationEntries]') AND type in (N'U'))
DROP TABLE [dbo].[EnumerationEntries]
GO
/****** Object:  Table [dbo].[MethodInvocations]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MethodInvocations]') AND type in (N'U'))
DROP TABLE [dbo].[MethodInvocations]
GO
/****** Object:  Table [dbo].[BaseParameters]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BaseParameters]') AND type in (N'U'))
DROP TABLE [dbo].[BaseParameters]
GO
/****** Object:  Table [dbo].[TypeRefs_GenericArgumentsCollection]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TypeRefs_GenericArgumentsCollection]') AND type in (N'U'))
DROP TABLE [dbo].[TypeRefs_GenericArgumentsCollection]
GO
/****** Object:  Table [dbo].[Visuals]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Visuals]') AND type in (N'U'))
DROP TABLE [dbo].[Visuals]
GO
/****** Object:  Table [dbo].[PresentableModelDescriptors]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PresentableModelDescriptors]') AND type in (N'U'))
DROP TABLE [dbo].[PresentableModelDescriptors]
GO
/****** Object:  Table [dbo].[PresenterInfos]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PresenterInfos]') AND type in (N'U'))
DROP TABLE [dbo].[PresenterInfos]
GO
/****** Object:  Table [dbo].[Structs]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Structs]') AND type in (N'U'))
DROP TABLE [dbo].[Structs]
GO
/****** Object:  Table [dbo].[TypeRefs]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TypeRefs]') AND type in (N'U'))
DROP TABLE [dbo].[TypeRefs]
GO
/****** Object:  Table [dbo].[ControlInfos]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ControlInfos]') AND type in (N'U'))
DROP TABLE [dbo].[ControlInfos]
GO
/****** Object:  Table [dbo].[Methods]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Methods]') AND type in (N'U'))
DROP TABLE [dbo].[Methods]
GO
/****** Object:  Table [dbo].[Enumerations]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Enumerations]') AND type in (N'U'))
DROP TABLE [dbo].[Enumerations]
GO
/****** Object:  Table [dbo].[Interfaces]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Interfaces]') AND type in (N'U'))
DROP TABLE [dbo].[Interfaces]
GO
/****** Object:  Table [dbo].[DataTypes]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DataTypes]') AND type in (N'U'))
DROP TABLE [dbo].[DataTypes]
GO
/****** Object:  Table [dbo].[Assemblies]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Assemblies]') AND type in (N'U'))
DROP TABLE [dbo].[Assemblies]
GO
/****** Object:  Table [dbo].[Auftraege]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auftraege]') AND type in (N'U'))
DROP TABLE [dbo].[Auftraege]
GO
/****** Object:  Table [dbo].[TestObjClasses]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TestObjClasses]') AND type in (N'U'))
DROP TABLE [dbo].[TestObjClasses]
GO
/****** Object:  Table [dbo].[WorkEffortAccounts_MitarbeiterCollection]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkEffortAccounts_MitarbeiterCollection]') AND type in (N'U'))
DROP TABLE [dbo].[WorkEffortAccounts_MitarbeiterCollection]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tasks]') AND type in (N'U'))
DROP TABLE [dbo].[Tasks]
GO
/****** Object:  Table [dbo].[Projekte_MitarbeiterCollection]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Projekte_MitarbeiterCollection]') AND type in (N'U'))
DROP TABLE [dbo].[Projekte_MitarbeiterCollection]
GO
/****** Object:  Table [dbo].[Kunden_EMailsCollection]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Kunden_EMailsCollection]') AND type in (N'U'))
DROP TABLE [dbo].[Kunden_EMailsCollection]
GO
/****** Object:  Table [dbo].[LastTests]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastTests]') AND type in (N'U'))
DROP TABLE [dbo].[LastTests]
GO
/****** Object:  Table [dbo].[LeistungsEinträge]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LeistungsEinträge]') AND type in (N'U'))
DROP TABLE [dbo].[LeistungsEinträge]
GO
/****** Object:  Table [dbo].[ObjectForDeletedProperties]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObjectForDeletedProperties]') AND type in (N'U'))
DROP TABLE [dbo].[ObjectForDeletedProperties]
GO
/****** Object:  Table [dbo].[PresenceRecords]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PresenceRecords]') AND type in (N'U'))
DROP TABLE [dbo].[PresenceRecords]
GO
/****** Object:  Table [dbo].[Projekte]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Projekte]') AND type in (N'U'))
DROP TABLE [dbo].[Projekte]
GO
/****** Object:  Table [dbo].[Kunden]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Kunden]') AND type in (N'U'))
DROP TABLE [dbo].[Kunden]
GO
/****** Object:  Table [dbo].[TestCustomObjects]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TestCustomObjects]') AND type in (N'U'))
DROP TABLE [dbo].[TestCustomObjects]
GO
/****** Object:  Table [dbo].[WorkEfforts]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkEfforts]') AND type in (N'U'))
DROP TABLE [dbo].[WorkEfforts]
GO
/****** Object:  Table [dbo].[WorkEffortAccounts]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkEffortAccounts]') AND type in (N'U'))
DROP TABLE [dbo].[WorkEffortAccounts]
GO
/****** Object:  Table [dbo].[AnotherTests]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AnotherTests]') AND type in (N'U'))
DROP TABLE [dbo].[AnotherTests]
GO
/****** Object:  Table [dbo].[CurrentSchema]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CurrentSchema]') AND type in (N'U'))
DROP TABLE [dbo].[CurrentSchema]
GO
/****** Object:  Table [dbo].[Icons]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Icons]') AND type in (N'U'))
DROP TABLE [dbo].[Icons]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_ColumnExists]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_ColumnExists]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_ColumnExists]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_FKConstraintExists]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_FKConstraintExists]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_FKConstraintExists]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_TableExists]    Script Date: 05/22/2009 17:30:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_TableExists]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_TableExists]
GO
/****** Object:  Table [dbo].[Mitarbeiter]    Script Date: 05/22/2009 17:30:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Mitarbeiter]') AND type in (N'U'))
DROP TABLE [dbo].[Mitarbeiter]
GO
/****** Object:  Table [dbo].[Modules]    Script Date: 05/22/2009 17:30:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Modules]') AND type in (N'U'))
DROP TABLE [dbo].[Modules]
GO
/****** Object:  Table [dbo].[Muhblas]    Script Date: 05/22/2009 17:30:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Muhblas]') AND type in (N'U'))
DROP TABLE [dbo].[Muhblas]
GO
/****** Object:  Table [dbo].[Muhblas]    Script Date: 05/22/2009 17:30:27 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Modules]    Script Date: 05/22/2009 17:30:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Modules]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Modules](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Namespace] [nvarchar](200) NOT NULL,
	[ModuleName] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[ExportGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Module] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Modules] ON
INSERT [dbo].[Modules] ([ID], [Namespace], [ModuleName], [Description], [ExportGuid]) VALUES (1, N'Kistl.App.Base', N'KistlBase', N'', N'ef6d5bd8-5826-4ce6-a9ca-c8438ddde773')
INSERT [dbo].[Modules] ([ID], [Namespace], [ModuleName], [Description], [ExportGuid]) VALUES (2, N'Kistl.App.Projekte', N'Projekte', N'', N'a56d9701-d98e-49ab-bd49-a491d09e2d46')
INSERT [dbo].[Modules] ([ID], [Namespace], [ModuleName], [Description], [ExportGuid]) VALUES (3, N'Kistl.App.TimeRecords', N'TimeRecords', N'', N'fb309065-8f8a-4f95-8794-9767c6b08f6c')
INSERT [dbo].[Modules] ([ID], [Namespace], [ModuleName], [Description], [ExportGuid]) VALUES (4, N'Kistl.App.GUI', N'GUI', N'', N'84f486f7-19fe-49ad-8e4a-6c05089e7684')
INSERT [dbo].[Modules] ([ID], [Namespace], [ModuleName], [Description], [ExportGuid]) VALUES (5, N'Kistl.App.Test', N'TestModule', N'', N'81e8ce31-65eb-46fe-ba86-de7452692d5b')
SET IDENTITY_INSERT [dbo].[Modules] OFF
/****** Object:  Table [dbo].[Mitarbeiter]    Script Date: 05/22/2009 17:30:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Mitarbeiter]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Mitarbeiter](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Geburtstag] [datetime] NULL,
	[SVNr] [nvarchar](20) NULL,
	[TelefonNummer] [nvarchar](50) NULL,
 CONSTRAINT [PK_Mitarbeiter] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Mitarbeiter] ON
INSERT [dbo].[Mitarbeiter] ([ID], [Name], [Geburtstag], [SVNr], [TelefonNummer]) VALUES (1, N'DI David Schmitt', NULL, N'asdf', NULL)
INSERT [dbo].[Mitarbeiter] ([ID], [Name], [Geburtstag], [SVNr], [TelefonNummer]) VALUES (2, N'Arthur Zaczek', CAST(0x00006F9700000000 AS DateTime), N'1234', N'')
INSERT [dbo].[Mitarbeiter] ([ID], [Name], [Geburtstag], [SVNr], [TelefonNummer]) VALUES (3, N'Susanne Dobler', NULL, NULL, NULL)
INSERT [dbo].[Mitarbeiter] ([ID], [Name], [Geburtstag], [SVNr], [TelefonNummer]) VALUES (4, N'Josef Pfleger', NULL, N'123', NULL)
INSERT [dbo].[Mitarbeiter] ([ID], [Name], [Geburtstag], [SVNr], [TelefonNummer]) VALUES (5, N'Peter Mayer', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Mitarbeiter] OFF
/****** Object:  UserDefinedFunction [dbo].[fn_TableExists]    Script Date: 05/22/2009 17:30:29 ******/
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
/****** Object:  UserDefinedFunction [dbo].[fn_FKConstraintExists]    Script Date: 05/22/2009 17:30:29 ******/
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
/****** Object:  UserDefinedFunction [dbo].[fn_ColumnExists]    Script Date: 05/22/2009 17:30:29 ******/
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
/****** Object:  Table [dbo].[Icons]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Icons]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Icons](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IconFile] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Icon] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
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
/****** Object:  Table [dbo].[CurrentSchema]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CurrentSchema]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CurrentSchema](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Schema] [ntext] NOT NULL,
 CONSTRAINT [PK_CurrentSchema] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AnotherTests]    Script Date: 05/22/2009 17:30:29 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[WorkEffortAccounts]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkEffortAccounts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkEffortAccounts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[BudgetHours] [float] NULL,
	[SpentHours] [float] NULL,
	[Notes] [nvarchar](4000) NULL,
 CONSTRAINT [PK_WorkEffortAccount] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[WorkEffortAccounts] ON
INSERT [dbo].[WorkEffortAccounts] ([ID], [Name], [BudgetHours], [SpentHours], [Notes]) VALUES (2, N'Kistlkostenträger', 70, 41.5, NULL)
INSERT [dbo].[WorkEffortAccounts] ([ID], [Name], [BudgetHours], [SpentHours], [Notes]) VALUES (3, N'Urlaube', NULL, 8, NULL)
INSERT [dbo].[WorkEffortAccounts] ([ID], [Name], [BudgetHours], [SpentHours], [Notes]) VALUES (4, N'Neuer Kostenträger für Ziviltechniker', 40, 0, NULL)
INSERT [dbo].[WorkEffortAccounts] ([ID], [Name], [BudgetHours], [SpentHours], [Notes]) VALUES (8, N'test', 100, 0, NULL)
INSERT [dbo].[WorkEffortAccounts] ([ID], [Name], [BudgetHours], [SpentHours], [Notes]) VALUES (9, N'test3333', 111, 0, NULL)
INSERT [dbo].[WorkEffortAccounts] ([ID], [Name], [BudgetHours], [SpentHours], [Notes]) VALUES (10, N'Test für''s rechnungswesen', 1, 0, NULL)
INSERT [dbo].[WorkEffortAccounts] ([ID], [Name], [BudgetHours], [SpentHours], [Notes]) VALUES (11, N'asdf', 1, 0, NULL)
SET IDENTITY_INSERT [dbo].[WorkEffortAccounts] OFF
/****** Object:  Table [dbo].[WorkEfforts]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkEfforts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkEfforts](
	[ID] [int] NOT NULL,
	[From] [datetime] NOT NULL,
	[Thru] [datetime] NULL,
	[Name] [nvarchar](400) NULL,
	[Notes] [nvarchar](4000) NULL,
	[fk_Mitarbeiter] [int] NOT NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[TestCustomObjects]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TestCustomObjects]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TestCustomObjects](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PersonName] [nvarchar](200) NOT NULL,
	[PhoneNumberMobile_Number] [nvarchar](50) NULL,
	[PhoneNumberMobile_AreaCode] [nvarchar](50) NULL,
	[PhoneNumberOffice_Number] [nvarchar](50) NULL,
	[PhoneNumberOffice_AreaCode] [nvarchar](50) NULL,
	[Birthday] [datetime] NULL,
 CONSTRAINT [PK_TestCustomObjects] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[TestCustomObjects] ON
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (1, N'TestPerson 1631444502', N'1768014687', N'1', N'0.0483946614192774', N'1', CAST(0x00009AF200F5684B AS DateTime))
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
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (16, N'TestPerson 1317848075', N'1248558912', N'1', N'1248558912', N'1', CAST(0x00009B5A00EFF61D AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (17, N'TestPerson 1322696194', N'497878945', N'1', N'497878945', N'1', CAST(0x00009B5A00F126FB AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (18, N'TestPerson 299301653', N'1462843748', N'1', N'1462843748', N'1', CAST(0x00009B5A00F1C6DD AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (19, N'TestPerson 1247437002', N'1069636784', N'1', N'1069636784', N'1', CAST(0x00009B5A00F5A412 AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (20, N'TestPerson 429153935', N'748836558', N'1', N'748836558', N'1', CAST(0x00009B5A00F741DD AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (21, N'TestPerson 1006129277', N'819641689', N'1', N'819641689', N'1', CAST(0x00009B5A00FDC17C AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (22, N'TestPerson 847866517', N'1366896772', N'1', N'1366896772', N'1', CAST(0x00009B5A010030B5 AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (23, N'TestPerson 843087413', N'541098405', N'1', N'541098405', N'1', CAST(0x00009B5A010B0A29 AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (24, N'TestPerson 981200617', N'897968082', N'1', N'897968082', N'1', CAST(0x00009B5A010CC3FB AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (25, N'TestPerson 883845878', N'289208922', N'1', N'289208922', N'1', CAST(0x00009B5B00C9E845 AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (26, N'TestPerson 354825311', N'357282141', N'1', N'357282141', N'1', CAST(0x00009B5B00CBCAB0 AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (27, N'TestPerson 905620022', N'1775521091', N'1', N'1775521091', N'1', CAST(0x00009B5B00CC2544 AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (28, N'TestPerson 23266482', N'1310781919', N'1', N'1310781919', N'1', CAST(0x00009B5B00CD3E30 AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (29, N'TestPerson 671814158', N'1606859041', N'1', N'1606859041', N'1', CAST(0x00009B5B00CDB0DB AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (30, N'TestPerson 1061875724', N'148072994', N'1', N'148072994', N'1', CAST(0x00009B5B00D82E92 AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (31, N'TestPerson 1170462528', N'1710876255', N'1', N'1710876255', N'1', CAST(0x00009B5B00D902AB AS DateTime))
INSERT [dbo].[TestCustomObjects] ([ID], [PersonName], [PhoneNumberMobile_Number], [PhoneNumberMobile_AreaCode], [PhoneNumberOffice_Number], [PhoneNumberOffice_AreaCode], [Birthday]) VALUES (32, N'TestPerson 48153817', N'1314816534', N'1', N'1314816534', N'1', CAST(0x00009B5B011FB70F AS DateTime))
SET IDENTITY_INSERT [dbo].[TestCustomObjects] OFF
/****** Object:  Table [dbo].[Kunden]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Kunden]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Kunden](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Kundenname] [nvarchar](200) NOT NULL,
	[Adresse] [nvarchar](200) NULL,
	[PLZ] [nvarchar](10) NOT NULL,
	[Ort] [nvarchar](100) NULL,
	[Land] [nvarchar](50) NULL,
 CONSTRAINT [PK_Kunde] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Kunden] ON
INSERT [dbo].[Kunden] ([ID], [Kundenname], [Adresse], [PLZ], [Ort], [Land]) VALUES (1, N'Accent2', N'Kremserstraße 1', N'2000', N'Krems', N'AT')
INSERT [dbo].[Kunden] ([ID], [Kundenname], [Adresse], [PLZ], [Ort], [Land]) VALUES (2, N'Susanne Dobler', N'Leopoldgasse', N'3400', N'Klosterneuburg', N'AT')
INSERT [dbo].[Kunden] ([ID], [Kundenname], [Adresse], [PLZ], [Ort], [Land]) VALUES (3, N'Bernhard', N'Bernhardstraße 1', N'1190', N'Wien', N'AT')
SET IDENTITY_INSERT [dbo].[Kunden] OFF
/****** Object:  Table [dbo].[Projekte]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Projekte]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Projekte](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[AufwandGes] [float] NULL,
	[Kundenname] [nvarchar](100) NULL,
 CONSTRAINT [PK_Projekte] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Projekte] ON
INSERT [dbo].[Projekte] ([ID], [Name], [AufwandGes], [Kundenname]) VALUES (1, N'Kistl', 0, N'Wir selbst')
INSERT [dbo].[Projekte] ([ID], [Name], [AufwandGes], [Kundenname]) VALUES (2, N'Ziviltechniker', 65, N'test')
INSERT [dbo].[Projekte] ([ID], [Name], [AufwandGes], [Kundenname]) VALUES (3, N'WebCMS.net V1.0', 0, NULL)
INSERT [dbo].[Projekte] ([ID], [Name], [AufwandGes], [Kundenname]) VALUES (4, N'Rechnungswesen', 2, NULL)
INSERT [dbo].[Projekte] ([ID], [Name], [AufwandGes], [Kundenname]) VALUES (5, N'Neues Projekt test', 11, NULL)
INSERT [dbo].[Projekte] ([ID], [Name], [AufwandGes], [Kundenname]) VALUES (6, N'test_abc2', 20, NULL)
INSERT [dbo].[Projekte] ([ID], [Name], [AufwandGes], [Kundenname]) VALUES (7, N'Neues Objekt', 0, N'')
INSERT [dbo].[Projekte] ([ID], [Name], [AufwandGes], [Kundenname]) VALUES (12, N'Neues Projekt im Context', 103, NULL)
INSERT [dbo].[Projekte] ([ID], [Name], [AufwandGes], [Kundenname]) VALUES (17, N'Another Project', NULL, NULL)
INSERT [dbo].[Projekte] ([ID], [Name], [AufwandGes], [Kundenname]) VALUES (18, N'Muhblah', 11, N'asdf')
INSERT [dbo].[Projekte] ([ID], [Name], [AufwandGes], [Kundenname]) VALUES (19, N'WebProjekt', 1000, N'test')
SET IDENTITY_INSERT [dbo].[Projekte] OFF
/****** Object:  Table [dbo].[PresenceRecords]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PresenceRecords]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PresenceRecords](
	[ID] [int] NOT NULL,
	[From] [datetime] NOT NULL,
	[Thru] [datetime] NULL,
	[fk_Mitarbeiter] [int] NOT NULL,
 CONSTRAINT [PK_PresenceRecords] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ObjectForDeletedProperties]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObjectForDeletedProperties]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ObjectForDeletedProperties](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[test] [nvarchar](50) NOT NULL,
	[DataType] [nvarchar](100) NULL,
	[IsAssociation] [bit] NULL,
	[Namespace] [nvarchar](100) NULL,
	[ReferenceObjectClassName] [nvarchar](200) NULL,
	[ReferencePropertyName] [nvarchar](200) NULL,
 CONSTRAINT [PK_ObjectForDeletedProperties] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[LeistungsEinträge]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LeistungsEinträge]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LeistungsEinträge](
	[ID] [int] NOT NULL,
	[Anfang] [datetime] NOT NULL,
	[Ende] [datetime] NULL,
	[Bezeichnung] [nvarchar](400) NULL,
	[Notizen] [nvarchar](4000) NULL,
	[fk_Mitarbeiter] [int] NOT NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[LastTests]    Script Date: 05/22/2009 17:30:29 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Kunden_EMailsCollection]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Kunden_EMailsCollection]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Kunden_EMailsCollection](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_Kunde] [int] NOT NULL,
	[EMails] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Kunden_EMailsCollection] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Kunden_EMailsCollection] ON
INSERT [dbo].[Kunden_EMailsCollection] ([ID], [fk_Kunde], [EMails]) VALUES (6, 2, N'susanne.dobler@mail.com')
INSERT [dbo].[Kunden_EMailsCollection] ([ID], [fk_Kunde], [EMails]) VALUES (42, 1, N'UnitTest21.11.2008 13:10:03@dasz.at')
INSERT [dbo].[Kunden_EMailsCollection] ([ID], [fk_Kunde], [EMails]) VALUES (43, 1, N'UnitTest21.11.2008 17:27:28@dasz.at')
INSERT [dbo].[Kunden_EMailsCollection] ([ID], [fk_Kunde], [EMails]) VALUES (45, 1, N'')
INSERT [dbo].[Kunden_EMailsCollection] ([ID], [fk_Kunde], [EMails]) VALUES (46, 1, N'')
SET IDENTITY_INSERT [dbo].[Kunden_EMailsCollection] OFF
/****** Object:  Table [dbo].[Projekte_MitarbeiterCollection]    Script Date: 05/22/2009 17:30:29 ******/
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
	[fk_Projekt_pos] [int] NULL,
	[fk_Mitarbeiter_pos] [int] NULL,
 CONSTRAINT [PK_Projekte_MitarbeiterCollection] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Projekte_MitarbeiterCollection] ON
INSERT [dbo].[Projekte_MitarbeiterCollection] ([ID], [fk_Projekt], [fk_Mitarbeiter], [fk_Projekt_pos], [fk_Mitarbeiter_pos]) VALUES (8, 4, 2, NULL, NULL)
INSERT [dbo].[Projekte_MitarbeiterCollection] ([ID], [fk_Projekt], [fk_Mitarbeiter], [fk_Projekt_pos], [fk_Mitarbeiter_pos]) VALUES (9, 3, 3, NULL, NULL)
INSERT [dbo].[Projekte_MitarbeiterCollection] ([ID], [fk_Projekt], [fk_Mitarbeiter], [fk_Projekt_pos], [fk_Mitarbeiter_pos]) VALUES (20, 17, 4, NULL, NULL)
INSERT [dbo].[Projekte_MitarbeiterCollection] ([ID], [fk_Projekt], [fk_Mitarbeiter], [fk_Projekt_pos], [fk_Mitarbeiter_pos]) VALUES (26, 2, 5, -1, 2)
INSERT [dbo].[Projekte_MitarbeiterCollection] ([ID], [fk_Projekt], [fk_Mitarbeiter], [fk_Projekt_pos], [fk_Mitarbeiter_pos]) VALUES (28, 2, 2, -1, 0)
INSERT [dbo].[Projekte_MitarbeiterCollection] ([ID], [fk_Projekt], [fk_Mitarbeiter], [fk_Projekt_pos], [fk_Mitarbeiter_pos]) VALUES (29, 2, 1, -1, 2)
INSERT [dbo].[Projekte_MitarbeiterCollection] ([ID], [fk_Projekt], [fk_Mitarbeiter], [fk_Projekt_pos], [fk_Mitarbeiter_pos]) VALUES (30, 5, 1, -1, 0)
INSERT [dbo].[Projekte_MitarbeiterCollection] ([ID], [fk_Projekt], [fk_Mitarbeiter], [fk_Projekt_pos], [fk_Mitarbeiter_pos]) VALUES (31, 18, 1, -1, 0)
SET IDENTITY_INSERT [dbo].[Projekte_MitarbeiterCollection] OFF
/****** Object:  Table [dbo].[Tasks]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tasks]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tasks](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[DatumVon] [datetime] NULL,
	[DatumBis] [datetime] NULL,
	[Aufwand] [float] NULL,
	[fk_Projekt] [int] NULL,
	[fk_Projekt_pos] [int] NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Tasks] ON
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (1, N'Planung Ziviltechniker', CAST(0x000098A900000000 AS DateTime), CAST(0x000098C800000000 AS DateTime), 44, 2, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (2, N'Umsetzung Ziviltechniker', CAST(0x000098A900000000 AS DateTime), CAST(0x000098C800000000 AS DateTime), 10, 2, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (3, N'Planung Kistl', CAST(0x0000988F00000000 AS DateTime), CAST(0x000098E400000000 AS DateTime), 10, NULL, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (5, N'Ein Test2', CAST(0x0000901A00000000 AS DateTime), CAST(0x0000901A00000000 AS DateTime), 10, 5, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (7, N'Neue Aufgabe', CAST(0x000098A900000000 AS DateTime), CAST(0x000098E400000000 AS DateTime), 10, 6, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (8, N'Zweite Aufgabe', CAST(0x000098A900000000 AS DateTime), CAST(0x000098AC00000000 AS DateTime), 10, 6, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (9, N'Buchhaltungsprogramm schreiben', NULL, NULL, 10, 17, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (10, N'Neuer Task', CAST(0x00009A0400000000 AS DateTime), CAST(0x00009A0500000000 AS DateTime), 1, 4, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (11, N'test', NULL, NULL, 1, 12, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (12, N'test2', NULL, NULL, 101, 12, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (13, N'NUnit Test Task', CAST(0x00009A6E00EAD54C AS DateTime), CAST(0x00009A6F00EAD54C AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (14, N'NUnit Test Task', CAST(0x00009A6E00EB35CE AS DateTime), CAST(0x00009A6F00EB35CE AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (15, N'NUnit Test Task', CAST(0x00009A6E00F414C2 AS DateTime), CAST(0x00009A6F00F414C2 AS DateTime), 1, 4, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (16, N'NUnit Test Task', CAST(0x00009A6E00F4712C AS DateTime), CAST(0x00009A6F00F4712C AS DateTime), 1, 5, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (17, N'NUnit Test Task', CAST(0x00009A6E00F49D28 AS DateTime), CAST(0x00009A6F00F49D28 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (18, N'NUnit Test Task', CAST(0x00009A6E00F4BBF6 AS DateTime), CAST(0x00009A6F00F4BBF6 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (19, N'TestTask', NULL, NULL, 1, 17, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (21, N'asdf', NULL, NULL, 2, 17, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (22, N'asdf', NULL, NULL, NULL, 17, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (23, N'test', NULL, NULL, 11, 18, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (24, N'New Task for Testing', NULL, NULL, 11, 2, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (25, N'NUnit Test Task', CAST(0x00009AE20123BCE6 AS DateTime), CAST(0x00009AE30123BCE6 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (26, N'NUnit Test Task', CAST(0x00009AE201257648 AS DateTime), CAST(0x00009AE301257648 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (27, N'NUnit Test Task', CAST(0x00009AE2012B3EC7 AS DateTime), CAST(0x00009AE3012B3EC7 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (28, N'NUnit Test Task', CAST(0x00009AE2013563AA AS DateTime), CAST(0x00009AE3013563AA AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (29, N'NUnit Test Task', CAST(0x00009AE20135A0BD AS DateTime), CAST(0x00009AE30135A0BD AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (30, N'NUnit Test Task', CAST(0x00009AE400B09584 AS DateTime), CAST(0x00009AE500B09584 AS DateTime), 1, 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (31, N'NUnit Test Task', CAST(0x00009AE400B10129 AS DateTime), CAST(0x00009AE500B10129 AS DateTime), 1, 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (32, N'NUnit Test Task', CAST(0x00009AE400CBC965 AS DateTime), CAST(0x00009AE500CBC965 AS DateTime), 1, 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (33, N'NUnit Test Task', CAST(0x00009AE40132AF38 AS DateTime), CAST(0x00009AE50132AF38 AS DateTime), 1, 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (34, N'NUnit Test Task', CAST(0x00009B4601153802 AS DateTime), CAST(0x00009B4701153802 AS DateTime), 1, 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (35, N'NUnit Test Task', CAST(0x00009B4601157396 AS DateTime), CAST(0x00009B4701157396 AS DateTime), 1, 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (36, N'NUnit Test Task', CAST(0x00009B460126C20C AS DateTime), CAST(0x00009B470126C20C AS DateTime), 1, 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (37, N'NUnit Test Task', CAST(0x00009B4900916C00 AS DateTime), CAST(0x00009B4A00916C00 AS DateTime), 1, 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (38, N'NUnit Test Task', CAST(0x00009B5A00EFF492 AS DateTime), CAST(0x00009B5B00EFF492 AS DateTime), 1, 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (39, N'NUnit Test Task', CAST(0x00009B5A00F125BC AS DateTime), CAST(0x00009B5B00F125BC AS DateTime), 1, 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (40, N'NUnit Test Task', CAST(0x00009B5A00F1C5A7 AS DateTime), CAST(0x00009B5B00F1C5A7 AS DateTime), 1, 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (41, N'NUnit Test Task', CAST(0x00009B5A00F5A2B5 AS DateTime), CAST(0x00009B5B00F5A2B5 AS DateTime), 1, 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (42, N'NUnit Test Task', CAST(0x00009B5A00F740AE AS DateTime), CAST(0x00009B5B00F740AE AS DateTime), 1, 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (43, N'NUnit Test Task', CAST(0x00009B5A00FDBFC1 AS DateTime), CAST(0x00009B5B00FDBFC1 AS DateTime), 1, 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (44, N'NUnit Test Task', CAST(0x00009B5A01002F4A AS DateTime), CAST(0x00009B5B01002F4A AS DateTime), 1, 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (45, N'NUnit Test Task', CAST(0x00009B5A010B08ED AS DateTime), CAST(0x00009B5B010B08ED AS DateTime), 1, 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (46, N'NUnit Test Task', CAST(0x00009B5A010CC296 AS DateTime), CAST(0x00009B5B010CC296 AS DateTime), 1, 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (47, N'NUnit Test Task', CAST(0x00009B5B00C9E6F3 AS DateTime), CAST(0x00009B5C00C9E6F3 AS DateTime), 1, 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (48, N'NUnit Test Task', CAST(0x00009B5B00CBC993 AS DateTime), CAST(0x00009B5C00CBC993 AS DateTime), 1, 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (49, N'NUnit Test Task', CAST(0x00009B5B00CC2433 AS DateTime), CAST(0x00009B5C00CC2433 AS DateTime), 1, 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (50, N'NUnit Test Task', CAST(0x00009B5B00CD3CD3 AS DateTime), CAST(0x00009B5C00CD3CD3 AS DateTime), 1, 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (51, N'NUnit Test Task', CAST(0x00009B5B00CDAFB2 AS DateTime), CAST(0x00009B5C00CDAFB2 AS DateTime), 1, 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (52, N'NUnit Test Task', CAST(0x00009B5B00D82D6E AS DateTime), CAST(0x00009B5C00D82D6E AS DateTime), 1, 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (53, N'NUnit Test Task', CAST(0x00009B5B00D90182 AS DateTime), CAST(0x00009B5C00D90182 AS DateTime), 1, 1, NULL)
INSERT [dbo].[Tasks] ([ID], [Name], [DatumVon], [DatumBis], [Aufwand], [fk_Projekt], [fk_Projekt_pos]) VALUES (54, N'NUnit Test Task', CAST(0x00009B5B011FB5B0 AS DateTime), CAST(0x00009B5C011FB5B0 AS DateTime), 1, 1, NULL)
SET IDENTITY_INSERT [dbo].[Tasks] OFF
/****** Object:  Table [dbo].[WorkEffortAccounts_MitarbeiterCollection]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkEffortAccounts_MitarbeiterCollection]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkEffortAccounts_MitarbeiterCollection](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_WorkEffortAccount] [int] NOT NULL,
	[fk_Mitarbeiter] [int] NOT NULL,
 CONSTRAINT [PK_WorkEffortAccounts_MitarbeiterCollection] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[WorkEffortAccounts_MitarbeiterCollection] ON
INSERT [dbo].[WorkEffortAccounts_MitarbeiterCollection] ([ID], [fk_WorkEffortAccount], [fk_Mitarbeiter]) VALUES (3, 3, 3)
INSERT [dbo].[WorkEffortAccounts_MitarbeiterCollection] ([ID], [fk_WorkEffortAccount], [fk_Mitarbeiter]) VALUES (4, 3, 4)
INSERT [dbo].[WorkEffortAccounts_MitarbeiterCollection] ([ID], [fk_WorkEffortAccount], [fk_Mitarbeiter]) VALUES (5, 2, 2)
INSERT [dbo].[WorkEffortAccounts_MitarbeiterCollection] ([ID], [fk_WorkEffortAccount], [fk_Mitarbeiter]) VALUES (6, 2, 1)
INSERT [dbo].[WorkEffortAccounts_MitarbeiterCollection] ([ID], [fk_WorkEffortAccount], [fk_Mitarbeiter]) VALUES (14, 2, 4)
INSERT [dbo].[WorkEffortAccounts_MitarbeiterCollection] ([ID], [fk_WorkEffortAccount], [fk_Mitarbeiter]) VALUES (15, 2, 3)
SET IDENTITY_INSERT [dbo].[WorkEffortAccounts_MitarbeiterCollection] OFF
/****** Object:  Table [dbo].[TestObjClasses]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TestObjClasses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TestObjClasses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StringProp] [nvarchar](200) NOT NULL,
	[TestEnumProp] [int] NOT NULL,
	[fk_ObjectProp] [int] NOT NULL,
	[MyIntProperty] [int] NULL,
	[fk_ObjectProp_pos] [int] NULL,
 CONSTRAINT [PK_TestObjClasses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[TestObjClasses] ON
INSERT [dbo].[TestObjClasses] ([ID], [StringProp], [TestEnumProp], [fk_ObjectProp], [MyIntProperty], [fk_ObjectProp_pos]) VALUES (5, N'First', 0, 1, NULL, NULL)
INSERT [dbo].[TestObjClasses] ([ID], [StringProp], [TestEnumProp], [fk_ObjectProp], [MyIntProperty], [fk_ObjectProp_pos]) VALUES (6, N'Second', 1, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[TestObjClasses] OFF
/****** Object:  Table [dbo].[Auftraege]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auftraege]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Auftraege](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_Mitarbeiter] [int] NULL,
	[Auftragsname] [nvarchar](200) NULL,
	[fk_Projekt] [int] NULL,
	[fk_Kunde] [int] NULL,
	[Auftragswert] [float] NULL,
	[fk_Mitarbeiter_pos] [int] NULL,
	[fk_Projekt_pos] [int] NULL,
	[fk_Kunde_pos] [int] NULL,
 CONSTRAINT [PK_Auftrag] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Auftraege] ON
INSERT [dbo].[Auftraege] ([ID], [fk_Mitarbeiter], [Auftragsname], [fk_Projekt], [fk_Kunde], [Auftragswert], [fk_Mitarbeiter_pos], [fk_Projekt_pos], [fk_Kunde_pos]) VALUES (1, 1, N'Auftrag für den Businessplan', NULL, 1, 10, NULL, NULL, NULL)
INSERT [dbo].[Auftraege] ([ID], [fk_Mitarbeiter], [Auftragsname], [fk_Projekt], [fk_Kunde], [Auftragswert], [fk_Mitarbeiter_pos], [fk_Projekt_pos], [fk_Kunde_pos]) VALUES (2, 2, N'Auftrag für Marketingunterlagen', NULL, 2, 100, NULL, NULL, NULL)
INSERT [dbo].[Auftraege] ([ID], [fk_Mitarbeiter], [Auftragsname], [fk_Projekt], [fk_Kunde], [Auftragswert], [fk_Mitarbeiter_pos], [fk_Projekt_pos], [fk_Kunde_pos]) VALUES (3, 2, N'Kistl Implementierungsprojekt', NULL, 1, 100000, NULL, NULL, NULL)
INSERT [dbo].[Auftraege] ([ID], [fk_Mitarbeiter], [Auftragsname], [fk_Projekt], [fk_Kunde], [Auftragswert], [fk_Mitarbeiter_pos], [fk_Projekt_pos], [fk_Kunde_pos]) VALUES (4, NULL, N'testauftrag', NULL, 2, 100, NULL, NULL, NULL)
INSERT [dbo].[Auftraege] ([ID], [fk_Mitarbeiter], [Auftragsname], [fk_Projekt], [fk_Kunde], [Auftragswert], [fk_Mitarbeiter_pos], [fk_Projekt_pos], [fk_Kunde_pos]) VALUES (5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Auftraege] ([ID], [fk_Mitarbeiter], [Auftragsname], [fk_Projekt], [fk_Kunde], [Auftragswert], [fk_Mitarbeiter_pos], [fk_Projekt_pos], [fk_Kunde_pos]) VALUES (6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Auftraege] ([ID], [fk_Mitarbeiter], [Auftragsname], [fk_Projekt], [fk_Kunde], [Auftragswert], [fk_Mitarbeiter_pos], [fk_Projekt_pos], [fk_Kunde_pos]) VALUES (7, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Auftraege] ([ID], [fk_Mitarbeiter], [Auftragsname], [fk_Projekt], [fk_Kunde], [Auftragswert], [fk_Mitarbeiter_pos], [fk_Projekt_pos], [fk_Kunde_pos]) VALUES (8, 5, N'Auftrag Ziviltechniker', 2, 3, 111111, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Auftraege] OFF
/****** Object:  Table [dbo].[Assemblies]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Assemblies]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Assemblies](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_Module] [int] NOT NULL,
	[AssemblyName] [nvarchar](200) NOT NULL,
	[IsClientAssembly] [bit] NOT NULL,
	[fk_Module_pos] [int] NULL,
	[ExportGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Assembly] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Assemblies] ON
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly], [fk_Module_pos], [ExportGuid]) VALUES (1, 1, N'Kistl.App.Projekte.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 1, NULL, N'8937dfc1-288e-49ca-a6c6-701e9c230b07')
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly], [fk_Module_pos], [ExportGuid]) VALUES (2, 1, N'Kistl.App.Projekte.Server, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 0, NULL, N'9acf0b2c-a4a1-4418-99b8-3ec69157c3f0')
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly], [fk_Module_pos], [ExportGuid]) VALUES (3, 4, N'Kistl.Client.ASPNET.Toolkit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 0, NULL, N'b4a7503f-38ba-4b60-af41-d702d041c09a')
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly], [fk_Module_pos], [ExportGuid]) VALUES (4, 4, N'Kistl.Client.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 0, NULL, N'2d670f6d-8208-4748-a197-a6ad4cffc392')
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly], [fk_Module_pos], [ExportGuid]) VALUES (13, 4, N'Kistl.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 0, NULL, N'242a1ea6-e8e1-420c-824d-0d2707ece25d')
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly], [fk_Module_pos], [ExportGuid]) VALUES (14, 4, N'Kistl.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 0, NULL, N'ccb4bfc9-d76a-43be-963e-bc3712e6b405')
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly], [fk_Module_pos], [ExportGuid]) VALUES (15, 4, N'Kistl.API, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 0, NULL, N'ba51f88e-57df-49cb-a907-b25e1091487b')
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly], [fk_Module_pos], [ExportGuid]) VALUES (16, 4, N'Kistl.Client.ASPNET.Toolkit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 0, NULL, N'eb5c0841-a9bb-4262-bb9a-5aed5a7086c7')
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly], [fk_Module_pos], [ExportGuid]) VALUES (17, 4, N'Kistl.Client.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 0, NULL, N'62afd077-5c95-46ce-8daa-ffda715ad3f5')
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly], [fk_Module_pos], [ExportGuid]) VALUES (18, 4, N'Kistl.Client.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 0, NULL, N'157c1754-6eed-49f7-883b-ae2543bafd7e')
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly], [fk_Module_pos], [ExportGuid]) VALUES (29, 4, N'mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', 0, NULL, N'59dbd4ab-f5d2-455f-8181-acffe0fcec0b')
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly], [fk_Module_pos], [ExportGuid]) VALUES (30, 4, N'Kistl.API.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 0, NULL, N'15b6b4c4-6a16-485a-aa55-017b0c76fc56')
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly], [fk_Module_pos], [ExportGuid]) VALUES (31, 4, N'WindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35', 0, NULL, N'331a8ad3-8ca2-411c-9823-5f6ab2acfdcb')
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly], [fk_Module_pos], [ExportGuid]) VALUES (32, 4, N'PresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35', 0, NULL, N'c218dca0-cb1a-4d74-aaf0-0180604c4f4c')
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly], [fk_Module_pos], [ExportGuid]) VALUES (33, 4, N'PresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35', 0, NULL, N'8ab0933e-9dda-4ae5-9cd8-d87aefe9b555')
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly], [fk_Module_pos], [ExportGuid]) VALUES (34, 4, N'Kistl.Client.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 0, NULL, N'f3a264d3-ea6e-4e03-8ab6-52aa41f7aeac')
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly], [fk_Module_pos], [ExportGuid]) VALUES (35, 4, N'Kistl.Client.ASPNET.Toolkit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 0, NULL, N'2780c2f0-cf4a-406f-8b11-9555662de568')
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly], [fk_Module_pos], [ExportGuid]) VALUES (36, 4, N'System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', 0, NULL, N'1f064a7c-8424-4cea-b640-6a8ad0c31324')
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly], [fk_Module_pos], [ExportGuid]) VALUES (37, 4, N'System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089', 0, NULL, N'2c8b4516-93cd-4a11-92ec-6c4d90cad8ac')
INSERT [dbo].[Assemblies] ([ID], [fk_Module], [AssemblyName], [IsClientAssembly], [fk_Module_pos], [ExportGuid]) VALUES (38, 4, N'Kistl.Client.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 0, NULL, N'1356f091-8beb-4b07-aa2f-010be2c14b76')
SET IDENTITY_INSERT [dbo].[Assemblies] OFF
/****** Object:  Table [dbo].[DataTypes]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DataTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DataTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](51) NOT NULL,
	[fk_DefaultIcon] [int] NULL,
	[fk_Module] [int] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[fk_Module_pos] [int] NULL,
	[fk_DefaultIcon_pos] [int] NULL,
	[ExportGuid] [uniqueidentifier] NOT NULL,
	[ShowIdInLists] [bit] NOT NULL,
	[ShowIconInLists] [bit] NOT NULL,
	[ShowNameInLists] [bit] NOT NULL,
 CONSTRAINT [PK_DataTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[DataTypes] ON
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (2, N'ObjectClass', 11, 1, N'Metadefinition Object for ObjectClasses.', NULL, NULL, N'20888dfc-1fbc-47c8-9f3c-c6a30a5c0048', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (3, N'Projekt', 3, 2, N'', NULL, NULL, N'885939e8-82e1-4fdf-b80e-5f612d5131d3', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (4, N'Task', 7, 2, N'', NULL, NULL, N'3fbb42ca-a084-491d-9135-85ed24f1ef78', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (6, N'Mitarbeiter', 5, 2, N'', NULL, NULL, N'77933a20-338a-4961-b751-62ffa0a75c6a', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (7, N'Property', 9, 1, N'Metadefinition Object for Properties. This class is abstract.', NULL, NULL, N'e5f93f63-9cb8-40a5-8118-d1d9e479370c', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (8, N'ValueTypeProperty', 9, 1, N'Metadefinition Object for ValueType Properties. This class is abstract.', NULL, NULL, N'b0c23c07-950f-47a0-af23-a925dc691b3e', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (9, N'StringProperty', 9, 1, N'Metadefinition Object for String Properties.', NULL, NULL, N'539d6b20-f0cb-461b-b087-a522fec6c838', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (10, N'Method', 2, 1, N'Metadefinition Object for Methods.', NULL, NULL, N'ef79c0b9-55e0-45ad-8233-1ff8f852661f', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (11, N'IntProperty', 9, 1, N'Metadefinition Object for Int Properties.', NULL, NULL, N'6f818a25-4513-4fd3-8ed1-a92429d3910d', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (12, N'BoolProperty', 9, 1, N'Metadefinition Object for Bool Properties.', NULL, NULL, N'3604a7b6-dffb-44ee-8464-5f292d7a0687', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (13, N'DoubleProperty', 9, 1, N'Metadefinition Object for Double Properties.', NULL, NULL, N'404782b3-fbbc-4190-9b96-43dad7177090', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (14, N'ObjectReferenceProperty', 9, 1, N'Metadefinition Object for ObjectReference Properties.', NULL, NULL, N'f1baf69c-a341-4a3a-b321-e782e1458e87', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (15, N'DateTimeProperty', 9, 1, N'Metadefinition Object for DateTime Properties.', NULL, NULL, N'1caadf11-7b95-4c68-8b42-87ac51b01ea0', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (18, N'Module', 1, 1, N'Metadefinition Object for Modules.', NULL, NULL, N'8a1ace7c-77f2-4b48-9b0b-bcb68c660d11', 1, 1, 0)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (19, N'Auftrag', 8, 2, N'', NULL, NULL, N'f6e11d1d-a832-413a-bf1d-5ecf5f7bc79d', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (20, N'WorkEffortAccount', 12, 3, N'An account of work efforts. May be used to limit the hours being expended.', NULL, NULL, N'15268915-6b8f-4884-9b7e-5975e0768301', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (26, N'Kunde', 6, 2, N'', NULL, NULL, N'b9ddd097-4f45-40c5-87e9-7331ab58727c', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (27, N'Icon', 4, 4, N'', NULL, NULL, N'78b6f354-013b-4129-a390-7f3a5a5e28e9', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (29, N'Assembly', 13, 1, N'', NULL, NULL, N'a590a975-66e5-421c-aa97-7ab3169e0e9b', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (30, N'MethodInvocation', 2, 1, N'Metadefinition Object for a MethodInvocation on a Method of a DataType.', NULL, NULL, N'f6e7bb30-3adf-4d49-a80c-6ea7b589afa7', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (33, N'DataType', 11, 1, N'Base Metadefinition Object for Objectclasses, Interfaces, Structs and Enumerations.', NULL, NULL, N'6f005f31-c09c-45f9-9bcb-44090ebf0d1f', 1, 0, 0)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (36, N'BaseParameter', 12, 1, N'Metadefinition Object for Parameter. This class is abstract.', NULL, NULL, N'63b8e3f7-e663-4fde-a09a-64ca876586bd', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (37, N'StringParameter', 12, 1, N'Metadefinition Object for String Parameter.', NULL, NULL, N'd3eee1cb-313d-465a-8a06-732ac119bc75', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (38, N'IntParameter', 12, 1, N'Metadefinition Object for Int Parameter.', NULL, NULL, N'24fb74a4-0d21-49d5-9c81-2ded8948d4d4', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (39, N'DoubleParameter', 12, 1, N'Metadefinition Object for Double Parameter.', NULL, NULL, N'74aa31e4-4dcf-46d5-a8b4-aa02b82bd2df', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (40, N'BoolParameter', 12, 1, N'Metadefinition Object for Bool Parameter.', NULL, NULL, N'2fe66550-c506-4a60-ac88-19db27bf1f4b', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (41, N'DateTimeParameter', 12, 1, N'Metadefinition Object for DateTime Parameter.', NULL, NULL, N'cd6e3f93-5a1d-4c56-bec7-59a951d9fed6', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (42, N'ObjectParameter', 12, 1, N'Metadefinition Object for Object Parameter.', NULL, NULL, N'3fb8bf11-cab6-478f-b9b8-3f6d70a70d37', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (43, N'CLRObjectParameter', 12, 1, N'Metadefinition Object for CLR Object Parameter.', NULL, NULL, N'012dfab4-934b-443e-853a-11a5da5b0627', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (44, N'Interface', 11, 1, N'Metadefinition Object for Interfaces.', NULL, NULL, N'7ea88d99-88f0-44a7-b0a3-da725e57595d', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (45, N'Enumeration', 8, 1, N'Metadefinition Object for Enumerations.', NULL, NULL, N'ee475de2-d626-49e9-9e40-6bb12cb026d4', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (46, N'EnumerationEntry', 8, 1, N'Metadefinition Object for an Enumeration Entry.', NULL, NULL, N'6365c62d-60a6-4fa3-9c78-370ffcc50478', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (47, N'EnumerationProperty', 9, 1, N'Metadefinition Object for Enumeration Properties.', NULL, NULL, N'19a8d9f0-4de6-4cc9-a75d-c1499e3a103b', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (48, N'ITestInterface', 1, 5, N'A Test Interface', NULL, NULL, N'c8ff9958-dd26-4a92-a049-7fa9d51d8bf2', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (50, N'TestEnum', NULL, 5, N'A TestEnum', NULL, NULL, N'67b48828-e7d2-4432-a942-88e96d74b40a', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (51, N'TestObjClass', 5, 5, N'', NULL, NULL, N'19f38f05-e88e-44c6-bfdf-d502b3632028', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (52, N'IRenderer', 2, 4, N'', NULL, NULL, N'2e93e071-875a-41ee-a768-5d55c2683546', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (53, N'Toolkit', 4, 4, N'', NULL, NULL, N'803fb38d-f0d2-409c-9f15-30da4bdcf576', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (54, N'ControlInfo', NULL, 4, N'', NULL, NULL, N'91cd6ed9-536a-4636-aa93-af7c02f2adb4', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (55, N'VisualType', NULL, 4, N'', NULL, NULL, N'3567a11f-1809-44e8-bafe-ce0ccec7ea2a', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (58, N'TestCustomObject', NULL, 5, N'', NULL, NULL, N'de155110-79cc-4dac-89d6-0916608be1fb', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (59, N'Muhblah', NULL, 5, N'', NULL, NULL, N'fd357e42-2c2c-4bef-8110-69a466d09af0', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (60, N'AnotherTest', NULL, 5, N'', NULL, NULL, N'6d00a4e3-75b0-4a56-bc9f-3e9812d9c8fe', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (61, N'LastTest', NULL, 5, N'', NULL, NULL, N'45c39fd8-fea1-41c7-aceb-74efaf7ea3f3', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (62, N'Struct', 11, 1, N'Metadefinition Object for Structs.', NULL, NULL, N'2cb3f778-dd6a-46c7-ad2b-5f8691313035', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (63, N'TestPhoneStruct', NULL, 5, N'', NULL, NULL, N'2510af08-089f-4252-8a98-ec84cb67bcb9', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (64, N'StructProperty', 9, 1, N'Metadefinition Object for Struct Properties.', NULL, NULL, N'7b5ba73f-91f4-4542-9542-4f418b5c109f', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (66, N'PresenterInfo', NULL, 4, N'', NULL, NULL, N'25a4040e-87fe-416d-bed5-111fa1c9cce1', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (67, N'Visual', NULL, 4, N'', NULL, NULL, N'98790e5d-808f-4e0b-8a1a-b304839f07ab', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (68, N'Template', NULL, 4, N'', NULL, NULL, N'c677d3fe-7dfe-4ea5-91e0-d1b0df9118be', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (69, N'Constraint', NULL, 1, N'', NULL, NULL, N'ac1d5ac9-d909-438f-a4f5-f64ea6904944', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (70, N'NotNullableConstraint', NULL, 1, N'', NULL, NULL, N'8604ef0c-f933-4f66-b7df-21d27c9003b2', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (71, N'IntegerRangeConstraint', NULL, 1, N'', NULL, NULL, N'31b03f62-b0d6-49ab-81e9-f912077d4e49', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (73, N'StringRangeConstraint', NULL, 1, N'', NULL, NULL, N'7bb90dc3-2b8c-4cff-ba8e-435ff386a4cf', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (74, N'MethodInvocationConstraint', NULL, 1, N'', NULL, NULL, N'2667704b-ea27-44ff-a6b2-0ef42ffccedc', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (75, N'IsValidIdentifierConstraint', NULL, 1, N'', NULL, NULL, N'ed8f30ad-186f-48ee-8dd5-a153d24dfada', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (76, N'IsValidNamespaceConstraint', NULL, 1, N'', NULL, NULL, N'94916227-138b-49e5-b62e-b982a45a5c21', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (77, N'Relation', 11, 1, N'Describes a Relation between two Object Classes', NULL, NULL, N'1c0e894f-4eb4-422f-8094-3095735b4917', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (78, N'StorageType', 10, 1, N'Storage Type of a 1:1 Releation.', NULL, NULL, N'351a22e8-d047-4878-b6d3-3945489139a0', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (79, N'TypeRef', 2, 1, N'This class models a reference to a specific, concrete Type. Generic Types have all parameters filled.', NULL, NULL, N'87766ae2-89a4-4c37-ab25-583a710c55e5', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (81, N'Multiplicity', NULL, 1, N'Describes the multiplicities of objects on RelationEnds', NULL, NULL, N'4f2b5005-672e-46ef-80ed-94b8cdd053db', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (82, N'RelationEnd', NULL, 1, N'Describes one end of a relation between two object classes', NULL, NULL, N'07817322-d4b9-4dd8-8464-bcb6745fef34', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (83, N'ViewDescriptor', NULL, 4, N'', NULL, NULL, N'ffda4604-1536-43b6-b951-f8753d5092ca', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (85, N'PresentableModelDescriptor', NULL, 4, N'', NULL, NULL, N'5d152c6f-6c1e-48b7-b03e-669e30468808', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (86, N'PresenceRecord', 13, 3, N'', NULL, NULL, N'b22c93cd-75c6-4fa9-8b06-9c4b3f5c818f', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (87, N'WorkEffort', 7, 3, N'A defined work effort of an employee.', NULL, NULL, N'dff456e5-f5a6-4da7-afdd-f4ea2a9e7a7a', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (88, N'IExportable', 10, 1, N'Marks a DataType as exportable', NULL, NULL, N'4ae6616a-eca3-47a5-b502-8e3c3913c2f0', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (89, N'GuidProperty', 9, 1, N'Guid Property', NULL, NULL, N'bd34d049-2b64-47f4-8d79-932008d0cf72', 1, 1, 1)
INSERT [dbo].[DataTypes] ([ID], [ClassName], [fk_DefaultIcon], [fk_Module], [Description], [fk_Module_pos], [fk_DefaultIcon_pos], [ExportGuid], [ShowIdInLists], [ShowIconInLists], [ShowNameInLists]) VALUES (90, N'CurrentSchema', 13, 1, N'Describes the currently loaded physical database schema', NULL, NULL, N'00000000-0000-0000-0000-000000000000', 0, 0, 0)
SET IDENTITY_INSERT [dbo].[DataTypes] OFF
/****** Object:  Table [dbo].[Interfaces]    Script Date: 05/22/2009 17:30:29 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[Interfaces] ([ID]) VALUES (48)
INSERT [dbo].[Interfaces] ([ID]) VALUES (52)
INSERT [dbo].[Interfaces] ([ID]) VALUES (88)
/****** Object:  Table [dbo].[Enumerations]    Script Date: 05/22/2009 17:30:29 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[Enumerations] ([ID]) VALUES (50)
INSERT [dbo].[Enumerations] ([ID]) VALUES (53)
INSERT [dbo].[Enumerations] ([ID]) VALUES (55)
INSERT [dbo].[Enumerations] ([ID]) VALUES (78)
INSERT [dbo].[Enumerations] ([ID]) VALUES (81)
/****** Object:  Table [dbo].[Methods]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Methods]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Methods](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_ObjectClass] [int] NOT NULL,
	[MethodName] [nvarchar](100) NOT NULL,
	[fk_Module] [int] NOT NULL,
	[IsDisplayable] [bit] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[fk_ObjectClass_pos] [int] NULL,
	[fk_Module_pos] [int] NULL,
	[ExportGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Methods] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Methods] ON
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (1, 7, N'GetPropertyTypeString', 1, 1, N'Returns the String representation of this Property Meta Object.', NULL, NULL, N'a9c1a6ff-ddb2-41a5-9ad0-460b8adea977')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (3, 19, N'RechnungErstellen', 2, 1, N'Testmethode zum Erstellen von Rechnungen mit Word', NULL, NULL, N'07b6746b-586f-43fb-ae7e-317cd1ace4bf')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (4, 2, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'462ba70e-0ad9-4648-bc52-affe08d96323')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (5, 2, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'3b60b40f-0ae1-4b82-8eda-7b86648bd46d')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (6, 2, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'36da6be6-490a-4284-a58f-d6ac54e0871f')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (7, 3, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'12f061e3-155a-4c1d-8a0f-6281d93b2dd2')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (8, 3, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'e3c26739-6d36-48af-b7c8-0dea1e95d481')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (9, 3, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'a6793e04-3be9-40a8-8326-6ab35d41e286')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (10, 4, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'2880b747-86ae-4190-89d9-17eb73679bd4')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (11, 4, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'58e0713d-ff39-4b2f-9c67-7a431a23cecb')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (12, 4, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'c237dd74-2b0a-4401-bd3c-0bb0c9a56d4f')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (13, 7, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'224eb140-9775-4581-a1b9-0f641c387888')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (14, 7, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'58c48d95-c9fa-4680-88a3-841160e9e829')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (15, 7, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'44412b16-8116-49dc-ac5b-94fdfb8f37dd')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (16, 6, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'0ad9d18b-9d12-41a7-8622-19f2bc8c08bd')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (17, 6, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'c0fa86cd-6ed0-4477-8ece-a505221b51f1')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (18, 6, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'f96aa058-87eb-4488-b678-cd48dddfa97d')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (19, 10, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'8b6e6d6d-2b58-4675-9fbd-fb9cd0c2cab9')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (20, 10, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'fdac7e4e-c83f-48b4-99bc-203094af6822')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (21, 10, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'3fbd3ccf-5ed8-4200-8ef9-fc8e1a02444a')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (22, 18, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'f1d2c2ef-417f-4c0c-9405-ab2741472d89')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (23, 18, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'd347e78c-8447-49d1-9fd0-b1e8a247a172')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (24, 18, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'83fd5e3a-f432-407b-837f-5e118ea076c6')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (25, 19, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'5da7d75f-db67-4212-8c4b-e4e2f11b7ec4')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (26, 19, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'f6b89a1d-3690-449f-a263-dd819bca71b0')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (27, 19, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'750b2239-a9c0-4545-bca5-a3f63404fb78')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (28, 20, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'1ae41a97-b201-47b6-ab5e-367434754856')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (29, 20, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'08f8382a-2604-43f7-b5b6-2e4ded0b64b9')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (30, 20, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'b8d8840d-001f-4a08-b881-fe286073a9ce')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (34, 26, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'5dbabf3a-ccb5-4fe5-bd1c-cf414f16b7d2')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (35, 26, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'09844b3f-dd1d-44a3-875b-7c2a0dc00061')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (36, 26, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'b8ad511e-a8ac-4fa6-b345-95add0c531d0')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (37, 27, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'b075469b-54cf-4d91-8c7c-82840e0a7acf')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (38, 27, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'240df30c-9d8e-468f-a82b-192946d08029')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (39, 27, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'56fe8d0d-5f03-4b03-83a6-5e6e57f27e02')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (40, 29, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'c2b4a0b8-a8cd-4f8a-8395-fdf239f4fcf3')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (41, 29, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'dd77ef1e-2122-4da9-9c35-21de9e23890d')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (42, 29, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'99a21155-965d-4f10-9253-7502343fe803')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (43, 30, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'3fb6522d-0423-4fd4-afae-0027f8a7f4fc')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (44, 30, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'8aaf37a7-14b3-4118-b17d-24595a0bc008')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (45, 30, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'cc95f806-75eb-4c93-a50c-4c2cb1a27ddc')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (74, 33, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'a90eb5bd-8ce6-4234-bdd7-2d012440cb80')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (75, 33, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'6f5acdb5-e439-44e4-978c-bcecf01979ac')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (76, 33, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'2f1b878a-2e75-4a28-855c-038ea402a147')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (79, 36, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'6df3da83-92f4-4f08-a073-9af9acd679f1')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (80, 36, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'a42ba745-446d-47f2-a157-e42bf8ce21c0')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (81, 36, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'0ea403f0-c6dd-4019-b64f-3e84e8c74ca5')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (82, 36, N'GetParameterTypeString', 1, 0, N'Returns the String representation of this Method-Parameter Meta Object.', NULL, NULL, N'32f8f8f2-6546-46ed-8f8a-c8c6ac11dc67')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (83, 6, N'TestMethodForParameter', 2, 0, N'', NULL, NULL, N'447088b9-ea3e-4c72-b1fe-761d430c4003')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (84, 45, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'fb9396ef-02a2-4a50-896d-4d8b88abaa20')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (85, 45, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'c7dfdb25-f52b-4e00-b0a0-0c9293412479')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (86, 45, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'a4aa6dfa-d725-4320-8959-b8677390788f')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (87, 46, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'4969e35b-2517-4b27-8b64-b35feb59fb50')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (88, 46, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'ce8176e6-952c-492c-840f-f15979b100e5')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (89, 46, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'f713f370-657c-4827-bb20-b9bf1cab2f2a')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (90, 48, N'TestMethod', 5, 1, N'', NULL, NULL, N'9e198988-498a-4936-8546-0a599b6cc613')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (91, 51, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'7e6774ad-ab16-451d-8a73-113ac6a71d4b')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (92, 51, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'588cd551-328c-4c73-85a2-64883bbbe71d')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (93, 51, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'84479312-f094-4281-8b7a-313e241b5719')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (95, 51, N'TestMethod', 5, 1, N'testmethod', NULL, NULL, N'4d28b129-0786-4add-bddd-ff026c107f5e')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (96, 52, N'ShowMessage', 4, 0, N'', NULL, NULL, N'033f91ab-9870-466c-a403-e7430d777f30')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (97, 52, N'ShowObject', 4, 0, N'', NULL, NULL, N'9225943b-38fb-45f1-a9a8-68a0b6f7e2c7')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (98, 52, N'ChooseObject', 4, 0, N'', NULL, NULL, N'4871f987-0022-4c79-a773-aa669b9d8d04')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (106, 58, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'4497e829-e3c3-47ee-9ccd-7c6c0adfe880')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (107, 58, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'2a053aba-cd8b-4884-b07f-604d2ea2b1e3')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (108, 58, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'fa736d90-0368-41c2-8184-f688f000a9a3')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (109, 59, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'a3e127c5-154b-4ae8-83c3-8641845ebdeb')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (110, 59, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'5e3fdc13-b4e8-4ff3-88b0-f4faba4b20c2')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (111, 59, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'7a12f380-a8a6-4519-abc2-3ca78aa328ea')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (112, 60, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'cc6add40-8c70-4729-bad5-b7f1b486aeb6')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (113, 60, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'8df9b35f-2859-45a4-a68b-e294d9a759e5')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (114, 60, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'22e9873f-4f85-4e7e-ae98-8e101bff9e83')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (115, 61, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'221fc474-9d41-4020-9b23-9bf6b62b78f1')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (116, 61, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'446112ed-b960-4faf-a802-28516949a1d0')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (117, 61, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'037c1ed1-37d7-4142-bb77-a4bfb8682839')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (118, 7, N'GetPropertyType', 1, 0, N'Returns the resulting Type of this Property Meta Object.', NULL, NULL, N'b024fc8a-4e17-4212-acfe-94cc79bf35fd')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (120, 33, N'GetDataTypeString', 1, 0, N'Returns the String representation of this Datatype Meta Object.', NULL, NULL, N'615d770c-ede8-4f0a-bcca-a254c99da892')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (121, 33, N'GetDataType', 1, 0, N'Returns the resulting Type of this Datatype Meta Object.', NULL, NULL, N'826c3b7c-c266-48cf-a715-5e39d675642c')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (123, 36, N'GetParameterType', 1, 0, N'Returns the resulting Type of this Method-Parameter Meta Object.', NULL, NULL, N'41b67886-d514-4ee3-949e-54a0aed86eac')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (124, 10, N'GetReturnParameter', 1, 1, N'Returns the Return Parameter Meta Object of this Method Meta Object.', NULL, NULL, N'687f24ab-be66-4ac8-8d43-b033cf05455d')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (125, 2, N'GetInheritedMethods', 1, 1, N'', NULL, NULL, N'9bf53e7a-3546-4a30-9446-ededdde3dbde')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (126, 66, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'5d46cc11-9f4e-41a9-b2c6-f72a9fbd17cd')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (127, 66, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'3b4d9ba1-b8c9-459a-820a-cd71a4b59c09')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (128, 66, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'c55bbe61-b83a-40b1-9c6f-0cea222efb19')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (129, 67, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'c5e8aa2f-33a4-4f85-a51b-e7860e1cf068')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (130, 67, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'c32efc1f-b1d4-474f-b0d1-2d4262781e46')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (131, 67, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'5a686d39-0880-49a6-a6eb-c355cdf23299')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (132, 68, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'1cb6f35d-68e8-4536-8b6d-1ce2cd4d1218')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (133, 68, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'9a200b36-c9be-4e8a-bf6d-b98cacab62c5')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (134, 68, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'9ffc17d1-6fd2-42f5-84a4-5b4ea92caa7a')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (135, 69, N'IsValid', 1, 0, N'', NULL, NULL, N'869cbcce-c822-4b86-89ce-bcb11a7b2bb6')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (136, 69, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'8be7722e-a164-4428-9940-546cb9006754')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (137, 69, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'f08f3262-d4a1-4002-9c2b-e1c01e334a90')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (138, 69, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'888d7974-13e3-4aad-b7ff-ebcae977eb44')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (139, 69, N'GetErrorText', 1, 0, N'', NULL, NULL, N'2056bc89-f6c6-4c27-96aa-12790af6fb24')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (141, 54, N'ToString', 1, 0, N'Autogenerated! Returns a String that represents the current Object.', NULL, NULL, N'2a422c9d-81f6-45be-af38-0919724cd672')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (142, 54, N'PreSave', 1, 0, N'Autogenerated! Method is called by the Context before a commit occurs.', NULL, NULL, N'e7ec6440-b145-47ed-84c0-be885e2e4db7')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (143, 54, N'PostSave', 1, 0, N'Autogenerated! Method is called by the Context after a commit occurs.', NULL, NULL, N'e0885afa-fcfc-475b-89c6-16d4b8de9fc1')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (144, 68, N'PrepareDefault', 4, 1, N'', NULL, NULL, N'80e69c37-0297-4d0c-be08-158ab7a919ff')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (145, 77, N'PreSave', 1, 0, N'', NULL, NULL, N'be25e5f7-2b44-45e1-9932-d7434f444763')
GO
print 'Processed 100 total records'
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (146, 77, N'ToString', 1, 0, N'', NULL, NULL, N'6583bdca-419d-4c18-b041-82ef35948d29')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (147, 77, N'PostSave', 1, 0, N'', NULL, NULL, N'93e2bb4a-7e4d-44ab-aef1-e0bce3866f3d')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (148, 79, N'ToString', 1, 0, N'', NULL, NULL, N'7c84c750-4d64-49eb-b02a-1ed9814a5836')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (149, 79, N'PreSave', 1, 0, N'', NULL, NULL, N'9eca3db8-3fa3-42bc-b074-8b6d4d26b934')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (150, 79, N'PostSave', 1, 0, N'', NULL, NULL, N'e81ddadd-8f10-45b4-b891-662a1cfd5f36')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (151, 79, N'AsType', 1, 0, N'get the referenced <see cref="System.Type"/>', NULL, NULL, N'f99da656-46fc-4846-8655-10f534a00102')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (155, 29, N'RegenerateTypeRefs', 1, 1, N'Regenerates the stored list of TypeRefs from the loaded assembly', NULL, NULL, N'818507bf-73f7-4e57-9730-ba3c42ddc418')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (157, 82, N'ToString', 1, 0, N'', NULL, NULL, N'14359316-9ee0-4971-a13a-089ef222ec1f')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (158, 82, N'PostSave', 1, 0, N'', NULL, NULL, N'c79b99e8-4b0c-4bbb-b8d4-feae09c5f2a7')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (159, 82, N'PreSave', 1, 0, N'', NULL, NULL, N'53e9616d-684b-467d-b6c8-89457034cc62')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (160, 83, N'PreSave', 1, 0, N'', NULL, NULL, N'09fc8d6c-2abb-4aab-8a96-9e85a98c8879')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (161, 83, N'PostSave', 1, 0, N'', NULL, NULL, N'1c5a3190-16d9-42ec-9502-486b88edd406')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (162, 83, N'ToString', 1, 0, N'', NULL, NULL, N'd6a377ca-66e6-473d-a7e9-b0cdead60a4b')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (163, 85, N'PreSave', 1, 0, N'', NULL, NULL, N'244fd20f-7562-43b2-8c74-76bf176f5f69')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (164, 85, N'PostSave', 1, 0, N'', NULL, NULL, N'212de77d-2fe9-4500-a42d-4712d7011513')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (165, 85, N'ToString', 1, 0, N'', NULL, NULL, N'116f7c88-1972-4a12-a732-463981a36b34')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (166, 86, N'PreSave', 1, 0, N'', NULL, NULL, N'4e9f29da-7e5d-460c-a12c-8078c0be085e')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (167, 86, N'ToString', 1, 0, N'', NULL, NULL, N'1edd2a6f-5f08-44d2-a6f9-a5064ea6ae05')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (168, 86, N'PostSave', 1, 0, N'', NULL, NULL, N'd77a3b06-be2f-4d71-96b2-c3905df83b2b')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (169, 87, N'PreSave', 1, 0, N'', NULL, NULL, N'5dc1d052-9471-415f-a2ff-d7e6bdb392a4')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (170, 87, N'ToString', 1, 0, N'', NULL, NULL, N'e4fa2c31-6590-4a44-8c62-4bd62c2b9e4f')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (171, 87, N'PostSave', 1, 0, N'', NULL, NULL, N'b8a84c7c-31fe-403c-be69-4e67c5afb5bf')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (172, 90, N'ToString', 1, 0, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (173, 90, N'PreSave', 1, 0, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000')
INSERT [dbo].[Methods] ([ID], [fk_ObjectClass], [MethodName], [fk_Module], [IsDisplayable], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [ExportGuid]) VALUES (174, 90, N'PostSave', 1, 0, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000')
SET IDENTITY_INSERT [dbo].[Methods] OFF
/****** Object:  Table [dbo].[ControlInfos]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ControlInfos]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ControlInfos](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_Assembly] [int] NOT NULL,
	[ClassName] [nvarchar](200) NOT NULL,
	[IsContainer] [bit] NOT NULL,
	[Platform] [int] NOT NULL,
	[ControlType] [int] NOT NULL,
	[fk_Assembly_pos] [int] NULL,
 CONSTRAINT [PK_ControlInfos] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[ControlInfos] ON
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (285, 4, N'Kistl.GUI.Renderer.WPF.EnumControl', 0, 0, 15, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (286, 4, N'Kistl.GUI.Renderer.WPF.StringListControl', 0, 0, 14, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (287, 4, N'Kistl.GUI.Renderer.WPF.EditSimpleProperty', 0, 0, 13, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (288, 4, N'Kistl.GUI.Renderer.WPF.IntegerListControl', 0, 0, 12, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (289, 4, N'Kistl.GUI.Renderer.WPF.EditIntProperty', 0, 0, 11, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (290, 4, N'Kistl.GUI.Renderer.WPF.DoubleListControl', 0, 0, 10, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (291, 4, N'Kistl.GUI.Renderer.WPF.EditDoubleProperty', 0, 0, 9, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (292, 4, N'Kistl.GUI.Renderer.WPF.DateTimeListControl', 0, 0, 8, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (293, 4, N'Kistl.GUI.Renderer.WPF.EditDateTimeProperty', 0, 0, 7, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (294, 4, N'Kistl.GUI.Renderer.WPF.BooleanListControl', 0, 0, 6, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (295, 4, N'Kistl.GUI.Renderer.WPF.EditBoolProperty', 0, 0, 5, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (296, 4, N'Kistl.GUI.Renderer.WPF.EnumEntryListControl', 0, 0, 16, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (297, 4, N'Kistl.GUI.Renderer.WPF.ObjectListControl', 0, 0, 3, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (298, 4, N'Kistl.GUI.Renderer.WPF.ObjectReferenceControl', 0, 0, 4, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (299, 4, N'Kistl.GUI.Renderer.WPF.GroupBoxWrapper', 1, 0, 2, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (300, 4, N'Kistl.GUI.Renderer.WPF.ObjectTabItem', 1, 0, 1, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (301, 4, N'Kistl.GUI.Renderer.WPF.Renderer', 1, 0, 0, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (302, 3, N'Kistl.Client.ASPNET.Toolkit.StringPropertyControlLoader', 0, 1, 13, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (303, 3, N'Kistl.Client.ASPNET.Toolkit.ObjectReferencePropertyControlLoader', 0, 1, 4, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (304, 3, N'Kistl.Client.ASPNET.Toolkit.ObjectListControlLoader', 0, 1, 3, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (305, 3, N'Kistl.Client.ASPNET.Toolkit.IntPropertyControlLoader', 0, 1, 11, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (306, 3, N'Kistl.Client.ASPNET.Toolkit.DoublePropertyControlLoader', 0, 1, 9, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (307, 3, N'Kistl.Client.ASPNET.Toolkit.DateTimePropertyControlLoader', 0, 1, 7, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (308, 3, N'Kistl.Client.ASPNET.Toolkit.BoolPropertyControlLoader', 0, 1, 5, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (309, 3, N'Kistl.Client.ASPNET.Toolkit.ObjectPanelLoader', 1, 1, 1, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (310, 3, N'Kistl.Client.ASPNET.Toolkit.Renderer', 1, 1, 0, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (311, 4, N'Kistl.GUI.Renderer.WPF.ActionControl', 0, 0, 17, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (312, 3, N'Kistl.Client.ASPNET.Toolkit.Controls.PropertyGroup', 1, 1, 2, NULL)
INSERT [dbo].[ControlInfos] ([ID], [fk_Assembly], [ClassName], [IsContainer], [Platform], [ControlType], [fk_Assembly_pos]) VALUES (313, 4, N'Kistl.GUI.Renderer.WPF.TemplateEditor', 0, 0, 19, NULL)
SET IDENTITY_INSERT [dbo].[ControlInfos] OFF
/****** Object:  Table [dbo].[TypeRefs]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TypeRefs]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TypeRefs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](600) NOT NULL,
	[fk_Assembly] [int] NOT NULL,
	[fk_Assembly_pos] [int] NULL,
	[fk_Parent] [int] NULL,
	[ExportGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_TypeRefs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[TypeRefs] ON
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (1, N'Kistl.App.Base.CustomClientActions_KistlBase', 1, NULL, 295, N'e954023a-9460-4a90-8ca3-12c109bb2e41')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (2, N'Kistl.App.TimeRecords.CustomServerActions_TimeRecords', 2, NULL, 295, N'31939aa7-e9d4-43da-aff4-279c944e722f')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (3, N'Kistl.App.Projekte.CustomServerActions_Projekte', 2, NULL, 295, N'6b4687eb-fa7f-41fb-b772-541c7e18f173')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (4, N'Kistl.App.Base.CustomServerActions_KistlBase', 2, NULL, 295, N'ae4a2fc0-0eb9-4d85-b962-e81ad9afed86')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (5, N'Kistl.App.GUI.CustomClientActions_GUI', 1, NULL, 295, N'2f979c67-61ba-4173-838b-378e2224c337')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (6, N'Kistl.App.TimeRecords.CustomClientActions_TimeRecords', 1, NULL, 295, N'a77fa1d1-9341-4300-a120-8ea17365642b')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (8, N'Kistl.App.Projekte.CustomClientActions_Projekte', 1, NULL, 295, N'6a0bd457-600e-4062-bd5d-cf786d32afbb')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (9, N'Kistl.Client.Presentables.ModuleModel', 14, NULL, 83, N'98b741e5-2dd5-4221-b001-01de816ded70')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (10, N'Kistl.Client.Presentables.ObjectClassModel', 14, NULL, 11, N'da04f1a5-fb30-4786-b329-8f070083a4f6')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (11, N'Kistl.Client.Presentables.DataTypeModel', 14, NULL, 83, N'41b6d0a1-a2ae-4c68-8997-503db83319bc')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (12, N'Kistl.Client.Presentables.SaveContextCommand', 14, NULL, 45, N'c9f71b76-a29a-46ce-9288-20263c83c7dd')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (13, N'Kistl.Client.Presentables.WorkspaceModel', 14, NULL, 96, N'd9fe8f77-b156-4929-add8-38cc2fa546e8')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (14, N'Kistl.Client.ClientExtensions', 14, NULL, 295, N'6fd1945a-790c-40d5-9a9c-f0e0eaa9b695')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (15, N'Kistl.Client.ClientHelper', 14, NULL, 295, N'117c6d76-559a-469a-8be3-efc2fcd753df')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (16, N'Kistl.Client.Presentables.ActionModel', 14, NULL, 96, N'cd9797e0-ad1b-4dd7-b66b-1f2528f7a50c')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (17, N'Kistl.Client.Presentables.ObjectListModel', 14, NULL, 355, N'85be3823-e778-4829-b214-69154c1e6164')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (18, N'Kistl.Client.Presentables.DataObjectSelectionTaskModel', 14, NULL, 357, N'312383a3-5c45-43cf-a032-5271792a4eec')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (23, N'Kistl.Client.ServerDomainManager', 14, NULL, 295, N'413cabb4-e306-4e6b-bd92-9216f092380e')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (24, N'Kistl.Client.Presentables.SynchronousThreadManager', 14, NULL, 295, N'91fdfdb4-7e1d-4949-952e-6da8c2b0ad48')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (25, N'Kistl.Client.Presentables.WPF.AsyncThreadManager', 14, NULL, 295, N'1067284c-7e5c-426d-b65d-7f46249ef266')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (26, N'Kistl.Client.Presentables.WPF.UiThreadManager', 14, NULL, 295, N'ba90f359-b491-44f7-8949-d1b919b0ae05')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (27, N'Kistl.Client.Presentables.IThreadManager', 14, NULL, NULL, N'64a9e92f-3461-43ac-b31d-780fd9c63977')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (31, N'Kistl.Client.TheseMethodsShouldBeImplementedOnKistlObjects', 14, NULL, 295, N'20efc010-a3ee-4bf8-83ae-0e2396a7fe17')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (34, N'Kistl.Client.Presentables.ObjectReferenceModel', 14, NULL, 374, N'59136763-5a94-4ae5-b9ab-6d679e86245a')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (39, N'Kistl.Client.GUI.IView', 14, NULL, NULL, N'4f23f8fc-83cc-4376-bef1-15550d8f5bed')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (40, N'Kistl.Client.Presentables.ModelState', 14, NULL, 300, N'0de66862-2d4e-443f-86b2-992f5315ea29')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (41, N'Kistl.Client.Presentables.MethodInvocationModel', 14, NULL, 83, N'68c7ae8c-6fc6-4c67-877b-7a84efd47b55')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (45, N'Kistl.Client.Presentables.CommandModel', 14, NULL, 96, N'34a220b5-e8b0-481e-beed-8948acf5173e')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (46, N'Kistl.Client.Presentables.ICommand', 14, NULL, NULL, N'b4994743-c3bd-4f6a-baac-8c3ce87cb4e7')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (47, N'Kistl.Client.GuiApplicationContext', 14, NULL, 302, N'5c0fb6c6-e174-42f3-9d9d-ee23b4c06bc2')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (48, N'Kistl.Client.IGuiApplicationContext', 14, NULL, NULL, N'b7a33d42-da6d-430d-8a37-962f4d6e0833')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (81, N'Kistl.Client.Presentables.ModelFactory', 14, NULL, 295, N'a2ef02e2-851b-4b34-bb7f-2a906dda177d')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (83, N'Kistl.Client.Presentables.DataObjectModel', 14, NULL, 96, N'a07f30a6-b920-4fef-98ac-53155791e804')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (92, N'Kistl.Client.Presentables.IClearableValue', 14, NULL, NULL, N'1ddd8333-40b8-4b7e-a98a-83be2c4ad51e')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (94, N'Kistl.Client.Presentables.KistlContextModel', 14, NULL, 96, N'c8e36412-31de-4ea0-90a3-4d4392f1c3c8')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (95, N'Kistl.Client.Presentables.KistlDebuggerAsModel', 14, NULL, 96, N'266f79a3-9b2f-4779-a9ae-9702ccf53a5a')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (96, N'Kistl.Client.Presentables.PresentableModel', 14, NULL, 295, N'6012afb5-9ef6-4552-9481-b46a39f8f54d')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (108, N'Kistl.App.Base.Multiplicity', 13, NULL, 300, N'558fcf1b-9f5f-4510-92c1-0c7364ed6b12')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (110, N'Kistl.App.Base.StorageType', 13, NULL, 300, N'03387ef8-1025-47ca-aeec-b53264efd693')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (112, N'Kistl.App.GUI.VisualType', 13, NULL, 300, N'77f2d68d-91a8-4639-8714-d9aca80d8562')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (114, N'Kistl.App.GUI.Toolkit', 13, NULL, 300, N'17cbd863-666b-4c69-b40f-92ef5f5fef9d')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (116, N'Kistl.App.Test.TestEnum', 13, NULL, 300, N'ea1df580-d4e4-421e-a449-f3d3fd5356c2')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (145, N'Kistl.Client.WPF.View.KistlDebuggerView', 18, NULL, 317, N'e68bbcf2-0d0d-446a-bc1e-4791e904c3fb')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (146, N'Kistl.Client.WPF.View.ListValueView', 18, NULL, 319, N'fc18b7ac-7673-4c6a-b3c0-7f016ecca3b2')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (147, N'Kistl.Client.WPF.View.EnumSelectionView', 18, NULL, 319, N'd1ea3d41-384e-4f72-940c-0471f2421082')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (148, N'Kistl.Client.WPF.View.TextValueSelectionView', 18, NULL, 319, N'937a06f5-8bdb-47e7-b30d-ef5e944e2eae')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (149, N'Kistl.Client.WPF.View.ActionView', 18, NULL, 318, N'a7c67d95-c198-47da-ad6d-af90e08137f7')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (150, N'Kistl.Client.WPF.View.SelectionDialog', 18, NULL, 317, N'12084abf-1e98-4d04-b66d-fed4ea47d3c2')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (151, N'Kistl.Client.WPF.View.NullableBoolValueView', 18, NULL, 319, N'58dc43e8-3fc4-46ce-b233-51459ebe4d2b')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (152, N'Kistl.Client.ASPNET.Toolkit.View.NullablePropertyTextBoxViewLoader', 16, NULL, 320, N'5c6cddb5-ed40-4331-b8de-122748175b18')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (153, N'Kistl.Client.Forms.View.NullablePropertyTextBoxView', 17, NULL, 328, N'c5b0021a-b59c-4e73-b756-60020da6ae3e')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (154, N'Kistl.Client.WPF.View.NullablePropertyTextBoxView', 18, NULL, 319, N'852d2841-6918-4faf-a8ef-5d93a1e5d548')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (155, N'Kistl.Client.WPF.View.DataObjectView', 18, NULL, 318, N'92fb7c93-822b-41f7-b25b-dd44e0fb3516')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (156, N'Kistl.Client.ASPNET.Toolkit.View.DataObjectListViewLoader', 16, NULL, 320, N'49ed1dda-5eb8-435e-a84f-3389db6b7d6e')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (157, N'Kistl.Client.Forms.View.DataObjectListView', 17, NULL, 330, N'9679c5f0-d0d6-47a1-b147-cbaa4a19d2d3')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (158, N'Kistl.Client.WPF.View.DataObjectListView', 18, NULL, 319, N'4652082a-202d-4d20-a0d4-81a4f69ffb85')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (159, N'Kistl.Client.ASPNET.Toolkit.View.DataObjectReferenceViewLoader', 16, NULL, 320, N'e2570546-0fd2-4bab-b836-0fdb24491c33')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (160, N'Kistl.Client.Forms.View.DataObjectReferenceView', 17, NULL, 332, N'389bdb3b-0e04-41bc-ab80-4b0e63fc180c')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (161, N'Kistl.Client.WPF.View.ObjectReferenceView', 18, NULL, 319, N'0695b951-f3fe-4bf2-a9b7-f893c5bb7c1c')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (162, N'Kistl.Client.ASPNET.Toolkit.View.DataObjectFullViewLoader', 16, NULL, 320, N'5bc00f1b-d31b-422b-a8aa-dbd72fa6718b')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (163, N'Kistl.Client.Forms.View.DataObjectFullView', 17, NULL, 334, N'f1f3ab2a-0b82-4d34-8f9e-bf2a809f4e04')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (164, N'Kistl.Client.WPF.View.DataObjectFullView', 18, NULL, 318, N'ba95fc85-c50f-494a-b4f0-5596ef38b7bf')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (165, N'Kistl.Client.ASPNET.Toolkit.View.WorkspaceViewLoader', 16, NULL, 320, N'7fbe5664-1b3c-4176-a360-85b98009886c')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (166, N'Kistl.Client.Forms.View.WorkspaceView', 17, NULL, 335, N'cc9508ab-9700-47d9-a969-9d78db989c49')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (167, N'Kistl.Client.WPF.View.WorkspaceView', 18, NULL, 317, N'28a89148-24d1-4cc9-accc-24cc7eb2dfde')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (273, N'Kistl.Client.Presentables.EnumerationPropertyModel`1', 14, NULL, 424, N'b664cb98-8a81-4a12-ab12-46ae2a7bb499')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (274, N'Kistl.Client.Presentables.EnumerationPropertyModel`1', 14, NULL, 429, N'567b58ce-dced-4a54-bc05-1ed0dfe8943a')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (275, N'Kistl.Client.Presentables.EnumerationPropertyModel`1', 14, NULL, 434, N'96ee55ea-57f8-40e4-a4c6-c60e39b9f35b')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (276, N'Kistl.Client.Presentables.EnumerationPropertyModel`1', 14, NULL, 439, N'c597cf42-2788-4d9c-990b-ffffd2fe8a11')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (277, N'Kistl.Client.Presentables.EnumerationPropertyModel`1', 14, NULL, 444, N'ec884659-371f-41bc-aeba-b9cd61d010df')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (278, N'Kistl.Client.WPF.View.PropertyGroupBoxView', 18, NULL, 347, N'9c2c3f17-3445-46e0-8b76-fc11f15dfbf5')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (282, N'Kistl.Client.Forms.View.DataObjectReferenceView', 17, NULL, 332, N'7852bc9a-ee8f-4e91-97e8-f863f52326b0')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (283, N'Kistl.Client.Presentables.PropertyGroupModel', 14, NULL, 96, N'8314a3e7-5e20-47bd-ba61-c3e88eae2ffc')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (295, N'System.Object', 29, NULL, NULL, N'c51c8086-60b2-410e-96c4-c1a32c1f57ee')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (299, N'System.ValueType', 29, NULL, 295, N'4a5b38e8-bca8-4061-8a70-69bbd593ddd8')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (300, N'System.Enum', 29, NULL, 299, N'80e14e91-1a63-4908-952f-9a3c3773c765')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (301, N'Kistl.API.ApplicationContext', 15, NULL, 295, N'25c0d6de-3768-4ebb-8210-4c789c869c1a')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (302, N'Kistl.API.Client.ClientApiContext', 30, NULL, 301, N'b732c334-a720-4796-ae97-9a2ccf636791')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (310, N'System.Windows.Threading.DispatcherObject', 31, NULL, 295, N'df23dac1-96d9-41dc-9aeb-bbfbad1ad7cb')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (311, N'System.Windows.DependencyObject', 31, NULL, 310, N'5a505de5-8b19-48f1-bdb8-6865ce665876')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (312, N'System.Windows.Media.Visual', 32, NULL, 311, N'69552e25-33e1-44b2-9b8c-da4a2f0a896b')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (313, N'System.Windows.UIElement', 32, NULL, 312, N'cf64b9d6-50c4-4f49-a1ad-1dd0ad8c9b13')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (314, N'System.Windows.FrameworkElement', 33, NULL, 313, N'c12ccef7-55ee-454f-befb-e917d00ab79d')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (315, N'System.Windows.Controls.Control', 33, NULL, 314, N'669c40cb-5030-4ed1-a713-17470c92a596')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (316, N'System.Windows.Controls.ContentControl', 33, NULL, 315, N'950b505b-54b5-44ae-a155-c06c96727d0a')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (317, N'System.Windows.Window', 33, NULL, 316, N'ee07bd36-41ee-43df-8099-f4dd0c355902')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (318, N'System.Windows.Controls.UserControl', 33, NULL, 316, N'1fda132a-a95c-4483-9f9d-1e952fa20245')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (319, N'Kistl.Client.WPF.View.PropertyView', 34, NULL, 318, N'b63e703b-9bbc-40cd-9dfd-e29bfb925826')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (320, N'Kistl.Client.ASPNET.Toolkit.View.ViewLoader', 35, NULL, 295, N'1c81e7f3-f89e-4ea9-902a-5e33851d71b0')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (321, N'System.MarshalByRefObject', 29, NULL, 295, N'304f8741-7dfd-4949-afda-0844842c2356')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (322, N'System.ComponentModel.Component', 36, NULL, 321, N'dedf1d31-e4e3-40ad-8ed5-a3df78137374')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (323, N'System.Windows.Forms.Control', 37, NULL, 322, N'249e5d3c-0dfb-4fe8-a67c-bf46b26c6923')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (324, N'System.Windows.Forms.ScrollableControl', 37, NULL, 323, N'f8d240ea-e7ff-43d1-bfc8-3d0840d85733')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (325, N'System.Windows.Forms.ContainerControl', 37, NULL, 324, N'a1254a60-8dfd-4928-8984-5ccf051da8e8')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (326, N'System.Windows.Forms.UserControl', 37, NULL, 325, N'774e54d3-6017-4992-b380-b11538217cba')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (328, N'Kistl.Client.Forms.View.NullablePropertyTextBoxViewDesignerProxy', 38, NULL, 416, N'cf875c98-2ac3-421e-94ec-35c04f76172c')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (330, N'Kistl.Client.Forms.View.DataObjectListViewDesignerProxy', 38, NULL, 418, N'd207fda2-1a03-4013-81a4-5d9d1d2c9821')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (332, N'Kistl.Client.Forms.View.DataObjectReferenceViewDesignerProxy', 38, NULL, 419, N'db1de821-5149-40eb-a79f-81bfb5b0ae4d')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (334, N'Kistl.Client.Forms.View.DataObjectFullViewDesignerProxy', 38, NULL, 420, N'd9712828-6231-46fa-8cd2-07d8f33577b9')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (335, N'System.Windows.Forms.Form', 37, NULL, 325, N'd5e69fd4-01d4-43e6-9fad-d9ec09475262')
GO
print 'Processed 100 total records'
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (346, N'System.Windows.Controls.HeaderedContentControl', 33, NULL, 316, N'8d9e2188-e9ff-4e2c-8d9b-b471044a21d2')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (347, N'System.Windows.Controls.GroupBox', 33, NULL, 346, N'f7493cb1-e0cc-4be0-8626-ef9c58290af8')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (354, N'System.Collections.Generic.ICollection`1', 29, NULL, NULL, N'c71f7d2b-bd78-4eff-8122-214e29740625')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (355, N'Kistl.Client.Presentables.PropertyModel`1', 14, NULL, 96, N'c1af8bc5-ad75-4725-88d6-9e34294c7d6b')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (357, N'Kistl.Client.Presentables.SelectionTaskModel`1', 14, NULL, 96, N'1b3d6d7f-3f7e-4e78-baa6-3d9f2bf8c887')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (374, N'Kistl.Client.Presentables.PropertyModel`1', 14, NULL, 96, N'1d98088b-42c9-4a90-886f-814d5707c453')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (382, N'Kistl.Client.Presentables.PropertyModel`1', 14, NULL, 96, N'7045df02-d480-4755-bf6d-2cae438dce1c')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (383, N'System.String', 29, NULL, 295, N'ec63294b-6e2c-49c7-b925-7e263e8c1fcf')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (384, N'Kistl.Client.Presentables.ReferencePropertyModel`1', 14, NULL, 382, N'006ae4fa-804d-47a7-97d5-33d99a0ebb51')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (394, N'Kistl.Client.Presentables.ChooseReferencePropertyModel`1', 14, NULL, 384, N'a1c64886-b50d-4e06-b806-dc359e605626')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (395, N'System.Nullable`1', 29, NULL, 299, N'e0f5d065-6411-479b-a56d-0eea1cd3a0c1')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (396, N'Kistl.Client.Presentables.PropertyModel`1', 14, NULL, 96, N'1758692e-7073-4841-add2-37b9421b0fb5')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (398, N'System.Int32', 29, NULL, 299, N'858e1452-5e20-4c92-bc8d-7b2b925c5052')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (399, N'Kistl.Client.Presentables.NullableValuePropertyModel`1', 14, NULL, 396, N'2f35b21a-73c2-4e38-ab28-7ee2404684be')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (400, N'System.Nullable`1', 29, NULL, 299, N'65528584-a977-4bdb-96d6-b128f5cca2ae')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (401, N'Kistl.Client.Presentables.PropertyModel`1', 14, NULL, 96, N'e8598d40-fe23-4a0a-b098-58b7bd483df8')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (403, N'System.Double', 29, NULL, 299, N'cdac9fc9-7317-46cc-bac4-d8d49cc81f80')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (404, N'Kistl.Client.Presentables.NullableValuePropertyModel`1', 14, NULL, 401, N'f223c178-c447-40a8-960e-ffba6b8423af')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (405, N'System.Nullable`1', 29, NULL, 299, N'0d25af45-d481-42b6-934b-061cced2df91')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (406, N'Kistl.Client.Presentables.PropertyModel`1', 14, NULL, 96, N'4fab9adf-16fd-410c-bbb0-ec317e59cfa0')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (408, N'System.DateTime', 29, NULL, 299, N'e0559b37-e9bc-4867-acff-2f86262cedc8')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (409, N'Kistl.Client.Presentables.NullableValuePropertyModel`1', 14, NULL, 406, N'59ab4b8c-ab9f-461b-9b9c-7941a7b4a9fa')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (410, N'System.Nullable`1', 29, NULL, 299, N'adfb5379-8cda-46a0-a5fa-bc3aee1866c7')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (411, N'Kistl.Client.Presentables.PropertyModel`1', 14, NULL, 96, N'71d65b2d-b3e0-4728-9b39-56cfcdbf44ea')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (413, N'System.Boolean', 29, NULL, 299, N'bd45e851-e071-463b-b7a9-c159f4a1bf15')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (414, N'Kistl.Client.Presentables.NullableValuePropertyModel`1', 14, NULL, 411, N'69d2fcb4-d385-497d-8159-20c03157d5c0')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (415, N'Kistl.Client.Presentables.IValueModel`1', 14, NULL, NULL, N'dd8e872b-d813-4a8e-baa8-7c275f510aaf')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (416, N'Kistl.Client.Forms.View.FormsUserControl`1', 17, NULL, 326, N'78a703f0-c9d1-4ef1-a8f6-6ae7c4afb807')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (418, N'Kistl.Client.Forms.View.FormsUserControl`1', 17, NULL, 326, N'e7f85846-d7c9-42fc-b5b5-2a8173b969b0')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (419, N'Kistl.Client.Forms.View.FormsUserControl`1', 17, NULL, 326, N'7124e68a-7206-4d98-8157-699b75470209')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (420, N'Kistl.Client.Forms.View.FormsUserControl`1', 17, NULL, 326, N'608b9387-4102-466d-bd8c-31082473589b')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (421, N'System.Nullable`1', 29, NULL, 299, N'54046e1b-63e7-4e76-ab27-797cdfce9c2d')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (422, N'Kistl.Client.Presentables.PropertyModel`1', 14, NULL, 96, N'57f6cd40-1ae9-447e-b025-668ea1d698e3')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (424, N'Kistl.Client.Presentables.NullableValuePropertyModel`1', 14, NULL, 422, N'1db9aa4d-c2be-452b-b548-ef2df41e40b5')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (426, N'System.Nullable`1', 29, NULL, 299, N'8aa8247f-4205-4786-9ea0-4cce2b03808e')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (427, N'Kistl.Client.Presentables.PropertyModel`1', 14, NULL, 96, N'50531036-5dcd-414c-a5fa-7d22a63f44e6')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (429, N'Kistl.Client.Presentables.NullableValuePropertyModel`1', 14, NULL, 427, N'1387a0dc-37df-4520-ad54-4526e6d4148e')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (431, N'System.Nullable`1', 29, NULL, 299, N'bffa699f-a48f-4e29-90de-a0ca19700087')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (432, N'Kistl.Client.Presentables.PropertyModel`1', 14, NULL, 96, N'0e06a99c-92a1-4de7-9b31-5a2052cce696')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (434, N'Kistl.Client.Presentables.NullableValuePropertyModel`1', 14, NULL, 432, N'e39e99f9-8da7-4df8-92dc-5eeeb4a25791')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (436, N'System.Nullable`1', 29, NULL, 299, N'7589d72b-28db-4273-84b4-bb703c20200a')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (437, N'Kistl.Client.Presentables.PropertyModel`1', 14, NULL, 96, N'788101a3-efb6-4b4c-a371-64ec17288945')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (439, N'Kistl.Client.Presentables.NullableValuePropertyModel`1', 14, NULL, 437, N'74121a59-0018-439b-90ad-eb4c967278f4')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (441, N'System.Nullable`1', 29, NULL, 299, N'337227d1-7707-4496-a4f9-f5bdf3d88b45')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (442, N'Kistl.Client.Presentables.PropertyModel`1', 14, NULL, 96, N'a5bb676b-e1a4-4b89-9662-d92eca9bd361')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (444, N'Kistl.Client.Presentables.NullableValuePropertyModel`1', 14, NULL, 442, N'00b70166-361a-4c24-b9d9-eaa4cad1c4a5')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (447, N'Kistl.API.IDataObject', 15, NULL, NULL, N'527370ff-ca5e-4c4f-a665-bd5483f1936f')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (448, N'Kistl.Client.Presentables.ObjectResultModel`1', 14, NULL, 560, N'03338e3b-2317-4e88-9858-a6a165dd57f9')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (449, N'Kistl.Client.Presentables.MethodResultModel`1', 14, NULL, 96, N'46af15ac-f0b1-415f-a47e-04f8d5c35b52')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (450, N'Kistl.Client.Presentables.ObjectResultModel`1', 14, NULL, 449, N'eda5fdba-d8e3-4a43-b69b-84d3617f105b')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (454, N'Kistl.Client.Presentables.NullableResultModel`1', 14, NULL, 556, N'b030fc3a-8022-4e46-97a3-7d9c3562c32e')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (458, N'Kistl.Client.Presentables.NullableResultModel`1', 14, NULL, 557, N'e4266301-3554-46cb-bd25-f13f545dcffa')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (462, N'Kistl.Client.Presentables.NullableResultModel`1', 14, NULL, 558, N'e57001ad-1e50-4d01-b5f7-5f7e0c27d2c8')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (466, N'Kistl.Client.Presentables.NullableResultModel`1', 14, NULL, 559, N'8bdb8b68-0dc3-46ff-a7a0-00bcc96feb4a')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (469, N'System.Collections.Generic.ICollection`1', 29, NULL, NULL, N'ead4700a-df68-43fb-a197-dd94d3f5d629')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (470, N'Kistl.Client.Presentables.PropertyModel`1', 14, NULL, 96, N'04a0d13e-0c1a-4024-8c45-c2bfc70e0ec5')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (556, N'Kistl.Client.Presentables.MethodResultModel`1', 14, NULL, 96, N'3abd5c28-0189-4710-9c60-ea460cf42259')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (557, N'Kistl.Client.Presentables.MethodResultModel`1', 14, NULL, 96, N'293e8e3d-7693-4532-9e30-10e6b4260dfa')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (558, N'Kistl.Client.Presentables.MethodResultModel`1', 14, NULL, 96, N'5ce764d1-e62f-4c41-9bc8-8289f4b54714')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (559, N'Kistl.Client.Presentables.MethodResultModel`1', 14, NULL, 96, N'4c9a647b-c168-42e9-87fa-bccd782e5885')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (560, N'Kistl.Client.Presentables.MethodResultModel`1', 14, NULL, 96, N'308dc904-c576-420a-bf19-85d1c1853d6c')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (561, N'System.Collections.Generic.ICollection`1', 29, NULL, NULL, N'849e5071-77f6-41de-a72f-12903ac9c497')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (562, N'System.Collections.Generic.ICollection`1', 29, NULL, NULL, N'69544b3c-96fb-48b3-a4ff-251d4e502859')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (563, N'Kistl.Client.Presentables.TimeRecords.WorkEffortModel', 14, NULL, 83, N'4b424049-d2b5-46b2-a88e-7fd330395838')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (564, N'Kistl.Client.Presentables.TimeRecords.WorkEffortRecorderModel', 14, NULL, 13, N'28114689-4fc3-44b6-b7f1-141dab6b78d9')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (565, N'Kistl.Client.WPF.View.TimeRecords.WorkEffortRecorderView', 4, NULL, 317, N'24dc200e-ee3f-4df3-ab34-f8196c338fd5')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (566, N'Kistl.Client.WPF.View.TimeRecords.WorkEffortView', 4, NULL, 318, N'c431cb71-0a53-42cc-b8a7-3df5924fea84')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (567, N'Kistl.Client.Presentables.PropertyModel`1', 14, NULL, 96, N'ab1aa1db-fe00-416a-b061-7ea6cd43b697')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (568, N'System.Nullable`1', 29, NULL, 299, N'3527d206-5197-4cbd-9792-85a12e2aea6e')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (569, N'System.Guid', 29, NULL, 299, N'd9403577-df05-45ac-b468-e74a9f762c8a')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (570, N'Kistl.Client.Presentables.NullableValuePropertyModel`1', 14, NULL, 567, N'7ea78a35-65a1-41f0-a0f0-da2dee7dd5bf')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (571, N'Kistl.Client.WPF.View.GridCells.StringValue', 4, NULL, 318, N'00000000-0000-0000-0000-000000000000')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (572, N'Kistl.Client.WPF.View.GridCells.BoolEditor', 4, NULL, 318, N'00000000-0000-0000-0000-000000000000')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (573, N'Kistl.Client.WPF.View.GridCells.StringEditor', 4, NULL, 318, N'00000000-0000-0000-0000-000000000000')
INSERT [dbo].[TypeRefs] ([ID], [FullName], [fk_Assembly], [fk_Assembly_pos], [fk_Parent], [ExportGuid]) VALUES (574, N'Kistl.Client.WPF.View.GridCells.ReferenceEditor', 4, NULL, 318, N'00000000-0000-0000-0000-000000000000')
SET IDENTITY_INSERT [dbo].[TypeRefs] OFF
/****** Object:  Table [dbo].[Structs]    Script Date: 05/22/2009 17:30:29 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[Structs] ([ID]) VALUES (63)
/****** Object:  Table [dbo].[PresenterInfos]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PresenterInfos]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PresenterInfos](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_PresenterAssembly] [int] NOT NULL,
	[PresenterTypeName] [nvarchar](200) NOT NULL,
	[fk_DataAssembly] [int] NULL,
	[DataTypeName] [nvarchar](200) NOT NULL,
	[ControlType] [int] NOT NULL,
	[fk_PresenterAssembly_pos] [int] NULL,
	[fk_DataAssembly_pos] [int] NULL,
 CONSTRAINT [PK_PresenterInfos] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[PresenterInfos] ON
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (3, 14, N'Kistl.GUI.DefaultMethodPresenter`1', 13, N'Kistl.App.Base.Method', 14, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (4, 14, N'Kistl.GUI.DefaultMethodPresenter`1', 13, N'Kistl.App.Base.Method', 13, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (5, 14, N'Kistl.GUI.DefaultMethodPresenter`1', 13, N'Kistl.App.Base.Method', 12, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (6, 14, N'Kistl.GUI.DefaultMethodPresenter`1', 13, N'Kistl.App.Base.Method', 11, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (7, 14, N'Kistl.GUI.DefaultMethodPresenter`1', 13, N'Kistl.App.Base.Method', 10, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (8, 14, N'Kistl.GUI.DefaultMethodPresenter`1', 13, N'Kistl.App.Base.Method', 9, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (9, 14, N'Kistl.GUI.DefaultMethodPresenter`1', 13, N'Kistl.App.Base.Method', 8, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (10, 14, N'Kistl.GUI.DefaultMethodPresenter`1', 13, N'Kistl.App.Base.Method', 7, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (11, 14, N'Kistl.GUI.DefaultMethodPresenter`1', 13, N'Kistl.App.Base.Method', 6, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (12, 14, N'Kistl.GUI.DefaultMethodPresenter`1', 13, N'Kistl.App.Base.Method', 5, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (13, 14, N'Kistl.GUI.ObjectListMethodPresenter', 13, N'Kistl.App.Base.Method', 3, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (14, 14, N'Kistl.GUI.ObjectMethodPresenter', 13, N'Kistl.App.Base.Method', 4, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (15, 14, N'Kistl.GUI.DefaultListPresenter`1', NULL, N'System.Collections.Generic.IList`1[[Kistl.App.Base.EnumerationEntry, Kistl.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]', 16, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (16, 14, N'Kistl.GUI.EnumerationPresenter`1', 13, N'Kistl.App.Base.EnumerationProperty', 15, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (17, 14, N'Kistl.GUI.DefaultListPresenter`1', 13, N'Kistl.App.Base.StringProperty', 14, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (18, 14, N'Kistl.GUI.DefaultValuePresenter`1', 13, N'Kistl.App.Base.StringProperty', 13, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (19, 14, N'Kistl.GUI.DefaultListPresenter`1', 13, N'Kistl.App.Base.IntProperty', 12, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (20, 14, N'Kistl.GUI.DefaultStructPresenter`1', 13, N'Kistl.App.Base.IntProperty', 11, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (21, 14, N'Kistl.GUI.DefaultListPresenter`1', 13, N'Kistl.App.Base.DoubleProperty', 10, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (22, 14, N'Kistl.GUI.DefaultStructPresenter`1', 13, N'Kistl.App.Base.DoubleProperty', 9, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (23, 14, N'Kistl.GUI.DefaultListPresenter`1', 13, N'Kistl.App.Base.DateTimeProperty', 8, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (24, 14, N'Kistl.GUI.DefaultStructPresenter`1', 13, N'Kistl.App.Base.DateTimeProperty', 7, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (25, 14, N'Kistl.GUI.DefaultListPresenter`1', 13, N'Kistl.App.Base.BoolProperty', 6, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (26, 14, N'Kistl.GUI.DefaultStructPresenter`1', 13, N'Kistl.App.Base.BoolProperty', 5, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (27, 14, N'Kistl.GUI.BackReferencePresenter`1', 13, N'Kistl.App.Base.BackReferenceProperty', 3, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (28, 14, N'Kistl.GUI.ObjectListPresenter`1', 13, N'Kistl.App.Base.ObjectReferenceProperty', 3, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (29, 14, N'Kistl.GUI.ObjectReferencePresenter`1', 13, N'Kistl.App.Base.ObjectReferenceProperty', 4, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (30, 14, N'Kistl.GUI.GroupPresenter', 14, N'Kistl.GUI.GroupPresenter', 2, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (31, 14, N'Kistl.GUI.ObjectPresenter', 15, N'Kistl.API.IDataObject', 1, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (32, 14, N'Kistl.GUI.ActionPresenter', 13, N'Kistl.App.Base.Method', 17, NULL, NULL)
INSERT [dbo].[PresenterInfos] ([ID], [fk_PresenterAssembly], [PresenterTypeName], [fk_DataAssembly], [DataTypeName], [ControlType], [fk_PresenterAssembly_pos], [fk_DataAssembly_pos]) VALUES (33, 4, N'Kistl.GUI.Renderer.WPF.TemplateEditorPresenter', 13, N'Kistl.App.Base.ObjectReferenceProperty', 19, NULL, NULL)
SET IDENTITY_INSERT [dbo].[PresenterInfos] OFF
/****** Object:  Table [dbo].[PresentableModelDescriptors]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PresentableModelDescriptors]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PresentableModelDescriptors](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](4000) NOT NULL,
	[DefaultVisualType] [int] NOT NULL,
	[fk_PresentableModelRef] [int] NOT NULL,
 CONSTRAINT [PK_PresentableModelDescriptors] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[PresentableModelDescriptors] ON
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (1, N'A debugger window showing the used IKistlContexts and their AttachedObjects', 22, 95)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (2, N'A top-level window containing a Workspace, a visual representation for IKistlContext', 24, 13)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (3, N'A task for the user: select an IDataObject from a list', 23, 18)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (4, N'An action which can be triggered by the user', 17, 16)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (5, N'A reference to an IDataObject', 4, 34)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (6, N'A list of IDataObjects', 3, 17)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (7, N'A complete IDataObject', 1, 83)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (8, N'Select a string value from a set of values', 26, 394)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (9, N'A string attribute', 13, 384)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (10, N'An integer attribute', 13, 399)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (11, N'A floating point attribute', 13, 404)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (12, N'A date and time attribute', 13, 409)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (13, N'A simple true/false attribute', 5, 414)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (14, N'A method returning an IDataObject reference', 4, 448)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (15, N'A group of properties', 2, 283)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (16, N'A method returning a string', 13, 450)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (17, N'A method returning an integer value', 13, 454)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (18, N'A method returning a floating point value', 13, 458)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (19, N'A method returning a date and time value', 13, 462)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (20, N'A method returning true or false', 5, 466)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (21, N'An enumeration value for Multiplicity', 15, 273)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (22, N'An enumeration value for StorageType', 15, 274)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (23, N'An enumeration value for VisualType', 15, 275)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (24, N'An enumeration value for Toolkit', 15, 276)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (25, N'An enumeration value for TestEnum', 15, 277)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (30, N'DataObjectModel with specific extensions for DataTypes', 1, 11)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (31, N'DataObjectModel with specific extensions for MethodInvocations', 1, 41)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (32, N'DataObjectModel with specific extensions for Modules', 1, 9)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (33, N'DataObjectModel with specific extensions for ObjectClasses', 1, 10)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (37, N'A model for a single work effort', 1, 563)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (38, N'A workspace for recording work efforts', 24, 564)
INSERT [dbo].[PresentableModelDescriptors] ([ID], [Description], [DefaultVisualType], [fk_PresentableModelRef]) VALUES (39, N'A GUID attribute', 27, 570)
SET IDENTITY_INSERT [dbo].[PresentableModelDescriptors] OFF
/****** Object:  Table [dbo].[Visuals]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Visuals]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Visuals](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[ControlType] [int] NOT NULL,
	[fk_Property] [int] NULL,
	[fk_Method] [int] NOT NULL,
	[fk_Property_pos] [int] NULL,
	[fk_Method_pos] [int] NULL,
 CONSTRAINT [PK_Visuals] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[TypeRefs_GenericArgumentsCollection]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TypeRefs_GenericArgumentsCollection]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TypeRefs_GenericArgumentsCollection](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_TypeRef] [int] NOT NULL,
	[fk_TypeRef_pos] [int] NULL,
	[fk_GenericArguments] [int] NOT NULL,
	[fk_GenericArguments_pos] [int] NULL,
	[ExportGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_TypeRefs_GenericArgumentsCollection] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ON
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (1, 273, 2147483647, 108, 0, N'cb012aac-38bd-46c8-8e27-892764fba21a')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (2, 274, 2147483647, 110, 0, N'f104ac47-e879-453e-b622-c1663d55f415')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (3, 275, 2147483647, 112, 0, N'ad86d298-9e2b-4eff-ba4d-889a05fe18b6')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (4, 276, 2147483647, 114, 0, N'503e66c0-9828-4f84-a2f8-106cd3621779')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (5, 277, 2147483647, 116, 0, N'f176c630-28bf-4a48-8b21-34d1375b711c')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (6, 355, 2147483647, 354, 0, N'5718947a-382d-4ab0-881f-628592f3ec80')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (7, 354, 2147483647, 83, 0, N'd4f01b9e-551f-44d0-908a-dc26acabb0af')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (9, 357, 2147483647, 83, 0, N'c2036d30-3942-40c4-babb-996dbc024b8a')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (26, 374, 2147483647, 83, 0, N'3b4b6ecd-c287-4f17-b896-807c547468d8')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (32, 382, 2147483647, 383, 0, N'e181d1eb-1cd9-4704-8b75-9c25eddc2936')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (33, 384, 2147483647, 383, 0, N'bdda6da6-48e4-41da-9e82-5bbe4b5ad1e5')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (43, 394, 2147483647, 383, 0, N'1582aa8a-a838-4d74-82d1-fafe0a9f0927')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (44, 396, 2147483647, 395, 0, N'd4c76b22-32de-4cf7-8700-0adc5eb47523')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (45, 395, 2147483647, 398, 0, N'8c6d1d92-4d88-412b-bf83-ea22a85a2522')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (47, 399, 2147483647, 398, 0, N'550ddaa7-2b50-4854-bc3b-82b521a616d3')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (48, 401, 2147483647, 400, 0, N'5ed90df1-cd86-4008-b643-8d80295c1f20')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (49, 400, 2147483647, 403, 0, N'c2d5ec12-f57a-4287-ab1d-a798bfe7233f')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (51, 404, 2147483647, 403, 0, N'8b83c90f-8145-4bc7-b33e-f002ef22352c')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (52, 406, 2147483647, 405, 0, N'349a6e04-34ce-4c7c-9e43-329476fda75b')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (53, 405, 2147483647, 408, 0, N'd5d38cde-d682-45b6-90a5-e8284622f1f1')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (55, 409, 2147483647, 408, 0, N'cae47988-febb-4e5f-9513-09300506d785')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (56, 411, 2147483647, 410, 0, N'407a2e3f-c0f8-4ba5-ac15-0b7facb49a56')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (57, 410, 2147483647, 413, 0, N'216c9704-2d13-42a1-88fa-c1893addcccf')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (59, 414, 2147483647, 413, 0, N'2b9e86f3-2225-4ec8-934c-c9a2e6791ac4')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (60, 416, 2147483647, 415, 0, N'e2b340c8-1307-414d-a44b-9671ee766675')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (61, 415, 2147483647, 383, 0, N'e2207981-7e2c-4e0a-959c-2e0da00e68a7')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (63, 418, 2147483647, 17, 0, N'523d401a-fec1-4e30-b07e-49df2ca575f1')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (64, 419, 2147483647, 34, 0, N'491c4f59-5f63-456e-8704-d0be52615753')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (65, 420, 2147483647, 83, 0, N'9bcd369f-340b-4f04-8a4e-3fc8ba4e931a')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (66, 422, 2147483647, 421, 0, N'038fbfbf-faa2-4e74-9c1e-5d56c5309ef7')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (67, 421, 2147483647, 108, 0, N'3dd59e85-b3d7-4e6f-8b53-aad7c9404628')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (69, 424, 2147483647, 108, 0, N'fe9387e5-c0ff-419c-a39b-e9c5424fea00')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (71, 427, 2147483647, 426, 0, N'a8ea4257-3f27-472f-89f1-afbfe76510e0')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (72, 426, 2147483647, 110, 0, N'17371a67-2e45-415f-af91-5db526cdda9b')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (74, 429, 2147483647, 110, 0, N'a35813b6-03c4-4024-b53b-1f5d1b51a194')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (76, 432, 2147483647, 431, 0, N'd03abbd1-0550-41f8-aabf-0a967838f0d1')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (77, 431, 2147483647, 112, 0, N'5170ea61-fb38-4f0f-9972-28aac2bdc177')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (82, 436, 2147483647, 114, 0, N'b7f697ef-9a96-4d21-af2c-b05bffc414c6')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (92, 448, 2147483647, 447, 0, N'01342f9e-9d6a-437e-982e-98292aa7a65f')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (93, 449, 2147483647, 383, 0, N'e5931405-a850-419a-8bec-125245424bd6')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (94, 450, 2147483647, 383, 0, N'de51d764-8e9c-4778-9285-e204fbba2125')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (98, 454, 2147483647, 398, 0, N'aef2d9bf-31a0-4d5f-baf8-c983f6f8ef0d')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (102, 458, 2147483647, 403, 0, N'e9a02f3a-1f6c-4504-8b21-99506d302b0d')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (106, 462, 2147483647, 408, 0, N'ba591c26-4b17-40f8-baa7-326134785c73')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (107, 559, 2147483647, 410, 0, N'a6a7fc37-5930-4bb8-a1e7-fb09af4cff5d')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (110, 466, 2147483647, 413, 0, N'9d9c72e2-8ace-48f6-8179-658339fdd42b')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (113, 470, 2147483647, 469, 0, N'8ac8312b-e106-4304-99dc-c95990290ae7')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (114, 469, 2147483647, 383, 0, N'1c3964e0-412b-4453-8771-dd9ee5f6264d')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (160, 434, 2147483647, 112, 0, N'fa9c71e2-80a3-4fd7-9474-14c74f75bfb9')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (164, 437, 2147483647, 436, 0, N'b9c379d8-f815-4651-821a-c6d3458710bf')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (167, 439, 2147483647, 114, 0, N'21d7e646-30bc-4453-b03a-05600401e05a')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (168, 442, 2147483647, 441, 0, N'47894b80-951a-4704-b9d6-f2531b2bc262')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (172, 441, 2147483647, 116, 0, N'f31c61b5-a144-4db5-9e5f-677ed3e56c72')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (174, 444, 2147483647, 116, 0, N'4a80bccb-11d0-4411-9934-750f4619af61')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (177, 556, 2147483647, 395, 0, N'fe3d6252-9728-49e5-a6d6-63d0fe3097d3')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (180, 557, 2147483647, 400, 0, N'dde28223-9e76-4cea-a733-0f652115c3b9')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (201, 558, 2147483647, 405, 0, N'5d2bdab1-db55-4099-8442-627481ce9638')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (203, 560, 2147483647, 447, 0, N'2857425c-7cec-4ae2-a733-8b60a140cbaf')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (204, 561, 2147483647, 395, 0, N'ecb9f113-e19c-4577-92d1-0184acb924cc')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (205, 562, 2147483647, 398, 0, N'e14186d8-92fc-4cb1-b2bb-bc0f141d666c')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (206, 567, 2147483647, 568, 0, N'50423043-3adc-4994-a931-66fa380135dc')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (207, 568, 2147483647, 569, 0, N'732a92f8-fd78-407a-ace2-6f8f654e79c8')
INSERT [dbo].[TypeRefs_GenericArgumentsCollection] ([ID], [fk_TypeRef], [fk_TypeRef_pos], [fk_GenericArguments], [fk_GenericArguments_pos], [ExportGuid]) VALUES (208, 570, 2147483647, 569, 0, N'098cf9f3-f254-45d8-8b0b-f49787dd370d')
SET IDENTITY_INSERT [dbo].[TypeRefs_GenericArgumentsCollection] OFF
/****** Object:  Table [dbo].[BaseParameters]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BaseParameters]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BaseParameters](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParameterName] [nvarchar](100) NOT NULL,
	[fk_Method] [int] NOT NULL,
	[fk_Method_pos] [int] NULL,
	[fk_Module] [int] NOT NULL,
	[IsList] [bit] NOT NULL,
	[IsReturnParameter] [bit] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[ExportGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BaseParameters] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[BaseParameters] ON
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (1, N'TestString', 83, 6, 2, 0, 0, NULL, N'55ec2e74-5d6a-418f-b370-86da5ec89a70')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (2, N'TestInt', 83, 4, 2, 0, 0, NULL, N'6c64e415-e5fd-4146-be0b-7aa9cf3727bd')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (3, N'ReturnParameter', 1, NULL, 1, 0, 1, NULL, N'f1abbc13-1167-42f3-9c27-826dd963fa65')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (5, N'ReturnParameter', 82, NULL, 1, 0, 1, NULL, N'8756dd12-12ad-4883-8137-ba086f474933')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (6, N'TestDouble', 83, 3, 2, 0, 0, NULL, N'bdb9baf5-8e78-4721-b97a-8d60997cfd01')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (7, N'TestBool', 83, 0, 2, 0, 0, NULL, N'a5538f74-d4c6-498a-95c0-b526dddf86be')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (8, N'TestDateTime', 83, 2, 2, 0, 0, NULL, N'9200143f-437e-4c8d-9150-fcdb6c3e4e11')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (9, N'TestDateTimeReturn', 83, 7, 2, 0, 1, NULL, N'9cf2824c-3570-4230-aa1a-c1146eb3226b')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (10, N'TestObjectParameter', 83, 5, 2, 0, 0, NULL, N'1fa8c4d8-83a5-42b9-848d-c113951fe49f')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (11, N'TestCLRObjectParameter', 83, 1, 2, 0, 0, NULL, N'5fc58d51-b4f0-4d78-b973-834d3e9e4962')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (12, N'DateTimeParam', 90, NULL, 5, 0, 0, NULL, N'ddd0bd0d-5e07-44fb-a9ce-e67fa232459c')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (13, N'DateTimeParamForTestMethod', 95, NULL, 5, 0, 0, NULL, N'1e80ab18-5034-4835-a688-e4ea028a7116')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (14, N'message', 96, NULL, 4, 0, 0, NULL, N'c2c27d9c-7e12-4e13-8014-dd0900de41dc')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (16, N'obj', 97, NULL, 4, 0, 0, NULL, N'e389c092-d4a5-44d6-a0d7-157e629fb032')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (18, N'ctx', 98, NULL, 4, 0, 0, NULL, N'887f23ba-d2dd-409d-a94a-18fd0615cb80')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (19, N'objectType', 98, NULL, 4, 0, 0, NULL, N'008a012a-6800-4048-b50f-6e9068e89bcf')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (20, N'result', 98, NULL, 4, 0, 1, NULL, N'c3dae1ab-3c30-4ada-991e-163db90df26e')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (21, N'ReturnParameter', 118, NULL, 1, 0, 1, NULL, N'ab9c14a8-e232-4705-bd91-256d8f718d4b')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (23, N'ReturnParameter', 120, NULL, 1, 0, 1, NULL, N'3eae78bd-7474-4e9f-87e7-9d8014f9e0bd')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (24, N'ReturnParameter', 121, NULL, 1, 0, 1, NULL, N'2a95f5c5-bae6-48e6-a4b9-15381fffd615')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (25, N'ReturnParameter', 123, NULL, 1, 0, 1, NULL, N'91bcf25a-efbe-498a-86a7-5d5a0b0e2b43')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (26, N'Result', 124, NULL, 1, 0, 1, NULL, N'95a488c0-e4fc-479d-8d88-786c8d364694')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (27, N'Result', 125, NULL, 1, 1, 1, NULL, N'd4869404-976b-4853-84fd-72403d4ea6e2')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (28, N'constrainedValue', 135, 2, 1, 0, 0, NULL, N'c72cd537-eff1-47fc-a58c-7ff6f0c5c708')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (29, N'return', 135, 0, 1, 0, 1, NULL, N'be54ef68-8c8b-4b84-a03b-4f4a6c687606')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (30, N'result', 139, 0, 1, 0, 1, NULL, N'007922b8-b97d-4a66-af57-fb7effe4e84a')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (31, N'constrainedValue', 139, 2, 1, 0, 0, NULL, N'bc641521-f091-4c9c-9065-0ef1d2813e90')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (33, N'result', 141, NULL, 4, 0, 1, NULL, N'b42c2a64-aa35-4f17-83f2-12bc1b3361fb')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (34, N'constrainedObject', 135, 1, 1, 0, 0, NULL, N'8b256774-a753-4463-9d36-dd652c02e5d2')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (35, N'constrainedObject', 139, 1, 1, 0, 0, NULL, N'd0d5e933-5bca-4be9-8cbb-91088065f2cb')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (36, N'cls', 144, NULL, 4, 0, 0, NULL, N'c0ee1435-955b-4ed8-b633-d4165a56b048')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (37, N'return', 151, 0, 1, 0, 1, N'the referenced Type', N'355175eb-1741-46a5-a03b-6f121604023e')
INSERT [dbo].[BaseParameters] ([ID], [ParameterName], [fk_Method], [fk_Method_pos], [fk_Module], [IsList], [IsReturnParameter], [Description], [ExportGuid]) VALUES (38, N'throwOnError', 151, 1, 1, 0, 0, N'whether to return null (false) or throw an Exception (true) on error', N'd66e8a92-2601-424f-8ca5-814a8dd9c8ce')
SET IDENTITY_INSERT [dbo].[BaseParameters] OFF
/****** Object:  Table [dbo].[MethodInvocations]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MethodInvocations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MethodInvocations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_Method] [int] NOT NULL,
	[MemberName] [nvarchar](200) NOT NULL,
	[fk_Module] [int] NOT NULL,
	[fk_InvokeOnObjectClass] [int] NOT NULL,
	[fk_Implementor] [int] NOT NULL,
	[fk_Method_pos] [int] NULL,
	[fk_Module_pos] [int] NULL,
	[fk_InvokeOnObjectClass_pos] [int] NULL,
	[fk_Implementor_pos] [int] NULL,
	[ExportGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_MethodInvocation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[MethodInvocations] ON
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (1, 8, N'OnToString_Projekt', 2, 3, 8, NULL, NULL, NULL, NULL, N'f387d644-edf4-486d-a8c7-c89d3e4f19d4')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (2, 17, N'OnToString_Mitarbeiter', 2, 6, 8, NULL, NULL, NULL, NULL, N'8bd5a9b3-a772-475d-804b-a297cfd7e701')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (4, 11, N'OnToString_Task', 2, 4, 8, NULL, NULL, NULL, NULL, N'6bacb191-b67b-4ef4-b4f6-57cc1516dd83')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (5, 5, N'OnToString_DataType', 1, 33, 1, NULL, NULL, NULL, NULL, N'd5293154-8f56-4787-ab32-e450ae395c69')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (6, 44, N'OnToString_MethodInvokation', 1, 30, 1, NULL, NULL, NULL, NULL, N'3ba9141d-086d-4dba-a2dd-14862c784b90')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (9, 20, N'OnToString_Method', 1, 10, 1, NULL, NULL, NULL, NULL, N'0d9aab5c-2aed-44ea-9296-5bc0b38923c0')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (10, 23, N'OnToString_Module', 1, 18, 1, NULL, NULL, NULL, NULL, N'd69600c6-665d-443f-9d78-22060f53991d')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (11, 26, N'OnToString_Auftrag', 2, 19, 8, NULL, NULL, NULL, NULL, N'b4a104bb-0c4d-4a95-9239-9511800e1319')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (12, 29, N'OnToString_WorkEffortAccount', 3, 20, 6, NULL, NULL, NULL, NULL, N'24fb2177-6843-421b-bac1-4a00f406b8d8')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (14, 35, N'OnToString_Kunde', 2, 26, 8, NULL, NULL, NULL, NULL, N'c20ef573-c5ff-449d-b535-2a4ef37e6303')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (15, 38, N'OnToString_Icon', 4, 27, 5, NULL, NULL, NULL, NULL, N'69d046b9-cfcc-4280-8008-17bcc613af85')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (16, 41, N'OnToString_Assembly', 1, 29, 1, NULL, NULL, NULL, NULL, N'6212f158-325d-4750-9f79-7b2ab43669c7')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (17, 14, N'OnToString_ObjectReferenceProperty', 1, 14, 1, NULL, NULL, NULL, NULL, N'0dbd7a68-f32a-42b6-a3ff-2753ff289ae9')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (19, 3, N'OnRechnungErstellen_Auftrag', 2, 19, 8, NULL, NULL, NULL, NULL, N'8a38bdff-88c7-4bfb-b1dc-05f8c8234b0a')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (20, 1, N'OnGetPropertyTypeString_StringProperty', 1, 9, 1, NULL, NULL, NULL, NULL, N'ddd2278f-9069-4767-9a05-11b87880c0d2')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (21, 1, N'OnGetPropertyTypeString_IntProperty', 1, 11, 1, NULL, NULL, NULL, NULL, N'56a64823-8ac3-4dc5-a11c-8db5b56dbddd')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (22, 1, N'OnGetPropertyTypeString_BoolProperty', 1, 12, 1, NULL, NULL, NULL, NULL, N'50f09af4-5e9d-4339-a34f-38d68077bbf7')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (23, 1, N'OnGetPropertyTypeString_DoubleProperty', 1, 13, 1, NULL, NULL, NULL, NULL, N'eac0f368-f0b1-45b9-8bd5-75210386dc1a')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (24, 1, N'OnGetPropertyTypeString_DateTimeProperty', 1, 15, 1, NULL, NULL, NULL, NULL, N'7b6ccd17-68f2-41fa-be47-30e0a487aa7e')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (26, 1, N'OnGetPropertyTypeString_ObjectReferenceProperty', 1, 14, 1, NULL, NULL, NULL, NULL, N'9185f3d4-ff4b-4994-9a4c-96c39eb4cd77')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (28, 6, N'OnPreSave_ObjectClass', 1, 2, 4, NULL, NULL, NULL, NULL, N'd833f03b-a7c1-4d77-b444-2e3917994964')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (29, 9, N'OnPreSetObject_Projekt', 2, 3, 3, NULL, NULL, NULL, NULL, N'16728243-4e95-4816-8fdf-ca5f5fbbe3fd')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (30, 12, N'OnPreSetObject_Task', 2, 4, 3, NULL, NULL, NULL, NULL, N'e7f3e42d-80c3-4821-9495-4959f4fc9e88')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (31, 1, N'OnGetPropertyTypeString_StringProperty', 1, 9, 4, NULL, NULL, NULL, NULL, N'5d48ffa9-6fbd-43f2-92bc-f7e259f7de98')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (32, 1, N'OnGetPropertyTypeString_IntProperty', 1, 11, 4, NULL, NULL, NULL, NULL, N'e82f1461-da07-40a6-be5b-8651fefb7514')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (33, 1, N'OnGetPropertyTypeString_BoolProperty', 1, 12, 4, NULL, NULL, NULL, NULL, N'1cc5de8e-4f7c-40dc-aaef-0473b2704853')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (34, 1, N'OnGetPropertyTypeString_DoubleProperty', 1, 13, 4, NULL, NULL, NULL, NULL, N'da4714d7-25a8-4833-8fd3-b8f4120e6888')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (35, 1, N'OnGetPropertyTypeString_DateTimeProperty', 1, 15, 4, NULL, NULL, NULL, NULL, N'e8e1f8cf-0c95-42e8-963e-ca6fc77aeed5')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (37, 1, N'OnGetPropertyTypeString_ObjectReferenceProperty', 1, 14, 4, NULL, NULL, NULL, NULL, N'ac64b75d-7f73-4e21-9cd9-d94aa113ce65')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (45, 30, N'OnPreSave_WorkEffortAccount', 3, 20, 2, NULL, NULL, NULL, NULL, N'3dc43cd7-a83e-4fe6-92e9-d98da3a42e6c')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (47, 82, N'OnGetParameterTypeString_StringParameter', 1, 37, 4, NULL, NULL, NULL, NULL, N'1f8bf9fa-d179-4a99-8d79-8f341b61a1c7')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (48, 82, N'OnGetParameterTypeString_IntParameter', 1, 38, 4, NULL, NULL, NULL, NULL, N'513e276a-ebd2-4ec2-992b-78f03f70dfbe')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (49, 80, N'OnToString_BaseParameter', 1, 36, 1, NULL, NULL, NULL, NULL, N'1574f805-6fae-4c81-9fa3-87cbce822c29')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (50, 82, N'OnGetParameterTypeString_StringParameter', 1, 37, 1, NULL, NULL, NULL, NULL, N'e2af53fe-2f46-47b0-b7d1-b24cadefb7a5')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (51, 82, N'OnGetParameterTypeString_IntParameter', 1, 38, 1, NULL, NULL, NULL, NULL, N'3aa5e223-2090-461b-9795-42e6a52c23d5')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (52, 82, N'OnGetParameterTypeString_DoubleParameter', 1, 39, 1, NULL, NULL, NULL, NULL, N'c2714497-843a-4580-ada6-0fec0f2354cb')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (53, 82, N'OnGetParameterTypeString_DoubleParameter', 1, 39, 4, NULL, NULL, NULL, NULL, N'19023e4b-caa1-45a0-b9d7-c94b6c49d620')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (54, 82, N'OnGetParameterTypeString_DateTimeParameter', 1, 41, 4, NULL, NULL, NULL, NULL, N'bd9d93b2-4c41-4bbb-b728-b177ebe94141')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (55, 82, N'OnGetParameterTypeString_BoolParameter', 1, 40, 4, NULL, NULL, NULL, NULL, N'f2641340-b44e-43de-9acf-4bd5e5f01e6b')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (56, 82, N'OnGetParameterTypeString_BoolParameter', 1, 40, 1, NULL, NULL, NULL, NULL, N'94ba66d2-0634-4079-acda-bc7998d5066d')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (57, 82, N'OnGetParameterTypeString_DateTimeParameter', 1, 41, 1, NULL, NULL, NULL, NULL, N'b2267475-a3ce-4d38-93c1-c80cad7b3bed')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (58, 82, N'OnGetParameterTypeString_ObjectParameter', 1, 42, 1, NULL, NULL, NULL, NULL, N'774e85c1-89fd-43b3-91df-54a5687eb176')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (59, 82, N'OnGetParameterTypeString_ObjectParameter', 1, 42, 4, NULL, NULL, NULL, NULL, N'1cc28c7d-f14e-4705-afdb-dcdf927367d3')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (60, 82, N'OnGetParameterTypeString_CLRObjectParameter', 1, 43, 1, NULL, NULL, NULL, NULL, N'362d841b-fe36-4172-b36b-d9917e2c296d')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (61, 82, N'OnGetParameterTypeString_CLRObjectParameter', 1, 43, 4, NULL, NULL, NULL, NULL, N'251a7e91-2304-479f-9b94-1ca7be1b5568')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (62, 79, N'OnPreSave_BaseParameter', 1, 36, 4, NULL, NULL, NULL, NULL, N'1a66deeb-a730-4dd6-88b1-b39938fc054e')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (63, 21, N'OnPreSave_Method', 1, 10, 4, NULL, NULL, NULL, NULL, N'1fc20923-3ad9-4c56-87f6-33503e5da0e4')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (64, 1, N'OnGetPropertyTypeString_EnumerationProperty', 1, 47, 4, NULL, NULL, NULL, NULL, N'3b661219-41f3-4532-b2de-d868adacd4cc')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (65, 1, N'OnGetPropertyTypeString_EnumerationProperty', 5, 47, 1, NULL, NULL, NULL, NULL, N'775b848f-b5a0-4f88-9fa6-a4138a692ef2')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (66, 85, N'OnToString_Enumeration', 1, 45, 1, NULL, NULL, NULL, NULL, N'60c0a9c2-37ff-42c3-834b-916d2758621b')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (67, 88, N'OnToString_EnumerationEntry', 1, 46, 1, NULL, NULL, NULL, NULL, N'd5ff96e3-047e-4a4f-9c47-4e00bae2cff9')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (72, 121, N'OnGetDataType_DataType', 1, 33, 1, NULL, NULL, NULL, NULL, N'7f8f1eed-b24d-4c12-902c-99845a38a660')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (73, 121, N'OnGetDataType_DataType', 1, 33, 4, NULL, NULL, NULL, NULL, N'1fd51a5f-c218-400e-b2c2-d8c155c7e209')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (74, 120, N'OnGetDataTypeString_DataType', 1, 33, 4, NULL, NULL, NULL, NULL, N'901159a3-1916-469e-be6c-2819549a28ca')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (75, 120, N'OnGetDataTypeString_DataType', 1, 33, 1, NULL, NULL, NULL, NULL, N'387352d3-53dd-4781-aa67-2d3e72a17efc')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (76, 123, N'OnGetParameterType_BaseParameter', 1, 36, 4, NULL, NULL, NULL, NULL, N'7d763ba7-06cc-4b73-a849-b6d7fc01f927')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (77, 123, N'OnGetParameterType_BaseParameter', 1, 36, 1, NULL, NULL, NULL, NULL, N'6832a4ee-fc58-4a33-8fa9-58262965c913')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (78, 123, N'OnGetParameterType_ObjectParameter', 1, 42, 1, NULL, NULL, NULL, NULL, N'0567f0b9-ea8b-4b00-bd45-063d5ad0ffc1')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (79, 123, N'OnGetParameterType_ObjectParameter', 1, 42, 4, NULL, NULL, NULL, NULL, N'1a294348-8170-45f5-bdc2-8cfef9884086')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (80, 124, N'OnGetReturnParameter_Method', 1, 10, 1, NULL, NULL, NULL, NULL, N'2f14a117-202e-4143-b113-6e22766d4178')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (81, 125, N'OnGetInheritedMethods_ObjectClass', 1, 2, 1, NULL, NULL, NULL, NULL, N'b47c766c-c8ad-4884-8a70-6d07280c8c49')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (82, 1, N'OnGetPropertyTypeString_StructProperty', 1, 64, 4, NULL, NULL, NULL, NULL, N'8599301e-7a78-44bd-a7a1-ee4198c88857')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (83, 1, N'OnGetPropertyTypeString_StructProperty', 1, 64, 1, NULL, NULL, NULL, NULL, N'ead61a5d-13e7-46e9-89c8-eeb3aaf50e33')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (87, 135, N'OnIsValid_Constraint', 1, 69, 1, NULL, NULL, NULL, NULL, N'65693fd7-2370-42d3-83af-fa884aee1b52')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (88, 135, N'OnIsValid_NotNullableConstraint', 1, 70, 1, NULL, NULL, NULL, NULL, N'3b7b97ce-56fd-402d-9dd8-3b4dc0c93106')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (89, 139, N'OnGetErrorText_NotNullableConstraint', 1, 70, 1, NULL, NULL, NULL, NULL, N'fca907ff-edd9-4afc-9e93-b624b2977665')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (90, 136, N'OnToString_NotNullableConstraint', 1, 70, 1, NULL, NULL, NULL, NULL, N'b9496293-8944-4db2-b7c5-1c7f4e745f33')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (91, 136, N'OnToString_IntegerRangeConstraint', 1, 71, 1, NULL, NULL, NULL, NULL, N'd4a21de9-8230-40aa-9459-2c901e26e6a8')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (92, 139, N'OnGetErrorText_IntegerRangeConstraint', 1, 71, 1, NULL, NULL, NULL, NULL, N'dfd3862f-6a15-4e15-af17-03b2139d9799')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (93, 135, N'OnIsValid_IntegerRangeConstraint', 1, 71, 1, NULL, NULL, NULL, NULL, N'8338b0a8-8aa7-483b-8d6e-1b6dfd267bb7')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (94, 141, N'OnToString_ControlInfo', 4, 54, 5, NULL, NULL, NULL, NULL, N'267f1386-adf5-4d62-afc1-3305cb6e44d8')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (95, 136, N'OnToString_StringRangeConstraint', 1, 73, 1, NULL, NULL, NULL, NULL, N'fd839d8e-3c9c-4fab-9a6a-2a0332055b56')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (96, 139, N'OnGetErrorText_StringRangeConstraint', 1, 73, 1, NULL, NULL, NULL, NULL, N'86948216-a253-4f33-9a26-829d57187930')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (97, 135, N'OnIsValid_StringRangeConstraint', 1, 73, 1, NULL, NULL, NULL, NULL, N'a676d5b0-7cd5-4b36-bf81-0ba8fc9c35a5')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (98, 139, N'OnGetErrorText_MethodInvocationConstraint', 1, 74, 1, NULL, NULL, NULL, NULL, N'9831e896-1442-433d-bc20-3bf56975f195')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (99, 136, N'OnToString_MethodInvocationConstraint', 1, 74, 1, NULL, NULL, NULL, NULL, N'29c81c1a-333a-4ca4-80dc-5748159d2bb7')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (100, 135, N'OnIsValid_MethodInvocationConstraint', 1, 74, 1, NULL, NULL, NULL, NULL, N'9e4e451e-a113-4a60-9676-60390c6f03d2')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (101, 126, N'OnToString_PresenterInfo', 4, 66, 5, NULL, NULL, NULL, NULL, N'5f5a9563-0a8a-4cb8-a51e-a8fae4a98348')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (102, 144, N'OnPrepareDefault_Template', 4, 68, 5, NULL, NULL, NULL, NULL, N'523c7fc1-7d9a-47d2-aa1e-815056b038b5')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (103, 129, N'OnToString_Visual', 4, 67, 5, NULL, NULL, NULL, NULL, N'7f866626-6125-4bc4-9ea2-4e462e790610')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (104, 135, N'OnIsValid_IsValidIdentifierConstraint', 1, 75, 1, NULL, NULL, NULL, NULL, N'7e8f8381-41bd-4c9f-bf3f-f3c7338d27f4')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (105, 139, N'OnGetErrorText_IsValidIdentifierConstraint', 1, 75, 1, NULL, NULL, NULL, NULL, N'9d8d4a56-0908-45b3-aae4-219c8176112b')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (106, 136, N'OnToString_IsValidIdentifierConstraint', 1, 75, 1, NULL, NULL, NULL, NULL, N'13d1f6c8-a554-4430-bbfc-b2a9c554fbd0')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (107, 135, N'OnIsValid_IsValidNamespaceConstraint', 1, 76, 1, NULL, NULL, NULL, NULL, N'45f63c85-c12b-48c0-a205-e0ddded61536')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (108, 151, N'OnAsType_TypeRef', 1, 79, 1, NULL, NULL, NULL, NULL, N'1601acf4-81f2-4e48-8d07-c9f917fe6b3f')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (109, 148, N'OnToString_TypeRef', 1, 79, 1, NULL, NULL, NULL, NULL, N'cb3fbcb4-55f5-4b5f-b3ad-cf8ce424739d')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (111, 155, N'OnRegenerateTypeRefs_Assembly', 1, 29, 1, NULL, NULL, NULL, NULL, N'41a3263c-0b67-42e4-a3dd-34215f3536ed')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (113, 157, N'OnToString_RelationEnd', 1, 82, 1, NULL, NULL, NULL, NULL, N'd67a2335-3d16-4a2d-938d-8e399c6f6a42')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (114, 146, N'OnToString_Relation', 1, 77, 1, NULL, NULL, NULL, NULL, N'11a661a7-00c1-403c-88d2-16eb62a7fcbe')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (115, 1, N'OnGetPropertyTypeString_Property', 1, 7, 4, NULL, NULL, NULL, NULL, N'01a404d3-9b4b-4a97-a762-d037fee186a3')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (116, 1, N'OnGetPropertyTypeString_Property', 1, 7, 1, NULL, NULL, NULL, NULL, N'34e03816-d0b6-45d1-840e-5ea6a3a72574')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (117, 118, N'OnGetPropertyType_Property', 1, 7, 4, NULL, NULL, NULL, NULL, N'4ded6dc8-4eda-4b7f-975a-56ed6eabc378')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (118, 118, N'OnGetPropertyType_Property', 1, 7, 1, NULL, NULL, NULL, NULL, N'39399484-a887-4783-bdbf-5e9e346b4350')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (119, 14, N'OnToString_Property', 1, 7, 1, NULL, NULL, NULL, NULL, N'fe091fe3-a2bb-4bf5-bc3d-160f41f11448')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (120, 165, N'OnToString_PresentableModelDescriptor', 4, 85, 5, NULL, NULL, NULL, NULL, N'9b06d50b-4bae-44f8-bafa-a46b5873895a')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (121, 1, N'OnGetPropertyTypeString_GuidProperty', 1, 89, 1, NULL, NULL, NULL, NULL, N'098d9140-88f6-4799-a4ba-3ebce35d8792')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (122, 1, N'OnGetPropertyTypeString_GuidProperty', 1, 89, 4, NULL, NULL, NULL, NULL, N'8ae2e6e6-e3a5-4685-9265-13ee7780552b')
INSERT [dbo].[MethodInvocations] ([ID], [fk_Method], [MemberName], [fk_Module], [fk_InvokeOnObjectClass], [fk_Implementor], [fk_Method_pos], [fk_Module_pos], [fk_InvokeOnObjectClass_pos], [fk_Implementor_pos], [ExportGuid]) VALUES (124, 162, N'OnToString_ViewDescriptor', 4, 83, 5, NULL, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000')
SET IDENTITY_INSERT [dbo].[MethodInvocations] OFF
/****** Object:  Table [dbo].[EnumerationEntries]    Script Date: 05/22/2009 17:30:29 ******/
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
	[Name] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](200) NULL,
 CONSTRAINT [PK_EnumerationEntries] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[EnumerationEntries] ON
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (2, 50, 0, N'First', N'First Test Entry')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (3, 50, 1, N'Second', N'Second Test Entry')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (5, 53, 0, N'WPF', N'WPF Toolkit')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (6, 53, 1, N'ASPNET', N'ASPNET Toolkit')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (7, 53, 2, N'TEST', N'TEST Toolkit')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (40, 55, 16, N'SimpleObjectList', N'')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (41, 55, 15, N'Enumeration', N'display a value from an Enumeration')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (42, 55, 14, N'StringList', N'')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (43, 55, 13, N'String', N'')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (44, 55, 12, N'IntegerList', N'')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (45, 55, 11, N'Integer', N'')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (46, 55, 10, N'DoubleList', N'')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (47, 55, 9, N'Double', N'')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (48, 55, 8, N'DateTimeList', N'a list of date/time values')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (49, 55, 7, N'DateTime', N'a date/time value')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (50, 55, 6, N'BooleanList', N'a list of booleans')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (51, 55, 5, N'Boolean', N'a boolean')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (52, 55, 4, N'ObjectReference', N'A reference to an object')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (53, 55, 3, N'ObjectList', N'A list of objects')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (54, 55, 2, N'PropertyGroup', N'A group of properties')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (55, 55, 1, N'Object', N'A full view of the object')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (56, 55, 0, N'Renderer', N'The renderer class is no actual "View", but neverthe less needs to be found')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (57, 55, 18, N'MenuGroup', N'')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (58, 55, 17, N'MenuItem', N'')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (59, 55, 19, N'TemplateEditor', N'')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (60, 78, 3, N'Replicate', N'The relation information is stored on both sides of the Relation')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (63, 81, 2, N'One', N'Required Element (exactly one)')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (64, 81, 1, N'ZeroOrOne', N'Optional Element (zero or one)')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (65, 81, 3, N'ZeroOrMore', N'Optional List Element (zero or more)')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (66, 78, 1, N'MergeIntoA', N'The relation information is stored with the A-side ObjectClass')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (67, 78, 2, N'MergeIntoB', N'The relation information is stored with the B-side ObjectClass')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (68, 78, 4, N'Separate', N'The relation information is stored in a separate entity')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (69, 55, 20, N'IntegerSlider', N'Displays an Integer with a slider instead of a text box')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (70, 55, 21, N'ObjectListEntry', N'An object as entry of a list')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (71, 55, 22, N'KistlDebugger', N'The debugger window for displaying the active contexts')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (74, 55, 23, N'SelectionTaskDialog', N'A task for the user: select a value from a list')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (75, 55, 24, N'WorkspaceWindow', N'A top-level window containing a Workspace, a visual representation for IKistlContext')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (76, 55, 26, N'StringSelection', N'Select a string from a aset of values')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (77, 53, 3, N'WinForms', N'Windows Forms Toolkit')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (78, 55, 27, N'Guid', N'')
INSERT [dbo].[EnumerationEntries] ([ID], [fk_Enumeration], [Value], [Name], [Description]) VALUES (79, 55, 28, N'GridCell', N'display a value in a GridCell')
SET IDENTITY_INSERT [dbo].[EnumerationEntries] OFF
/****** Object:  Table [dbo].[DoubleParameters]    Script Date: 05/22/2009 17:30:29 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[DoubleParameters] ([ID]) VALUES (6)
/****** Object:  Table [dbo].[IntParameters]    Script Date: 05/22/2009 17:30:29 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[IntParameters] ([ID]) VALUES (2)
/****** Object:  Table [dbo].[DateTimeParameters]    Script Date: 05/22/2009 17:30:29 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[DateTimeParameters] ([ID]) VALUES (8)
INSERT [dbo].[DateTimeParameters] ([ID]) VALUES (9)
INSERT [dbo].[DateTimeParameters] ([ID]) VALUES (12)
INSERT [dbo].[DateTimeParameters] ([ID]) VALUES (13)
/****** Object:  Table [dbo].[BoolParameters]    Script Date: 05/22/2009 17:30:29 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[BoolParameters] ([ID]) VALUES (7)
INSERT [dbo].[BoolParameters] ([ID]) VALUES (29)
INSERT [dbo].[BoolParameters] ([ID]) VALUES (38)
/****** Object:  Table [dbo].[CLRObjectParameters]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLRObjectParameters]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CLRObjectParameters](
	[ID] [int] NOT NULL,
	[fk_Assembly] [int] NULL,
	[FullTypeName] [nvarchar](200) NOT NULL,
	[fk_Assembly_pos] [int] NULL,
 CONSTRAINT [PK_CLRObjectParameters] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName], [fk_Assembly_pos]) VALUES (11, NULL, N'System.Guid', NULL)
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName], [fk_Assembly_pos]) VALUES (16, NULL, N'Kistl.API.IDataObject', NULL)
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName], [fk_Assembly_pos]) VALUES (18, NULL, N'Kistl.API.IKistlContext', NULL)
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName], [fk_Assembly_pos]) VALUES (19, NULL, N'System.Type', NULL)
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName], [fk_Assembly_pos]) VALUES (20, NULL, N'Kistl.API.IDataObject', NULL)
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName], [fk_Assembly_pos]) VALUES (21, NULL, N'System.Type', NULL)
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName], [fk_Assembly_pos]) VALUES (24, NULL, N'System.Type', NULL)
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName], [fk_Assembly_pos]) VALUES (25, NULL, N'System.Type', NULL)
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName], [fk_Assembly_pos]) VALUES (28, NULL, N'System.Object', NULL)
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName], [fk_Assembly_pos]) VALUES (31, NULL, N'System.Object', NULL)
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName], [fk_Assembly_pos]) VALUES (34, NULL, N'System.Object', NULL)
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName], [fk_Assembly_pos]) VALUES (35, NULL, N'System.Object', NULL)
INSERT [dbo].[CLRObjectParameters] ([ID], [fk_Assembly], [FullTypeName], [fk_Assembly_pos]) VALUES (37, NULL, N'System.Type', NULL)
/****** Object:  Table [dbo].[Visuals_MenuTreeCollection]    Script Date: 05/22/2009 17:30:29 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Visuals_ContextMenuCollection]    Script Date: 05/22/2009 17:30:29 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Visuals_ChildrenCollection]    Script Date: 05/22/2009 17:30:29 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Templates]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Templates]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Templates](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DisplayName] [nvarchar](200) NOT NULL,
	[fk_VisualTree] [int] NOT NULL,
	[DisplayedTypeFullName] [nvarchar](200) NOT NULL,
	[fk_DisplayedTypeAssembly] [int] NOT NULL,
	[fk_VisualTree_pos] [int] NULL,
	[fk_DisplayedTypeAssembly_pos] [int] NULL,
 CONSTRAINT [PK_Templates] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ViewDescriptors]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ViewDescriptors]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ViewDescriptors](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VisualType] [int] NOT NULL,
	[Toolkit] [int] NOT NULL,
	[fk_ControlRef] [int] NOT NULL,
	[fk_PresentedModelDescriptor] [int] NOT NULL,
 CONSTRAINT [PK_ViewDescriptors] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[ViewDescriptors] ON
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (1, 22, 0, 145, 1)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (2, 24, 1, 165, 2)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (3, 24, 3, 166, 2)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (4, 24, 0, 167, 2)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (5, 23, 0, 150, 3)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (6, 17, 0, 149, 4)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (7, 4, 1, 159, 5)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (8, 4, 3, 282, 5)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (9, 4, 0, 161, 5)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (10, 3, 1, 156, 6)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (11, 3, 3, 157, 6)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (12, 3, 0, 158, 6)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (13, 1, 1, 162, 7)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (14, 1, 3, 163, 7)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (15, 21, 0, 155, 7)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (16, 1, 0, 164, 7)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (17, 26, 0, 148, 8)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (18, 14, 0, 146, 9)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (19, 13, 1, 152, 9)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (20, 13, 3, 153, 9)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (21, 13, 0, 154, 9)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (22, 13, 1, 152, 10)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (23, 13, 3, 153, 10)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (24, 13, 0, 154, 10)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (25, 13, 1, 152, 11)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (26, 13, 3, 153, 11)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (27, 13, 0, 154, 11)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (28, 13, 1, 152, 12)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (29, 13, 3, 153, 12)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (30, 13, 0, 154, 12)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (31, 13, 1, 152, 13)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (32, 13, 3, 153, 13)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (33, 5, 0, 151, 13)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (34, 2, 0, 278, 15)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (35, 13, 1, 152, 16)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (36, 13, 3, 153, 16)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (37, 13, 0, 154, 16)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (38, 13, 1, 152, 17)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (39, 13, 3, 153, 17)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (40, 13, 0, 154, 17)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (41, 13, 1, 152, 18)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (42, 13, 3, 153, 18)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (43, 13, 0, 154, 18)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (44, 13, 1, 152, 19)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (45, 13, 3, 153, 19)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (46, 13, 0, 154, 19)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (47, 13, 1, 152, 20)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (48, 13, 3, 153, 20)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (49, 5, 0, 151, 20)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (50, 13, 1, 152, 21)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (51, 13, 3, 153, 21)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (52, 15, 0, 147, 21)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (53, 13, 1, 152, 22)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (54, 13, 3, 153, 22)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (55, 15, 0, 147, 22)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (56, 13, 1, 152, 23)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (57, 13, 3, 153, 23)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (58, 15, 0, 147, 23)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (59, 13, 1, 152, 24)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (60, 13, 3, 153, 24)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (61, 15, 0, 147, 24)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (62, 13, 1, 152, 25)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (63, 13, 3, 153, 25)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (64, 15, 0, 147, 25)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (65, 24, 0, 565, 38)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (66, 1, 0, 566, 37)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (67, 27, 1, 152, 39)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (68, 27, 3, 153, 39)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (69, 27, 0, 154, 39)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (70, 28, 0, 573, 10)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (71, 28, 0, 573, 9)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (72, 28, 0, 571, 16)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (73, 28, 0, 571, 14)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (74, 28, 0, 574, 5)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (75, 28, 0, 571, 6)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (76, 28, 0, 571, 39)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (77, 28, 0, 573, 11)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (78, 28, 0, 573, 12)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (80, 28, 0, 571, 17)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (81, 28, 0, 571, 18)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (82, 28, 0, 571, 19)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (83, 28, 0, 571, 20)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (84, 28, 0, 571, 25)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (85, 28, 0, 571, 24)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (86, 28, 0, 571, 23)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (87, 28, 0, 571, 22)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (88, 28, 0, 571, 21)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (89, 28, 0, 571, 8)
INSERT [dbo].[ViewDescriptors] ([ID], [VisualType], [Toolkit], [fk_ControlRef], [fk_PresentedModelDescriptor]) VALUES (90, 28, 0, 572, 13)
SET IDENTITY_INSERT [dbo].[ViewDescriptors] OFF
/****** Object:  Table [dbo].[ObjectClasses]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObjectClasses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ObjectClasses](
	[ID] [int] NOT NULL,
	[TableName] [nvarchar](100) NOT NULL,
	[fk_BaseObjectClass] [int] NULL,
	[IsSimpleObject] [bit] NOT NULL,
	[IsFrozenObject] [bit] NOT NULL,
	[fk_DefaultModel] [int] NULL,
	[fk_BaseObjectClass_pos] [int] NULL,
	[fk_DefaultModel_pos] [int] NULL,
	[fk_DefaultPresentableModelDescriptor] [int] NULL,
 CONSTRAINT [PK_ObjectClasses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (2, N'ObjectClasses', 33, 0, 0, 10, NULL, NULL, 33)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (3, N'Projekte', NULL, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (4, N'Tasks', NULL, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (6, N'Mitarbeiter', NULL, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (7, N'Properties', NULL, 0, 1, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (8, N'ValueTypeProperties', 7, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (9, N'StringProperties', 8, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (10, N'Methods', NULL, 0, 1, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (11, N'IntProperties', 8, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (12, N'BoolProperties', 8, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (13, N'DoubleProperties', 8, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (14, N'ObjectReferenceProperties', 7, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (15, N'DateTimeProperties', 8, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (18, N'Modules', NULL, 0, 1, 9, NULL, NULL, 32)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (19, N'Auftraege', NULL, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (20, N'WorkEffortAccounts', NULL, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (26, N'Kunden', NULL, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (27, N'Icons', NULL, 1, 1, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (29, N'Assemblies', NULL, 0, 1, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (30, N'MethodInvocations', NULL, 0, 1, 41, NULL, NULL, 31)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (33, N'DataTypes', NULL, 0, 1, 11, NULL, NULL, 30)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (36, N'BaseParameters', NULL, 0, 1, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (37, N'StringParameters', 36, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (38, N'IntParameters', 36, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (39, N'DoubleParameters', 36, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (40, N'BoolParameters', 36, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (41, N'DateTimeParameters', 36, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (42, N'ObjectParameters', 36, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (43, N'CLRObjectParameters', 36, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (44, N'Interfaces', 33, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (45, N'Enumerations', 33, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (46, N'EnumerationEntries', NULL, 1, 1, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (47, N'EnumerationProperties', 8, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (51, N'TestObjClasses', NULL, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (54, N'ControlInfos', NULL, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (58, N'TestCustomObjects', NULL, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (59, N'Muhblas', NULL, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (60, N'AnotherTests', NULL, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (61, N'LastTests', NULL, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (62, N'Structs', 33, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (64, N'StructProperties', 7, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (66, N'PresenterInfos', NULL, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (67, N'Visuals', NULL, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (68, N'Templates', NULL, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (69, N'Constraints', NULL, 0, 1, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (70, N'NotNullableConstraints', 69, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (71, N'IntegerRangeConstraints', 69, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (73, N'StringRangeConstraints', 69, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (74, N'MethodInvocationConstraints', 69, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (75, N'IsValidIdentifierConstraints', 69, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (76, N'IsValidNamespaceConstraints', 75, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (77, N'Relations', NULL, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (79, N'TypeRefs', NULL, 0, 1, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (82, N'RelationEnds', NULL, 0, 0, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (83, N'ViewDescriptors', NULL, 0, 1, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (85, N'PresentableModelDescriptors', NULL, 0, 1, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (86, N'PresenceRecords', NULL, 0, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (87, N'WorkEfforts', NULL, 0, 0, NULL, NULL, NULL, 37)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (89, N'GuidProperties', 8, 0, 1, NULL, NULL, NULL, 7)
INSERT [dbo].[ObjectClasses] ([ID], [TableName], [fk_BaseObjectClass], [IsSimpleObject], [IsFrozenObject], [fk_DefaultModel], [fk_BaseObjectClass_pos], [fk_DefaultModel_pos], [fk_DefaultPresentableModelDescriptor]) VALUES (90, N'CurrentSchema', NULL, 0, 0, NULL, NULL, NULL, 7)
/****** Object:  Table [dbo].[Properties]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Properties]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Properties](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IsNullable] [bit] NOT NULL,
	[IsList] [bit] NOT NULL,
	[IsIndexed] [bit] NOT NULL,
	[fk_ObjectClass] [int] NOT NULL,
	[PropertyName] [nvarchar](100) NOT NULL,
	[AltText] [nvarchar](200) NULL,
	[fk_Module] [int] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[fk_ObjectClass_pos] [int] NULL,
	[fk_Module_pos] [int] NULL,
	[CategoryTags] [nvarchar](4000) NULL,
	[fk_ValueModelDescriptor] [int] NULL,
	[ExportGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Properties] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Properties] ON
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (1, 0, 0, 0, 33, N'ClassName', N'Der Name der Objektklasse', 1, N'Der Name der Objektklasse', NULL, NULL, N'Summary,DataModel, Description', 9, N'083bbf12-aac6-4f5f-802a-d3701550bc84')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (3, 0, 0, 0, 2, N'TableName', N'Tabellenname in der Datenbank', 1, N'Tabellenname in der Datenbank', NULL, NULL, N'Physical', 9, N'2a5e5111-199c-4dce-8369-ce35ee741568')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (7, 1, 1, 0, 33, N'Properties', N'Eigenschaften der Objektklasse', 1, N'Eigenschaften der Objektklasse', NULL, NULL, N'DataModel', 6, N'e7d91162-0aa8-4fe3-9e29-d0519781ceb7')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (8, 0, 0, 0, 7, N'ObjectClass', NULL, 1, N'', NULL, NULL, N'DataModel', 5, N'bdaacacd-c8cb-45cf-a329-28f942337273')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (9, 1, 0, 0, 7, N'PropertyName', NULL, 1, N'', NULL, NULL, N'DataModel,Summary', 9, N'8c474623-7e53-4ca6-a996-f3b5a8c72834')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (11, 0, 0, 0, 7, N'IsList', NULL, 1, N'', NULL, NULL, N'DataModel,Summary', 13, N'b2bd1528-c22f-4e12-b80f-f8234a2c0831')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (13, 1, 0, 0, 3, N'Name', N'Projektname', 2, N'Projektname', NULL, NULL, NULL, 9, N'b5482479-fd14-4990-86f4-49872e2eeeb8')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (14, 1, 1, 0, 3, N'Tasks', NULL, 2, N'', NULL, NULL, NULL, 6, N'f6ff71b0-ccaf-4c7d-8e2b-1210a9df4b0f')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (15, 1, 0, 0, 4, N'Name', N'Taskname', 2, N'Taskname', NULL, NULL, NULL, 9, N'91595e02-411c-40f2-ab83-4cced76e954d')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (16, 1, 0, 0, 4, N'DatumVon', N'Start Datum', 2, N'Start Datum', NULL, 12, NULL, 12, N'1485a7b7-c4d5-456a-a18a-0c409c3eca8e')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (17, 1, 0, 0, 4, N'DatumBis', N'Enddatum', 2, N'Enddatum', NULL, 12, NULL, 12, N'2b705496-388a-43a8-82e8-b17b652a55fc')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (18, 1, 0, 0, 4, N'Aufwand', N'Aufwand in Stunden', 2, N'Aufwand in Stunden', NULL, 11, NULL, 11, N'a28f7536-9b8a-49ca-bc97-d28e1c2c4d3e')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (19, 1, 0, 0, 4, N'Projekt', N'Verknüpfung zum Projekt', 2, N'Verknüpfung zum Projekt', NULL, NULL, NULL, 5, N'5545ba8a-3e89-4b22-bd66-c12f3622ace0')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (20, 1, 0, 0, 6, N'Name', N'Vorname Nachname', 2, N'Vorname Nachname', NULL, NULL, NULL, 9, N'5aab79fd-3083-4ce1-a558-ed1449ecddce')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (21, 1, 1, 1, 6, N'Projekte', N'Projekte des Mitarbeiters für die er Verantwortlich ist', 2, N'Projekte des Mitarbeiters für die er Verantwortlich ist', NULL, NULL, NULL, 6, N'1abb5a1b-ba9f-4b75-b6ea-3d28be877b7c')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (22, 1, 1, 1, 3, N'Mitarbeiter', NULL, 2, N'', NULL, NULL, NULL, 6, N'3e60fe29-ac50-4232-bbeb-af023ede02f6')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (23, 1, 0, 0, 3, N'AufwandGes', NULL, 2, N'', NULL, 11, NULL, 11, N'a26cec7d-1e5c-44f5-9c56-92af595739eb')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (25, 1, 0, 0, 2, N'BaseObjectClass', N'Pointer auf die Basisklasse', 1, N'Pointer auf die Basisklasse', NULL, NULL, N'DataModel', 5, N'ad060d41-bc7a-41b8-a3e3-ec9302c8c714')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (26, 0, 0, 0, 7, N'IsNullable', NULL, 1, N'', NULL, NULL, N'DataModel,Summary', 13, N'6aa68dc6-d7f3-4809-89bb-e7474df0bde4')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (27, 1, 1, 0, 2, N'SubClasses', N'Liste der vererbten Klassen', 1, N'Liste der vererbten Klassen', NULL, NULL, N'DataModel', 6, N'0914de6e-966c-46fc-9359-e4da6c3608b1')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (28, 0, 0, 0, 9, N'Length', NULL, 1, N'', NULL, NULL, N'DataModel', 10, N'3588888e-b280-4e8d-8a7b-53f452b81bf0')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (29, 0, 0, 0, 10, N'ObjectClass', NULL, 1, N'', NULL, NULL, N'Summary', 5, N'9afc74a4-4eeb-4c39-879c-eacc8f369fa7')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (30, 0, 0, 0, 10, N'MethodName', NULL, 1, N'', NULL, NULL, N'Summary', 9, N'88de8421-488e-452e-8289-33074054b22f')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (31, 1, 1, 0, 33, N'Methods', N'Liste aller Methoden der Objektklasse.', 1, N'Liste aller Methoden der Objektklasse.', NULL, NULL, N'DataModel', 6, N'e9f8a1f1-a5ed-44a6-bbf3-9b040766f19f')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (38, 1, 0, 0, 6, N'Geburtstag', N'Herzlichen Glückwunsch zum Geburtstag', 2, N'Herzlichen Glückwunsch zum Geburtstag', NULL, 12, NULL, 12, N'b10bf288-1252-49c3-9129-cfabb1637c47')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (39, 1, 0, 0, 6, N'SVNr', N'NNNN TTMMYY', 2, N'NNNN TTMMYY', NULL, NULL, NULL, 9, N'505988c4-dd50-4a0f-be21-a360b25e7d7d')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (40, 1, 0, 0, 6, N'TelefonNummer', N'+43 123 12345678', 2, N'+43 123 12345678', NULL, NULL, NULL, 9, N'73230333-f975-4f0b-9dd3-e850b48d9c13')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (41, 1, 0, 0, 7, N'AltText', NULL, 1, N'', NULL, NULL, N'Description', 9, N'd5412422-270c-4e98-a67d-00bde83b7766')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (42, 0, 0, 0, 18, N'Namespace', N'CLR Namespace des Moduls', 1, N'CLR Namespace des Moduls', NULL, NULL, N'Summary', 9, N'36d2b9e7-d6b9-4a9c-a363-7e059a637919')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (43, 0, 0, 0, 18, N'ModuleName', N'Name des Moduls', 1, N'Name des Moduls', NULL, NULL, N'Summary', 9, N'63facb30-d8f7-42f6-8c14-85933d5f94b8')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (44, 1, 1, 0, 18, N'DataTypes', N'Datentypendes Modules', 1, N'Datentypendes Modules', NULL, NULL, N'Main', 6, N'a1711984-5263-4407-ac67-6e4123954976')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (45, 0, 0, 0, 33, N'Module', N'Modul der Objektklasse', 1, N'Modul der Objektklasse', NULL, NULL, N'Summary,MetaData', 5, N'4e1fb30b-e528-4968-95b0-f3a38eafe643')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (46, 0, 0, 0, 14, N'ReferenceObjectClass', N'Pointer zur Objektklasse', 1, N'Pointer zur Objektklasse', NULL, NULL, NULL, 5, N'1d0ac841-129a-41d9-8f7b-026216db5b6d')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (48, 1, 0, 0, 3, N'Kundenname', N'Bitte geben Sie den Kundennamen ein', 2, N'Bitte geben Sie den Kundennamen ein', NULL, NULL, NULL, 9, N'cd6be045-d1bd-4086-b848-c83249f5ca9b')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (49, 1, 0, 0, 19, N'Mitarbeiter', NULL, 2, N'', NULL, NULL, NULL, 5, N'5b57288b-835a-459e-8532-9f47e17ab2b5')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (50, 1, 0, 0, 19, N'Auftragsname', N'Bitte füllen Sie einen sprechenden Auftragsnamen aus', 2, N'Bitte füllen Sie einen sprechenden Auftragsnamen aus', NULL, NULL, NULL, 9, N'aaffed82-1f4c-4c0f-a52d-3ca4dbdefe94')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (51, 1, 0, 0, 19, N'Projekt', N'Projekt zum Auftrag', 2, N'Projekt zum Auftrag', NULL, NULL, NULL, 5, N'a0ad574a-356b-4962-a98d-c305b1289154')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (52, 0, 0, 0, 20, N'Name', N'descriptive name of this account', 3, N'Name des TimeRecordsskontos', NULL, NULL, N'Summary,Main', 9, N'763b0b46-8309-4532-ba98-36575f02a1d1')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (59, 0, 0, 0, 26, N'Kundenname', N'Name des Kunden', 2, N'Name des Kunden', NULL, NULL, NULL, 9, N'2817a845-b2d5-43ed-b0f1-5a6692a62183')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (60, 1, 0, 0, 26, N'Adresse', N'Adresse & Hausnummer', 2, N'Adresse &amp; Hausnummer', NULL, NULL, NULL, 9, N'7ba07561-15f4-495a-b2eb-59006e4210e5')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (61, 0, 0, 0, 26, N'PLZ', N'Postleitzahl', 2, N'Postleitzahl', NULL, NULL, NULL, 9, N'cafb4b93-4a1a-4753-8ec0-c65936a0d129')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (62, 1, 0, 0, 26, N'Ort', N'Ort', 2, N'Ort', NULL, NULL, NULL, 9, N'5281cbe0-8f63-4a2d-bb9e-2ee04588202d')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (63, 1, 0, 0, 26, N'Land', N'Land', 2, N'Land', NULL, NULL, NULL, 9, N'c01afb40-9f28-494f-9058-9d0eca79a125')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (64, 1, 0, 0, 19, N'Kunde', N'Kunde des Projektes', 2, N'Kunde des Projektes', NULL, NULL, NULL, 5, N'57c977da-c113-4ce6-9484-3828f74c4193')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (65, 1, 0, 0, 19, N'Auftragswert', N'Wert in EUR des Auftrages', 2, N'Wert in EUR des Auftrages', NULL, 11, NULL, 11, N'f252395f-7867-4299-9965-66f7a7b8f3c5')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (67, 1, 1, 0, 3, N'Auftraege', N'Aufträge', 2, N'Aufträge', NULL, NULL, NULL, 6, N'30a1d8b6-4db5-45a0-a9a8-531472a9107e')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (68, 0, 0, 0, 27, N'IconFile', N'Filename of the Icon', 4, N'Filename of the Icon', NULL, NULL, NULL, 9, N'cdbdfc01-5faa-416b-960f-2eb220f268fe')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (69, 1, 0, 0, 33, N'DefaultIcon', N'Standard Icon wenn IIcon nicht implementiert ist', 4, N'Standard Icon wenn IIcon nicht implementiert ist', NULL, NULL, N'Summary,Description, GUI', 5, N'b1402cda-de87-4b2a-bd65-a950b8dd7a9f')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (70, 0, 0, 0, 29, N'Module', N'Module', 1, N'Module', NULL, NULL, NULL, 5, N'8d579192-717e-4f2c-90ed-1c066255e270')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (71, 0, 0, 0, 29, N'AssemblyName', N'Full Assemblyname eg. MyActions, Version=1.0.0.0', 1, N'Full Assemblyname eg. MyActions, Version=1.0.0.0', NULL, NULL, NULL, 9, N'9a9dbd59-6816-4d25-9ef2-da84b96bf454')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (72, 0, 0, 0, 7, N'Module', N'Zugehörig zum Modul', 1, N'Zugehörig zum Modul', NULL, NULL, N'MetaData', 5, N'2105acf5-0b98-4d0b-9be4-049a502a4f03')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (73, 0, 0, 0, 10, N'Module', N'Zugehörig zum Modul', 1, N'Zugehörig zum Modul', NULL, NULL, N'Summary', 5, N'51640f6f-b2ae-4f26-915e-fda5a2c060a6')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (74, 0, 0, 0, 30, N'Method', N'Methode, die Aufgerufen wird', 1, N'Methode, die Aufgerufen wird', NULL, NULL, N'Summary', 5, N'b9db3b3b-d00d-479f-843f-6d41704eb079')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (77, 0, 0, 0, 30, N'MemberName', N'Name des implementierenden Members', 1, N'Name des implementierenden Members', NULL, NULL, N'Summary', 8, N'3282e04e-6cdd-4ba3-911c-3dd77b3eba66')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (78, 0, 0, 0, 30, N'Module', N'Zugehörig zum Modul', 1, N'Zugehörig zum Modul', NULL, NULL, N'Summary', 5, N'b959b313-2063-4391-bb97-14ac85d5dff0')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (79, 0, 0, 0, 30, N'InvokeOnObjectClass', N'In dieser Objektklasse implementieren', 1, N'In dieser Objektklasse implementieren', NULL, NULL, N'Summary', 5, N'94e98a50-cad2-4779-8c50-e4922d653781')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (80, 1, 1, 0, 33, N'MethodInvocations', N'all implemented Methods in this DataType', 1, N'all implemented Methods in this DataType', NULL, NULL, N'DataModel', 6, N'0f8a3e8b-29f6-49eb-99ba-f55ca1e161e9')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (81, 1, 1, 0, 10, N'MethodInvokations', N'Methodenaufrufe implementiert in dieser Objekt Klasse', 1, N'Methodenaufrufe implementiert in dieser Objekt Klasse', NULL, NULL, N'Main', 6, N'dc2bd380-6e63-4a44-bcc3-192780f80606')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (82, 1, 1, 0, 18, N'Assemblies', N'Assemblies des Moduls', 1, N'Assemblies des Moduls', NULL, NULL, N'Main', 6, N'cab23a85-a179-475c-a70f-77789e2a2907')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (83, 0, 0, 0, 29, N'IsClientAssembly', N'Legt fest, ob es sich um ein Client-Assembly handelt.', 1, N'Legt fest, ob es sich um ein Client-Assembly handelt.', NULL, NULL, NULL, 13, N'd6b0af6c-b0ed-40be-b8dd-76d2379989f5')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (85, 1, 1, 0, 26, N'EMails', N'EMails des Kunden - können mehrere sein', 2, N'EMails des Kunden - können mehrere sein', NULL, NULL, NULL, NULL, N'1d0f6da6-4b69-48d7-9e94-bfb5466654b9')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (86, 1, 1, 0, 20, N'Mitarbeiter', N'Zugeordnete Mitarbeiter', 3, N'Zugeordnete Mitarbeiter', NULL, NULL, N'Main', 6, N'21ed2b37-6e10-4aff-b4c1-554a1cc0e967')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (89, 1, 0, 0, 20, N'BudgetHours', N'Number of currently allocated hours', 3, N'Maximal erlaubte Stundenanzahl', NULL, 11, N'Summary,Main', 11, N'2f57b6c8-d798-43de-b9c8-29675ff0c65f')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (90, 1, 0, 0, 20, N'SpentHours', N'Number of already spent hours', 3, N'Aktuell gebuchte Stunden', NULL, 11, N'Summary,Main', 11, N'f7816f8a-0b07-429c-9161-47ca495a2e41')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (91, 0, 0, 0, 36, N'ParameterName', N'Name des Parameter', 1, N'Name des Parameter', NULL, NULL, NULL, 9, N'25c82fbd-cf5d-4021-b549-fccb46e166b3')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (92, 0, 0, 0, 36, N'Method', N'Methode des Parameters', 1, N'Methode des Parameters', NULL, NULL, NULL, 5, N'29d7eba7-6b87-438a-910d-1a2bf17d8215')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (94, 0, 0, 0, 36, N'IsList', N'Parameter wird als List<> generiert', 1, N'Parameter wird als List&lt;&gt; generiert', NULL, NULL, NULL, 13, N'ec4d5dbc-f738-4eb3-a663-2328d0baa79c')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (95, 0, 0, 0, 36, N'IsReturnParameter', N'Es darf nur ein Return Parameter angegeben werden', 1, N'Es darf nur ein Return Parameter angegeben werden', NULL, NULL, NULL, 13, N'ba5bfb2e-f679-41b2-93ef-fc795e2e92d4')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (96, 1, 1, 1, 10, N'Parameter', N'Parameter der Methode', 1, N'Parameter der Methode', NULL, NULL, N'Main', 6, N'8dace0a9-6db1-458d-b054-ace4a3d906c2')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (97, 0, 0, 0, 42, N'DataType', N'Kistl-Typ des Parameters', 1, N'Kistl-Typ des Parameters', NULL, NULL, NULL, 5, N'9bd64c60-7282-47f0-8069-528a175fcc92')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (98, 1, 0, 0, 43, N'Assembly', N'Assembly des CLR Objektes, NULL für Default Assemblies', 1, N'Assembly des CLR Objektes, NULL für Default Assemblies', NULL, NULL, NULL, 5, N'304b34ac-b581-40ce-826c-0fc0cab93bb6')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (99, 0, 0, 0, 43, N'FullTypeName', N'Name des CLR Datentypen', 1, N'Name des CLR Datentypen', NULL, NULL, NULL, 9, N'7aa087db-ef36-4a93-9bc8-e0e34c9d4d4b')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (100, 0, 0, 0, 46, N'Enumeration', N'Übergeordnete Enumeration', 1, N'Übergeordnete Enumeration', NULL, NULL, NULL, 5, N'115c3bfb-72fd-46f2-81fe-74ce1cfa1874')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (103, 1, 1, 0, 45, N'EnumerationEntries', N'Einträge der Enumeration', 1, N'Einträge der Enumeration', NULL, NULL, NULL, 6, N'1619c8a7-b969-4c05-851c-7a2545cda484')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (104, 0, 0, 0, 47, N'Enumeration', N'Enumeration der Eigenschaft', 1, N'Enumeration der Eigenschaft', NULL, NULL, NULL, 5, N'1144c061-3610-495f-b8b4-951058bb0c23')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (105, 1, 1, 0, 2, N'ImplementsInterfaces', N'Interfaces der Objektklasse', 1, N'Interfaces der Objektklasse', NULL, NULL, N'DataModel', 6, N'a9ec04c2-0807-4d6c-a96a-824d13e5c571')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (107, 0, 0, 0, 48, N'StringProp', N'String Property für das Testinterface', 5, N'String Property für das Testinterface', NULL, NULL, NULL, 9, N'dd027211-bc39-4279-b567-47ee7f0de22f')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (108, 0, 0, 0, 48, N'ObjectProp', N'Objektpointer für das Testinterface', 5, N'Objektpointer für das Testinterface', NULL, NULL, NULL, 5, N'8abd5f26-3d12-4b48-8a2b-16adbb97d845')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (109, 0, 0, 0, 51, N'StringProp', N'String Property', 5, N'String Property', NULL, NULL, NULL, 9, N'c9a3769e-7a53-4e1d-b894-72dc1b4e9aea')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (110, 0, 0, 0, 51, N'TestEnumProp', N'Test Enumeration Property', 5, N'Test Enumeration Property', NULL, NULL, NULL, 25, N'89470dda-4ac6-4bb4-9221-d16f80f8d95a')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (111, 0, 0, 0, 48, N'TestEnumProp', N'Test Enum Property', 5, N'Test Enum Property', NULL, NULL, NULL, 25, N'657b719f-dcda-4308-9587-4e2c10e7b60f')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (112, 0, 0, 0, 51, N'ObjectProp', N'testtest', 5, N'testtest', NULL, NULL, NULL, 5, N'e93b3fc2-2fc9-4577-9a93-a51ed2a4190f')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (113, 0, 0, 0, 52, N'Platform', N'The Toolkit used by this Renderer', 4, N'The Toolkit used by this Renderer', NULL, NULL, NULL, 24, N'83ab9087-52a5-400d-9e41-bd46fb5e7957')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (114, 0, 0, 0, 54, N'Assembly', N'The assembly containing the Control', 4, N'The assembly containing the Control', NULL, NULL, NULL, 5, N'869e64d3-8dd1-4313-beb5-0388bfd2c990')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (115, 0, 0, 0, 54, N'ClassName', N'The name of the class implementing this Control', 4, N'The name of the class implementing this Control', NULL, NULL, NULL, 9, N'27aab79e-a059-4a5d-a9d1-df2e4755c370')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (116, 0, 0, 0, 54, N'IsContainer', N'Whether or not this Control can contain other Controls', 4, N'Whether or not this Control can contain other Controls', NULL, NULL, NULL, 13, N'90f6cf7e-2da3-4d73-9684-157f6a614bf3')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (117, 0, 0, 0, 54, N'Platform', N'The toolkit of this Control.', 4, N'The toolkit of this Control.', NULL, NULL, NULL, 24, N'f6e9f880-7cec-4ddc-8f81-3ebda8838c81')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (118, 0, 0, 0, 54, N'ControlType', N'The type of Control of this implementation', 4, N'The type of Control of this implementation', NULL, NULL, NULL, 23, N'd3c3e9ac-59da-42c0-9865-8789353ab4e0')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (119, 0, 0, 0, 2, N'IsSimpleObject', N'Setting this to true marks the instances of this class as "simple." At first this will only mean that they''ll be displayed inline.', 4, N'Setting this to true marks the instances of this class as &quot;simple.&quot; At first this will only mean that they''ll be displayed inline.', NULL, NULL, N'DataModel', 13, N'edc853d3-0d02-4492-9159-c548c7713e9b')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (124, 0, 0, 0, 10, N'IsDisplayable', N'Shows this Method in th GUI', 4, N'Shows this Method in th GUI', NULL, NULL, N'Summary', 13, N'5ac29d6a-9dec-4d88-8f66-59ee7a139f4d')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (126, 1, 0, 0, 51, N'MyIntProperty', N'test', 5, N'test', NULL, NULL, NULL, 10, N'29c0242b-cd1c-42b4-8ca0-be0a209afcbf')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (127, 1, 0, 0, 63, N'Number', N'Enter a Number', 5, N'Enter a Number', NULL, NULL, NULL, 9, N'd2f60356-2244-46f3-b0a0-2dcfd76005bc')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (128, 1, 0, 0, 63, N'AreaCode', N'Enter Area Code', 5, N'Enter Area Code', NULL, NULL, NULL, 9, N'7921bed7-1671-44cc-8f2e-cf9d5418641e')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (129, 0, 0, 0, 64, N'StructDefinition', N'Definition of this Struct', 1, N'Definition of this Struct', NULL, NULL, NULL, 5, N'0d78c157-c106-4728-9af2-7992da7c935d')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (130, 0, 0, 0, 58, N'PersonName', N'Persons Name', 5, N'Person&apos;s Name', NULL, NULL, NULL, 9, N'bd501a1f-a0a3-4ddf-b6c2-8fdc9ffdfabd')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (131, 1, 0, 0, 58, N'PhoneNumberMobile', N'Mobile Phone Number', 5, N'Mobile Phone Number', NULL, NULL, NULL, NULL, N'c9788c6f-e59b-4b4e-9222-1cc726961b20')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (132, 1, 0, 0, 58, N'PhoneNumberOffice', N'Office Phone Number', 5, N'Office Phone Number', NULL, NULL, NULL, NULL, N'f2e60b5a-767c-4369-b178-3397b22a3f7e')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (133, 1, 0, 0, 58, N'Birthday', N'Happy Birthday!', 5, N'Happy Birthday!', NULL, 12, NULL, 12, N'bdcf86b8-4c47-4c50-b340-d9323344c7f0')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (135, 0, 0, 0, 46, N'Value', N'The CLR value of this entry', 1, N'The CLR value of this entry', NULL, NULL, NULL, 10, N'2fea1d2e-d5ed-457f-9828-4df8c3d3d3aa')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (136, 0, 0, 0, 46, N'Name', N'CLR name of this entry', 1, N'CLR name of this entry', NULL, NULL, NULL, 9, N'1c1e497b-294f-442e-8793-478b298d4aba')
GO
print 'Processed 100 total records'
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (137, 0, 0, 0, 66, N'ControlType', N'which controls are handled by this Presenter', 4, N'which controls are handled by this Presenter', NULL, NULL, NULL, 23, N'bfb33f62-d829-4d17-b08e-363228c848a6')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (138, 0, 0, 0, 66, N'PresenterAssembly', N'Where to find the implementation of the Presenter', 4, N'Where to find the implementation of the Presenter', NULL, NULL, NULL, 5, N'69179c7a-4e68-4c72-b238-18c2c63fb9ae')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (139, 0, 0, 0, 66, N'PresenterTypeName', N'The CLR namespace and class name of the Presenter', 4, N'The CLR namespace and class name of the Presenter', NULL, NULL, NULL, 9, N'a0752a9c-80d3-44ea-812a-eb3f3de95ebb')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (147, 1, 0, 0, 66, N'DataAssembly', N'The Assembly of the Data Type', 4, N'The Assembly of the Data Type', NULL, NULL, NULL, 5, N'2606a688-e2b8-4340-932d-732c7c139261')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (148, 0, 0, 0, 66, N'DataTypeName', N'The CLR namespace and class name of the Data Type', 4, N'The CLR namespace and class name of the Data Type', NULL, NULL, NULL, 9, N'427389c4-1b88-44e9-90aa-6ff27218d558')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (149, 0, 0, 0, 67, N'Description', N'A short description of the utility of this visual', 4, N'A short description of the utility of this visual', NULL, NULL, NULL, 9, N'8d3b7c91-2bbf-4dcf-bc37-318dc0fda92d')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (150, 0, 0, 0, 67, N'ControlType', N'Which visual is represented here', 4, N'Which visual is represented here', NULL, NULL, NULL, 23, N'bdeb28ac-665e-4bb6-8f7b-0ae983d77d56')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (151, 1, 1, 0, 67, N'Children', N'if this is a container, here are the visually contained/controlled children of this Visual', 4, N'if this is a container, here are the visually contained/controlled children of this Visual', NULL, NULL, NULL, 6, N'9f69c3bd-e274-4639-b30c-8d2a9599917b')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (152, 1, 0, 0, 67, N'Property', N'The Property to display', 4, N'The Property to display', NULL, NULL, NULL, 5, N'a432e3ff-61ed-4726-8559-f34516181065')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (153, 0, 0, 0, 67, N'Method', N'The Method whose return value shoud be displayed', 4, N'The Method whose return value shoud be displayed', NULL, NULL, NULL, 5, N'0b55b2ba-3ac0-4631-8a73-1e8846c8e9b1')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (154, 0, 0, 0, 68, N'DisplayName', N'a short name to identify this Template to the user', 4, N'a short name to identify this Template to the user', NULL, NULL, NULL, 9, N'4fc51781-b0fe-495c-91a1-90e484345515')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (155, 0, 0, 0, 68, N'VisualTree', N'The visual representation of this Template', 4, N'The visual representation of this Template', NULL, NULL, NULL, 5, N'5d2880a4-716a-4bdc-aaa9-379c006e7ed4')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (162, 0, 0, 0, 68, N'DisplayedTypeFullName', N'FullName of the Type that is displayed with this Template', 4, N'FullName of the Type that is displayed with this Template', NULL, NULL, NULL, 9, N'4b683aa1-45a9-4c5e-80e7-0ff30f5b798c')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (163, 0, 0, 0, 68, N'DisplayedTypeAssembly', N'Assembly of the Type that is displayed with this Template', 4, N'Assembly of the Type that is displayed with this Template', NULL, NULL, NULL, 5, N'c81105da-97e4-4685-af88-792c68e55a17')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (164, 1, 1, 0, 67, N'ContextMenu', N'The context menu for this Visual', 4, N'The context menu for this Visual', NULL, NULL, NULL, 6, N'7b18f26e-0f3f-4554-b469-1029bd4ca10b')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (165, 1, 1, 0, 68, N'Menu', N'The main menu for this Template', 4, N'The main menu for this Template', NULL, NULL, NULL, 6, N'5e9612d5-019a-416b-a2e2-dfc9674a50f6')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (167, 1, 0, 0, 69, N'Reason', N'The reason of this constraint', 1, N'The reason of this constraint', NULL, NULL, NULL, 9, N'49f759b3-de60-4cee-be06-c712e901c24e')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (168, 0, 0, 0, 71, N'Max', N'The biggest value accepted by this constraint', 1, N'The biggest value accepted by this constraint', NULL, NULL, NULL, 10, N'dff43695-5b93-4378-a01d-94a82d29dcef')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (169, 0, 0, 0, 71, N'Min', N'The smallest value accepted by this constraint', 1, N'The smallest value accepted by this constraint', NULL, NULL, NULL, 10, N'8afdbf66-c979-4c09-8872-1a44aa1dbf72')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (170, 1, 1, 0, 7, N'Constraints', N'The list of constraints applying to this Property', 1, N'The list of constraints applying to this Property', NULL, NULL, N'DataModel', 6, N'fd8f14da-e647-48cb-8593-3a30984f5c96')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (171, 0, 0, 0, 69, N'ConstrainedProperty', N'The property to be constrained', 1, N'The property to be constrained', NULL, NULL, NULL, 5, N'438b9307-fb40-4afe-a66f-a5762c41e14b')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (172, 0, 0, 0, 73, N'MaxLength', N'The maximal length of this StringProperty', 1, N'The maximal length of this StringProperty', NULL, NULL, NULL, 10, N'17aa679d-72d0-480e-9bd9-b37f4eba1d68')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (173, 0, 0, 0, 73, N'MinLength', N'The minimal length of this StringProperty', 1, N'The minimal length of this StringProperty', NULL, NULL, NULL, 10, N'8d3e24f7-c8c8-4bb3-931e-d0452e7ee5b6')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (174, 0, 0, 0, 2, N'IsFrozenObject', N'if true then all Instances appear in FozenContext.', 1, N'if true then all Instances appear in FozenContext.', NULL, NULL, N'Physical', 13, N'13c33710-ea02-4621-ad50-294a1f36b07d')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (175, 1, 0, 0, 33, N'Description', N'Description of this DataType', 1, N'Description of this DataType', NULL, NULL, N'Description', 9, N'2cffd4f2-cb84-4f39-9bd1-19fd2e160bad')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (176, 1, 0, 0, 7, N'Description', N'Description of this Property', 1, N'Description of this Property', NULL, NULL, N'Description', 9, N'5905ae85-6a44-4dbd-9752-49cac467d3cd')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (177, 1, 0, 0, 36, N'Description', N'Description of this Parameter', 1, N'Description of this Parameter', NULL, NULL, NULL, 9, N'20668b5a-ecaa-4531-81d8-6e50c9858ff0')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (178, 1, 0, 0, 46, N'Description', N'Description of this Enumeration Entry', 1, N'Description of this Enumeration Entry', NULL, NULL, NULL, 9, N'3366c523-0593-4a29-978f-5ac8a4f15eca')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (179, 1, 0, 0, 18, N'Description', N'Description of this Module', 1, N'Description of this Module', NULL, NULL, N'Main', 9, N'79408b86-1731-42ad-89b2-ed5c567fbf8a')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (180, 1, 0, 0, 10, N'Description', N'Description of this Method', 1, N'Description of this Method', NULL, NULL, N'Main', 9, N'cbf27789-e98f-4d9f-88e9-f3ff89e8c952')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (183, 1, 0, 0, 77, N'Storage', N'Storagetype for 1:1 Relations', 1, N'Storagetype for 1:1 Relations. Must be null for non 1:1 Relations.', NULL, NULL, NULL, 22, N'ba4f10fd-f7cf-4237-93a6-734e7e5c6b8a')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (184, 1, 0, 0, 77, N'Description', N'Description of this Relation', 1, N'Description of this Relation', NULL, NULL, NULL, 9, N'56948ee3-f1a7-44c3-956a-9baa863c5092')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (204, 0, 0, 0, 7, N'IsIndexed', N'Whether or not a list-valued property has a index', 1, N'Whether or not a list-valued property has a index', NULL, NULL, N'DataModel', 13, N'b62c7fee-bb67-46a6-b481-81554e788aa0')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (205, 0, 0, 0, 79, N'FullName', N'', 1, N'', NULL, NULL, NULL, 9, N'e418e513-e623-4a8f-bcbd-8572a29b7c82')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (206, 0, 0, 0, 79, N'Assembly', N'The assembly containing the referenced Type.', 1, N'The assembly containing the referenced Type.', NULL, NULL, NULL, 5, N'885bfa97-3d43-48bb-a0aa-1049298714ff')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (207, 1, 1, 1, 79, N'GenericArguments', N'list of type arguments', 1, N'list of type arguments', NULL, NULL, NULL, 6, N'443e3370-b1f4-46e8-9779-1a8d9ba1c8a6')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (208, 0, 0, 0, 30, N'Implementor', N'The Type implementing this invocation', 1, N'The Type implementing this invocation', NULL, NULL, N'Summary', 5, N'521868ca-503c-409e-bfa5-2e9d887d0a19')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (212, 1, 0, 0, 2, N'DefaultModel', N'The default model to use for the UI', 4, N'The default model to use for the UI', NULL, NULL, N'GUI', 5, N'db204b39-bfd3-4988-bd7a-250410b8acf1')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (213, 1, 0, 0, 77, N'A', N'The A-side of this Relation.', 1, N'The A-side of this Relation.', NULL, NULL, NULL, 5, N'd4429d3c-8fd1-468e-88d5-17abfd658d04')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (214, 1, 0, 0, 77, N'B', N'The B-side of this Relation.', 1, N'The B-side of this Relation.', NULL, NULL, NULL, 5, N'20331803-079e-471e-ae45-f4d004aef48e')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (215, 0, 0, 0, 82, N'Type', N'Specifies which type this End of the relation has. MUST NOT be null.', 1, N'Specifies which type this End of the relation has. MUST NOT be null.', NULL, NULL, NULL, 5, N'd4bfc4e0-6b57-49f0-91fd-b0de428484e0')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (216, 0, 0, 0, 82, N'RoleName', N'This end''s role name in the relation', 1, N'This end&apos;s role name in the relation', NULL, NULL, NULL, 9, N'b32efbfc-5212-44e7-b25f-f4724b63cbee')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (217, 0, 0, 0, 82, N'Role', N'Which RelationEndRole this End has', 1, N'Which RelationEndRole this End has', NULL, NULL, NULL, 10, N'377a7a10-b475-4259-8f35-a90d956f9331')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (218, 1, 0, 0, 82, N'Navigator', N'The ORP to navigate FROM this end of the relation. MAY be null.', 1, N'The ORP to navigate FROM this end of the relation. MAY be null.', NULL, NULL, NULL, 5, N'6b25eaab-f746-47ec-a91e-f92ec6fccada')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (219, 0, 0, 0, 82, N'Multiplicity', N'Specifies how many instances may occur on this end of the relation.', 1, N'Specifies how many instances may occur on this end of the relation.', NULL, NULL, NULL, 21, N'cdbcada8-4deb-4c4f-a7a4-24716b0a0ccd')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (220, 0, 0, 0, 82, N'HasPersistentOrder', N'Is true, if this RelationEnd persists the order of its elements', 1, N'Is true, if this RelationEnd persists the order of its elements', NULL, NULL, NULL, 13, N'edd8d122-7b58-4bbb-bf00-33caa8b69cc2')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (222, 0, 0, 0, 14, N'RelationEnd', NULL, 1, N'The RelationEnd describing this Property', NULL, NULL, NULL, 5, N'63ba109d-92c6-4ced-980b-0a52aabfaec0')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (223, 1, 0, 0, 82, N'BParent', NULL, 1, N'The Relation using this RelationEnd as B', NULL, NULL, NULL, 5, N'521ea0ba-ae3b-4a60-ae28-f366b3ee78f1')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (224, 1, 0, 0, 82, N'AParent', NULL, 1, N'The Relation using this RelationEnd as A', NULL, NULL, NULL, 5, N'dd6057d0-78bb-4242-9670-ec6c09bd4d92')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (225, 1, 0, 0, 7, N'CategoryTags', NULL, 1, N'A space separated list of category names containing this Property', NULL, NULL, N'Description, GUI, Summary', 9, N'13418a59-a804-4bc7-88ed-4d3509940301')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (226, 0, 0, 0, 83, N'PresentedModelDescriptor', NULL, 4, N'The PresentableModel usable by this View', NULL, NULL, N'Main', 5, N'ca6bc98d-ce9c-43c1-ba6b-93cb99c2e3e0')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (227, 0, 0, 0, 83, N'ControlRef', NULL, 4, N'The control implementing this View', NULL, NULL, N'Main', 5, N'eff6276d-975b-4a0d-bd3c-ad76af2189c3')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (228, 0, 0, 0, 83, N'Toolkit', NULL, 4, N'Which toolkit provides this View', NULL, NULL, N'Summary', 24, N'2a798728-d79d-471f-be51-1f488beb8dc1')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (229, 0, 0, 0, 83, N'VisualType', NULL, 4, N'The visual type of this View', NULL, NULL, N'Summary', 23, N'af9c1a03-ce46-4719-96c0-c38287d26ac0')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (231, 0, 0, 0, 85, N'PresentableModelRef', NULL, 4, N'The described CLR class&apos; reference', NULL, NULL, N'Main', 5, N'554288d1-f5f4-4b22-908b-01525a1d0f9b')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (232, 0, 0, 0, 85, N'Description', NULL, 4, N'describe this PresentableModel', NULL, NULL, N'Summary,Main', 9, N'93e25648-50f9-40d8-8753-e5dadab68e1d')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (233, 0, 0, 0, 85, N'DefaultVisualType', NULL, 4, N'The default visual type used for this PresentableModel', NULL, NULL, N'Summary,Main', 23, N'2ab3364a-561c-40f3-a83a-731ce0f1e2de')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (234, 0, 0, 0, 2, N'DefaultPresentableModelDescriptor', NULL, 4, N'The default PresentableModel to use for this ObjectClass', NULL, NULL, N'Main', 5, N'11adedb9-d32a-4da9-b986-0534e65df760')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (235, 0, 0, 0, 79, N'Parent', NULL, 1, N'The TypeRef of the BaseClass of the referenced Type', NULL, NULL, NULL, 5, N'f7ed21a0-9a41-40eb-b3ab-b35591f2edd7')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (236, 0, 0, 0, 7, N'ValueModelDescriptor', NULL, 4, N'The PresentableModel to use for values of this Property', NULL, NULL, N'Main', 5, N'84e0996a-081f-4a17-a34d-54cf23991301')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (237, 0, 0, 0, 20, N'Notes', NULL, 3, N'Space for notes', NULL, NULL, N'Main', 9, N'79c8188d-d8e2-41b7-82c9-08f384fd6b68')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (238, 0, 0, 0, 86, N'From', NULL, 3, N'Point in time when the presence started.', NULL, 12, N'Summary', 12, N'3833e790-e2f2-43c6-b9c2-79dd4a03c8c6')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (239, 1, 0, 0, 86, N'Thru', NULL, 3, N'Point in time (inclusive) when the presence ended.', NULL, 12, N'Summary', 12, N'17dabad9-a47e-46b8-a72e-b7616af0ceae')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (244, 1, 0, 0, 86, N'Mitarbeiter', NULL, 3, N'Which employee was present.', NULL, NULL, N'Summary', 5, N'b67880d2-37b0-436f-8628-6637fbe19e31')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (245, 0, 0, 0, 87, N'Name', NULL, 3, N'A short label describing this work effort.', NULL, NULL, N'Summary', 9, N'a96df76c-c45c-4d21-8221-8c7deaac4814')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (246, 0, 0, 0, 87, N'Notes', NULL, 3, N'Space for notes', NULL, NULL, N'Main', 9, N'1744a31b-a1c3-4e7c-834c-504521240478')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (247, 0, 0, 0, 87, N'From', NULL, 3, N'Point in time when the work effort started.', NULL, 12, N'Summary', 12, N'b169f505-9b5f-4e4e-ae25-a46bc9926c87')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (248, 1, 0, 0, 87, N'Thru', NULL, 3, N'Point in time (inclusive) when the work effort ended.', NULL, 12, N'Summary', 12, N'553440f1-3b22-402b-ba5b-355f21cc31d9')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (249, 0, 0, 0, 87, N'Mitarbeiter', NULL, 3, N'Which employee effected this work effort.', NULL, NULL, N'Summary', 5, N'720f5bcf-5654-4114-8fba-f57fb7bd48ea')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (251, 0, 0, 0, 88, N'ExportGuid', N'Export Guid', 1, N'Export Guid', NULL, NULL, N'Export', 39, N'59ce9855-9e67-455f-b6fa-636c47da5ae2')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (252, 0, 0, 0, 33, N'ExportGuid', N'Export Guid', 1, N'Export Guid', NULL, NULL, N'Export', 39, N'1fdb011e-2098-4077-b5e9-dd2eeafa727c')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (253, 0, 0, 0, 7, N'ExportGuid', N'Export Guid', 1, N'Export Guid', NULL, NULL, N'Export', 39, N'ca0a099d-3f4c-4604-8303-d751e57041bb')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (254, 0, 0, 0, 36, N'ExportGuid', N'Export Guid', 1, N'Export Guid', NULL, NULL, N'Export', 39, N'74265fbf-2340-4828-82fa-cff4a0d18ffa')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (255, 0, 0, 0, 29, N'ExportGuid', N'Export Guid', 1, N'Export Guid', NULL, NULL, N'Export', 39, N'9c1ddbcf-24b9-47cb-a27d-043fc47e4002')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (256, 0, 0, 0, 69, N'ExportGuid', N'Export Guid', 1, N'Export Guid', NULL, NULL, N'Export', 39, N'8da6d02c-9d9e-4db8-91ee-24a3fd1c74e1')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (257, 0, 0, 0, 10, N'ExportGuid', N'Export Guid', 1, N'Export Guid', NULL, NULL, N'Export', 39, N'842eb3fc-3c8f-47d6-a59f-225c75ec2439')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (259, 0, 0, 0, 30, N'ExportGuid', N'Export Guid', 1, N'Export Guid', NULL, NULL, N'Export', 39, N'53e7daa2-aba7-4cd0-bab6-3c0d07648b2e')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (260, 0, 0, 0, 18, N'ExportGuid', N'Export Guid', 1, N'Export Guid', NULL, NULL, N'Export', 39, N'75e3db82-220c-474e-973a-ceb65fd8386d')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (261, 0, 0, 0, 77, N'ExportGuid', N'Export Guid', 1, N'Export Guid', NULL, NULL, N'Export', 39, N'1e600012-3b35-4dc6-af28-1f858b095a15')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (262, 0, 0, 0, 82, N'ExportGuid', N'Export Guid', 1, N'Export Guid', NULL, NULL, N'Export', 39, N'4bbe4a44-dc99-4455-9c03-ae78903fcee2')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (263, 0, 0, 0, 79, N'ExportGuid', N'Export Guid', 1, N'Export Guid', NULL, NULL, N'Export', 39, N'48430be7-e17f-48ad-ac8b-7f9cb5341318')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (264, 0, 0, 0, 33, N'ShowNameInLists', NULL, 4, NULL, NULL, NULL, N'Summary,GUI', 13, N'00000000-0000-0000-0000-000000000000')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (265, 0, 0, 0, 33, N'ShowIconInLists', NULL, 4, NULL, NULL, NULL, N'Summary,GUI', 13, N'00000000-0000-0000-0000-000000000000')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (266, 0, 0, 0, 33, N'ShowIdInLists', NULL, 4, NULL, NULL, NULL, N'Summary,GUI', 13, N'00000000-0000-0000-0000-000000000000')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (267, 0, 0, 0, 90, N'Schema', NULL, 1, N'XML descriptor of the current schema', NULL, NULL, N'Main', 9, N'00000000-0000-0000-0000-000000000000')
INSERT [dbo].[Properties] ([ID], [IsNullable], [IsList], [IsIndexed], [fk_ObjectClass], [PropertyName], [AltText], [fk_Module], [Description], [fk_ObjectClass_pos], [fk_Module_pos], [CategoryTags], [fk_ValueModelDescriptor], [ExportGuid]) VALUES (268, 0, 0, 0, 90, N'Version', NULL, 1, N'Version number of this schema', NULL, NULL, N'Summary', 10, N'00000000-0000-0000-0000-000000000000')
SET IDENTITY_INSERT [dbo].[Properties] OFF
/****** Object:  Table [dbo].[StringParameters]    Script Date: 05/22/2009 17:30:29 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[StringParameters] ([ID]) VALUES (1)
INSERT [dbo].[StringParameters] ([ID]) VALUES (3)
INSERT [dbo].[StringParameters] ([ID]) VALUES (5)
INSERT [dbo].[StringParameters] ([ID]) VALUES (14)
INSERT [dbo].[StringParameters] ([ID]) VALUES (23)
INSERT [dbo].[StringParameters] ([ID]) VALUES (30)
INSERT [dbo].[StringParameters] ([ID]) VALUES (33)
/****** Object:  Table [dbo].[ObjectParameters]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObjectParameters]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ObjectParameters](
	[ID] [int] NOT NULL,
	[fk_DataType] [int] NOT NULL,
	[fk_DataType_pos] [int] NULL,
 CONSTRAINT [PK_ObjectParameters] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[ObjectParameters] ([ID], [fk_DataType], [fk_DataType_pos]) VALUES (10, 19, NULL)
INSERT [dbo].[ObjectParameters] ([ID], [fk_DataType], [fk_DataType_pos]) VALUES (26, 36, NULL)
INSERT [dbo].[ObjectParameters] ([ID], [fk_DataType], [fk_DataType_pos]) VALUES (27, 10, NULL)
INSERT [dbo].[ObjectParameters] ([ID], [fk_DataType], [fk_DataType_pos]) VALUES (36, 2, NULL)
/****** Object:  Table [dbo].[ObjectReferenceProperties]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObjectReferenceProperties]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ObjectReferenceProperties](
	[ID] [int] NOT NULL,
	[fk_ReferenceObjectClass] [int] NOT NULL,
	[fk_RightOf] [int] NULL,
	[fk_LeftOf] [int] NULL,
	[fk_ReferenceObjectClass_pos] [int] NULL,
 CONSTRAINT [PK_ObjectReferenceProperties] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (7, 7, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (8, 33, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (14, 4, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (19, 3, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (21, 3, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (22, 6, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (25, 2, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (27, 2, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (29, 33, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (31, 10, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (44, 33, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (45, 18, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (46, 2, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (49, 6, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (51, 3, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (64, 26, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (67, 19, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (69, 27, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (70, 18, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (72, 18, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (73, 18, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (74, 10, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (78, 18, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (79, 33, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (80, 30, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (81, 30, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (82, 29, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (86, 6, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (92, 10, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (96, 36, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (97, 33, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (98, 29, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (100, 45, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (103, 46, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (104, 45, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (105, 44, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (108, 26, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (112, 26, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (114, 29, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (129, 62, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (138, 29, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (147, 29, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (151, 67, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (152, 7, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (153, 10, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (155, 67, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (163, 29, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (164, 67, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (165, 67, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (170, 69, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (171, 7, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (206, 29, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (207, 79, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (208, 79, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (212, 79, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (213, 82, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (214, 82, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (215, 2, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (218, 14, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (222, 82, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (223, 77, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (224, 77, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (226, 85, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (227, 79, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (231, 79, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (234, 85, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (235, 79, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (236, 85, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (244, 6, NULL, NULL, NULL)
INSERT [dbo].[ObjectReferenceProperties] ([ID], [fk_ReferenceObjectClass], [fk_RightOf], [fk_LeftOf], [fk_ReferenceObjectClass_pos]) VALUES (249, 6, NULL, NULL, NULL)
/****** Object:  Table [dbo].[ObjectClasses_ImplementsInterfacesCollection]    Script Date: 05/22/2009 17:30:29 ******/
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
	[ExportGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ObjectClasses_ImplementsInterfacesCollection] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[ObjectClasses_ImplementsInterfacesCollection] ON
INSERT [dbo].[ObjectClasses_ImplementsInterfacesCollection] ([ID], [fk_ObjectClass], [fk_ImplementsInterfaces], [ExportGuid]) VALUES (1, 51, 48, N'83c778d4-d0f7-473d-82c4-4fed100cee73')
INSERT [dbo].[ObjectClasses_ImplementsInterfacesCollection] ([ID], [fk_ObjectClass], [fk_ImplementsInterfaces], [ExportGuid]) VALUES (2, 33, 88, N'a6a4d2e2-af4f-4e89-91fb-d45ce9ad1398')
INSERT [dbo].[ObjectClasses_ImplementsInterfacesCollection] ([ID], [fk_ObjectClass], [fk_ImplementsInterfaces], [ExportGuid]) VALUES (3, 7, 88, N'c9be36f4-7b5b-4091-bd44-69d9b93d760b')
INSERT [dbo].[ObjectClasses_ImplementsInterfacesCollection] ([ID], [fk_ObjectClass], [fk_ImplementsInterfaces], [ExportGuid]) VALUES (4, 36, 88, N'88bffda5-ad38-481e-8228-403409cf4c51')
INSERT [dbo].[ObjectClasses_ImplementsInterfacesCollection] ([ID], [fk_ObjectClass], [fk_ImplementsInterfaces], [ExportGuid]) VALUES (5, 29, 88, N'cd8ee755-9a26-4ca9-b7ce-04ae527570bb')
INSERT [dbo].[ObjectClasses_ImplementsInterfacesCollection] ([ID], [fk_ObjectClass], [fk_ImplementsInterfaces], [ExportGuid]) VALUES (6, 69, 88, N'23b3b971-c114-46b2-8820-0761c1b1ab03')
INSERT [dbo].[ObjectClasses_ImplementsInterfacesCollection] ([ID], [fk_ObjectClass], [fk_ImplementsInterfaces], [ExportGuid]) VALUES (7, 10, 88, N'bcacb5d6-ea87-4043-bab8-72e12172f208')
INSERT [dbo].[ObjectClasses_ImplementsInterfacesCollection] ([ID], [fk_ObjectClass], [fk_ImplementsInterfaces], [ExportGuid]) VALUES (9, 30, 88, N'650cae93-fece-4cfc-94a5-b2bb164d4479')
INSERT [dbo].[ObjectClasses_ImplementsInterfacesCollection] ([ID], [fk_ObjectClass], [fk_ImplementsInterfaces], [ExportGuid]) VALUES (10, 18, 88, N'1194d8e6-4055-47d0-8ae7-3b98e81af860')
INSERT [dbo].[ObjectClasses_ImplementsInterfacesCollection] ([ID], [fk_ObjectClass], [fk_ImplementsInterfaces], [ExportGuid]) VALUES (11, 77, 88, N'26c296be-793b-4503-a3ab-f1bb544d17d1')
INSERT [dbo].[ObjectClasses_ImplementsInterfacesCollection] ([ID], [fk_ObjectClass], [fk_ImplementsInterfaces], [ExportGuid]) VALUES (12, 82, 88, N'e993dd36-7296-4e38-bd70-f0a8016066ea')
INSERT [dbo].[ObjectClasses_ImplementsInterfacesCollection] ([ID], [fk_ObjectClass], [fk_ImplementsInterfaces], [ExportGuid]) VALUES (13, 79, 88, N'45dd167a-e3a4-456c-aa86-bd7b1667a1a2')
SET IDENTITY_INSERT [dbo].[ObjectClasses_ImplementsInterfacesCollection] OFF
/****** Object:  Table [dbo].[RelationEnds]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RelationEnds]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RelationEnds](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_Type] [int] NOT NULL,
	[RoleName] [nvarchar](200) NOT NULL,
	[Role] [int] NOT NULL,
	[fk_Navigator] [int] NULL,
	[Multiplicity] [int] NOT NULL,
	[HasPersistentOrder] [bit] NOT NULL,
	[fk_Type_pos] [int] NULL,
	[fk_Navigator_pos] [int] NULL,
	[ExportGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_RelationEnds] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[RelationEnds] ON
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (3, 33, N'ObjectClass', 1, 7, 2, 0, NULL, NULL, N'801c85f9-9463-4298-a2b9-ce237b307707')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (4, 7, N'Properties', 2, 8, 3, 0, NULL, NULL, N'f6a0541f-8f37-4599-ab35-15b6360185b0')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (9, 3, N'Projekt', 1, 14, 1, 0, NULL, NULL, N'e8b582ca-641f-4457-963c-299d0df7cb96')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (10, 4, N'Tasks', 2, 19, 3, 0, NULL, NULL, N'31d885e4-e1f8-48a0-bc56-e247ae0019bc')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (11, 3, N'Projekte', 1, 22, 3, 1, NULL, NULL, N'6d38fd11-4fab-4141-8cf5-79ff02742452')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (12, 6, N'Mitarbeiter', 2, 21, 3, 1, NULL, NULL, N'90d8ea3f-bcd8-4af9-a563-d66c18feb061')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (13, 2, N'BaseObjectClass', 1, 27, 1, 0, NULL, NULL, N'5df5018b-da33-4372-876c-7c8e63b0f8ec')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (14, 2, N'SubClasses', 2, 25, 3, 0, NULL, NULL, N'74b66243-dc52-44b2-bae6-705a0757f81f')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (15, 33, N'ObjectClass', 1, 31, 2, 0, NULL, NULL, N'57c812b2-52f2-4263-881c-27027befe397')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (16, 10, N'Methods', 2, 29, 3, 0, NULL, NULL, N'27c2ae21-81da-4d4f-a00f-0e1fcf73837b')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (17, 18, N'Module', 1, 44, 2, 0, NULL, NULL, N'2b8dbd1c-260e-4015-a6a3-6ea06ae7e111')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (18, 33, N'DataTypes', 2, 45, 3, 0, NULL, NULL, N'8b2ca723-0ce5-43b3-9ee0-d09c7edb776c')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (19, 14, N'ObjectReferenceProperty', 1, 46, 3, 0, NULL, NULL, N'3976cd56-ce89-4993-a012-88fd654b604c')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (20, 2, N'ReferenceObjectClass', 2, NULL, 1, 0, NULL, NULL, N'66d2e950-ca23-4bbf-8a57-5b61d3eabd38')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (23, 19, N'Auftrag', 1, 49, 3, 0, NULL, NULL, N'1427a21f-b4da-43c1-bcb8-71bd6a27960e')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (24, 6, N'Mitarbeiter', 2, NULL, 1, 0, NULL, NULL, N'e51cfabb-1917-446e-886b-45bd44057fe9')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (25, 3, N'Projekt', 1, 67, 1, 0, NULL, NULL, N'a2fb0f1d-16df-459e-81af-18e929ed618e')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (26, 19, N'Auftraege', 2, 51, 3, 0, NULL, NULL, N'02072e7c-b6be-4f1d-af7d-bf8e8231ebaf')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (33, 19, N'Auftrag', 1, 64, 3, 0, NULL, NULL, N'62743958-e7ae-4110-a16c-cc6e4ae795b9')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (34, 26, N'Kunde', 2, NULL, 1, 0, NULL, NULL, N'446fc236-8585-4ff2-b09a-d5bd581ba8e1')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (35, 33, N'DataType', 1, 69, 3, 0, NULL, NULL, N'8f2957f3-a431-4c8d-8be6-f4ab81de8993')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (36, 27, N'DefaultIcon', 2, NULL, 1, 0, NULL, NULL, N'689746b0-9408-45f9-b982-12688144b340')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (37, 18, N'Module', 1, 82, 2, 0, NULL, NULL, N'875e6c41-06fe-4a58-965a-3cc04ac393a4')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (38, 29, N'Assemblies', 2, 70, 3, 0, NULL, NULL, N'bfec0668-c1ce-45f7-97bc-2ae526ce5e09')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (39, 7, N'BaseProperty', 1, 72, 3, 0, NULL, NULL, N'3ecc43dd-f231-4b6d-af16-ee14f684105c')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (40, 18, N'Module', 2, NULL, 1, 0, NULL, NULL, N'ecad7625-4c0a-49de-a284-0e4161d6bf58')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (41, 10, N'Method', 1, 73, 3, 0, NULL, NULL, N'c5fd0dbb-dc82-4b64-8a27-3f638eb26061')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (42, 18, N'Module', 2, NULL, 1, 0, NULL, NULL, N'ba29e8bc-527d-4c05-a3fd-f4cc0f90fc55')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (43, 10, N'Method', 1, 81, 2, 0, NULL, NULL, N'5da26df1-7cd6-4a2f-bbed-a0bef3d9b399')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (44, 30, N'MethodInvokations', 2, 74, 3, 0, NULL, NULL, N'116c627e-85c0-4869-be58-eb64863f1026')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (45, 30, N'MethodInvocation', 1, 78, 3, 0, NULL, NULL, N'906a787f-bef4-4c56-82f7-e1b37496dce0')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (46, 18, N'Module', 2, NULL, 1, 0, NULL, NULL, N'1b4bf512-9cad-4c5e-8929-24319beffea6')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (47, 33, N'InvokeOnObjectClass', 1, 80, 2, 0, NULL, NULL, N'd792bdf6-f3d5-4e12-97b4-504970320a41')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (48, 30, N'MethodInvocations', 2, 79, 3, 0, NULL, NULL, N'99bb88df-9414-4e01-81f8-ff5aaebafbf5')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (49, 20, N'WorkEffortAccount', 1, 86, 3, 0, NULL, NULL, N'd89e79ac-5d52-47d4-bfff-961fed899ee3')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (50, 6, N'Mitarbeiter', 2, NULL, 3, 0, NULL, NULL, N'90cd2865-c873-4c5f-9363-9d2cf8bbcb49')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (53, 10, N'Method', 1, 96, 2, 0, NULL, NULL, N'0eae7cbd-7fbe-4ad7-82cd-319e7809587b')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (54, 36, N'Parameter', 2, 92, 3, 1, NULL, NULL, N'13be2187-e34c-4d27-8b1f-fd6281c634bf')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (55, 42, N'ObjectParameter', 1, 97, 3, 0, NULL, NULL, N'4ac67064-622c-4524-937b-9490cda09d03')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (56, 33, N'DataType', 2, NULL, 1, 0, NULL, NULL, N'1d22ec11-6b74-4d38-be2a-bdeb305460b6')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (57, 43, N'CLRObjectParameter', 1, 98, 3, 0, NULL, NULL, N'c9c59acd-23ca-4dcb-8d17-184abd807763')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (58, 29, N'Assembly', 2, NULL, 1, 0, NULL, NULL, N'687f6c35-4667-4c1d-bf31-ef47f65b28f6')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (59, 45, N'Enumeration', 1, 103, 2, 0, NULL, NULL, N'0bebb013-39f8-4689-83b8-43c0b47f682a')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (60, 46, N'EnumerationEntries', 2, 100, 3, 0, NULL, NULL, N'cbeccf82-4509-4036-a785-5400e4675945')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (61, 47, N'EnumerationProperty', 1, 104, 3, 0, NULL, NULL, N'2ab5b64a-6582-43d4-97ca-a4337c664ab5')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (62, 45, N'Enumeration', 2, NULL, 1, 0, NULL, NULL, N'2fa35c0b-660a-4b95-a241-a1bdc4a155b5')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (63, 2, N'ObjectClass', 1, 105, 3, 0, NULL, NULL, N'febffc17-2bef-4c1a-9771-0c51c729d248')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (64, 44, N'ImplementsInterfaces', 2, NULL, 3, 0, NULL, NULL, N'f377174b-c527-4e15-9156-cf3be8d8c20d')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (65, 51, N'TestObjClass', 1, 112, 3, 0, NULL, NULL, N'738de243-fdf1-43f7-8b14-4f960aa2f622')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (66, 26, N'ObjectProp', 2, NULL, 1, 0, NULL, NULL, N'e4be5fa3-b7a3-431b-a4ce-7c434ceaef2f')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (67, 54, N'ControlInfo', 1, 114, 3, 0, NULL, NULL, N'23db1005-fedb-4f7c-9494-5d4b6f2e1568')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (68, 29, N'Assembly', 2, NULL, 1, 0, NULL, NULL, N'706b0e27-3fb1-428b-b848-578cba51e6e3')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (69, 64, N'StructProperty', 1, 129, 3, 0, NULL, NULL, N'f03a1d89-0f51-4119-a4b0-e2420abc2d83')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (70, 62, N'StructDefinition', 2, NULL, 1, 0, NULL, NULL, N'e97e8a8e-37db-48b4-811b-29b1b6cfbe75')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (71, 66, N'PresenterInfo', 1, 138, 3, 0, NULL, NULL, N'f5d02eaf-d1b4-4de4-b504-92eb258324c5')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (72, 29, N'PresenterAssembly', 2, NULL, 1, 0, NULL, NULL, N'bf58f500-394c-4efe-8ef9-3fbe26ee658a')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (73, 66, N'PresenterInfo', 1, 147, 3, 0, NULL, NULL, N'836c9925-3c0c-45cb-a364-000eee4c1040')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (74, 29, N'DataAssembly', 2, NULL, 1, 0, NULL, NULL, N'7ac127ad-e18f-4182-8268-225f9f643177')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (75, 67, N'Visual', 1, 151, 3, 0, NULL, NULL, N'cc4262dd-defd-4992-8edb-c44b267378f9')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (76, 67, N'Children', 2, NULL, 3, 0, NULL, NULL, N'e93bd45d-a2e0-481a-9b82-60215f6f4d39')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (77, 67, N'Visual', 1, 152, 3, 0, NULL, NULL, N'8de5dbe2-e2b5-45a4-a366-10dc3b4f64cd')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (78, 7, N'Property', 2, NULL, 1, 0, NULL, NULL, N'60607525-aa94-4839-b269-8f1e02c6a478')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (79, 67, N'Visual', 1, 153, 3, 0, NULL, NULL, N'cbbef768-0d7c-4ff4-bb7d-b6c411f43925')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (80, 10, N'Method', 2, NULL, 1, 0, NULL, NULL, N'56c0f4ad-0dde-4e1c-b967-72d3c10db3bb')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (81, 68, N'Template', 1, 155, 3, 0, NULL, NULL, N'ddcefca9-0300-42b1-b706-ea7c30f2b88b')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (82, 67, N'VisualTree', 2, NULL, 1, 0, NULL, NULL, N'24be6a57-1ec4-4a00-8074-51c3da0371fb')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (83, 68, N'Template', 1, 163, 3, 0, NULL, NULL, N'546cd13d-3424-4f9b-a67a-17e57bb33212')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (84, 29, N'DisplayedTypeAssembly', 2, NULL, 1, 0, NULL, NULL, N'665ba925-1f83-42a9-983f-12b20ec53b74')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (85, 67, N'Visual', 1, 164, 3, 0, NULL, NULL, N'3f3e2222-09b0-422c-b985-199ba526d5f8')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (86, 67, N'ContextMenu', 2, NULL, 3, 0, NULL, NULL, N'fd1fb50b-2c81-47f3-a34f-b40100859468')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (87, 68, N'Template', 1, 165, 3, 0, NULL, NULL, N'94f58fb9-4fcd-4df5-9c6f-1b7abb858fae')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (88, 67, N'Menu', 2, NULL, 3, 0, NULL, NULL, N'a112ccf3-83d7-4b06-9721-3494eabfb32c')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (89, 7, N'ConstrainedProperty', 1, 170, 2, 0, NULL, NULL, N'2ce1f1f8-c515-445c-ab65-8c9bafef2f4f')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (90, 69, N'Constraints', 2, 171, 3, 0, NULL, NULL, N'f6124a61-40a3-4c73-b102-c7b379d9b1ac')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (95, 79, N'TypeRef', 1, 206, 3, 0, NULL, NULL, N'baccac38-3234-47a4-b036-37fda4a6bd06')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (96, 29, N'Assembly', 2, NULL, 1, 0, NULL, NULL, N'55a0e065-ef23-4c8b-8c1d-ace822e34be4')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (97, 79, N'TypeRef', 1, 207, 3, 0, NULL, NULL, N'74033769-cefe-41b9-b94c-bbda8a290df4')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (98, 79, N'GenericArguments', 2, NULL, 3, 1, NULL, NULL, N'3e102702-2123-4d7b-930d-0336c31a2c1a')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (99, 30, N'MethodInvocation', 1, 208, 3, 0, NULL, NULL, N'99f9c319-210f-4e33-889f-aa8cd97105dd')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (100, 79, N'Implementor', 2, NULL, 1, 0, NULL, NULL, N'23731034-1f12-4d44-b234-61cb4d70183c')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (102, 79, N'LayoutRef', 2, NULL, 1, 0, NULL, NULL, N'cfecb91c-55c8-46bf-bf48-739bc06e1f80')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (104, 79, N'ViewRef', 2, NULL, 1, 0, NULL, NULL, N'6e4bccb7-0605-4190-a3ae-8ec9b5356f2b')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (105, 2, N'ObjectClass', 1, 212, 3, 0, NULL, NULL, N'4dac8b71-7c23-41c2-8209-a2503f1d878b')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (106, 79, N'DefaultModel', 2, NULL, 1, 0, NULL, NULL, N'51def9be-1384-468a-98a4-bf1276808536')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (107, 77, N'Relation', 1, 213, 1, 0, NULL, NULL, N'c9a3cd17-11a9-487e-b9af-c8e88c9e51de')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (108, 82, N'A', 2, 224, 1, 0, NULL, NULL, N'385711ad-5e97-40e9-9d1e-27dc1a60c8c5')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (109, 77, N'Relation', 1, 214, 1, 0, NULL, NULL, N'148a4638-801e-4fe8-ac83-cea08114076e')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (110, 82, N'B', 2, 223, 1, 0, NULL, NULL, N'944c5542-0371-4839-b66c-7f15ba291f7e')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (111, 82, N'RelationEnd', 1, 215, 3, 0, NULL, NULL, N'01dbe471-358c-4d50-84e0-3ad8f3ea2756')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (112, 2, N'Type', 2, NULL, 1, 0, NULL, NULL, N'659b2d77-3395-478f-98a0-e6355cf6db6c')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (113, 82, N'RelationEnd', 1, 218, 1, 0, NULL, NULL, N'd3e1601d-da4c-4dfa-b99b-5fd43f8fecd6')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (114, 14, N'Navigator', 2, 222, 1, 0, NULL, NULL, N'7c2d4f4d-62b5-4fa6-a44f-c5a2239b9ee9')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (115, 85, N'PresentedModelDescriptor', 2, NULL, 2, 0, NULL, NULL, N'4351060a-1c59-4528-88a0-9be0efd6584c')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (116, 79, N'ControlRef', 2, NULL, 2, 0, NULL, NULL, N'fe1a5f16-7f85-4e04-8f64-9277d0461008')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (117, 83, N'View', 1, 226, 3, 0, NULL, NULL, N'f8802162-340a-4b44-971e-9080fe58714b')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (118, 83, N'View', 1, 227, 3, 0, NULL, NULL, N'cd048a20-cd26-4a12-8208-362a86ed0762')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (119, 79, N'PresentableModelRef', 2, NULL, 2, 0, NULL, NULL, N'7ebd7ba7-7574-4523-b2d9-4639561261fe')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (120, 85, N'Descriptor', 1, 231, 3, 0, NULL, NULL, N'eccb7be2-6c54-4fbb-83f4-dceed3825b65')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (121, 85, N'DefaultPresentableModelDescriptor', 2, NULL, 2, 0, NULL, NULL, N'83bf4a69-385b-486d-b0f7-c2a77dd351dc')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (122, 2, N'Presentable', 1, 234, 3, 0, NULL, NULL, N'8085fac0-4883-414b-a0d3-9680dfd9ee14')
GO
print 'Processed 100 total records'
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (123, 79, N'Parent', 2, NULL, 2, 0, NULL, NULL, N'4d133a9c-c123-4075-b484-67096b51f239')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (124, 79, N'Child', 1, 235, 3, 0, NULL, NULL, N'bb9ccce7-6efb-4ea7-8f47-2ac4feca2012')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (125, 85, N'ValueModelDescriptor', 2, NULL, 2, 0, NULL, NULL, N'b793d4e7-e0c0-4455-8606-4e9f39cd71a2')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (126, 7, N'Property', 1, 236, 3, 0, NULL, NULL, N'32fe5f8c-1e3f-4be5-a77f-40e3714dfecc')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (131, 6, N'Mitarbeiter', 2, NULL, 2, 0, NULL, NULL, N'c1ab6c56-2844-4e31-b1b1-91363f1fdeb0')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (132, 86, N'PresenceRecord', 1, 244, 3, 0, NULL, NULL, N'809b31f0-bc46-45e4-b9f4-6b7e565f78fa')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (133, 6, N'Mitarbeiter', 2, NULL, 2, 0, NULL, NULL, N'9441d9f0-d5cf-4400-9f71-dd7afe94e6ac')
INSERT [dbo].[RelationEnds] ([ID], [fk_Type], [RoleName], [Role], [fk_Navigator], [Multiplicity], [HasPersistentOrder], [fk_Type_pos], [fk_Navigator_pos], [ExportGuid]) VALUES (134, 87, N'WorkEffort', 1, 249, 3, 0, NULL, NULL, N'3a863b64-7def-4257-95b4-282bbef0af3f')
SET IDENTITY_INSERT [dbo].[RelationEnds] OFF
/****** Object:  Table [dbo].[ValueTypeProperties]    Script Date: 05/22/2009 17:30:29 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
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
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (59)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (60)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (61)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (62)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (63)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (65)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (68)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (71)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (77)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (83)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (85)
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
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (174)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (175)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (176)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (177)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (178)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (179)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (180)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (183)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (184)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (204)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (205)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (216)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (217)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (219)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (220)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (225)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (228)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (229)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (232)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (233)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (237)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (238)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (239)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (245)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (246)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (247)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (248)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (251)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (252)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (253)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (254)
GO
print 'Processed 100 total records'
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (255)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (256)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (257)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (259)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (260)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (261)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (262)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (263)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (264)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (265)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (266)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (267)
INSERT [dbo].[ValueTypeProperties] ([ID]) VALUES (268)
/****** Object:  Table [dbo].[Templates_MenuCollection]    Script Date: 05/22/2009 17:30:29 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[StructProperties]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StructProperties]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StructProperties](
	[ID] [int] NOT NULL,
	[fk_StructDefinition] [int] NOT NULL,
	[fk_StructDefinition_pos] [int] NULL,
 CONSTRAINT [PK_StructProperties] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[StructProperties] ([ID], [fk_StructDefinition], [fk_StructDefinition_pos]) VALUES (131, 63, NULL)
INSERT [dbo].[StructProperties] ([ID], [fk_StructDefinition], [fk_StructDefinition_pos]) VALUES (132, 63, NULL)
/****** Object:  Table [dbo].[Constraints]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Constraints]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Constraints](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Reason] [nvarchar](400) NULL,
	[fk_ConstrainedProperty] [int] NOT NULL,
	[fk_ConstrainedProperty_pos] [int] NULL,
	[ExportGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Constraints] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Constraints] ON
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (74, NULL, 171, NULL, N'c5120dbe-5a11-4260-a11f-45e117c58b90')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (75, NULL, 169, NULL, N'06e2ed3e-3d31-4e97-bb26-e0dab169bcd2')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (76, NULL, 168, NULL, N'0521c305-3d04-432d-aca3-129ceface8a5')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (77, NULL, 163, NULL, N'19025ac2-18f1-4ca0-bf84-046bbdbe6373')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (78, NULL, 162, NULL, N'f44c840f-6e1d-4f16-bd02-01be4b9181d4')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (79, NULL, 155, NULL, N'1087f476-cdf9-4842-a65c-61f98c4c90bd')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (80, NULL, 154, NULL, N'b1141a65-52a5-40aa-849d-5ce3a7d108e9')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (81, NULL, 153, NULL, N'0ea0d6ce-39bd-487c-ab4b-a2fe82a3288c')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (82, NULL, 150, NULL, N'fa92cbb3-7800-4466-8c7c-66084146ec8b')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (83, NULL, 149, NULL, N'82b789d5-b0e6-4ce8-8e9d-f47488e35aa9')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (84, NULL, 148, NULL, N'88a673d9-8c84-4bd7-a993-c1dcc3cb21b4')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (85, NULL, 139, NULL, N'fdd3c8e5-c91a-4b2a-9432-73d73eacc92b')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (86, NULL, 138, NULL, N'a3a7e425-5a2b-4073-98b5-b7edc738031e')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (87, NULL, 137, NULL, N'77787bf7-a7ef-4472-be61-bdf1acf14361')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (88, NULL, 136, NULL, N'4c25b2db-baf6-4324-8e8a-5e2b6456748e')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (89, NULL, 135, NULL, N'a53354a8-0430-4999-83cb-0a1c17d3f21d')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (90, NULL, 130, NULL, N'3beb0ce0-0d11-4632-9fc0-213b77c5a2fd')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (91, NULL, 129, NULL, N'719eabc5-121f-45d2-9a60-f5bf89f82274')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (92, NULL, 124, NULL, N'c22a0e29-ad3f-421a-a2b1-232abd619385')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (93, NULL, 119, NULL, N'fa02149f-23d6-4643-b4ef-67dcc46c041e')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (94, NULL, 118, NULL, N'9b0590be-5ed0-49cb-8902-45f0cb6b0337')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (95, NULL, 117, NULL, N'33a0080f-def2-4022-99ae-1050b41675eb')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (96, NULL, 116, NULL, N'5c6bd74d-c40d-45ae-af19-99e347ec7007')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (97, NULL, 115, NULL, N'480a3607-9c01-4b7e-a03a-7a741fe5474a')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (98, NULL, 114, NULL, N'c29d1423-8ac6-4026-a7bd-ca4b425d8a6e')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (99, NULL, 113, NULL, N'adfb5aee-a756-441a-a3a6-7eaeb08a19db')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (100, NULL, 112, NULL, N'd0b1d81b-55fd-4054-8c72-469a81dc7b00')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (101, NULL, 111, NULL, N'8e1b5a72-6331-43a2-a67c-15efea409bbf')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (102, NULL, 110, NULL, N'084ea5e7-9f3c-4409-be13-ba8216e68c02')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (103, NULL, 109, NULL, N'749309c0-a829-4961-8215-cc50413d1c93')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (104, NULL, 108, NULL, N'bf9cb49f-2e99-4b34-91f1-d9b9967a6397')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (105, NULL, 107, NULL, N'e1ad9448-61f1-40e9-b2d7-84f7f9ae3c87')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (106, NULL, 104, NULL, N'6c2ab435-0f67-42f7-8a81-2c5436a42270')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (107, NULL, 100, NULL, N'f972652e-e707-4b6a-8cd8-01bd02f9407a')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (108, NULL, 99, NULL, N'8dae81e6-f816-422b-8dcb-fb47c8958421')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (109, NULL, 97, NULL, N'b30835bf-be04-4659-81e1-94332913bc7b')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (110, NULL, 95, NULL, N'4c758375-6554-49df-8ee0-3e7c65e3dea4')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (111, NULL, 94, NULL, N'f141dbc7-c0ba-4da9-ad3e-2b27e9ee56a5')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (113, NULL, 92, NULL, N'328edb79-6740-4b70-851a-4102fd436a9d')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (114, NULL, 91, NULL, N'b6fe9ba2-dbc4-46f1-8714-d7e78c092c55')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (117, NULL, 83, NULL, N'0aa19f66-3744-4715-ad2c-1e403798b616')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (118, NULL, 79, NULL, N'17891b76-45c6-4690-946a-7ed992b0796a')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (119, NULL, 78, NULL, N'6dc35b4f-819a-4e92-b5dd-935fc0d5e82b')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (120, NULL, 77, NULL, N'39a3c59c-be53-495e-b678-302c9989c08f')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (123, NULL, 74, NULL, N'7d7e5d84-1d67-4ab4-bfe6-e660fff6a153')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (124, NULL, 73, NULL, N'33643649-a491-458c-85f4-5bde72a00b97')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (125, NULL, 72, NULL, N'eb320076-3623-4963-af8a-de0e5f73b8d4')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (126, NULL, 71, NULL, N'c78c15aa-0053-4534-b8f6-d1fe89041309')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (127, NULL, 70, NULL, N'1325e296-44e0-4f44-9612-23a036f9aa5f')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (128, NULL, 68, NULL, N'5b02e68e-9df4-4a9c-8a14-eb18c4756dfd')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (129, NULL, 61, NULL, N'ca8cfb25-7a1b-4f86-8f52-36116b55b4e7')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (130, NULL, 59, NULL, N'786c8923-1fff-4be1-8610-5e7b5bf07b5f')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (136, NULL, 52, NULL, N'fcee3194-936d-439f-af38-6b74e792247d')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (137, NULL, 45, NULL, N'e3ed0fe1-d595-4b70-b605-af5fc4d3d86a')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (138, NULL, 43, NULL, N'b0fd7994-c43b-447e-a331-8be502f20f07')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (139, NULL, 42, NULL, N'24eba663-6d13-41eb-bbd0-a596b15ed1d0')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (140, NULL, 30, NULL, N'33f629cd-7591-479a-8da7-9b41a05ee720')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (141, NULL, 29, NULL, N'2acbd368-ee76-4c8f-b35f-be87d82fc857')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (142, NULL, 26, NULL, N'f85d7bba-727b-4d06-b250-faa392fc8411')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (143, NULL, 11, NULL, N'029ad4ce-d8d0-488b-9443-58e72285edb4')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (144, NULL, 8, NULL, N'e1bd67bd-4437-4f45-86ef-5eb9dbfd4eae')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (145, NULL, 3, NULL, N'191153b3-3cb3-45ae-95d2-4ad0fdb48620')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (146, NULL, 1, NULL, N'4bf7cf4b-bc50-4a2d-8686-6f9b2480cee7')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (147, N'Strings have to have at least one character.', 28, NULL, N'e5695d96-15aa-460f-be55-6c3fd7567d5e')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (148, N'strings in the database should not be longer than 4k', 172, NULL, N'04372ada-88c7-4ff8-8f28-ce76c3f736a3')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (149, N'strings in the database should not be longer than 4k', 173, NULL, N'967f623c-dd56-4224-9544-322128b292d7')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (150, NULL, 173, NULL, N'051aa6de-2426-41db-a48d-bd6ddbde1c32')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (151, NULL, 172, NULL, N'cd7613ed-8809-4892-bb92-7d02a11438bb')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (152, NULL, 167, NULL, N'50d38cf9-75d2-4f05-8d64-5a46391247e4')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (153, NULL, 162, NULL, N'a5e5b5a6-d6ee-4f74-9e6e-1d3d297162fa')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (154, NULL, 154, NULL, N'8901b641-ef28-4e94-8d28-039c6a4947e1')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (155, NULL, 149, NULL, N'2ae24cc6-140c-4c5d-a639-ac3da7187ae8')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (156, NULL, 148, NULL, N'ae58c480-5209-4618-8a9b-44ea53910298')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (157, NULL, 139, NULL, N'183b4bbf-2bbe-499e-afc1-98de0ee23fa8')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (158, NULL, 136, NULL, N'46db572f-58a4-4673-827b-7e5b976c202e')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (159, NULL, 130, NULL, N'bb45ba61-6b65-47bd-9d42-dc947d0d8014')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (160, NULL, 128, NULL, N'25ef284a-39a7-4d28-92d4-fe2ebd7e003f')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (161, NULL, 127, NULL, N'a5e9106b-bf3f-4626-a73e-548d1177d1f3')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (162, N'', 115, NULL, N'64d60cb4-5098-4d03-b4cc-e44b7f845243')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (163, NULL, 109, NULL, N'888c5b65-32b7-424c-8c59-b9880697a13a')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (164, NULL, 107, NULL, N'b3cab736-23be-4c1a-8fb7-1b4e689aa804')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (165, NULL, 99, NULL, N'19c8dc77-8079-43ab-8db3-5112cfc54700')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (166, NULL, 91, NULL, N'8efb3794-6f27-4c83-b3ff-6cd9dd680a99')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (168, NULL, 85, NULL, N'd23346da-59c9-4011-bc17-59f9f925fa78')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (169, NULL, 77, NULL, N'4a2de5ff-e539-4795-b13a-a04965d849a1')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (171, NULL, 71, NULL, N'33b4207b-fc81-4e99-a27f-d3b7e24dc06d')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (172, NULL, 68, NULL, N'1e20b663-808b-48d7-9db2-d3b525303cb1')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (173, NULL, 63, NULL, N'16b78e66-93ed-4817-9e11-45caef1da299')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (174, NULL, 62, NULL, N'59493532-e14f-4968-b19c-46792767e357')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (175, NULL, 61, NULL, N'ff85dca6-a0f1-4410-b9a1-e5c4e9cd3539')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (176, NULL, 60, NULL, N'e48a9549-018c-4da4-856c-29c15fba81ec')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (177, NULL, 59, NULL, N'ecba903c-9f8b-44f9-a048-0d241c480a44')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (178, NULL, 52, NULL, N'628a2b96-f7fe-4b4e-b2ff-9f76c10b4c50')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (179, NULL, 50, NULL, N'19a0fe43-535c-4d4b-b038-c036a34a496f')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (180, NULL, 48, NULL, N'339ba200-167c-4bc7-8409-a2350107c12a')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (181, NULL, 43, NULL, N'45f9afdb-7529-4f4f-af22-7b957342071a')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (182, NULL, 42, NULL, N'73821828-029d-498b-b3f2-8bc0708778f1')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (183, NULL, 41, NULL, N'e4e53e52-a40f-4d98-820e-3132cd586718')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (184, NULL, 40, NULL, N'12758ab2-e0dd-4212-9491-426af93b79a4')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (185, NULL, 39, NULL, N'eecb73e6-3890-4f04-ac98-1abe7d4de818')
GO
print 'Processed 100 total records'
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (186, NULL, 30, NULL, N'106355d6-2e2b-4af6-9bc8-358b06eee884')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (187, NULL, 20, NULL, N'25dda4b3-fdcd-4c74-aac0-151fe9f0b6ac')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (188, NULL, 15, NULL, N'c11b167a-d87c-4fb2-a8e1-b25880839b97')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (189, NULL, 13, NULL, N'cffb8784-4b8e-4c87-95d1-68b2bb9522df')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (190, NULL, 9, NULL, N'9f8f82dc-0fb6-41b3-bddc-453c4eb67542')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (191, NULL, 3, NULL, N'3216b626-121c-477a-b457-b4f2a347fd5c')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (192, N'Classes should have a non-empty name. Names longer than 51 become unwieldy.', 1, NULL, N'4b7fea07-8714-4280-96dc-33bdededc0b2')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (193, N'Method.ObjectClass and InvokeOnObjectClass have to match.', 74, NULL, N'c7efdbc8-05fc-439e-ac38-ec850fac5fc3')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (194, NULL, 3, NULL, N'6aa150ce-666b-4823-ae6f-6df3c2d569a2')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (195, NULL, 1, NULL, N'c0facd37-aec9-4716-b969-339b2fc361d5')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (196, NULL, 9, NULL, N'033a551a-4325-4e2b-8533-0ef28b043215')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (198, NULL, 42, NULL, N'2bcb81bd-5f50-44d6-b247-9938404e219b')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (199, NULL, 30, NULL, N'533b99b1-63d8-4c73-8856-7714c86fd828')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (200, NULL, 91, NULL, N'2cfa54f9-3e62-4db3-92c9-1f1c60ce5367')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (201, NULL, 136, NULL, N'c12b7b3d-548f-4d4d-91fa-8181e8d930d9')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (202, NULL, 174, NULL, N'2cf03dce-7f66-4f85-968f-708ebd1819f4')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (203, NULL, 179, NULL, N'b4f189d0-a7cc-4c44-83bb-e9264583dab7')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (204, NULL, 178, NULL, N'c9ecb11b-d23d-43f9-9c51-c17ac743bbda')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (205, NULL, 177, NULL, N'df796b11-89ce-4f1a-94e6-4b25e6658d5a')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (206, NULL, 176, NULL, N'02367a9f-35f0-4bb1-97f4-4d9999b6fa35')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (207, NULL, 175, NULL, N'b7bb2f92-c1e4-405f-bcb3-6de8969d134d')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (208, NULL, 180, NULL, N'323c7941-6578-4729-905b-31883c3fd13f')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (209, NULL, 184, NULL, N'8d4ed43a-62d6-45fd-b3e3-cfb60e11da6e')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (212, N'Cannot create legal O/R mapping without length', 28, NULL, N'c9459199-02a0-4ca2-b475-56b935449659')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (216, N'RelationEndRole can only be 1 (''A'') or 2 (''B'')', 217, NULL, N'368ded25-0fd0-4e68-bf71-0a56bd4ed681')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (217, NULL, 237, NULL, N'91b2db4b-a391-4321-af1b-8e9322cd0e70')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (218, NULL, 232, NULL, N'ef21d105-f97d-4f81-a98b-25d472cbc41f')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (219, NULL, 225, NULL, N'9d9c8bf8-26cf-4a35-a542-1fc4ebcd2504')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (220, NULL, 216, NULL, N'41d415ec-3004-4157-b185-d1e51b3244f0')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (221, NULL, 205, NULL, N'53b91dd8-1f92-4fd8-8795-d1cada6f3f9b')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (223, NULL, 238, NULL, N'5ed7d5f9-fa60-45c7-b0e4-c80440988b5a')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (224, NULL, 237, NULL, N'b07940bf-ed6b-4fc9-815e-a397933897c9')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (225, NULL, 236, NULL, N'c05fca0d-3b4c-451e-a080-b9d9af14340d')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (226, NULL, 235, NULL, N'28808e4b-f3de-4338-9c02-e9e0740afa71')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (227, NULL, 234, NULL, N'63cadc77-394a-44e5-ae89-b3f510e7ba82')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (228, NULL, 233, NULL, N'0407e4cc-2432-4a7a-9699-96affef22e57')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (229, NULL, 232, NULL, N'757f243b-b2b0-4696-a0ce-daef8641f2f7')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (230, NULL, 231, NULL, N'6d82fced-ed0f-45c9-8b54-0e2cfee2af4c')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (231, NULL, 229, NULL, N'113da634-8baa-4968-88cf-d599f817f4ea')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (232, NULL, 228, NULL, N'3f15b9a2-5fc3-46fa-91b3-5cbc5dafc98e')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (233, NULL, 227, NULL, N'4a4ee025-8f31-4ef4-9490-a4a1115f5a15')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (234, NULL, 226, NULL, N'a1036a7a-8bde-4272-b24c-6a07cd982064')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (235, NULL, 222, NULL, N'991e12d8-4a9d-4f5b-b100-e6a1e678fb26')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (236, NULL, 220, NULL, N'e89a9ec3-7f54-4559-8861-33f2b513623b')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (237, NULL, 219, NULL, N'b99bbaca-68d9-46b8-b2b6-be2667b6e260')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (238, NULL, 217, NULL, N'bd50f224-90c9-40f1-9981-46e6aa90cf3b')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (239, NULL, 216, NULL, N'c5e2a150-eaae-4fac-8c06-7bb4685fdce2')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (240, NULL, 215, NULL, N'ceda48e9-575c-44e3-99e5-3e0ee6a95506')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (241, NULL, 208, NULL, N'8c7b0eff-374b-4793-a873-fb956e8525e7')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (242, NULL, 206, NULL, N'00a1c9bb-55f4-4dee-a8df-44e579a3dc1a')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (243, NULL, 205, NULL, N'2eac40b0-02eb-4317-a99d-2c54b6d9a7e6')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (244, NULL, 204, NULL, N'e226933e-aef3-47f6-990d-ffb67974f3a0')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (246, NULL, 246, NULL, N'dc6ae076-e73f-4d2f-9f19-342c5fc69bee')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (247, NULL, 245, NULL, N'8a89b37e-bcff-4aa7-a6e3-6612a3ee98f4')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (248, NULL, 249, NULL, N'd4098f61-81aa-4003-aa9a-4fabd8af93ce')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (250, NULL, 247, NULL, N'38763442-8c34-4dc3-843b-a562bb082604')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (251, NULL, 246, NULL, N'5b13593f-c995-4b9a-b76d-48020e2bd931')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (252, NULL, 245, NULL, N'11e9acfc-73b0-4010-b93b-e23030e77dbd')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (253, NULL, 46, NULL, N'6dad4852-3225-4d6a-81ea-3753d5ea3e25')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (254, NULL, 252, NULL, N'0e004542-17a8-4a55-bce0-6bc1fec284d3')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (255, NULL, 251, NULL, N'14c2864d-8c22-4ba8-8834-631c0a634958')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (256, NULL, 253, NULL, N'12c4983e-07c5-4ebe-811a-de1cf984eb94')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (257, NULL, 254, NULL, N'35e5c20e-9377-4052-8348-0d5503a6622a')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (258, NULL, 255, NULL, N'c599a54b-354f-453a-83b2-1b2e9f29ea26')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (259, NULL, 256, NULL, N'df17be87-0c46-41b7-9fa4-498729e8c3a5')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (260, NULL, 257, NULL, N'0657525f-e876-42bd-b50c-d28613f3809a')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (261, NULL, 259, NULL, N'238ee0c8-3bf5-4d85-8c9f-94c7a2346f68')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (262, NULL, 260, NULL, N'4983bc6b-74c6-4e65-a03d-e00b974391a7')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (263, NULL, 261, NULL, N'3c317709-9e4e-40de-8b99-0e1512c90328')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (264, NULL, 262, NULL, N'0e338ebb-7e79-493a-bc14-23ed42af79f5')
INSERT [dbo].[Constraints] ([ID], [Reason], [fk_ConstrainedProperty], [fk_ConstrainedProperty_pos], [ExportGuid]) VALUES (265, NULL, 263, NULL, N'352b4949-d05e-40c6-b722-f3607e741101')
SET IDENTITY_INSERT [dbo].[Constraints] OFF
/****** Object:  Table [dbo].[MethodInvocationConstraints]    Script Date: 05/22/2009 17:30:29 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[MethodInvocationConstraints] ([ID]) VALUES (193)
/****** Object:  Table [dbo].[IsValidIdentifierConstraints]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IsValidIdentifierConstraints]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[IsValidIdentifierConstraints](
	[ID] [int] NOT NULL,
 CONSTRAINT [PK_IsValidIdentifierConstraints] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[IsValidIdentifierConstraints] ([ID]) VALUES (194)
INSERT [dbo].[IsValidIdentifierConstraints] ([ID]) VALUES (195)
INSERT [dbo].[IsValidIdentifierConstraints] ([ID]) VALUES (196)
INSERT [dbo].[IsValidIdentifierConstraints] ([ID]) VALUES (198)
INSERT [dbo].[IsValidIdentifierConstraints] ([ID]) VALUES (199)
INSERT [dbo].[IsValidIdentifierConstraints] ([ID]) VALUES (200)
INSERT [dbo].[IsValidIdentifierConstraints] ([ID]) VALUES (201)
/****** Object:  Table [dbo].[IntProperties]    Script Date: 05/22/2009 17:30:29 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[IntProperties] ([ID]) VALUES (28)
INSERT [dbo].[IntProperties] ([ID]) VALUES (126)
INSERT [dbo].[IntProperties] ([ID]) VALUES (135)
INSERT [dbo].[IntProperties] ([ID]) VALUES (168)
INSERT [dbo].[IntProperties] ([ID]) VALUES (169)
INSERT [dbo].[IntProperties] ([ID]) VALUES (172)
INSERT [dbo].[IntProperties] ([ID]) VALUES (173)
INSERT [dbo].[IntProperties] ([ID]) VALUES (217)
INSERT [dbo].[IntProperties] ([ID]) VALUES (268)
/****** Object:  Table [dbo].[IntegerRangeConstraints]    Script Date: 05/22/2009 17:30:29 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[IntegerRangeConstraints] ([ID], [Max], [Min]) VALUES (147, 4000, 1)
INSERT [dbo].[IntegerRangeConstraints] ([ID], [Max], [Min]) VALUES (148, 4000, 0)
INSERT [dbo].[IntegerRangeConstraints] ([ID], [Max], [Min]) VALUES (149, 4000, 0)
INSERT [dbo].[IntegerRangeConstraints] ([ID], [Max], [Min]) VALUES (216, 2, 1)
/****** Object:  Table [dbo].[GuidProperties]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GuidProperties]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GuidProperties](
	[ID] [int] NOT NULL,
 CONSTRAINT [PK_GuidProperties] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[GuidProperties] ([ID]) VALUES (251)
INSERT [dbo].[GuidProperties] ([ID]) VALUES (252)
INSERT [dbo].[GuidProperties] ([ID]) VALUES (253)
INSERT [dbo].[GuidProperties] ([ID]) VALUES (254)
INSERT [dbo].[GuidProperties] ([ID]) VALUES (255)
INSERT [dbo].[GuidProperties] ([ID]) VALUES (256)
INSERT [dbo].[GuidProperties] ([ID]) VALUES (257)
INSERT [dbo].[GuidProperties] ([ID]) VALUES (259)
INSERT [dbo].[GuidProperties] ([ID]) VALUES (260)
INSERT [dbo].[GuidProperties] ([ID]) VALUES (261)
INSERT [dbo].[GuidProperties] ([ID]) VALUES (262)
INSERT [dbo].[GuidProperties] ([ID]) VALUES (263)
/****** Object:  Table [dbo].[EnumerationProperties]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EnumerationProperties]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EnumerationProperties](
	[ID] [int] NOT NULL,
	[fk_Enumeration] [int] NOT NULL,
	[fk_Enumeration_pos] [int] NULL,
 CONSTRAINT [PK_EnumerationProperties] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[EnumerationProperties] ([ID], [fk_Enumeration], [fk_Enumeration_pos]) VALUES (110, 50, NULL)
INSERT [dbo].[EnumerationProperties] ([ID], [fk_Enumeration], [fk_Enumeration_pos]) VALUES (111, 50, NULL)
INSERT [dbo].[EnumerationProperties] ([ID], [fk_Enumeration], [fk_Enumeration_pos]) VALUES (113, 53, NULL)
INSERT [dbo].[EnumerationProperties] ([ID], [fk_Enumeration], [fk_Enumeration_pos]) VALUES (117, 53, NULL)
INSERT [dbo].[EnumerationProperties] ([ID], [fk_Enumeration], [fk_Enumeration_pos]) VALUES (118, 55, NULL)
INSERT [dbo].[EnumerationProperties] ([ID], [fk_Enumeration], [fk_Enumeration_pos]) VALUES (137, 55, NULL)
INSERT [dbo].[EnumerationProperties] ([ID], [fk_Enumeration], [fk_Enumeration_pos]) VALUES (150, 55, NULL)
INSERT [dbo].[EnumerationProperties] ([ID], [fk_Enumeration], [fk_Enumeration_pos]) VALUES (183, 78, NULL)
INSERT [dbo].[EnumerationProperties] ([ID], [fk_Enumeration], [fk_Enumeration_pos]) VALUES (219, 81, NULL)
INSERT [dbo].[EnumerationProperties] ([ID], [fk_Enumeration], [fk_Enumeration_pos]) VALUES (228, 53, NULL)
INSERT [dbo].[EnumerationProperties] ([ID], [fk_Enumeration], [fk_Enumeration_pos]) VALUES (229, 55, NULL)
INSERT [dbo].[EnumerationProperties] ([ID], [fk_Enumeration], [fk_Enumeration_pos]) VALUES (233, 55, NULL)
/****** Object:  Table [dbo].[DoubleProperties]    Script Date: 05/22/2009 17:30:29 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[DoubleProperties] ([ID]) VALUES (18)
INSERT [dbo].[DoubleProperties] ([ID]) VALUES (23)
INSERT [dbo].[DoubleProperties] ([ID]) VALUES (65)
INSERT [dbo].[DoubleProperties] ([ID]) VALUES (89)
INSERT [dbo].[DoubleProperties] ([ID]) VALUES (90)
/****** Object:  Table [dbo].[DateTimeProperties]    Script Date: 05/22/2009 17:30:29 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[DateTimeProperties] ([ID]) VALUES (16)
INSERT [dbo].[DateTimeProperties] ([ID]) VALUES (17)
INSERT [dbo].[DateTimeProperties] ([ID]) VALUES (38)
INSERT [dbo].[DateTimeProperties] ([ID]) VALUES (133)
INSERT [dbo].[DateTimeProperties] ([ID]) VALUES (238)
INSERT [dbo].[DateTimeProperties] ([ID]) VALUES (239)
INSERT [dbo].[DateTimeProperties] ([ID]) VALUES (247)
INSERT [dbo].[DateTimeProperties] ([ID]) VALUES (248)
/****** Object:  Table [dbo].[BoolProperties]    Script Date: 05/22/2009 17:30:29 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[BoolProperties] ([ID]) VALUES (11)
INSERT [dbo].[BoolProperties] ([ID]) VALUES (26)
INSERT [dbo].[BoolProperties] ([ID]) VALUES (83)
INSERT [dbo].[BoolProperties] ([ID]) VALUES (94)
INSERT [dbo].[BoolProperties] ([ID]) VALUES (95)
INSERT [dbo].[BoolProperties] ([ID]) VALUES (116)
INSERT [dbo].[BoolProperties] ([ID]) VALUES (119)
INSERT [dbo].[BoolProperties] ([ID]) VALUES (124)
INSERT [dbo].[BoolProperties] ([ID]) VALUES (174)
INSERT [dbo].[BoolProperties] ([ID]) VALUES (204)
INSERT [dbo].[BoolProperties] ([ID]) VALUES (220)
INSERT [dbo].[BoolProperties] ([ID]) VALUES (264)
INSERT [dbo].[BoolProperties] ([ID]) VALUES (265)
INSERT [dbo].[BoolProperties] ([ID]) VALUES (266)
/****** Object:  Table [dbo].[StringRangeConstraints]    Script Date: 05/22/2009 17:30:29 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
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
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (168, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (169, 200, 0)
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
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (203, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (204, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (205, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (206, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (207, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (208, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (209, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (217, 4000, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (218, 4000, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (219, 4000, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (220, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (221, 200, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (246, 4000, 0)
INSERT [dbo].[StringRangeConstraints] ([ID], [MaxLength], [MinLength]) VALUES (247, 400, 0)
/****** Object:  Table [dbo].[StringProperties]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StringProperties]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StringProperties](
	[ID] [int] NOT NULL,
	[Length] [int] NOT NULL,
 CONSTRAINT [PK_StringProperties] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
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
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (77, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (85, 200)
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
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (175, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (176, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (177, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (178, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (179, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (180, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (184, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (205, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (216, 200)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (225, 4000)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (232, 4000)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (237, 4000)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (245, 400)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (246, 4000)
INSERT [dbo].[StringProperties] ([ID], [Length]) VALUES (267, 4000)
/****** Object:  Table [dbo].[StringConstraints]    Script Date: 05/22/2009 17:30:29 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Relations]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Relations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Relations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Storage] [int] NULL,
	[Description] [nvarchar](200) NULL,
	[fk_A] [int] NULL,
	[fk_B] [int] NULL,
	[fk_A_pos] [int] NULL,
	[fk_B_pos] [int] NULL,
	[ExportGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Relations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Relations] ON
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (19, 2, NULL, 3, 4, NULL, NULL, N'f7e487a4-6922-40bf-a404-24ce6accbb83')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (22, 2, NULL, 9, 10, NULL, NULL, N'434dab4f-0dcd-4724-a62b-730540ce143a')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (23, 4, NULL, 11, 12, NULL, NULL, N'c7b3cf10-cdc8-454c-826c-04a0f7e5ef3e')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (24, 2, NULL, 13, 14, NULL, NULL, N'cba32040-b1f2-4267-991a-f3dc0e349ff5')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (25, 2, NULL, 15, 16, NULL, NULL, N'557ddcb4-8e99-48f2-9107-ef0cbff8066b')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (26, 2, NULL, 17, 18, NULL, NULL, N'52c4ab07-f341-4eb3-86e2-05f27c8af2f7')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (27, 1, NULL, 19, 20, NULL, NULL, N'29b2b281-979e-470c-80fc-e6d9a0d58846')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (29, 1, NULL, 23, 24, NULL, NULL, N'4964faf9-ebae-4287-91f8-6d2112a5921c')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (30, 2, NULL, 25, 26, NULL, NULL, N'062fa6cf-bdb1-4994-9e8b-5fe5426c60aa')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (34, 1, NULL, 33, 34, NULL, NULL, N'c0c472d1-95b3-4588-812b-0d41c6e692b5')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (35, 1, NULL, 35, 36, NULL, NULL, N'eb852cc4-3977-42b9-9fcd-3a8c60aa49ac')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (36, 2, NULL, 37, 38, NULL, NULL, N'a10474db-85df-4731-a86c-124e54f3d146')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (37, 1, NULL, 39, 40, NULL, NULL, N'bffae7c3-c5f3-4139-ae96-577f4c9fed8f')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (38, 1, NULL, 41, 42, NULL, NULL, N'b251ee8c-2821-441e-b631-d215c006f1c8')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (39, 2, NULL, 43, 44, NULL, NULL, N'4a388300-4e4d-45d2-b04e-0fe8efc25fec')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (40, 1, NULL, 45, 46, NULL, NULL, N'379b7181-a832-431f-a48d-ef1dd1996414')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (41, 2, NULL, 47, 48, NULL, NULL, N'dabf87d2-8038-4bc7-978f-f043151c7d25')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (42, 4, NULL, 49, 50, NULL, NULL, N'7db412de-b90b-48ba-8340-1e6ac8c8fbaf')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (44, 2, NULL, 53, 54, NULL, NULL, N'f7738ce1-9784-4b8b-8156-9f4f0e97f937')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (45, 1, NULL, 55, 56, NULL, NULL, N'a6a30705-15ad-4a3a-b624-23305fe2807a')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (46, 1, NULL, 57, 58, NULL, NULL, N'7958f200-b54a-4826-b9f8-15245a520ad3')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (47, 2, NULL, 59, 60, NULL, NULL, N'55bd59b8-ad37-4837-b066-d505f86316fe')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (48, 1, NULL, 61, 62, NULL, NULL, N'f85ff30f-0907-4e28-806e-a7f1aac98acb')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (49, 4, NULL, 63, 64, NULL, NULL, N'692c1064-37a2-4be3-a81e-4cb91f673aa3')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (50, 1, NULL, 65, 66, NULL, NULL, N'9d44eac8-2470-4373-a2bf-df3bc16d3454')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (51, 1, NULL, 67, 68, NULL, NULL, N'3937047e-2ce5-49f1-86e9-31cd5a07f597')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (52, 1, NULL, 69, 70, NULL, NULL, N'cbc99fc5-2b15-4829-b4ae-bc8e38e767a8')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (53, 1, NULL, 71, 72, NULL, NULL, N'a4a80d3c-782b-4100-af08-699410b0a8dc')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (54, 1, NULL, 73, 74, NULL, NULL, N'0d3439ab-0bad-45c1-a064-7812c698ccd9')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (55, 4, NULL, 75, 76, NULL, NULL, N'4d4e1ffd-f362-40e2-9fe1-0711ded83241')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (56, 1, NULL, 77, 78, NULL, NULL, N'73178882-7f93-444b-bf93-75db193904cf')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (57, 1, NULL, 79, 80, NULL, NULL, N'304c9a1e-7365-45ee-a685-348fd76f10e7')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (58, 1, NULL, 81, 82, NULL, NULL, N'299a4cf9-3f3e-4b89-b6ba-6b163b4e5dc0')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (59, 1, NULL, 83, 84, NULL, NULL, N'0e64ccd9-2f72-489a-83a4-095f949fdee3')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (60, 4, NULL, 85, 86, NULL, NULL, N'358c14b9-fef5-495d-8d44-04e84186830e')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (61, 4, NULL, 87, 88, NULL, NULL, N'81ff3089-57da-478c-8be5-fd23abc222a2')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (62, 2, NULL, 89, 90, NULL, NULL, N'6fa271a3-e365-4b8d-9cb1-575d7a3b5d6a')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (65, 1, NULL, 95, 96, NULL, NULL, N'c10b1abc-3786-40f6-8c8c-dccdd8dc03ef')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (66, 4, NULL, 97, 98, NULL, NULL, N'8b41ffa4-8ffa-4d96-b4e5-708188045c71')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (67, 1, NULL, 99, 100, NULL, NULL, N'dc9013af-8758-40b4-8f52-c2c8683a13e0')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (70, 1, NULL, 105, 106, NULL, NULL, N'05f8e0f8-24cb-4262-94dc-b744db8ba73c')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (71, 1, NULL, 107, 108, NULL, NULL, N'918ea3af-d3da-4c67-b89f-c60915c65553')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (72, 1, NULL, 109, 110, NULL, NULL, N'b33de3e9-c788-43f9-acb8-5306ec869684')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (73, 1, NULL, 111, 112, NULL, NULL, N'1d8d8e0b-cb0d-4746-a4c5-85c8f399e00a')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (74, 1, NULL, 113, 114, NULL, NULL, N'4a6b1f0e-50cf-47e8-a6b4-0be41fb44db4')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (75, 1, N'This relation describes which presentable model can be displayed with this view', 117, 115, NULL, NULL, N'25aef3e6-1533-4c1b-8ee2-4f75aca3c494')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (76, 1, N'This relation describes which control implements which view', 118, 116, NULL, NULL, N'6c1c4c7c-7f0c-4c80-a937-ed6af8774d3f')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (77, 1, N'This relation between a PresentableModelDescriptor and the described PresentableModel''s Type', 120, 119, NULL, NULL, N'9d771d87-3b28-4e5e-be33-ea71028e1720')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (78, 1, N'This relation between a PresentableModelDescriptor and the described PresentableModel''s Type', 122, 121, NULL, NULL, N'1ae94c81-3359-45e8-b97a-b61add91abba')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (79, 1, N'This relation describes the underlying inheritance relationships', 124, 123, NULL, NULL, N'2094dc91-456b-4cdf-ac0c-bf97f5c85a7e')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (80, 1, N'Connects a Property with the PresentableModel to display her value', 126, 125, NULL, NULL, N'3437ea5d-d926-4a0b-a848-9dafedf7ad6a')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (81, 1, N'which Mitarbeiter had which PresenceRecords', 132, 131, NULL, NULL, N'f6d98929-883a-4457-a49f-157324bd5ae3')
INSERT [dbo].[Relations] ([ID], [Storage], [Description], [fk_A], [fk_B], [fk_A_pos], [fk_B_pos], [ExportGuid]) VALUES (82, 1, N'which Mitarbeiter has effected which WorkEfforts', 134, 133, NULL, NULL, N'3963b6bc-bb5a-4615-b4db-56eecd9d3f97')
SET IDENTITY_INSERT [dbo].[Relations] OFF
/****** Object:  Table [dbo].[NotNullableConstraints]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NotNullableConstraints]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NotNullableConstraints](
	[ID] [int] NOT NULL,
	[Reason] [nvarchar](400) NULL,
 CONSTRAINT [PK_NotNullableConstraints] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
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
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (113, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (114, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (117, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (118, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (119, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (120, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (123, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (124, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (125, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (126, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (127, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (128, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (129, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (130, NULL)
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
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (202, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (212, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (223, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (224, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (225, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (226, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (227, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (228, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (229, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (230, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (231, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (232, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (233, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (234, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (235, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (236, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (237, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (238, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (239, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (240, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (241, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (242, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (243, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (244, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (248, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (250, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (251, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (252, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (253, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (254, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (255, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (256, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (257, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (258, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (259, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (260, NULL)
GO
print 'Processed 100 total records'
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (261, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (262, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (263, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (264, NULL)
INSERT [dbo].[NotNullableConstraints] ([ID], [Reason]) VALUES (265, NULL)
/****** Object:  Table [dbo].[IsValidNamespaceConstraints]    Script Date: 05/22/2009 17:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IsValidNamespaceConstraints]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[IsValidNamespaceConstraints](
	[ID] [int] NOT NULL,
 CONSTRAINT [PK_IsValidNamespaceConstraints] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[IsValidNamespaceConstraints] ([ID]) VALUES (198)
/****** Object:  ForeignKey [FK_Kunde_EMailsCollectionEntry_Kunde_fk_Kunde]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Kunde_EMailsCollectionEntry_Kunde_fk_Kunde]') AND parent_object_id = OBJECT_ID(N'[dbo].[Kunden_EMailsCollection]'))
ALTER TABLE [dbo].[Kunden_EMailsCollection]  WITH CHECK ADD  CONSTRAINT [FK_Kunde_EMailsCollectionEntry_Kunde_fk_Kunde] FOREIGN KEY([fk_Kunde])
REFERENCES [dbo].[Kunden] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Kunde_EMailsCollectionEntry_Kunde_fk_Kunde]') AND parent_object_id = OBJECT_ID(N'[dbo].[Kunden_EMailsCollection]'))
ALTER TABLE [dbo].[Kunden_EMailsCollection] CHECK CONSTRAINT [FK_Kunde_EMailsCollectionEntry_Kunde_fk_Kunde]
GO
/****** Object:  ForeignKey [FK_Projekt_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projekt_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projekte_MitarbeiterCollection]'))
ALTER TABLE [dbo].[Projekte_MitarbeiterCollection]  WITH CHECK ADD  CONSTRAINT [FK_Projekt_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter] FOREIGN KEY([fk_Mitarbeiter])
REFERENCES [dbo].[Mitarbeiter] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projekt_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projekte_MitarbeiterCollection]'))
ALTER TABLE [dbo].[Projekte_MitarbeiterCollection] CHECK CONSTRAINT [FK_Projekt_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]
GO
/****** Object:  ForeignKey [FK_Projekt_MitarbeiterCollectionEntry_Projekt_fk_Projekt]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projekt_MitarbeiterCollectionEntry_Projekt_fk_Projekt]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projekte_MitarbeiterCollection]'))
ALTER TABLE [dbo].[Projekte_MitarbeiterCollection]  WITH CHECK ADD  CONSTRAINT [FK_Projekt_MitarbeiterCollectionEntry_Projekt_fk_Projekt] FOREIGN KEY([fk_Projekt])
REFERENCES [dbo].[Projekte] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projekt_MitarbeiterCollectionEntry_Projekt_fk_Projekt]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projekte_MitarbeiterCollection]'))
ALTER TABLE [dbo].[Projekte_MitarbeiterCollection] CHECK CONSTRAINT [FK_Projekt_MitarbeiterCollectionEntry_Projekt_fk_Projekt]
GO
/****** Object:  ForeignKey [FK_Task_Projekt_fk_Projekt]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Task_Projekt_fk_Projekt]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tasks]'))
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Task_Projekt_fk_Projekt] FOREIGN KEY([fk_Projekt])
REFERENCES [dbo].[Projekte] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Task_Projekt_fk_Projekt]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tasks]'))
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Task_Projekt_fk_Projekt]
GO
/****** Object:  ForeignKey [FK_WorkEffortAccount_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkEffortAccount_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkEffortAccounts_MitarbeiterCollection]'))
ALTER TABLE [dbo].[WorkEffortAccounts_MitarbeiterCollection]  WITH CHECK ADD  CONSTRAINT [FK_WorkEffortAccount_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter] FOREIGN KEY([fk_Mitarbeiter])
REFERENCES [dbo].[Mitarbeiter] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkEffortAccount_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkEffortAccounts_MitarbeiterCollection]'))
ALTER TABLE [dbo].[WorkEffortAccounts_MitarbeiterCollection] CHECK CONSTRAINT [FK_WorkEffortAccount_MitarbeiterCollectionEntry_Mitarbeiter_fk_Mitarbeiter]
GO
/****** Object:  ForeignKey [FK_WorkEffortAccount_MitarbeiterCollectionEntry_WorkEffortAccount_fk_WorkEffortAccount]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkEffortAccount_MitarbeiterCollectionEntry_WorkEffortAccount_fk_WorkEffortAccount]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkEffortAccounts_MitarbeiterCollection]'))
ALTER TABLE [dbo].[WorkEffortAccounts_MitarbeiterCollection]  WITH CHECK ADD  CONSTRAINT [FK_WorkEffortAccount_MitarbeiterCollectionEntry_WorkEffortAccount_fk_WorkEffortAccount] FOREIGN KEY([fk_WorkEffortAccount])
REFERENCES [dbo].[WorkEffortAccounts] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkEffortAccount_MitarbeiterCollectionEntry_WorkEffortAccount_fk_WorkEffortAccount]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkEffortAccounts_MitarbeiterCollection]'))
ALTER TABLE [dbo].[WorkEffortAccounts_MitarbeiterCollection] CHECK CONSTRAINT [FK_WorkEffortAccount_MitarbeiterCollectionEntry_WorkEffortAccount_fk_WorkEffortAccount]
GO
/****** Object:  ForeignKey [FK_TestObjClass_Kunde_fk_ObjectProp]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TestObjClass_Kunde_fk_ObjectProp]') AND parent_object_id = OBJECT_ID(N'[dbo].[TestObjClasses]'))
ALTER TABLE [dbo].[TestObjClasses]  WITH CHECK ADD  CONSTRAINT [FK_TestObjClass_Kunde_fk_ObjectProp] FOREIGN KEY([fk_ObjectProp])
REFERENCES [dbo].[Kunden] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TestObjClass_Kunde_fk_ObjectProp]') AND parent_object_id = OBJECT_ID(N'[dbo].[TestObjClasses]'))
ALTER TABLE [dbo].[TestObjClasses] CHECK CONSTRAINT [FK_TestObjClass_Kunde_fk_ObjectProp]
GO
/****** Object:  ForeignKey [FK_Auftrag_Kunde_fk_Kunde]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Auftrag_Kunde_fk_Kunde]') AND parent_object_id = OBJECT_ID(N'[dbo].[Auftraege]'))
ALTER TABLE [dbo].[Auftraege]  WITH CHECK ADD  CONSTRAINT [FK_Auftrag_Kunde_fk_Kunde] FOREIGN KEY([fk_Kunde])
REFERENCES [dbo].[Kunden] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Auftrag_Kunde_fk_Kunde]') AND parent_object_id = OBJECT_ID(N'[dbo].[Auftraege]'))
ALTER TABLE [dbo].[Auftraege] CHECK CONSTRAINT [FK_Auftrag_Kunde_fk_Kunde]
GO
/****** Object:  ForeignKey [FK_Auftrag_Mitarbeiter_fk_Mitarbeiter]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Auftrag_Mitarbeiter_fk_Mitarbeiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[Auftraege]'))
ALTER TABLE [dbo].[Auftraege]  WITH CHECK ADD  CONSTRAINT [FK_Auftrag_Mitarbeiter_fk_Mitarbeiter] FOREIGN KEY([fk_Mitarbeiter])
REFERENCES [dbo].[Mitarbeiter] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Auftrag_Mitarbeiter_fk_Mitarbeiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[Auftraege]'))
ALTER TABLE [dbo].[Auftraege] CHECK CONSTRAINT [FK_Auftrag_Mitarbeiter_fk_Mitarbeiter]
GO
/****** Object:  ForeignKey [FK_Auftrag_Projekt_fk_Projekt]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Auftrag_Projekt_fk_Projekt]') AND parent_object_id = OBJECT_ID(N'[dbo].[Auftraege]'))
ALTER TABLE [dbo].[Auftraege]  WITH CHECK ADD  CONSTRAINT [FK_Auftrag_Projekt_fk_Projekt] FOREIGN KEY([fk_Projekt])
REFERENCES [dbo].[Projekte] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Auftrag_Projekt_fk_Projekt]') AND parent_object_id = OBJECT_ID(N'[dbo].[Auftraege]'))
ALTER TABLE [dbo].[Auftraege] CHECK CONSTRAINT [FK_Auftrag_Projekt_fk_Projekt]
GO
/****** Object:  ForeignKey [FK_Assembly_Module_fk_Module]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Assembly_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[Assemblies]'))
ALTER TABLE [dbo].[Assemblies]  WITH CHECK ADD  CONSTRAINT [FK_Assembly_Module_fk_Module] FOREIGN KEY([fk_Module])
REFERENCES [dbo].[Modules] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Assembly_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[Assemblies]'))
ALTER TABLE [dbo].[Assemblies] CHECK CONSTRAINT [FK_Assembly_Module_fk_Module]
GO
/****** Object:  ForeignKey [FK_DataType_Icon_fk_DefaultIcon]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DataType_Icon_fk_DefaultIcon]') AND parent_object_id = OBJECT_ID(N'[dbo].[DataTypes]'))
ALTER TABLE [dbo].[DataTypes]  WITH CHECK ADD  CONSTRAINT [FK_DataType_Icon_fk_DefaultIcon] FOREIGN KEY([fk_DefaultIcon])
REFERENCES [dbo].[Icons] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DataType_Icon_fk_DefaultIcon]') AND parent_object_id = OBJECT_ID(N'[dbo].[DataTypes]'))
ALTER TABLE [dbo].[DataTypes] CHECK CONSTRAINT [FK_DataType_Icon_fk_DefaultIcon]
GO
/****** Object:  ForeignKey [FK_DataType_Module_fk_Module]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DataType_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[DataTypes]'))
ALTER TABLE [dbo].[DataTypes]  WITH CHECK ADD  CONSTRAINT [FK_DataType_Module_fk_Module] FOREIGN KEY([fk_Module])
REFERENCES [dbo].[Modules] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DataType_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[DataTypes]'))
ALTER TABLE [dbo].[DataTypes] CHECK CONSTRAINT [FK_DataType_Module_fk_Module]
GO
/****** Object:  ForeignKey [FK_Interface_DataType_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Interface_DataType_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Interfaces]'))
ALTER TABLE [dbo].[Interfaces]  WITH CHECK ADD  CONSTRAINT [FK_Interface_DataType_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[DataTypes] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Interface_DataType_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Interfaces]'))
ALTER TABLE [dbo].[Interfaces] CHECK CONSTRAINT [FK_Interface_DataType_ID]
GO
/****** Object:  ForeignKey [FK_Enumeration_DataType_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Enumeration_DataType_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Enumerations]'))
ALTER TABLE [dbo].[Enumerations]  WITH CHECK ADD  CONSTRAINT [FK_Enumeration_DataType_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[DataTypes] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Enumeration_DataType_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Enumerations]'))
ALTER TABLE [dbo].[Enumerations] CHECK CONSTRAINT [FK_Enumeration_DataType_ID]
GO
/****** Object:  ForeignKey [FK_Method_DataType_fk_ObjectClass]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Method_DataType_fk_ObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[Methods]'))
ALTER TABLE [dbo].[Methods]  WITH CHECK ADD  CONSTRAINT [FK_Method_DataType_fk_ObjectClass] FOREIGN KEY([fk_ObjectClass])
REFERENCES [dbo].[DataTypes] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Method_DataType_fk_ObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[Methods]'))
ALTER TABLE [dbo].[Methods] CHECK CONSTRAINT [FK_Method_DataType_fk_ObjectClass]
GO
/****** Object:  ForeignKey [FK_Method_Module_fk_Module]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Method_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[Methods]'))
ALTER TABLE [dbo].[Methods]  WITH CHECK ADD  CONSTRAINT [FK_Method_Module_fk_Module] FOREIGN KEY([fk_Module])
REFERENCES [dbo].[Modules] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Method_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[Methods]'))
ALTER TABLE [dbo].[Methods] CHECK CONSTRAINT [FK_Method_Module_fk_Module]
GO
/****** Object:  ForeignKey [FK_ControlInfo_Assembly_fk_Assembly]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ControlInfo_Assembly_fk_Assembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[ControlInfos]'))
ALTER TABLE [dbo].[ControlInfos]  WITH CHECK ADD  CONSTRAINT [FK_ControlInfo_Assembly_fk_Assembly] FOREIGN KEY([fk_Assembly])
REFERENCES [dbo].[Assemblies] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ControlInfo_Assembly_fk_Assembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[ControlInfos]'))
ALTER TABLE [dbo].[ControlInfos] CHECK CONSTRAINT [FK_ControlInfo_Assembly_fk_Assembly]
GO
/****** Object:  ForeignKey [FK_TypeRef_Assembly_fk_Assembly]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TypeRef_Assembly_fk_Assembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[TypeRefs]'))
ALTER TABLE [dbo].[TypeRefs]  WITH CHECK ADD  CONSTRAINT [FK_TypeRef_Assembly_fk_Assembly] FOREIGN KEY([fk_Assembly])
REFERENCES [dbo].[Assemblies] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TypeRef_Assembly_fk_Assembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[TypeRefs]'))
ALTER TABLE [dbo].[TypeRefs] CHECK CONSTRAINT [FK_TypeRef_Assembly_fk_Assembly]
GO
/****** Object:  ForeignKey [FK_TypeRef_TypeRef_fk_Parent]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TypeRef_TypeRef_fk_Parent]') AND parent_object_id = OBJECT_ID(N'[dbo].[TypeRefs]'))
ALTER TABLE [dbo].[TypeRefs]  WITH CHECK ADD  CONSTRAINT [FK_TypeRef_TypeRef_fk_Parent] FOREIGN KEY([fk_Parent])
REFERENCES [dbo].[TypeRefs] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TypeRef_TypeRef_fk_Parent]') AND parent_object_id = OBJECT_ID(N'[dbo].[TypeRefs]'))
ALTER TABLE [dbo].[TypeRefs] CHECK CONSTRAINT [FK_TypeRef_TypeRef_fk_Parent]
GO
/****** Object:  ForeignKey [FK_Struct_DataType_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Struct_DataType_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Structs]'))
ALTER TABLE [dbo].[Structs]  WITH CHECK ADD  CONSTRAINT [FK_Struct_DataType_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[DataTypes] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Struct_DataType_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Structs]'))
ALTER TABLE [dbo].[Structs] CHECK CONSTRAINT [FK_Struct_DataType_ID]
GO
/****** Object:  ForeignKey [FK_PresenterInfo_Assembly_fk_DataAssembly]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PresenterInfo_Assembly_fk_DataAssembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[PresenterInfos]'))
ALTER TABLE [dbo].[PresenterInfos]  WITH CHECK ADD  CONSTRAINT [FK_PresenterInfo_Assembly_fk_DataAssembly] FOREIGN KEY([fk_DataAssembly])
REFERENCES [dbo].[Assemblies] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PresenterInfo_Assembly_fk_DataAssembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[PresenterInfos]'))
ALTER TABLE [dbo].[PresenterInfos] CHECK CONSTRAINT [FK_PresenterInfo_Assembly_fk_DataAssembly]
GO
/****** Object:  ForeignKey [FK_PresenterInfo_Assembly_fk_PresenterAssembly]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PresenterInfo_Assembly_fk_PresenterAssembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[PresenterInfos]'))
ALTER TABLE [dbo].[PresenterInfos]  WITH CHECK ADD  CONSTRAINT [FK_PresenterInfo_Assembly_fk_PresenterAssembly] FOREIGN KEY([fk_PresenterAssembly])
REFERENCES [dbo].[Assemblies] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PresenterInfo_Assembly_fk_PresenterAssembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[PresenterInfos]'))
ALTER TABLE [dbo].[PresenterInfos] CHECK CONSTRAINT [FK_PresenterInfo_Assembly_fk_PresenterAssembly]
GO
/****** Object:  ForeignKey [FK_PresentableModelDescriptors_TypeRef_fk_PresentableModelRef]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PresentableModelDescriptors_TypeRef_fk_PresentableModelRef]') AND parent_object_id = OBJECT_ID(N'[dbo].[PresentableModelDescriptors]'))
ALTER TABLE [dbo].[PresentableModelDescriptors]  WITH CHECK ADD  CONSTRAINT [FK_PresentableModelDescriptors_TypeRef_fk_PresentableModelRef] FOREIGN KEY([fk_PresentableModelRef])
REFERENCES [dbo].[TypeRefs] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PresentableModelDescriptors_TypeRef_fk_PresentableModelRef]') AND parent_object_id = OBJECT_ID(N'[dbo].[PresentableModelDescriptors]'))
ALTER TABLE [dbo].[PresentableModelDescriptors] CHECK CONSTRAINT [FK_PresentableModelDescriptors_TypeRef_fk_PresentableModelRef]
GO
/****** Object:  ForeignKey [FK_Visual_Method_fk_Method]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_Method_fk_Method]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals]'))
ALTER TABLE [dbo].[Visuals]  WITH CHECK ADD  CONSTRAINT [FK_Visual_Method_fk_Method] FOREIGN KEY([fk_Method])
REFERENCES [dbo].[Methods] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_Method_fk_Method]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals]'))
ALTER TABLE [dbo].[Visuals] CHECK CONSTRAINT [FK_Visual_Method_fk_Method]
GO
/****** Object:  ForeignKey [FK_TypeRef_GenericArgumentsCollectionEntry_TypeRef_fk_GenericArguments]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TypeRef_GenericArgumentsCollectionEntry_TypeRef_fk_GenericArguments]') AND parent_object_id = OBJECT_ID(N'[dbo].[TypeRefs_GenericArgumentsCollection]'))
ALTER TABLE [dbo].[TypeRefs_GenericArgumentsCollection]  WITH CHECK ADD  CONSTRAINT [FK_TypeRef_GenericArgumentsCollectionEntry_TypeRef_fk_GenericArguments] FOREIGN KEY([fk_GenericArguments])
REFERENCES [dbo].[TypeRefs] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TypeRef_GenericArgumentsCollectionEntry_TypeRef_fk_GenericArguments]') AND parent_object_id = OBJECT_ID(N'[dbo].[TypeRefs_GenericArgumentsCollection]'))
ALTER TABLE [dbo].[TypeRefs_GenericArgumentsCollection] CHECK CONSTRAINT [FK_TypeRef_GenericArgumentsCollectionEntry_TypeRef_fk_GenericArguments]
GO
/****** Object:  ForeignKey [FK_TypeRef_GenericArgumentsCollectionEntry_TypeRef_fk_TypeRef]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TypeRef_GenericArgumentsCollectionEntry_TypeRef_fk_TypeRef]') AND parent_object_id = OBJECT_ID(N'[dbo].[TypeRefs_GenericArgumentsCollection]'))
ALTER TABLE [dbo].[TypeRefs_GenericArgumentsCollection]  WITH CHECK ADD  CONSTRAINT [FK_TypeRef_GenericArgumentsCollectionEntry_TypeRef_fk_TypeRef] FOREIGN KEY([fk_TypeRef])
REFERENCES [dbo].[TypeRefs] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TypeRef_GenericArgumentsCollectionEntry_TypeRef_fk_TypeRef]') AND parent_object_id = OBJECT_ID(N'[dbo].[TypeRefs_GenericArgumentsCollection]'))
ALTER TABLE [dbo].[TypeRefs_GenericArgumentsCollection] CHECK CONSTRAINT [FK_TypeRef_GenericArgumentsCollectionEntry_TypeRef_fk_TypeRef]
GO
/****** Object:  ForeignKey [FK_BaseParameter_Method_fk_Method]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BaseParameter_Method_fk_Method]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseParameters]'))
ALTER TABLE [dbo].[BaseParameters]  WITH CHECK ADD  CONSTRAINT [FK_BaseParameter_Method_fk_Method] FOREIGN KEY([fk_Method])
REFERENCES [dbo].[Methods] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BaseParameter_Method_fk_Method]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseParameters]'))
ALTER TABLE [dbo].[BaseParameters] CHECK CONSTRAINT [FK_BaseParameter_Method_fk_Method]
GO
/****** Object:  ForeignKey [FK_BaseParameter_Module_fk_Module]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BaseParameter_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseParameters]'))
ALTER TABLE [dbo].[BaseParameters]  WITH CHECK ADD  CONSTRAINT [FK_BaseParameter_Module_fk_Module] FOREIGN KEY([fk_Module])
REFERENCES [dbo].[Modules] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BaseParameter_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseParameters]'))
ALTER TABLE [dbo].[BaseParameters] CHECK CONSTRAINT [FK_BaseParameter_Module_fk_Module]
GO
/****** Object:  ForeignKey [FK_MethodInvocation_DataType_fk_InvokeOnObjectClass]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocation_DataType_fk_InvokeOnObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocations]'))
ALTER TABLE [dbo].[MethodInvocations]  WITH CHECK ADD  CONSTRAINT [FK_MethodInvocation_DataType_fk_InvokeOnObjectClass] FOREIGN KEY([fk_InvokeOnObjectClass])
REFERENCES [dbo].[DataTypes] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocation_DataType_fk_InvokeOnObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocations]'))
ALTER TABLE [dbo].[MethodInvocations] CHECK CONSTRAINT [FK_MethodInvocation_DataType_fk_InvokeOnObjectClass]
GO
/****** Object:  ForeignKey [FK_MethodInvocation_Method_fk_Method]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocation_Method_fk_Method]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocations]'))
ALTER TABLE [dbo].[MethodInvocations]  WITH CHECK ADD  CONSTRAINT [FK_MethodInvocation_Method_fk_Method] FOREIGN KEY([fk_Method])
REFERENCES [dbo].[Methods] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocation_Method_fk_Method]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocations]'))
ALTER TABLE [dbo].[MethodInvocations] CHECK CONSTRAINT [FK_MethodInvocation_Method_fk_Method]
GO
/****** Object:  ForeignKey [FK_MethodInvocation_Module_fk_Module]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocation_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocations]'))
ALTER TABLE [dbo].[MethodInvocations]  WITH CHECK ADD  CONSTRAINT [FK_MethodInvocation_Module_fk_Module] FOREIGN KEY([fk_Module])
REFERENCES [dbo].[Modules] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocation_Module_fk_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocations]'))
ALTER TABLE [dbo].[MethodInvocations] CHECK CONSTRAINT [FK_MethodInvocation_Module_fk_Module]
GO
/****** Object:  ForeignKey [FK_MethodInvocation_TypeRef_fk_Implementor]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocation_TypeRef_fk_Implementor]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocations]'))
ALTER TABLE [dbo].[MethodInvocations]  WITH CHECK ADD  CONSTRAINT [FK_MethodInvocation_TypeRef_fk_Implementor] FOREIGN KEY([fk_Implementor])
REFERENCES [dbo].[TypeRefs] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocation_TypeRef_fk_Implementor]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocations]'))
ALTER TABLE [dbo].[MethodInvocations] CHECK CONSTRAINT [FK_MethodInvocation_TypeRef_fk_Implementor]
GO
/****** Object:  ForeignKey [FK_EnumerationEntry_Enumeration_fk_Enumeration]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EnumerationEntry_Enumeration_fk_Enumeration]') AND parent_object_id = OBJECT_ID(N'[dbo].[EnumerationEntries]'))
ALTER TABLE [dbo].[EnumerationEntries]  WITH CHECK ADD  CONSTRAINT [FK_EnumerationEntry_Enumeration_fk_Enumeration] FOREIGN KEY([fk_Enumeration])
REFERENCES [dbo].[Enumerations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EnumerationEntry_Enumeration_fk_Enumeration]') AND parent_object_id = OBJECT_ID(N'[dbo].[EnumerationEntries]'))
ALTER TABLE [dbo].[EnumerationEntries] CHECK CONSTRAINT [FK_EnumerationEntry_Enumeration_fk_Enumeration]
GO
/****** Object:  ForeignKey [FK_DoubleParameter_BaseParameter_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoubleParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoubleParameters]'))
ALTER TABLE [dbo].[DoubleParameters]  WITH CHECK ADD  CONSTRAINT [FK_DoubleParameter_BaseParameter_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[BaseParameters] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoubleParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoubleParameters]'))
ALTER TABLE [dbo].[DoubleParameters] CHECK CONSTRAINT [FK_DoubleParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_IntParameter_BaseParameter_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_IntParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[IntParameters]'))
ALTER TABLE [dbo].[IntParameters]  WITH CHECK ADD  CONSTRAINT [FK_IntParameter_BaseParameter_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[BaseParameters] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_IntParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[IntParameters]'))
ALTER TABLE [dbo].[IntParameters] CHECK CONSTRAINT [FK_IntParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_DateTimeParameter_BaseParameter_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DateTimeParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[DateTimeParameters]'))
ALTER TABLE [dbo].[DateTimeParameters]  WITH CHECK ADD  CONSTRAINT [FK_DateTimeParameter_BaseParameter_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[BaseParameters] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DateTimeParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[DateTimeParameters]'))
ALTER TABLE [dbo].[DateTimeParameters] CHECK CONSTRAINT [FK_DateTimeParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_BoolParameter_BaseParameter_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BoolParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[BoolParameters]'))
ALTER TABLE [dbo].[BoolParameters]  WITH CHECK ADD  CONSTRAINT [FK_BoolParameter_BaseParameter_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[BaseParameters] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BoolParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[BoolParameters]'))
ALTER TABLE [dbo].[BoolParameters] CHECK CONSTRAINT [FK_BoolParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_CLRObjectParameter_Assembly_fk_Assembly]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLRObjectParameter_Assembly_fk_Assembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLRObjectParameters]'))
ALTER TABLE [dbo].[CLRObjectParameters]  WITH CHECK ADD  CONSTRAINT [FK_CLRObjectParameter_Assembly_fk_Assembly] FOREIGN KEY([fk_Assembly])
REFERENCES [dbo].[Assemblies] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLRObjectParameter_Assembly_fk_Assembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLRObjectParameters]'))
ALTER TABLE [dbo].[CLRObjectParameters] CHECK CONSTRAINT [FK_CLRObjectParameter_Assembly_fk_Assembly]
GO
/****** Object:  ForeignKey [FK_CLRObjectParameter_BaseParameter_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLRObjectParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLRObjectParameters]'))
ALTER TABLE [dbo].[CLRObjectParameters]  WITH CHECK ADD  CONSTRAINT [FK_CLRObjectParameter_BaseParameter_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[BaseParameters] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLRObjectParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLRObjectParameters]'))
ALTER TABLE [dbo].[CLRObjectParameters] CHECK CONSTRAINT [FK_CLRObjectParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_Visual_MenuTreeCollectionEntry_Visual_fk_MenuTree]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_MenuTreeCollectionEntry_Visual_fk_MenuTree]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_MenuTreeCollection]'))
ALTER TABLE [dbo].[Visuals_MenuTreeCollection]  WITH CHECK ADD  CONSTRAINT [FK_Visual_MenuTreeCollectionEntry_Visual_fk_MenuTree] FOREIGN KEY([fk_MenuTree])
REFERENCES [dbo].[Visuals] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_MenuTreeCollectionEntry_Visual_fk_MenuTree]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_MenuTreeCollection]'))
ALTER TABLE [dbo].[Visuals_MenuTreeCollection] CHECK CONSTRAINT [FK_Visual_MenuTreeCollectionEntry_Visual_fk_MenuTree]
GO
/****** Object:  ForeignKey [FK_Visual_MenuTreeCollectionEntry_Visual_fk_Visual]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_MenuTreeCollectionEntry_Visual_fk_Visual]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_MenuTreeCollection]'))
ALTER TABLE [dbo].[Visuals_MenuTreeCollection]  WITH CHECK ADD  CONSTRAINT [FK_Visual_MenuTreeCollectionEntry_Visual_fk_Visual] FOREIGN KEY([fk_Visual])
REFERENCES [dbo].[Visuals] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_MenuTreeCollectionEntry_Visual_fk_Visual]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_MenuTreeCollection]'))
ALTER TABLE [dbo].[Visuals_MenuTreeCollection] CHECK CONSTRAINT [FK_Visual_MenuTreeCollectionEntry_Visual_fk_Visual]
GO
/****** Object:  ForeignKey [FK_Visual_ContextMenuCollectionEntry_Visual_fk_ContextMenu]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_ContextMenuCollectionEntry_Visual_fk_ContextMenu]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_ContextMenuCollection]'))
ALTER TABLE [dbo].[Visuals_ContextMenuCollection]  WITH CHECK ADD  CONSTRAINT [FK_Visual_ContextMenuCollectionEntry_Visual_fk_ContextMenu] FOREIGN KEY([fk_ContextMenu])
REFERENCES [dbo].[Visuals] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_ContextMenuCollectionEntry_Visual_fk_ContextMenu]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_ContextMenuCollection]'))
ALTER TABLE [dbo].[Visuals_ContextMenuCollection] CHECK CONSTRAINT [FK_Visual_ContextMenuCollectionEntry_Visual_fk_ContextMenu]
GO
/****** Object:  ForeignKey [FK_Visual_ContextMenuCollectionEntry_Visual_fk_Visual]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_ContextMenuCollectionEntry_Visual_fk_Visual]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_ContextMenuCollection]'))
ALTER TABLE [dbo].[Visuals_ContextMenuCollection]  WITH CHECK ADD  CONSTRAINT [FK_Visual_ContextMenuCollectionEntry_Visual_fk_Visual] FOREIGN KEY([fk_Visual])
REFERENCES [dbo].[Visuals] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_ContextMenuCollectionEntry_Visual_fk_Visual]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_ContextMenuCollection]'))
ALTER TABLE [dbo].[Visuals_ContextMenuCollection] CHECK CONSTRAINT [FK_Visual_ContextMenuCollectionEntry_Visual_fk_Visual]
GO
/****** Object:  ForeignKey [FK_Visual_ChildrenCollectionEntry_Visual_fk_Children]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_ChildrenCollectionEntry_Visual_fk_Children]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_ChildrenCollection]'))
ALTER TABLE [dbo].[Visuals_ChildrenCollection]  WITH CHECK ADD  CONSTRAINT [FK_Visual_ChildrenCollectionEntry_Visual_fk_Children] FOREIGN KEY([fk_Children])
REFERENCES [dbo].[Visuals] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_ChildrenCollectionEntry_Visual_fk_Children]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_ChildrenCollection]'))
ALTER TABLE [dbo].[Visuals_ChildrenCollection] CHECK CONSTRAINT [FK_Visual_ChildrenCollectionEntry_Visual_fk_Children]
GO
/****** Object:  ForeignKey [FK_Visual_ChildrenCollectionEntry_Visual_fk_Visual]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_ChildrenCollectionEntry_Visual_fk_Visual]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_ChildrenCollection]'))
ALTER TABLE [dbo].[Visuals_ChildrenCollection]  WITH CHECK ADD  CONSTRAINT [FK_Visual_ChildrenCollectionEntry_Visual_fk_Visual] FOREIGN KEY([fk_Visual])
REFERENCES [dbo].[Visuals] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Visual_ChildrenCollectionEntry_Visual_fk_Visual]') AND parent_object_id = OBJECT_ID(N'[dbo].[Visuals_ChildrenCollection]'))
ALTER TABLE [dbo].[Visuals_ChildrenCollection] CHECK CONSTRAINT [FK_Visual_ChildrenCollectionEntry_Visual_fk_Visual]
GO
/****** Object:  ForeignKey [FK_Template_Assembly_fk_DisplayedTypeAssembly]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_Assembly_fk_DisplayedTypeAssembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[Templates]'))
ALTER TABLE [dbo].[Templates]  WITH CHECK ADD  CONSTRAINT [FK_Template_Assembly_fk_DisplayedTypeAssembly] FOREIGN KEY([fk_DisplayedTypeAssembly])
REFERENCES [dbo].[Assemblies] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_Assembly_fk_DisplayedTypeAssembly]') AND parent_object_id = OBJECT_ID(N'[dbo].[Templates]'))
ALTER TABLE [dbo].[Templates] CHECK CONSTRAINT [FK_Template_Assembly_fk_DisplayedTypeAssembly]
GO
/****** Object:  ForeignKey [FK_Template_Visual_fk_VisualTree]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_Visual_fk_VisualTree]') AND parent_object_id = OBJECT_ID(N'[dbo].[Templates]'))
ALTER TABLE [dbo].[Templates]  WITH CHECK ADD  CONSTRAINT [FK_Template_Visual_fk_VisualTree] FOREIGN KEY([fk_VisualTree])
REFERENCES [dbo].[Visuals] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_Visual_fk_VisualTree]') AND parent_object_id = OBJECT_ID(N'[dbo].[Templates]'))
ALTER TABLE [dbo].[Templates] CHECK CONSTRAINT [FK_Template_Visual_fk_VisualTree]
GO
/****** Object:  ForeignKey [FK_ViewDescriptor_TypeRef_fk_ControlRef]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ViewDescriptor_TypeRef_fk_ControlRef]') AND parent_object_id = OBJECT_ID(N'[dbo].[ViewDescriptors]'))
ALTER TABLE [dbo].[ViewDescriptors]  WITH CHECK ADD  CONSTRAINT [FK_ViewDescriptor_TypeRef_fk_ControlRef] FOREIGN KEY([fk_ControlRef])
REFERENCES [dbo].[TypeRefs] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ViewDescriptor_TypeRef_fk_ControlRef]') AND parent_object_id = OBJECT_ID(N'[dbo].[ViewDescriptors]'))
ALTER TABLE [dbo].[ViewDescriptors] CHECK CONSTRAINT [FK_ViewDescriptor_TypeRef_fk_ControlRef]
GO
/****** Object:  ForeignKey [FK_ViewDescriptor_TypeRef_fk_PresentedModelDescriptor]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ViewDescriptor_TypeRef_fk_PresentedModelDescriptor]') AND parent_object_id = OBJECT_ID(N'[dbo].[ViewDescriptors]'))
ALTER TABLE [dbo].[ViewDescriptors]  WITH CHECK ADD  CONSTRAINT [FK_ViewDescriptor_TypeRef_fk_PresentedModelDescriptor] FOREIGN KEY([fk_PresentedModelDescriptor])
REFERENCES [dbo].[PresentableModelDescriptors] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ViewDescriptor_TypeRef_fk_PresentedModelDescriptor]') AND parent_object_id = OBJECT_ID(N'[dbo].[ViewDescriptors]'))
ALTER TABLE [dbo].[ViewDescriptors] CHECK CONSTRAINT [FK_ViewDescriptor_TypeRef_fk_PresentedModelDescriptor]
GO
/****** Object:  ForeignKey [FK_ObjectClass_DataType_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_DataType_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses]'))
ALTER TABLE [dbo].[ObjectClasses]  WITH CHECK ADD  CONSTRAINT [FK_ObjectClass_DataType_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[DataTypes] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_DataType_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses]'))
ALTER TABLE [dbo].[ObjectClasses] CHECK CONSTRAINT [FK_ObjectClass_DataType_ID]
GO
/****** Object:  ForeignKey [FK_ObjectClass_ObjectClass_fk_BaseObjectClass]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_ObjectClass_fk_BaseObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses]'))
ALTER TABLE [dbo].[ObjectClasses]  WITH CHECK ADD  CONSTRAINT [FK_ObjectClass_ObjectClass_fk_BaseObjectClass] FOREIGN KEY([fk_BaseObjectClass])
REFERENCES [dbo].[ObjectClasses] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_ObjectClass_fk_BaseObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses]'))
ALTER TABLE [dbo].[ObjectClasses] CHECK CONSTRAINT [FK_ObjectClass_ObjectClass_fk_BaseObjectClass]
GO
/****** Object:  ForeignKey [FK_ObjectClass_PresentableModelDescriptor_fk_DefaultPresentableModelDescriptor]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_PresentableModelDescriptor_fk_DefaultPresentableModelDescriptor]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses]'))
ALTER TABLE [dbo].[ObjectClasses]  WITH CHECK ADD  CONSTRAINT [FK_ObjectClass_PresentableModelDescriptor_fk_DefaultPresentableModelDescriptor] FOREIGN KEY([fk_DefaultPresentableModelDescriptor])
REFERENCES [dbo].[PresentableModelDescriptors] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_PresentableModelDescriptor_fk_DefaultPresentableModelDescriptor]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses]'))
ALTER TABLE [dbo].[ObjectClasses] CHECK CONSTRAINT [FK_ObjectClass_PresentableModelDescriptor_fk_DefaultPresentableModelDescriptor]
GO
/****** Object:  ForeignKey [FK_ObjectClass_TypeRef_fk_DefaultModel]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_TypeRef_fk_DefaultModel]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses]'))
ALTER TABLE [dbo].[ObjectClasses]  WITH CHECK ADD  CONSTRAINT [FK_ObjectClass_TypeRef_fk_DefaultModel] FOREIGN KEY([fk_DefaultModel])
REFERENCES [dbo].[TypeRefs] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_TypeRef_fk_DefaultModel]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses]'))
ALTER TABLE [dbo].[ObjectClasses] CHECK CONSTRAINT [FK_ObjectClass_TypeRef_fk_DefaultModel]
GO
/****** Object:  ForeignKey [FK_Properties_DataTypes]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Properties_DataTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[Properties]'))
ALTER TABLE [dbo].[Properties]  WITH CHECK ADD  CONSTRAINT [FK_Properties_DataTypes] FOREIGN KEY([fk_ObjectClass])
REFERENCES [dbo].[DataTypes] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Properties_DataTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[Properties]'))
ALTER TABLE [dbo].[Properties] CHECK CONSTRAINT [FK_Properties_DataTypes]
GO
/****** Object:  ForeignKey [FK_Property_PresentableModelDescriptor_fk_ValueModelDescriptor]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Property_PresentableModelDescriptor_fk_ValueModelDescriptor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Properties]'))
ALTER TABLE [dbo].[Properties]  WITH CHECK ADD  CONSTRAINT [FK_Property_PresentableModelDescriptor_fk_ValueModelDescriptor] FOREIGN KEY([fk_ValueModelDescriptor])
REFERENCES [dbo].[PresentableModelDescriptors] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Property_PresentableModelDescriptor_fk_ValueModelDescriptor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Properties]'))
ALTER TABLE [dbo].[Properties] CHECK CONSTRAINT [FK_Property_PresentableModelDescriptor_fk_ValueModelDescriptor]
GO
/****** Object:  ForeignKey [FK_StringParameter_BaseParameter_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StringParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StringParameters]'))
ALTER TABLE [dbo].[StringParameters]  WITH CHECK ADD  CONSTRAINT [FK_StringParameter_BaseParameter_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[BaseParameters] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StringParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StringParameters]'))
ALTER TABLE [dbo].[StringParameters] CHECK CONSTRAINT [FK_StringParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_ObjectParameter_BaseParameter_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectParameters]'))
ALTER TABLE [dbo].[ObjectParameters]  WITH CHECK ADD  CONSTRAINT [FK_ObjectParameter_BaseParameter_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[BaseParameters] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectParameter_BaseParameter_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectParameters]'))
ALTER TABLE [dbo].[ObjectParameters] CHECK CONSTRAINT [FK_ObjectParameter_BaseParameter_ID]
GO
/****** Object:  ForeignKey [FK_ObjectParameter_DataType_fk_DataType]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectParameter_DataType_fk_DataType]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectParameters]'))
ALTER TABLE [dbo].[ObjectParameters]  WITH CHECK ADD  CONSTRAINT [FK_ObjectParameter_DataType_fk_DataType] FOREIGN KEY([fk_DataType])
REFERENCES [dbo].[DataTypes] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectParameter_DataType_fk_DataType]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectParameters]'))
ALTER TABLE [dbo].[ObjectParameters] CHECK CONSTRAINT [FK_ObjectParameter_DataType_fk_DataType]
GO
/****** Object:  ForeignKey [FK_ObjectReferenceProperty_ObjectClass_fk_ReferenceObjectClass]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectReferenceProperty_ObjectClass_fk_ReferenceObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectReferenceProperties]'))
ALTER TABLE [dbo].[ObjectReferenceProperties]  WITH CHECK ADD  CONSTRAINT [FK_ObjectReferenceProperty_ObjectClass_fk_ReferenceObjectClass] FOREIGN KEY([fk_ReferenceObjectClass])
REFERENCES [dbo].[ObjectClasses] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectReferenceProperty_ObjectClass_fk_ReferenceObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectReferenceProperties]'))
ALTER TABLE [dbo].[ObjectReferenceProperties] CHECK CONSTRAINT [FK_ObjectReferenceProperty_ObjectClass_fk_ReferenceObjectClass]
GO
/****** Object:  ForeignKey [FK_ObjectReferenceProperty_Property_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectReferenceProperty_Property_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectReferenceProperties]'))
ALTER TABLE [dbo].[ObjectReferenceProperties]  WITH CHECK ADD  CONSTRAINT [FK_ObjectReferenceProperty_Property_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[Properties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectReferenceProperty_Property_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectReferenceProperties]'))
ALTER TABLE [dbo].[ObjectReferenceProperties] CHECK CONSTRAINT [FK_ObjectReferenceProperty_Property_ID]
GO
/****** Object:  ForeignKey [FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_fk_ImplementsInterfaces]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_fk_ImplementsInterfaces]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses_ImplementsInterfacesCollection]'))
ALTER TABLE [dbo].[ObjectClasses_ImplementsInterfacesCollection]  WITH CHECK ADD  CONSTRAINT [FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_fk_ImplementsInterfaces] FOREIGN KEY([fk_ImplementsInterfaces])
REFERENCES [dbo].[Interfaces] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_fk_ImplementsInterfaces]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses_ImplementsInterfacesCollection]'))
ALTER TABLE [dbo].[ObjectClasses_ImplementsInterfacesCollection] CHECK CONSTRAINT [FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_fk_ImplementsInterfaces]
GO
/****** Object:  ForeignKey [FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_ObjectClass]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_ObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses_ImplementsInterfacesCollection]'))
ALTER TABLE [dbo].[ObjectClasses_ImplementsInterfacesCollection]  WITH CHECK ADD  CONSTRAINT [FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_ObjectClass] FOREIGN KEY([fk_ObjectClass])
REFERENCES [dbo].[ObjectClasses] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_ObjectClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[ObjectClasses_ImplementsInterfacesCollection]'))
ALTER TABLE [dbo].[ObjectClasses_ImplementsInterfacesCollection] CHECK CONSTRAINT [FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_ObjectClass]
GO
/****** Object:  ForeignKey [FK_RelationEnd_ObjectClass_fk_Type]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RelationEnd_ObjectClass_fk_Type]') AND parent_object_id = OBJECT_ID(N'[dbo].[RelationEnds]'))
ALTER TABLE [dbo].[RelationEnds]  WITH CHECK ADD  CONSTRAINT [FK_RelationEnd_ObjectClass_fk_Type] FOREIGN KEY([fk_Type])
REFERENCES [dbo].[ObjectClasses] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RelationEnd_ObjectClass_fk_Type]') AND parent_object_id = OBJECT_ID(N'[dbo].[RelationEnds]'))
ALTER TABLE [dbo].[RelationEnds] CHECK CONSTRAINT [FK_RelationEnd_ObjectClass_fk_Type]
GO
/****** Object:  ForeignKey [FK_RelationEnd_Property_fk_Navigator]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RelationEnd_Property_fk_Navigator]') AND parent_object_id = OBJECT_ID(N'[dbo].[RelationEnds]'))
ALTER TABLE [dbo].[RelationEnds]  WITH CHECK ADD  CONSTRAINT [FK_RelationEnd_Property_fk_Navigator] FOREIGN KEY([fk_Navigator])
REFERENCES [dbo].[Properties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RelationEnd_Property_fk_Navigator]') AND parent_object_id = OBJECT_ID(N'[dbo].[RelationEnds]'))
ALTER TABLE [dbo].[RelationEnds] CHECK CONSTRAINT [FK_RelationEnd_Property_fk_Navigator]
GO
/****** Object:  ForeignKey [FK_ValueTypeProperty_Property_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ValueTypeProperty_Property_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ValueTypeProperties]'))
ALTER TABLE [dbo].[ValueTypeProperties]  WITH CHECK ADD  CONSTRAINT [FK_ValueTypeProperty_Property_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[Properties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ValueTypeProperty_Property_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ValueTypeProperties]'))
ALTER TABLE [dbo].[ValueTypeProperties] CHECK CONSTRAINT [FK_ValueTypeProperty_Property_ID]
GO
/****** Object:  ForeignKey [FK_Template_MenuCollectionEntry_Template_fk_Template]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_MenuCollectionEntry_Template_fk_Template]') AND parent_object_id = OBJECT_ID(N'[dbo].[Templates_MenuCollection]'))
ALTER TABLE [dbo].[Templates_MenuCollection]  WITH CHECK ADD  CONSTRAINT [FK_Template_MenuCollectionEntry_Template_fk_Template] FOREIGN KEY([fk_Template])
REFERENCES [dbo].[Templates] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_MenuCollectionEntry_Template_fk_Template]') AND parent_object_id = OBJECT_ID(N'[dbo].[Templates_MenuCollection]'))
ALTER TABLE [dbo].[Templates_MenuCollection] CHECK CONSTRAINT [FK_Template_MenuCollectionEntry_Template_fk_Template]
GO
/****** Object:  ForeignKey [FK_Template_MenuCollectionEntry_Visual_fk_Menu]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_MenuCollectionEntry_Visual_fk_Menu]') AND parent_object_id = OBJECT_ID(N'[dbo].[Templates_MenuCollection]'))
ALTER TABLE [dbo].[Templates_MenuCollection]  WITH CHECK ADD  CONSTRAINT [FK_Template_MenuCollectionEntry_Visual_fk_Menu] FOREIGN KEY([fk_Menu])
REFERENCES [dbo].[Visuals] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_MenuCollectionEntry_Visual_fk_Menu]') AND parent_object_id = OBJECT_ID(N'[dbo].[Templates_MenuCollection]'))
ALTER TABLE [dbo].[Templates_MenuCollection] CHECK CONSTRAINT [FK_Template_MenuCollectionEntry_Visual_fk_Menu]
GO
/****** Object:  ForeignKey [FK_StructProperty_Property_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StructProperty_Property_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StructProperties]'))
ALTER TABLE [dbo].[StructProperties]  WITH CHECK ADD  CONSTRAINT [FK_StructProperty_Property_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[Properties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StructProperty_Property_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StructProperties]'))
ALTER TABLE [dbo].[StructProperties] CHECK CONSTRAINT [FK_StructProperty_Property_ID]
GO
/****** Object:  ForeignKey [FK_StructProperty_Struct_fk_StructDefinition]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StructProperty_Struct_fk_StructDefinition]') AND parent_object_id = OBJECT_ID(N'[dbo].[StructProperties]'))
ALTER TABLE [dbo].[StructProperties]  WITH CHECK ADD  CONSTRAINT [FK_StructProperty_Struct_fk_StructDefinition] FOREIGN KEY([fk_StructDefinition])
REFERENCES [dbo].[Structs] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StructProperty_Struct_fk_StructDefinition]') AND parent_object_id = OBJECT_ID(N'[dbo].[StructProperties]'))
ALTER TABLE [dbo].[StructProperties] CHECK CONSTRAINT [FK_StructProperty_Struct_fk_StructDefinition]
GO
/****** Object:  ForeignKey [FK_Constraints_Properties]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Constraints_Properties]') AND parent_object_id = OBJECT_ID(N'[dbo].[Constraints]'))
ALTER TABLE [dbo].[Constraints]  WITH CHECK ADD  CONSTRAINT [FK_Constraints_Properties] FOREIGN KEY([fk_ConstrainedProperty])
REFERENCES [dbo].[Properties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Constraints_Properties]') AND parent_object_id = OBJECT_ID(N'[dbo].[Constraints]'))
ALTER TABLE [dbo].[Constraints] CHECK CONSTRAINT [FK_Constraints_Properties]
GO
/****** Object:  ForeignKey [FK_MethodInvocationConstraint_Constraint_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocationConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocationConstraints]'))
ALTER TABLE [dbo].[MethodInvocationConstraints]  WITH CHECK ADD  CONSTRAINT [FK_MethodInvocationConstraint_Constraint_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[Constraints] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MethodInvocationConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[MethodInvocationConstraints]'))
ALTER TABLE [dbo].[MethodInvocationConstraints] CHECK CONSTRAINT [FK_MethodInvocationConstraint_Constraint_ID]
GO
/****** Object:  ForeignKey [FK_IsValidIdentifierConstraint_Constraint_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_IsValidIdentifierConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[IsValidIdentifierConstraints]'))
ALTER TABLE [dbo].[IsValidIdentifierConstraints]  WITH CHECK ADD  CONSTRAINT [FK_IsValidIdentifierConstraint_Constraint_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[Constraints] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_IsValidIdentifierConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[IsValidIdentifierConstraints]'))
ALTER TABLE [dbo].[IsValidIdentifierConstraints] CHECK CONSTRAINT [FK_IsValidIdentifierConstraint_Constraint_ID]
GO
/****** Object:  ForeignKey [FK_IntProperty_ValueTypeProperty_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_IntProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[IntProperties]'))
ALTER TABLE [dbo].[IntProperties]  WITH CHECK ADD  CONSTRAINT [FK_IntProperty_ValueTypeProperty_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[ValueTypeProperties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_IntProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[IntProperties]'))
ALTER TABLE [dbo].[IntProperties] CHECK CONSTRAINT [FK_IntProperty_ValueTypeProperty_ID]
GO
/****** Object:  ForeignKey [FK_IntegerRangeConstraint_Constraint_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_IntegerRangeConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[IntegerRangeConstraints]'))
ALTER TABLE [dbo].[IntegerRangeConstraints]  WITH CHECK ADD  CONSTRAINT [FK_IntegerRangeConstraint_Constraint_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[Constraints] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_IntegerRangeConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[IntegerRangeConstraints]'))
ALTER TABLE [dbo].[IntegerRangeConstraints] CHECK CONSTRAINT [FK_IntegerRangeConstraint_Constraint_ID]
GO
/****** Object:  ForeignKey [FK_GuidProperties_ValueTypeProperties]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GuidProperties_ValueTypeProperties]') AND parent_object_id = OBJECT_ID(N'[dbo].[GuidProperties]'))
ALTER TABLE [dbo].[GuidProperties]  WITH CHECK ADD  CONSTRAINT [FK_GuidProperties_ValueTypeProperties] FOREIGN KEY([ID])
REFERENCES [dbo].[ValueTypeProperties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GuidProperties_ValueTypeProperties]') AND parent_object_id = OBJECT_ID(N'[dbo].[GuidProperties]'))
ALTER TABLE [dbo].[GuidProperties] CHECK CONSTRAINT [FK_GuidProperties_ValueTypeProperties]
GO
/****** Object:  ForeignKey [FK_EnumerationProperty_Enumeration_fk_Enumeration]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EnumerationProperty_Enumeration_fk_Enumeration]') AND parent_object_id = OBJECT_ID(N'[dbo].[EnumerationProperties]'))
ALTER TABLE [dbo].[EnumerationProperties]  WITH CHECK ADD  CONSTRAINT [FK_EnumerationProperty_Enumeration_fk_Enumeration] FOREIGN KEY([fk_Enumeration])
REFERENCES [dbo].[Enumerations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EnumerationProperty_Enumeration_fk_Enumeration]') AND parent_object_id = OBJECT_ID(N'[dbo].[EnumerationProperties]'))
ALTER TABLE [dbo].[EnumerationProperties] CHECK CONSTRAINT [FK_EnumerationProperty_Enumeration_fk_Enumeration]
GO
/****** Object:  ForeignKey [FK_EnumerationProperty_ValueTypeProperty_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EnumerationProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[EnumerationProperties]'))
ALTER TABLE [dbo].[EnumerationProperties]  WITH CHECK ADD  CONSTRAINT [FK_EnumerationProperty_ValueTypeProperty_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[ValueTypeProperties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EnumerationProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[EnumerationProperties]'))
ALTER TABLE [dbo].[EnumerationProperties] CHECK CONSTRAINT [FK_EnumerationProperty_ValueTypeProperty_ID]
GO
/****** Object:  ForeignKey [FK_DoubleProperty_ValueTypeProperty_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoubleProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoubleProperties]'))
ALTER TABLE [dbo].[DoubleProperties]  WITH CHECK ADD  CONSTRAINT [FK_DoubleProperty_ValueTypeProperty_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[ValueTypeProperties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoubleProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoubleProperties]'))
ALTER TABLE [dbo].[DoubleProperties] CHECK CONSTRAINT [FK_DoubleProperty_ValueTypeProperty_ID]
GO
/****** Object:  ForeignKey [FK_DateTimeProperty_ValueTypeProperty_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DateTimeProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[DateTimeProperties]'))
ALTER TABLE [dbo].[DateTimeProperties]  WITH CHECK ADD  CONSTRAINT [FK_DateTimeProperty_ValueTypeProperty_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[ValueTypeProperties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DateTimeProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[DateTimeProperties]'))
ALTER TABLE [dbo].[DateTimeProperties] CHECK CONSTRAINT [FK_DateTimeProperty_ValueTypeProperty_ID]
GO
/****** Object:  ForeignKey [FK_BoolProperty_ValueTypeProperty_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BoolProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[BoolProperties]'))
ALTER TABLE [dbo].[BoolProperties]  WITH CHECK ADD  CONSTRAINT [FK_BoolProperty_ValueTypeProperty_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[ValueTypeProperties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BoolProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[BoolProperties]'))
ALTER TABLE [dbo].[BoolProperties] CHECK CONSTRAINT [FK_BoolProperty_ValueTypeProperty_ID]
GO
/****** Object:  ForeignKey [FK_StringRangeConstraint_Constraint_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StringRangeConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StringRangeConstraints]'))
ALTER TABLE [dbo].[StringRangeConstraints]  WITH CHECK ADD  CONSTRAINT [FK_StringRangeConstraint_Constraint_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[Constraints] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StringRangeConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StringRangeConstraints]'))
ALTER TABLE [dbo].[StringRangeConstraints] CHECK CONSTRAINT [FK_StringRangeConstraint_Constraint_ID]
GO
/****** Object:  ForeignKey [FK_StringProperty_ValueTypeProperty_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StringProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StringProperties]'))
ALTER TABLE [dbo].[StringProperties]  WITH CHECK ADD  CONSTRAINT [FK_StringProperty_ValueTypeProperty_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[ValueTypeProperties] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StringProperty_ValueTypeProperty_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StringProperties]'))
ALTER TABLE [dbo].[StringProperties] CHECK CONSTRAINT [FK_StringProperty_ValueTypeProperty_ID]
GO
/****** Object:  ForeignKey [FK_StringConstraint_Constraint_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StringConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StringConstraints]'))
ALTER TABLE [dbo].[StringConstraints]  WITH CHECK ADD  CONSTRAINT [FK_StringConstraint_Constraint_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[Constraints] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StringConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StringConstraints]'))
ALTER TABLE [dbo].[StringConstraints] CHECK CONSTRAINT [FK_StringConstraint_Constraint_ID]
GO
/****** Object:  ForeignKey [FK_Relation_RelationEnd_fk_A]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Relation_RelationEnd_fk_A]') AND parent_object_id = OBJECT_ID(N'[dbo].[Relations]'))
ALTER TABLE [dbo].[Relations]  WITH CHECK ADD  CONSTRAINT [FK_Relation_RelationEnd_fk_A] FOREIGN KEY([fk_A])
REFERENCES [dbo].[RelationEnds] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Relation_RelationEnd_fk_A]') AND parent_object_id = OBJECT_ID(N'[dbo].[Relations]'))
ALTER TABLE [dbo].[Relations] CHECK CONSTRAINT [FK_Relation_RelationEnd_fk_A]
GO
/****** Object:  ForeignKey [FK_Relation_RelationEnd_fk_B]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Relation_RelationEnd_fk_B]') AND parent_object_id = OBJECT_ID(N'[dbo].[Relations]'))
ALTER TABLE [dbo].[Relations]  WITH CHECK ADD  CONSTRAINT [FK_Relation_RelationEnd_fk_B] FOREIGN KEY([fk_B])
REFERENCES [dbo].[RelationEnds] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Relation_RelationEnd_fk_B]') AND parent_object_id = OBJECT_ID(N'[dbo].[Relations]'))
ALTER TABLE [dbo].[Relations] CHECK CONSTRAINT [FK_Relation_RelationEnd_fk_B]
GO
/****** Object:  ForeignKey [FK_NotNullableConstraint_Constraint_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NotNullableConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[NotNullableConstraints]'))
ALTER TABLE [dbo].[NotNullableConstraints]  WITH CHECK ADD  CONSTRAINT [FK_NotNullableConstraint_Constraint_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[Constraints] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NotNullableConstraint_Constraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[NotNullableConstraints]'))
ALTER TABLE [dbo].[NotNullableConstraints] CHECK CONSTRAINT [FK_NotNullableConstraint_Constraint_ID]
GO
/****** Object:  ForeignKey [FK_IsValidNamespaceConstraint_IsValidIdentifierConstraint_ID]    Script Date: 05/22/2009 17:30:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_IsValidNamespaceConstraint_IsValidIdentifierConstraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[IsValidNamespaceConstraints]'))
ALTER TABLE [dbo].[IsValidNamespaceConstraints]  WITH CHECK ADD  CONSTRAINT [FK_IsValidNamespaceConstraint_IsValidIdentifierConstraint_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[IsValidIdentifierConstraints] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_IsValidNamespaceConstraint_IsValidIdentifierConstraint_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[IsValidNamespaceConstraints]'))
ALTER TABLE [dbo].[IsValidNamespaceConstraints] CHECK CONSTRAINT [FK_IsValidNamespaceConstraint_IsValidIdentifierConstraint_ID]
GO
