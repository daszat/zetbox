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

namespace Zetbox.Client.Presentables.ObjectEditor
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables.ZetboxBase;

    public class WorkspaceViewModel
        : WindowViewModel, IMultipleInstancesManager, IContextViewModel, IDeleteCommandParameter, IDisposable
    {
        public new delegate WorkspaceViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public WorkspaceViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
            dataCtx.IsModifiedChanged += dataCtx_IsModifiedChanged;
            Items = new ObservableCollection<ViewModel>();
            Items.CollectionChanged += new NotifyCollectionChangedEventHandler(Items_CollectionChanged);
            appCtx.Factory.OnIMultipleInstancesManagerCreated(dataCtx, this);
        }

        void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("ShowItemsList");
        }

        #region Data
        /// <summary>
        /// A list of "active" <see cref="IDataObject"/>s
        /// </summary>
        public ObservableCollection<ViewModel> Items { get; private set; }

        public string ItemsLabel
        {
            get
            {
                return WorkspaceViewModelResources.ItemsLabel;
            }
        }

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
                    var old = _selectedItem;
                    _selectedItem = value;

                    PropertyChangedEventHandler handler = (sender, e) => { if (e.PropertyName == "Name") OnPropertyChanged("Name"); };
                    if (_selectedItem != null) _selectedItem.PropertyChanged += handler;
                    if (old != null) old.PropertyChanged -= handler;

                    OnPropertyChanged("SelectedItem");
                    OnPropertyChanged("Name");
                }
            }
        }

        private bool? _ShowItemsList;

        /// <summary>
        /// Whether or not the Items list is shown. If set to null, it will reflect whether or not there is more than one Item.
        /// </summary>
        public bool? ShowItemsList
        {
            get
            {
                return _ShowItemsList ?? Items.Count > 1;
            }
            set
            {
                if (_ShowItemsList != value)
                {
                    _ShowItemsList = value;
                    OnPropertyChanged("ShowItemsList");
                }
            }
        }
        #endregion

        #region Context change management
        public bool IsContextModified
        {
            get
            {
                return DataContext.IsModified;
            }
        }

        void dataCtx_IsModifiedChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("IsContextModified");
        }

        public override bool CanClose()
        {
            if (IsContextModified)
            {
                return ViewModelFactory.GetDecisionFromUser(WorkspaceViewModelResources.CanClose, WorkspaceViewModelResources.CanClose_Title);
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Commands

        #region AdditionalCommands
        private ObservableCollection<ICommandViewModel> _AdditionalCommands = null;
        public ObservableCollection<ICommandViewModel> AdditionalCommands
        {
            get
            {
                if (_AdditionalCommands == null)
                {
                    _AdditionalCommands = new ObservableCollection<ICommandViewModel>();
                    _AdditionalCommands.CollectionChanged += _AdditionalCommands_CollectionChanged;
                }
                return _AdditionalCommands;
            }
        }

        void _AdditionalCommands_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("ShowAdditionalCommands");
        }

        private bool? _ShowAdditionalCommands = null;
        public bool ShowAdditionalCommands
        {
            get
            {
                return _ShowAdditionalCommands ?? (_AdditionalCommands != null && _AdditionalCommands.Count > 0);
            }
            set
            {
                if (_ShowAdditionalCommands != value)
                {
                    _ShowAdditionalCommands = value;
                    OnPropertyChanged("ShowAdditionalCommands");
                }
            }
        }
        #endregion

        #region DeleteCommand

        private DeleteDataObjectCommand _DeleteCommand;
        public ICommandViewModel DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = ViewModelFactory.CreateViewModel<DeleteDataObjectCommand.Factory>().Invoke(DataContext, this, this, null, false);
                }
                return _DeleteCommand;
            }
        }

        public void Delete()
        {
            DeleteCommand.Execute(null);
        }

        #endregion

        #region Save Context

        private ICommandViewModel _saveCommand;
        /// <summary>
        /// This command submits all outstanding changes of this Workspace to the data store.
        /// The parameter has to be <value>null</value>.
        /// </summary>
        public ICommandViewModel SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                            DataContext,
                            this,
                            WorkspaceViewModelResources.SaveCommand_Name,
                            WorkspaceViewModelResources.SaveCommand_Tooltip,
                            Save, CanSave, null);

                return _saveCommand;
            }
        }

        private ICommandViewModel _saveAndCloseCommand;
        /// <summary>
        /// This command submits all outstanding changes of this Workspace to the data store.
        /// The parameter has to be <value>null</value>.
        /// </summary>
        public ICommandViewModel SaveAndCloseCommand
        {
            get
            {
                if (_saveAndCloseCommand == null)
                    _saveAndCloseCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>()
                        .Invoke(DataContext, this, WorkspaceViewModelResources.SaveAndCloseCommand_Name, WorkspaceViewModelResources.SaveAndCloseCommand_Tooltip,
                        () => { SaveAndClose(); }, CanSave, null);

                return _saveAndCloseCommand;
            }
        }

        public void Close()
        {
            this.Show = false;
        }

        void IContextViewModel.Abort()
        {
            Close();
        }

        List<IDataErrorInfo> _currentErrors = new List<IDataErrorInfo>();

        public IEnumerable<IDataErrorInfo> GetErrors()
        {
            return _currentErrors;
        }

        public void UpdateErrors()
        {
            _currentErrors = DataContext.AttachedObjects
                .Where(o => o.ObjectState == DataObjectState.Modified || o.ObjectState == DataObjectState.New)
                .OfType<IDataErrorInfo>()
                .Where(s => !String.IsNullOrEmpty(s.Error))
                .ToList();

            foreach (var e in _errorViewModels.ToArray())
            {
                if (!string.IsNullOrEmpty(e.Error))
                {
                    _currentErrors.Add(e);
                }
                else
                {
                    _errorViewModels.Remove(e);
                }
            }
        }

        /// <summary>
        /// Returns a cached result.
        /// Called too often, will slow UI down if it would realy evaluate errors
        /// </summary>
        /// <returns></returns>
        public bool CanSave()
        {
            return _currentErrors.Count == 0;
        }

        public void Save()
        {
            ViewModelFactory.TriggerDelayedTask(this, () =>
            {
                SaveDelayed();
            });
        }

        public void SaveAndClose()
        {
            ViewModelFactory.TriggerDelayedTask(this, () =>
            {
                if (SaveDelayed())
                {
                    Close();
                }
            });
        }

        private bool SaveDelayed()
        {
            UpdateErrors();
            if (_currentErrors.Count == 0)
            {
                try
                {
                    DataContext.SubmitChanges();
                    foreach (var i in Items.OfType<DataObjectViewModel>().ToList())
                    {
                        if (i.Object == null || i.Object.ObjectState == DataObjectState.Deleted)
                        {
                            Items.Remove(i);
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    if (ex.GetInnerException() is ConcurrencyException)
                    {
                        ViewModelFactory.ShowMessage(WorkspaceViewModelResources.ConcurrencyException_Message, WorkspaceViewModelResources.ConcurrencyException_Caption);
                        return false;
                    }
                    else if (ex.GetInnerException() is FKViolationException)
                    {
                        ViewModelFactory.ShowMessage(WorkspaceViewModelResources.FKViolationException_Caption, WorkspaceViewModelResources.FKViolationException_Message);
                        return false;
                    }
                    else if (ex.GetInnerException() is UniqueConstraintViolationException)
                    {
                        ViewModelFactory.ShowMessage(WorkspaceViewModelResources.UniqueConstraintViolationException_Caption, WorkspaceViewModelResources.UniqueConstraintViolationException_Message);
                        return false;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                ShowVerificationResults();
            }

            return false;
        }

        #endregion

        #region AbortCommand
        private ICommandViewModel _AbortCommand = null;
        public ICommandViewModel AbortCommand
        {
            get
            {
                if (_AbortCommand == null)
                {
                    _AbortCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>()
                        .Invoke(DataContext, this, WorkspaceViewModelResources.AbortCommand_Name, WorkspaceViewModelResources.AbortCommand_Tooltip,
                        Close, null, null);
                }
                return _AbortCommand;
            }
        }
        #endregion

        #region Verify Context

        private ICommandViewModel _verifyCommand;
        /// <summary>
        /// This command checks whether all constraints of the attached objects are satisfied.
        /// </summary>
        public ICommandViewModel VerifyCommand
        {
            get
            {
                if (_verifyCommand == null)
                    _verifyCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                            DataContext,
                            this,
                            WorkspaceViewModelResources.VerifyContextCommand_Name,
                            WorkspaceViewModelResources.VerifyContextCommand_Tootlip,
                            ShowVerificationResults,
                            null, null);

                return _verifyCommand;
            }
        }

        public void ShowVerificationResults()
        {
            var loader = ViewModelFactory.CreateDelayedTask(this, () =>
            {
                UpdateErrors();
                if (_currentErrors.Count > 0)
                {
                    ViewModelFactory.ShowModel(ViewModelFactory.CreateViewModel<ErrorListViewModel.Factory>().Invoke(DataContext, this), true);
                }
            });
            loader.Trigger();
        }
        void IContextViewModel.Verify()
        {
            ShowVerificationResults();
        }
        #endregion

        #region ReportProblemCommand
        private ICommandViewModel _ReportProblemCommand = null;
        public ICommandViewModel ReportProblemCommand
        {
            get
            {
                if (_ReportProblemCommand == null)
                {
                    _ReportProblemCommand = ViewModelFactory.CreateViewModel<ReportProblemCommand.Factory>().Invoke(DataContext, this);
                }
                return _ReportProblemCommand;
            }
        }
        #endregion

        #region ElevatedModeCommand
        private ICommandViewModel _ElevatedModeCommand = null;
        public ICommandViewModel ElevatedModeCommand
        {
            get
            {
                if (_ElevatedModeCommand == null)
                {
                    _ElevatedModeCommand = ViewModelFactory.CreateViewModel<ElevatedModeCommand.Factory>().Invoke(DataContext, this);
                }
                return _ElevatedModeCommand;
            }
        }

        #endregion

        #endregion

        #region ErrorManagement
        private HashSet<IDataErrorInfo> _errorViewModels = new HashSet<IDataErrorInfo>();
        public void RegisterError(IDataErrorInfo vmdl)
        {
            _errorViewModels.Add(vmdl);
        }
        #endregion

        #region Model Management

        public DataObjectViewModel ShowForeignObject(IDataObject other, ControlKind requestedKind = null)
        {
            if (other == null)
                return null;

            var here = DataContext.Find(DataContext.GetInterfaceType(other), other.ID);
            var vm = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, this, here);
            SelectedItem = vm;
            vm.RequestedKind = requestedKind;
            AddItem(vm);
            return vm;
        }

        /// <summary>
        /// Show a foreign model by finding and creating the equivalent model on the local DataContext.
        /// </summary>
        /// <param name="dataObject"></param>
        /// <returns></returns>
        public DataObjectViewModel ShowForeignModel(DataObjectViewModel dataObject)
        {
            return ShowForeignModel(dataObject, null);
        }

        /// <summary>
        /// Show a foreign model by finding and creating the equivalent model on the local DataContext.
        /// </summary>
        /// <param name="dataObject"></param>
        /// <param name="requestedKind"></param>
        /// <returns></returns>
        public DataObjectViewModel ShowForeignModel(DataObjectViewModel dataObject, ControlKind requestedKind)
        {
            if (dataObject == null)
                return null;

            return ShowForeignObject(dataObject.Object, requestedKind);
        }

        public void ShowModel(ViewModel mdl)
        {
            if (!Items.Contains(mdl))
            {
                Items.Add(mdl);
            }
            // reestablish selection 
            SelectedItem = mdl;
        }

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

        #region ViewModel Member
        public override string Name
        {
            get { return SelectedItem != null ? SelectedItem.Name : WorkspaceViewModelResources.Name; }
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            ViewModelFactory.OnIMultipleInstancesManagerDisposed(DataContext, this);
        }

        #endregion

        #region IDeleteCommandParameter members
        bool IDeleteCommandParameter.IsReadOnly { get { return false; } }
        bool IDeleteCommandParameter.AllowDelete { get { return true; } }
        IEnumerable<ViewModel> IDeleteCommandParameter.SelectedItems { get { return new[] { SelectedItem }; } }
        #endregion

    }
}
