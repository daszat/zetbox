
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    public abstract class CompoundObjectNHibernateImpl
        : BaseCompoundObject
    {
        protected CompoundObjectNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        protected override void OnPropertyChanging(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanging(property, oldValue, newValue);
            if (ParentObject != null)
            {
                ((IDataObject)ParentObject).NotifyPropertyChanging(ParentProperty + "." + property, oldValue, newValue);
            }
        }

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);
            if (ParentObject != null)
            {
                ((IDataObject)ParentObject).NotifyPropertyChanged(ParentProperty + "." + property, oldValue, newValue);
            }
        }
    }
}
