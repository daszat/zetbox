using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Client.Presentables.KistlBase;

namespace Kistl.Client.Presentables
{
    public class DataObjectSelectionTaskModel
        : ViewModel, IRefreshCommandListener
    {
        public new delegate DataObjectSelectionTaskModel Factory(IKistlContext dataCtx,
            DataType type,
            IQueryable qry,
            Action<DataObjectModel> callback,
            IList<CommandModel> additionalActions);

        /// <summary>
        /// Initializes a new instance of the SelectionTaskModel class. This is protected since there 
        /// is no ViewModelDescriptor for this class. Instead, either use the
        /// <see cref="DataObjectSelectionTaskModel"/> or inherit this for a specific type yourself and 
        /// add your own ViewModelDescriptor and View.
        /// </summary>
        /// <param name="appCtx"></param>
        /// <param name="dataCtx"></param>
        /// <param name="type"></param>
        /// <param name="qry"></param>
        /// <param name="callback"></param>
        /// <param name="additionalActions"></param>
        public DataObjectSelectionTaskModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            DataType type,
            IQueryable qry,
            Action<DataObjectModel> callback,
            IList<CommandModel> additionalActions)
            : base(appCtx, dataCtx)
        {
            _callback = callback;
            _additionalActions = new ReadOnlyCollection<CommandModel>(additionalActions ?? new CommandModel[] { });
            ListViewModel = ModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(dataCtx, type, qry);
            ListViewModel.Commands.Clear();
            foreach (var cmd in _additionalActions)
            {
                ListViewModel.Commands.Add(cmd);
            }

            ListViewModel.ItemsDefaultAction += new InstanceListViewModel.ItemsDefaultActionHandler(ListViewModel_ItemsDefaultAction);
        }

        void ListViewModel_ItemsDefaultAction(IEnumerable<DataObjectModel> objects)
        {
            var obj = objects.FirstOrDefault();
            if (obj != null) Choose(obj);
        }

        public InstanceListViewModel ListViewModel { get; private set; }


        #region Public interface
        public ReadOnlyCollection<CommandModel> AdditionalActions
        {
            get
            {
                return _additionalActions;
            }
        }

        public void Choose(DataObjectModel choosen)
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

        public DataObjectModel SelectedItem
        {
            get
            {
                return ListViewModel.SelectedItems.FirstOrDefault();
            }
        }

        private bool _show = true;
        public bool Show
        {
            get { return _show; }
            private set { _show = value; OnPropertyChanged("Show"); }
        }

        #endregion

        private Action<DataObjectModel> _callback;
        private ReadOnlyCollection<CommandModel> _additionalActions;

        public override string Name
        {
            get { return "Choose object of Type " + ListViewModel.DataTypeModel.Name; }
        }
    }
}
