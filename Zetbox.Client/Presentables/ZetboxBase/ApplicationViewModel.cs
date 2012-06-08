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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using Zetbox.API.Client;
using Zetbox.App.GUI;
using Zetbox.Client.Presentables.GUI;

namespace Zetbox.Client.Presentables.ZetboxBase
{
    public class ApplicationViewModel : ViewModel
    {
        public new delegate ApplicationViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Application app);

        protected readonly Func<ClientIsolationLevel, IZetboxContext> ctxFactory;

        protected readonly Application app;

        public ApplicationViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            Application app,
            Func<ClientIsolationLevel, IZetboxContext> ctxFactory)
            : base(appCtx, dataCtx, parent)
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
                    ctxFactory(ClientIsolationLevel.MergeServerData) // no data changes in applications! Open a workspace
                    , null
                );
                ViewModelFactory.ShowModel(newWorkspace, true);
            }
            else if (appMdl.RootScreen != null)
            {
                var newWorkspace = ViewModelFactory.CreateViewModel<NavigatorViewModel.Factory>().Invoke(
                    ctxFactory(ClientIsolationLevel.MergeServerData), // no data changes on navigation screens! Open a workspace
                    null,
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
