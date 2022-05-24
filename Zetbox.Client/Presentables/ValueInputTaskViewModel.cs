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
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Zetbox.Client.Presentables
{
    [ViewModelDescriptor]
    public class ValueInputTaskViewModel
        : WindowViewModel, IValueInputTaskViewModel
    {
        public new delegate ValueInputTaskViewModel Factory(IZetboxContext dataCtx, ViewModel parent, string name,
            IEnumerable<ViewModel> items,
            IEnumerable<Tuple<object, BaseValueViewModel>> valueModels,
            Action<Dictionary<object, object>> callback);

        public ValueInputTaskViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, string name,
                IEnumerable<ViewModel> items,
                IEnumerable<Tuple<object, BaseValueViewModel>> valueModels,
                Action<Dictionary<object, object>> callback)
            : base(appCtx, dataCtx, parent)
        {
            if (callback == null) throw new ArgumentNullException("callback");

            _items = items;
            _valueModels = valueModels;
            _callback = callback;
            _name = name;
        }

        private Action<Dictionary<object, object>> _callback;
        public Action CancelCallback { get; set; }

        public class CanInvokeEventArgs<T> : EventArgs
        {
            public T Result { get; set; }
            public Dictionary<object, object> Values { get; set; }
        }

        public event EventHandler<CanInvokeEventArgs<bool>> CanInvoke;
        public event EventHandler<CanInvokeEventArgs<string>> CanInvokeReason;

        #region Parameter
        private IEnumerable<ViewModel> _items;
        public IEnumerable<ViewModel> Items
        {
            get
            {
                return _items;
            }
        }


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
        private SimpleCommandViewModel _InvokeCommand = null;
        private void EnsureInvokeCommand()
        {
            if (_InvokeCommand == null)
            {
                _InvokeCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                    DataContext,
                    this,
                    ValueInputTaskViewModelResources.InvokeCommand_Name,
                    ValueInputTaskViewModelResources.InvokeCommand_Tooltip,
                    Invoke,
                    () =>
                    {
                        var e = new CanInvokeEventArgs<bool>() { Result = true, Values = ExtractValues() };
                        CanInvoke?.Invoke(this, e);
                        return Task.FromResult(e.Result);
                    },
                    () =>
                    {
                        var e = new CanInvokeEventArgs<string>() { Result = "", Values = ExtractValues() };
                        CanInvokeReason?.Invoke(this, e);
                        return Task.FromResult(e.Result);
                    });
                _InvokeCommand.IsDefault = true;
            }
        }

        public ICommandViewModel InvokeCommand
        {
            get
            {
                EnsureInvokeCommand();
                return _InvokeCommand;
            }
        }

        public void SetInvokeCommandLabel(string acceptLabel)
        {
            EnsureInvokeCommand();
            _InvokeCommand.Label = acceptLabel;
        }

        public Task Invoke()
        {
            _callback(ExtractValues());
            Show = false;

            return  Task.CompletedTask;;
        }

        private Dictionary<object, object> ExtractValues()
        {
            var parameter = _valueModels.ToDictionary(k => k.Item1, i => i.Item2.ValueModel.GetUntypedValue().Result);
            return parameter;
        }

        private SimpleCommandViewModel _CancelCommand = null;
        private void EnsureCancelCommand()
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
                _CancelCommand.IsCancel = true;
            }
        }

        public ICommandViewModel CancelCommand
        {
            get
            {
                EnsureCancelCommand();
                return _CancelCommand;
            }
        }

        public void SetCancelCommandLabel(string cancelLabel)
        {
            EnsureCancelCommand();
            _CancelCommand.Label = cancelLabel;
        }

        public Task Cancel()
        {
            if (CancelCallback != null) CancelCallback();
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

        public void AddButton(string label, string tooltip, Action<Dictionary<object, object>> callback)
        {
            var cmd = ViewModelFactory
                        .CreateViewModel<SimpleCommandViewModel.Factory>()
                        .Invoke(
                            DataContext,
                            this,
                            label,
                            tooltip,
                            () =>
                            {
                                callback(ExtractValues());
                                Show = false;

                                return Task.CompletedTask;
                            },
                            null,
                            null);
            Commands.Insert(0, cmd);
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            foreach (var i in _items)
            {
                i.Dispose();
            }
        }

        protected override void OnClose()
        {
            base.OnClose();
            this.Dispose();
        }
    }
}
