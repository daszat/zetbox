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

namespace Zetbox.Client.Presentables.ValueViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Linq.Expressions;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Async;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.ZetboxBase;

    [ViewModelDescriptor]
    public class ObjectReferenceViewModel
        : ValueViewModel<DataObjectViewModel, IDataObject>, INewCommandParameter, IOpenCommandParameter
    {
        public new delegate ObjectReferenceViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public ObjectReferenceViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            IValueModel mdl)
            : base(appCtx, dataCtx, parent, mdl)
        {
            ObjectReferenceModel = (IObjectReferenceValueModel)mdl;
            var relEnd = ObjectReferenceModel.RelEnd;

            if (relEnd == null && mdl.IsReadOnly)
            {
                // could be e.g. a calculated object ref property
                _allowSelectValue = false;
                _allowCreateNewItem = false;
                _allowCreateNewItemOnSelect = false;
                _allowClear = false;
            }
            else if (relEnd == null && !mdl.IsReadOnly)
            {
                // could be e.g. a filter
                _allowSelectValue = true;
                _allowCreateNewItem = false;
                _allowCreateNewItemOnSelect = false;
                _allowClear = true;
            }
            else
            {
                var rel = relEnd.Parent;
                if (rel != null)
                {
                    var relType = rel.GetRelationType();
                    if (relType == RelationType.one_n && rel.Containment == ContainmentSpecification.Independent)
                    {
                        // search first
                        _allowSelectValue = true;
                        _allowCreateNewItem = false;
                        _allowCreateNewItemOnSelect = true;
                    }
                    else if (relType == RelationType.one_one)
                    {
                        if ((rel.Containment == ContainmentSpecification.AContainsB && rel.A == relEnd) ||
                           (rel.Containment == ContainmentSpecification.BContainsA && rel.B == relEnd))
                        {
                            // This end is creating the value, don't change another item
                            _allowSelectValue = false;
                            _allowCreateNewItem = true;
                        }
                        else
                        {
                            // possibility to change parent, but do not create a new one
                            // search first
                            _allowSelectValue = true;
                            _allowCreateNewItem = false;
                            _allowCreateNewItemOnSelect = true;
                        }
                    }
                }
            }
            dataCtx.IsElevatedModeChanged += new EventHandler(dataCtx_IsElevatedModeChanged);
        }

        void dataCtx_IsElevatedModeChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("AllowDelete");
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
                    OnPropertyChanged("AllowAddNew");
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
        private bool _allowCreateNewItemOnSelect = true;
        public bool AllowCreateNewItemOnSelect
        {
            get
            {
                return _allowCreateNewItemOnSelect;
            }
            set
            {
                if (_allowCreateNewItemOnSelect != value)
                {
                    _allowCreateNewItemOnSelect = value;
                    OnPropertyChanged("AllowCreateNewItemOnSelect");
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

        public override string Name
        {
            get { return Value == null ? "(null)" : "Reference to " + Value.Name; }
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

            if (ObjectReferenceModel.RelEnd != null && ObjectReferenceModel is BasePropertyValueModel)
            {
                var obj = (IDataObject)((BasePropertyValueModel)ObjectReferenceModel).Object;
                var navigator = ObjectReferenceModel.RelEnd.Navigator;
                ObjectReferenceHelper.AddActionViewModels(cmds, obj, navigator.Methods, this, ViewModelFactory);
            }

            return cmds;
        }

        #region OpenReference

        public void OpenReference()
        {
            if (OpenReferenceCommand.CanExecute(null))
                OpenReferenceCommand.Execute(null);
        }

        private OpenDataObjectCommand _openReferenceCommand;
        public ICommandViewModel OpenReferenceCommand
        {
            get
            {
                if (_openReferenceCommand == null)
                {
                    _openReferenceCommand = ViewModelFactory.CreateViewModel<OpenDataObjectCommand.Factory>().Invoke(
                        DataContext,
                        this);
                }
                return _openReferenceCommand;
            }
        }

        bool IOpenCommandParameter.AllowOpen { get { return true; } }
        IEnumerable<ViewModel> ICommandParameter.SelectedItems { get { return ValueAsync == null ? null : new[] { ValueAsync }; } }

        #endregion

        #region CreateNewItemAndSetValue

        private NewDataObjectCommand _createNewItemAndSetValueCommand;
        public ICommandViewModel CreateNewItemAndSetValueCommand
        {
            get
            {
                if (_createNewItemAndSetValueCommand == null)
                {
                    _createNewItemAndSetValueCommand = ViewModelFactory.CreateViewModel<NewDataObjectCommand.Factory>().Invoke(
                        DataContext,
                        this,
                        ReferencedClass);
                    _createNewItemAndSetValueCommand.LocalModelCreated += vm => Value = vm;
                }
                return _createNewItemAndSetValueCommand;
            }
        }

        public void CreateNewItemAndSetValue()
        {
            if (CreateNewItemAndSetValueCommand.CanExecute(null))
                CreateNewItemAndSetValueCommand.Execute(null);
        }

        bool INewCommandParameter.AllowAddNew { get { return AllowCreateNewItem; } }

        #endregion

        #region SelectValue

        public void SelectValue()
        {
            var selectionTask = CreateDataObjectSelectionTask();
            OnDataObjectSelectionTaskCreated(selectionTask);
            ViewModelFactory.ShowDialog(selectionTask);
        }

        /// <summary>
        /// Creates a data object selection task for the referenced class. The choose action should set this.Value with the first selected item.
        /// </summary>
        /// <returns></returns>
        protected virtual DataObjectSelectionTaskViewModel CreateDataObjectSelectionTask()
        {
            var selectionTask = ViewModelFactory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(
                DataContext,
                this,
                ReferencedClass,
                null,
                (chosen) =>
                {
                    if (chosen != null)
                    {
                        Value = chosen.First();
                    }
                },
                null);
            selectionTask.ListViewModel.AllowDelete = false;
            selectionTask.ListViewModel.AllowOpen = false;
            selectionTask.ListViewModel.AllowAddNew = AllowCreateNewItemOnSelect;
            return selectionTask;
        }

        public event DataObjectSelectionTaskCreatedEventHandler DataObjectSelectionTaskCreated;
        protected virtual void OnDataObjectSelectionTaskCreated(DataObjectSelectionTaskViewModel vmdl)
        {
            var temp = DataObjectSelectionTaskCreated;
            if (temp != null)
            {
                temp(this, new DataObjectSelectionTaskEventArgs(vmdl));
            }
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
                        () => AllowSelectValue && !IsReadOnly,
                        null);
                    _SelectValueCommand.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.search_png.Find(FrozenContext));
                }
                return _SelectValueCommand;
            }
        }
        #endregion
        #endregion

        #region Value
        protected override ParseResult<DataObjectViewModel> ParseValue(string str)
        {
            throw new NotSupportedException();
        }

        protected override void OnValueModelPropertyChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                ClearValueCache();
            }
            else if (e.PropertyName == "HighlightAsync")
            {
                OnHighlightChanged();
            }
            base.OnValueModelPropertyChanged(e);
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            switch (propertyName)
            {
                case "ValueAsync":
                    OnPropertyChanged("SelectedItems");
                    OnHighlightChanged();
                    break;
                case "Value":
                    ClearValueCache();
                    break;
                default:
                    break;
            }
            base.OnPropertyChanged(propertyName);
        }

        private ZbTask<DataObjectViewModel> _fetchValueTask;
        protected override ZbTask<DataObjectViewModel> GetValueFromModelAsync()
        {
            if (_fetchValueTask == null)
            {
                SetBusy();
                _fetchValueTask = new ZbTask<DataObjectViewModel>(ValueModel.GetValueAsync());
                _fetchValueTask.Finally(ClearBusy);
                // Avoid stackoverflow
                _fetchValueTask.OnResult(t =>
                {
                    if (!_valueCacheInititalized)
                    {
                        var obj = ValueModel.Value;
                        if (obj != null)
                        {
                            _valueCache = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), ValueModel.Value);
                            EnsureValuePossible(_valueCache);
                        }
                        _valueCacheInititalized = true;
                        OnPropertyChanged("ValueAsync");
                    }
                    t.Result = _valueCache;
                });
            }

            return _fetchValueTask;
        }

        protected override void SetValueToModel(DataObjectViewModel value)
        {
            ValueModel.Value = value != null ? value.Object : null;
            EnsureValuePossible(value);
            NotifyValueChanged();
        }

        private void EnsureValuePossible(DataObjectViewModel value)
        {
            if (_possibleValues != null && value != null)
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

            if (_fetchValueTask != null)
            {
                ClearBusy(); // TODO: Workaround! Cancel should call Finally?
                _fetchValueTask.Cancel();
            }
            _fetchValueTask = null;
        }

        public override DataObjectViewModel Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                if (_fetchValueTask != null)
                {
                    ClearBusy(); // TODO: Workaround! Cancel should call Finally?
                    _fetchValueTask.Cancel();
                }

                _fetchValueTask = null;

                base.Value = value;
            }
        }

        public override DataObjectViewModel ValueAsync
        {
            get
            {
                GetValueFromModelAsync();
                return _valueCache;
            }
            set
            {
                // reuse synchronous setter to await/cancel a potential running fetch task
                this.Value = value;
            }
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

        private ZbTask<ReadOnlyObservableCollection<ViewModel>> _getPossibleValuesROTask;
        private ReadOnlyObservableCollection<ViewModel> _possibleValuesRO;
        private ObservableCollection<ViewModel> _possibleValues;
        public ReadOnlyObservableCollection<ViewModel> PossibleValues
        {
            get
            {
                TriggerPossibleValuesROAsync();
                _getPossibleValuesROTask.Wait();
                return _possibleValuesRO;
            }
        }

        public ReadOnlyObservableCollection<ViewModel> PossibleValuesAsync
        {
            get
            {
                TriggerPossibleValuesROAsync();
                return _possibleValuesRO;
            }
        }

        private void TriggerPossibleValuesROAsync()
        {
            if (_getPossibleValuesROTask == null)
            {
                var task = GetPossibleValuesAsync();
                _getPossibleValuesROTask = new ZbTask<ReadOnlyObservableCollection<ViewModel>>(task);
                _getPossibleValuesROTask.OnResult(t =>
                {
                    if (task.Result.Item2)
                    {
                        var cmdMdl = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                            DataContext,
                            this,
                            ObjectReferenceViewModelResources.PossibleValues_More,
                            ObjectReferenceViewModelResources.PossibleValues_More_Tooltip,
                            SelectValue,
                            null,
                            null);
                        cmdMdl.RequestedKind = Zetbox.NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_CommandLinkKind.Find(FrozenContext);
                        task.Result.Item1.Add(cmdMdl);
                    }
                    _possibleValues = new ObservableCollection<ViewModel>(task.Result.Item1);
                    _possibleValuesRO = new ReadOnlyObservableCollection<ViewModel>(_possibleValues);
                    EnsureValuePossible(Value);
                    OnPropertyChanged("PossibleValuesAsync");
                });
            }
        }

        private IQueryable GetUntypedQueryHack<T>()
            where T : class, IDataObject
        {
            return DataContext.GetQuery<T>();
        }

        protected virtual IQueryable GetUntypedQuery(ObjectClass cls)
        {
            var mi = this.GetType().FindGenericMethod("GetUntypedQueryHack", new[] { cls.GetDescribedInterfaceType().Type }, new Type[0], isPrivate: true);
            return (IQueryable)mi.Invoke(this, new object[0]);
        }

        protected virtual ZbTask<Tuple<List<ViewModel>, bool>> GetPossibleValuesAsync()
        {
            // No selection allowed -> no inline search allowed
            if (!AllowSelectValue)
            {
                return new ZbTask<Tuple<List<ViewModel>, bool>>(new Tuple<List<ViewModel>, bool>(new List<ViewModel>(), false));
            }

            var qry = GetUntypedQuery(ReferencedClass);
            qry = ApplyFilter(qry);
            // Abort query of null was returned
            if (qry == null)
            {
                return new ZbTask<Tuple<List<ViewModel>, bool>>(new Tuple<List<ViewModel>, bool>(new List<ViewModel>(), false));
            }

            var fetchTask = qry.Take(PossibleValuesLimit + 1).ToListAsync();

            var lstTask = new ZbTask<Tuple<List<ViewModel>, bool>>(fetchTask)
                .OnResult(t =>
                {
                    var mdlList = fetchTask.Result
                                .OfType<IDataObject>()
                                .Take(PossibleValuesLimit)
                                .Select(i => DataObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), i))
                                .Cast<ViewModel>()
                                .OrderBy(v => v.Name)
                                .ToList();

                    t.Result = new Tuple<List<ViewModel>, bool>(mdlList, mdlList.Count > PossibleValuesLimit);

                    // Add current value if not already present
                    if (Value != null && !mdlList.Contains(Value))
                    {
                        mdlList.Add(Value);
                    }
                });

            return lstTask;
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
        protected virtual IQueryable ApplyFilter(IQueryable qry)
        {
            FetchFilterModels();
            if (_filterModels.Count == 0) return qry;

            if (string.IsNullOrEmpty(SearchString))
            {
                if (RespectRequiredFilter && _filterModels.Any(f => f.Required)) return null;
                return qry;
            }

            LambdaExpression tmp = null;

            foreach (FilterModel f in _filterModels)
            {
                var valMdl = f.FilterArguments.First().Value as ClassValueModel<string>;
                if (valMdl != null)
                {
                    valMdl.Value = SearchString;
                    var expr = f.GetExpression(qry);
                    if (tmp == null)
                        tmp = expr;
                    else
                        tmp = tmp.OrElse(expr);
                }
            }

            if (tmp == null) return qry;
            return qry.AddFilter(tmp);
        }

        private List<IFilterModel> _filterModels;
        private void FetchFilterModels()
        {
            if (_filterModels == null)
            {
                _filterModels = new List<IFilterModel>();
                // Resolve default property filter
                var t = ReferencedClass;
                while (t != null)
                {
                    // Add ObjectClass filter expressions
                    foreach (var cfc in t.FilterConfigurations)
                    {
                        _filterModels.Add(cfc.CreateFilterModel(DataContext));
                    }

                    // Add Property filter expressions
                    foreach (var prop in t.Properties.Where(p => p.FilterConfiguration != null))
                    {
                        _filterModels.Add(prop.FilterConfiguration.CreateFilterModel(DataContext));
                    }
                    t = t.BaseObjectClass;
                }
            }
        }

        private string _searchString;
        public string SearchString
        {
            get
            {
                return _searchString;
            }
            set
            {
                if (_searchString != value)
                {
                    _searchString = value;
                    OnPropertyChanged("SearchString");
                    OnErrorChanged(); // Maybe error state has changed
                }
            }
        }

        private bool _RespectRequiredFilter = true;
        /// <summary>
        /// If set to false, no filter is required. Default value is true.
        /// </summary>
        public bool RespectRequiredFilter
        {
            get
            {
                return _RespectRequiredFilter;
            }
            set
            {
                if (_RespectRequiredFilter != value)
                {
                    _RespectRequiredFilter = value;
                    OnPropertyChanged("RespectRequiredFilter");
                }
            }
        }

        public void ResetPossibleValues()
        {
            _possibleValues = null;
            _possibleValuesRO = null;
            _getPossibleValuesROTask = null;
            OnPropertyChanged("PossibleValues");
            OnPropertyChanged("PossibleValuesAsync");
        }

        public override string Error
        {
            get
            {
                if (!string.IsNullOrEmpty(SearchString) && Value == null)
                {
                    return ObjectReferenceViewModelResources.Error_SearchString_NoValue;
                }
                else
                {
                    return base.Error;
                }
            }
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

        private PropertyTask<Highlight> _HighlightTask;
        private PropertyTask<Highlight> EnsureHighlightTask()
        {
            if (_HighlightTask != null) return _HighlightTask;

            return _HighlightTask = new PropertyTask<Highlight>(
                notifier: () =>
                {
                    OnHighlightChanged();
                },
                createTask: () =>
                {
                    var result = new ZbTask<Highlight>(GetValueFromModelAsync());
                    // This must be done on the UI-Thread
                    // Accessing any property might trigger accesses to the zetbox context
                    result.OnResult(t => t.Result = Value != null && Value.Highlight != Highlight.None ? Value.Highlight : base.Highlight);
                    return result;
                },
                set: (Highlight value) =>
                {
                    throw new NotImplementedException();
                });
        }

        public override Highlight Highlight
        {
            get { return EnsureHighlightTask().Get(); }
        }

        public override Highlight HighlightAsync
        {
            get { return EnsureHighlightTask().GetAsync(); }
        }

        #endregion

        #region DragDrop
        public virtual bool CanDrop
        {
            get
            {
                return !IsReadOnly;
            }
        }

        public virtual bool OnDrop(object data)
        {
            if (data is IDataObject[])
            {
                var lst = (IDataObject[])data;
                if (lst.Length != 1)
                    return false;

                var obj = lst.Single();
                if (!ReferencedClass.GetDescribedInterfaceType().Type.IsAssignableFrom(obj.Context.GetInterfaceType(obj).Type))
                    return false;

                if (obj.Context != DataContext)
                {
                    if (obj.ObjectState == DataObjectState.New) return false;
                    obj = DataContext.Find(DataContext.GetInterfaceType(obj), obj.ID);
                }
                Value = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, this.GetWorkspace(), obj);
            }
            return false;
        }

        public virtual object DoDragDrop()
        {
            if (!IsNull && Value.ObjectState.In(DataObjectState.Unmodified, DataObjectState.Modified, DataObjectState.New))
            {
                return new IDataObject[] { Value.Object };
            }
            return null;
        }
        #endregion
    }
}
