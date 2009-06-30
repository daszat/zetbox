using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;
using System.ComponentModel;

namespace Kistl.Client.Presentables
{

    public abstract class MethodResultModel<TValue>
        : PresentableModel
    {
        protected MethodResultModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, Method m)
            : base(appCtx, dataCtx)
        {
            Object = obj;
            Method = m;

            Method.PropertyChanged += MethodPropertyChanged;
            Object.PropertyChanged += ObjectPropertyChanged;

            GetResult();
        }

        #region Public Interface

        // TODO: proxying implementations might block on that
        public string Label { get { return Method.MethodName; } }
        // TODO: proxying implementations might block on that
        public string ToolTip { get { return Method.Description; } }

        public abstract TValue Value { get; protected set; }

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
            Value = Object.CallMethod<TValue>(Method.MethodName);
        }

        /// <summary>
        /// Notifies listner that the object state has changed
        /// </summary>
        protected virtual void OnResultChanged()
        {
            OnPropertyChanged("Value");
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
                case "MethodName": OnPropertyChanged("Label"); break;
                case "Description": OnPropertyChanged("ToolTip"); break;
            }
        }

        #endregion

        protected IDataObject Object { get; private set; }
        protected Method Method { get; private set; }


    }

    public class NullableResultModel<TValue>
        : MethodResultModel<Nullable<TValue>>, IReadOnlyValueModel<Nullable<TValue>>
        where TValue : struct
    {
        public NullableResultModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, Method m)
            : base(appCtx, dataCtx, obj, m)
        {
        }

        /// <summary>
        /// Whether or not the method returned a value. <seealso cref="IsNull"/>
        /// </summary>
        public bool HasValue { get { return _valueCache.HasValue; } }

        /// <summary>
        /// Whether or not the method returned null. <seealso cref="HasValue"/>
        /// </summary>
        public bool IsNull { get { return !_valueCache.HasValue; } }

        private Nullable<TValue> _valueCache;

        /// <summary>
        /// The value of the property presented by this model
        /// </summary>
        public override Nullable<TValue> Value
        {
            get {return _valueCache; }
            protected set
            {
                if (!_valueCache.HasValue && !value.HasValue)
                    return;

                _valueCache = value;
                OnResultChanged();
            }
        }
    }

    public class ObjectResultModel<TValue>
        : MethodResultModel<TValue>, IReadOnlyValueModel<TValue>
        where TValue : class
    {
        public ObjectResultModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, Method m)
            : base(appCtx, dataCtx, obj, m)
        {
        }

        /// <summary>
        /// Whether or not the method returned a value. <seealso cref="IsNull"/>
        /// </summary>
        public bool HasValue { get { return Value != null; } }

        /// <summary>
        /// Whether or not the method returned null. <seealso cref="HasValue"/>
        /// </summary>
        public bool IsNull { get { return Value == null; } }

        private TValue _valueCache;

        /// <summary>
        /// The value of the property presented by this model
        /// </summary>
        public override TValue Value
        {
            get { return _valueCache; }
            protected set
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
