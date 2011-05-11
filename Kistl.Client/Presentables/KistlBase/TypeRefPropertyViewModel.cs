
namespace Kistl.Client.Presentables.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Client.Presentables.ValueViewModels;
    using Kistl.Client.Models;

    public class TypeRefPropertyViewModel
           : ObjectReferenceViewModel
    {
        public new delegate TypeRefPropertyViewModel Factory(IKistlContext dataCtx, IValueModel mdl);

        public TypeRefPropertyViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            IValueModel mdl)
            : base(appCtx, dataCtx, mdl)
        {
            // TODO: use a static reference here
            //if (prop.GetReferencedObjectClass().Name != "TypeRef")
            //{
            //    throw new ArgumentOutOfRangeException("prop", "Can only handle TypeRef References");
            //}
        }

        protected override System.Collections.ObjectModel.ObservableCollection<ICommandViewModel> CreateCommands()
        {
            var cmds = base.CreateCommands();
            cmds.Add(LoadTypeRefFromAssemblyRefCommand);
            cmds.Add(LoadTypeRefFromAssemblyFileCommand);
            return cmds;
        }

        private LoadTypeRefFromAssemblyFileCommand _loadTypeRefFromAssemblyFileCommand = null;
        public ICommandViewModel LoadTypeRefFromAssemblyFileCommand
        {
            get
            {
                if (_loadTypeRefFromAssemblyFileCommand == null)
                {
                    _loadTypeRefFromAssemblyFileCommand = ViewModelFactory.CreateViewModel<LoadTypeRefFromAssemblyFileCommand.Factory>().Invoke(DataContext, this, this);
                }
                return _loadTypeRefFromAssemblyFileCommand;
            }
        }

        private LoadTypeRefFromAssemblyRefCommand _loadTypeRefFromAssemblyRefCommand = null;
        public ICommandViewModel LoadTypeRefFromAssemblyRefCommand
        {
            get
            {
                if (_loadTypeRefFromAssemblyRefCommand == null)
                {
                    _loadTypeRefFromAssemblyRefCommand = ViewModelFactory.CreateViewModel<LoadTypeRefFromAssemblyRefCommand.Factory>().Invoke(DataContext, this, this);
                }
                return _loadTypeRefFromAssemblyRefCommand;
            }
        }
    }
}