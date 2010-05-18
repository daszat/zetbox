using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;
using ObjectEditorWorkspace = Kistl.Client.Presentables.ObjectEditor.WorkspaceViewModel;

namespace Kistl.Client.Presentables.KistlBase
{
    public class OpenDataObjectCommand : CommandModel
    {
        public new delegate OpenDataObjectCommand Factory(IKistlContext dataCtx);

        protected readonly Func<IKistlContext> ctxFactory;

        public OpenDataObjectCommand(IViewModelDependencies appCtx, IKistlContext dataCtx, Func<IKistlContext> ctxFactory)
            : base(appCtx, dataCtx, "Open", "Opens the current selected Object")
        {
            this.ctxFactory = ctxFactory;
        }

        public override bool CanExecute(object data)
        {
            return (data is IEnumerable<DataObjectModel>) || (data is DataObjectModel);
        }

        protected override void DoExecute(object data)
        {
            IEnumerable<DataObjectModel> objects = null;
            if (data is IEnumerable<DataObjectModel>)
            {
                objects = (IEnumerable<DataObjectModel>)data;
            }
            else if (data is DataObjectModel)
            {
                objects = new DataObjectModel[] { (DataObjectModel)data };
            }

            if (objects == null) return;

            var newWorkspace = ModelFactory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(ctxFactory());
            foreach (var item in objects)
            {
                newWorkspace.ShowForeignModel(item);
            }
            ModelFactory.ShowModel(newWorkspace, true);
        }
    }

    public class NewDataObjectCommand : CommandModel
    {
        public new delegate NewDataObjectCommand Factory(IKistlContext dataCtx, DataType type);

        protected readonly Func<IKistlContext> ctxFactory;
        protected DataType Type { get; private set; }

        public NewDataObjectCommand(IViewModelDependencies appCtx, 
            IKistlContext dataCtx, DataType type,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, dataCtx, "New", "Creates a new Object")
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
            var newCtx =  ctxFactory();
            var newWorkspace = ModelFactory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(newCtx);
            var newObj = newCtx.Create(DataContext.GetInterfaceType(Type.GetDataType()));
            newWorkspace.ShowForeignModel(ModelFactory.CreateViewModel<DataObjectModel.Factory>(newObj).Invoke(newCtx, newObj));
            ModelFactory.ShowModel(newWorkspace, true);
        }
    }

    public class EditDataObjectClassCommand : CommandModel
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
            var newWorkspace = ModelFactory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(newCtx);
            newWorkspace.ShowForeignModel(ModelFactory.CreateViewModel<DataObjectModel.Factory>(objClass).Invoke(newCtx, objClass));
            ModelFactory.ShowModel(newWorkspace, true);
        }
    }

    public interface IRefreshCommandListener
    {
        void Refresh();
    }

    public class RefreshCommand : CommandModel
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
}
