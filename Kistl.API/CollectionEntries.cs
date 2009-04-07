using System.Diagnostics;
using System;

namespace Kistl.API
{

    public interface IRelationCollectionEntry : IPersistenceObject
    {
        int RelationID { get; }

        IDataObject AObject { get; set; }
        IDataObject BObject { get; set; }
    }

    public interface IRelationListEntry : IRelationCollectionEntry
    {
        int? AIndex { get; set; }
        int? BIndex { get; set; }
    }

    public interface IRelationCollectionEntry<AType, BType> : IRelationCollectionEntry
        where AType : IDataObject
        where BType : IDataObject
    {
        AType A { get; set; }
        BType B { get; set; }
    }

    public interface IRelationListEntry<AType, BType> : IRelationCollectionEntry<AType, BType>, IRelationListEntry
        where AType : IDataObject
        where BType : IDataObject
    {
    }


    public interface IValueCollectionEntry : IPersistenceObject
    {
        IDataObject ParentObject { get; set; }
        object ValueObject { get; set; }
    }

    public interface IValueListEntry : IValueCollectionEntry
    {
        int? Index { get; set; }
    }

    public interface IValueCollectionEntry<TParent, TValue> : IValueCollectionEntry
        where TParent : IDataObject
    {
        TParent Parent { get; set; }
        TValue Value { get; set; }
    }

    public interface IValueListEntry<TParent, TValue> : IValueCollectionEntry<TParent, TValue>, IValueListEntry
        where TParent : IDataObject
    {
    }
}