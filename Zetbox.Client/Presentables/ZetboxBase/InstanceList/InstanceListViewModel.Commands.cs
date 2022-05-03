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
    using System.Threading.Tasks;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.FilterViewModels;

    public partial class InstanceListViewModel
        : IRefreshCommandListener, IDeleteCommandParameter, INewCommandParameter, IOpenCommandParameter
    {
        #region Commands
        protected override async Task<System.Collections.ObjectModel.ObservableCollection<ICommandViewModel>> CreateCommands()
        {
            var result = await base.CreateCommands();

            var showOpenCommand = AllowOpen && (OpenCommand == DefaultCommand);
            var showDefaultCommand = OpenCommand != DefaultCommand;

            if (AllowAddNew) result.Add(NewCommand);
            if (showOpenCommand) result.Add(OpenCommand);
            if (showDefaultCommand) result.Add(DefaultCommand);
            result.Add(RefreshCommand);
            if (AllowDelete) result.Add(DeleteCommand);

            if (AllowExport) result.Add(ExportContainerCommand);

            if (AllowMerge) result.Add(MergeCommand);

            return result;
        }

        private void UpdateCommands()
        {
            if (commandsStore == null) return;

            // Recreate list, but preserce content
            // This is needed, as the Toolbar will not re-apply a DataTemplate
            // http://stackoverflow.com/questions/16019855/how-can-i-solve-a-toolbar-bug-with-datatemplates
            commandsStore = new ObservableCollection<ICommandViewModel>(commandsStore);

            var showOpenCommand = AllowOpen && (OpenCommand == DefaultCommand);
            var showDefaultCommand = OpenCommand != DefaultCommand;

            if (commandsStore.Contains(NewCommand)) commandsStore.Remove(NewCommand);
            if (commandsStore.Contains(OpenCommand)) commandsStore.Remove(OpenCommand);
            if (commandsStore.Contains(_defaultCommand)) commandsStore.Remove(DefaultCommand);
            if (commandsStore.Contains(RefreshCommand)) commandsStore.Remove(RefreshCommand);
            if (commandsStore.Contains(DeleteCommand)) commandsStore.Remove(DeleteCommand);
            if (commandsStore.Contains(ExportContainerCommand)) commandsStore.Remove(ExportContainerCommand);
            if (commandsStore.Contains(MergeCommand)) commandsStore.Remove(MergeCommand);

            var index = 0;
            if (AllowAddNew) commandsStore.Insert(index++, NewCommand);
            if (showOpenCommand) commandsStore.Insert(index++, OpenCommand);
            if (showDefaultCommand) commandsStore.Insert(index++, DefaultCommand);
            commandsStore.Insert(index++, RefreshCommand);
            if (AllowDelete) commandsStore.Insert(index++, DeleteCommand);
            if (AllowExport) commandsStore.Insert(index++, ExportContainerCommand);
            if (AllowMerge) commandsStore.Insert(index++, MergeCommand);

            OnPropertyChanged("Commands");
        }

        private ICommandViewModel _defaultCommand;
        public ICommandViewModel DefaultCommand
        {
            get
            {
                return _defaultCommand ?? OpenCommand;
            }
            set
            {
                if (_defaultCommand != value)
                {
                    if (_defaultCommand != null && commandsStore != null && commandsStore.Contains(_defaultCommand))
                    {
                        commandsStore.Remove(commandsStore);
                    }
                    _defaultCommand = value;
                    OnPropertyChanged("DefaultCommand");
                    UpdateCommands();
                }
            }
        }

        public void Default()
        {
            if (DefaultCommand.CanExecute(null))
                DefaultCommand.Execute(null);
        }

        private RefreshCommand _RefreshCommand;
        public ICommandViewModel RefreshCommand
        {
            get
            {
                if (_RefreshCommand == null)
                {
                    _RefreshCommand = ViewModelFactory.CreateViewModel<RefreshCommand.Factory>().Invoke(
                        DataContext,
                        this);
                    _RefreshCommand.CanRefresh += (s, e) =>
                    {
                        e.CanRefresh = FilterList.IsFilterValid;
                        if (!e.CanRefresh)
                        {
                            e.CanRefreshReason = FilterListEntryViewModelResources.RequiredFilterMissingReason;
                        }
                    };
                }
                return _RefreshCommand;
            }
        }

        private OpenDataObjectCommand _OpenCommand;
        public ICommandViewModel OpenCommand
        {
            get
            {
                EnsureOpenCommand();
                return _OpenCommand;
            }
        }

        private void EnsureOpenCommand()
        {
            if (_OpenCommand == null)
            {
                _OpenCommand = ViewModelFactory.CreateViewModel<OpenDataObjectCommand.Factory>().Invoke(DataContext, this);
            }
        }

        public void Open()
        {
            if (OpenCommand.CanExecute(null))
                OpenCommand.Execute(null);
        }

        private NewDataObjectCommand _NewCommand;
        public ICommandViewModel NewCommand
        {
            get
            {
                EnsureNewCommand();
                return _NewCommand;
            }
        }

        private void EnsureNewCommand()
        {
            if (_NewCommand == null)
            {
                _NewCommand = ViewModelFactory.CreateViewModel<NewDataObjectCommand.Factory>().Invoke(
                    DataContext,
                    this,
                    DataType);

                _NewCommand.ObjectCreated += OnObjectCreated;
                _NewCommand.LocalModelCreated += OnLocalModelCreated;
            }
        }

        public void New()
        {
            if (NewCommand.CanExecute(null))
                NewCommand.Execute(null);
        }

        private void OnLocalModelCreated(DataObjectViewModel vm)
        {
            AddLocalInstance(vm);
            this.SelectedItem = vm;
        }

        public delegate void ObjectCreatedHandler(IDataObject obj);
        public event ObjectCreatedHandler ObjectCreated;

        private void OnObjectCreated(IDataObject obj)
        {
            ObjectCreatedHandler temp = ObjectCreated;
            if (temp != null)
            {
                temp(obj);
            }
        }

        private DeleteDataObjectCommand _DeleteCommand;
        public ICommandViewModel DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = ViewModelFactory.CreateViewModel<DeleteDataObjectCommand.Factory>().Invoke(DataContext, this);
                }
                return _DeleteCommand;
            }
        }

        public void Delete()
        {
            if (DeleteCommand.CanExecute(null))
                DeleteCommand.Execute(null);
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
                        () => Task.FromResult(AllowSelectColumns),
                        null);
                    Task.Run(async () => _SelectColumnsCommand.Icon = await IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.todo_png.Find(FrozenContext)));
                }
                return _SelectColumnsCommand;
            }
        }

        public async Task SelectColumns()
        {
            var dlg = ViewModelFactory.CreateViewModel<PropertySelectionTaskViewModel.Factory>()
                .Invoke(DataContext,
                    this,
                    _type,
                    props => { });
            dlg.FollowRelationsOne = true;
            dlg.FollowRelationsMany = true;
            dlg.FollowRelationsManyDeep = false; // Only first level!
            dlg.MultiSelect = true;
            dlg.UpdateInitialSelectedProperties(this.DisplayedProperties);
            dlg.SelectedPropertySelectionChanged += (s, e) =>
            {
                if (e.Item.IsSelected)
                {
                    AddDisplayColumn(e.Item.Properties);
                    ViewMethod = InstanceListViewMethod.Details;
                }
                else
                {
                    RemoveDisplayColumn(e.Item.Property);
                }
            };
            await ViewModelFactory.ShowDialog(dlg);
        }

        public void ResetDisplayedColumns()
        {
            _displayedColumns = null;
            OnPropertyChanged("DisplayedColumns");
        }

        private ICommandViewModel _MergeCommand = null;
        public ICommandViewModel MergeCommand
        {
            get
            {
                if (_MergeCommand == null)
                {
                    _MergeCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, 
                        InstanceListViewModelResources.MergeCommand, 
                        InstanceListViewModelResources.MergeCommand_Tooltip, 
                        Merge, 
                        CanMerge, 
                        CanMergeReason);
                    Task.Run(async () => _MergeCommand.Icon = await IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.reload_png.Find(FrozenContext)));
                }
                return _MergeCommand;
            }
        }

        public Task<bool> CanMerge()
        {
            return Task.FromResult(SelectedItems.Count == 2);
        }

        public Task<string> CanMergeReason()
        {
            return Task.FromResult(InstanceListViewModelResources.MergeCommand_Reason);
        }

        public async Task Merge()
        {
            if (!(await CanMerge())) return;

            if (GetWorkspace() is IContextViewModel && GetWorkspace() is IMultipleInstancesManager)
            {
                // Stay in current workspace
                var ws = (IMultipleInstancesManager)GetWorkspace();
                if(ws != null)
                {
                    var task = ViewModelFactory.CreateViewModel<ObjectEditor.MergeObjectsTaskViewModel.Factory>().Invoke(DataContext, GetWorkspace(), SelectedItems[0].Object, SelectedItems[1].Object);
                    ws.AddItem(task);
                    ws.SelectedItem = task;
                }
            } 
            else 
            {
                var newScope = ViewModelFactory.CreateNewScope();
                var newCtx = newScope.ViewModelFactory.CreateNewContext();

                var ift = DataType.GetDescribedInterfaceType();
                var target = newCtx.Find(ift, SelectedItems[0].ID);
                var source = newCtx.Find(ift, SelectedItems[1].ID);

                var ws = ObjectEditor.WorkspaceViewModel.Create(newScope.Scope, newCtx);
                var task = newScope.ViewModelFactory.CreateViewModel<ObjectEditor.MergeObjectsTaskViewModel.Factory>().Invoke(newCtx, ws, target, source);
                ws.ShowModel(task);
                await newScope.ViewModelFactory.ShowModel(ws, RequestedWorkspaceKind, true);
            }
        }
        #endregion

        #region IDeleteCommandParameter Members
        public bool IsReadOnly { get { return false; } }
        IEnumerable<ViewModel> ICommandParameter.SelectedItems { get { return SelectedItems; } }
        #endregion
    }
}
