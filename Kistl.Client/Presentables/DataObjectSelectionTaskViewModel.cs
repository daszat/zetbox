
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
            IQueryable qry,
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
            IQueryable qry,
            Action<DataObjectViewModel> callback,
            IList<CommandViewModel> additionalActions)
            : base(appCtx, dataCtx)
        {
            _callback = callback;
            _additionalActions = new ReadOnlyCollection<CommandViewModel>(additionalActions ?? new CommandViewModel[] { });
            ListViewModel = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(dataCtx, type, qry);

            foreach (var cmd in _additionalActions)
            {
                ListViewModel.Commands.Add(cmd);
            }

            ListViewModel.ItemsDefaultAction += new InstanceListViewModel.ItemsDefaultActionHandler(ListViewModel_ItemsDefaultAction);
        }

        void ListViewModel_ItemsDefaultAction(IEnumerable<DataObjectViewModel> objects)
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

        public void Choose(DataObjectViewModel choosen)
        {
            if (ListViewModel.Instances.Contains(choosen))
            {
                _callback(choosen);
            }
            else
            {
                _callback(null);
            }
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
            get { return "Choose object of Type " + ListViewModel.DataTypeViewModel.Name; }
        }
    }
}
