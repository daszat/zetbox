
namespace Kistl.DalProvider.Base.RelationWrappers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    public class ASideCollectionWrapper<TA, TB, TEntry, TBaseCollection>
        : BaseCollectionWrapper<TA, TB, TB, TA, TEntry, TBaseCollection>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationEntry<TA, TB>
        where TBaseCollection : class, ICollection<TEntry>
    {
        public ASideCollectionWrapper(TB parentObject, TBaseCollection baseCollection)
            : base(parentObject, baseCollection)
        {
        }

        protected override IEnumerable<TA> GetItems()
        {
            Collection.ForEach(e => e.AttachToContext(ParentObject.Context));
            return Collection.Select(e => e.A);
        }

        protected override TA ItemFromEntry(TEntry entry)
        {
            entry.AttachToContext(ParentObject.Context);
            return entry.A;
        }

        protected override TEntry InitialiseEntry(TEntry entry, TA item)
        {
            entry.B = ParentObject;
            entry.A = item;
            return entry;
        }
    }

    public class BSideCollectionWrapper<TA, TB, TEntry, TBaseCollection>
        : BaseCollectionWrapper<TA, TB, TA, TB, TEntry, TBaseCollection>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationEntry<TA, TB>
        where TBaseCollection : class, ICollection<TEntry>
    {
        public BSideCollectionWrapper(TA parentObject, TBaseCollection baseCollection)
            : base(parentObject, baseCollection)
        {
        }

        protected override IEnumerable<TB> GetItems()
        {
            Collection.ForEach(e => e.AttachToContext(ParentObject.Context));
            return Collection.Select(e => e.B);
        }

        protected override TB ItemFromEntry(TEntry entry)
        {
            entry.AttachToContext(ParentObject.Context);
            return entry.B;
        }

        protected override TEntry InitialiseEntry(TEntry entry, TB item)
        {
            entry.A = ParentObject;
            entry.B = item;
            return entry;
        }
    }
}