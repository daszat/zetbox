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
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using Zetbox.API;
using Zetbox.API.Client;
using Zetbox.API.Configuration;
using Zetbox.App.Extensions;
using Zetbox.Client.Presentables;
using Zetbox.Client.Presentables.ObjectBrowser;

namespace Zetbox.Client.Forms
{
    static class Program
    {
        static IContainer container;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var config = ZetboxConfig.FromFile(null, "Zetbox.Forms.xml");
            AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config, true);

            var builder = Zetbox.API.Utils.AutoFacBuilder.CreateContainerBuilder(config, config.Client.Modules);
            container = builder.Build();
            API.AppDomainInitializer.InitializeFrom(container);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mdlFactory = container.Resolve<IViewModelFactory>();
            var initialWorkspace = mdlFactory.CreateViewModel<WorkspaceViewModel.Factory>().Invoke(container.Resolve<IZetboxContext>(), null);
            mdlFactory.ShowModel(initialWorkspace, true);

            Application.Run();
        }
    }
}
