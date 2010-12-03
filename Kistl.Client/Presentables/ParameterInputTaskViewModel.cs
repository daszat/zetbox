using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.API.Configuration;
using Kistl.API.Utils;
using Kistl.Client.Models;
using Kistl.Client.Presentables.ValueViewModels;

namespace Kistl.Client.Presentables
{
    [ViewModelDescriptor]
    public class ParameterInputTaskViewModel
        : WindowViewModel
    {
        public new delegate ParameterInputTaskViewModel Factory(IKistlContext dataCtx, Method method, Action<object[]> callback);

        public ParameterInputTaskViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx, Method method, Action<object[]> callback)
            : base(appCtx, dataCtx)
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
        public IReadOnlyList<BaseValueViewModel> ParameterModels
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
                    var result = BaseValueViewModel.Fetch(ViewModelFactory, DataContext, v, v.GetValueModel());
                    return result;
                });
            }
        }

        private LookupDictionary<string, BaseParameter, BaseValueViewModel> _parameterModelsByName;
        public LookupDictionary<string, BaseParameter, BaseValueViewModel> ParameterModelsByName
        {
            get
            {
                if (_parameterModelsByName == null)
                {
                    FetchParameterModels();
                    _parameterModelsByName = new LookupDictionary<string, BaseParameter, BaseValueViewModel>(
                        _parameterList,
                        k => k.Name,
                        v => _parameterModels[v]
                    );
                }
                return _parameterModelsByName;
            }
        }
        #endregion

        public override string Name
        {
            get { return _method.Name; }
        }

        #region Commands
        private ICommandViewModel _InvokeCommand = null;
        public ICommandViewModel InvokeCommand
        {
            get
            {
                if (_InvokeCommand == null)
                {
                    _InvokeCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, "Invoke", "Invokes the method", Invoke, null);
                }
                return _InvokeCommand;
            }
        }

        public void Invoke()
        {
            var parameter = ParameterModels.Select(i => i.ValueModel.GetUntypedValue()).ToArray();
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
                    _CancelCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, "Cancel", "Cancel the invocation", Cancel, null);
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
