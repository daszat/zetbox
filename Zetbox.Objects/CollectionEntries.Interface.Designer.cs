// <autogenerated/>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

using Zetbox.API;

// <autogenerated/>

namespace Zetbox.App.Base
{
    using Zetbox.API;
    using Zetbox.App.Base;

    /// <summary>
    /// ObjectCollectionEntry for on which other properties this one depends (used for change notification)
    /// </summary>
    [Zetbox.API.DefinitionGuid("47595643-e8d0-48ef-82c7-2d24de8a784e")]
    public interface CalculatedObjectReferenceProperty_dependsOn_Property_RelationEntry : IRelationEntry<CalculatedObjectReferenceProperty, Property> 
    {

    }
}// <autogenerated/>

namespace Zetbox.App.Base
{
    using Zetbox.API;
    using Zetbox.App.Base;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    [Zetbox.API.DefinitionGuid("692c1064-37a2-4be3-a81e-4cb91f673aa3")]
    public interface DataType_implements_Interface_RelationEntry : IRelationEntry<DataType, Interface> 
    {

    }
}// <autogenerated/>

namespace at.dasz.DocumentManagement
{
    using Zetbox.API;
    using at.dasz.DocumentManagement;
    using Zetbox.App.Base;

    /// <summary>
    /// ObjectCollectionEntry for Document has Revisions
    /// </summary>
    [Zetbox.API.DefinitionGuid("69d27812-e981-443b-a94b-dfe1a95f3aad")]
    public interface Document_has_Blob_RelationEntry : IRelationListEntry<Document, Blob> 
    {

    }
}// <autogenerated/>

namespace Zetbox.App.Base
{
    using Zetbox.API;
    using Zetbox.App.Base;

    /// <summary>
    /// ObjectCollectionEntry for Identites are member of Groups
    /// </summary>
    [Zetbox.API.DefinitionGuid("3efb7ae8-ba6b-40e3-9482-b45d1c101743")]
    public interface Identity_memberOf_Group_RelationEntry : IRelationEntry<Identity, Group> 
    {

    }
}// <autogenerated/>

namespace Zetbox.App.Base
{
    using Zetbox.API;
    using Zetbox.App.Base;

    /// <summary>
    /// ObjectCollectionEntry for UniqueContraints ensures uniqueness on one or more properties
    /// </summary>
    [Zetbox.API.DefinitionGuid("29235ba6-5979-4ed8-8e75-6bd0837c7f28")]
    public interface IndexConstraint_ensures_unique_on_Property_RelationEntry : IRelationEntry<IndexConstraint, Property> 
    {

    }
}// <autogenerated/>

namespace Zetbox.App.Test
{
    using Zetbox.API;
    using Zetbox.App.Test;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    [Zetbox.API.DefinitionGuid("d1e0da3e-ce64-4587-b62d-70c0f4371d97")]
    public interface Muhblah_has_TestCustomObject_RelationEntry : IRelationEntry<Muhblah, TestCustomObject> 
    {

    }
}// <autogenerated/>

namespace Zetbox.App.Test
{
    using Zetbox.API;
    using Zetbox.App.Test;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    [Zetbox.API.DefinitionGuid("3555da6e-0e9b-4f7c-903e-a51f3cce7cd9")]
    public interface N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntry : IRelationEntry<N_to_M_relations_A, N_to_M_relations_B> 
    {

    }
}// <autogenerated/>

namespace Zetbox.App.GUI
{
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.GUI;

    /// <summary>
    /// ObjectCollectionEntry for These Groups are allowed to see this Screen
    /// </summary>
    [Zetbox.API.DefinitionGuid("b88c12ac-eabe-4aee-913e-5edd9d2a193a")]
    public interface NavigationEntry_accessed_by_Group_RelationEntry : IRelationEntry<NavigationEntry, Group> 
    {

    }
}// <autogenerated/>

namespace Zetbox.App.GUI
{
    using Zetbox.API;
    using Zetbox.App.Base;

    /// <summary>
    /// ObjectCollectionEntry for An ObjRef Property can show additional methods
    /// </summary>
    [Zetbox.API.DefinitionGuid("02b3e9d5-fc2e-4ffe-8867-0977b88437cc")]
    public interface ObjectReferenceProperty_shows_Method_RelationEntry : IRelationEntry<ObjectReferenceProperty, Method> 
    {

    }
}// <autogenerated/>

namespace Zetbox.App.Projekte
{
    using Zetbox.API;
    using Zetbox.App.Projekte;

    /// <summary>
    /// ObjectCollectionEntry for Projekte werden von Mitarbeitern durchgeführt
    /// </summary>
    [Zetbox.API.DefinitionGuid("c7b3cf10-cdc8-454c-826c-04a0f7e5ef3e")]
    public interface Projekt_haben_Mitarbeiter_RelationEntry : IRelationListEntry<Projekt, Mitarbeiter> 
    {

    }
}// <autogenerated/>

