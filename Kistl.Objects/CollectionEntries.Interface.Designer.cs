using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

using Kistl.API;


namespace Kistl.App.Base
{
    using Kistl.API;
    using Kistl.App.Base;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    public interface ObjectClass_ImplementsInterfaces49CollectionEntry : IRelationCollectionEntry<ObjectClass, Interface> 
    {

    }
}
namespace Kistl.App.Projekte
{
    using Kistl.API;
    using Kistl.App.Projekte;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    public interface Projekt_Mitarbeiter23CollectionEntry : IRelationListEntry<Projekt, Mitarbeiter> 
    {

    }
}
namespace Kistl.App.GUI
{
    using Kistl.API;
    using Kistl.App.GUI;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    public interface Template_Menu61CollectionEntry : IRelationCollectionEntry<Template, Visual> 
    {

    }
}
namespace Kistl.App.Base
{
    using Kistl.API;
    using Kistl.App.Base;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    public interface TypeRef_GenericArguments66CollectionEntry : IRelationListEntry<TypeRef, TypeRef> 
    {

    }
}
namespace Kistl.App.GUI
{
    using Kistl.API;
    using Kistl.App.GUI;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    public interface Visual_Children55CollectionEntry : IRelationCollectionEntry<Visual, Visual> 
    {

    }
}
namespace Kistl.App.GUI
{
    using Kistl.API;
    using Kistl.App.GUI;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    public interface Visual_ContextMenu60CollectionEntry : IRelationCollectionEntry<Visual, Visual> 
    {

    }
}
namespace Kistl.App.TimeRecords
{
    using Kistl.API;
    using Kistl.App.TimeRecords;
    using Kistl.App.Projekte;

    /// <summary>
    /// ObjectCollectionEntry for 
    /// </summary>
    public interface WorkEffortAccount_Mitarbeiter42CollectionEntry : IRelationCollectionEntry<WorkEffortAccount, Mitarbeiter> 
    {

    }
}
namespace Kistl.App.Projekte
{
    using Kistl.API;

    /// <summary>
    /// ValueCollectionEntry for EMails des Kunden - können mehrere sein
    /// </summary>
    public interface Kunde_EMailsCollectionEntry : IValueCollectionEntry<Kunde, System.String> 
    {

    }
}