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
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.ValueViewModels;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    [ViewModelDescriptor]
    public class ParameterInputTaskViewModel
        : WindowViewModel, IValueInputTaskViewModel
    {
        public new delegate ParameterInputTaskViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Method method, Action<object[]> callback);

        public ParameterInputTaskViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, Method method, Action<object[]> callback)
            : base(appCtx, dataCtx, parent)
        {
            if (callback == null) throw new ArgumentNullException("callback");

            _method = method;
            _callback = callback;
        }

        private Method _method;
        private Action<object[]> _callback;

        #region Parameter
        public IEnumerable<ViewModel> Items
        {
            get
            {
                return ValueViewModels;
            }
        }

        private List<BaseParameter> _parameterList = null;
        private void FetchParameterList()
        {
            if (_parameterList == null)
            {
                _parameterList = _method.Parameter.Where(i => !i.IsReturnParameter).ToList();
            }

        }

        private ReadOnlyProjectedList<BaseParameter, BaseValueViewModel> _parameterModelList;
        public IEnumerable<BaseValueViewModel> ValueViewModels
        {
            get
            {
                if (_parameterModelList == null)
                {
                    FetchParameterModels();
                    _parameterModelList = new ReadOnlyProjectedList<BaseParameter, BaseValueViewModel>(_parameterList, p => _parameterModels[p], m => null); //m.Property);
                }
                return _parameterModelList;
            }
        }

        private LookupDictionary<BaseParameter, BaseParameter, BaseValueViewModel> _parameterModels;
        private void FetchParameterModels()
        {
            if (_parameterModels == null)
            {
                FetchParameterList();
                _parameterModels = new LookupDictionary<BaseParameter, BaseParameter, BaseValueViewModel>(_parameterList, k => k, async v =>
                {
                    var result = BaseValueViewModel.Fetch(ViewModelFactory, DataContext, this, v, await v.GetValueModel(DataContext, v.IsNullable));
                    return result;
                });
            }
        }

        private ILookup<object, BaseValueViewModel> _parameterModelsByName;
        public ILookup<object, BaseValueViewModel> ValueViewModelsByName
        {
            get
            {
                if (_parameterModelsByName == null)
                {
                    FetchParameterModels();
                    _parameterModelsByName = _parameterModels.ToLookup(k => (object)k.Key.Name, v => v.Value);
                }
                return _parameterModelsByName;
            }
        }
        #endregion

        private string _name;
        public override string Name
        {
            get
            {
                Task.Run(async () => await GetName());
                return _name;
            }
        }

        public async Task<string> GetName()
        {
            _name = await _method.GetLabel();
            OnPropertyChanged(nameof(Name));
            return _name;
        }

        #region Commands
        private SimpleCommandViewModel _InvokeCommand = null;
        public ICommandViewModel InvokeCommand
        {
            get
            {
                if (_InvokeCommand == null)
                {
                    _InvokeCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        ParameterInputTaskViewModelResources.InvokeCommand_Name,
                        ParameterInputTaskViewModelResources.InvokeCommand_Tooltip,
                        Invoke,
                        null,
                        null);
                    _InvokeCommand.IsDefault = true;
                }
                return _InvokeCommand;
            }
        }

        public Task Invoke()
        {
            var parameter = ValueViewModels.Select(i => i.ValueModel.GetUntypedValue()).ToArray();
            _callback(parameter);
            Show = false;

            return Task.CompletedTask;
        }

        private ICommandViewModel _CancelCommand = null;
        public ICommandViewModel CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        null,
                        ParameterInputTaskViewModelResources.CancelCommand_Name,
                        ParameterInputTaskViewModelResources.CancelCommand_Tooltip,
                        Cancel,
                        null,
                        null);
                }
                return _CancelCommand;
            }
        }

        public Task Cancel()
        {
            Show = false;
            return Task.CompletedTask;
        }

        protected override async Task<ObservableCollection<ICommandViewModel>> CreateCommands()
        {
            var result = await base.CreateCommands();
            result.Add(InvokeCommand);
            result.Add(CancelCommand);
            return result;
        }
        #endregion

        protected override void OnClose()
        {
            base.OnClose();
            Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (_parameterModelList != null)
            {
                foreach (var p in _parameterModelList)
                {
                    p.Dispose();
                }
            }
        }
    }
}
