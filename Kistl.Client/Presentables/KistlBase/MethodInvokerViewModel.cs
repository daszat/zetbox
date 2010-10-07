
namespace Kistl.Client.Presentables.KistlBase
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

    public class MethodInvokerViewModel
        : ViewModel
    {
        public new delegate MethodInvokerViewModel Factory(IKistlContext dataCtx, Method mdl);

        public MethodInvokerViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx, Method mdl)
            : base(appCtx, dataCtx)
        {
            _method = mdl;
        }

        private Method _method;

        private ReadOnlyProjectedList<BaseParameter, ViewModel> _parameteryModels = null;
        public IReadOnlyList<ViewModel> ParameterModels
        {
            get
            {
                if (_parameteryModels == null)
                {
                    //_parameteryModels = new ReadOnlyProjectedList<BaseParameter, ViewModel>(
                    //    _invocation.Parameter.ToList(),
                    //    param => ModelFactory.CreateViewModel<BasePropertyModel.Factory>(property).Invoke(DataContext, Object, param),
                    //    null);
                }
                return _parameteryModels;
            }
        }
        private LookupDictionary<string, BaseParameter, ViewModel> _propertyModelsByName = null;
        public LookupDictionary<string, BaseParameter, ViewModel> PropertyModelsByName
        {
            get
            {
                if (_propertyModelsByName == null)
                {
                    //_propertyModelsByName = new LookupDictionary<string, BaseParameter, ViewModel>(_invocation.Parameter.ToList(), 
                    //    param => param.Name, prop => ModelFactory.CreateViewModel<BasePropertyModel.Factory>(prop).Invoke(DataContext, Object, prop));
                }
                return _propertyModelsByName;
            }
        }

        public override string Name
        {
            get { return _method.Name; }
        }
    }
}
