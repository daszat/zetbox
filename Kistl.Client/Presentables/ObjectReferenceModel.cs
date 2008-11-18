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
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory,
            IDataObject referenceHolder, ObjectReferenceProperty prop)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory, referenceHolder, prop)
        { }

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

        #endregion

        #region Async handlers and UI callbacks

        protected override void AsyncGetPropertyValue()
        {
            Async.Verify();
            IDataObject newValue = Object.GetPropertyValue<IDataObject>(Property.PropertyName);
            UI.Queue(UI, () => Value = newValue == null ? null : Factory.CreateModel<DataObjectModel>(newValue));
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
                    // TODO: search for existing DOModel
                    _domain.Add(Factory.CreateModel<DataObjectModel>(obj));
                }
                State = ModelState.Active;
            });
        }

        #endregion

    }
}
