
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.Client.Presentables.ValueViewModels;

    // TODO: (re)move to ValueViewModels\ and  ..\Models\
    public abstract class BaseMethodResultModel
        : ViewModel
    {
        public new delegate BaseMethodResultModel Factory(IKistlContext dataCtx, IDataObject obj, Method m);

        protected BaseMethodResultModel(
               IViewModelDependencies appCtx, IKistlContext dataCtx,
               IDataObject obj, Method m)
            : base(appCtx, dataCtx)
        {
            Object = obj;
            Method = m;
        }

        public IDataObject Object { get; private set; }
        public Method Method { get; private set; }
    }

    public abstract class MethodResultModel<TValue>
        : BaseMethodResultModel, IValueViewModel<string>, IFormattedValueViewModel
    {
        public new delegate MethodResultModel<TValue> Factory(IKistlContext dataCtx, IDataObject obj, Method m);

        protected MethodResultModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            IDataObject obj, Method m)
            : base(appCtx, dataCtx, obj, m)
        {
            Method.PropertyChanged += MethodPropertyChanged;
            Object.PropertyChanged += ObjectPropertyChanged;

            GetResult();
        }

        #region Public Interface

        // TODO: proxying implementations might block on that
        public string Label { get { return Method.Name; } }
        // TODO: proxying implementations might block on that
        public string ToolTip { get { return Method.Description; } }

        public abstract TValue Value { get; set; }

        public bool IsReadOnly { get { return true; } set { throw new NotSupportedException(); } }

        public override string Name
        {
            get { return Label; }
        }

        #endregion

        #region Utilities and UI callbacks

        /// <summary>
        /// Loads the method's result from the object into the cache.
        /// </summary>
        protected void GetResult()
        {
            Value = Object.CallMethod<TValue>(Method.Name);
        }

        /// <summary>
        /// Notifies listner that the object state has changed
        /// </summary>
        protected virtual void OnResultChanged()
        {
            OnPropertyChanged("Value");
            OnPropertyChanged("FormattedValue");
            OnPropertyChanged("IsNull");
            OnPropertyChanged("HasValue");
        }
        #endregion

        #region PropertyChanged event handlers

        private void ObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // always have to recalculate
            GetResult();
        }

        private void MethodPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Name": OnPropertyChanged("Label"); break;
                case "Description": OnPropertyChanged("ToolTip"); break;
            }
        }

        #endregion

        #region IReadOnlyValueModel<string> Members

        public abstract bool HasValue { get; }
        public abstract bool IsNull { get; }

        string IValueViewModel<string>.Value
        {
            get
            {
                return HasValue ? Value.ToString() : "(null)";
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        #endregion

        #region IValueModelAsString Members

        public string FormattedValue
        {
            get
            {
                return ((IValueViewModel<string>)this).Value;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        #endregion

        #region IValueViewModel Members


        public bool AllowNullInput
        {
            get { throw new NotImplementedException(); }
        }

        public void ClearValue()
        {
            throw new NotImplementedException();
        }

        public ICommandViewModel ClearValueCommand
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }

    public class NullableResultModel<TValue>
        : MethodResultModel<Nullable<TValue>>, IValueViewModel<Nullable<TValue>>
        where TValue : struct
    {
        public new delegate NullableResultModel<TValue> Factory(IKistlContext dataCtx, IDataObject obj, Method m);

        public NullableResultModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            IDataObject obj, Method m)
            : base(appCtx, dataCtx, obj, m)
        {
        }

        /// <summary>
        /// Whether or not the method returned a value. <seealso cref="IsNull"/>
        /// </summary>
        public override bool HasValue { get { return _valueCache.HasValue; } }

        /// <summary>
        /// Whether or not the method returned null. <seealso cref="HasValue"/>
        /// </summary>
        public override bool IsNull { get { return !_valueCache.HasValue; } }

        private Nullable<TValue> _valueCache;

        /// <summary>
        /// The value of the property presented by this model
        /// </summary>
        public override Nullable<TValue> Value
        {
            get { return _valueCache; }
            set
            {
                if (!_valueCache.HasValue && !value.HasValue)
                    return;

                _valueCache = value;
                OnResultChanged();
            }
        }
    }

    public class ObjectResultModel<TValue>
        : MethodResultModel<TValue>, IValueViewModel<TValue>
        where TValue : class
    {
        public new delegate ObjectResultModel<TValue> Factory(IKistlContext dataCtx, IDataObject obj, Method m);

        public ObjectResultModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            IDataObject obj, Method m)
            : base(appCtx, dataCtx, obj, m)
        {
        }

        /// <summary>
        /// Whether or not the method returned a value. <seealso cref="IsNull"/>
        /// </summary>
        public override bool HasValue { get { return Value != null; } }

        /// <summary>
        /// Whether or not the method returned null. <seealso cref="HasValue"/>
        /// </summary>
        public override bool IsNull { get { return Value == null; } }

        private TValue _valueCache;

        /// <summary>
        /// The value of the property presented by this model
        /// </summary>
        public override TValue Value
        {
            get { return _valueCache; }
            set
            {
                if (_valueCache != value)
                {
                    _valueCache = value;
                    OnResultChanged();
                }
            }
        }
    }
}
