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
using Zetbox.API.Common;
using System.Threading.Tasks;

namespace Zetbox.Client.Presentables.ZetboxBase
{
    public class ApplicationViewModel : ViewModel
    {
        public new delegate ApplicationViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Application app);

        protected readonly Application app;

        public ApplicationViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            Application app)
            : base(appCtx, dataCtx, parent)
        {
            if (app == null) throw new ArgumentNullException("app");
            this.app = app;
            _wndMdlType = app.WorkspaceViewModel != null
                ? Type.GetType(app.WorkspaceViewModel.ViewModelTypeRef, true)
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

        public override string Name
        {
            get
            {
                if (app.Module != null)
                    return Assets.GetString(app.Module, ZetboxAssetKeys.Applications, ZetboxAssetKeys.ConstructNameKey(app), app.Name);
                else
                    return app.Name;
            }
        }

        public override System.Drawing.Image Icon
        {
            get
            {
                if (base.Icon == null)
                    Task.Run(async () => base.Icon = await IconConverter.ToImage(app.Icon));
                return base.Icon;
            }
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

            var newScope = ViewModelFactory.CreateNewScope();

            if (appMdl.WindowModelType != null)
            {
                var newWorkspace = newScope.ViewModelFactory.CreateViewModel<WindowViewModel.Factory>(appMdl.WindowModelType).Invoke(
                    newScope.ViewModelFactory.CreateNewContext(ContextIsolationLevel.MergeQueryData), // no data changes in applications! Open a workspace
                    null
                );
                newWorkspace.Closed += (s, e) => newScope.Dispose();
                ViewModelFactory.ShowModel(newWorkspace, true);
            }
            else if (appMdl.RootScreen != null)
            {
                var newWorkspace = newScope.ViewModelFactory.CreateViewModel<NavigatorViewModel.Factory>().Invoke(
                    newScope.ViewModelFactory.CreateNewContext(ContextIsolationLevel.MergeQueryData), // no data changes on navigation screens! Open a workspace
                    null,
                    appMdl.RootScreen
                );
                newWorkspace.Closed += (s, e) => newScope.Dispose();
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
