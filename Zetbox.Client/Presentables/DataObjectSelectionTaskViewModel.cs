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

namespace Zetbox.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables.ZetboxBase;

    public class DataObjectSelectionTaskEventArgs : EventArgs
    {
        public DataObjectSelectionTaskEventArgs(DataObjectSelectionTaskViewModel vmdl)
        {
            TaskViewModel = vmdl;
        }

        public DataObjectSelectionTaskViewModel TaskViewModel { get; private set; }
    }

    public delegate void DataObjectSelectionTaskCreatedEventHandler(object sender, DataObjectSelectionTaskEventArgs e);

    public class DataObjectSelectionTaskViewModel
        : WindowViewModel, IRefreshCommandListener
    {
        public new delegate DataObjectSelectionTaskViewModel Factory(IZetboxContext dataCtx, ViewModel parent,
            ObjectClass type,
            Func<IQueryable> qry,
            Action<IEnumerable<DataObjectViewModel>> callback,
            IList<CommandViewModel> additionalActions);

        /// <summary>
        /// Initializes a new instance of the SelectionTaskModel class. This is protected since there 
        /// is no ViewModelDescriptor for this class. Instead, either use the
        /// <see cref="DataObjectSelectionTaskViewModel"/> or inherit this for a specific type yourself and 
        /// add your own ViewModelDescriptor and View.
        /// </summary>
        /// <param name="appCtx"></param>
        /// <param name="dataCtx"></param>
        /// <param name="parent">Parent ViewModel</param>
        /// <param name="type"></param>
        /// <param name="qry"></param>
        /// <param name="callback"></param>
        /// <param name="additionalActions"></param>
        public DataObjectSelectionTaskViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            ObjectClass type,
            Func<IQueryable> qry,
            Action<IEnumerable<DataObjectViewModel>> callback,
            IList<CommandViewModel> additionalActions)
            : base(appCtx, dataCtx, parent)
        {
            _callback = callback;
            _additionalActions = new ReadOnlyCollection<CommandViewModel>(additionalActions ?? new CommandViewModel[] { });
            ListViewModel = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(dataCtx, this, () => dataCtx, type, qry);
            ListViewModel.AllowAddNew = true;
            ListViewModel.ObjectCreated += ListViewModel_ObjectCreated;
            ListViewModel.ItemsDefaultAction += ListViewModel_ItemsDefaultAction;
            ListViewModel.ViewMethod = InstanceListViewMethod.Details;

            foreach (var cmd in _additionalActions)
            {
                ListViewModel.Commands.Add(cmd);
            }
        }

        void ListViewModel_ObjectCreated(IDataObject obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            // Same like choose
            var mdl = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), obj);
            Choose(new[] { mdl });
        }

        void ListViewModel_ItemsDefaultAction(InstanceListViewModel sender, IEnumerable<DataObjectViewModel> objects)
        {
            if (objects != null && objects.Count() > 0) Choose(objects);
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
                        this,
                        DataObjectSelectionTaskViewModelResources.Choose,
                        DataObjectSelectionTaskViewModelResources.Choose_Tooltip,
                        () => Choose(SelectedItems),
                        () => SelectedItems != null && SelectedItems.Count() > 0,
                        null);
                }
                return _ChooseCommand;
            }
        }

        public void Choose(IEnumerable<DataObjectViewModel> obj)
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
                        this,
                        DataObjectSelectionTaskViewModelResources.Cancel,
                        DataObjectSelectionTaskViewModelResources.Cancel_Tooltip,
                        Cancel,
                        null,
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

        public IEnumerable<DataObjectViewModel> SelectedItems
        {
            get
            {
                return ListViewModel.SelectedItems;
            }
        }

        #endregion

        private Action<IEnumerable<DataObjectViewModel>> _callback;
        private ReadOnlyCollection<CommandViewModel> _additionalActions;

        public override string Name
        {
            get { return string.Format(DataObjectSelectionTaskViewModelResources.Name, ListViewModel.DataTypeViewModel.Name); }
        }
    }
}
