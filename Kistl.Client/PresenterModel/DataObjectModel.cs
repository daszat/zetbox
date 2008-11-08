using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.GUI;

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
            _object.PropertyChanged += ObjectPropertyChanged;
            Async.Queue(() =>
            {
                UpdateViewCache();

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

        private string _iconPathCache;
        public string IconPath
        {
            get
            {
                UI.Verify();
                return _iconPathCache;
            }
            set
            {
                UI.Verify();
                if (value != _iconPathCache)
                {
                    _iconPathCache = value;
                    OnPropertyChanged("IconPath");
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

        private static string GetIconPath(string name)
        {
            string result = ApplicationContext.Current.Configuration.Client.DocumentStore
                + @"\GUI.Icons\"
                + name;
            result = System.IO.Path.IsPathRooted(result) ? result : Environment.CurrentDirectory + "\\" + result;
            return result;
        }

        private void UpdateViewCache()
        {
            Async.Verify();
            lock (_object.Context)
            {
                _toStringCache = _object.ToString();
                InvokePropertyChanged("Name");

                Icon icon = null;
                if (_object is Icon)
                {
                    icon = (Icon)_object;
                }
                else if (_object is ObjectClass)
                {
                    icon = ((ObjectClass)_object).DefaultIcon;
                }
                else
                {
                    icon = _object.GetObjectClass(_object.Context.GetReadonlyContext()).DefaultIcon;
                }
                if (icon != null)
                {
                    string newIconPath = GetIconPath(icon.IconFile);
                    UI.Queue(() => IconPath = newIconPath);
                }
                else
                {
                    UI.Queue(() => IconPath = "");
                }
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

        #region PropertyChanged event handlers

        private void ObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Async.Verify();
            lock (_object.Context)
            {
                // flag to the user that something's happening
                UI.Queue(() => this.State = ModelState.Loading);
                UpdateViewCache();
            }
            // all updates done
            UI.Queue(() => this.State = ModelState.Active);
        }

        #endregion

        private IDataObject _object;
    }
}
