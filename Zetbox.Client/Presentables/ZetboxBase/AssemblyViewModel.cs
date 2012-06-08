
namespace Zetbox.Client.Presentables.ZetboxBase
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    
    public class AssemblyViewModel
        : DataObjectViewModel
    {
        public new delegate DataObjectViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Assembly obj);

        private Assembly _assembly;

        public AssemblyViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            Assembly obj)
            : base(appCtx, dataCtx, parent, obj)
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
                        t => ViewModelFactory.CreateViewModel<SystemTypeViewModel.Factory>().Invoke(DataContext, this, t),
                        mdl => mdl.Value);
                }
                return _typeList;
            }
        }

        #endregion
    }
}
