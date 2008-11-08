using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Client.PresenterModel
{
    /// <summary>
    /// Proxies a whole IDataObject
    /// </summary>
    public class DataObjectModel : PresentableModel
    {
        public DataObjectModel(IThreadManager uiManager, IThreadManager asyncManager, IDataObject obj)
            : base(uiManager, asyncManager)
        {
            _propertyModels = new ObservableCollection<PresentableModel>();
            _object = obj;
            Async.Queue(() =>
            {
                _toStringCache = _object.ToString();
                InvokePropertyChanged("Name");

                FetchProperties();

                UI.Queue(() => this.State = ModelState.Active);
            });
        }

        #region Public Interface

        private ObservableCollection<PresentableModel> _propertyModels;
        public ObservableCollection<PresentableModel> PropertyModels
        {
            get
            {
                UI.Verify();
                return _propertyModels;
            }
            private set
            {
                UI.Verify();
                if (value != _propertyModels)
                {
                    _propertyModels = value;
                    OnPropertyChanged("PropertyModels");
                }
            }
        }

        private string _toStringCache;
        public string Name
        {
            get
            {
                UI.Verify();
                return _toStringCache;
            }
            set
            {
                UI.Verify();
                if (value != _toStringCache)
                {
                    _toStringCache = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        #endregion

        #region Async handlers and UI callbacks

        private void FetchProperties()
        {
            Async.Verify();
            lock (_object.Context)
            {
                ObjectClass cls = _object.GetObjectClass(_object.Context);
                var props = cls.Properties;
                UI.Queue(() => SetClassPropertyModels(cls, props));
            }
        }

        private void SetClassPropertyModels(ObjectClass cls, IEnumerable<BaseProperty> props)
        {
            UI.Verify();
            foreach (var pm in props)
            {
                if (pm is BoolProperty)
                {
                    PropertyModels.Add(new BoolPropertyModel(UI, Async, _object, (BoolProperty)pm));
                }
                else if (pm is DateTimeProperty)
                {
                    PropertyModels.Add(new DateTimePropertyModel(UI, Async, _object, (DateTimeProperty)pm));
                }
                else if (pm is DoubleProperty)
                {
                    PropertyModels.Add(new DoublePropertyModel(UI, Async, _object, (DoubleProperty)pm));
                }
                else if (pm is IntProperty)
                {
                    PropertyModels.Add(new IntPropertyModel(UI, Async, _object, (IntProperty)pm));
                }
                else if (pm is StringProperty)
                {
                    PropertyModels.Add(new StringPropertyModel(UI, Async, _object, (StringProperty)pm));
                }
                else if (pm is ObjectReferenceProperty)
                {
                    var orp = (ObjectReferenceProperty)pm;
                    if (!orp.IsList)
                        PropertyModels.Add(new ReferencePropertyModel(UI, Async, _object, orp));
                }
            }
        }

        #endregion

        private IDataObject _object;
    }
}
