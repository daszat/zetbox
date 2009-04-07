using System.Diagnostics;
using System;

namespace Kistl.API
{

    public interface IRelationCollectionEntry : IPersistenceObject
    {
        int RelationID { get; }

        IDataObject AObject { get; }
        IDataObject BObject { get; }
    }

    public interface IRelationListEntry : IRelationCollectionEntry
    {
        int? AIndex { get; set; }
        int? BIndex { get; set; }
    }

    public interface IRelationCollectionEntry<AType, BType> : IRelationCollectionEntry
        where AType : class, IDataObject
        where BType : class, IDataObject
    {
        AType A { get; }
        BType B { get; }
    }

    public interface IRelationListEntry<AType, BType> : IRelationCollectionEntry<AType, BType>, IRelationListEntry
        where AType : class, IDataObject
        where BType : class, IDataObject
    {
    }


    public interface IValueCollectionEntry : IPersistenceObject
    {
        IDataObject ParentObject { get; }
        object ValueObject { get; }
    }

    public interface IValueListEntry : IValueCollectionEntry
    {
        int? Index { get; set; }
    }

    public interface IValueCollectionEntry<TParent, TValue> : IValueCollectionEntry
        where TParent : class, IDataObject
    {
        TParent Parent { get; }
        TValue Value { get; }
    }

    public interface IValueListEntry<TParent, TValue> : IValueCollectionEntry<TParent, TValue>, IValueListEntry
        where TParent : class, IDataObject
    {
    }

    /// <summary>
    /// Collection Entry Interface. A Collection Entry is a "connection" Object between other Data Objects 
    /// (ObjectReferenceProperty, IsList=true) or just a simple Collection (eg. StringProperty, IsList=true).
    /// TODO: Seperate value collection from n:m collection and implement AObject & BObject as IDataObjects. 
    /// Also add fk_A & fk_B to the Interface (e.g. IRelationEntry)
    /// </summary>
    [Obsolete("Will die")]
    public interface ICollectionEntry : IPersistenceObject
    {
        int RelationID { get; }

        object AObject { get; }
        object BObject { get; }
    }

    // TODO: Remove "New" when new Generator works
    /// <summary>
    /// Typed Collection Entry Interface. A Collection Entry is a "connection" Object between other Data Objects 
    /// (ObjectReferenceProperty, IsList=true) or just a simple Collection (eg. StringProperty, IsList=true).
    /// </summary>
    [Obsolete("Will die")]
    public interface INewCollectionEntry<AType, BType> : ICollectionEntry
    {

        /// <summary>
        /// A part of this collection entry
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        AType A { get; set; }

        /// <summary>
        /// B part of this collection entry
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        BType B { get; set; }


        ///// <summary>
        ///// foreign key to the parent
        ///// </summary>
        //int fk_A { get; set; }
    }

    [Obsolete("Will die")]
    public interface INewCollectionEntrySorted : ICollectionEntry
    {
        int? AIndex { get; set; }
        int? BIndex { get; set; }
    }

    [Obsolete("Will die")]
    public interface INewListEntry<AType, BType> : INewCollectionEntry<AType, BType>, INewCollectionEntrySorted
    {
    }
}