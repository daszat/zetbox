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

    /// <summary>
    /// Mainprogramm
    /// </summary>
    public static class Program
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Server.Service");

        public static int Main(string[] arguments)
        {
            // Fix working directory
            //if (Environment.OSVersion.Platform != PlatformID.Unix && Environment.UserInteractive == false)
            //{
            //    // Starting as windows service -> fixing path
            //    Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            //}
            
            Logging.Configure();

            Log.InfoFormat("Starting Zetbox Server with args [{0}]", String.Join(" ", arguments));

            try
            {
                var config = ExtractConfig(ref arguments);
                AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);

                using (var container = CreateMasterContainer(config))
                {
                    OptionSet options = new OptionSet();

                    // activate all registered options
                    container.Resolve<IEnumerable<Option>>().OrderBy(o => o.Prototype).ForEach(o => options.Add(o));

                    List<string> extraArguments = null;
                    try
                    {
                        extraArguments = options.Parse(arguments);
                    }
                    catch (OptionException e)
                    {
                        Log.Fatal("Error in commandline", e);
                        return 1;
                    }

                    if (extraArguments != null && extraArguments.Count > 0)
                    {
                        Log.FatalFormat("Unrecognized arguments on commandline: {0}", string.Join(", ", extraArguments.ToArray()));
                        return 1;
                    }

                    var actions = config.AdditionalCommandlineActions;

                    // process command line
                    if (actions.Count > 0)
                    {
                        using (Log.DebugTraceMethodCall("CmdLineActions", "processing commandline actions"))
                        {
                            foreach (var action in actions)
                            {
                                using (var innerContainer = container.BeginLifetimeScope())
                                {
                                    action(innerContainer);
                                }
                            }
                        }
                        Log.Info("Shutting down");
                    }
                    else
                    {
                        var service = container.Resolve<WindowsService>();
                        //if (Environment.UserInteractive || Environment.OSVersion.Platform == PlatformID.Unix) // Environment.UserInteractive returns false under mono
                        //{
                            service.StartService();
                            Log.Info("Waiting for console input to shutdown");
                            Console.WriteLine("Services started, press the anykey to exit");
                            Console.ReadKey();
                            Log.Info("Shutting down");
                            service.StopService();
                        //}
                        //else
                        //{
                        //    var ServicesToRun = new System.ServiceProcess.ServiceBase[] { service };
                        //    System.ServiceProcess.ServiceBase.Run(ServicesToRun);
                        //}
                    }
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

            builder.RegisterType<WindowsService>().SingleInstance();

            // register deployment-specific components
            builder.RegisterModule(new ConfigurationSettingsReader("servercomponents"));

            var container = builder.Build();
            Zetbox.Server.Helper.InitializeAppDomain(container);
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
            return ZetboxConfig.FromFile(configFilePath, "Zetbox.Server.Service.xml");
        }
    }
}
