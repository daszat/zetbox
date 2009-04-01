using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Utils;
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
            _object.PropertyChanged += ObjectPropertyChanged;
            // TODO: Optional machen!
            UpdateViewCache();
        }

        #region Public Interface

        private IDataObject _object;
        internal IDataObject Object { get { return _object; } }

        public int ID
        {
            get
            {
                return IsInDesignMode
                    ? 42
                    // this should always be instantaneous
                    : _object.ID;
            }
        }

        private ReadOnlyProjection<Property, PresentableModel> _propertyModels;
        public IReadOnlyCollection<PresentableModel> PropertyModels
        {
            get
            {
                if (_propertyModels == null)
                {
                    _propertyModels = new ReadOnlyProjection<Property,PresentableModel>(
                        FetchPropertyList().ToList(),
                        property => Factory.CreateModel(
                            DataMocks.LookupDefaultPropertyModelDescriptor(property),
                            DataContext,
                            new object[] { _object, property })
                        );
                }
                return _propertyModels;
            }
        }

        private ReadOnlyProjection<Method, PresentableModel> _methodResultsCache;
        public IReadOnlyCollection<PresentableModel> MethodResults
        {
            get
            {
                UI.Verify();

                if (_methodResultsCache == null)
                {
                    _methodResultsCache = new ReadOnlyProjection<Method,PresentableModel>(
                        FetchMethodList().ToList(),
                        method =>
                        {
                            ObjectClass cls = _object.GetObjectClass(MetaContext);
                            return ModelFromMethod(cls, method);
                        });
                }
                return _methodResultsCache;
            }
        }

        private ObservableCollection<ActionModel> _actionsCache;
        private ReadOnlyObservableCollection<ActionModel> _actionsView;
        public ReadOnlyObservableCollection<ActionModel> Actions
        {
            get
            {
                if (_actionsView == null)
                {
                    _actionsCache = new ObservableCollection<ActionModel>();
                    _actionsView = new ReadOnlyObservableCollection<ActionModel>(_actionsCache);
                    FetchActions();
                }
                return _actionsView;
            }
        }

        private string _toStringCache;
        public string Name
        {
            get
            {
                return _toStringCache;
            }
            private set
            {
                if (value != _toStringCache)
                {
                    _toStringCache = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string _longNameCache;
        public string LongName
        {
            get
            {
                return _longNameCache;
            }
            private set
            {
                if (value != _longNameCache)
                {
                    _longNameCache = value;
                    OnPropertyChanged("LongName");
                }
            }
        }

        private string _iconPathCache;
        public string IconPath
        {
            get
            {
                return _iconPathCache;
            }
            set
            {
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
            DataContext.Delete(Object);
        }

        public InterfaceType GetInterfaceType()
        {
            return Object.GetInterfaceType();
        }

        #endregion

        #region Utilities and UI callbacks

        private IEnumerable<Property> FetchPropertyList()
        {
            // load properties from MetaContext
            ObjectClass cls = _object.GetObjectClass(MetaContext);
            var props = new List<Property>();
            while (cls != null)
            {
                foreach (Property p in cls.Properties)
                {
                    props.Add(p);
                }
                cls = cls.BaseObjectClass;
            }

            return props;
        }

        private IEnumerable<Method> FetchMethodList()
        {
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

        private void FetchActions()
        {
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

            SetClassActionModels(cls, actions);
        }

        // TODO: should go to renderer and use database backed decision tables
        protected virtual void SetClassActionModels(ObjectClass cls, IEnumerable<Method> methods)
        {
            foreach (var action in methods)
            {
                Debug.Assert(action.Parameter.Count == 0);
                _actionsCache.Add(Factory.CreateSpecificModel<ActionModel>(DataContext, _object, action));
            }
        }

        // TODO: should go to renderer and use database backed decision tables
        protected virtual PresentableModel ModelFromMethod(ObjectClass cls, Method pm)
        {
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
            result = Path.IsPathRooted(result) ? result : Environment.CurrentDirectory + "\\" + result;
            return result;
        }

        protected void UpdateViewCache()
        {

            // update Name
            _toStringCache = String.Format("{0} {1}",
                _object.ObjectState.ToUserString(),
                _object.ToString());
            _longNameCache = String.Format("{0}: {1}",
                _object.GetInterfaceType().Type.FullName,
                _toStringCache);
            OnPropertyChanged("Name");
            OnPropertyChanged("LongName");

            // update IconPath
            Icon icon = GetIcon();
            if (icon != null)
            {
                string newIconPath = GetIconPath(icon.IconFile);
                IconPath = newIconPath;
            }
            else
            {
                IconPath = "";
            }
        }

        /// <summary>
        /// Override this to present a custom icon
        /// </summary>
        /// <returns>an <see cref="Icon"/> describing the desired icon</returns>
        protected virtual Icon GetIcon()
        {
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

        private void ObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // notify consumers if ID has changed
            if (e.PropertyName == "ID")
                OnPropertyChanged("ID");

            UpdateViewCache();
            // all updates done
        }

        #endregion

    }
}
