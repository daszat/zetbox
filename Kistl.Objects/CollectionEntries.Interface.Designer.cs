// <autogenerated/>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

using Kistl.API;

// <autogenerated/>

namespace Kistl.App.GUI
{
    using Kistl.API;
    using Kistl.App.GUI;
    using Kistl.App.Base;

    /// <summary>
    /// ObjectCollectionEntry for Describes the supported interfaces of a specific class of ControlKinds
    /// </summary>
    public interface ControlKindClass_supports_TypeRef_RelationEntry : IRelationCollectionEntry<ControlKindClass, TypeRef> 
    {

    }
}// <autogenerated/>

namespace Kistl.App.Test
{
    using Kistl.API;
    using Kistl.App.Test;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    public interface Muhblah_has_TestCustomObject_RelationEntry : IRelationCollectionEntry<Muhblah, TestCustomObject> 
    {

    }
}// <autogenerated/>

namespace Kistl.App.Base
{
    using Kistl.API;
    using Kistl.App.Base;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    public interface ObjectClass_implements_Interface_RelationEntry : IRelationCollectionEntry<ObjectClass, Interface> 
    {

    }
}// <autogenerated/>

namespace Kistl.App.GUI
{
    using Kistl.API;
    using Kistl.App.GUI;

    /// <summary>
    /// ObjectCollectionEntry for a list of additional control kinds for displaying this model
    /// </summary>
    public interface PresentableModelDescriptor_displayedBy_ControlKind_RelationEntry : IRelationCollectionEntry<PresentableModelDescriptor, ControlKind> 
    {

    }
}// <autogenerated/>

namespace Kistl.App.Projekte
{
    using Kistl.API;
    using Kistl.App.Projekte;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    public interface Projekt_haben_Mitarbeiter_RelationEntry : IRelationListEntry<Projekt, Mitarbeiter> 
    {

    }
}// <autogenerated/>

namespace Kistl.App.GUI
{
    using Kistl.API;
    using Kistl.App.GUI;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    public interface Template_hasMenu_Visual_RelationEntry : IRelationCollectionEntry<Template, Visual> 
    {

    }
}// <autogenerated/>

namespace Kistl.App.Base
{
    using Kistl.API;
    using Kistl.App.Base;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    public interface TypeRef_hasGenericArguments_TypeRef_RelationEntry : IRelationListEntry<TypeRef, TypeRef> 
    {

    }
}// <autogenerated/>

namespace Kistl.App.GUI
{
    using Kistl.API;
    using Kistl.App.GUI;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    public interface Visual_contains_Visual_RelationEntry : IRelationCollectionEntry<Visual, Visual> 
    {

    }
}// <autogenerated/>

namespace Kistl.App.GUI
{
    using Kistl.API;
    using Kistl.App.GUI;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    public interface Visual_hasContextMenu_Visual_RelationEntry : IRelationCollectionEntry<Visual, Visual> 
    {

    }
}// <autogenerated/>

namespace Kistl.App.TimeRecords
{
    using Kistl.API;
    using Kistl.App.TimeRecords;
    using Kistl.App.Projekte;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    public interface WorkEffortAccount_has_Mitarbeiter_RelationEntry : IRelationCollectionEntry<WorkEffortAccount, Mitarbeiter> 
    {

    }
}// <autogenerated/>

namespace Kistl.App.Projekte
{
    using Kistl.API;

    /// <summary>
    /// ValueCollectionEntry for EMails des Kunden - können mehrere sein
    /// </summary>
    public interface Kunde_EMails_CollectionEntry : IValueCollectionEntry<Kunde, System.String> 
    {

    }
}