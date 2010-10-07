using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using Kistl.API;
using Kistl.API.Client;
using Kistl.API.Configuration;
using Kistl.App.Extensions;
using Kistl.Client.Presentables;
using Kistl.Client.Presentables.ObjectBrowser;

namespace Kistl.Client.Forms
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
            var config = KistlConfig.FromFile("DefaultFormsConfig.xml");
            AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);

            var builder = Kistl.API.Utils.AutoFacBuilder.CreateContainerBuilder(config, config.Client.Modules);
            container = builder.Build();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mdlFactory = container.Resolve<IViewModelFactory>();
            var initialWorkspace = mdlFactory.CreateViewModel<WorkspaceViewModel.Factory>().Invoke(container.Resolve<IKistlContext>());
            mdlFactory.ShowModel(initialWorkspace, true);

            Application.Run();
        }
    }
}
