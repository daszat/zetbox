using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using Kistl.API;
using Kistl.API.Client;
using Kistl.API.Configuration;
using Kistl.Client.Presentables;
using Kistl.Client.Presentables.ObjectBrowser;

namespace Kistl.Client.Forms
{
    static class Program
    {
        static GuiApplicationContext AppContext { get; set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var config = KistlConfig.FromFile("DefaultFormsConfig.xml");
            AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);
            Assembly interfaces = Assembly.Load(Kistl.API.Helper.InterfaceAssembly);
            Assembly implementation = Assembly.Load(Kistl.API.Helper.ClientAssembly);
            AppContext = new GuiApplicationContext(config, "TEST", () => new MemoryContext(interfaces, implementation));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var initialWorkspace = AppContext.Factory.CreateSpecificModel<WorkspaceViewModel>(KistlContext.GetContext());
            AppContext.Factory.ShowModel(initialWorkspace, true);

            Application.Run();
        }
    }
}
