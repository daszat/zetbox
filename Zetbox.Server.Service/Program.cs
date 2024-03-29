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

namespace Zetbox.Server.Service
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Autofac;
    using Autofac.Configuration;
    using Zetbox.API;
    using Zetbox.API.Common;
    using Zetbox.API.Configuration;
    using Zetbox.API.Server;
    using Zetbox.API.Server.PerfCounter;
    using Zetbox.API.Utils;
    using Zetbox.App.Extensions;
    using Zetbox.App.Packaging;
    using System.Configuration;

    /// <summary>
    /// Mainprogramm
    /// </summary>
    public static class Program
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger(typeof(Program));

        public static int Main(string[] arguments)
        {
            // Fix working directory: Services start in System32
            // Services can't have arguments, therefore we need to cwd.
            if (arguments == null || arguments.Length == 0)
            {
                Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            }

            Logging.Configure();

            Log.InfoFormat("Starting Zetbox Service with args [{0}] at {1}", String.Join(" ", arguments), Environment.CurrentDirectory);

            try
            {
                var config = ExtractConfig(ref arguments);

                AssemblyLoader.Bootstrap(config);

                using (var container = CreateMasterContainer(config))
                {
                    var scm = container.Resolve<IServiceControlManager>();
                    scm.Start();
                    Log.Info("Waiting for console input to shutdown");
                    Console.WriteLine("Services started, press the anykey to exit");
                    Console.ReadKey();
                    Log.Info("Shutting down");
                    scm.Stop();
                }
                Log.Info("Exiting");
                return 0;
            }
            catch (Exception ex)
            {
                Log.Error("Server Application failed:", ex);
                return 1;
            }
        }

        internal static IContainer CreateMasterContainer(ZetboxConfig config)
        {
            var builder = Zetbox.API.Utils.AutoFacBuilder.CreateContainerBuilder(config, config.Server.Modules);

            var container = builder.Build();
            container.ApplyPerfCounterTracker();

            API.AppDomainInitializer.InitializeFrom(container);
            return container;
        }
        private static ZetboxConfig ExtractConfig(ref string[] args)
        {
            string configFilePath;
            if (args.Length > 0 && File.Exists(args[0]))
            {
                configFilePath = args[0];
                // remove consumed config file argument
                args = args.Skip(1).ToArray();
            }
            else
            {
                configFilePath = String.Empty;
            }
            return ZetboxConfig.FromFile(HostType.Server, configFilePath, "Zetbox.Server.Service.xml");
        }
    }
}
