
namespace Zetbox.Server.WinService
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
    using Zetbox.API.Configuration;
    using Zetbox.API.Server;
    using Zetbox.API.Server.PerfCounter;
    using Zetbox.API.Utils;
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
                bool consoleMode = false;
                if (arguments.Length == 0)
                {
                    consoleMode = false;
                    Log.InfoFormat("Changed working directory to '{0}'", Environment.CurrentDirectory);
                }
                else if (arguments.Length == 1)
                {
                    switch (arguments[0])
                    {
                        case "/console":
                        case "-console":
                        case "--console":
                            consoleMode = true;
                            Log.Info("Staying in foreground");
                            break;
                        case "/help":
                        case "-help":
                        case "--help":
                            Log.Warn("Start with --console to keep the service in the foreground.");
                            Environment.Exit(1);
                            break;
                    }
                }
                else
                {
                    Log.Fatal("Too many parameters. Start with --help to see usage.");
                    Environment.Exit(1);
                }

                AssemblyLoader.Bootstrap(config);

                using (var container = CreateMasterContainer(config))
                {
                    var service = container.Resolve<WindowsService>();
                    if (consoleMode)
                    {
                        service.StartService();
                        Log.Info("Waiting for console input to shutdown");
                        Console.WriteLine("Services started, press the anykey to exit");
                        Console.ReadKey();
                        Log.Info("Shutting down");
                        service.StopService();
                    }
                    else
                    {
                        var ServicesToRun = new System.ServiceProcess.ServiceBase[] { service };
                        System.ServiceProcess.ServiceBase.Run(ServicesToRun);
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