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

namespace Zetbox.Client.Presentables.ZetboxBase
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using Zetbox.API.Utils;
    using Zetbox.Client.Presentables.ValueViewModels;

    public partial class InstanceListViewModel
    {
        readonly Dictionary<DataObjectViewModel, DataObjectViewModelProxy> _proxyCache = new Dictionary<DataObjectViewModel, DataObjectViewModelProxy>();

        private DataObjectViewModel GetObjectFromProxy(DataObjectViewModelProxy p)
        {
            if (p.Object == null)
            {
                var obj = DataContext.Create(DataContext.GetInterfaceType(_type.GetDataType()));
                p.Object = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), obj);
                _proxyCache[p.Object] = p;
                OnObjectCreated(obj);
            }
            return p.Object;
        }

        private DataObjectViewModelProxy GetProxy(DataObjectViewModel vm)
        {
            DataObjectViewModelProxy result;
            if (!_proxyCache.TryGetValue(vm, out result))
            {
                result = new DataObjectViewModelProxy() { Object = vm };
                _proxyCache[vm] = result;
            }
            return result;
        }

        /// <summary>
        /// Hack for those who do not check element types by traversing from inherited interfaces
        /// e.g. DataGrid from WPF
        /// </summary>
        public sealed class ProxyList : AbstractProjectedList<DataObjectViewModel, DataObjectViewModelProxy>, IList, IList<DataObjectViewModelProxy>
        {
            public ProxyList(IList<DataObjectViewModel> list, Func<DataObjectViewModel, DataObjectViewModelProxy> select, Func<DataObjectViewModelProxy, DataObjectViewModel> inverter)
                : base(list, select, inverter, false)
            {
            }
        }

        private ProxyList _proxyInstances = null;
        /// <summary>
        /// Allow instances to be added external
        /// </summary>
        public ProxyList ProxyInstances
        {
            get
            {
                if (_proxyInstances == null)
                {
                    if(_instances == null)
                        ReloadInstances();

                    _proxyInstances = new ProxyList(
                        _instances,
                        (vm) => GetProxy(vm),
                        (p) => GetObjectFromProxy(p));
                }
                return _proxyInstances;
            }
        }

        private ObservableProjectedList<DataObjectViewModel, DataObjectViewModelProxy> _selectedProxies = null;
        public ObservableProjectedList<DataObjectViewModel, DataObjectViewModelProxy> SelectedProxies
        {
            get
            {
                if (_selectedProxies == null)
                {
                    _selectedProxies = new ObservableProjectedList<DataObjectViewModel, DataObjectViewModelProxy>(
                        SelectedItems,
                        (vm) => GetProxy(vm),
                        (p) => GetObjectFromProxy(p));
                }
                return _selectedProxies;
            }
        }

        private void ResetProxies()
        {
            _proxyCache.Clear();
            _proxyInstances = null;
            OnPropertyChanged("ProxyInstances");
        }
    }
}
