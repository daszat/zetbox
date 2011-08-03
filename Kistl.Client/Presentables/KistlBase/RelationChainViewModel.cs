
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
    using System.Collections.ObjectModel;

    [ViewModelDescriptor]
    public class RelationChainViewModel
           : ObjectListViewModel
    {
        public new delegate RelationChainViewModel Factory(IKistlContext dataCtx, ViewModel parent, IValueModel mdl);

        public RelationChainViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent,
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
        protected override ObservableCollection<ICommandViewModel> CreateCommands()
        {
            var cmds = new ObservableCollection<ICommandViewModel>();

            cmds.Add(AddRelationCommand);
            cmds.Add(RemoveCommand);

            return cmds;
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
                        null);
                }
                return _AddRelationCommand;
            }
        }

        public void AddRelation()
        {
            if (Value.Count == 0 && StartingObjectClass == null)
            {
                SelectStartingObjectClass();
            }
            else
            {
                ContinueAddRelation();
            }
        }

        private void ContinueAddRelation()
        {
            var lastRel = Value.Count > 0 ? (Relation)Value.LastOrDefault().Object : null;
            var lastAType = lastRel != null ? lastRel.A.Type : (ObjectClass)StartingObjectClass.Object;
            var lastBType = lastRel != null ? lastRel.B.Type : (ObjectClass)StartingObjectClass.Object;

            var qry = DataContext.GetQuery<Relation>()
                .Where(i => i.A.Type == lastAType || i.A.Type == lastBType || i.B.Type == lastAType || i.B.Type == lastBType);

            var selTaskVMdl = ViewModelFactory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(
                    DataContext,
                    ViewModelFactory.GetWorkspace(DataContext),
                    typeof(Relation).GetObjectClass(FrozenContext),
                    () => qry,
                    new Action<DataObjectViewModel>(delegate(DataObjectViewModel chosen)
                    {
                        if (chosen != null)
                        {
                            AddItem(chosen);
                        }
                    }),
                    null);
            selTaskVMdl.ListViewModel.ShowCommands = false;
            selTaskVMdl.ListViewModel.EnableAutoFilter = false;
            selTaskVMdl.ListViewModel.AddFilter(new ToStringFilterModel(FrozenContext));
            ViewModelFactory.ShowModel(selTaskVMdl, true);
        }

        public void SelectStartingObjectClass()
        {
            var lstMdl = ViewModelFactory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(
                    DataContext,
                    this,
                    typeof(ObjectClass).GetObjectClass(FrozenContext),
                    () => DataContext.GetQuery<ObjectClass>(),
                    new Action<DataObjectViewModel>(delegate(DataObjectViewModel chosen)
                    {
                        if (chosen != null)
                        {
                            StartingObjectClass = chosen;
                            ContinueAddRelation();
                        }
                    }),
                    null);
            lstMdl.ListViewModel.ShowCommands = false;
            ViewModelFactory.ShowModel(lstMdl, true);
        }

        public override void RemoveItem(DataObjectViewModel item)
        {
            if (item == null) { throw new ArgumentNullException("item"); }

            EnsureValueCache();
            var idx = ValueModel.Value.IndexOf(item.Object);
            if (idx != -1)
            {
                for (int i = ValueModel.Value.Count - 1; i >= idx; i--)
                {
                    ValueModel.Value.RemoveAt(i);
                }
            }
        }

        #endregion
    }
}