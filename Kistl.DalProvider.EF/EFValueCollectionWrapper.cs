
namespace Kistl.DalProvider.Ef
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    public class EfValueCollectionWrapper<TParent, TValue, TEntry, TEntryCollection>
        : ValueCollectionWrapper<TParent, TValue, TEntry, TEntryCollection>
        where TParent : IDataObject
        where TEntry : class, IValueCollectionEntry<TParent, TValue>
        where TEntryCollection : ICollection<TEntry>
    {
        public EfValueCollectionWrapper(IKistlContext ctx, TParent parent, Action parentNotifier, TEntryCollection collection)
            : base(ctx, parent, parentNotifier, collection)
        {
        }
        public EfValueCollectionWrapper(IKistlContext ctx, TParent parent, TEntryCollection collection)
            : base(ctx, parent, null, collection)
        {
        }
    }

    public class EfValueListWrapper<TParent, TValue, TEntry, TEntryCollection>
        : ValueListWrapper<TParent, TValue, TEntry, TEntryCollection>
        where TParent : IDataObject
        where TEntry : class, IValueListEntry<TParent, TValue>
        where TEntryCollection : IList<TEntry>
    {
        public EfValueListWrapper(IKistlContext ctx, TParent parent, Action parentNotifier, TEntryCollection collection)
            : base(ctx, parent, parentNotifier, collection)
        {
        }
        public EfValueListWrapper(IKistlContext ctx, TParent parent, TEntryCollection collection)
            : base(ctx, parent, null, collection)
        {
        }
    }
}
