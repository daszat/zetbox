
namespace Zetbox.Client.Presentables.ZetboxBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.Client.Models;

    public class TypeRefPropertyViewModel
           : ObjectReferenceViewModel
    {
        public new delegate TypeRefPropertyViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public TypeRefPropertyViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            IValueModel mdl)
            : base(appCtx, dataCtx, parent, mdl)
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