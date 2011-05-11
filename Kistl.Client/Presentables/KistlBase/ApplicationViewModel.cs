using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Client;
using Kistl.App.GUI;
using Kistl.Client.Presentables.GUI;

namespace Kistl.Client.Presentables.KistlBase
{
    public class ApplicationViewModel : ViewModel
    {
        public new delegate ApplicationViewModel Factory(IKistlContext dataCtx, Application app);

        protected readonly Func<ClientIsolationLevel, IKistlContext> ctxFactory;

        protected readonly Application app;

        public ApplicationViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            Application app,
            Func<ClientIsolationLevel, IKistlContext> ctxFactory)
            : base(appCtx, dataCtx)
        {
            if (app == null) throw new ArgumentNullException("app");
            this.ctxFactory = ctxFactory;
            this.app = app;
            _name = app.Name;
            _wndMdlType = app.WorkspaceViewModel != null
                ? app.WorkspaceViewModel.ViewModelRef.AsType(true)
                : null;
        }

        private Type _wndMdlType;
        public Type WindowModelType
        {
            get
            {
                return _wndMdlType;
            }
        }

        public NavigationScreen RootScreen
        {
            get
            {
                return app.RootScreen;
            }
        }

        private string _name;
        public override string Name
        {
            get { return _name; }
        }

        #region Open Application

        private static ICommandViewModel _openApplicationCommand = null;
        public ICommandViewModel OpenApplicationCommand
        {
            get
            {
                if (_openApplicationCommand == null)
                {
                    _openApplicationCommand = ViewModelFactory.CreateViewModel<SimpleItemCommandViewModel<ApplicationViewModel>.Factory>().Invoke(DataContext,
                        this,
                        ApplicationViewModelResources.OpenApplicationCommand_Name,
                        ApplicationViewModelResources.OpenApplicationCommand_Tooltip,
                        (i) => i.ForEach(a => OpenApplication(a)));
                }
                return _openApplicationCommand;
            }
        }

        public void OpenApplication(ApplicationViewModel appMdl)
        {
            if (appMdl == null) throw new ArgumentNullException("appMdl");

            if (appMdl.WindowModelType != null)
            {
                // responsibility to externalCtx's disposal passes to newWorkspace
                var newWorkspace = ViewModelFactory.CreateViewModel<WindowViewModel.Factory>(appMdl.WindowModelType).Invoke(
                    ctxFactory(ClientIsolationLevel.PrefereClientData) // TODO: Backward compatibility? 
                );
                ViewModelFactory.ShowModel(newWorkspace, true);
            }
            else if (appMdl.RootScreen != null)
            {
                var newWorkspace = ViewModelFactory.CreateViewModel<NavigatorViewModel.Factory>().Invoke(
                    ctxFactory(ClientIsolationLevel.MergeServerData), // TODO: no data changes on navigation screens?
                    appMdl.RootScreen
                );
                ViewModelFactory.ShowModel(newWorkspace, true);
            }
            else
            {
                // TODO: protect by constraint. See Case#1649
                throw new NotSupportedException("Application has no defined startup Screen");
            }
        }

        #endregion
    }
}
