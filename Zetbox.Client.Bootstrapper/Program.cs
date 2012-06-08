using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Kistl.Client.Bootstrapper
{
    static class Program
    {

        public static string[] Args { get; private set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Args = args;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Bootstrapper());
        }
    }
}
