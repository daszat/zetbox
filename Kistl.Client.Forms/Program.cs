using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Kistl.API.Client;
using Kistl.API.Configuration;
using Kistl.Client.Presentables;
using Kistl.API;

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
            var config = KistlConfig.FromFile("");
            AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);
            AppContext = new GuiApplicationContext(config, "TEST");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var initialWorkspace = AppContext.Factory.CreateSpecificModel<WorkspaceModel>(KistlContext.GetContext());
            AppContext.Factory.ShowModel(initialWorkspace, true);

            Application.Run();
        }
    }
}
