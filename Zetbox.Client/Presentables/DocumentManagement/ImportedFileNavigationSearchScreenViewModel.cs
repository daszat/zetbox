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
    using Autofac;
    using System.Threading.Tasks;

    [ViewModelDescriptor]
    public class ImportedFileNavigationSearchScreenViewModel : NavigationSearchScreenViewModel
    {
        public new delegate ImportedFileNavigationSearchScreenViewModel Factory(IZetboxContext dataCtx, ViewModel parent, NavigationSearchScreen screen);

        public ImportedFileNavigationSearchScreenViewModel(IViewModelDependencies appCtx,
            IZetboxContext dataCtx, ViewModel parent, NavigationSearchScreen screen)
            : base(appCtx, dataCtx, parent, screen)
        {
            base.Type = typeof(ImportedFile).GetObjectClass(FrozenContext);
        }

        protected override void InitializeListViewModel(ZetboxBase.InstanceListViewModel mdl)
        {
            // setup default behavior
            base.ListViewModel.ViewMethod = InstanceListViewMethod.Details;
            base.ListViewModel.AllowAddNew = false;
            base.ListViewModel.AllowDelete = true;

            // call base later as a nav-screen may override default behavior
            base.InitializeListViewModel(mdl);

            // add custom command
            base.ListViewModel.Commands.Insert(1, OpenAllCommand);
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
                        OpenAll,
                        () => Task.FromResult(ListViewModel.Instances.Count > 0),
                        () => Task.FromResult(ImportedFileNavigationSearchScreenViewModelResources.OpenAllCommand_Reason));
                    Task.Run(async () => _OpenAllCommand.Icon = await IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.fileopen_png.Find(FrozenContext)));
                }
                return _OpenAllCommand;
            }
        }

        public async Task OpenAll()
        {
            var newScope = ViewModelFactory.CreateNewScope();
            var newCtx = newScope.ViewModelFactory.CreateNewContext();

            var newWorkspace = ObjectEditor.WorkspaceViewModel.Create(newScope.Scope, newCtx);
            await newScope.ViewModelFactory.ShowModel(newWorkspace, true);

            newScope.ViewModelFactory.CreateDelayedTask(newWorkspace, () =>
            {
                foreach (var obj in ListViewModel.Instances)
                {
                    newWorkspace.ShowObject(obj.Object, activate: false);
                }
                newWorkspace.SelectedItem = newWorkspace.Items.FirstOrDefault();
            }).Trigger();
        }
    }
}
