using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Client;
using Kistl.App.GUI;

namespace Kistl.Client.Presentables.KistlBase
{
    public class ApplicationViewModel : ViewModel
    {
        public new delegate ApplicationViewModel Factory(IKistlContext dataCtx, Application app);

        protected readonly Func<IKistlContext> ctxFactory;

        protected readonly Application app;

        public ApplicationViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            Application app,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, dataCtx)
        {
            if (app == null) throw new ArgumentNullException("app");
            this.ctxFactory = ctxFactory;
            this.app = app;
            _name = app.Name;
            _wndMdlType = app.WorkspaceViewModel.ViewModelRef.AsType(true);
        }

        private Type _wndMdlType;
        public Type WindowModelType
        {
            get
            {
                return _wndMdlType;
            }
        }

        private string _name;
        public override string Name
        {
            get { return _name; }
        }

        #region Open Applicaton

        private static ICommand _openApplicatonCommand = null;
        public ICommand OpenApplicatonCommand
        {
            get
            {
                if (_openApplicatonCommand == null)
                {
                    _openApplicatonCommand = ModelFactory.CreateViewModel<SimpleItemCommandModel<ApplicationViewModel>.Factory>().Invoke(DataContext, 
                        "Open Application", 
                        "Opens an Application in a new window", 
                        (i) => i.ForEach(a => OpenApplicaton(a)));
                }
                return _openApplicatonCommand;
            }
        }

        public void OpenApplicaton(ApplicationViewModel app)
        {
            if (app == null) throw new ArgumentNullException("app");
            var externalCtx = ctxFactory();

            // responsibility to externalCtx's disposal passes to newWorkspace
            var newWorkspace = ModelFactory.CreateViewModel<WindowViewModel.Factory>(app.WindowModelType).Invoke(externalCtx);
            ModelFactory.ShowModel(newWorkspace, true);
        }

        #endregion
    }
}
