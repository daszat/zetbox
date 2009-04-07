using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.DALProvider.EF
{
    public class EFValueCollectionWrapper<TParent, TValue, TEntry, TEntryCollection> : BaseValueCollectionWrapper<TParent, TValue, TEntry, TEntryCollection>
        where TParent : class, IDataObject
        where TEntry : class, IValueCollectionEntry<TParent, TValue>
        where TEntryCollection : ICollection<TEntry>
    {
        public EFValueCollectionWrapper(IKistlContext ctx, TParent parent, TEntryCollection collection)
            : base(ctx, parent, collection)
        {
        }

        protected override TEntry CreateEntry()
        {
            return (TEntry)ctx.CreateValueCollectionEntry<TEntry>();
        }
    }

    public class EFValueListWrapper<TParent, TValue, TEntry, TEntryCollection> : BaseValueListWrapper<TParent, TValue, TEntry, TEntryCollection>
        where TParent : class, IDataObject
        where TEntry : class, IValueListEntry<TParent, TValue>
        where TEntryCollection : IList<TEntry>
    {
        public EFValueListWrapper(IKistlContext ctx, TParent parent, TEntryCollection collection)
            : base(ctx, parent, collection)
        {
        }

        protected override TEntry CreateEntry()
        {
            return (TEntry)ctx.CreateValueCollectionEntry<TEntry>();
        }
    }
}
