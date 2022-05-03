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
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.ValueViewModels;

    [ViewModelDescriptor]
    public class RelationChainViewModel
        : ObjectListViewModel
    {
        public new delegate RelationChainViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public RelationChainViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            IObjectCollectionValueModel<IList<IDataObject>> mdl)
            : base(appCtx, dataCtx, parent, mdl)
        {
        }

        private DataObjectViewModel _startingObjectClass = null;
        public DataObjectViewModel StartingObjectClass
        {
            get
            {
                return _startingObjectClass;
            }
            set
            {
                if (_startingObjectClass != value)
                {
                    _startingObjectClass = value;
                    OnPropertyChanged("StartingObjectClass");
                }
            }
        }

        #region Commands
        protected override Task<ObservableCollection<ICommandViewModel>> CreateCommands()
        {
            var cmds = new ObservableCollection<ICommandViewModel>();

            cmds.Add(OpenCommand);
            cmds.Add(AddRelationCommand);
            cmds.Add(RemoveCommand);

            return Task.FromResult(cmds);
        }

        private ICommandViewModel _AddRelationCommand = null;
        public ICommandViewModel AddRelationCommand
        {
            get
            {
                if (_AddRelationCommand == null)
                {
                    _AddRelationCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        null,
                        RelationChainViewModelResources.AddRelationCommand_Name,
                        RelationChainViewModelResources.AddRelationCommand_Tooltip,
                        AddRelation,
                        CanAddRelation,
                        CanAddRelationReason);
                }
                return _AddRelationCommand;
            }
        }

        public Task<bool> CanAddRelation()
        {
            return Task.FromResult((Value.Count == 0 && StartingObjectClass == null) || GetLastClass() != null);
        }

        public Task<string> CanAddRelationReason()
        {
            if ((Value.Count > 0 || StartingObjectClass != null) && GetLastClass() == null)
            {
                return Task.FromResult(RelationChainViewModelResources.AddRelationCommand_ChainInvalidReason);
            }

            return Task.FromResult(string.Empty);
        }

        public Task AddRelation()
        {
            if (Value.Count == 0 && StartingObjectClass == null)
            {
                SelectStartingObjectClass();
            }
            else
            {
                ContinueAddRelation();
            }

            return Task.CompletedTask;
        }

        private ObjectClass GetLastClass()
        {
            var relations = Value.Select(dovm => dovm.Object).Cast<Relation>();
            ObjectClass nextType = StartingObjectClass?.Object as ObjectClass;
            foreach (var rel in relations)
            {
                if (rel.A.Type == nextType)
                {
                    nextType = rel.B.Type;
                }
                else if (rel.B.Type == nextType)
                {
                    nextType = rel.A.Type;
                }
                else
                {
                    return null;
                }
            }
            return nextType;
        }

        private void ContinueAddRelation()
        {
            var lastType = GetLastClass();
            if (lastType == null) return;

            var qry = DataContext.GetQuery<Relation>()
                .Where(i => i.A.Type == lastType || i.B.Type == lastType)
                .ToList()
                .Where(i => !((i.A.Type.ImplementsIChangedBy() && i.A.Navigator != null && i.A.Navigator.Name == "ChangedBy")
                           || (i.B.Type.ImplementsIChangedBy() && i.B.Navigator != null && i.B.Navigator.Name == "ChangedBy")))
                .AsQueryable();

            var selTaskVMdl = ViewModelFactory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(
                    DataContext,
                    ViewModelFactory.GetWorkspace(DataContext),
                    typeof(Relation).GetObjectClass(FrozenContext),
                    () => qry,
                    (chosen) =>
                    {
                        if (chosen != null)
                        {
                            Add(chosen.First());
                        }
                    },
                    null);
            selTaskVMdl.ListViewModel.ShowCommands = false;
            selTaskVMdl.ListViewModel.EnableAutoFilter = false;
            selTaskVMdl.ListViewModel.AddFilter(new ToStringFilterModel(FrozenContext));
            ViewModelFactory.ShowDialog(selTaskVMdl);
        }

        public void SelectStartingObjectClass()
        {
            var lstMdl = ViewModelFactory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(
                    DataContext,
                    this,
                    typeof(ObjectClass).GetObjectClass(FrozenContext),
                    () => DataContext.GetQuery<ObjectClass>(),
                    (chosen) =>
                    {
                        if (chosen != null)
                        {
                            StartingObjectClass = chosen.First();
                            ContinueAddRelation();
                        }
                    },
                    null);
            lstMdl.ListViewModel.ShowCommands = false;
            ViewModelFactory.ShowDialog(lstMdl);
        }

        public override Task Remove()
        {
            if (SelectedItems == null || SelectedItems.Count == 0) return Task.CompletedTask;

            EnsureValueCache();

            // remove all items from the end, to avoid breaking the chain.
            var idx = SelectedItems.Min(i => ValueModel.Value.IndexOf(i.Object));
            if (idx != -1)
            {
                for (int i = ValueModel.Value.Count - 1; i >= idx; i--)
                {
                    ValueModel.Value.RemoveAt(i);
                }
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}