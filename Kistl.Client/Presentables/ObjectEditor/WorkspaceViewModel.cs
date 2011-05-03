
namespace Kistl.Client.Presentables.ObjectEditor
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Client.Presentables.KistlBase;
    using System.Collections.Specialized;

    public class WorkspaceViewModel
        : WindowViewModel, IMultipleInstancesManager, IDisposable
    {
        public new delegate WorkspaceViewModel Factory(IKistlContext dataCtx);

        public WorkspaceViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
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
        public bool ShowItemsList
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
        private ICommandViewModel _DeleteCommand = null;
        public ICommandViewModel DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = ViewModelFactory.CreateViewModel<SimpleItemCommandViewModel<DataObjectViewModel>.Factory>().Invoke(
                        DataContext,
                        WorkspaceViewModelResources.DeleteCommand_Name,
                        WorkspaceViewModelResources.DeleteCommand_Tooltip,
                        (items) => items.ForEach(i => i.Delete()));
                }
                return _DeleteCommand;
            }
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
                            WorkspaceViewModelResources.SaveCommand_Name,
                            WorkspaceViewModelResources.SaveCommand_Tooltip,
                            Save, CanSave);

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
                        .Invoke(DataContext, WorkspaceViewModelResources.SaveAndCloseCommand_Name, WorkspaceViewModelResources.SaveAndCloseCommand_Tooltip,
                        () => { Save(); Close(); }, CanSave);

                return _saveAndCloseCommand;
            }
        }

        public void Close()
        {
            this.Show = false;
        }

        private IEnumerable<string> UpdateErrors()
        {
            var result = DataContext.AttachedObjects
                .Where(o => o.ObjectState == DataObjectState.Modified || o.ObjectState == DataObjectState.New)
                .OfType<IDataErrorInfo>()
                .Select(o => o.Error)
                .Where(s => !String.IsNullOrEmpty(s))
                .ToList();

            // Cache that result
            _canSave = result.Count == 0;

            return result;
        }

        // Defaults to true
        // error validation is not called automatically yet
        private bool _canSave = true;

        /// <summary>
        /// Returns a cached result.
        /// Called too often, will slow UI down if it would realy evaluate errors
        /// </summary>
        /// <returns></returns>
        public bool CanSave()
        {
            return _canSave;
        }

        public void Save()
        {
            var loader = ViewModelFactory.CreateDelayedTask(this, () =>
            {
                var errors = UpdateErrors().ToArray();
                if (errors.Length == 0)
                {
                    DataContext.SubmitChanges();
                }
            });
            loader.Trigger();
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
                        .Invoke(DataContext, WorkspaceViewModelResources.AbortCommand_Name, WorkspaceViewModelResources.AbortCommand_Tooltip,
                        Close, null);
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
        public ICommandViewModel VerifyContextCommand
        {
            get
            {
                if (_verifyCommand == null)
                    _verifyCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                            DataContext,
                            WorkspaceViewModelResources.VerifyContextCommand_Name,
                            WorkspaceViewModelResources.VerifyContextCommand_Tootlip,
                            ShowVerificationResults,
                            null);

                return _verifyCommand;
            }
        }

        public void ShowVerificationResults()
        {
            var loader = ViewModelFactory.CreateDelayedTask(this, () =>
            {
                UpdateErrors();
                var elm = ViewModelFactory.CreateViewModel<ErrorListViewModel.Factory>().Invoke(DataContext);
                elm.RefreshErrors();
                ViewModelFactory.ShowModel(elm, true);
            });
            loader.Trigger();
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
                    _ReportProblemCommand = ViewModelFactory.CreateViewModel<ReportProblemCommand.Factory>().Invoke(DataContext);
                }
                return _ReportProblemCommand;
            }
        }
        #endregion

        #endregion

        #region Model Management
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
        public DataObjectViewModel ShowForeignModel(DataObjectViewModel dataObject, Kistl.App.GUI.ControlKind requestedKind)
        {
            if (dataObject == null || dataObject.Object == null)
                return null;

            var other = dataObject.Object;
            var here = DataContext.Find(DataContext.GetInterfaceType(other), other.ID);
            var vm = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, here);
            SelectedItem = vm;
            vm.RequestedKind = requestedKind;
            AddItem(vm);
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
    }
}
