
namespace Kistl.DalProvider.EF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    public class EFValueCollectionWrapper<TParent, TValue, TEntry, TEntryCollection>
        : ValueCollectionWrapper<TParent, TValue, TEntry, TEntryCollection>
        where TParent : IDataObject
        where TEntry : class, IValueCollectionEntry<TParent, TValue>
        where TEntryCollection : ICollection<TEntry>
    {
        public EFValueCollectionWrapper(IKistlContext ctx, TParent parent, Action parentNotifier, TEntryCollection collection)
            : base(ctx, parent, parentNotifier, collection)
        {
        }
        public EFValueCollectionWrapper(IKistlContext ctx, TParent parent, TEntryCollection collection)
            : base(ctx, parent, null, collection)
        {
        }
    }

    public class EFValueListWrapper<TParent, TValue, TEntry, TEntryCollection>
        : ValueListWrapper<TParent, TValue, TEntry, TEntryCollection>
        where TParent : IDataObject
        where TEntry : class, IValueListEntry<TParent, TValue>
        where TEntryCollection : IList<TEntry>
    {
        public EFValueListWrapper(IKistlContext ctx, TParent parent, Action parentNotifier, TEntryCollection collection)
            : base(ctx, parent, parentNotifier, collection)
        {
        }
        public EFValueListWrapper(IKistlContext ctx, TParent parent, TEntryCollection collection)
            : base(ctx, parent, null, collection)
        {
        }
    }
}
