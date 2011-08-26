
namespace Kistl.Client.Presentables.ValueViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.App.GUI;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables.ValueViewModels;

    [ViewModelDescriptor]
    public class ObjectReferenceViewModel
        : ValueViewModel<DataObjectViewModel, IDataObject>
    {
        public new delegate ObjectReferenceViewModel Factory(IKistlContext dataCtx, ViewModel parent, IValueModel mdl);

        public ObjectReferenceViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent,
            IValueModel mdl)
            : base(appCtx, dataCtx, parent, mdl)
        {
            ObjectReferenceModel = (IObjectReferenceValueModel)mdl;
            var relEnd = ObjectReferenceModel.RelEnd;

            if (relEnd == null && mdl.IsReadOnly)
            {
                // could be e.g. a calculated object ref property
                _allowClear = false;
                _allowCreateNewItem = false;
                _allowDelete = false;
                _allowSelectValue = false;
            }
            else if (relEnd == null && !mdl.IsReadOnly)
            {
                // could be e.g. a filter
                _allowClear = true;
                _allowCreateNewItem = false;
                _allowDelete = false;
                _allowSelectValue = true;
            }
            else
            {
                var rel = relEnd.Parent;
                if (rel != null)
                {
                    var relType = rel.GetRelationType();
                    if (relType == RelationType.one_n && rel.Containment == ContainmentSpecification.Independent)
                    {
                        _allowCreateNewItem = false; // search first
                    }
                    else if (relType == RelationType.one_one)
                    {
                        if ((rel.Containment == ContainmentSpecification.AContainsB && rel.A == relEnd) ||
                           (rel.Containment == ContainmentSpecification.BContainsA && rel.B == relEnd))
                        {
                            _allowSelectValue = false; // This end is creating the value, don't change another item
                        }
                        else
                        {
                            _allowCreateNewItem = false; // possibility to change parent, but do not create a new one
                        }
                    }
                }
            }
        }

        #region Public Interface

        public IObjectReferenceValueModel ObjectReferenceModel { get; private set; }
        public ObjectClass ReferencedClass { get { return ObjectReferenceModel.ReferencedClass; } }

        private bool _allowCreateNewItem = true;
        public bool AllowCreateNewItem
        {
            get
            {
                return _allowCreateNewItem;
            }
            set
            {
                if (_allowCreateNewItem != value)
                {
                    _allowCreateNewItem = value;
                    OnPropertyChanged("AllowCreateNewItem");
                }
            }
        }

        private bool _allowSelectValue = true;
        public bool AllowSelectValue
        {
            get
            {
                return _allowSelectValue;
            }
            set
            {
                if (_allowSelectValue != value)
                {
                    _allowSelectValue = value;
                    OnPropertyChanged("AllowSelectValue");
                }
            }
        }

        private bool _allowClear = true;
        public bool AllowClear
        {
            get
            {
                return _allowClear;
            }
            set
            {
                if (_allowClear != value)
                {
                    _allowClear = value;
                    OnPropertyChanged("Clear");
                }
            }
        }

        // Not supported by any command yet
        private bool _allowDelete = false;
        public bool AllowDelete
        {
            get
            {
                return _allowDelete;
            }
            set
            {
                if (_allowDelete != value)
                {
                    _allowDelete = value;
                    OnPropertyChanged("AllowDelete");
                }
            }
        }

        public override string Name
        {
            get { return Value == null ? "(null)" : "Reference to " + Value.Name; }
        }
        #endregion

        #region Utilities and UI callbacks

        private void CollectChildClasses(int id, List<ObjectClass> children)
        {
            var nextChildren = FrozenContext
                .GetQuery<ObjectClass>()
                .Where(oc => oc.BaseObjectClass != null && oc.BaseObjectClass.ID == id)
                .ToList();

            if (nextChildren.Count() > 0)
            {
                foreach (ObjectClass oc in nextChildren)
                {
                    if (!oc.IsAbstract) children.Add(oc);
                    CollectChildClasses(oc.ID, children);
                };
            }
        }

        #endregion

        #region Commands

        protected override ObservableCollection<ICommandViewModel> CreateCommands()
        {
            var cmds = base.CreateCommands();

            cmds.Add(SelectValueCommand);
            cmds.Add(CreateNewItemAndSetValueCommand);
            cmds.Add(OpenReferenceCommand);
            cmds.Add(ClearValueCommand);

            return cmds;
        }

        #region OpenReference

        private bool CanOpen
        {
            get
            {
                return Value != null ? ViewModelFactory.CanShowModel(Value) : false;
            }
        }

        public void OpenReference()
        {
            if (CanOpen)
                ViewModelFactory.ShowModel(Value, true);
        }

        private ICommandViewModel _openReferenceCommand;
        public ICommandViewModel OpenReferenceCommand
        {
            get
            {
                if (_openReferenceCommand == null)
                {
                    _openReferenceCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        ObjectReferenceViewModelResources.OpenReferenceCommand_Name,
                        ObjectReferenceViewModelResources.OpenReferenceCommand_Tooltip,
                        () => OpenReference(),
                        () => CanOpen, 
                        null);
                    _openReferenceCommand.Icon = FrozenContext.FindPersistenceObject<Icon>(NamedObjects.Icon_fileopen_png);
                }
                return _openReferenceCommand;
            }
        }
        #endregion

        #region CreateNewItemAndSetValue

        /// <summary>
        /// creates a new target and references it
        /// </summary>
        public void CreateNewItemAndSetValue()
        {
            ObjectClass baseclass = ObjectReferenceModel.ReferencedClass;

            var children = new List<ObjectClass>();
            if (baseclass.IsAbstract == false)
            {
                children.Add(baseclass);
            }
            CollectChildClasses(baseclass.ID, children);

            if (children.Count == 1)
            {
                var targetType = baseclass.GetDescribedInterfaceType();
                var item = this.DataContext.Create(targetType);
                var model = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), item);

                Value = model;
                ViewModelFactory.ShowModel(model, true);
            }
            else
            {
                var lstMdl = ViewModelFactory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(
                        DataContext, this,
                        typeof(ObjectClass).GetObjectClass(FrozenContext),
                        () => children.AsQueryable(),
                        new Action<DataObjectViewModel>(delegate(DataObjectViewModel chosen)
                        {
                            if (chosen != null)
                            {
                                var targetType = ((ObjectClass)chosen.Object).GetDescribedInterfaceType();
                                var item = this.DataContext.Create(targetType);
                                var model = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), item);

                                Value = model;
                                ViewModelFactory.ShowModel(model, true);
                            }
                        }), null);
                lstMdl.ListViewModel.ShowCommands = false;

                ViewModelFactory.ShowModel(lstMdl, true);
            }
        }

        private ICommandViewModel _createNewItemAndSetValueCommand;
        public ICommandViewModel CreateNewItemAndSetValueCommand
        {
            get
            {
                if (_createNewItemAndSetValueCommand == null)
                {
                    _createNewItemAndSetValueCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        ObjectReferenceViewModelResources.CreateNewItemAndSetValueCommand_Name,
                        ObjectReferenceViewModelResources.CreateNewItemAndSetValueCommand_Tooltip,
                        CreateNewItemAndSetValue,
                        () => AllowCreateNewItem && !DataContext.IsReadonly && !IsReadOnly, 
                        null);
                    _createNewItemAndSetValueCommand.Icon = FrozenContext.FindPersistenceObject<Icon>(NamedObjects.Icon_new_png);
                }
                return _createNewItemAndSetValueCommand;
            }
        }
        #endregion

        #region SelectValue

        public void SelectValue()
        {
            var ifType = ReferencedClass.GetDescribedInterfaceType();
            var selectionTask = ViewModelFactory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(
                DataContext,
                this,
                ifType.GetObjectClass(FrozenContext),
                null,
                new Action<DataObjectViewModel>(delegate(DataObjectViewModel chosen)
                {
                    if (chosen != null)
                    {
                        Value = chosen;
                    }
                }),
                null);
            selectionTask.ListViewModel.AllowDelete = false;
            selectionTask.ListViewModel.ShowOpenCommand = false;
            selectionTask.ListViewModel.AllowAddNew = true;

            ViewModelFactory.ShowModel(selectionTask, true);
        }

        private ICommandViewModel _SelectValueCommand;

        public ICommandViewModel SelectValueCommand
        {
            get
            {
                if (_SelectValueCommand == null)
                {
                    _SelectValueCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        ObjectReferenceViewModelResources.SelectValueCommand_Name,
                        ObjectReferenceViewModelResources.SelectValueCommand_Tooltip,
                        () => SelectValue(),
                        () => AllowSelectValue && !DataContext.IsReadonly && !IsReadOnly, 
                        null);
                    _SelectValueCommand.Icon = FrozenContext.FindPersistenceObject<Icon>(NamedObjects.Icon_search_png);
                }
                return _SelectValueCommand;
            }
        }
        #endregion
        #endregion

        #region Value
        protected override ParseResult<DataObjectViewModel> ParseValue(string str)
        {
            throw new NotImplementedException();
        }

        protected override void OnValueModelPropertyChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                ClearValueCache();
            }
            base.OnValueModelPropertyChanged(e);
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            if (propertyName == "Value")
            {
                ClearValueCache();
            }
            base.OnPropertyChanged(propertyName);
        }

        protected override DataObjectViewModel GetValueFromModel()
        {
            if (!_valueCacheInititalized)
            {
                UpdateValueCache();
            }
            return _valueCache;
        }

        protected override void SetValueToModel(DataObjectViewModel value)
        {
            ValueModel.Value = value != null ? value.Object : null;
            EnsureValuePossible(value);
            NotifyValueChanged();
        }

        private void EnsureValuePossible(DataObjectViewModel value)
        {
            if (_possibleValues != null)
            {
                // Add if not found
                if (!_possibleValues.Contains(value))
                {
                    _possibleValues.Add(value);
                }
            }
        }

        private bool _valueCacheInititalized = false;
        private DataObjectViewModel _valueCache;

        private void ClearValueCache()
        {
            _valueCache = null;
            _valueCacheInititalized = false;
        }

        private void UpdateValueCache()
        {
            var obj = ValueModel.Value;
            if (obj != null)
            {
                _valueCache = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), ValueModel.Value);
                EnsureValuePossible(_valueCache);
            }
            _valueCacheInititalized = true;
        }
        #endregion

        #region DropDown support

        private int _possibleValuesLimit = 50;
        public int PossibleValuesLimit
        {
            get
            {
                return _possibleValuesLimit;
            }
            set
            {
                if (_possibleValuesLimit != value)
                {
                    _possibleValuesLimit = value;
                    ResetPossibleValues();
                    OnPropertyChanged("PossibleValuesLimit");
                }
            }
        }

        private ReadOnlyObservableCollection<ViewModel> _possibleValuesRO;
        private ObservableCollection<ViewModel> _possibleValues;
        public ReadOnlyObservableCollection<ViewModel> PossibleValues
        {
            get
            {
                if (_possibleValues == null)
                {
                    bool needMoreButton;
                    var mdlList = GetPossibleValues(out needMoreButton);
                    if (needMoreButton)
                    {
                        var cmdMdl = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                            DataContext,
                            this,
                            ObjectReferenceViewModelResources.PossibleValues_More,
                            ObjectReferenceViewModelResources.PossibleValues_More_Tooltip,
                            SelectValue,
                            null, 
                            null);
                        cmdMdl.RequestedKind = FrozenContext.FindPersistenceObject<ControlKind>(NamedObjects.ControlKind_Kistl_App_GUI_CommandLinkKind);
                        mdlList.Add(cmdMdl);
                    }
                    _possibleValues = new ObservableCollection<ViewModel>(mdlList);
                    _possibleValuesRO = new ReadOnlyObservableCollection<ViewModel>(_possibleValues);
                }
                return _possibleValuesRO;
            }
        }

        private IQueryable<IDataObject> GetUntypedQueryHack<T>()
            where T : class, IDataObject
        {
            return DataContext.GetQuery<T>().Cast<IDataObject>();
        }

        public IQueryable<IDataObject> GetUntypedQuery(ObjectClass cls)
        {
            var mi = this.GetType().FindGenericMethod(true, "GetUntypedQueryHack", new[] { cls.GetDescribedInterfaceType().Type }, new Type[0]);
            return (IQueryable<IDataObject>)mi.Invoke(this, new object[0]);
        }

        protected virtual List<ViewModel> GetPossibleValues(out bool needMoreButton)
        {
            var qry = GetUntypedQuery(ReferencedClass);
            qry = ApplyFilter(qry);
            // Abort query of null was returned
            if (qry == null)
            {
                needMoreButton = false;
                return new List<ViewModel>();
            }

            var lst = qry.Take(PossibleValuesLimit + 1).ToList();

            var mdlList = lst
                        .Take(PossibleValuesLimit)
                        .Select(i => DataObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), i))
                        .Cast<ViewModel>()
                        .OrderBy(v => v.Name)
                        .ToList();

            needMoreButton = lst.Count > PossibleValuesLimit;

            // Add current value if not already present
            if (Value != null && !mdlList.Contains(Value))
            {
                mdlList.Add(Value);
            }

            return mdlList;
        }

        /// <summary>
        /// Override this method to apply a filter on the possible value query.
        /// </summary>
        /// <remarks>
        /// <para>If null is returned the query will be aborted and an empty list will be shown.</para>
        /// <para>To Apply no filter simply return the given query.</para>
        /// <para>The base implementation returns the query. So no need to call base.</para>
        /// </remarks>
        /// <param name="qry">Query to filter</param>
        /// <returns>filtered query or null</returns>
        protected virtual IQueryable<IDataObject> ApplyFilter(IQueryable<IDataObject> qry)
        {
            return qry;
        }

        protected void ResetPossibleValues()
        {
            _possibleValues = null;
            _possibleValuesRO = null;
            OnPropertyChanged("PossibleValues");
        }

        private GridDisplayConfiguration _displayedColumns = null;
        public GridDisplayConfiguration DisplayedColumns
        {
            get
            {
                if (_displayedColumns == null)
                {
                    _displayedColumns = CreateDisplayedColumns();
                }
                return _displayedColumns;
            }
        }
        protected virtual GridDisplayConfiguration CreateDisplayedColumns()
        {
            var result = new GridDisplayConfiguration();
            result.BuildColumns(ReferencedClass, GridDisplayConfiguration.Mode.ReadOnly, false);
            return result;
        }
        #endregion

        #region Highlight
        public override Highlight Highlight
        {
            get
            {
                return (Value != null ? Value.Highlight : null) ?? base.Highlight;
            }
        }
        #endregion
    }
}
