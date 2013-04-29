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
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Async;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.ZetboxBase;

    public abstract class BaseCompoundCollectionViewModel<TModelCollection>
        : ValueViewModel<IReadOnlyObservableList<CompoundObjectViewModel>, TModelCollection>
        where TModelCollection : ICollection<ICompoundObject>
    {
        public new delegate BaseCompoundCollectionViewModel<TModelCollection> Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public BaseCompoundCollectionViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            ICompoundCollectionValueModel<TModelCollection> mdl)
            : base(appCtx, dataCtx, parent, mdl)
        {
            this.ObjectCollectionModel = mdl;
            dataCtx.IsElevatedModeChanged += new EventHandler(dataCtx_IsElevatedModeChanged);
        }

        public ICompoundCollectionValueModel<TModelCollection> ObjectCollectionModel { get; private set; }
        public CompoundObject ReferencedClass { get { return ObjectCollectionModel.CompoundObjectDefinition; } }

        void dataCtx_IsElevatedModeChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("AllowAddNew");
            OnPropertyChanged("AllowDelete");
            CreateCommands();
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
            GridDisplayConfiguration.Mode mode = IsInlineEditable ?
                  GridDisplayConfiguration.Mode.Editable
                : GridDisplayConfiguration.Mode.ReadOnly;

            result.BuildColumns(ReferencedClass, mode, true);
            return result;
        }

        private ObservableCollection<CompoundObjectViewModel> _selectedItems = null;
        public ObservableCollection<CompoundObjectViewModel> SelectedItems
        {
            get
            {
                if (_selectedItems == null)
                {
                    _selectedItems = new ObservableCollection<CompoundObjectViewModel>();
                    _selectedItems.CollectionChanged += _selectedItems_CollectionChanged;
                }
                return _selectedItems;
            }
        }

        void _selectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("SelectedItem");
        }

        /// <summary>
        /// The current first selected item
        /// </summary>
        /// <remarks>
        /// If set explicit, all other selected items will be cleard from the SelectesItems List
        /// </remarks>
        public CompoundObjectViewModel SelectedItem
        {
            get
            {
                if (SelectedItems.Count > 0)
                {
                    return _selectedItems[0];
                }
                return null;
            }
            set
            {
                this.SelectedItems.Clear();
                if (value != null) this.SelectedItems.Add(value);
                OnPropertyChanged("SelectedItem");
            }
        }

        #region Allow*

        public bool IsInlineEditable
        {
            get
            {
                return true;
            }
        }

        private bool _allowAddNew = true;
        public bool AllowAddNew
        {
            get
            {
                if (DataContext.IsElevatedMode) return true;
                return _allowAddNew;
            }
            set
            {
                if (_allowAddNew != value)
                {
                    _allowAddNew = value;
                    OnPropertyChanged("AllowAddNew");
                }
            }
        }

        private bool _allowAddNewWhenAddingExisting = true;
        public bool AllowAddNewWhenAddingExisting
        {
            get
            {
                return _allowAddNewWhenAddingExisting;
            }
            set
            {
                if (_allowAddNewWhenAddingExisting != value)
                {
                    _allowAddNew = value;
                    OnPropertyChanged("AllowAddNewWhenAddingExisting");
                }
            }
        }

        private bool? _allowInlineAddNew = null;
        public bool AllowInlineAddNew
        {
            get
            {
                return AllowAddNew && IsInlineEditable && !HasChildClasses && _allowInlineAddNew != false;
            }
            set
            {
                if (_allowInlineAddNew != value)
                {
                    _allowInlineAddNew = value;
                    OnPropertyChanged("AllowInlineAddNew");
                }
            }
        }

        private bool _allowAddExisting = true;
        public bool AllowAddExisting
        {
            get
            {
                return _allowAddExisting;
            }
            set
            {
                if (_allowAddExisting != value)
                {
                    _allowAddExisting = value;
                    OnPropertyChanged("AllowAddExisting");
                }
            }
        }

        private bool _allowOpen = true;
        public bool AllowOpen
        {
            get
            {
                if (DataContext.IsElevatedMode) return true;
                return _allowOpen;
            }
            set
            {
                if (_allowOpen != value)
                {
                    _allowOpen = value;
                    OnPropertyChanged("AllowOpen");
                }
            }
        }

        private bool _allowRemove = true;
        public bool AllowRemove
        {
            get
            {
                return _allowRemove;
            }
            set
            {
                if (_allowRemove != value)
                {
                    _allowRemove = value;
                    OnPropertyChanged("AllowRemove");
                }
            }
        }

        private bool _allowDelete = true;
        public bool AllowDelete
        {
            get
            {
                if (DataContext.IsElevatedMode) return true;
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

        // Case 2282: Sorting Lists will delete 1:n Entries
        // This is evil, as it would delete an Object during 
        // resort of an ObjectRefList.
        // so inline deleting is forbidden.
        private bool _allowInlineDelete = false;
        public bool AllowInlineDelete
        {
            get
            {
                return _allowInlineDelete;
            }
            set
            {
                if (_allowInlineDelete != value)
                {
                    if (value == true)
                    {
                        throw new NotSupportedException("Case 2282: Inline deleting is forbidden as it has some issues with resorting.");
                    }
                    _allowInlineDelete = value;
                    OnPropertyChanged("AllowInlineDelete");
                }
            }
        }
        #endregion

        #region Commands

        
        protected override ObservableCollection<ICommandViewModel> CreateCommands()
        {
            var cmds = base.CreateCommands();

            if (AllowAddNew) cmds.Add(CreateNewCommand);
            if (AllowOpen) cmds.Add(OpenCommand);
            if (AllowDelete) cmds.Add(DeleteCommand);

            return cmds;
        }

        private ICommandViewModel _CreateNewCommand = null;
        public ICommandViewModel CreateNewCommand
        {
            get
            {
                EnsureNewCommand();
                return _CreateNewCommand;
            }
        }

        private void EnsureNewCommand()
        {
            if (_CreateNewCommand == null)
            {
                _CreateNewCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                    DataContext,
                    this,
                    CommonCommandsResources.NewDataObjectCommand_Name,
                    CommonCommandsResources.NewDataObjectCommand_Tooltip,
                    CreateNew,
                    CanCreateNew, 
                    () => string.Empty);
            }
        }

        public bool CanCreateNew()
        {
            return AllowAddNew;
        }

        public void CreateNew()
        {
            if (!CanCreateNew()) return;

            var obj = DataContext.CreateCompoundObject(ReferencedClass.GetDescribedInterfaceType());
            OnObjectCreated(obj);
            var vmdl = CompoundObjectViewModel.Fetch(ViewModelFactory, DataContext, this, obj);
            Add(vmdl);
        }

        public delegate void ObjectCreatedHandler(ICompoundObject obj);
        public event ObjectCreatedHandler ObjectCreated;

        private void OnObjectCreated(ICompoundObject obj)
        {
            ObjectCreatedHandler temp = ObjectCreated;
            if (temp != null)
            {
                temp(obj);
            }
        }

        private ICommandViewModel _OpenCommand;
        public ICommandViewModel OpenCommand
        {
            get
            {
                EnsureOpenCommand();
                return _OpenCommand;
            }
        }

        private void EnsureOpenCommand()
        {
            if (_OpenCommand == null)
            {
                _OpenCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                    DataContext, 
                    this,
                    CommonCommandsResources.OpenDataObjectCommand_Name,
                    CommonCommandsResources.OpenDataObjectCommand_Tooltip,
                    Open,
                    CanOpen,
                    () => CommonCommandsResources.DataObjectCommand_NothingSelected);
            }
        }

        public bool CanOpen()
        {
            return SelectedItems.Count > 0;
        }

        public void Open()
        {
            if (!CanOpen()) return;

            foreach (var cpObj in SelectedItems)
            {
            }
        }

        private ICommandViewModel _deleteCommand = null;
        public ICommandViewModel DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        CommonCommandsResources.DeleteDataObjectCommand_Name,
                        CommonCommandsResources.DeleteDataObjectCommand_Tooltip,
                        Delete, // Collection will change while deleting!
                        () => SelectedItems != null && SelectedItems.Count() > 0 && AllowRemove && !IsReadOnly,
                        null);
                }
                return _deleteCommand;
            }
        }

        public virtual void Add(CompoundObjectViewModel item)
        {
            if (item == null) { throw new ArgumentNullException("item"); }

            EnsureValueCache();
            ValueModel.Value.Add(item.Object);

            SelectedItem = item;
        }

        public virtual void Delete()
        {
            if (SelectedItems == null || SelectedItems.Count == 0) return;

            EnsureValueCache();
            foreach (var item in SelectedItems.ToArray())
            {
                ValueModel.Value.Remove(item.Object);
            }
        }

        public virtual void Remove()
        {
            // Redirect -> it's the same
            Delete();
        }

        #endregion

        #region GotFromDerivedClasses

        protected void EnsureValueCache()
        {
            GetValueFromModel().Wait();
        }

        ReadOnlyObservableProjectedList<ICompoundObject, CompoundObjectViewModel> _valueCache;
        SortedWrapper<ICompoundObject> _wrapper;
        private ZbTask<IReadOnlyObservableList<CompoundObjectViewModel>> _fetchValueTask;
        protected override ZbTask<IReadOnlyObservableList<CompoundObjectViewModel>> GetValueFromModel()
        {
            if (_fetchValueTask == null)
            {
                SetBusy();
                _fetchValueTask = new ZbTask<IReadOnlyObservableList<CompoundObjectViewModel>>(ObjectCollectionModel.GetValueAsync())
                    .OnResult(t =>
                    {
                        _wrapper = new SortedWrapper<ICompoundObject>(ObjectCollectionModel.Value, ReferencedClass.GetDescribedInterfaceType(), ObjectCollectionModel, InitialSortProperty);
                        _valueCache = new ReadOnlyObservableProjectedList<ICompoundObject, CompoundObjectViewModel>(
                            _wrapper,
                            obj => CompoundObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), obj),
                            mdl => mdl.Object);
                        _valueCache.CollectionChanged += ValueListChanged;
                        t.Result = _valueCache;
                    });
                // TODO: Split here to avoid a stackoverflow exception!
                // -> OnPropertyChanged("ValueProxiesAsync") triggers ValueProxiesAsync.get
                // -> ValueProxiesAsync.get is calling GetValueFromModel()
                // -> _fetchValueTask has not been assigned yet!
                _fetchValueTask.OnResult(t =>
                {
                    OnPropertyChanged("Value");
                    OnPropertyChanged("ValueAsync");
                    OnPropertyChanged("ValueProxiesAsync");
                    ClearBusy();
                });
            };
            return _fetchValueTask;
        }

        //private IDelayedTask _valueLoader;
        public override IReadOnlyObservableList<CompoundObjectViewModel> Value
        {
            get
            {
                GetValueFromModel().Wait();
                return _valueCache;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override IReadOnlyObservableList<CompoundObjectViewModel> ValueAsync
        {
            get
            {
                GetValueFromModel();
                return _valueCache;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        protected override void SetValueToModel(IReadOnlyObservableList<CompoundObjectViewModel> value)
        {
            throw new NotSupportedException();
        }

        protected abstract string InitialSortProperty { get; }

        #endregion

        #region Utilities and UI callbacks

        private bool? _hasChildClasses;
        public bool HasChildClasses
        {
            get
            {
                if (_hasChildClasses == null)
                {
                    _hasChildClasses = FrozenContext.GetQuery<ObjectClass>()
                        .Any(oc => oc.BaseObjectClass == ReferencedClass);
                }
                return _hasChildClasses.Value;
            }
        }
        #endregion

        #region Event handlers

        protected void ValueListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var obj in e.NewItems.OfType<INotifyPropertyChanged>())
                {
                    obj.PropertyChanged += AnyPropertyChangedHandler;
                }
            }

            if (e.OldItems != null)
            {
                foreach (var obj in e.OldItems.OfType<INotifyPropertyChanged>())
                {
                    obj.PropertyChanged -= AnyPropertyChangedHandler;
                }
            }

            NotifyValueChanged();
        }

        private void AnyPropertyChangedHandler(object sender, EventArgs e)
        {
            OnPropertyChanged("ValueAsync");
            OnPropertyChanged("Value");
        }

        #endregion

        #region IValueViewModel Members

        public override void ClearValue()
        {
            EnsureValueCache();
            ValueModel.Value.Clear();
        }
        #endregion

        #region Proxy
        private CompoundObjectViewModel GetObjectFromProxy(CompoundObjectViewModelProxy p)
        {
            if (p.Object == null)
            {
                var obj = DataContext.CreateCompoundObject(DataContext.GetInterfaceType(this.ReferencedClass.GetDataType()));
                p.Object = CompoundObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), obj);
                _proxyCache[p.Object.Object] = p;
            }
            return p.Object;
        }

        Dictionary<ICompoundObject, CompoundObjectViewModelProxy> _proxyCache = new Dictionary<ICompoundObject, CompoundObjectViewModelProxy>();
        private CompoundObjectViewModelProxy GetProxy(ICompoundObject obj)
        {
            CompoundObjectViewModelProxy result;
            if (!_proxyCache.TryGetValue(obj, out result))
            {
                result = new CompoundObjectViewModelProxy() { Object = CompoundObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), obj) };
                _proxyCache[obj] = result;
            }
            return result;
        }

        //private IDelayedTask _proxyLoader;
        //private ZbTask _proxyLoader;
        private CompoundListViewModelProxyList _proxyInstances = null;
        private bool _proxyInstancesLoading = false;
        /// <summary>
        /// Allow instances to be added external
        /// </summary>
        public CompoundListViewModelProxyList ValueProxiesAsync
        {
            get
            {
                if (!_proxyInstancesLoading)
                {
                    _proxyInstancesLoading = true;
                    GetValueFromModel()
                        .OnResult(t =>
                        {
                            _proxyInstances = new CompoundListViewModelProxyList(
                                ObjectCollectionModel,
                                ObjectCollectionModel.Value,
                                (vm) => GetProxy(vm),
                                (p) => GetObjectFromProxy(p).Object);
                            OnPropertyChanged("ValueProxiesAsync");
                        });
                }
                return _proxyInstances;
            }
        }

        private ObservableProjectedList<CompoundObjectViewModel, CompoundObjectViewModelProxy> _selectedProxies = null;
        public ObservableProjectedList<CompoundObjectViewModel, CompoundObjectViewModelProxy> SelectedProxies
        {
            get
            {
                if (_selectedProxies == null)
                {
                    _selectedProxies = new ObservableProjectedList<CompoundObjectViewModel, CompoundObjectViewModelProxy>(
                        SelectedItems,
                        (vm) => GetProxy(vm.Object),
                        (p) => GetObjectFromProxy(p));
                }
                return _selectedProxies;
            }
        }
        #endregion

        #region Sorting
        private string _sortProperty = null;
        private System.ComponentModel.ListSortDirection _sortDirection = System.ComponentModel.ListSortDirection.Ascending;

        public void Sort(string propName, System.ComponentModel.ListSortDirection direction)
        {
            _sortProperty = propName;
            _sortDirection = direction;
            OnPropertyChanged("SortProperty");
            OnPropertyChanged("SortDirection");

            EnsureValueCache();
            _wrapper.Sort(propName, direction, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            OnPropertyChanged("ValueProxiesAsync");
        }

        public string SortProperty { get { return _sortProperty; } }
        public System.ComponentModel.ListSortDirection SortDirection { get { return _sortDirection; } }
        #endregion

        #region IFormattedValueViewModel Members
        protected override string FormatValue(IReadOnlyObservableList<CompoundObjectViewModel> value)
        {
            if (value.Count == 0) return BaseObjectCollectionViewModelResources.NoItems;
            if (value.Count == 1) return string.Format(BaseObjectCollectionViewModelResources.OneItem, value[0].Name);
            return string.Format(BaseObjectCollectionViewModelResources.MoreItems, value.Count, value[0].Name);
        }
        protected override ParseResult<IReadOnlyObservableList<CompoundObjectViewModel>> ParseValue(string str)
        {
            throw new NotSupportedException();
        }
        #endregion
    
    }

    public class CompoundObjectViewModelProxy
    {
        public CompoundObjectViewModelProxy()
        {
        }

        public CompoundObjectViewModel Object { get; set; }
        public System.Drawing.Image Icon
        {
            get { return Object != null ? Object.Icon : null; }
        }

        public Highlight Highlight
        {
            get
            {
                return Object != null ? Object.Highlight : Highlight.None;
            }
        }
    }

    /// <summary>
    /// Hack for those who do not check element types by traversing from inherited interfaces
    /// e.g. DataGrid from WPF
    /// Can't be a inner class, because it's deriving the generic parameter from BaseObjectCollectionViewModel. This confuses the WPF DataGrid
    /// </summary>
    public sealed class CompoundListViewModelProxyList : AbstractObservableProjectedList<ICompoundObject, CompoundObjectViewModelProxy>, IList, IList<CompoundObjectViewModelProxy>
    {
        public CompoundListViewModelProxyList(INotifyCollectionChanged notifier, object collection, Func<ICompoundObject, CompoundObjectViewModelProxy> select, Func<CompoundObjectViewModelProxy, ICompoundObject> inverter)
            : base(notifier, collection, select, inverter, false)
        {
        }
    }
}
