
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.App.GUI;
    using Kistl.API.Configuration;

    /// <summary>
    /// Proxies a whole IDataObject
    /// </summary>
    public class DataObjectModel
        : ViewModel, IDataErrorInfo, IViewModelWithIcon
    {
        public new delegate DataObjectModel Factory(IKistlContext dataCtx, IDataObject obj);

        protected readonly KistlConfig config;

        public DataObjectModel(
            IViewModelDependencies appCtx, KistlConfig config, IKistlContext dataCtx,
            IDataObject obj)
            : base(appCtx, dataCtx)
        {
            this.config = config;
            _object = obj;
            _object.PropertyChanged += ObjectPropertyChanged;
            // TODO: Optional machen!
            InitialiseViewCache();
        }

        #region Property Management

        private List<Property> _propertyList = null;
        private void FetchPropertyList()
        {
            if (_propertyList == null)
            {
                // load properties from MetaContext
                ObjectClass cls = _object.GetObjectClass(FrozenContext);
                _propertyList = new List<Property>();
                while (cls != null)
                {
                    foreach (Property p in cls.Properties)
                    {
                        _propertyList.Add(p);
                    }
                    cls = cls.BaseObjectClass;
                }
            }

        }

        private ReadOnlyProjectedList<Property, BasePropertyViewModel> _propertyModelList;
        public IReadOnlyList<BasePropertyViewModel> PropertyModels
        {
            get
            {
                if (_propertyModelList == null)
                {
                    FetchPropertyModels();
                    _propertyModelList = new ReadOnlyProjectedList<Property, BasePropertyViewModel>(_propertyList, p => _propertyModels[p], m => m.Property);
                }
                return _propertyModelList;
            }
        }

        private LookupDictionary<Property, Property, BasePropertyViewModel> _propertyModels;
        private void FetchPropertyModels()
        {
            if (_propertyModels == null)
            {
                FetchPropertyList();
                _propertyModels = new LookupDictionary<Property,Property,BasePropertyViewModel>( _propertyList, k => k, v =>
                {
                    var result = ModelFactory.CreateViewModel<BasePropertyViewModel.Factory>(v).Invoke(DataContext, Object, v);
                    result.IsReadOnly = IsReadOnly;
                    return result;
                });
            }
        }

        private LookupDictionary<string, Property, BasePropertyViewModel> _propertyModelsByName;
        public LookupDictionary<string, Property, BasePropertyViewModel> PropertyModelsByName
        {
            get
            {
                if (_propertyModelsByName == null)
                {
                    FetchPropertyModels();
                    _propertyModelsByName = new LookupDictionary<string, Property, BasePropertyViewModel>(
                        _propertyList,
                        k => k.Name,
                        v => _propertyModels[v]
                    );
                }
                return _propertyModelsByName;
            }
        }

        private ReadOnlyCollection<PropertyGroupModel> _propertyGroups;
        public ReadOnlyCollection<PropertyGroupModel> PropertyGroups
        {
            get
            {
                if (_propertyGroups == null)
                {
                    _propertyGroups = new ReadOnlyCollection<PropertyGroupModel>(CreatePropertyGroups());

                }
                return _propertyGroups;
            }
        }

        protected virtual List<PropertyGroupModel> CreatePropertyGroups()
        {
            FetchPropertyModels();
            return _propertyList
                        .SelectMany(p => (String.IsNullOrEmpty(p.CategoryTags) ? "Uncategorised" : p.CategoryTags)
                                            .Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                                            .Select(s => new { Category = s, Property = p }))
                        .GroupBy(x => x.Category, x => x.Property)
                        .OrderBy(group => group.Key)
                        .Select<IGrouping<string, Property>, PropertyGroupModel>(group =>
                        {
                            var lst = group.Select(p => _propertyModels[p]).Cast<ViewModel>().ToList();

                            if (lst.Count == 1)
                            {
                                return ModelFactory.CreateViewModel<SinglePropertyGroupModel.Factory>().Invoke(
                                    DataContext, group.Key, lst);
                            }
                            else
                            {
                                return ModelFactory.CreateViewModel<MultiplePropertyGroupModel.Factory>().Invoke(
                                    DataContext, group.Key, lst);
                            }
                        })
                        .ToList();
        }

        public LookupDictionary<string, PropertyGroupModel, PropertyGroupModel> PropertyGroupsByName
        {
            get
            {
                return new LookupDictionary<string, PropertyGroupModel, PropertyGroupModel>(PropertyGroups, mdl => mdl.Title, mdl => mdl);
            }
        }

        private PropertyGroupModel _selectedPropertyGroup;
        public PropertyGroupModel SelectedPropertyGroup
        {
            get
            {
                if (_selectedPropertyGroup == null && PropertyGroups.Count > 0)
                {
                    _selectedPropertyGroup = PropertyGroupsByName.ContainsKey("Summary") ? PropertyGroupsByName["Summary"] : PropertyGroups.FirstOrDefault();
                }
                return _selectedPropertyGroup;
            }
            set
            {
                // only accept new value if it is a contained model
                // Do not accept null's
                if (value != null && (PropertyGroupsByName.ContainsKey(value.Name) && PropertyGroupsByName[value.Name] == value))
                {
                    _selectedPropertyGroup = value;
                    OnPropertyChanged("SelectedPropertyGroup");
                    OnPropertyChanged("SelectedPropertyGroupName");
                }
            }
        }
        public string SelectedPropertyGroupName
        {
            get
            {
                return SelectedPropertyGroup == null ? null : SelectedPropertyGroup.Name;
            }
            set
            {
                if (PropertyGroupsByName.ContainsKey(value))
                {
                    SelectedPropertyGroup = PropertyGroupsByName[value];
                }
            }
        }
        #endregion

        #region Public Interface

        private IDataObject _object;
        public IDataObject Object { get { return _object; } }

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

        protected bool isReadOnlyStore = false;
        public virtual bool IsReadOnly
        {
            get
            {
                return isReadOnlyStore;
            }
            set
            {
                if (isReadOnlyStore != value)
                {
                    isReadOnlyStore = value;
                    if (_propertyModels != null)
                    {
                        foreach (var p in _propertyModels.Cast<BasePropertyViewModel>())
                        {
                            p.IsReadOnly = IsReadOnly;
                        }
                    }
                    OnPropertyChanged("IsReadOnly");
                }
            }
        }

        #region MethodResults

        private List<Method> _MethodList = null;
        private void FetchMethodList()
        {
            if (_MethodList != null) return;
            // load properties from MetaContext
            ObjectClass cls = _object.GetObjectClass(FrozenContext);
            var methods = new List<Method>();
            while (cls != null)
            {
                foreach (Method m in cls.Methods.Where(m =>
                    m.IsDisplayable
                    && m.Parameter.Count == 1
                    && m.Parameter.Single().IsReturnParameter
                    && !(m.Parameter.Single() is ObjectParameter))) // Could be a CreateRelatedUseCase, and we don't want to go around creating new objects
                {
                    methods.Add(m);
                }
                cls = cls.BaseObjectClass;
            }

            _MethodList = methods;
        }

        private ReadOnlyProjectedList<Method, BaseMethodResultModel> _MethodResultsList;
        public IReadOnlyList<BaseMethodResultModel> MethodResults
        {
            get
            {
                if (_MethodResultsList == null)
                {
                    FetchMethodModels();
                    _MethodResultsList = new ReadOnlyProjectedList<Method, BaseMethodResultModel>(_MethodList, p => _MethodModels[p], m => m.Method);
                }
                return _MethodResultsList;
            }
        }

        private LookupDictionary<Method, Method, BaseMethodResultModel> _MethodModels;
        private void FetchMethodModels()
        {
            if (_MethodModels == null)
            {
                FetchMethodList();
                _MethodModels = new LookupDictionary<Method, Method, BaseMethodResultModel>(
                    _MethodList, 
                    k => k,
                    v => ModelFromMethod(v)
                );
            }
        }

        private LookupDictionary<string, Method, BaseMethodResultModel> _MethodResultsByName;
        public LookupDictionary<string, Method, BaseMethodResultModel> MethodResultsByName
        {
            get
            {
                if (_MethodResultsByName == null)
                {
                    FetchMethodModels();
                    _MethodResultsByName = new LookupDictionary<string, Method, BaseMethodResultModel>(
                        _MethodList,
                        k => k.Name,
                        v => _MethodModels[v]
                    );
                }
                return _MethodResultsByName;
            }
        }
        #endregion


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
        private IDictionary<string, ActionModel> _actionModelsByName;
        public IDictionary<string, ActionModel> ActionModelsByName
        {
            get
            {
                if (_actionModelsByName == null)
                {
                    _actionModelsByName = Actions.ToDictionary(a => a.Name);
                }
                return _actionModelsByName;
            }
        }



        private string _nameCache;
        public override string Name
        {
            get
            {
                return _nameCache;
            }
        }

        private string _longNameCache;
        public string LongName
        {
            get
            {
                return _longNameCache;
            }
        }

        public override string ToString()
        {
            return _nameCache;
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
            return DataContext.GetInterfaceType(Object);
        }

        #endregion

        #region Utilities and UI callbacks
        private void FetchActions()
        {
            // load properties
            ObjectClass cls = _object.GetObjectClass(FrozenContext);
            var actions = new List<Method>();
            while (cls != null)
            {
                actions.AddRange(cls.Methods
                    .Where(m => m.IsDisplayable
                        && (m.Parameter.Count == 0
                        || (m.Parameter.Count == 1
                            && m.Parameter.Single().IsReturnParameter
                            && m.Name.StartsWith("Create")))));

                cls = cls.BaseObjectClass;
            }

            SetClassActionModels(cls, actions);
        }

        // TODO: should go to renderer and use database backed decision tables
        protected virtual void SetClassActionModels(ObjectClass cls, IEnumerable<Method> methods)
        {
            foreach (var action in methods)
            {
                //Debug.Assert(action.Parameter.Count == 0);
                _actionsCache.Add(ModelFactory.CreateViewModel<ActionModel.Factory>().Invoke(DataContext, _object, action));
            }
        }

        // TODO: should go to renderer and use database backed decision tables
        protected virtual BaseMethodResultModel ModelFromMethod(Method pm)
        {
            Debug.Assert(pm.Parameter.Single().IsReturnParameter);
            var retParam = pm.GetReturnParameter();

            if (retParam is BoolParameter && !retParam.IsList)
            {
                return (ModelFactory.CreateViewModel<NullableResultModel<Boolean>.Factory>().Invoke(DataContext, _object, pm));
            }
            else if (retParam is DateTimeParameter && !retParam.IsList)
            {
                return (ModelFactory.CreateViewModel<NullableResultModel<DateTime>.Factory>().Invoke(DataContext, _object, pm));
            }
            else if (retParam is DoubleParameter && !retParam.IsList)
            {
                return (ModelFactory.CreateViewModel<NullableResultModel<Double>.Factory>().Invoke(DataContext, _object, pm));
            }
            else if (retParam is IntParameter && !retParam.IsList)
            {
                return (ModelFactory.CreateViewModel<NullableResultModel<int>.Factory>().Invoke(DataContext, _object, pm));
            }
            else if (retParam is DecimalParameter && !retParam.IsList)
            {
                return (ModelFactory.CreateViewModel<NullableResultModel<decimal>.Factory>().Invoke(DataContext, _object, pm));
            }
            else if (retParam is StringParameter && !retParam.IsList)
            {
                return (ModelFactory.CreateViewModel<ObjectResultModel<string>.Factory>().Invoke(DataContext, _object, pm));
            }
            else if (retParam is ObjectParameter && !retParam.IsList)
            {
                return (ModelFactory.CreateViewModel<ObjectResultModel<IDataObject>.Factory>().Invoke(DataContext, _object, pm));
            }
            //else if (retParam is EnumParameter && !retParam.IsList)
            //{
            //    return (ModelFactory.CreateViewModel<NullableResultModel<?>.Factory>().Invoke(DataContext, _object, pm));
            //}
            else
            {
                Logging.Log.WarnFormat("No model for method: '{0}' of return type '{1}'", pm, pm.GetReturnParameter().GetParameterTypeString());
                return null;
            }
        }

        private void InitialiseViewCache()
        {
            // update Name
            _nameCache = String.Format("{0} {1}",
                _object.ObjectState.ToUserString(),
                _object.ToString());
            _longNameCache = String.Format("{0}: {1}",
                _object.ReadOnlyContext.GetInterfaceType(_object).Type.FullName,
                _nameCache);
        }

        protected void UpdateViewCache()
        {
            InitialiseViewCache();
            OnPropertyChanged("Name");
            OnPropertyChanged("LongName");

        }

        private Icon _iconCache = null;
        /// <summary>
        /// Override this to present a custom icon
        /// </summary>
        /// <returns>an <see cref="Icon"/> describing the desired icon</returns>
        public virtual Icon Icon
        {
            get
            {
                if (_iconCache == null)
                {
                    if (_object is Icon)
                    {
                        _iconCache = (Icon)_object;
                    }
                    else
                    {
                        _iconCache = _object.GetObjectClass(FrozenContext).DefaultIcon;
                    }
                }
                return _iconCache;
            }
        }

        public override ControlKind RequestedKind
        {
            get
            {
                return base.RequestedKind ?? _object.GetObjectClass(FrozenContext).RequestedKind;
            }
            set
            {
                base.RequestedKind = value;
            }
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

        #region IDataErrorInfo Members

        public string Error
        {
            get { return String.Join("\n", PropertyModels.OfType<IDataErrorInfo>().Select(idei => idei.Error).Where(s => !String.IsNullOrEmpty(s)).ToArray()); }
        }

        public string this[string columnName]
        {
            get
            {
                if (PropertyModelsByName.ContainsKey(columnName))
                {
                    var idei = PropertyModelsByName[columnName] as IDataErrorInfo;
                    if (idei != null)
                    {
                        return idei.Error;
                    }
                }
                return String.Empty;
            }
        }

        #endregion
    }
}
