using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.API.Client
{
    public class ClientValueCollectionWrapper<TParent, TValue, TEntry, TEntryCollection> : ValueCollectionWrapper<TParent, TValue, TEntry, TEntryCollection>
        where TParent : IDataObject
        where TEntry : class, IValueCollectionEntry<TParent, TValue>
        where TEntryCollection : ICollection<TEntry>
    {
        public ClientValueCollectionWrapper(IKistlContext ctx, TParent parent, TEntryCollection collection)
            : base(ctx, parent, collection)
        {
        }
    }

    public class ClientValueListWrapper<TParent, TValue, TEntry, TEntryCollection> : ValueListWrapper<TParent, TValue, TEntry, TEntryCollection>
        where TParent : IDataObject
        where TEntry : class, IValueListEntry<TParent, TValue>
        where TEntryCollection : IList<TEntry>
    {
        public ClientValueListWrapper(IKistlContext ctx, TParent parent, TEntryCollection collection)
            : base(ctx, parent, collection)
        {
        }
    }
}
