﻿// This file is part of zetbox.
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

    public partial class InstanceListViewModel
        : IRefreshCommandListener, IDeleteCommandParameter, INewCommandParameter, IOpenCommandParameter
    {
        #region Commands
        protected override ObservableCollection<ICommandViewModel> CreateCommands()
        {
            var result = base.CreateCommands();

            if (AllowAddNew) result.Add(NewCommand);
            if (AllowOpen) result.Add(OpenCommand);
            result.Add(RefreshCommand);
            if (AllowDelete) result.Add(DeleteCommand);

            if (AllowExport) result.Add(ExportContainerCommand);

            return result;
        }

        private void UpdateCommands()
        {
            if (commandsStore == null) return;
            if (!AllowAddNew && commandsStore.Contains(NewCommand)) commandsStore.Remove(NewCommand);
            if (!AllowOpen && commandsStore.Contains(OpenCommand)) commandsStore.Remove(OpenCommand);
            if (commandsStore.Contains(RefreshCommand)) commandsStore.Remove(RefreshCommand);
            if (!AllowDelete && commandsStore.Contains(DeleteCommand)) commandsStore.Remove(DeleteCommand);
            if (!AllowExport && commandsStore.Contains(ExportContainerCommand)) commandsStore.Remove(ExportContainerCommand);

            if (AllowAddNew && !commandsStore.Contains(NewCommand)) commandsStore.Insert(0, NewCommand);
            if (AllowOpen && !commandsStore.Contains(OpenCommand)) commandsStore.Insert(AllowAddNew ? 1 : 0, OpenCommand);
            if (!commandsStore.Contains(RefreshCommand)) commandsStore.Insert((AllowAddNew ? 1 : 0) + (AllowOpen ? 1 : 0), RefreshCommand);
            if (AllowDelete && !commandsStore.Contains(DeleteCommand)) commandsStore.Insert((AllowAddNew ? 1 : 0) + (AllowOpen ? 1 : 0) + 1, DeleteCommand);
            if (AllowExport && !commandsStore.Contains(ExportContainerCommand)) commandsStore.Insert((AllowAddNew ? 1 : 0) + (AllowOpen ? 1 : 0) + 1 + (AllowDelete ? 1 : 0), ExportContainerCommand);
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
                        e.CanRefresh = !FilterList.RequiredFilterMissing;
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
            NewCommand.Execute(null);
        }

        private void OnLocalModelCreated(DataObjectViewModel vm)
        {
            AddLocalInstance(vm);
            this.SelectedItem = vm;
        }

        //public void ActivateItem(DataObjectViewModel item)
        //{
        //    ActivateDataObjectCommand.ActivateItem(ViewModelFactory, DataContext, FrozenContext, this, item, IsInlineEditable);
        //    OnItemsOpened(ViewModelFactory.GetWorkspace(DataContext), new[] { item });
        //}

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

        #region IDeleteCommandParameter Members
        public bool IsReadOnly { get { return false; } }
        IEnumerable<ViewModel> ICommandParameter.SelectedItems { get { return SelectedItem == null ? null : new[] { SelectedItem }; } }
        #endregion
    }
}
