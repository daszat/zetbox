using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using System.Collections.ObjectModel;
using Kistl.App.Base;

namespace Kistl.Client.Presentables
{
    public class ObjectListModel
        : PropertyModel<ICollection<DataObjectModel>>, IValueModel<ObservableCollection<DataObjectModel>>
    {

        public ObjectListModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory,
            IDataObject referenceHolder, ObjectReferenceProperty prop)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory, referenceHolder, prop)
        {
            if (!prop.IsList)
                throw new ArgumentOutOfRangeException("prop", "ObjectReferenceProperty must be a list");

        }

        #region Public interface: IValueModel<ObservableCollection<DataObjectModel>> Members

        public bool HasValue { get { UI.Verify(); return true; } }
        public bool IsNull { get { UI.Verify(); return false; } }

        private ObservableCollection<DataObjectModel> _value;
        public ObservableCollection<DataObjectModel> Value
        {
            get
            {
                UI.Verify();
                if (_value == null)
                    _value = new ObservableCollection<DataObjectModel>();
                return _value;
            }
            private set
            {
                UI.Verify();
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged("Value");
                }
            }
        }

        #endregion

        #region Async handlers and UI callbacks

        protected override void AsyncGetPropertyValue()
        {
            Async.Verify();
            IEnumerable newValue = Object.GetPropertyValue<IEnumerable>(Property.PropertyName);
            UI.Queue(UI, () => SyncValues(newValue));
        }

        private void SyncValues(IEnumerable elements)
        {
            UI.Verify();
            ObservableCollection<DataObjectModel> newValue = new ObservableCollection<DataObjectModel>();
            foreach (IDataObject obj in elements.Cast<IDataObject>())
            {
                newValue.Add(Factory.CreateModel<DataObjectModel>(obj));
            }
            // almost optimal atomic update
            Value = newValue;
        }

        #endregion

    }

    public class ObjectBackListModel
        : PropertyModel<ICollection<DataObjectModel>>, IValueModel<ObservableCollection<DataObjectModel>>
    {

        public ObjectBackListModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory,
            IDataObject referenceHolder, BackReferenceProperty prop)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory, referenceHolder, prop)
        {
        }

        #region Public interface: IValueModel<ObservableCollection<DataObjectModel>> Members

        public bool HasValue { get { UI.Verify(); return true; } }
        public bool IsNull { get { UI.Verify(); return false; } }

        private ObservableCollection<DataObjectModel> _value;
        public ObservableCollection<DataObjectModel> Value
        {
            get
            {
                UI.Verify();
                if (_value == null)
                    _value = new ObservableCollection<DataObjectModel>();
                return _value;
            }
            private set
            {
                UI.Verify();
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged("Value");
                }
            }
        }

        #endregion

        #region Async handlers and UI callbacks

        protected override void AsyncGetPropertyValue()
        {
            Async.Verify();
            IEnumerable newValue = Object.GetPropertyValue<IEnumerable>(Property.PropertyName);
            UI.Queue(UI, () => SyncValues(newValue));
        }

        private void SyncValues(IEnumerable elements)
        {
            UI.Verify();
            ObservableCollection<DataObjectModel> newValue = new ObservableCollection<DataObjectModel>();
            foreach (IDataObject obj in elements.Cast<IDataObject>())
            {
                newValue.Add(Factory.CreateModel<DataObjectModel>(obj));
            }
            // almost optimal atomic update
            Value = newValue;
        }

        #endregion

    }
}
