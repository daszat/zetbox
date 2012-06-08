using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

using Zetbox.API;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.API.Configuration;
using Zetbox.API.Utils;
using Zetbox.Client.Models;
using Zetbox.Client.Presentables.ValueViewModels;

namespace Zetbox.Client.Presentables
{
    [ViewModelDescriptor]
    public class ValueInputTaskViewModel
        : WindowViewModel, Zetbox.Client.Presentables.IValueInputTaskViewModel
    {
        public new delegate ValueInputTaskViewModel Factory(IZetboxContext dataCtx, ViewModel parent, string name, IList<BaseValueViewModel> valueModels, Action<object[]> callback);

        public ValueInputTaskViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, string name, IList<BaseValueViewModel> valueModels, Action<object[]> callback)
            : base(appCtx, dataCtx, parent)
        {
            if (callback == null) throw new ArgumentNullException("callback");

            _valueModels = valueModels;
            _callback = callback;
            _name = name;
        }

        private Action<object[]> _callback;

        #region Parameter

        private IList<BaseValueViewModel> _valueModels;
        public IEnumerable<BaseValueViewModel> ValueViewModels
        {
            get
            {
                return _valueModels;
            }
        }

        private ILookup<string, BaseValueViewModel> _valueModelsByName;
        public ILookup<string, BaseValueViewModel> ValueViewModelsByName
        {
            get
            {
                if (_valueModelsByName == null)
                {
                    _valueModelsByName = _valueModels.ToLookup(k => k.Name);
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
            var parameter = _valueModels.Select(i => i.ValueModel.GetUntypedValue()).ToArray();
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
