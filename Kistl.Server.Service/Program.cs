
namespace Kistl.Server.Service
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Autofac;
    using Autofac.Configuration;
    using Autofac.Integration.Wcf;
    using Kistl.API;
    using Kistl.API.Common;
    using Kistl.API.Configuration;
    using Kistl.API.Server;
    using Kistl.API.Server.PerfCounter;
    using Kistl.API.Utils;
    using Kistl.App.Extensions;
    using Kistl.App.Packaging;

    /// <summary>
    /// Mainprogramm
    /// </summary>
    public static class Program
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Service");

        private static OptionSet options;

        private static void PrintHelpAndExit()
        {
            PrintHelp();
            Environment.Exit(1);
        }

        private static void PrintHelp()
        {
            if (options != null)
            {
                options.WriteOptionDescriptions(Console.Out);
            }
            else
            {
                Log.Fatal("Error while generating commandline help");
            }
        }

        public static int Main(string[] arguments)
        {
            Logging.Configure();

            Log.InfoFormat("Starting Kistl Server with args [{0}]", String.Join(" ", arguments));

            bool waitForKey = false;
            try
            {
                var config = ExtractConfig(ref arguments);
                AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);

                using (var container = CreateMasterContainer(config))
                {
                    List<Action<ILifetimeScope>> actions = new List<Action<ILifetimeScope>>();

                    options = new OptionSet()
                    {
                        { "help", "prints this help", v => { if ( v != null) { PrintHelpAndExit(); } } },
                    };

                    foreach (var cmdLineData in container.Resolve<IEnumerable<CmdLineData>>())
                    {
                        var d = cmdLineData; // decouple from loop variable
                        options.Add(d.Prototype, d.Description, v => { if (v != null) { config.AdditionalCommandlineOptions.Add(d.DataKey, v); } });
                    }

                    foreach (var cmdLineAction in container.Resolve<IEnumerable<CmdLineAction>>())
                    {
                        var d = cmdLineAction; // decouple from loop variable
                        options.Add(d.Prototype, d.Description, v => { if (v != null) { actions.Add(scope => d.Invoke(scope, v)); } });
                    }

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

                    Log.TraceTotalMemory("Before InitApplicationContext");

                    // process command line
                    if (actions.Count > 0)
                    {
                        foreach (var action in actions)
                        {
                            using (var innerContainer = container.BeginLifetimeScope())
                            {
                                action(innerContainer);
                            }
                        }

                        Log.TraceTotalMemory("After commandline processed");

                        if (waitForKey)
                        {
                            Log.Info("Waiting for console input to shutdown");
                            Console.WriteLine("Hit the anykey to exit");
                            Console.ReadKey();
                        }

                        Log.Info("Shutting down");
                    }
                    else
                    {
                        IServiceControlManager scm = null;
                        if (container.TryResolve<IServiceControlManager>(out scm))
                        {
                            Log.Info("Starting zetbox Services");
                            scm.Start();
                            Log.Info("Waiting for console input to shutdown");
                            Console.WriteLine("Services started, press the anykey to exit");
                            Console.ReadKey();
                            Log.Info("Shutting down");
                        }
                        else
                        {
                            Log.Error("No IServiceControlManager registered");
                        }

                        if (scm != null) scm.Stop();
                    }
                }
                Log.Info("Exiting");
                return 0;
            }
            catch (Exception ex)
            {
                Log.Error("Server Application failed:", ex);
                if (waitForKey)
                {
                    Log.Info("Waiting for console input to shutdown");
                    Console.WriteLine("Hit the anykey to exit");
                    Console.ReadKey();
                    Log.Info("Exiting");
                }
                return 1;
            }
        }

        internal static IContainer CreateMasterContainer(KistlConfig config)
        {
            var builder = Kistl.API.Utils.AutoFacBuilder.CreateContainerBuilder(config, config.Server.Modules);

            // register deployment-specific components
            builder.RegisterModule(new ConfigurationSettingsReader("servercomponents"));

            var container = builder.Build();
            AutofacServiceHostFactory.Container = container;
            return container;
        }

        private static KistlConfig ExtractConfig(ref string[] args)
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
            return KistlConfig.FromFile(configFilePath, "Kistl.Server.Service.xml");
        }
    }
}
