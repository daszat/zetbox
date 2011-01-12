
namespace Kistl.Client.Presentables.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    
    public class AssemblyViewModel
        : DataObjectViewModel
    {
#if MONO
        // See https://bugzilla.novell.com/show_bug.cgi?id=660553
        public delegate DataObjectViewModel Factory(IKistlContext dataCtx, Assembly obj);
#else
        public new delegate DataObjectViewModel Factory(IKistlContext dataCtx, Assembly obj);
#endif

        private Assembly _assembly;

        public AssemblyViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            Assembly obj)
            : base(appCtx, dataCtx, obj)
        {
            _assembly = obj;
        }

        #region Public Interface

        private ReadOnlyProjectedList<Type, SystemTypeViewModel> _typeList;
        public IReadOnlyList<SystemTypeViewModel> Types
        {
            get
            {
                if (_typeList == null)
                {
                    _typeList = new ReadOnlyProjectedList<Type, SystemTypeViewModel>(
                        System.Reflection.Assembly.ReflectionOnlyLoad(_assembly.Name).GetTypes(),
                        t => ViewModelFactory.CreateViewModel<SystemTypeViewModel.Factory>().Invoke(DataContext, t),
                        mdl => mdl.Value);
                }
                return _typeList;
            }
        }

        #endregion
    }
}
