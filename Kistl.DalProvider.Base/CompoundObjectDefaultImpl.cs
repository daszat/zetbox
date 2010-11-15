
namespace Kistl.DalProvider.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    /// <summary>
    /// local proxy
    /// </summary>
    public abstract class CompoundObjectDefaultImpl
        : BaseCompoundObject
    {
        protected CompoundObjectDefaultImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        protected override void OnPropertyChanging(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanging(property, oldValue, newValue);
            if (ParentObject != null)
                ParentObject.NotifyPropertyChanging(ParentProperty, null, null);
        }

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);
            if (ParentObject != null)
                ParentObject.NotifyPropertyChanged(ParentProperty, null, null);
        }
    }
}
