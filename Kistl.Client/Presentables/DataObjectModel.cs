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
using Kistl.Client.GUI.DB;

namespace Kistl.Client.Presentables
{
    /// <summary>
    /// Proxies a whole IDataObject
    /// </summary>
    public class DataObjectModel : PresentableModel
    {

        public static DataObjectModel CreateDesignMock()
        {
            return new DataObjectModel(true);
        }

        public DataObjectModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj)
            : base(appCtx, dataCtx)
        {
            _object = obj;
            _object.PropertyChanged += AsyncObjectPropertyChanged;
            // TODO: Optional machen!
            Async.Queue(DataContext, () =>
            {
                AsyncUpdateViewCache();
                UI.Queue(UI, () => this.State = ModelState.Active);
            });
        }

        protected DataObjectModel(bool designMode)
            : base(designMode)
        {
            _propertyModelsCache = new ObservableCollection<PresentableModel>() {
                NullableValuePropertyModel<bool>.CreateDesignMock(true),
                NullableValuePropertyModel<int>.CreateDesignMock(42),
                NullableValuePropertyModel<double>.CreateDesignMock(Math.PI),
                NullableValuePropertyModel<DateTime>.CreateDesignMock(DateTime.Now),
                //NullableValuePropertyModel<string>.CreateDesignMock("short test string"),
                //NullableValuePropertyModel<string>.CreateDesignMock("Lore ipsum long test string. Lore ipsum long test string. Lore ipsum long test string. Lore ipsum long test string. Lore ipsum long test string. Lore ipsum long test string. Lore ipsum long test string."),
            };

            Name = "Some Name";
            // TODO: ship and link "real" icon here
            IconPath = "/illegal icon path";
        }

        #region Public Interface

        public int ID
        {
            get
            {
                UI.Verify();
                return IsInDesignMode 
                    ? 42
                    // this should always be instantaneous
                    : _object.ID;
            }
        }

        private ObservableCollection<PresentableModel> _propertyModelsCache;
        public ObservableCollection<PresentableModel> PropertyModels
        {
            get
            {
                UI.Verify();
                if (_propertyModelsCache == null)
                {
                    _propertyModelsCache = new ObservableCollection<PresentableModel>();
                    State = ModelState.Loading;
                    Async.Queue(DataContext, () =>
                    {
                        AsyncFetchProperties();
                        UI.Queue(UI, () => this.State = ModelState.Active);
                    });
                }
                return _propertyModelsCache;
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

        /// <summary>
        /// Schedules the underlying object for deletion.
        /// </summary>
        public void Delete()
        {
            UI.Verify();
            State = ModelState.Loading;
            Async.Queue(DataContext, () =>
            {
                DataContext.Delete(Object);
                UI.Queue(UI, () => State = ModelState.Active);
            });
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
        protected virtual void SetClassPropertyModels(ObjectClass cls, IEnumerable<BaseProperty> props)
        {
            UI.Verify();
            foreach (var pm in props)
            {
                PropertyModels.Add(Factory
                    .CreateModel(
                        DataMocks.LookupDefaultPropertyModelDescriptor(pm),
                        DataContext,
                        _object, pm));
            }
        }

        // TODO: should go to renderer and use database backed decision tables
        protected virtual void SetClassMethodModels(ObjectClass cls, IEnumerable<Method> methods)
        {
            UI.Verify();
            foreach (var pm in methods)
            {
                Debug.Assert(pm.Parameter.Single().IsReturnParameter);
                var retParam = pm.GetReturnParameter();

                if (retParam is BoolParameter && !retParam.IsList)
                {
                    PropertyModels.Add(Factory.CreateSpecificModel<NullableResultModel<Boolean>>(DataContext, _object, pm));
                }
                else if (pm is DateTimeParameter && !retParam.IsList)
                {
                    PropertyModels.Add(Factory.CreateSpecificModel<NullableResultModel<DateTime>>(DataContext, _object, pm));
                }
                else if (pm is DoubleParameter && !retParam.IsList)
                {
                    PropertyModels.Add(Factory.CreateSpecificModel<NullableResultModel<Double>>(DataContext, _object, pm));
                }
                else if (pm is IntParameter && !retParam.IsList)
                {
                    PropertyModels.Add(Factory.CreateSpecificModel<NullableResultModel<int>>(DataContext, _object, pm));
                }
                else if (pm is StringParameter && !retParam.IsList)
                {
                    PropertyModels.Add(Factory.CreateSpecificModel<ObjectResultModel<string>>(DataContext, _object, pm));
                }
                else if (pm is ObjectParameter && !retParam.IsList)
                {
                    PropertyModels.Add(Factory.CreateSpecificModel<ObjectResultModel<IDataObject>>(DataContext, _object, pm));
                }
                else
                {
                    Trace.TraceWarning("No model for property: '{0}' of Type '{1}'", pm, pm.GetType());
                }
            }
        }

        private string GetIconPath(string name)
        {
            string result = AppContext.Configuration.Client.DocumentStore
                + @"\GUI.Icons\"
                + name;
            result = System.IO.Path.IsPathRooted(result) ? result : Environment.CurrentDirectory + "\\" + result;
            return result;
        }

        protected void AsyncUpdateViewCache()
        {
            Async.Verify();

            // update Name
            _toStringCache = String.Format("{0} {1}", _object.ObjectState.ToUserString(), _object.ToString());
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

            // notify consumers if ID has changed
            if (e.PropertyName == "ID")
                AsyncOnPropertyChanged("ID");

            // defer updating the cache into another work item
            Async.Queue(DataContext, () =>
            {
                // flag to the user that something's happening
                UI.Queue(UI, () => this.State = ModelState.Loading);
                AsyncUpdateViewCache();
                // all updates done
                UI.Queue(UI, () => this.State = ModelState.Active);
            });


        }

        #endregion

        private IDataObject _object;

        /// <summary>
        /// Contrary to all other Properties, this directly exposes the 
        /// modelled IDataObject and thus may neither be thread-safe nor 
        /// high-latency.
        /// </summary>
        // other models need access to this.
        public IDataObject Object { get { return _object; } }
    }
}
