
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
    using Kistl.API.Configuration;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.App.GUI;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables.ValueViewModels;

    /// <summary>
    /// Proxies a whole IDataObject
    /// </summary>
    public class DataObjectViewModel
        : ViewModel, IDataErrorInfo, IViewModelWithIcon
    {
        public new delegate DataObjectViewModel Factory(IKistlContext dataCtx, IDataObject obj);

        protected readonly KistlConfig config;

        public DataObjectViewModel(
            IViewModelDependencies appCtx, KistlConfig config, IKistlContext dataCtx,
            IDataObject obj)
            : base(appCtx, dataCtx)
        {
            this.config = config;
            _object = obj;
            _object.PropertyChanged += Object_PropertyChanged;
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

        private ReadOnlyProjectedList<Property, BaseValueViewModel> _propertyModelList;
        public IReadOnlyList<BaseValueViewModel> PropertyModels
        {
            get
            {
                if (_propertyModelList == null)
                {
                    FetchPropertyModels();
                    _propertyModelList = new ReadOnlyProjectedList<Property, BaseValueViewModel>(_propertyList, p => _propertyModels[p], m => null); //m.Property);
                }
                return _propertyModelList;
            }
        }

        private LookupDictionary<Property, Property, BaseValueViewModel> _propertyModels;
        private void FetchPropertyModels()
        {
            if (_propertyModels == null)
            {
                FetchPropertyList();
                _propertyModels = new LookupDictionary<Property, Property, BaseValueViewModel>(_propertyList, k => k, v =>
                {
                    var result = ViewModelFactory.CreateViewModel<BaseValueViewModel.Factory>(v).Invoke(DataContext, v.GetPropertyValueModel(Object));
                    result.IsReadOnly = IsReadOnly;
                    return result;
                });
            }
        }

        private LookupDictionary<string, Property, BaseValueViewModel> _propertyModelsByName;
        public LookupDictionary<string, Property, BaseValueViewModel> PropertyModelsByName
        {
            get
            {
                if (_propertyModelsByName == null)
                {
                    FetchPropertyModels();
                    _propertyModelsByName = new LookupDictionary<string, Property, BaseValueViewModel>(
                        _propertyList,
                        k => k.Name,
                        v => _propertyModels[v]
                    );
                    OnPropertyModelsByNameCreated();
                }
                return _propertyModelsByName;
            }
        }

        protected virtual void OnPropertyModelsByNameCreated()
        {
        }

        private ReadOnlyCollection<PropertyGroupViewModel> _propertyGroups;
        public ReadOnlyCollection<PropertyGroupViewModel> PropertyGroups
        {
            get
            {
                if (_propertyGroups == null)
                {
                    _propertyGroups = new ReadOnlyCollection<PropertyGroupViewModel>(CreatePropertyGroups());

                }
                return _propertyGroups;
            }
        }

        protected virtual List<PropertyGroupViewModel> CreatePropertyGroups()
        {
            FetchPropertyModels();
            return _propertyList
                        .SelectMany(p => (String.IsNullOrEmpty(p.CategoryTags) ? "Uncategorised" : p.CategoryTags)
                                            .Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                                            .Select(s => new { Category = s, Property = p }))
                        .GroupBy(x => x.Category, x => x.Property)
                        .OrderBy(group => group.Key)
                        .Select<IGrouping<string, Property>, PropertyGroupViewModel>(group =>
                        {
                            var lst = group.Select(p => _propertyModels[p]).Cast<ViewModel>().ToList();

                            if (lst.Count == 1)
                            {
                                return ViewModelFactory.CreateViewModel<SinglePropertyGroupViewModel.Factory>().Invoke(
                                    DataContext, group.Key, lst);
                            }
                            else
                            {
                                return ViewModelFactory.CreateViewModel<MultiplePropertyGroupViewModel.Factory>().Invoke(
                                    DataContext, group.Key, lst);
                            }
                        })
                        .ToList();
        }

        public LookupDictionary<string, PropertyGroupViewModel, PropertyGroupViewModel> PropertyGroupsByName
        {
            get
            {
                return new LookupDictionary<string, PropertyGroupViewModel, PropertyGroupViewModel>(PropertyGroups, mdl => mdl.Title, mdl => mdl);
            }
        }

        private PropertyGroupViewModel _selectedPropertyGroup;
        public PropertyGroupViewModel SelectedPropertyGroup
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
                        foreach (var p in _propertyModels.Cast<BaseValueViewModel>())
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

        private ReadOnlyProjectedList<Method, BaseValueViewModel> _MethodResultsList;
        public IReadOnlyList<BaseValueViewModel> MethodResults
        {
            get
            {
                if (_MethodResultsList == null)
                {
                    FetchMethodModels();
                    _MethodResultsList = new ReadOnlyProjectedList<Method, BaseValueViewModel>(_MethodList, p => _MethodModels[p], null); //m => m.Method);
                }
                return _MethodResultsList;
            }
        }

        private LookupDictionary<Method, Method, BaseValueViewModel> _MethodModels;
        private void FetchMethodModels()
        {
            if (_MethodModels == null)
            {
                FetchMethodList();
                _MethodModels = new LookupDictionary<Method, Method, BaseValueViewModel>(
                    _MethodList,
                    k => k,
                    v => ViewModelFactory.CreateViewModel<BaseValueViewModel.Factory>(v.GetReturnParameter()).Invoke(DataContext, v.GetValueModel(Object))
                );
            }
        }

        private LookupDictionary<string, Method, BaseValueViewModel> _MethodResultsByName;
        public LookupDictionary<string, Method, BaseValueViewModel> MethodResultsByName
        {
            get
            {
                if (_MethodResultsByName == null)
                {
                    FetchMethodModels();
                    _MethodResultsByName = new LookupDictionary<string, Method, BaseValueViewModel>(
                        _MethodList,
                        k => k.Name,
                        v => _MethodModels[v]
                    );
                }
                return _MethodResultsByName;
            }
        }
        #endregion


        private ObservableCollection<ActionViewModel> _actionsCache;
        private ReadOnlyObservableCollection<ActionViewModel> _actionsView;
        public ReadOnlyObservableCollection<ActionViewModel> Actions
        {
            get
            {
                if (_actionsView == null)
                {
                    _actionsCache = new ObservableCollection<ActionViewModel>();
                    _actionsView = new ReadOnlyObservableCollection<ActionViewModel>(_actionsCache);
                    FetchActions();
                }
                return _actionsView;
            }
        }
        private IDictionary<string, ActionViewModel> _ActionViewModelsByName;
        public IDictionary<string, ActionViewModel> ActionViewModelsByName
        {
            get
            {
                if (_ActionViewModelsByName == null)
                {
                    _ActionViewModelsByName = Actions.ToDictionary(a => a.MethodName);
                }
                return _ActionViewModelsByName;
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
                actions.AddRange(cls.Methods.Where(m => m.IsDisplayable));
                cls = cls.BaseObjectClass;
            }

            SetClassActionViewModels(cls, actions);
        }

        // TODO: should go to renderer and use database backed decision tables
        protected virtual void SetClassActionViewModels(ObjectClass cls, IEnumerable<Method> methods)
        {
            foreach (var action in methods)
            {
                //Debug.Assert(action.Parameter.Count == 0);
                _actionsCache.Add(ViewModelFactory.CreateViewModel<ActionViewModel.Factory>(action).Invoke(DataContext, _object, action));
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

        private void Object_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // notify consumers if ID has changed
            if (e.PropertyName == "ID")
                OnPropertyChanged("ID");

            UpdateViewCache();

            OnObjectPropertyChanged(e.PropertyName);
        }

        protected virtual void OnObjectPropertyChanged(string propName)
        {
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
