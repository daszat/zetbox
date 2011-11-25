
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables.ValueViewModels;

    [ViewModelDescriptor]
    public class ParameterInputTaskViewModel
        : WindowViewModel, IValueInputTaskViewModel
    {
        public new delegate ParameterInputTaskViewModel Factory(IKistlContext dataCtx, ViewModel parent, Method method, Action<object[]> callback);

        public ParameterInputTaskViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, Method method, Action<object[]> callback)
            : base(appCtx, dataCtx,  parent)
        {
            if (callback == null) throw new ArgumentNullException("callback");

            _method = method;
            _callback = callback;
        }

        private Method _method;
        private Action<object[]> _callback;

        #region Parameter
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
                _parameterModels = new LookupDictionary<BaseParameter, BaseParameter, BaseValueViewModel>(_parameterList, k => k, v =>
                {
                    var result = BaseValueViewModel.Fetch(ViewModelFactory, DataContext, this, v, v.GetValueModel(false /* c.IsNullable */));
                    return result;
                });
            }
        }

        private ILookup<string, BaseValueViewModel> _parameterModelsByName;
        public ILookup<string, BaseValueViewModel> ValueViewModelsByName
        {
            get
            {
                if (_parameterModelsByName == null)
                {
                    FetchParameterModels();
                    _parameterModelsByName = ValueViewModels.ToLookup(k => k.Name);
                }
                return _parameterModelsByName;
            }
        }
        #endregion

        public override string Name
        {
            get { return _method.GetLabel(); }
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
                        ParameterInputTaskViewModelResources.InvokeCommand_Name,
                        ParameterInputTaskViewModelResources.InvokeCommand_Tooltip, 
                        Invoke,
                        null, 
                        null);
                }
                return _InvokeCommand;
            }
        }

        public void Invoke()
        {
            var parameter = ValueViewModels.Select(i => i.ValueModel.GetUntypedValue()).ToArray();
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

        public void Cancel()
        {
            Show = false;
        }
        #endregion
    }
}
