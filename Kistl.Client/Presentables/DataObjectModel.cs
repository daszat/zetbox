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

        //public static DataObjectModel CreateDesignMock()
        //{
        //    return new DataObjectModel(true);
        //}

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

        private ImmutableAsyncList<BaseProperty, PresentableModel> _propertyModels;
        public ReadOnlyCollection<PresentableModel> PropertyModels
        {
            get
            {
                UI.Verify();

                if (_propertyModels == null)
                {
                    _propertyModels = AsyncListFactory.UiCreateImmutable<BaseProperty, PresentableModel>(
                        AppContext, DataContext, this,
                        AsyncFetchPropertyList,
                        property => Factory.CreateModel(
                            DataMocks.LookupDefaultPropertyModelDescriptor(property),
                            DataContext,
                            new object[] { _object, property })
                        );
                }
                return _propertyModels.GetUiView();
            }
        }

        private ImmutableAsyncList<Method, PresentableModel> _methodResultsCache;
        public ReadOnlyCollection<PresentableModel> MethodResults
        {
            get
            {
                UI.Verify();

                if (_methodResultsCache == null)
                {
                    _methodResultsCache = AsyncListFactory.UiCreateImmutable<Method, PresentableModel>(
                        AppContext, DataContext, this,
                        AsyncFetchMethodList,
                        method =>
                        {
                            // TODO: cache cls
                            ObjectClass cls = _object.GetObjectClass(MetaContext);
                            return ModelFromMethod(cls, method);
                        });
                }
                return _methodResultsCache.GetUiView();
            }
        }

        private ObservableCollection<ActionModel> _actionsCache;
        private ReadOnlyObservableCollection<ActionModel> _actionsView;
        public ReadOnlyObservableCollection<ActionModel> Actions
        {
            get
            {
                UI.Verify();
                if (_actionsView == null)
                {
                    _actionsCache = new ObservableCollection<ActionModel>();
                    _actionsView = new ReadOnlyObservableCollection<ActionModel>(_actionsCache);
                    State = ModelState.Loading;
                    Async.Queue(DataContext, () =>
                    {
                        AsyncFetchActions();
                        UI.Queue(UI, () => this.State = ModelState.Active);
                    });
                }
                return _actionsView;
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

        private IEnumerable<BaseProperty> AsyncFetchPropertyList()
        {
            Async.Verify();

            // load properties from MetaContext
            ObjectClass cls = _object.GetObjectClass(MetaContext);
            var props = new List<BaseProperty>();
            while (cls != null)
            {
                foreach (BaseProperty p in cls.Properties)
                {
                    props.Add(p);
                }
                cls = cls.BaseObjectClass;
            }

            return props;
        }

        private IEnumerable<Method> AsyncFetchMethodList()
        {
            Async.Verify();

            // load properties from MetaContext
            ObjectClass cls = _object.GetObjectClass(MetaContext);
            var methods = new List<Method>();
            while (cls != null)
            {
                foreach (Method m in cls.Methods.Where(m =>
                    m.IsDisplayable
                    && m.Parameter.Count == 1
                    && m.Parameter.Single().IsReturnParameter))
                {
                    methods.Add(m);
                }
                cls = cls.BaseObjectClass;
            }

            return methods;
        }

        private void AsyncFetchActions()
        {
            Async.Verify();

            // load properties
            ObjectClass cls = _object.GetObjectClass(MetaContext);
            var actions = new List<Method>();
            while (cls != null)
            {
                cls.Methods
                    .Where(m => m.IsDisplayable && m.Parameter.Count == 0)
                    .ForEach<Method>(m => actions.Add(m));

                cls = cls.BaseObjectClass;
            }

            UI.Queue(UI, () =>
            {
                SetClassActionModels(cls, actions);
            });
        }

        // TODO: should go to renderer and use database backed decision tables
        protected virtual void SetClassActionModels(ObjectClass cls, IEnumerable<Method> methods)
        {
            UI.Verify();
            foreach (var action in methods)
            {
                Debug.Assert(action.Parameter.Count == 0);
                _actionsCache.Add(Factory.CreateSpecificModel<ActionModel>(DataContext, _object, action));
            }
        }

        // TODO: should go to renderer and use database backed decision tables
        protected virtual PresentableModel ModelFromMethod(ObjectClass cls, Method pm)
        {
            UI.Verify();
            Debug.Assert(pm.Parameter.Single().IsReturnParameter);
            var retParam = pm.GetReturnParameter();

            if (retParam is BoolParameter && !retParam.IsList)
            {
                return (Factory.CreateSpecificModel<NullableResultModel<Boolean>>(DataContext, _object, pm));
            }
            else if (pm is DateTimeParameter && !retParam.IsList)
            {
                return (Factory.CreateSpecificModel<NullableResultModel<DateTime>>(DataContext, _object, pm));
            }
            else if (pm is DoubleParameter && !retParam.IsList)
            {
                return (Factory.CreateSpecificModel<NullableResultModel<Double>>(DataContext, _object, pm));
            }
            else if (pm is IntParameter && !retParam.IsList)
            {
                return (Factory.CreateSpecificModel<NullableResultModel<int>>(DataContext, _object, pm));
            }
            else if (pm is StringParameter && !retParam.IsList)
            {
                return (Factory.CreateSpecificModel<ObjectResultModel<string>>(DataContext, _object, pm));
            }
            else if (pm is ObjectParameter && !retParam.IsList)
            {
                return (Factory.CreateSpecificModel<ObjectResultModel<IDataObject>>(DataContext, _object, pm));
            }
            else
            {
                Trace.TraceWarning("No model for property: '{0}' of Type '{1}'", pm, pm.GetType());
                return null;
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
                icon = _object.GetObjectClass(MetaContext).DefaultIcon;
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
        /// low-latency.
        /// </summary>
        // other models need access to this.
        public IDataObject Object { get { return _object; } }
    }
}
