
namespace Kistl.Client.Presentables.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.GUI;
    using ObjectEditorWorkspace = Kistl.Client.Presentables.ObjectEditor.WorkspaceViewModel;

    public class OpenDataObjectCommand : ItemCommandViewModel<DataObjectViewModel>
    {
        public new delegate OpenDataObjectCommand Factory(IKistlContext dataCtx, ControlKind reqWorkspaceKind, ControlKind reqEditorKind);

        protected readonly Func<IKistlContext> ctxFactory;

        public OpenDataObjectCommand(IViewModelDependencies appCtx, Func<IKistlContext> ctxFactory,
            IKistlContext dataCtx, ControlKind reqWorkspaceKind, ControlKind reqEditorKind
            )
            : base(appCtx, dataCtx, "Open", "Opens the current selected Object")
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
            var newWorkspace = ViewModelFactory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(ctxFactory());
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

    public class DeleteDataObjectCommand : ItemCommandViewModel<DataObjectViewModel>
    {
        public new delegate DeleteDataObjectCommand Factory(IKistlContext dataCtx, IRefreshCommandListener listener, bool submitChanges);
        protected IRefreshCommandListener Listener { get; private set; }
        protected bool SubmitChanges { get; private set; }

        public DeleteDataObjectCommand(IViewModelDependencies appCtx, IKistlContext dataCtx, IRefreshCommandListener listener, bool submitChanges)
            : base(appCtx, dataCtx, "Delete", "Deletes the current selected Object")
        {
            this.Listener = listener;
            this.SubmitChanges = submitChanges;
        }

        protected override void DoExecute(IEnumerable<DataObjectViewModel> data)
        {
            if (SubmitChanges && !ViewModelFactory.GetDecisionFromUser("Are you sure that you want to delete these items?", "Deleting items"))
            {
                return;
            }

            foreach (var item in data)
            {
                DataContext.Delete(item.Object);
            }

            if (SubmitChanges) DataContext.SubmitChanges();
            if (Listener != null) Listener.Refresh();
        }
    }

    public class NewDataObjectCommand : CommandViewModel
    {
        public new delegate NewDataObjectCommand Factory(IKistlContext dataCtx, DataType type, ControlKind reqWorkspaceKind, ControlKind reqEditorKind, IRefreshCommandListener listener);

        protected readonly Func<IKistlContext> ctxFactory;
        protected DataType Type { get; private set; }
        protected IRefreshCommandListener Listener { get; private set; }

        public NewDataObjectCommand(IViewModelDependencies appCtx, Func<IKistlContext> ctxFactory,
            IKistlContext dataCtx, DataType type, ControlKind reqWorkspaceKind, ControlKind reqEditorKind, IRefreshCommandListener listener)
            : base(appCtx, dataCtx, "New", "Creates a new Object")
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
            var isSimpleObject = Type is ObjectClass && ((ObjectClass)Type).IsSimpleObject;

            var newCtx = isSimpleObject ? DataContext : ctxFactory();
            var newObj = newCtx.Create(DataContext.GetInterfaceType(Type.GetDataType()));
            OnObjectCreated(newObj);

            if (!isSimpleObject)
            {
                var newWorkspace = ViewModelFactory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(newCtx);
                newWorkspace.ShowForeignModel(DataObjectViewModel.Fetch(ViewModelFactory, newCtx, newObj), RequestedEditorKind);
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
        public new delegate EditDataObjectClassCommand Factory(IKistlContext dataCtx, DataType type);

        protected readonly Func<IKistlContext> ctxFactory;
        protected DataType Type { get; private set; }

        public EditDataObjectClassCommand(IViewModelDependencies appCtx,
            IKistlContext dataCtx, DataType type,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, dataCtx, "Edit Class", "Opens the Editor for the current lists class")
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
            var objClass = newCtx.Find<DataType>(this.Type.ID);
            var newWorkspace = ViewModelFactory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(newCtx);
            newWorkspace.ShowForeignModel(DataObjectViewModel.Fetch(ViewModelFactory, newCtx, objClass));
            ViewModelFactory.ShowModel(newWorkspace, true);
        }
    }

    public interface IRefreshCommandListener
    {
        void Refresh();
    }

    public class RefreshCommand : CommandViewModel
    {
        public new delegate RefreshCommand Factory(IKistlContext dataCtx, IRefreshCommandListener listener);

        protected IRefreshCommandListener Listener { get; private set; }

        public RefreshCommand(IViewModelDependencies appCtx, IKistlContext dataCtx, IRefreshCommandListener listener)
            : base(appCtx, dataCtx, "Refresh", "Refreshes the current list")
        {
            this.Listener = listener;
        }

        public override bool CanExecute(object data)
        {
            return Listener != null;
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
        public new delegate ReportProblemCommand Factory(IKistlContext dataCtx);

        private readonly IProblemReporter _reporter;

        public ReportProblemCommand(IViewModelDependencies appCtx, IProblemReporter reporter, IKistlContext dataCtx)
            : base(appCtx, dataCtx, "Report a Problem", "Reports a Problem")
        {
            this._reporter = reporter;
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
                    _reporter.Report();
                }
                catch (Exception ex)
                {
                    // The Reporter has a problem...
                    ViewModelFactory.ShowMessage("Unable to send Problem:\n" + ex.Message, "Error");
                }
            }
        }
    }
}
