using System.Diagnostics;

namespace Kistl.API
{

    /// <summary>
    /// Collection Entry Interface. A Collection Entry is a "connection" Object between other Data Objects 
    /// (ObjectReferenceProperty, IsList=true) or just a simple Collection (eg. StringProperty, IsList=true).
    /// </summary>
    public interface ICollectionEntry : IPersistenceObject
    {
    }

    // TODO: Remove "New" when new Generator works
    /// <summary>
    /// Typed Collection Entry Interface. A Collection Entry is a "connection" Object between other Data Objects 
    /// (ObjectReferenceProperty, IsList=true) or just a simple Collection (eg. StringProperty, IsList=true).
    /// </summary>
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

    public interface INewCollectionEntrySorted : ICollectionEntry
    {
        int? AIndex { get; set; }
        int? BIndex { get; set; }
    }

    public interface INewListEntry<AType, BType> : INewCollectionEntry<AType, BType>, INewCollectionEntrySorted
    {
    }



    // TODO: rename INewCollectionEntry to this
    /// <summary> legacy interface </summary>
    public interface ICollectionEntry<LEFT, RIGHT> : ICollectionEntry
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        LEFT Value { get; set; }
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        RIGHT Parent { get; set; }
        int fk_Parent { get; set; }
    }

    /// <summary> legacy interface </summary>
    public interface ICollectionEntrySorted : ICollectionEntry
    {
        int? ValueIndex { get; set; }
        int? ParentIndex { get; set; }
    }

    /// <summary> legacy interface </summary>
    public interface ICollectionEntrySorted<VALUE, PARENT> : ICollectionEntry<VALUE, PARENT>, ICollectionEntrySorted
    {
    }
}