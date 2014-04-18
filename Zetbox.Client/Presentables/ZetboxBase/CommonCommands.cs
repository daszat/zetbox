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

namespace Zetbox.Client.Presentables.ZetboxBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using ObjectEditorWorkspace = Zetbox.Client.Presentables.ObjectEditor.WorkspaceViewModel;

    public class ItemsOpeningEventArgs : EventArgs
    {
        public ItemsOpeningEventArgs(IZetboxContext ctx, ObjectEditor.WorkspaceViewModel workspace, IEnumerable<ViewModel> items)
        {
            DataContext = ctx;
            Workspace = workspace;
            Items = items.ToList();
        }

        /// <summary>
        /// The current data context.
        /// </summary>
        public IZetboxContext DataContext { get; private set; }
        /// <summary>
        /// The target workspace.
        /// </summary>
        public ObjectEditor.WorkspaceViewModel Workspace { get; private set; }
        /// <summary>
        /// Modify the Items collection to modify the actually openend items.
        /// </summary>
        public List<ViewModel> Items { get; set; }
        /// <summary>
        /// Set this to true to suppress running the default ItemsOpen implementation.
        /// </summary>
        public bool Handled { get; set; }
    }

    public class ItemsOpenedEventArgs : EventArgs
    {
        public ItemsOpenedEventArgs(IEnumerable<ViewModel> items)
        {
            Items = items;
        }
        public IEnumerable<ViewModel> Items { get; private set; }
    }

    public abstract class ActivateDataObjectCommand : CommandViewModel
    {
        protected readonly Func<IZetboxContext> ctxFactory;

        protected bool UseSeparateContext { get { return !(ViewModelFactory.GetWorkspace(DataContext) is IContextViewModel); } }

        protected IRequestedEditorKinds RequestedKinds { get { return Parent as IRequestedEditorKinds; } }

        protected ControlKind RequestedEditorKind
        {
            get
            {
                return RequestedKinds != null
                    ? RequestedKinds.RequestedEditorKind
                    : null;
            }
        }
        protected ControlKind RequestedWorkspaceKind
        {
            get
            {
                return RequestedKinds != null
                    ? RequestedKinds.RequestedWorkspaceKind
                    : null;
            }
        }

        public ActivateDataObjectCommand(IViewModelDependencies appCtx, Func<IZetboxContext> ctxFactory, IZetboxContext dataCtx, ViewModel parent, string label, string tooltip)
            : base(appCtx, dataCtx, parent, label, tooltip)
        {
            if (ctxFactory == null) throw new ArgumentNullException("ctxFactory");

            this.ctxFactory = ctxFactory;
        }

        protected void ActivateItem(IEnumerable<ViewModel> items)
        {
            if (items == null || items.Count() == 0) return;

            var openingArgs = new ItemsOpeningEventArgs(DataContext, ViewModelFactory.GetWorkspace(DataContext) as ObjectEditorWorkspace, items);

            OnItemsOpening(this, openingArgs);

            // abort handling the event if it is already handled
            if (openingArgs.Handled == true)
                return;

            foreach (var item in openingArgs.Items)
            {
                var dovm = item as DataObjectViewModel;
                if (dovm != null && dovm.Object.GetObjectClass(FrozenContext).IsSimpleObject)
                {
                    // Open in a Dialog
                    var dlg = ViewModelFactory.CreateViewModel<SimpleDataObjectEditorTaskViewModel.Factory>().Invoke(DataContext, Parent, item);
                    ViewModelFactory.ShowDialog(dlg);
                }
                else
                {
                    ViewModelFactory.ShowModel(item, true);
                }
            }

            OnItemsOpened(this, new ItemsOpenedEventArgs(openingArgs.Items));
        }

        protected void ActivateForeignItems(IZetboxContext newCtx, IEnumerable<IDataObject> items)
        {
            if (newCtx == null) throw new ArgumentNullException("newCtx");
            if (items == null || items.Count() == 0) return;

            var newWorkspace = ViewModelFactory.CreateViewModel<ObjectEditor.WorkspaceViewModel.Factory>().Invoke(newCtx, null);

            ViewModelFactory.ShowModel(newWorkspace, RequestedWorkspaceKind, true);

            // ShowForeignObject may take a while
            ViewModelFactory.CreateDelayedTask(newWorkspace, () =>
            {
                var newViewModels = items.Select(i => DataObjectViewModel.Fetch(ViewModelFactory, newCtx, this, newCtx.Find(DataContext.GetInterfaceType(i), i.ID)));
                var openingArgs = new ItemsOpeningEventArgs(newCtx, newWorkspace, newViewModels);

                OnItemsOpening(newWorkspace, openingArgs);

                // abort handling the event if it is already handled
                if (openingArgs.Handled == true)
                    return;

                foreach (var newItem in openingArgs.Items)
                {
                    newWorkspace.ShowModel(newItem);
                }

                OnItemsOpened(newWorkspace, new ItemsOpenedEventArgs(openingArgs.Items));

                newWorkspace.SelectedItem = newWorkspace.Items.FirstOrDefault();
            }).Trigger();
        }


        public event EventHandler<ItemsOpeningEventArgs> ItemsOpening;
        protected void OnItemsOpening(object workspace, ItemsOpeningEventArgs args)
        {
            var temp = ItemsOpening;
            if (temp != null)
            {
                temp(workspace, args);
            }
        }

        public event EventHandler<ItemsOpenedEventArgs> ItemsOpened;
        protected void OnItemsOpened(object workspace, ItemsOpenedEventArgs args)
        {
            var temp = ItemsOpened;
            if (temp != null)
            {
                temp(workspace, args);
            }
        }
    }

    public interface ICommandParameter : INotifyPropertyChanged
    {
        IEnumerable<ViewModel> SelectedItems { get; }
    }

    public interface IOpenCommandParameter : ICommandParameter
    {
        bool AllowOpen { get; }
    }

    public class OpenDataObjectCommand : ActivateDataObjectCommand
    {
        public new delegate OpenDataObjectCommand Factory(IZetboxContext dataCtx, ViewModel parent);

        protected IEnumerable<ViewModel> SelectedItems { get { return ((ICommandParameter)Parent).SelectedItems; } }
        protected IOpenCommandParameter Parameter { get { return Parent as IOpenCommandParameter; } }

        public OpenDataObjectCommand(IViewModelDependencies appCtx, Func<IZetboxContext> ctxFactory,
            IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, ctxFactory, dataCtx, parent, CommonCommandsResources.OpenDataObjectCommand_Name, CommonCommandsResources.OpenDataObjectCommand_Tooltip)
        {
            if (!(parent is ICommandParameter)) throw new ArgumentOutOfRangeException("parent", "parent needs to implement ICommandParameter");

            if (Parameter != null)
                Parameter.PropertyChanged += OnParameterChanged;
        }

        public override System.Drawing.Image Icon
        {
            get
            {
                return base.Icon ?? (base.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.fileopen_png.Find(FrozenContext)));
            }
            set
            {
                base.Icon = value;
            }
        }

        public override bool CanExecute(object data)
        {
            if (Parameter == null)
            {
                Reason = CommonCommandsResources.DataObjectCommand_NothingSelected;
                return false;
            }
            else if (!Parameter.AllowOpen)
            {
                Reason = CommonCommandsResources.DataObjectCommand_NotAllowed;
                return false;
            }
            else if (SelectedItems == null
              || SelectedItems.Count() == 0)
            {
                Reason = CommonCommandsResources.DataObjectCommand_NothingSelected;
                return false;
            }
            else if (!UseSeparateContext && SelectedItems.Any(vm => !ViewModelFactory.CanShowModel(vm)))
            {
                Reason = CommonCommandsResources.OpenDataObjectCommand_SomeCanNotBeOpened;
                return false;
            }

            Reason = string.Empty;
            return true;
        }

        protected override void DoExecute(object data)
        {
            var vModels = SelectedItems.OfType<DataObjectViewModel>().ToList();
            if (UseSeparateContext)
            {
                ActivateForeignItems(ctxFactory(), vModels.Select(dovm => dovm.Object));
            }
            else
            {
                ActivateItem(vModels);
                OnItemsOpened(ViewModelFactory.GetWorkspace(DataContext), new ItemsOpenedEventArgs(vModels));
            }
        }

        private void OnParameterChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "AllowOpen" || e.PropertyName == "SelectedItems")
            {
                OnCanExecuteChanged();
            }
        }
    }

    public interface IDeleteCommandParameter : ICommandParameter, INotifyPropertyChanged
    {
        bool IsReadOnly { get; }
        bool AllowDelete { get; }
    }

    public class DeleteDataObjectCommand : CommandViewModel
    {
        public new delegate DeleteDataObjectCommand Factory(IZetboxContext dataCtx, ViewModel parent);

        protected IDeleteCommandParameter Parameter { get { return Parent as IDeleteCommandParameter; } }
        protected IRefreshCommandListener Listener { get { return Parent as IRefreshCommandListener; } }
        protected IEnumerable<ViewModel> SelectedItems { get { return ((ICommandParameter)Parent).SelectedItems; } }

        protected bool UseSeparateContext
        {
            get
            {
                return DataContext.IsReadonly;
            }
        }

        private readonly Func<IZetboxContext> _ctxFactory;
        private readonly IZetboxContextExceptionHandler _exceptionHandler;

        public DeleteDataObjectCommand(IViewModelDependencies appCtx,
            IZetboxContext dataCtx, ViewModel parent,
            Func<IZetboxContext> ctxFactory, IZetboxContextExceptionHandler exceptionHandler)
            : base(appCtx, dataCtx, parent, CommonCommandsResources.DeleteDataObjectCommand_Name, CommonCommandsResources.DeleteDataObjectCommand_Tooltip)
        {
            if (ctxFactory == null) throw new ArgumentNullException("ctxFactory");
            if (exceptionHandler == null) throw new ArgumentNullException("exceptionHandler");
            if (!(parent is ICommandParameter)) throw new ArgumentOutOfRangeException("parent", "parent needs to implement ICommandParameter");

            if (this.Parameter != null)
                this.Parameter.PropertyChanged += OnParameterChanged;

            this._ctxFactory = ctxFactory;
            this._exceptionHandler = exceptionHandler;
        }

        public override System.Drawing.Image Icon
        {
            get
            {
                return base.Icon ?? (base.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.delete_png.Find(FrozenContext)));
            }
            set
            {
                base.Icon = value;
            }
        }

        public override bool CanExecute(object data)
        {
            if (data != null)
            {
                Reason = string.Format(CommonCommandsResources.DataObjectCommand_ProgrammerError, data);
                return false;
            }
            else if (Parameter == null)
            {
                Reason = CommonCommandsResources.DataObjectCommand_NothingSelected;
                return false;
            }
            else if (Parameter.IsReadOnly)
            {
                Reason = CommonCommandsResources.DataObjectCommand_IsReadOnly;
                return false;
            }
            else if (!Parameter.AllowDelete)
            {
                Reason = CommonCommandsResources.DataObjectCommand_NotAllowed;
                return false;
            }
            else if (SelectedItems == null)
            {
                Reason = CommonCommandsResources.DataObjectCommand_NothingSelected;
                return false;
            }

            var itemsCount = SelectedItems.OfType<object>().Count();

            if (itemsCount == 0)
            {
                Reason = CommonCommandsResources.DataObjectCommand_NothingSelected;
                return false;
            }

            var dataObjects = SelectedItems.OfType<DataObjectViewModel>().ToList();

            if (dataObjects.Count != itemsCount || !dataObjects.All(dovm => dovm.Object.CurrentAccessRights.HasDeleteRights()))
            {
                Reason = CommonCommandsResources.DeleteDataObjectCommand_SomeMayNotBeDeleted;
                return false;
            }

            // whew!
            Reason = string.Empty;
            return true;
        }

        protected override void DoExecute(object data)
        {
            if (UseSeparateContext && !ViewModelFactory.GetDecisionFromUser(CommonCommandsResources.DeleteDataObjectCommand_Confirm, CommonCommandsResources.DeleteDataObjectCommand_Confirm_Title))
            {
                return;
            }

            if (UseSeparateContext)
            {
                try
                {
                    using (var ctx = _ctxFactory())
                    {
                        // make local copy to avoid stumbling over changing lists while iterating over them
                        foreach (var item in SelectedItems.Cast<DataObjectViewModel>().ToList())
                        {
                            var other = item.Object;
                            var here = ctx.Find(ctx.GetInterfaceType(other), other.ID);
                            DoDelete(ctx, here);
                        }
                        ctx.SubmitChanges();
                    }
                }
                catch (Exception ex)
                {
                    if (!_exceptionHandler.Show(DataContext, ex))
                    {
                        throw;
                    }
                }
            }
            else
            {
                // make local copy to avoid stumbling over changing lists while iterating over them
                foreach (var item in SelectedItems.Cast<DataObjectViewModel>().ToList())
                {
                    DoDelete(DataContext, item.Object);
                }
            }

            if (Listener != null) Listener.Refresh();
        }

        private void DoDelete(IZetboxContext ctx, IDataObject obj)
        {
            var deactivatable = obj as IDeactivatable;
            if (deactivatable == null
                || (ctx.IsElevatedMode && ViewModelFactory.GetDecisionFromUser(CommonCommandsResources.DeleteDataObjectCommand_ElevatedDeleteDeactivated, CommonCommandsResources.DeleteDataObjectCommand_ElevatedDeleteDeactivated_Title)))
            {
                ctx.Delete(obj);
            }
            else
            {
                deactivatable.IsDeactivated = true;
            }
        }

        private void OnParameterChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsReadOnly" || e.PropertyName == "AllowDelete" || e.PropertyName == "SelectedItems")
            {
                OnCanExecuteChanged();
            }
        }
    }

    public interface INewCommandParameter : INotifyPropertyChanged
    {
        bool IsReadOnly { get; }
        bool AllowAddNew { get; }
    }

    public interface IRequestedEditorKinds
    {
        ControlKind RequestedEditorKind { get; }
        ControlKind RequestedWorkspaceKind { get; }
    }

    public class NewDataObjectCommand : ActivateDataObjectCommand
    {
        public new delegate NewDataObjectCommand Factory(IZetboxContext dataCtx, ViewModel parent, ObjectClass type);

        protected INewCommandParameter Parameter { get { return Parent as INewCommandParameter; } }
        protected IRefreshCommandListener Listener { get { return Parent as IRefreshCommandListener; } }
        protected ObjectClass Type { get; private set; }

        public NewDataObjectCommand(IViewModelDependencies appCtx, Func<IZetboxContext> ctxFactory,
            IZetboxContext dataCtx, ViewModel parent, ObjectClass type)
            : base(appCtx, ctxFactory, dataCtx, parent, CommonCommandsResources.NewDataObjectCommand_Name, CommonCommandsResources.NewDataObjectCommand_Tooltip)
        {
            if (this.Parameter != null)
                this.Parameter.PropertyChanged += OnParameterChanged;
            this.Type = type;
        }

        public override System.Drawing.Image Icon
        {
            get
            {
                return base.Icon ?? (base.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.new_png.Find(FrozenContext)));
            }
            set
            {
                base.Icon = value;
            }
        }

        public override bool CanExecute(object data)
        {
            if (Parameter == null)
            {
                Reason = CommonCommandsResources.DataObjectCommand_NothingSelected;
                return false;
            }
            else if (!Parameter.AllowAddNew)
            {
                Reason = CommonCommandsResources.DataObjectCommand_NotAllowed;
                return false;
            }
            else if (Parameter.IsReadOnly)
            {
                Reason = CommonCommandsResources.DataObjectCommand_IsReadOnly;
                return false;
            }
            else if (Type.HasAccessControlList() && !Type.GetGroupAccessRights(CurrentIdentity).HasCreateRights())
            {
                Reason = CommonCommandsResources.DataObjectCommand_NotAllowed;
                return false;
            }

            Reason = string.Empty;
            return true;
        }

        protected override void DoExecute(object data)
        {
            var allCandidates = new List<ObjectClass>();
            allCandidates.Add(Type);
            Type.CollectChildClasses(allCandidates, false);
            var candidates = allCandidates.Where(i => i.IsAbstract == false && i.IsCreatedProgrammatically == false).ToList();
            if (candidates.Count == 0)
            {
                // Fallback
                candidates = allCandidates.Where(i => i.IsAbstract == false).ToList();
            }

            if (candidates.Count == 1)
            {
                CreateItem(candidates.First());
            }
            else
            {
                var lstMdl = ViewModelFactory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(
                        DataContext, this,
                        typeof(ObjectClass).GetObjectClass(FrozenContext),
                        () => candidates.AsQueryable(),
                        (chosen) =>
                        {
                            if (chosen != null)
                            {
                                CreateItem((ObjectClass)chosen.First().Object);
                            }
                        }, null);
                lstMdl.SelectionType = Type.Name;
                lstMdl.RequestedKind = NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_DataObjectSelectionTaskSimpleKind.Find(FrozenContext);

                ViewModelFactory.ShowModel(lstMdl, true);
            }
        }

        private void CreateItem(ObjectClass dtType)
        {
            if (UseSeparateContext)
            {
                var newCtx = ctxFactory();
                var newObj = newCtx.Create(DataContext.GetInterfaceType(dtType.GetDataType()));
                OnObjectCreated(newObj);
                ActivateForeignItems(newCtx, new[] { newObj });
            }
            else
            {
                var newObj = DataContext.Create(DataContext.GetInterfaceType(dtType.GetDataType()));
                OnObjectCreated(newObj);

                var mdl = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), newObj);
                OnLocalModelCreated(mdl);

                ActivateItem(new[] { mdl });

                OnRefresh();
            }
        }

        protected virtual void OnRefresh()
        {
            if (Listener != null)
            {
                Listener.Refresh();
            }
        }

        public delegate void ObjectCreatedHandler(IDataObject obj);
        public event ObjectCreatedHandler ObjectCreated;
        protected void OnObjectCreated(IDataObject obj)
        {
            ObjectCreatedHandler temp = ObjectCreated;
            if (temp != null)
            {
                temp(obj);
            }
        }

        public delegate void LocalModelCreatedHandler(DataObjectViewModel vm);
        public event LocalModelCreatedHandler LocalModelCreated;
        protected void OnLocalModelCreated(DataObjectViewModel vm)
        {
            LocalModelCreatedHandler temp = LocalModelCreated;
            if (temp != null)
            {
                temp(vm);
            }
        }

        private void OnParameterChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsReadOnly" || e.PropertyName == "AllowAddNew" || e.PropertyName == "IsInlineEditable")
            {
                OnCanExecuteChanged();
            }
        }
    }

    public class EditDataObjectClassCommand : CommandViewModel
    {
        public new delegate EditDataObjectClassCommand Factory(IZetboxContext dataCtx, ViewModel parent, DataType type);

        protected readonly Func<IZetboxContext> ctxFactory;
        protected DataType Type { get; private set; }

        public EditDataObjectClassCommand(IViewModelDependencies appCtx,
            IZetboxContext dataCtx, ViewModel parent, DataType type,
            Func<IZetboxContext> ctxFactory)
            : base(appCtx, dataCtx, parent, CommonCommandsResources.EditDataObjectClassCommand_Name, CommonCommandsResources.EditDataObjectClassCommand_Tooltip)
        {
            this.Type = type;
            this.ctxFactory = ctxFactory;
        }

        public override bool CanExecute(object data)
        {
            return true;
        }

        protected override void DoExecute(object data)
        {
            var newCtx = ctxFactory();
            var newObjClass = newCtx.FindPersistenceObject<DataType>(this.Type.ExportGuid);
            var newWorkspace = ViewModelFactory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(newCtx, null);
            newWorkspace.ShowObject(newObjClass);
            ViewModelFactory.ShowModel(newWorkspace, true);
        }
    }

    public interface IRefreshCommandListener
    {
        void Refresh();
    }

    public class RefreshCommand : CommandViewModel
    {
        public new delegate RefreshCommand Factory(IZetboxContext dataCtx, ViewModel parent);

        protected IRefreshCommandListener Listener { get { return Parent as IRefreshCommandListener; } }

        public RefreshCommand(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent, CommonCommandsResources.RefreshCommand_Name, CommonCommandsResources.RefreshCommand_Tooltip)
        {
        }

        public override System.Drawing.Image Icon
        {
            get
            {
                return base.Icon ?? (base.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.reload_png.Find(FrozenContext)));
            }
            set
            {
                base.Icon = value;
            }
        }

        public class CanRefreshEventArgs : EventArgs
        {
            public CanRefreshEventArgs()
            {
                CanRefresh = true;
            }
            public bool CanRefresh { get; set; }
            public string CanRefreshReason { get; set; }
        }
        public event EventHandler<CanRefreshEventArgs> CanRefresh;
        public override bool CanExecute(object data)
        {
            if (Listener == null)
            {
                Reason = CommonCommandsResources.DataObjectCommand_NothingSelected;
                return false;
            }

            var temp = CanRefresh;
            if (temp == null)
            {
                Reason = string.Empty;
                return true;
            }

            var args = new CanRefreshEventArgs();
            temp(this, args);
            Reason = args.CanRefreshReason;
            return args.CanRefresh;
        }

        protected override void DoExecute(object data)
        {
            if (CanExecute(data))
            {
                Listener.Refresh();
            }
        }
    }

    public class ReportProblemCommand : CommandViewModel
    {
        public new delegate ReportProblemCommand Factory(IZetboxContext dataCtx, ViewModel parent);

        private readonly IProblemReporter _reporter;
        private readonly IScreenshotTool _screenShot;

        public ReportProblemCommand(IViewModelDependencies appCtx, IProblemReporter reporter, IScreenshotTool screenShot, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent, CommonCommandsResources.ReportProblemCommand_Name, CommonCommandsResources.ReportProblemCommand_Tooltip)
        {
            if (reporter == null) throw new ArgumentNullException("reporter");
            if (screenShot == null) throw new ArgumentNullException("screenShot");

            this._reporter = reporter;
            this._screenShot = screenShot;
        }

        public override System.Drawing.Image Icon
        {
            get
            {
                return base.Icon ?? (base.Icon = IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.info_png.Find(FrozenContext)));
            }
            set
            {
                base.Icon = value;
            }
        }

        public override bool UseDelayedTask
        {
            get
            {
                return false;
            }
            set
            {
                // Ignore
            }
        }

        public override bool CanExecute(object data)
        {
            return true;
        }

        protected override void DoExecute(object data)
        {
            if (CanExecute(data))
            {
                try
                {
                    _reporter.Report(CommonCommandsResources.ReportProblemCommand_MessageTemplate,
                        CommonCommandsResources.ReportProblemCommand_DescriptionTemplate,
                        _screenShot.GetScreenshot(),
                        null);
                }
                catch (Exception ex)
                {
                    // The Reporter has a problem...
                    ViewModelFactory.ShowMessage(string.Format(CommonCommandsResources.ReportProblemCommand_Error, ex.Message), CommonCommandsResources.ReportProblemCommand_Error_Title);
                }
            }
        }
    }

    public class ElevatedModeCommand : CommandViewModel
    {
        public new delegate ElevatedModeCommand Factory(IZetboxContext dataCtx, ViewModel parent);

        public ElevatedModeCommand(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent, CommonCommandsResources.ElevatedModeCommand_Name, CommonCommandsResources.ElevatedModeCommand_Tooltip)
        {

        }

        public override System.Drawing.Image Icon
        {
            get
            {
                return base.Icon ?? (base.Icon = IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.otheroptions_ico.Find(FrozenContext)));
            }
            set
            {
                base.Icon = value;
            }
        }

        public bool Show
        {
            get
            {
                return CanExecute(null);
            }
        }

        public bool IsElevated
        {
            get
            {
                return DataContext.IsElevatedMode;
            }
        }

        public override bool CanExecute(object data)
        {
            if (CurrentIdentity == null || !CurrentIdentity.IsAdministrator())
            {
                this.Reason = CommonCommandsResources.ElevatedModeCommand_Error;
                return false;
            }

            Reason = string.Empty;
            return true;
        }

        protected override void DoExecute(object data)
        {
            if (CanExecute(data))
            {
                DataContext.SetElevatedMode(!DataContext.IsElevatedMode);
                OnPropertyChanged("IsElevated");
            }
        }
    }

    public class ObjectBrowserCommand : CommandViewModel
    {
        public new delegate ObjectBrowserCommand Factory(IZetboxContext dataCtx, ViewModel parent);

        private Func<IZetboxContext> _ctxFactory;

        public ObjectBrowserCommand(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, Func<IZetboxContext> ctxFactory)
            : base(appCtx, dataCtx, parent, CommonCommandsResources.ObjectBrowserCommand_Name, CommonCommandsResources.ObjectBrowserCommand_Tooltip)
        {
            if (ctxFactory == null) throw new ArgumentNullException("ctxFactory");
            _ctxFactory = ctxFactory;
        }

        public override System.Drawing.Image Icon
        {
            get
            {
                return base.Icon ?? (base.Icon = IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.propertiesORoptions_ico.Find(FrozenContext)));
            }
            set
            {
                base.Icon = value;
            }
        }

        public bool Show
        {
            get
            {
                return CanExecute(null);
            }
        }

        public override bool CanExecute(object data)
        {
            if (CurrentIdentity == null || !CurrentIdentity.IsAdministrator())
            {
                this.Reason = CommonCommandsResources.ElevatedModeCommand_Error;
                return false;
            }

            Reason = string.Empty;
            return true;
        }

        protected override void DoExecute(object data)
        {
            if (CanExecute(data))
            {
                var ws = ViewModelFactory.CreateViewModel<Zetbox.Client.Presentables.ObjectBrowser.WorkspaceViewModel.Factory>().Invoke(_ctxFactory(), null);
                ControlKind launcher = Zetbox.NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_LauncherKind.Find(FrozenContext);
                ViewModelFactory.ShowModel(ws, launcher, true);
            }
        }
    }
}
