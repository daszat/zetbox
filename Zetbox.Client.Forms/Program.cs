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
            var config = ZetboxConfig.FromFile(null, "DefaultFormsConfig.xml");
            AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);

            var builder = Zetbox.API.Utils.AutoFacBuilder.CreateContainerBuilder(config, config.Client.Modules);
            container = builder.Build();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mdlFactory = container.Resolve<IViewModelFactory>();
            var initialWorkspace = mdlFactory.CreateViewModel<WorkspaceViewModel.Factory>().Invoke(container.Resolve<IZetboxContext>(), null);
            mdlFactory.ShowModel(initialWorkspace, true);

            Application.Run();
        }
    }
}
