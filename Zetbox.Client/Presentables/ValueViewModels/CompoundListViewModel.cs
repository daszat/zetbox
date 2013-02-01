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

    public class CompoundListViewModel
        : ValueViewModel<IReadOnlyObservableList<CompoundObjectViewModel>, IList<ICompoundObject>>, IValueListViewModel<CompoundObjectViewModel, IReadOnlyObservableList<CompoundObjectViewModel>>
    {
        public new delegate CompoundListViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public CompoundListViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            ICompoundCollectionValueModel mdl)
            : base(appCtx, dataCtx, parent, mdl)
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

        public void ActivateItem(CompoundObjectViewModel item)
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
        }

        protected override ZbTask<IReadOnlyObservableList<CompoundObjectViewModel>> GetValueFromModel()
        {
            return new ZbTask<IReadOnlyObservableList<CompoundObjectViewModel>>(ZbTask.Synchron, () =>
            {
                if (_valueCache == null)
                {
                    _valueCache = new ReadOnlyObservableProjectedList<ICompoundObject, CompoundObjectViewModel>(
                        ValueModel, ValueModel.Value,
                        obj => CompoundObjectViewModel.Fetch(ViewModelFactory, DataContext, this, obj),
                        mdl => mdl.Object);
                    //_valueCache.CollectionChanged += ValueListChanged;
                }
                return _valueCache;
            });
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

            result.BuildColumns(ValueModel.CompoundObjectDefinition, mode, true);
            return result;
        }

        #region Proxy
        private CompoundObjectViewModel GetObjectFromProxy(CompoundObjectViewModelProxy p)
        {
            if (p.Object == null)
            {
                var obj = DataContext.CreateCompoundObject(DataContext.GetInterfaceType(this.ValueModel.CompoundObjectDefinition.GetDataType()));
                p.Object = CompoundObjectViewModel.Fetch(ViewModelFactory, DataContext, this, obj);
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
                result = new CompoundObjectViewModelProxy() { Object = CompoundObjectViewModel.Fetch(ViewModelFactory, DataContext, this, obj) };
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
