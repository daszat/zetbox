
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    public abstract class CompoundCollectionEntryNHibernateImpl<TA, TAImpl, TB, TBImpl>
        : BaseNhPersistenceObject
        where TA : class, IDataObject
        where TAImpl : class, IDataObject, TA
        where TB : class, ICompoundObject
        where TBImpl : class, ICompoundObject, TB
    {
        protected CompoundCollectionEntryNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        //public abstract TAImpl Parent { get; set; }
        //public abstract TBImpl Value { get; set; }

        //IDataObject IValueCollectionEntry.ParentObject
        //{
        //    get
        //    {
        //        return Parent;
        //    }
        //    set
        //    {
        //        if (this.IsReadonly)
        //            throw new ReadOnlyObjectException();
        //        Parent = (TAImpl)value;
        //    }
        //}

        //object IValueCollectionEntry.ValueObject
        //{
        //    get
        //    {
        //        return Value;
        //    }
        //    set
        //    {
        //        if (this.IsReadonly)
        //            throw new ReadOnlyObjectException();
        //        Value = (TBImpl)value;
        //    }
        //}
    }
}
