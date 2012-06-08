namespace Zetbox.Client.Presentables.DocumentManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.App.GUI;
    using at.dasz.DocumentManagement;

    [ViewModelDescriptor]
    public class ImportedFileNavigationSearchScreenViewModel : NavigationSearchScreenViewModel
    {
        public new delegate ImportedFileNavigationSearchScreenViewModel Factory(IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen);

        private readonly Func<IZetboxContext> _ctxFactory;

        public ImportedFileNavigationSearchScreenViewModel(IViewModelDependencies appCtx, Func<IZetboxContext> ctxFactory,
            IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen)
            : base(appCtx, dataCtx, ctxFactory, parent, screen)
        {
            _ctxFactory = ctxFactory;
            base.Type = typeof(ImportedFile).GetObjectClass(FrozenContext);
        }

        protected override void InitializeListViewModel(ZetboxBase.InstanceListViewModel mdl)
        {
            base.InitializeListViewModel(mdl);

            base.ListViewModel.ViewMethod = InstanceListViewMethod.Details;
            base.ListViewModel.AllowAddNew = false;
            base.ListViewModel.Commands.Add(OpenAllCommand);
        }

        private ICommandViewModel _OpenAllCommand = null;
        public ICommandViewModel OpenAllCommand
        {
            get
            {
                if (_OpenAllCommand == null)
                {
                    _OpenAllCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        ImportedFileNavigationSearchScreenViewModelResources.OpenAllCommand_Label,
                        ImportedFileNavigationSearchScreenViewModelResources.OpenAllCommand_Tooltip,
                        OpenAll, null, null);
                    _OpenAllCommand.Icon = Zetbox.NamedObjects.Gui.Icons.ZetboxBase.fileopen_png.Find(FrozenContext);
                }
                return _OpenAllCommand;
            }
        }

        public void OpenAll()
        {
            var newCtx = _ctxFactory();
            var newWorkspace = ViewModelFactory.CreateViewModel<ObjectEditor.WorkspaceViewModel.Factory>().Invoke(newCtx, null);
            ViewModelFactory.ShowModel(newWorkspace, true);

            ViewModelFactory.CreateDelayedTask(newWorkspace, () =>
            {
                foreach (var obj in ListViewModel.Instances)
                {
                    newWorkspace.ShowForeignModel(obj);
                }
                newWorkspace.SelectedItem = newWorkspace.Items.FirstOrDefault();
                newWorkspace.IsBusy = false;
            }).Trigger();
        }
    }
}
