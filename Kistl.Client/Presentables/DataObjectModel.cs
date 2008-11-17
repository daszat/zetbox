using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.GUI;

namespace Kistl.Client.Presentables
{
    /// <summary>
    /// Proxies a whole IDataObject
    /// </summary>
    public class DataObjectModel : PresentableModel
    {
        public DataObjectModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory,
            IDataObject obj)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory)
        {
            _object = obj;
            _object.PropertyChanged += AsyncObjectPropertyChanged;
            Async.Queue(DataContext, () =>
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
                    Async.Queue(DataContext, () =>
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

            // load properties
            ObjectClass cls = _object.GetObjectClass(GuiContext);
            List<BaseProperty> props = new List<BaseProperty>();
            List<Method> methods = new List<Method>();
            while (cls != null)
            {
                foreach (BaseProperty p in cls.Properties)
                {
                    props.Add(p);
                }
                foreach (Method m
                    in cls.Methods.Where(m => m.Parameter.Count == 1
                        && m.Parameter.Single().IsReturnParameter
                        && m.IsDisplayable))
                {
                    methods.Add(m);
                }
                cls = cls.BaseObjectClass;
            }
            UI.Queue(UI, () =>
            {
                SetClassPropertyModels(cls, props);
                SetClassMethodModels(cls, methods);
            });
        }

        // TODO: should go to renderer and use database backed decision tables
        private void SetClassPropertyModels(ObjectClass cls, IEnumerable<BaseProperty> props)
        {
            UI.Verify();
            foreach (var pm in props)
            {
                var prop = pm as Property;

                if (pm is BoolProperty && !prop.IsList)
                {
                    if (prop.IsNullable)
                        PropertyModels.Add(Factory.CreateModel<NullableBoolModel>(_object, (BoolProperty)pm));
                    else
                        PropertyModels.Add(Factory.CreateModel<BoolModel>(_object, (BoolProperty)pm));
                }
                else if (pm is DateTimeProperty && !prop.IsList)
                {
                    if (prop.IsNullable)
                        PropertyModels.Add(Factory.CreateModel<NullableDateTimeModel>(_object, (DateTimeProperty)pm));
                    else
                        PropertyModels.Add(Factory.CreateModel<DateTimeModel>(_object, (DateTimeProperty)pm));
                }
                else if (pm is DoubleProperty && !prop.IsList)
                {
                    if (prop.IsNullable)
                        PropertyModels.Add(Factory.CreateModel<NullableDoubleModel>(_object, (DoubleProperty)pm));
                    else
                        PropertyModels.Add(Factory.CreateModel<DoubleModel>(_object, (DoubleProperty)pm));
                }
                else if (pm is IntProperty && !prop.IsList)
                {
                    if (prop.IsNullable)
                        PropertyModels.Add(Factory.CreateModel<NullableIntModel>(_object, (IntProperty)pm));
                    else
                        PropertyModels.Add(Factory.CreateModel<IntModel>(_object, (IntProperty)pm));
                }
                else if (pm is StringProperty && !prop.IsList)
                {
                    PropertyModels.Add(Factory.CreateModel<StringModel>(_object, (StringProperty)pm));
                }
                else if (pm is ObjectReferenceProperty)
                {
                    var orp = (ObjectReferenceProperty)pm;
                    if (orp.IsList)
                        PropertyModels.Add(Factory.CreateModel<ObjectListModel>(_object, orp));
                    else
                        PropertyModels.Add(Factory.CreateModel<ObjectReferenceModel>(_object, orp));
                }
                else if (pm is BackReferenceProperty)
                {
                    var brp = (BackReferenceProperty)pm;
                    PropertyModels.Add(Factory.CreateModel<ObjectBackListModel>(_object, brp));
                }
                else
                {
                    Console.Error.WriteLine("==>> No model for property: '{0}' of Type '{1}'", pm, pm.GetType());
                }
            }
        }

        // TODO: should go to renderer and use database backed decision tables
        private void SetClassMethodModels(ObjectClass cls, IEnumerable<Method> methods)
        {
            UI.Verify();
            foreach (var pm in methods)
            {
                Debug.Assert(pm.Parameter.Single().IsReturnParameter);
                var retParam = pm.GetReturnParameter();

                if (retParam is BoolParameter && !retParam.IsList)
                {
                    PropertyModels.Add(Factory.CreateModel<BoolResultModel>(_object, pm));
                }
                else if (pm is DateTimeParameter && !retParam.IsList)
                {
                    PropertyModels.Add(Factory.CreateModel<DateTimeResultModel>(_object, pm));
                }
                else if (pm is DoubleParameter && !retParam.IsList)
                {
                    PropertyModels.Add(Factory.CreateModel<DoubleResultModel>(_object, pm));
                }
                else if (pm is IntParameter && !retParam.IsList)
                {
                    PropertyModels.Add(Factory.CreateModel<IntResultModel>(_object, pm));
                }
                else if (pm is StringParameter && !retParam.IsList)
                {
                    PropertyModels.Add(Factory.CreateModel<StringResultModel>(_object, pm));
                }
                else if (pm is ObjectParameter && !retParam.IsList)
                {
                    PropertyModels.Add(Factory.CreateModel<DataObjectResultModel>(_object, pm));
                }
                else
                {
                    Trace.TraceWarning("No model for property: '{0}' of Type '{1}'", pm, pm.GetType());
                }
            }
        }

        private static string GetIconPath(string name)
        {
            string result = GuiApplicationContext.Current.Configuration.Client.DocumentStore
                + @"\GUI.Icons\"
                + name;
            result = System.IO.Path.IsPathRooted(result) ? result : Environment.CurrentDirectory + "\\" + result;
            return result;
        }

        protected void AsyncUpdateViewCache()
        {
            Async.Verify();

            // update Name
            _toStringCache = _object.ToString();
            AsyncOnPropertyChanged("Name");

            // update IconPath
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
                icon = _object.GetObjectClass(GuiContext).DefaultIcon;
            }
            return icon;
        }


        #endregion

        #region PropertyChanged event handlers

        private void AsyncObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Async.Verify();

            // defer updating the cache into another work item
            Async.Queue(DataContext, () =>
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
