
namespace Kistl.Client.Presentables.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.API.Configuration;
    
    public class AssemblyModel
        : DataObjectViewModel
    {
        public new delegate DataObjectViewModel Factory(IKistlContext dataCtx, Assembly obj);

        private Assembly _assembly;

        public AssemblyModel(
            IViewModelDependencies appCtx, KistlConfig config, IKistlContext dataCtx,
            Assembly obj)
            : base(appCtx, config, dataCtx, obj)
        {
            _assembly = obj;
        }

        #region Public Interface

        private ReadOnlyProjectedList<Type, SystemTypeModel> _typeList;
        public IReadOnlyList<SystemTypeModel> Types
        {
            get
            {
                if (_typeList == null)
                {
                    _typeList = new ReadOnlyProjectedList<Type, SystemTypeModel>(
                        System.Reflection.Assembly.ReflectionOnlyLoad(_assembly.Name).GetTypes(),
                        t => ModelFactory.CreateViewModel<SystemTypeModel.Factory>().Invoke(DataContext, t),
                        mdl => mdl.Value);
                }
                return _typeList;
            }
        }

        #endregion
    }
}
