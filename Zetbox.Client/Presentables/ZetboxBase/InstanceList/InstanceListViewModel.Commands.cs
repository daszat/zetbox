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

namespace Zetbox.Client.Presentables.ZetboxBase
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.FilterViewModels;

    public partial class InstanceListViewModel : IRefreshCommandListener, IDeleteCommandParameter
    {
        #region Commands
        protected override ObservableCollection<ICommandViewModel> CreateCommands()
        {
            var result = base.CreateCommands();

            if (AllowAddNew) result.Add(NewCommand);
            if (ShowOpenCommand) result.Add(OpenCommand);
            if (ShowRefreshCommand) result.Add(RefreshCommand);
            if (AllowDelete) result.Add(DeleteCommand);

            if (ShowExportCommand) result.Add(ExportContainerCommand);

            return result;
        }

        private void UpdateCommands()
        {
            if (commandsStore == null) return;
            if (!AllowAddNew && commandsStore.Contains(NewCommand)) commandsStore.Remove(NewCommand);
            if (!ShowOpenCommand && commandsStore.Contains(OpenCommand)) commandsStore.Remove(OpenCommand);
            if (!ShowRefreshCommand && commandsStore.Contains(RefreshCommand)) commandsStore.Remove(RefreshCommand);
            if (!AllowDelete && commandsStore.Contains(DeleteCommand)) commandsStore.Remove(DeleteCommand);
            if (!ShowExportCommand && commandsStore.Contains(ExportContainerCommand)) commandsStore.Remove(ExportContainerCommand);

            if (AllowAddNew && !commandsStore.Contains(NewCommand)) commandsStore.Insert(0, NewCommand);
            if (ShowOpenCommand && !commandsStore.Contains(OpenCommand)) commandsStore.Insert(AllowAddNew ? 1 : 0, OpenCommand);
            if (ShowRefreshCommand && !commandsStore.Contains(RefreshCommand)) commandsStore.Insert((AllowAddNew ? 1 : 0) + (ShowOpenCommand ? 1 : 0), RefreshCommand);
            if (AllowDelete && !commandsStore.Contains(DeleteCommand)) commandsStore.Insert((AllowAddNew ? 1 : 0) + (ShowOpenCommand ? 1 : 0) + (ShowRefreshCommand ? 1 : 0), DeleteCommand);
            if (ShowExportCommand && !commandsStore.Contains(ExportContainerCommand)) commandsStore.Insert((AllowAddNew ? 1 : 0) + (ShowOpenCommand ? 1 : 0) + (ShowRefreshCommand ? 1 : 0) + (AllowDelete ? 1 : 0), ExportContainerCommand);
        }

        private ICommandViewModel _RefreshCommand;
        public ICommandViewModel RefreshCommand
        {
            get
            {
                if (_RefreshCommand == null)
                {
                    _RefreshCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        CommonCommandsResources.RefreshCommand_Name,
                        CommonCommandsResources.RefreshCommand_Tooltip,
                        ReloadInstances,
                        CanExecReloadInstances,
                        CanExecReloadInstancesReason);
                    _RefreshCommand.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.reload_png.Find(FrozenContext));
                }
                return _RefreshCommand;
            }
        }

        private ICommandViewModel _OpenCommand;
        public ICommandViewModel OpenCommand
        {
            get
            {
                if (_OpenCommand == null)
                {
                    _OpenCommand = ViewModelFactory.CreateViewModel<SimpleItemCommandViewModel<DataObjectViewModel>.Factory>().Invoke(
                        DataContext,
                        this,
                        CommonCommandsResources.OpenDataObjectCommand_Name,
                        CommonCommandsResources.OpenDataObjectCommand_Tooltip,
                        OpenObjects);
                    _OpenCommand.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.fileopen_png.Find(FrozenContext));
                }
                return _OpenCommand;
            }
        }

        private ICommandViewModel _NewCommand;
        public ICommandViewModel NewCommand
        {
            get
            {
                if (_NewCommand == null)
                {
                    _NewCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext, this,
                        CommonCommandsResources.NewDataObjectCommand_Name,
                        CommonCommandsResources.NewDataObjectCommand_Tooltip,
                        NewObject,
                        () => AllowAddNew,
                        null);
                    _NewCommand.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.new_png.Find(FrozenContext));
                }
                return _NewCommand;
            }
        }

        public void NewObject()
        {
            if (!AllowAddNew) return;

            ObjectClass baseclass = _type;

            var children = new List<ObjectClass>();
            if (baseclass.IsAbstract == false)
            {
                children.Add(baseclass);
            }
            baseclass.CollectChildClasses(children, false);

            if (children.Count == 1)
            {
                CreateNewObjectAndNotify(children.Single());
            }
            else
            {
                var lstMdl = ViewModelFactory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(
                                        DataContext, this,
                                        typeof(ObjectClass).GetObjectClass(FrozenContext),
                                        () => children.AsQueryable(),
                                        (chosen) =>
                                        {
                                            if (chosen != null)
                                            {
                                                CreateNewObjectAndNotify((ObjectClass)chosen.First().Object);
                                            }
                                        }, null);
                lstMdl.ListViewModel.ShowCommands = false;

                ViewModelFactory.ShowDialog(lstMdl);
            }
        }

        private void CreateNewObjectAndNotify(ObjectClass type)
        {
            var workingCtx = workingCtxFactory == null ? DataContext : workingCtxFactory();
            var obj = workingCtx.Create(DataContext.GetInterfaceType(type.GetDataType()));
            OnObjectCreated(obj);

            if (isEmbedded())
            {
                // TODO: Reorganize this control - it's too complex
                var mdl = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), obj);
                AddLocalInstance(mdl);
                this.SelectedItem = mdl;

                if (this.DataType.IsSimpleObject && !IsEditable)
                {
                    // Open in a Dialog
                    var dlg = ViewModelFactory.CreateViewModel<SimpleDataObjectEditorTaskViewModel.Factory>().Invoke(DataContext, this, mdl);
                    ViewModelFactory.ShowDialog(dlg);
                }
                else if (!this.DataType.IsSimpleObject)
                {
                    ViewModelFactory.ShowModel(mdl, true);
                }
                // Don't open simple objects
            }
            else
            {
                var newWorkspace = ViewModelFactory.CreateViewModel<ObjectEditor.WorkspaceViewModel.Factory>().Invoke(workingCtx, null);
                newWorkspace.ShowForeignModel(DataObjectViewModel.Fetch(ViewModelFactory, workingCtx, newWorkspace, obj), RequestedEditorKind);
                ViewModelFactory.ShowModel(newWorkspace, RequestedWorkspaceKind, true);
            }
        }

        public delegate void ObjectCreatedHandler(IDataObject obj);
        public event ObjectCreatedHandler ObjectCreated;

        protected void OnObjectCreated(IDataObject obj)
        {
            ObjectCreatedHandler temp = ObjectCreated;
            if (temp != null)
            {
                temp(obj);
            }
        }

        private DeleteDataObjectCommand _DeleteCommand;
        public DeleteDataObjectCommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = ViewModelFactory.CreateViewModel<DeleteDataObjectCommand.Factory>().Invoke(DataContext, this, this, this, !isEmbedded());
                }
                return _DeleteCommand;
            }
        }

        private ICommandViewModel _SelectColumnsCommand = null;
        public ICommandViewModel SelectColumnsCommand
        {
            get
            {
                if (_SelectColumnsCommand == null)
                {
                    _SelectColumnsCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext,
                        null, // Hide Ardorner
                        InstanceListViewModelResources.SelectColumnsCommand,
                        InstanceListViewModelResources.SelectColumnsCommand_Tooltip,
                        SelectColumns,
                        () => AllowSelectColumns,
                        null);
                    _SelectColumnsCommand.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.todo_png.Find(FrozenContext));
                }
                return _SelectColumnsCommand;
            }
        }

        public void SelectColumns()
        {
            var dlg = ViewModelFactory.CreateViewModel<PropertySelectionTaskViewModel.Factory>()
                .Invoke(DataContext,
                    this,
                    _type,
                    props => { });
            dlg.FollowRelationsOne = true;
            dlg.MultiSelect = true;
            dlg.UpdateInitialSelectedProperties(this.DisplayedProperties);
            dlg.SelectedPropertySelectionChanged += (s, e) =>
            {
                if (e.Item.IsSelected)
                {
                    DisplayedColumns.Columns.Add(ColumnDisplayModel.Create(GridDisplayConfiguration.Mode.ReadOnly, e.Item.Properties));
                    ViewMethod = InstanceListViewMethod.Details;
                }
                else
                {
                    var col = DisplayedColumns.Columns.FirstOrDefault(c => c.Property == e.Item.Property);
                    if (col != null) DisplayedColumns.Columns.Remove(col);
                }
            };
            ViewModelFactory.ShowDialog(dlg);
        }

        public void ResetDisplayedColumns()
        {
            _displayedColumns = null;
            OnPropertyChanged("DisplayedColumns");
        }
        #endregion

        #region IRefreshCommandListener Member
        /// <summary>
        /// Just calls ReloadInstances().
        /// </summary>
        void IRefreshCommandListener.Refresh()
        {
            ReloadInstances();
        }
        #endregion

        #region IDeleteCommandParameter Members
        public bool IsReadOnly { get { return false; } }
        IEnumerable<ViewModel> IDeleteCommandParameter.SelectedItems { get { return SelectedItems; } }
        #endregion
    }
}
