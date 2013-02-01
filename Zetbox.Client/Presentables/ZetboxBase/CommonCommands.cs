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

    public class OpenDataObjectCommand : ItemCommandViewModel<DataObjectViewModel>
    {
        public new delegate OpenDataObjectCommand Factory(IZetboxContext dataCtx, ViewModel parent, ControlKind reqWorkspaceKind, ControlKind reqEditorKind);

        protected readonly Func<IZetboxContext> ctxFactory;

        public OpenDataObjectCommand(IViewModelDependencies appCtx, Func<IZetboxContext> ctxFactory,
            IZetboxContext dataCtx, ViewModel parent, ControlKind reqWorkspaceKind, ControlKind reqEditorKind
            )
            : base(appCtx, dataCtx, parent, CommonCommandsResources.OpenDataObjectCommand_Name, CommonCommandsResources.OpenDataObjectCommand_Tooltip)
        {
            this.ctxFactory = ctxFactory;
            this._requestedWorkspaceKind = reqWorkspaceKind;
            this._requestedEditorKind = reqEditorKind;
        }

        private ControlKind _requestedEditorKind;
        public ControlKind RequestedEditorKind
        {
            get
            {
                return _requestedEditorKind;
            }
            set
            {
                if (_requestedEditorKind != value)
                {
                    _requestedEditorKind = value;
                    OnPropertyChanged("RequestedEditorKind");
                }
            }
        }

        private ControlKind _requestedWorkspaceKind;
        public ControlKind RequestedWorkspaceKind
        {
            get
            {
                return _requestedWorkspaceKind;
            }
            set
            {
                if (_requestedWorkspaceKind != value)
                {
                    _requestedWorkspaceKind = value;
                    OnPropertyChanged("RequestedWorkspaceKind");
                }
            }
        }

        public delegate void ModelCreatedEventHandler(DataObjectViewModel mdl);
        public event ModelCreatedEventHandler ModelCreated;

        protected override void DoExecute(IEnumerable<DataObjectViewModel> data)
        {
            var newWorkspace = ViewModelFactory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(ctxFactory(), null);
            foreach (var item in data)
            {
                var newMdl = newWorkspace.ShowForeignModel(item, RequestedEditorKind);
                ModelCreatedEventHandler temp = ModelCreated;
                if (temp != null)
                {
                    temp(newMdl);
                }
            }
            ViewModelFactory.ShowModel(newWorkspace, RequestedWorkspaceKind, true);
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
        public new delegate DeleteDataObjectCommand Factory(IZetboxContext dataCtx, ViewModel parent, IDeleteCommandParameter parameter, IRefreshCommandListener listener, bool useSeparateContext);

        protected IDeleteCommandParameter Parameter { get; private set; }
        protected IRefreshCommandListener Listener { get; private set; }
        protected bool UseSeparateContext { get; private set; }

        private readonly Func<IZetboxContext> _ctxFactory;

        public DeleteDataObjectCommand(IViewModelDependencies appCtx,
            IZetboxContext dataCtx, ViewModel parent, IDeleteCommandParameter parameter, IRefreshCommandListener listener, bool useSeparateContext,
            Func<IZetboxContext> ctxFactory)
            : base(appCtx, dataCtx, parent, CommonCommandsResources.DeleteDataObjectCommand_Name, CommonCommandsResources.DeleteDataObjectCommand_Tooltip)
        {
            this.Parameter = parameter;
            this.Parameter.PropertyChanged += OnParameterChanged;
            this.Listener = listener;
            this.UseSeparateContext = useSeparateContext;
            this._ctxFactory = ctxFactory;
        }

        private System.Drawing.Image _icon;
        public override System.Drawing.Image Icon
        {
            get
            {
                return base.Icon ?? _icon ?? (_icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.delete_png.Find(FrozenContext)));
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
            // TODO: re-enable after converting all commands
            //if (data != null)
            //{
            //    Reason = string.Format(CommonCommandsResources.DeleteDataObjectCommand_ProgrammerError, data);
            //    return false;
            //}
            //else

            if (Parameter.IsReadOnly || DataContext.IsReadonly)
            {
                Reason = CommonCommandsResources.DeleteDataObjectCommand_IsReadOnly;
                return false;
            }
            else if (!Parameter.AllowDelete)
            {
                Reason = CommonCommandsResources.DeleteDataObjectCommand_NotAllowed;
                return false;
            }
            else if (Parameter.SelectedItems == null)
            {
                Reason = CommonCommandsResources.DeleteDataObjectCommand_NothingSelected;
                return false;
            }

            var itemsCount = Parameter.SelectedItems.OfType<object>().Count();

            if (itemsCount == 0)
            {
                Reason = CommonCommandsResources.DeleteDataObjectCommand_NothingSelected;
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
            if (e.PropertyName == "IsReadonly" || e.PropertyName == "AllowDelete" || e.PropertyName == "SelectedItems")
            {
                OnCanExecuteChanged();
            }
        }
    }

    public class NewDataObjectCommand : CommandViewModel
    {
        public new delegate NewDataObjectCommand Factory(IZetboxContext dataCtx, ViewModel parent, ObjectClass type, ControlKind reqWorkspaceKind, ControlKind reqEditorKind, IRefreshCommandListener listener);

        protected readonly Func<IZetboxContext> ctxFactory;
        protected ObjectClass Type { get; private set; }
        protected IRefreshCommandListener Listener { get; private set; }

        public NewDataObjectCommand(IViewModelDependencies appCtx, Func<IZetboxContext> ctxFactory,
            IZetboxContext dataCtx, ViewModel parent, ObjectClass type, ControlKind reqWorkspaceKind, ControlKind reqEditorKind, IRefreshCommandListener listener)
            : base(appCtx, dataCtx, parent, CommonCommandsResources.NewDataObjectCommand_Name, CommonCommandsResources.NewDataObjectCommand_Tooltip)
        {
            this.Type = type;
            this.ctxFactory = ctxFactory;
            this._requestedWorkspaceKind = reqWorkspaceKind;
            this._requestedEditorKind = reqEditorKind;
            this.Listener = listener;
        }

        private ControlKind _requestedEditorKind;
        public ControlKind RequestedEditorKind
        {
            get
            {
                return _requestedEditorKind;
            }
            set
            {
                if (_requestedEditorKind != value)
                {
                    _requestedEditorKind = value;
                    OnPropertyChanged("RequestedEditorKind");
                }
            }
        }

        private ControlKind _requestedWorkspaceKind;
        public ControlKind RequestedWorkspaceKind
        {
            get
            {
                return _requestedWorkspaceKind;
            }
            set
            {
                if (_requestedWorkspaceKind != value)
                {
                    _requestedWorkspaceKind = value;
                    OnPropertyChanged("RequestedWorkspaceKind");
                }
            }
        }

        public override bool CanExecute(object data)
        {
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
            var isSimpleObject = dtType.IsSimpleObject;

            var newCtx = isSimpleObject ? DataContext : ctxFactory();
            var newObj = newCtx.Create(DataContext.GetInterfaceType(dtType.GetDataType()));
            OnObjectCreated(newObj);

            if (!isSimpleObject)
            {
                var newWorkspace = ViewModelFactory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(newCtx, null);
                newWorkspace.ShowForeignModel(DataObjectViewModel.Fetch(ViewModelFactory, newCtx, newWorkspace, newObj), RequestedEditorKind);
                ViewModelFactory.ShowModel(newWorkspace, RequestedWorkspaceKind, true);
            }
            else if (Listener != null)
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
            var objClass = newCtx.FindPersistenceObject<DataType>(this.Type.ExportGuid);
            var newWorkspace = ViewModelFactory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(newCtx, null);
            newWorkspace.ShowForeignModel(DataObjectViewModel.Fetch(ViewModelFactory, newCtx, Parent, objClass));
            ViewModelFactory.ShowModel(newWorkspace, true);
        }
    }

    public interface IRefreshCommandListener
    {
        void Refresh();
    }

    public class RefreshCommand : CommandViewModel
    {
        public new delegate RefreshCommand Factory(IZetboxContext dataCtx, ViewModel parent, IRefreshCommandListener listener);

        protected IRefreshCommandListener Listener { get; private set; }

        public RefreshCommand(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, IRefreshCommandListener listener)
            : base(appCtx, dataCtx, parent, CommonCommandsResources.RefreshCommand_Name, CommonCommandsResources.RefreshCommand_Tooltip)
        {
            this.Listener = listener;
        }

        public override bool CanExecute(object data)
        {
            var result = Listener != null;
            return result;
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
                return base.Icon ?? IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.otheroptions_ico.Find(FrozenContext));
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
            var result = CurrentIdentity != null && CurrentIdentity.IsAdmininistrator();
            this.Reason = result ? CommonCommandsResources.ElevatedModeCommand_Error : string.Empty;
            return result;
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
