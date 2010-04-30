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
            RecentObjects = new ObservableCollection<ViewModel>();
            appCtx.Factory.OnIMultipleInstancesManagerCreated(dataCtx, this);
        }

        #region Data
        /// <summary>
        /// A list of "active" <see cref="IDataObject"/>s
        /// </summary>
        public ObservableCollection<ViewModel> RecentObjects { get; private set; }

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

        #region Save Context

        private SaveContextCommand _saveCommand;
        /// <summary>
        /// This command submits all outstanding changes of this Workspace to the data store.
        /// The parameter has to be <value>null</value>.
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = ModelFactory.CreateViewModel<SaveContextCommand.Factory>().Invoke(DataContext);

                return _saveCommand;
            }
        }

        #endregion

        #region Create New Instance

        private CreateNewInstanceCommand _createNewInstanceCommand = null;
        /// <summary>
        /// Creates a new instance of an <see cref="ObjectClass"/> and makes it the currently selected instance.
        /// </summary>
        public ICommand CreateNewInstanceCommand
        {
            get
            {
                if (_createNewInstanceCommand == null)
                {
                    _createNewInstanceCommand = ModelFactory.CreateViewModel<CreateNewInstanceCommand.Factory>().Invoke(DataContext, this);
                }
                return _createNewInstanceCommand;
            }
        }

        #endregion

        #region History Touch

        /// <summary>
        /// registers a user contact with the mdl in this <see cref="WorkspaceViewModel"/>'s history
        /// </summary>
        /// <param name="mdl"></param>
        public void HistoryTouch(ViewModel mdl)
        {
            // fetch old SelectedItem to reestablish selection after modifying RecentObjects
            var item = SelectedItem;
            if (!RecentObjects.Contains(mdl))
            {
                RecentObjects.Add(mdl);
            }
            // reestablish selection 
            SelectedItem = item;
        }

        #endregion

        #region Verify Context

        private VerifyContextCommand _verifyCommand;
        /// <summary>
        /// This command checks whether all constraints of the attached objects are satisfied.
        /// </summary>
        public ICommand VerifyContextCommand
        {
            get
            {
                if (_verifyCommand == null)
                    _verifyCommand = ModelFactory.CreateViewModel<VerifyContextCommand.Factory>().Invoke(DataContext);

                return _verifyCommand;
            }
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
            if (dataObject == null || dataObject.Object == null)
                return;

            var other = dataObject.Object;
            var here = DataContext.Find(DataContext.GetInterfaceType(other), other.ID);
            SelectedItem = ModelFactory.CreateViewModel<DataObjectModel.Factory>(here).Invoke(DataContext, here);
            HistoryTouch(SelectedItem);
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

    /// <summary>
    /// </summary>
    internal class VerifyContextCommand : CommandModel
    {
        public new delegate VerifyContextCommand Factory(IKistlContext dataCtx);

        public VerifyContextCommand(IViewModelDependencies appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx, "Verify", "Verifies that all constraints are met.")
        {
        }

        /// <summary>
        /// Returns true.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool CanExecute(object data)
        {
            return true;
        }

        protected override void DoExecute(object data)
        {
            var elm = ModelFactory.CreateViewModel<ErrorListModel.Factory>().Invoke(DataContext);
            elm.RefreshErrors();
            ModelFactory.ShowModel(elm, true);
        }
    }

    /// <summary>
    /// This command submits all outstanding changes of this Workspace to the data store.
    /// The parameter has to be <value>null</value>.
    /// </summary>
    internal class SaveContextCommand : CommandModel
    {
        public new delegate SaveContextCommand Factory(IKistlContext dataCtx);

        public SaveContextCommand(IViewModelDependencies appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx, "Save", "Saves outstanding changes to the data store.")
        {
        }

        private bool CheckValidity()
        {
            var errors = GetErrors();
            return true; // GetErrors().Count() == 0;
        }

        private IEnumerable<string> GetErrors()
        {
            return DataContext.AttachedObjects.Where(po => po.ObjectState != DataObjectState.Unmodified).OfType<IDataErrorInfo>().Select(o => o.Error).Where(s => !String.IsNullOrEmpty(s));
        }
        /// <summary>
        /// Returns true if no data was passed.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool CanExecute(object data)
        {
            return data == null && CheckValidity();
        }

        protected override void DoExecute(object data)
        {
            var errors = GetErrors().ToArray();
            if (errors.Length > 0)
            {
                // throw new InvalidOperationException("Cannot save due to the following errors: " + String.Join("\n", errors));
            }
            DataContext.SubmitChanges();
        }

    }

    /// <summary>
    /// Creates a new instance of an <see cref="ObjectClass"/> and makes it the currently selected instance.
    /// </summary>
    internal class CreateNewInstanceCommand : CommandModel
    {
        public new delegate CreateNewInstanceCommand Factory(IKistlContext dataCtx, WorkspaceViewModel parent);

        public CreateNewInstanceCommand(IViewModelDependencies appCtx, IKistlContext dataCtx, WorkspaceViewModel parent)
            : base(appCtx, dataCtx, "New", "Create a new instance")
        {
            _parent = parent;
        }

        private WorkspaceViewModel _parent;

        public override bool CanExecute(object data)
        {
            return DataContext != null
                && !DataContext.IsReadonly
                && _parent != null
                && data != null
                && data is ObjectClassModel;
        }

        protected override void DoExecute(object data)
        {
            if (CanExecute(data))
            {
                var objectClass = data as ObjectClassModel;
                var newObject = DataContext.Create(objectClass.GetDescribedInterfaceType());
                var newModel = ModelFactory.CreateViewModel<DataObjectModel.Factory>(newObject).Invoke(DataContext, newObject);
                _parent.HistoryTouch(newModel);
                _parent.SelectedItem = newModel;
            }
        }
    }
}
