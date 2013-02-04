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

    public interface IActivateCommandParameter : INotifyPropertyChanged
    {
        bool IsInlineEditable { get; }
    }

    public abstract class ActivateDataObjectCommand : CommandViewModel
    {
        protected bool UseSeparateContext { get; private set; }

        private IActivateCommandParameter Parameter { get { return Parent as IActivateCommandParameter; } }

        protected bool IsInlineEditable { get { return Parameter == null ? false : Parameter.IsInlineEditable; } }
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

        public ActivateDataObjectCommand(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, string label, string tooltip,
            bool useSeparateContext)
            : base(appCtx, dataCtx, parent, label, tooltip)
        {
            this.UseSeparateContext = useSeparateContext;
        }

        public static void ActivateItem(IViewModelFactory vmFactory, IZetboxContext dataCtx, IFrozenContext frozenCtx, ViewModel parent, DataObjectViewModel item, bool isInlineEditable)
        {
            if (vmFactory == null) throw new ArgumentNullException("vmFactory");
            if (dataCtx == null) throw new ArgumentNullException("dataCtx");
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");
            if (parent == null) throw new ArgumentNullException("parent");
            if (item == null) return;

            var type = item.Object.GetObjectClass(frozenCtx);

            if (type.IsSimpleObject)
            {
                if (isInlineEditable)
                {
                    // don't show simple objects, IF they are inline editable
                }
                else
                {
                    // Open in a Dialog
                    var dlg = vmFactory.CreateViewModel<SimpleDataObjectEditorTaskViewModel.Factory>().Invoke(dataCtx, parent, item);
                    vmFactory.ShowDialog(dlg);
                }
            }
            else
            {
                vmFactory.ShowModel(item, true);
            }
        }

        public static void ActivateForeignItems(IViewModelFactory vmFactory, IZetboxContext dataCtx, IEnumerable<IDataObject> items, ControlKind requestedWorkspaceKind, ControlKind requestedEditorKind, ItemsOpenedHandler callback)
        {
            if (vmFactory == null) throw new ArgumentNullException("vmFactory");
            if (dataCtx == null) throw new ArgumentNullException("dataCtx");
            if (items == null || items.Count() == 0) return;

            var newWorkspace = vmFactory.CreateViewModel<ObjectEditor.WorkspaceViewModel.Factory>().Invoke(dataCtx, null);

            vmFactory.ShowModel(newWorkspace, requestedWorkspaceKind, true);

            // ShowForeignObject may take a while
            vmFactory.CreateDelayedTask(newWorkspace, () =>
            {
                var openedForeignItems = items
                    .Select(i => newWorkspace.ShowForeignObject(i, requestedEditorKind))
                    .ToList(); // force evaluation, event might do it multiple times or not at all!

                if (callback != null)
                    callback(newWorkspace, openedForeignItems);

                newWorkspace.SelectedItem = newWorkspace.Items.FirstOrDefault();
            }).Trigger();
        }

        public delegate void ItemsOpenedHandler(ViewModel workspace, IEnumerable<DataObjectViewModel> items);
        public event ItemsOpenedHandler ItemsOpened;
        protected void OnItemsOpened(ViewModel workspace, IEnumerable<DataObjectViewModel> items)
        {
            ItemsOpenedHandler temp = ItemsOpened;
            if (temp != null)
            {
                temp(workspace, items);
            }
        }
    }

    public interface IOpenCommandParameter : IActivateCommandParameter
    {
        bool AllowOpen { get; }
        IEnumerable<ViewModel> SelectedItems { get; }
    }

    public class OpenDataObjectCommand : ActivateDataObjectCommand
    {
        public new delegate OpenDataObjectCommand Factory(IZetboxContext dataCtx, ViewModel parent, bool useSeparateContext);

        protected readonly Func<IZetboxContext> ctxFactory;
        protected IOpenCommandParameter Parameter { get { return Parent as IOpenCommandParameter; } }

        public OpenDataObjectCommand(IViewModelDependencies appCtx, Func<IZetboxContext> ctxFactory,
            IZetboxContext dataCtx, ViewModel parent, bool useSeparateContext
            )
            : base(appCtx, dataCtx, parent, CommonCommandsResources.OpenDataObjectCommand_Name, CommonCommandsResources.OpenDataObjectCommand_Tooltip, useSeparateContext)
        {
            this.ctxFactory = ctxFactory;
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

        private IEnumerable<DataObjectViewModel> GetViewModels()
        {
            return Parameter == null
                ? Enumerable.Empty<DataObjectViewModel>()
                : Parameter.SelectedItems.OfType<DataObjectViewModel>();
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
            else if (Parameter.SelectedItems == null
              || Parameter.SelectedItems.Count() == 0
              && Parameter.SelectedItems.Any(vm => !ViewModelFactory.CanShowModel(vm)))
            {
                Reason = CommonCommandsResources.DataObjectCommand_NothingSelected;
                return false;
            }
            Reason = string.Empty;
            return true;
        }

        protected override void DoExecute(object data)
        {
            var vModels = GetViewModels().ToList();
            if (UseSeparateContext)
            {
                var newCtx = ctxFactory();
                ActivateForeignItems(ViewModelFactory, newCtx, vModels.Select(dovm => dovm.Object), RequestedWorkspaceKind, RequestedEditorKind, OnItemsOpened);
            }
            else
            {
                foreach (var vm in vModels)
                {
                    ActivateItem(ViewModelFactory, DataContext, FrozenContext, this, vm, IsInlineEditable);
                }
                OnItemsOpened(ViewModelFactory.GetWorkspace(DataContext), vModels);
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

    public interface IDeleteCommandParameter : INotifyPropertyChanged
    {
        bool IsReadOnly { get; }
        bool AllowDelete { get; }
        IEnumerable<ViewModel> SelectedItems { get; }
    }

    public class DeleteDataObjectCommand : CommandViewModel
    {
        public new delegate DeleteDataObjectCommand Factory(IZetboxContext dataCtx, ViewModel parent, bool useSeparateContext);

        protected IDeleteCommandParameter Parameter { get { return Parent as IDeleteCommandParameter; } }
        protected IRefreshCommandListener Listener { get { return Parent as IRefreshCommandListener; } }
        protected bool UseSeparateContext { get; private set; }

        private readonly Func<IZetboxContext> _ctxFactory;

        public DeleteDataObjectCommand(IViewModelDependencies appCtx,
            IZetboxContext dataCtx, ViewModel parent, bool useSeparateContext,
            Func<IZetboxContext> ctxFactory)
            : base(appCtx, dataCtx, parent, CommonCommandsResources.DeleteDataObjectCommand_Name, CommonCommandsResources.DeleteDataObjectCommand_Tooltip)
        {
            this.Parameter.PropertyChanged += OnParameterChanged;
            this.UseSeparateContext = useSeparateContext;
            this._ctxFactory = ctxFactory;
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

        private IEnumerable<DataObjectViewModel> GetViewModels()
        {
            return Parameter.SelectedItems.OfType<DataObjectViewModel>();
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
            else if (Parameter.IsReadOnly || DataContext.IsReadonly)
            {
                Reason = CommonCommandsResources.DataObjectCommand_IsReadOnly;
                return false;
            }
            else if (!Parameter.AllowDelete)
            {
                Reason = CommonCommandsResources.DataObjectCommand_NotAllowed;
                return false;
            }
            else if (Parameter.SelectedItems == null)
            {
                Reason = CommonCommandsResources.DataObjectCommand_NothingSelected;
                return false;
            }

            var itemsCount = Parameter.SelectedItems.OfType<object>().Count();

            if (itemsCount == 0)
            {
                Reason = CommonCommandsResources.DataObjectCommand_NothingSelected;
                return false;
            }

            var dataObjectsCount = GetViewModels().Count();

            if (dataObjectsCount != itemsCount || !GetViewModels().All(dovm => dovm.Object.CurrentAccessRights.HasDeleteRights()))
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
                using (var ctx = _ctxFactory())
                {
                    // make local copy to avoid stumbling over changing lists while iterating over them
                    foreach (var item in GetViewModels().ToList())
                    {
                        var other = item.Object;
                        var here = ctx.Find(ctx.GetInterfaceType(other), other.ID);
                        ctx.Delete(here);
                    }
                    ctx.SubmitChanges();
                }
            }
            else
            {
                // make local copy to avoid stumbling over changing lists while iterating over them
                foreach (var item in GetViewModels().ToList())
                {
                    DataContext.Delete(item.Object);
                }
            }

            if (Listener != null) Listener.Refresh();
        }

        private void OnParameterChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsReadOnly" || e.PropertyName == "AllowDelete" || e.PropertyName == "SelectedItems")
            {
                OnCanExecuteChanged();
            }
        }
    }

    public interface INewCommandParameter : IActivateCommandParameter
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
        public new delegate NewDataObjectCommand Factory(IZetboxContext dataCtx, ViewModel parent, ObjectClass type, bool useSeparateContext);

        public static void ChooseObjectClass(IViewModelFactory vmFactory, IZetboxContext ctx, IFrozenContext frozenCtx, ViewModel parent, ObjectClass baseClass, Action<ObjectClass> createNewObjectAndNotify)
        {
            if (vmFactory == null) throw new ArgumentNullException("vmFactory");
            if (ctx == null) throw new ArgumentNullException("ctx");
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");
            if (parent == null) throw new ArgumentNullException("parent");
            if (baseClass == null) throw new ArgumentNullException("baseClass");
            if (createNewObjectAndNotify == null) throw new ArgumentNullException("createNewObjectAndNotify");

            var children = new List<ObjectClass>();
            if (baseClass.IsAbstract == false)
            {
                children.Add(baseClass);
            }
            baseClass.CollectChildClasses(children, false);

            if (children.Count == 1)
            {
                createNewObjectAndNotify(children.Single());
            }
            else
            {
                var lstMdl = vmFactory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(
                    ctx,
                    parent,
                    (ObjectClass)NamedObjects.Base.Classes.Zetbox.App.Base.ObjectClass.Find(frozenCtx),
                    () => children.AsQueryable(),
                    (chosen) =>
                    {
                        if (chosen != null)
                        {
                            createNewObjectAndNotify((ObjectClass)chosen.First().Object);
                        }
                    },
                    null);
                lstMdl.ListViewModel.ShowCommands = false;

                vmFactory.ShowDialog(lstMdl);
            }
        }

        protected readonly Func<IZetboxContext> ctxFactory;
        protected INewCommandParameter Parameter { get { return Parent as INewCommandParameter; } }
        protected IRefreshCommandListener Listener { get { return Parent as IRefreshCommandListener; } }
        protected ObjectClass Type { get; private set; }

        public NewDataObjectCommand(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, ObjectClass type, bool useSeparateContext,
            Func<IZetboxContext> ctxFactory)
            : base(appCtx, dataCtx, parent, CommonCommandsResources.NewDataObjectCommand_Name, CommonCommandsResources.NewDataObjectCommand_Tooltip, useSeparateContext)
        {
            if (this.Parameter != null)
                this.Parameter.PropertyChanged += OnParameterChanged;
            this.ctxFactory = ctxFactory;
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
            if (!UseSeparateContext && DataContext.IsReadonly)
            {
                Reason = CommonCommandsResources.DataObjectCommand_IsReadOnly;
                return false;
            }
            else if (Parameter == null)
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

            Reason = string.Empty;
            return true;
        }

        protected override void DoExecute(object data)
        {
            ObjectClass baseclass = Type;

            var children = new List<ObjectClass>();
            if (baseclass.IsAbstract == false)
            {
                children.Add(baseclass);
            }
            baseclass.CollectChildClasses(children, false);

            if (children.Count == 1)
            {
                CreateItem(Type);
            }
            else
            {
                var lstMdl = ViewModelFactory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(
                        DataContext, this,
                        typeof(ObjectClass).GetObjectClass(FrozenContext),
                        () => children.AsQueryable(),
                        (chosen) =>
                        {
                            if (chosen != null)
                            {
                                CreateItem((ObjectClass)chosen.First().Object);
                            }
                        }, null);
                lstMdl.ListViewModel.ShowCommands = false;

                ViewModelFactory.ShowModel(lstMdl, true);
            }
        }

        private void CreateItem(ObjectClass dtType)
        {
            var newCtx = UseSeparateContext ? ctxFactory() : DataContext;
            var newObj = newCtx.Create(DataContext.GetInterfaceType(dtType.GetDataType()));

            OnObjectCreated(newObj);

            if (UseSeparateContext)
            {
                ActivateForeignItems(ViewModelFactory, newCtx, new[] { newObj }, RequestedWorkspaceKind, RequestedEditorKind, OnItemsOpened);
            }
            else
            {
                var mdl = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), newObj);

                ActivateItem(ViewModelFactory, newCtx, FrozenContext, this, mdl, IsInlineEditable);

                if (Listener != null)
                {
                    Listener.Refresh();
                }
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
            newWorkspace.ShowForeignObject(newObjClass);
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
            if (CurrentIdentity == null || !CurrentIdentity.IsAdmininistrator())
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
}
