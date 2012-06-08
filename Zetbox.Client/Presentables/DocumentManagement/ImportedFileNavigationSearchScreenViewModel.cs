// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
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
