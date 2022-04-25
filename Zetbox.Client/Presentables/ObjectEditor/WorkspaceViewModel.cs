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
    using System.Threading.Tasks;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.Client.Presentables.ZetboxBase;

    [ViewModelDescriptor]
    public class WorkspaceViewModel
        : WindowViewModel, IMultipleInstancesManager, IContextViewModel, IDeleteCommandParameter
    {
        public new delegate WorkspaceViewModel Factory(IZetboxContext dataCtx, ViewModel parent);
        private readonly IZetboxContextExceptionHandler _exceptionHandler;

        public static WorkspaceViewModel Create(ILifetimeScope scope, IZetboxContext ctx)
        {
            if (scope == null) throw new ArgumentNullException("scope");
            if (ctx == null) throw new ArgumentNullException("ctx");

            var vmf = scope.Resolve<IViewModelFactory>();

            var ws = vmf.CreateViewModel<ObjectEditor.WorkspaceViewModel.Factory>().Invoke(ctx, null);
            ws.Closed += (s, e) => scope.Dispose();
            return ws;
        }

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

            ValidationManager.Changed += ValidationManager_Changed;
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

        public string ErrorsLabel
        {
            get
            {
                return WorkspaceViewModelResources.ErrorsLabel;
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

        #region CommandCollection management
        protected override ObservableCollection<ICommandViewModel> CreateCommands()
        {
            var result = base.CreateCommands();

            result.Add(SaveAndCloseCommand);
            result.Add(SaveCommand);
            result.Add(VerifyCommand);
            result.Add(DeleteCommand);
            result.Add(AbortCommand);

            return result;
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
                {
                    _saveCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                            DataContext,
                            this,
                            WorkspaceViewModelResources.SaveCommand_Name,
                            WorkspaceViewModelResources.SaveCommand_Tooltip,
                            () => { Save(); }, CanSave, null);
                    _saveCommand.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.save_png.Find(FrozenContext));
                }
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
                {
                    _saveAndCloseCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>()
                        .Invoke(DataContext, this, WorkspaceViewModelResources.SaveAndCloseCommand_Name, WorkspaceViewModelResources.SaveAndCloseCommand_Tooltip,
                        () => { SaveAndClose(); }, CanSave, null);
                    _saveAndCloseCommand.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.save_png.Find(FrozenContext));
                }
                return _saveAndCloseCommand;
            }
        }

        void IContextViewModel.Abort()
        {
            Close();
        }

        private IEnumerable<ErrorDescriptor> _errors;
        public IEnumerable<ErrorDescriptor> Errors
        {
            get
            {
                if (_errors == null)
                {
                    _errors = ValidationManager
                        .Errors
                        .Where(i => i.Source is DataObjectViewModel)
                        .Select(i => ViewModelFactory.CreateViewModel<ErrorDescriptor.Factory>().Invoke(DataContext, this, i));
                }
                return _errors;
            }
        }

        public void UpdateErrors()
        {
            ValidationManager.Validate();
        }

        void ValidationManager_Changed(object sender, EventArgs e)
        {
            _errors = null;
            OnPropertyChanged("Errors");
        }

        /// <summary>
        /// Returns a cached result.
        /// Called too often, will slow UI down if it would realy evaluate errors
        /// </summary>
        /// <returns></returns>
        public bool CanSave()
        {
            return ValidationManager.IsValid;
        }

        public event EventHandler Saving;
        private void OnSaving()
        {
            var temp = Saving;
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }

        public event EventHandler Saved;
        private void OnSaved()
        {
            var temp = Saved;
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }

        public bool Save()
        {
            UpdateErrors();
            if (ValidationManager.IsValid)
            {
                try
                {
                    OnSaving();
                    DataContext.SubmitChanges();
                    OnSaved();

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
                    var tmp = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>()
                        .Invoke(DataContext, this, WorkspaceViewModelResources.AbortCommand_Name, WorkspaceViewModelResources.AbortCommand_Tooltip,
                        Close, null, null);
                    tmp.RequestedKind = NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_CommandLinkKind.Find(FrozenContext);

                    _AbortCommand = tmp;
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
                {
                    _verifyCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                            DataContext,
                            this,
                            WorkspaceViewModelResources.VerifyContextCommand_Name,
                            WorkspaceViewModelResources.VerifyContextCommand_Tootlip,
                            ShowVerificationResults,
                            null, null);
                    _verifyCommand.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.ok_png.Find(FrozenContext));
                }
                return _verifyCommand;
            }
        }

        public void ShowVerificationResults()
        {
            UpdateErrors();
            if (!ValidationManager.IsValid)
            {
                ShowErrors = true;
            }
            else
            {
                ShowErrors = false;
            }
        }

        private bool _showErrors = false;
        public bool ShowErrors
        {
            get
            {
                return _showErrors;
            }
            set
            {
                if (_showErrors != value)
                {
                    _showErrors = value;
                    OnPropertyChanged("ShowErrors");
                }
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

        #region Model Management

        public DataObjectViewModel ShowObject(IDataObject obj, ControlKind requestedKind = null, bool activate = true)
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

            if (activate)
            {
                SelectedItem = vm;
            }
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

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ViewModelFactory.OnIMultipleInstancesManagerDisposed(DataContext, this);
            }
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

        public virtual async Task<bool> OnDrop(object data)
        {
            if (data is IDataObject[])
            {
                var lst = (IDataObject[])data;
                foreach (var obj in lst)
                {
                    ShowObject(obj);
                }

                return true;
            }
            if (data is string[])
            {
                var files = (string[])data;
                var objects = new List<IDataObject>();
                foreach (var file in files)
                {
                    try
                    {
                        var xml = new System.Xml.XmlDocument();
                        xml.Load(file);
                        if (xml.DocumentElement.LocalName == "ZetboxPackaging")
                        {
                            objects.AddRange((await Zetbox.App.Packaging.Importer.LoadFromXml(DataContext, file)).OfType<IDataObject>());
                        }
                    }
                    catch (Exception ex)
                    {
                        // not an xml...
                        Zetbox.API.Utils.Logging.Client.Error("Unable to import file.", ex);
                    }
                }

                if (objects.Count > 0)
                {
                    foreach (var obj in objects)
                    {
                        ShowObject(obj, activate: false);
                    }

                    ViewModelFactory.CreateDelayedTask(this, () =>
                    {
                        this.SelectedItem = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, this, objects.First());
                    }).Trigger();
                }
                return true;
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

    public class ErrorDescriptor : ViewModel
    {
        public new delegate ErrorDescriptor Factory(IZetboxContext dataCtx, WorkspaceViewModel parent, ValidationError error);

        private readonly ValidationError _error;
        private readonly WorkspaceViewModel _workspace;

        public ErrorDescriptor(IViewModelDependencies dependencies, IZetboxContext dataCtx, WorkspaceViewModel parent, ValidationError error)
            : base(dependencies, dataCtx, parent)
        {
            this._error = error;
            this._workspace = parent;
        }

        public ViewModel Item { get { return _error.Source; } }
        public ValidationError Error { get { return _error; } }

        public IEnumerable<string> Errors { get { return _error.Errors; } }
        public IEnumerable<ErrorDescriptor> Children
        {
            get
            {
                return _error
                    .Children
                    .Select(i => ViewModelFactory.CreateViewModel<ErrorDescriptor.Factory>().Invoke(DataContext, _workspace, i));
            }
        }

        private ICommandViewModel _GotoObjectCommand = null;
        public ICommandViewModel GotoObjectCommand
        {
            get
            {
                if (_GotoObjectCommand == null)
                {
                    _GotoObjectCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        null,
                        WorkspaceViewModelResources.GotoObjectCommand_Name,
                        WorkspaceViewModelResources.GotoObjectCommand_Tooltip,
                        GotoObject, CanExecGotoObject, null);
                }
                return _GotoObjectCommand;
            }
        }

        public bool CanExecGotoObject()
        {
            return Item is DataObjectViewModel || Item is BaseValueViewModel;
        }

        public void GotoObject()
        {
            if (CanExecGotoObject())
            {
                var item = Item;
                if (item is DataObjectViewModel)
                {
                    ViewModelFactory.ShowModel(item, true);
                }
                if (item is BaseValueViewModel)
                {
                    var objVmdl = item.Parent as DataObjectViewModel;
                    if (objVmdl != null)
                    {
                        ViewModelFactory.ShowModel(objVmdl, true);
                        var grp = objVmdl.PropertyGroups.FirstOrDefault(i => i.PropertyModels.Contains(item));
                        if (grp != null)
                        {
                            objVmdl.SelectedPropertyGroup = grp;
                        }
                    }
                }
            }
        }

        public override string Name
        {
            get { return Item.IfNotNull(i => i.Name); }
        }
    }
}
