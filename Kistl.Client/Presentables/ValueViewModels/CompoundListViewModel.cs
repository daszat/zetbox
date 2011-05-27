
namespace Kistl.Client.Presentables.ValueViewModels
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

    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Client.Models;
    using Kistl.App.GUI;

    public class CompoundListViewModel
        : ValueViewModel<IReadOnlyObservableList<CompoundObjectViewModel>, IList<ICompoundObject>>, IValueListViewModel<CompoundObjectViewModel, IReadOnlyObservableList<CompoundObjectViewModel>>
    {
        public new delegate CompoundListViewModel Factory(IKistlContext dataCtx, IValueModel mdl);

        public CompoundListViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            ICompoundCollectionValueModel mdl)
            : base(appCtx, dataCtx, mdl)
        {
            this.ValueModel = mdl;
        }

        public new ICompoundCollectionValueModel ValueModel { get; private set; }

        private ReadOnlyObservableProjectedList<ICompoundObject, CompoundObjectViewModel> _valueCache;
        private CompoundObjectViewModel _selectedItem;

        public void MoveItemUp(CompoundObjectViewModel item)
        {
            throw new NotSupportedException();
        }

        public void MoveItemDown(CompoundObjectViewModel item)
        {
            throw new NotSupportedException();
        }

        public bool HasPersistentOrder
        {
            // TODO: fix this lie
            get { return false; }
        }

        public void AddItem(CompoundObjectViewModel item)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(CompoundObjectViewModel item)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(CompoundObjectViewModel item)
        {
            throw new NotImplementedException();
        }

        public void ActivateItem(CompoundObjectViewModel item, bool activate)
        {
            throw new NotImplementedException();
        }

        public CompoundObjectViewModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (value != _selectedItem)
                {
                    _selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        public new IReadOnlyObservableList<CompoundObjectViewModel> Value
        {
            get
            {
                return GetValueFromModel();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        protected override IReadOnlyObservableList<CompoundObjectViewModel> GetValueFromModel()
        {
            if (_valueCache == null)
            {
                _valueCache = new ReadOnlyObservableProjectedList<ICompoundObject, CompoundObjectViewModel>(
                    ValueModel, ValueModel.Value,
                    obj => CompoundObjectViewModel.Fetch(ViewModelFactory, DataContext, obj),
                    mdl => mdl.Object);
                //_valueCache.CollectionChanged += ValueListChanged;
            }
            return _valueCache;
        }

        protected override void SetValueToModel(IReadOnlyObservableList<CompoundObjectViewModel> value)
        {
            throw new NotSupportedException();
        }

        protected override ValueViewModel<IReadOnlyObservableList<CompoundObjectViewModel>, IList<ICompoundObject>>.ParseResult<IReadOnlyObservableList<CompoundObjectViewModel>> ParseValue(string str)
        {
            throw new NotSupportedException();
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
            GridDisplayConfiguration.Mode mode = GridDisplayConfiguration.Mode.Editable;

            result.BuildColumns(ValueModel.CompoundObjectDefinition, mode);
            return result;
        }

        #region Proxy
        private CompoundObjectViewModel GetObjectFromProxy(CompoundObjectViewModelProxy p)
        {
            if (p.Object == null)
            {
                var obj = DataContext.CreateCompoundObject(DataContext.GetInterfaceType(this.ValueModel.CompoundObjectDefinition.GetDataType()));
                p.Object = CompoundObjectViewModel.Fetch(ViewModelFactory, DataContext, obj);
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
                result = new CompoundObjectViewModelProxy() { Object = CompoundObjectViewModel.Fetch(ViewModelFactory, DataContext, obj) };
                _proxyCache[obj] = result;
            }
            return result;
        }

        private CompoundListViewModelProxyList _proxyInstances = null;
        /// <summary>
        /// Allow instances to be added external
        /// </summary>
        public CompoundListViewModelProxyList ValueProxies
        {
            get
            {
                if (_proxyInstances == null)
                {
                    //EnsureValueCache();
                    _proxyInstances = new CompoundListViewModelProxyList(
                        this.ValueModel,
                        this.ValueModel.Value,
                        (vm) => GetProxy(vm),
                        (p) => GetObjectFromProxy(p).Object);
                }
                return _proxyInstances;
            }
        }

        //private ObservableProjectedList<DataObjectViewModel, DataObjectViewModelProxy> _selectedProxies = null;
        //public ObservableProjectedList<DataObjectViewModel, DataObjectViewModelProxy> SelectedProxies
        //{
        //    get
        //    {
        //        if (_selectedProxies == null)
        //        {
        //            _selectedProxies = new ObservableProjectedList<DataObjectViewModel, DataObjectViewModelProxy>(
        //                SelectedItems,
        //                (vm) => GetProxy(vm.Object),
        //                (p) => GetObjectFromProxy(p));
        //        }
        //        return _selectedProxies;
        //    }
        //}
        #endregion

    }

    public class CompoundObjectViewModelProxy
    {
        public CompoundObjectViewModelProxy()
        {
        }

        public CompoundObjectViewModel Object { get; set; }
        public App.GUI.Icon Icon
        {
            get { return Object != null ? Object.Icon : null; }
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
