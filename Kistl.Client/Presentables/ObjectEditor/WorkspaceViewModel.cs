using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.API.Client;
using System.ComponentModel;

namespace Kistl.Client.Presentables.ObjectEditor
{
    public class WorkspaceViewModel
        : WindowViewModel, IMultipleInstancesManager, IDisposable
    {
        public new delegate WorkspaceViewModel Factory(IKistlContext dataCtx);

        public WorkspaceViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
            Items = new ObservableCollection<ViewModel>();
            appCtx.Factory.OnIMultipleInstancesManagerCreated(dataCtx, this);
        }

        #region Data
        /// <summary>
        /// A list of "active" <see cref="IDataObject"/>s
        /// </summary>
        public ObservableCollection<ViewModel> Items { get; private set; }

        private ViewModel _selectedItem;
        /// <summary>
        /// The last selected ViewModel.
        /// </summary>
        public ViewModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        #endregion

        #region Commands

        private ICommand _DeleteCommand = null;
        public ICommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = ModelFactory.CreateViewModel<SimpleItemCommandModel<DataObjectModel>.Factory>().Invoke(DataContext, "Delete", "",
                        (items) => items.ForEach(i => i.Delete()));
                }
                return _DeleteCommand;
            }
        }

        #region Save Context

        private ICommand _saveCommand;
        /// <summary>
        /// This command submits all outstanding changes of this Workspace to the data store.
        /// The parameter has to be <value>null</value>.
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = ModelFactory.CreateViewModel<SimpleCommandModel.Factory>()
                        .Invoke(DataContext, "Save", "Saves outstanding changes to the data store.",
                        Save, CanSave);

                return _saveCommand;
            }
        }

        private ICommand _saveAndCloseCommand;
        /// <summary>
        /// This command submits all outstanding changes of this Workspace to the data store.
        /// The parameter has to be <value>null</value>.
        /// </summary>
        public ICommand SaveAndCloseCommand
        {
            get
            {
                if (_saveAndCloseCommand == null)
                    _saveAndCloseCommand = ModelFactory.CreateViewModel<SimpleCommandModel.Factory>()
                        .Invoke(DataContext, "Save & Close", "Saves outstanding changes to the data store and closes the current workspace.",
                        () => { Save(); Close(); }, CanSave);

                return _saveAndCloseCommand;
            }
        }

        public void Close()
        {
            this.Show = false;
        }

        private IEnumerable<string> GetErrors()
        {
            return DataContext.AttachedObjects
                .Where(o => o.ObjectState == DataObjectState.Modified || o.ObjectState == DataObjectState.New)
                .OfType<IDataErrorInfo>()
                .Select(o => o.Error)
                .Where(s => !String.IsNullOrEmpty(s));
        }

        public bool CanSave()
        {
            return GetErrors().Count() == 0;
        }

        public void Save()
        {
            var errors = GetErrors().ToArray();
            if (errors.Length == 0)
            {
                DataContext.SubmitChanges();
            }
        }
        #endregion

        #region AbortCommand
        private ICommand _AbortCommand = null;
        public ICommand AbortCommand
        {
            get
            {
                if (_AbortCommand == null)
                {
                    _AbortCommand = ModelFactory.CreateViewModel<SimpleCommandModel.Factory>()
                        .Invoke(DataContext, "Abort", "Closes this workspace without saving", 
                        Close, null);
                }
                return _AbortCommand;
            }
        }
        #endregion

        #region History Touch

        /// <summary>
        /// registers a user contact with the mdl in this <see cref="WorkspaceViewModel"/>'s history
        /// </summary>
        /// <param name="mdl"></param>
        public void AddItem(ViewModel mdl)
        {
            // fetch old SelectedItem to reestablish selection after modifying RecentObjects
            var item = SelectedItem;
            if (!Items.Contains(mdl))
            {
                Items.Add(mdl);
            }
            // reestablish selection 
            SelectedItem = item;
        }

        #endregion

        #region Verify Context

        private ICommand _verifyCommand;
        /// <summary>
        /// This command checks whether all constraints of the attached objects are satisfied.
        /// </summary>
        public ICommand VerifyContextCommand
        {
            get
            {
                if (_verifyCommand == null)
                    _verifyCommand = ModelFactory.CreateViewModel<SimpleCommandModel.Factory>()
                        .Invoke(DataContext, "Verify", "Verifies that all constraints are met.", ShowVerificationResults, null);

                return _verifyCommand;
            }
        }

        public void ShowVerificationResults()
        {
            var elm = ModelFactory.CreateViewModel<ErrorListModel.Factory>().Invoke(DataContext);
            elm.RefreshErrors();
            ModelFactory.ShowModel(elm, true);
        }


        #endregion

        #endregion

        /// <summary>
        /// Show a foreign model by finding and creating the equivalent model on the local DataContext.
        /// </summary>
        /// <param name="dataObject"></param>
        /// <returns></returns>
        public void ShowForeignModel(DataObjectModel dataObject)
        {
            ShowForeignModel(dataObject, null);
        }

        /// <summary>
        /// Show a foreign model by finding and creating the equivalent model on the local DataContext.
        /// </summary>
        /// <param name="dataObject"></param>
        /// <param name="requestedKind"></param>
        /// <returns></returns>
        public void ShowForeignModel(DataObjectModel dataObject, Kistl.App.GUI.ControlKind requestedKind)
        {
            if (dataObject == null || dataObject.Object == null)
                return;

            var other = dataObject.Object;
            var here = DataContext.Find(DataContext.GetInterfaceType(other), other.ID);
            var vm = ModelFactory.CreateViewModel<DataObjectModel.Factory>(here).Invoke(DataContext, here);
            SelectedItem = vm;
            vm.RequestedKind = requestedKind;
            AddItem(vm);
        }

        public override string Name
        {
            get { return "Workspace"; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            ModelFactory.OnIMultipleInstancesManagerDisposed(DataContext, this);
        }

        #endregion
    }
}
