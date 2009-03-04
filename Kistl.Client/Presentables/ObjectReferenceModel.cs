using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Client.Presentables
{

    public class ObjectReferenceModel
        : PropertyModel<DataObjectModel>, IValueModel<DataObjectModel>
    {
        public ObjectReferenceModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject referenceHolder, ObjectReferenceProperty prop)
            : base(appCtx, dataCtx, referenceHolder, prop)
        {
            AllowNullInput = prop.IsNullable;
        }

        #region Public Interface

        public bool HasValue
        {
            get
            {
                UI.Verify();
                return _valueCache != null;
            }
            set
            {
                UI.Verify();
                if (!value)
                    Value = null;
            }
        }

        public bool IsNull
        {
            get
            {
                UI.Verify();
                return _valueCache == null;
            }
            set
            {
                UI.Verify();
                if (value)
                    Value = null;
            }
        }

        public bool AllowNullInput { get; private set; }

        public void ClearValue()
        {
            if (AllowNullInput) Value = null;
            else throw new InvalidOperationException();
        }

        private DataObjectModel _valueCache;
        /// <summary>
        /// The value of the property presented by this model
        /// </summary>
        public DataObjectModel Value
        {
            get { UI.Verify(); return _valueCache; }
            set
            {
                UI.Verify();

                _valueCache = value;
                State = ModelState.Loading;
                Async.Queue(DataContext, () =>
                {
                    Object.SetPropertyValue<IDataObject>(Property.PropertyName, _valueCache == null ? null : _valueCache.Object);
                    AsyncCheckConstraints();
                    UI.Queue(UI, () => this.State = ModelState.Active);
                });
                OnPropertyChanged("Value");
                OnPropertyChanged("HasValue");
                OnPropertyChanged("IsNull");
            }
        }

        private ObservableCollection<DataObjectModel> _domain;
        public ObservableCollection<DataObjectModel> Domain
        {
            get
            {
                UI.Verify();
                if (_domain == null)
                {
                    _domain = new ObservableCollection<DataObjectModel>();
                    Async.Queue(DataContext, AsyncFetchDomain);
                }
                return _domain;
            }
        }

        public void OpenReference()
        {
            UI.Verify();
            if (Value != null)
                Factory.ShowModel(Value, true);
        }

        /// <summary>
        /// creates a new target and references it
        /// </summary>
        public void CreateNew()
        {
            UI.Verify();
            State = ModelState.Loading;
            Async.Queue(DataContext, () =>
            {
                IDataObject newObj = (IDataObject)DataContext.Create(Property.GetPropertyType());
                Object.SetPropertyValue<IDataObject>(Property.PropertyName, newObj);
                // State will be reset by PropertyChanged event
            });
        }

        #endregion

        #region Async handlers and UI callbacks

        protected override void AsyncGetPropertyValue()
        {
            Async.Verify();
            IDataObject newValue = Object.GetPropertyValue<IDataObject>(Property.PropertyName);
            UI.Queue(UI, () => Value = newValue == null ? null : Factory.CreateSpecificModel<DataObjectModel>(DataContext, newValue));
        }

        private void AsyncFetchDomain()
        {
            Async.Verify();
            Debug.Assert(_domain != null);

            UI.Queue(UI, () => State = ModelState.Loading);

            var objs = DataContext.GetQuery(Property.GetPropertyType())
                .ToList().OrderBy(obj => obj.ToString()).ToList();

            UI.Queue(UI, () =>
            {
                foreach (var obj in objs)
                {
                    _domain.Add(Factory.CreateSpecificModel<DataObjectModel>(DataContext, obj));
                }
                State = ModelState.Active;
            });
        }

        #endregion

    }
}
