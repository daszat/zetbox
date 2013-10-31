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

namespace Zetbox.Client.Presentables
{
    [ViewModelDescriptor]
    internal class ValueInputTaskViewModel
        : WindowViewModel, Zetbox.Client.Presentables.IValueInputTaskViewModel
    {
        public new delegate ValueInputTaskViewModel Factory(IZetboxContext dataCtx, ViewModel parent, string name, IEnumerable<Tuple<object, BaseValueViewModel>> valueModels, Action<Dictionary<object, object>> callback);

        public ValueInputTaskViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, string name, IEnumerable<Tuple<object, BaseValueViewModel>> valueModels, Action<Dictionary<object, object>> callback)
            : base(appCtx, dataCtx, parent)
        {
            if (callback == null) throw new ArgumentNullException("callback");

            _valueModels = valueModels;
            _callback = callback;
            _name = name;
        }

        private Action<Dictionary<object, object>> _callback;

        #region Parameter

        private IEnumerable<Tuple<object, BaseValueViewModel>> _valueModels;
        public IEnumerable<BaseValueViewModel> ValueViewModels
        {
            get
            {
                return _valueModels.Select(t => t.Item2);
            }
        }

        private ILookup<object, BaseValueViewModel> _valueModelsByName;
        public ILookup<object, BaseValueViewModel> ValueViewModelsByName
        {
            get
            {
                if (_valueModelsByName == null)
                {
                    _valueModelsByName = _valueModels.ToLookup(k => k.Item1, v => v.Item2);
                }
                return _valueModelsByName;
            }
        }
        #endregion

        private string _name;
        public override string Name
        {
            get { return _name; }
        }

        #region Commands
        private ICommandViewModel _InvokeCommand = null;
        public ICommandViewModel InvokeCommand
        {
            get
            {
                if (_InvokeCommand == null)
                {
                    _InvokeCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        ValueInputTaskViewModelResources.InvokeCommand_Name,
                        ValueInputTaskViewModelResources.InvokeCommand_Tooltip,
                        Invoke,
                        null, null);
                }
                return _InvokeCommand;
            }
        }

        public void Invoke()
        {
            var parameter = _valueModels.ToDictionary(k => k.Item1, i => i.Item2.ValueModel.GetUntypedValue());
            _callback(parameter);
            Show = false;
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
                        this,
                        ValueInputTaskViewModelResources.CancelCommand_Name,
                        ValueInputTaskViewModelResources.CancelCommand_Tooltip,
                        Cancel,
                        null, null);
                }
                return _CancelCommand;
            }
        }

        public void Cancel()
        {
            Show = false;
        }
        #endregion
    }
}
