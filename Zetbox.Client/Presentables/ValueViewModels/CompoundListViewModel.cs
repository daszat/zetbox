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

namespace Zetbox.Client.Presentables.ValueViewModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Models;

    /// <summary>
    /// </summary>
    [ViewModelDescriptor]
    public class CompoundListViewModel
        : BaseCompoundCollectionViewModel<IList<ICompoundObject>>, IValueListViewModel<CompoundObjectViewModel, IReadOnlyObservableList<CompoundObjectViewModel>>, ISortableViewModel
    {
        public new delegate CompoundListViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public CompoundListViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            ICompoundCollectionValueModel<IList<ICompoundObject>> mdl)
            : base(appCtx, dataCtx, parent, mdl)
        {
        }

        protected override ObservableCollection<ICommandViewModel> CreateCommands()
        {
            var commands = base.CreateCommands();
            commands.Add(ClearSortCommand);
            return commands;
        }

        #region Public interface and IReadOnlyValueModel<IReadOnlyObservableCollection<CompoundObjectViewModel>> Members

        protected override string InitialSortProperty { get { return null; } }


        public bool HasPersistentOrder
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Move
        public bool CanMove()
        {
            return SelectedItem != null && !IsFiltering && !IsSorting && !IsReadOnly;
        }

        public void MoveItemUp()
        {
            var memories = SelectedItems.ToList();
            memories.ForEach(i => MoveItemUp(i));
            SelectedItems.Clear();
            memories.ForEach(i => SelectedItems.Add(i));
        }

        public void MoveItemUp(CompoundObjectViewModel item)
        {
            if (item == null) { return; }

            var idx = ValueModel.Value.IndexOf(item.Object);
            if (idx > 0)
            {
                ValueModel.Value.RemoveAt(idx);
                ValueModel.Value.Insert(idx - 1, item.Object);
            }
        }

        public void MoveItemTop()
        {
            var memories = SelectedItems.ToList();
            memories.Reverse();
            memories.ForEach(i => MoveItemTop(i));
            SelectedItems.Clear();
            memories.ForEach(i => SelectedItems.Add(i));
        }

        public void MoveItemTop(CompoundObjectViewModel item)
        {
            if (item == null) { return; }

            var idx = ValueModel.Value.IndexOf(item.Object);
            if (idx > 0)
            {
                ValueModel.Value.RemoveAt(idx);
                ValueModel.Value.Insert(0, item.Object);
            }
        }

        public void MoveItemDown()
        {
            var memories = SelectedItems.ToList();
            memories.ForEach(i => MoveItemDown(i));
            SelectedItems.Clear();
            memories.ForEach(i => SelectedItems.Add(i));
        }

        public void MoveItemDown(CompoundObjectViewModel item)
        {
            if (item == null) { return; }

            var idx = ValueModel.Value.IndexOf(item.Object);
            if (idx != -1 && idx + 1 < Value.Count)
            {
                ValueModel.Value.RemoveAt(idx);
                ValueModel.Value.Insert(idx + 1, item.Object);
            }
        }
        public void MoveItemBottom()
        {
            var memories = SelectedItems.ToList();
            memories.ForEach(i => MoveItemBottom(i));
            SelectedItems.Clear();
            memories.ForEach(i => SelectedItems.Add(i));
        }

        public void MoveItemBottom(CompoundObjectViewModel item)
        {
            if (item == null) { return; }

            var idx = ValueModel.Value.IndexOf(item.Object);
            if (idx != -1 && idx + 1 < Value.Count)
            {
                ValueModel.Value.RemoveAt(idx);
                ValueModel.Value.Add(item.Object);
            }
        }

        private ICommandViewModel _MoveItemUpCommand = null;
        public ICommandViewModel MoveItemUpCommand
        {
            get
            {
                if (_MoveItemUpCommand == null)
                {
                    _MoveItemUpCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        BaseObjectCollectionViewModelResources.MoveItemUpCommand_Name,
                        BaseObjectCollectionViewModelResources.MoveItemUpCommand_Tooltip,
                        MoveItemUp,
                        CanMove,
                        null);
                }
                return _MoveItemUpCommand;
            }
        }

        private ICommandViewModel _MoveItemTopCommand = null;
        public ICommandViewModel MoveItemTopCommand
        {
            get
            {
                if (_MoveItemTopCommand == null)
                {
                    _MoveItemTopCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        BaseObjectCollectionViewModelResources.MoveItemTopCommand_Name,
                        BaseObjectCollectionViewModelResources.MoveItemTopCommand_Tooltip,
                        MoveItemTop,
                        CanMove,
                        null);
                }
                return _MoveItemTopCommand;
            }
        }

        private ICommandViewModel _MoveItemDownCommand = null;
        public ICommandViewModel MoveItemDownCommand
        {
            get
            {
                if (_MoveItemDownCommand == null)
                {
                    _MoveItemDownCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        BaseObjectCollectionViewModelResources.MoveItemDownCommand_Name,
                        BaseObjectCollectionViewModelResources.MoveItemDownCommand_Tooltip,
                        MoveItemDown,
                        CanMove,
                        null);
                }
                return _MoveItemDownCommand;
            }
        }

        private ICommandViewModel _MoveItemBottomCommand = null;
        public ICommandViewModel MoveItemBottomCommand
        {
            get
            {
                if (_MoveItemBottomCommand == null)
                {
                    _MoveItemBottomCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        BaseObjectCollectionViewModelResources.MoveItemBottomCommand_Name,
                        BaseObjectCollectionViewModelResources.MoveItemBottomCommand_Tooltip,
                        MoveItemBottom,
                        CanMove,
                        null);
                }
                return _MoveItemBottomCommand;
            }
        }
        #endregion

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case "SortProperty":
                    OnPropertyChanged("IsSorting");
                    break;
            }
        }

        public bool IsSorting
        {
            get
            {
                return !string.IsNullOrEmpty(SortProperty);
            }
        }

        private ICommandViewModel _ClearSortCommand = null;
        public ICommandViewModel ClearSortCommand
        {
            get
            {
                if (_ClearSortCommand == null)
                {
                    _ClearSortCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Clear sort", "Clear the current sorting",
                        ClearSort,
                        () => IsSorting,
                        null);
                }
                return _ClearSortCommand;
            }
        }

        public void ClearSort()
        {
            if (IsSorting)
            {
                Sort(null, SortDirection);
            }
        }
    }
}
