using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Kistl
{
    /// <summary>
    /// TODO: Das ist Würstelcode!!!!
    /// </summary>
    static class Program
    {
        private static Kistl.API.IKistlAppDomain server;
        private static Kistl.API.IKistlAppDomain client;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                MessageBox.Show("Usage: Kistl <configfile.xml>");
                return;
            }

            Kistl.API.Configuration.KistlConfig cfg;
            try
            {
                cfg = Kistl.API.Configuration.KistlConfig.FromFile(args[0]);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            // Get Files
            string destPath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            destPath += destPath.EndsWith(@"\") ? "" : @"\";
            string destSubPath = cfg.ConfigName;
            Path.GetInvalidPathChars().ToList().ForEach(c => destSubPath = destSubPath.Replace(c, '_'));
            destPath += @"dasz\Kistl\" + destSubPath + @"\";

            Directory.CreateDirectory(destPath + "bin");
            Directory.GetFiles(destPath + "bin").ToList().ForEach(f => File.Delete(f));

            foreach (string dir in cfg.SourceFileLocation)
            {
                foreach (string file in Directory.GetFiles(dir))
                {
                    File.Copy(file, destPath + @"bin\" + Path.GetFileName(file), true);
                }
            }

            if (cfg.Server.StartServer)
            {
                // Start Server
                // Create a new AppDomain for the Server!
                // Damit trennt man den Server schön brav vom Client & kann 
                // ObjectBrokerFactory.Init zwei mal aufrufen :-)
                AppDomainSetup serverInfo = new AppDomainSetup();
                serverInfo.ApplicationBase = destPath;
                serverInfo.PrivateBinPath = "bin";
                serverInfo.ConfigurationFile = destPath + @"bin\Kistl.Server.dll.config";

                AppDomain serverDomain = AppDomain.CreateDomain("ServerAppDomain", null, serverInfo);
                server = (Kistl.API.IKistlAppDomain)serverDomain.CreateInstanceAndUnwrap("Kistl.Server", "Kistl.Server.Server");
                server.Start();
            }

            if (cfg.Client.StartClient)
            {
                // Start Client
                AppDomainSetup clientInfo = new AppDomainSetup();
                clientInfo.ApplicationBase = destPath;
                clientInfo.PrivateBinPath = "bin";
                clientInfo.ConfigurationFile = destPath + @"bin\Kistl.Client.dll.config";

                AppDomain clientDomain = AppDomain.CreateDomain("ClientAppDomain", null, clientInfo);
                client = (Kistl.API.IKistlAppDomain)clientDomain.CreateInstanceAndUnwrap("Kistl.Client", "Kistl.Client.Client");

                // This is a blocking call
                // ToDo: Change to a non blocking call
                client.Start();

                if (server != null)
                {
                    server.Stop();
                }
            }
            else
            {
                // Wait for the shutdown Event
                while (true)
                {
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
    }
}
