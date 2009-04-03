using System.Diagnostics;

namespace Kistl.API
{

    /// <summary>
    /// Collection Entry Interface. A Collection Entry is a "connection" Object between other Data Objects 
    /// (ObjectReferenceProperty, IsList=true) or just a simple Collection (eg. StringProperty, IsList=true).
    /// TODO: Seperate value collection from n:m collection and implement AObject & BObject as IDataObjects. 
    /// Also add fk_A & fk_B to the Interface (e.g. IRelationEntry)
    /// </summary>
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
}