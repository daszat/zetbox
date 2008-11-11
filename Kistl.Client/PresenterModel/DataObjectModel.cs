using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.GUI;
using System.Diagnostics;

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

            _object = obj;
            _object.PropertyChanged += AsyncObjectPropertyChanged;
            Async.Queue(_object.Context, () =>
            {
                AsyncUpdateViewCache();
                UI.Queue(UI, () => this.State = ModelState.Active);
            });
        }

        #region Public Interface

        public int ID { get { UI.Verify(); return _object.ID; } }

        private ObservableCollection<PresentableModel> _propertyModels;
        public ObservableCollection<PresentableModel> PropertyModels
        {
            get
            {
                UI.Verify();
                if (_propertyModels == null)
                {
                    _propertyModels = new ObservableCollection<PresentableModel>();
                    State = ModelState.Loading;
                    Async.Queue(_object.Context, () =>
                    {
                        AsyncFetchProperties();
                        UI.Queue(UI, () => this.State = ModelState.Active);
                    });
                }
                return _propertyModels;
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
            private set
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

        private void AsyncFetchProperties()
        {
            Async.Verify();
            ObjectClass cls = _object.GetObjectClass(_object.Context);
            List<BaseProperty> props = new List<BaseProperty>();
            while (cls != null)
            {
                foreach (BaseProperty p in cls.Properties)
                {
                    props.Add(p);
                }
                cls = cls.BaseObjectClass;
            }
            UI.Queue(UI, () => SetClassPropertyModels(cls, props));
        }

        private static string GetIconPath(string name)
        {
            string result = ApplicationContext.Current.Configuration.Client.DocumentStore
                + @"\GUI.Icons\"
                + name;
            result = System.IO.Path.IsPathRooted(result) ? result : Environment.CurrentDirectory + "\\" + result;
            return result;
        }

        private void AsyncUpdateViewCache()
        {
            Async.Verify();
            _toStringCache = _object.ToString();
            AsyncOnPropertyChanged("Name");

            Icon icon = AsyncGetIcon();
            if (icon != null)
            {
                string newIconPath = GetIconPath(icon.IconFile);
                UI.Queue(UI, () => IconPath = newIconPath);
            }
            else
            {
                UI.Queue(UI, () => IconPath = "");
            }
        }

        /// <summary>
        /// Override this to present a custom icon
        /// </summary>
        /// <returns>an <see cref="Icon"/> describing the desired icon</returns>
        protected virtual Icon AsyncGetIcon()
        {
            Async.Verify();
            Icon icon = null;
            if (_object is Icon)
            {
                icon = (Icon)_object;
            }
            else
            {
                icon = _object.GetObjectClass(_object.Context.GetReadonlyContext()).DefaultIcon;
            }
            return icon;
        }

        private void SetClassPropertyModels(ObjectClass cls, IEnumerable<BaseProperty> props)
        {
            UI.Verify();
            foreach (var pm in props)
            {
                var prop = pm as Property;

                if (pm is BoolProperty && !prop.IsList)
                {
                    if (prop.IsNullable)
                        PropertyModels.Add(new NullableBoolModel(UI, Async, _object, (BoolProperty)pm));
                    else
                        PropertyModels.Add(new BoolModel(UI, Async, _object, (BoolProperty)pm));
                }
                else if (pm is DateTimeProperty && !prop.IsList)
                {
                    if (prop.IsNullable)
                        PropertyModels.Add(new NullableDateTimeModel(UI, Async, _object, (DateTimeProperty)pm));
                    else
                        PropertyModels.Add(new DateTimeModel(UI, Async, _object, (DateTimeProperty)pm));
                }
                else if (pm is DoubleProperty && !prop.IsList)
                {
                    if (prop.IsNullable)
                        PropertyModels.Add(new NullableDoubleModel(UI, Async, _object, (DoubleProperty)pm));
                    else
                        PropertyModels.Add(new DoubleModel(UI, Async, _object, (DoubleProperty)pm));
                }
                else if (pm is IntProperty && !prop.IsList)
                {
                    if (prop.IsNullable)
                        PropertyModels.Add(new NullableIntModel(UI, Async, _object, (IntProperty)pm));
                    else
                        PropertyModels.Add(new IntModel(UI, Async, _object, (IntProperty)pm));
                }
                else if (pm is StringProperty && !prop.IsList)
                {
                    PropertyModels.Add(new StringModel(UI, Async, _object, (StringProperty)pm));
                }
                else if (pm is ObjectReferenceProperty && !prop.IsList)
                {
                    var orp = (ObjectReferenceProperty)pm;
                    if (!orp.IsList)
                        PropertyModels.Add(new ObjectReferenceModel(UI, Async, _object, orp));
                }
                else
                {
                    Trace.TraceWarning("No model for property: '{0}' of Type '{1}'", pm, pm.GetType());
                }
            }
        }

        #endregion

        #region PropertyChanged event handlers

        private void AsyncObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Async.Verify();

            // defer updating the cache into another work item
            Async.Queue(_object.Context, () =>
            {
                // flag to the user that something's happening
                UI.Queue(UI, () => this.State = ModelState.Loading);
                AsyncUpdateViewCache();
                // all updates done
                UI.Queue(UI, () => this.State = ModelState.Active);
            });

            // notify consumers if ID has changed
            if (e.PropertyName == "ID")
                OnPropertyChanged("ID");
        }

        #endregion

        private IDataObject _object;

        // other models need access here
        internal IDataObject Object { get { return _object; } }
    }
}
