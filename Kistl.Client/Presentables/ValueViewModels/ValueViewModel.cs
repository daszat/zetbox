using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.Client.Models;

namespace Kistl.Client.Presentables.ValueViewModels
{
    public abstract class BaseValueViewModel : ViewModel, IValueViewModel
    {
        public new delegate BaseValueViewModel Factory(IKistlContext dataCtx, IValueModel mdl);

        public BaseValueViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, IValueModel mdl)
            : base(dependencies, dataCtx)
        {
            this.Model = mdl;
        }

        public IValueModel Model { get; private set; }

        public override string Name
        {
            get { throw new NotImplementedException(); }
        }

        #region IValueViewModel Members

        public bool HasValue
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsNull
        {
            get { throw new NotImplementedException(); }
        }

        public bool AllowNullInput
        {
            get { throw new NotImplementedException(); }
        }

        public string Label
        {
            get { throw new NotImplementedException(); }
        }

        public string ToolTip
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public void ClearValue()
        {
            throw new NotImplementedException();
        }

        public ICommand ClearValueCommand
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }

    public abstract class ValueViewModel<TValue> : BaseValueViewModel, IValueViewModel<TValue>
    {
        public new delegate ValueViewModel<TValue> Factory(IKistlContext dataCtx, IValueModel mdl);

        public ValueViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, IValueModel mdl)
            : base(dependencies, dataCtx, mdl)
        {
            this.Model = (IValueModel<TValue>)mdl;
        }

        public new IValueModel<TValue> Model { get; private set; }

        #region IValueViewModel<TValue> Members

        public TValue Value
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }

    public class NullableStructValueViewModel<TValue> : ValueViewModel<Nullable<TValue>>
        where TValue: struct
    {
        public new delegate NullableStructValueViewModel<TValue> Factory(IKistlContext dataCtx, IValueModel mdl);

        public NullableStructValueViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, IValueModel mdl)
            : base(dependencies, dataCtx, mdl)
        {
        }

    }

    public class ClassValueViewModel<TValue> : ValueViewModel<TValue>
        where TValue : class
    {
        public new delegate ClassValueViewModel<TValue> Factory(IKistlContext dataCtx, IValueModel mdl);

        public ClassValueViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, IValueModel mdl)
            : base(dependencies, dataCtx, mdl)
        {
        }
    }
}
