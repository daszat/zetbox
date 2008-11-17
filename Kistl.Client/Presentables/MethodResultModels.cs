using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;
using System.ComponentModel;

namespace Kistl.Client.Presentables
{

    public abstract class MethodResultModel<TValue> : PresentableModel
    {
        public MethodResultModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory,
            IDataObject obj, Method m)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory)
        {
            Object = obj;
            Method = m;

            Method.PropertyChanged += AsyncMethodPropertyChanged;
            Object.PropertyChanged += AsyncObjectPropertyChanged;
            Async.Queue(Object.Context, () =>
            {
                this.AsyncGetResult();
                UI.Queue(UI, () => { this.State = ModelState.Active; });
            });
        }

        #region Public Interface

        // TODO: proxying implementations might block on that
        public string Label { get { return Method.MethodName; } }
        // TODO: proxying implementations might block on that
        public string ToolTip { get { return Method.Description; } }

        public abstract TValue Value { get; protected set; }

        #endregion

        #region Async handlers and UI callbacks

        /// <summary>
        /// Loads the method's result from the object into the cache.
        /// Called on the Async Thread.
        /// </summary>
        protected void AsyncGetResult()
        {
            Async.Verify();
            TValue newValue = Object.CallMethod<TValue>(Method.MethodName);
            UI.Queue(UI, () => Value = newValue);
        }

        /// <summary>
        /// Notifies listner that the object state has changed
        /// </summary>
        protected virtual void AsyncOnResultChanged()
        {
            Async.Verify();
            AsyncOnPropertyChanged("Value");
            AsyncOnPropertyChanged("IsNull");
            AsyncOnPropertyChanged("HasValue");
        }
        #endregion

        #region PropertyChanged event handlers

        private void AsyncObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Async.Verify();

            // although we're already Async here, defer actually checking 
            // the object onto another thread
            Async.Queue(DataContext, () =>
            {
                // flag to the user that something's happening
                UI.Queue(UI, () => this.State = ModelState.Loading);

                // always have to recalculate
                AsyncGetResult();

                // all updates done
                UI.Queue(UI, () => this.State = ModelState.Active);
            });
        }

        private void AsyncMethodPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Async.Verify();
            switch (e.PropertyName)
            {
                case "MethodName": AsyncOnPropertyChanged("Label"); break;
                case "Description": AsyncOnPropertyChanged("ToolTip"); break;
            }
        }

        #endregion

        protected IDataObject Object { get; private set; }
        protected Method Method { get; private set; }

    }

    public abstract class StructResultModel<TValue>
        : MethodResultModel<TValue>, IValueModel<TValue>
        where TValue : struct
    {
        public StructResultModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory,
            IDataObject obj, Method m)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory, obj, m)
        {
        }


        /// <summary>
        /// Whether or not the method returned a value. <seealso cref="IsNull"/>
        /// </summary>
        public bool HasValue { get { UI.Verify(); return true; } }

        /// <summary>
        /// Whether or not the method returned null. <seealso cref="HasValue"/>
        /// </summary>
        public bool IsNull { get { UI.Verify(); return false; } }


        private TValue _valueCache;

        /// <summary>
        /// The value of the property presented by this model
        /// </summary>
        public override TValue Value
        {
            get { UI.Verify(); return _valueCache; }
            protected set
            {
                UI.Verify();
                if (!_valueCache.Equals(value))
                {
                    _valueCache = value;
                    Async.Queue(DataContext, AsyncOnResultChanged);
                }
            }
        }
    }

    public abstract class NullableResultModel<TValue>
        : MethodResultModel<Nullable<TValue>>, IValueModel<Nullable<TValue>>
        where TValue : struct
    {
        public NullableResultModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory,
            IDataObject obj, Method m)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory, obj, m)
        {
        }

        /// <summary>
        /// Whether or not the method returned a value. <seealso cref="IsNull"/>
        /// </summary>
        public bool HasValue { get { UI.Verify(); return _valueCache.HasValue; } }

        /// <summary>
        /// Whether or not the method returned null. <seealso cref="HasValue"/>
        /// </summary>
        public bool IsNull { get { UI.Verify(); return !_valueCache.HasValue; } }

        private Nullable<TValue> _valueCache;

        /// <summary>
        /// The value of the property presented by this model
        /// </summary>
        public override Nullable<TValue> Value
        {
            get { UI.Verify(); return _valueCache; }
            protected set
            {
                UI.Verify();
                if (!_valueCache.HasValue && !value.HasValue)
                    return;

                _valueCache = value;
                Async.Queue(DataContext, AsyncOnResultChanged);
            }
        }
    }

    public class ObjectResultModel<TValue>
        : MethodResultModel<TValue>, IValueModel<TValue>
        where TValue : class
    {
        public ObjectResultModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory,
            IDataObject obj, Method m)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory, obj, m)
        {
        }

        /// <summary>
        /// Whether or not the method returned a value. <seealso cref="IsNull"/>
        /// </summary>
        public bool HasValue { get { UI.Verify(); return Value != null; } }

        /// <summary>
        /// Whether or not the method returned null. <seealso cref="HasValue"/>
        /// </summary>
        public bool IsNull { get { UI.Verify(); return Value == null; } }

        private TValue _valueCache;

        /// <summary>
        /// The value of the property presented by this model
        /// </summary>
        public override TValue Value
        {
            get { UI.Verify(); return _valueCache; }
            protected set
            {
                UI.Verify();
                if (_valueCache != value)
                {
                    _valueCache = value;
                    Async.Queue(DataContext, AsyncOnResultChanged);
                }
            }
        }
    }

}
