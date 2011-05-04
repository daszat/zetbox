
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.Client.Presentables.KistlBase;

    public class DataObjectSelectionTaskViewModel
        : WindowViewModel, IRefreshCommandListener
    {
        public new delegate DataObjectSelectionTaskViewModel Factory(IKistlContext dataCtx,
            ObjectClass type,
            Func<IQueryable> qry,
            Action<DataObjectViewModel> callback,
            IList<CommandViewModel> additionalActions);

        /// <summary>
        /// Initializes a new instance of the SelectionTaskModel class. This is protected since there 
        /// is no ViewModelDescriptor for this class. Instead, either use the
        /// <see cref="DataObjectSelectionTaskViewModel"/> or inherit this for a specific type yourself and 
        /// add your own ViewModelDescriptor and View.
        /// </summary>
        /// <param name="appCtx"></param>
        /// <param name="dataCtx"></param>
        /// <param name="type"></param>
        /// <param name="qry"></param>
        /// <param name="callback"></param>
        /// <param name="additionalActions"></param>
        public DataObjectSelectionTaskViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            ObjectClass type,
            Func<IQueryable> qry,
            Action<DataObjectViewModel> callback,
            IList<CommandViewModel> additionalActions)
            : base(appCtx, dataCtx)
        {
            _callback = callback;
            _additionalActions = new ReadOnlyCollection<CommandViewModel>(additionalActions ?? new CommandViewModel[] { });
            ListViewModel = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(dataCtx, () => dataCtx, type, qry);
            ListViewModel.AllowAddNew = true;
            ListViewModel.ObjectCreated += ListViewModel_ObjectCreated;
            ListViewModel.ItemsDefaultAction += ListViewModel_ItemsDefaultAction;

            foreach (var cmd in _additionalActions)
            {
                ListViewModel.Commands.Add(cmd);
            }
        }

        void ListViewModel_ObjectCreated(IDataObject obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            // Same like choose
            var mdl = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, obj);
            Choose(mdl);
        }

        void ListViewModel_ItemsDefaultAction(InstanceListViewModel sender, IEnumerable<DataObjectViewModel> objects)
        {
            var obj = objects.FirstOrDefault();
            if (obj != null) Choose(obj);
        }

        public InstanceListViewModel ListViewModel { get; private set; }

        #region Public interface

        public ReadOnlyCollection<CommandViewModel> AdditionalActions
        {
            get
            {
                return _additionalActions;
            }
        }

        private ICommandViewModel _ChooseCommand = null;
        public ICommandViewModel ChooseCommand
        {
            get
            {
                if (_ChooseCommand == null)
                {
                    _ChooseCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext, 
                        DataObjectSelectionTaskViewModelResources.Choose,
                        DataObjectSelectionTaskViewModelResources.Choose_Tooltip, 
                        () => Choose(SelectedItem), 
                        () => SelectedItem != null);
                }
                return _ChooseCommand;
            }
        }

        public void Choose(DataObjectViewModel obj)
        {
            _callback(obj);
            Show = false;
        }

        private ICommandViewModel _CancelCommand = null;
        public ICommandViewModel CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        DataObjectSelectionTaskViewModelResources.Cancel,
                        DataObjectSelectionTaskViewModelResources.Cancel_Tooltip,
                        Cancel, 
                        null);
                }
                return _CancelCommand;
            }
        }

        public void Cancel()
        {
            _callback(null);
            Show = false;
        }

        public void Refresh()
        {
            ListViewModel.ReloadInstances();
        }

        public DataObjectViewModel SelectedItem
        {
            get
            {
                return ListViewModel.SelectedItems.FirstOrDefault();
            }
        }

        #endregion

        private Action<DataObjectViewModel> _callback;
        private ReadOnlyCollection<CommandViewModel> _additionalActions;

        public override string Name
        {
            get { return string.Format(DataObjectSelectionTaskViewModelResources.Name, ListViewModel.DataTypeViewModel.Name); }
        }
    }
}
