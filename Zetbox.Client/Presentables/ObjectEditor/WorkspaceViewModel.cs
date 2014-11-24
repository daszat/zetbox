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

    [ViewModelDescriptor]
    public class WorkspaceViewModel
        : WindowViewModel, IMultipleInstancesManager, IContextViewModel, IDeleteCommandParameter, IDisposable
    {
        public new delegate WorkspaceViewModel Factory(IZetboxContext dataCtx, ViewModel parent);
        private readonly IZetboxContextExceptionHandler _exceptionHandler;

        public WorkspaceViewModel(IViewModelDependencies appCtx,
            IZetboxContext dataCtx, ViewModel parent,
            IZetboxContextExceptionHandler exceptionHandler)
            : base(appCtx, dataCtx, parent)
        {
            if (exceptionHandler == null) throw new ArgumentNullException("exceptionHandler");

            _exceptionHandler = exceptionHandler;
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

        public string SaveChangesHintText
        {
            get
            {
                return WorkspaceViewModelResources.SaveChangesHint;
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
                    _DeleteCommand = ViewModelFactory.CreateViewModel<DeleteDataObjectCommand.Factory>().Invoke(DataContext, this);
                }
                return _DeleteCommand;
            }
        }

        public void Delete()
        {
            if (DeleteCommand.CanExecute(null))
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
                            () => { Save(); }, CanSave, null);

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
                .Where(o => o.CurrentAccessRights.HasReadRights())
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

        public bool Save()
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
                    if (_exceptionHandler.Show(DataContext, ex))
                    {
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

        public void SaveAndClose()
        {
            if (Save())
            {
                Close();
            }
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
            UpdateErrors();
            if (_currentErrors.Count > 0)
            {
                var errorList = ViewModelFactory.CreateViewModel<ErrorListViewModel.Factory>().Invoke(DataContext, this);
                errorList.RefreshErrors();
                ViewModelFactory.ShowModel(errorList, true);
            }
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

        public DataObjectViewModel ShowObject(IDataObject obj, ControlKind requestedKind = null)
        {
            obj = obj == null || obj.Context == DataContext
                ? obj
                : DataContext.Find(obj.Context.GetInterfaceType(obj), obj.ID);

            var vm = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, this, obj);
            if (!Items.Contains(vm))
            {
                vm.RequestedKind = requestedKind;
                AddItem(vm);
            }
            SelectedItem = vm;
            return vm;
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
        bool IDeleteCommandParameter.IsReadOnly
        {
            get
            {
                var dovm = SelectedItem as DataObjectViewModel;
                return dovm != null ? dovm.IsReadOnly : true;
            }
        }
        bool IDeleteCommandParameter.AllowDelete { get { return true; } }
        IEnumerable<ViewModel> ICommandParameter.SelectedItems { get { return SelectedItem == null ? null : new[] { SelectedItem }; } }
        #endregion

        #region DragDrop
        public virtual bool CanDrop
        {
            get
            {
                return true;
            }
        }

        public virtual bool OnDrop(object data)
        {
            if (data is IDataObject[])
            {
                var lst = (IDataObject[])data;
                foreach (var obj in lst)
                {
                    ShowObject(obj);
                }
            }
            return false;
        }

        public virtual object DoDragDrop()
        {
            var obj = (SelectedItem as DataObjectViewModel).IfNotNull(dvm => dvm.Object);
            if (obj != null && obj.ObjectState.In(DataObjectState.Unmodified, DataObjectState.Modified, DataObjectState.New))
            {
                return new IDataObject[] { obj };
            }
            return null;
        }
        #endregion
    }
}
