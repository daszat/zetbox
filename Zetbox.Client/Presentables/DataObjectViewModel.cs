// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Client.Presentables
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
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.API.Common;
    using Zetbox.Client.GUI;

    /// <summary>
    /// Proxies a whole IDataObject
    /// </summary>
    [ViewModelDescriptor]
    public class DataObjectViewModel
        : ViewModel, IDataErrorInfo
    {
        public new delegate DataObjectViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IDataObject obj);

        public static DataObjectViewModel Fetch(IViewModelFactory f, IZetboxContext dataCtx, ViewModel parent, IDataObject obj)
        {
            return (DataObjectViewModel)dataCtx.GetViewModelCache(f.PerfCounter).LookupOrCreate(obj, () => f.CreateViewModel<DataObjectViewModel.Factory>(obj).Invoke(dataCtx, parent, obj));
        }

        public DataObjectViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            IDataObject obj)
            : base(appCtx, dataCtx, parent)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            _object = obj;
            _object.PropertyChanged += Object_PropertyChanged;
            isReadOnlyStore = _object.IsReadonly;
            dataCtx.IsElevatedModeChanged += new EventHandler(dataCtx_IsElevatedModeChanged);

            ValidationManager.Register(this);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            ValidationManager.Unregister(this);
        }

        void dataCtx_IsElevatedModeChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("IsReadOnly");
            OnHighlightChanged();
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
        /// <summary>
        /// A read only list of all known BaseValueViewModels fetched from the ObjectClass.
        /// </summary>
        public API.Utils.IReadOnlyList<BaseValueViewModel> PropertyModels
        {
            get
            {
                if (_propertyModelList == null)
                {
                    FetchPropertyModels();
                    _propertyModelList = new ReadOnlyProjectedList<Property, BaseValueViewModel>(
                        _propertyList,
                        p => _propertyModels[p],
                        m => null); //m.Property);
                    OnPropertyModelsCreated();
                }
                return _propertyModelList;
            }
        }

        /// <summary>
        /// Called after the PropertyModels list has been created.
        /// </summary>
        protected virtual void OnPropertyModelsCreated()
        {
        }

        private LookupDictionary<Property, Property, BaseValueViewModel> _propertyModels;
        private void FetchPropertyModels()
        {
            if (_propertyModels == null)
            {
                FetchPropertyList();
                _propertyModels = new LookupDictionary<Property, Property, BaseValueViewModel>(
                    _propertyList,
                    k => k,
                    v =>
                    {
                        var result = BaseValueViewModel.Fetch(ViewModelFactory, DataContext, this, v, v.GetPropertyValueModel(Object));
                        result.IsReadOnly = IsReadOnly;
                        return result;
                    });
            }
        }

        private LookupDictionary<string, Property, BaseValueViewModel> _propertyModelsByName;
        /// <summary>
        /// Dictionary of BaseValueViewModels with the property name as the key.
        /// </summary>
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

        /// <summary>
        /// Called after the PropertyModelsByName dictionary has been created.
        /// </summary>
        protected virtual void OnPropertyModelsByNameCreated()
        {
        }

        private ReadOnlyCollection<PropertyGroupViewModel> _propertyGroups;

        /// <summary>
        /// A read only collection of property groups. See CreatePropertyGroups for more information.
        /// </summary>
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

        public sealed class PropertyGroupCollection : List<ViewModel>
        {
            public PropertyGroupCollection()
            {

            }

            public PropertyGroupCollection(IEnumerable<ViewModel> lst)
                : base(lst)
            {

            }

            public ViewModel this[string propName]
            {
                get
                {
                    return this
                        .OfType<BaseValueViewModel>()
                        .Single(vm => ((BasePropertyValueModel)vm.ValueModel).Property.Name == propName);
                }
            }

            public IEnumerable<KeyValuePair<string, ViewModel>> GetWithKeys()
            {
                return this
                    .OfType<BaseValueViewModel>()
                    .Select(vm => new KeyValuePair<string, ViewModel>(((BasePropertyValueModel)vm.ValueModel).Property.Name, vm));
            }
        }

        /// <summary>
        /// Creates the property groups list.
        /// </summary>
        /// <remarks>
        /// Property groups are created based on the properties summary tags. Due to the fact, 
        /// that properties can have more than one summary
        /// tag, properties may appear in more than one property group. Properties with no
        /// summary tags appears in the "Uncategorised" group. Note, that currently
        /// summary tags may not contain spaces as the space is defined as the seperator.
        /// You can override this method to add custom property groups.
        /// </remarks>
        /// <returns>List of property groups</returns>
        protected virtual List<PropertyGroupViewModel> CreatePropertyGroups()
        {
            FetchPropertyModels();
            var isAdmin = CurrentPrincipal != null ? CurrentPrincipal.IsAdministrator() : false;
            var zbBaseModule = FrozenContext.GetQuery<Module>().Single(m => m.Name == "ZetboxBase");

            return _propertyList
                        .SelectMany(p => (String.IsNullOrEmpty(p.CategoryTags) ? Properties.Resources.Uncategorised : p.CategoryTags)
                                            .Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                                            .Select(s => new { Category = s.Trim(), SortKey = GetCategorySortKey(s.Trim()), Property = p }))
                        .Where(x => isAdmin || x.Category != "Hidden")
                        .GroupBy(x => x.SortKey, x => x)
                        .OrderBy(group => group.Key)
                        .Select(group =>
                        {
                            var firstProp = group.First();
                            var tag = firstProp.Category;
                            var lst = new PropertyGroupCollection(group.Select(p => _propertyModels[p.Property]));
                            var propModule = firstProp.Property.Module;
                            var translatedTag = TranslatePropertyGroupTag(tag, propModule, zbBaseModule);
                            return CreatePropertyGroup(tag, translatedTag, lst);
                        })
                        .ToList();
        }

        protected virtual string TranslatePropertyGroupTag(string tag, Module propModule, Module zbBaseModule)
        {
            var translatedTag = Assets.GetString(zbBaseModule, ZetboxAssetKeys.CategoryTags, tag);
            if (string.IsNullOrWhiteSpace(translatedTag))
            {
                translatedTag = Assets.GetString(propModule, ZetboxAssetKeys.CategoryTags, tag, tag);
            }

            return translatedTag;
        }

        protected virtual PropertyGroupViewModel CreatePropertyGroup(string tag, string translatedTag, PropertyGroupCollection lst)
        {
            if (lst.Count == 1)
            {
                return (PropertyGroupViewModel)ViewModelFactory.CreateViewModel<SinglePropertyGroupViewModel.Factory>().Invoke(
                    DataContext, this, tag, translatedTag, lst);
            }
            else
            {
                return (PropertyGroupViewModel)ViewModelFactory.CreateViewModel<MultiplePropertyGroupViewModel.Factory>().Invoke(
                    DataContext, this, tag, translatedTag, lst);
            }
        }

        private string GetCategorySortKey(string cat)
        {
            switch (cat)
            {
                case "Summary": return "1|Summary";
                case "Main": return "2|Main";
                default: return "3|" + cat;
                case "Meta": return "4|Meta";
            }
        }

        public LookupDictionary<string, PropertyGroupViewModel, PropertyGroupViewModel> PropertyGroupsByName
        {
            get
            {
                return new LookupDictionary<string, PropertyGroupViewModel, PropertyGroupViewModel>(PropertyGroups, mdl => mdl.TagName, mdl => mdl);
            }
        }

        private PropertyGroupViewModel _selectedPropertyGroup;
        public PropertyGroupViewModel SelectedPropertyGroup
        {
            get
            {
                if (_selectedPropertyGroup == null && PropertyGroups.Count > 0)
                {
                    var main = PropertyGroupsByName.ContainsKey("Main") ? PropertyGroupsByName["Main"] : null;
                    var summary = PropertyGroupsByName.ContainsKey("Summary") ? PropertyGroupsByName["Summary"] : null;
                    _selectedPropertyGroup = main ?? summary ?? PropertyGroups.FirstOrDefault();
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
        public DataObjectState ObjectState { get { return _object.ObjectState; } }

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
        /// <summary>
        /// Specifies, that the underlying object should be read only. Note: this sets every property to read only true. 
        /// In constructors use isReadOnlyStore instead. It will be propergated down later.
        /// </summary>
        public virtual bool IsReadOnly
        {
            get
            {
                if (DataContext.IsElevatedMode) return false;
                if (DataContext.IsReadonly) return true;
                if (Object.IsReadonly) return true;
                return isReadOnlyStore;
            }
            set
            {
                if (isReadOnlyStore != value)
                {
                    isReadOnlyStore = value;
                    var propIsReadOnly = this.IsReadOnly;

                    if (_propertyModels != null)
                    {
                        foreach (var e in _propertyModels)
                        {
                            e.Value.IsReadOnly = propIsReadOnly;
                        }
                    }
                    OnPropertyChanged("IsReadOnly");
                }
            }
        }

        private string _nameCache;
        public override string Name
        {
            get
            {
                InitialiseToStringCache();
                return _nameCache;
            }
        }

        public override string ToString()
        {
            return Name;
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

        #region Actions
        private List<ActionViewModel> _allActionsCache;
        private ObservableCollection<ICommandViewModel> _actionsCommandCache;
        private ReadOnlyObservableCollection<ICommandViewModel> _actionsCommandView;
        private void FetchActions()
        {
            if (_actionsCommandCache == null)
            {
                var actions = new List<Method>();
                _actionsCommandCache = new ObservableCollection<ICommandViewModel>();
                _actionsCommandView = new ReadOnlyObservableCollection<ICommandViewModel>(_actionsCommandCache);

                // load properties
                ObjectClass cls = _object.GetObjectClass(FrozenContext);
                while (cls != null)
                {
                    actions.AddRange(cls.Methods.Where(m => m.IsDisplayable));
                    cls = cls.BaseObjectClass;
                }

                _allActionsCache = ObjectReferenceHelper.AddActionViewModels(_actionsCommandCache, _object, actions, this, ViewModelFactory);
            }
        }

        public ReadOnlyObservableCollection<ICommandViewModel> Actions
        {
            get
            {
                if (_actionsCommandView == null)
                {
                    FetchActions();
                }
                return _actionsCommandView;
            }
        }
        private IDictionary<string, ActionViewModel> _ActionViewModelsByName;
        public IDictionary<string, ActionViewModel> ActionViewModelsByName
        {
            get
            {
                if (_ActionViewModelsByName == null)
                {
                    FetchActions();
                    _ActionViewModelsByName = _allActionsCache.ToDictionary(a => a.MethodName);
                }
                return _ActionViewModelsByName;
            }
        }
        #endregion

        #region Commands
        protected override ObservableCollection<ICommandViewModel> CreateCommands()
        {
            var result = base.CreateCommands();

            if (CanMerge())
            {
                result.Add(MergeCommand);
            }

            return result;
        }

        #region Merge command
        private ICommandViewModel _MergeCommand = null;
        public ICommandViewModel MergeCommand
        {
            get
            {
                if (_MergeCommand == null)
                {
                    _MergeCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        DataObjectViewModelResources.MergeCommand_Name,
                        DataObjectViewModelResources.MergeCommand_Tooltip,
                        Merge, 
                        CanMerge,
                        () => DataObjectViewModelResources.MergeCommand_Reason);
                    _MergeCommand.Icon = IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.reload_png.Find(FrozenContext));
                }
                return _MergeCommand;
            }
        }

        private bool CanMerge()
        {
            if (DataContext.IsElevatedMode) return true;

            return Object.CurrentAccessRights.HasWriteRights()
                && Object.GetObjectClass(FrozenContext).ImplementsIMergeable()
                && GetWorkspace() is IMultipleInstancesManager;
        }

        public void Merge()
        {
            var ws = (IMultipleInstancesManager)GetWorkspace();

            var task = ViewModelFactory.CreateViewModel<ObjectEditor.MergeObjectsTaskViewModel.Factory>().Invoke(DataContext, GetWorkspace(), Object, null);
            ws.AddItem(task);
            ws.SelectedItem = task;
        }
        #endregion
        #endregion

        #region Utilities and UI callbacks

        private void InitialiseToStringCache()
        {
            // update Name
            if (_nameCache == null)
            {
                _nameCache = _object.ToString();
            }
        }

        protected void UpdateToStringCache()
        {
            _nameCache = null;
            OnPropertyChanged("Name");
        }

        private System.Drawing.Image _iconCache = null;
        /// <summary>
        /// Override this to present a custom icon
        /// </summary>
        /// <returns>an <see cref="Icon"/> describing the desired icon</returns>
        public override System.Drawing.Image Icon
        {
            get
            {
                if (_iconCache == null)
                {
                    if (_object is Icon)
                    {
                        _iconCache = IconConverter.ToImage((Icon)_object);
                    }
                    else
                    {
                        _iconCache = IconConverter.ToImage(_object.GetObjectClass(FrozenContext).DefaultIcon);
                    }
                }
                return _iconCache;
            }
            set
            {
                if (_iconCache != null)
                {
                    _iconCache = value;
                    OnPropertyChanged("Icon");
                }
            }
        }

        public override Highlight Highlight
        {
            get
            {
                if (DataContext.IsElevatedMode) return Highlight.Bad;
                if (Object.CurrentAccessRights.HasOnlyReadRightsOrNone()) return Highlight.Deactivated;
                // Reflect readonly only on changable context
                if (!DataContext.IsReadonly && (!IsEnabled || IsReadOnly)) return Highlight.Deactivated;
                if (Object is IDeactivatable && ((IDeactivatable)Object).IsDeactivated) return Highlight.Deactivated;
                return Highlight.None;
            }
        }

        public override Highlight HighlightAsync
        {
            get
            {
                // no async version required: nothing dangerous happens there.
                return Highlight;
            }
        }


        public override ControlKind RequestedKind
        {
            get
            {
                if (Object.CurrentAccessRights.HasNoRights()) return Zetbox.NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_AccessDeniedDataObjectKind.Find(FrozenContext);
                return base.RequestedKind ?? _object.GetObjectClass(FrozenContext).RequestedKind;
            }
            set
            {
                base.RequestedKind = value;
            }
        }

        public virtual string AccessDeniedText
        {
            get
            {
                return DataObjectViewModelResources.AccessDeniedText;
            }
        }

        #endregion

        #region PropertyChanged event handlers

        private void Object_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // notify consumers if ID has changed
            switch (e.PropertyName)
            {
                case "ID":
                    OnPropertyChanged("ID");
                    break;
                case "IsDeactivated":
                    OnHighlightChanged();
                    break;
            }

            UpdateToStringCache();
            OnPropertyChanged("ObjectState");

            OnObjectPropertyChanged(e.PropertyName);
        }

        protected virtual void OnObjectPropertyChanged(string propName)
        {
        }
        #endregion

        #region Validation
        string IDataErrorInfo.Error
        {
            get
            {
                if (ValidationError != null)
                {
                    return ValidationError.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                return ((IDataErrorInfo)this).Error;
            }
        }

        protected override bool NeedsValidation
        {
            get
            {
                if (!base.NeedsValidation) return false;

                // No access rights, no validation
                if (Object.CurrentAccessRights.HasNoRights()) return false;

                // If any property is invalid (due to a change that not has been written back), validate
                if (_propertyModels != null)
                {
                    if (_propertyModels.Any(p => !p.Value.IsValid)) return true;
                }

                // Deleted, not attacted or unmodified -> no validation
                if (Object.ObjectState.In(DataObjectState.Deleted, DataObjectState.Unmodified, DataObjectState.Detached, DataObjectState.NotDeserialized)) return false;

                return true;
            }
        }

        protected override void DoValidate()
        {
            base.DoValidate();
            if (!NeedsValidation) return;

            var objError = Object.Validate();
            if (!objError.IsValid)
            {
                ValidationError.AddErrors(objError.Errors);
            }

            if (_propertyModels != null)
            {
                // Validate only, if properties are loaded
                var errors = PropertyModels
                    .Select(i => i.Validate())
                    .ToArray()
                    .Where(i => i.HasErrors)
                    .ToArray();

                if (errors.Any())
                {
                    ValidationError.AddError(DataObjectViewModelResources.ErrorInvalidProperties);
                    ValidationError.Children.Clear();
                    ValidationError.AddChildren(errors);
                }
            }
        }

        #endregion
    }
}