namespace Zetbox.App.Base
{
    using Zetbox.API;
    using Zetbox.App.Base;

    /// <summary>
    /// ObjectCollectionEntry for RoleMembership needs Relations to resolve Roles
    /// </summary>
    [Zetbox.API.DefinitionGuid("f74d425f-e733-4cba-baca-f4a05fbc0a80")]
    public interface RoleMembership_resolves_Relation_RelationEntry : IRelationListEntry<RoleMembership, Relation> 
    {

    }
}// <autogenerated/>

namespace Zetbox.App.SchemaMigration
{
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.SchemaMigration;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    [Zetbox.API.DefinitionGuid("fb27e3f8-3615-4f3b-ae2a-2b89b8782e27")]
    public interface SourceColumn_created_Property_RelationEntry : IRelationListEntry<SourceColumn, Property> 
    {

    }
}// <autogenerated/>

namespace Zetbox.App.GUI
{
    using Zetbox.API;
    using Zetbox.App.GUI;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    [Zetbox.API.DefinitionGuid("81ff3089-57da-478c-8be5-fd23abc222a2")]
    public interface Template_hasMenu_Visual_RelationEntry : IRelationEntry<Template, Visual> 
    {

    }
}// <autogenerated/>

namespace Zetbox.App.Test
{
    using Zetbox.API;
    using Zetbox.App.Test;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    [Zetbox.API.DefinitionGuid("6819ca86-571c-4d59-bc30-cc1fb0decc9e")]
    public interface TestStudent_füllt_aus_Fragebogen_RelationEntry : IRelationEntry<TestStudent, Fragebogen> 
    {

    }
}// <autogenerated/>

namespace Zetbox.App.Base
{
    using Zetbox.API;
    using Zetbox.App.Base;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    [Zetbox.API.DefinitionGuid("8b41ffa4-8ffa-4d96-b4e5-708188045c71")]
    public interface TypeRef_hasGenericArguments_TypeRef_RelationEntry : IRelationListEntry<TypeRef, TypeRef> 
    {

    }
}// <autogenerated/>

namespace Zetbox.App.GUI
{
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.GUI;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    [Zetbox.API.DefinitionGuid("786dae2f-cb6e-454d-93fd-192541df928d")]
    public interface ViewDescriptor_supports_TypeRef_RelationEntry : IRelationEntry<ViewDescriptor, TypeRef> 
    {

    }
}// <autogenerated/>

namespace Zetbox.App.GUI
{
    using Zetbox.API;
    using Zetbox.App.GUI;

    /// <summary>
    /// ObjectCollectionEntry for a list of additional control kinds for displaying this model
    /// </summary>
    [Zetbox.API.DefinitionGuid("5404456a-4527-4e40-a660-b4a5e96e4a47")]
    public interface ViewModelDescriptor_displayedBy_ControlKind_RelationEntry : IRelationEntry<ViewModelDescriptor, ControlKind> 
    {

    }
}// <autogenerated/>

namespace Zetbox.App.GUI
{
    using Zetbox.API;
    using Zetbox.App.GUI;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    [Zetbox.API.DefinitionGuid("4d4e1ffd-f362-40e2-9fe1-0711ded83241")]
    public interface Visual_contains_Visual_RelationEntry : IRelationEntry<Visual, Visual> 
    {

    }
}// <autogenerated/>

namespace Zetbox.App.GUI
{
    using Zetbox.API;
    using Zetbox.App.GUI;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    [Zetbox.API.DefinitionGuid("358c14b9-fef5-495d-8d44-04e84186830e")]
    public interface Visual_hasContextMenu_Visual_RelationEntry : IRelationEntry<Visual, Visual> 
    {

    }
}// <autogenerated/>

namespace Zetbox.App.Projekte
{
    using Zetbox.API;
    using Zetbox.App.Base;

    /// <summary>
    /// ValueCollectionEntry for EMails des Kunden - können mehrere sein
    /// </summary>
    [Zetbox.API.DefinitionGuid("1d0f6da6-4b69-48d7-9e94-bfb5466654b9")]
    public interface Kunde_EMails_CollectionEntry : IValueCollectionEntry<Kunde, string> 
    {

    }
}// <autogenerated/>

namespace Zetbox.App.Test
{
    using Zetbox.API;
    using Zetbox.App.Base;

    /// <summary>
    /// ValueCollectionEntry for 
    /// </summary>
    [Zetbox.API.DefinitionGuid("0c0c1101-118a-4ce2-806c-d30a03b19dde")]
    public interface TestCustomObject_PhoneNumbersOther_CollectionEntry : IValueCollectionEntry<TestCustomObject, Zetbox.App.Test.TestPhoneCompoundObject> 
    {

    }
}