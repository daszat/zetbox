
namespace Zetbox.DalProvider.Ef
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.API.Common;
    using Zetbox.DalProvider.Base.RelationWrappers;

    public class EfValueCollectionWrapper<TParent, TValue, TEntry, TEntryCollection>
        : ValueCollectionWrapper<TParent, TValue, TEntry, TEntryCollection>
        where TParent : IDataObject
        where TEntry : class, IValueCollectionEntry<TParent, TValue>
        where TEntryCollection : ICollection<TEntry>
    {
        public EfValueCollectionWrapper(IZetboxContext ctx, TParent parent, Action parentNotifier, TEntryCollection collection)
            : base(ctx, parent, parentNotifier, collection)
        {
        }
        public EfValueCollectionWrapper(IZetboxContext ctx, TParent parent, TEntryCollection collection)
            : base(ctx, parent, null, collection)
        {
        }
    }

    public class EfValueListWrapper<TParent, TValue, TEntry, TEntryCollection>
        : ValueListWrapper<TParent, TValue, TEntry, TEntryCollection>
        where TParent : IDataObject
        where TEntry : class, IValueListEntry<TParent, TValue>
        where TEntryCollection : ICollection<TEntry>
    {
        public EfValueListWrapper(IZetboxContext ctx, TParent parent, Action parentNotifier, TEntryCollection collection)
            : base(ctx, parent, parentNotifier, collection)
        {
        }
        public EfValueListWrapper(IZetboxContext ctx, TParent parent, TEntryCollection collection)
            : base(ctx, parent, null, collection)
        {
        }
    }
}
