using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.GUI.DB;
using Kistl.GUI.Renderer;

namespace Kistl.Client
{
    public class Manager
    {
        /// <summary>
        /// Initialise the connection to the server
        /// </summary>
        /// <param name="args">a string[] containing the commandline arguments</param>
        /// <returns>a string[] containing all unhandled commandline arguments</returns>
        public static string[] Create(string[] args, Toolkit tk)
        {
            string[] result;

            Kistl.API.APIInit init = new Kistl.API.APIInit();
            if (args.Length == 1 && !args[0].StartsWith("-"))
            {
                init.Init(Kistl.API.HostType.Client, args[0]);
                result = new string[] { };
            }
            else
            {
                init.Init(Kistl.API.HostType.Client);
                result = (string[])args.Clone();
            }

            API.CustomActionsManagerFactory.Init(new CustomActionsManagerClient());

            Renderer = KistlGUIContext.CreateRenderer(tk);

            return result;
        }

        public static IRenderer Renderer { get; private set; }
    }
}
