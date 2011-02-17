
namespace Kistl.Server.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Autofac;
    using Autofac.Configuration;
    using Autofac.Integration.Wcf;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Server;
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
                List<Action<ILifetimeScope, List<string>>> actions = new List<Action<ILifetimeScope, List<string>>>();

                options = new OptionSet()
                    {
                        { "export=", "export the database to the specified xml file. Any extra argument is used as modulenames", 
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<IServer>().Export(v, args.ToArray())); } }
                            },
                        { "import=", "import the database from the specified xml file",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<IServer>().Import(v)); } }
                            },
                        { "publish=", "publish the specified modules to this xml file. Any extra argument is used as modulenames",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<IServer>().Publish(v, args.ToArray())); } }
                            },
                        { "deploy=", "deploy the database from the specified xml file",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<IServer>().Deploy(v)); } }
                            },
                        { "checkdeployedschema", "checks the sql schema against the deployed schema",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<IServer>().CheckSchema(false)); } } 
                            },
                        { "checkschema=", "checks the sql schema against the metadata (parameter 'meta') or a specified xml file",
                            v => {
                                if (v == null) { return; }
                                
                                if (v.Equals("meta"))
                                {
                                    actions.Add((c, args) => c.Resolve<IServer>().CheckSchemaFromCurrentMetaData(false));
                                } 
                                else 
                                {
                                    actions.Add((c, args) => c.Resolve<IServer>().CheckSchema(v, false));
                                }
                            }},
                        { "repairschema", "checks the schema against the deployed schema and tries to correct it",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<IServer>().CheckSchema(true)); } }
                            },
                        { "updatedeployedschema", "updates the schema to the current metadata",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<IServer>().UpdateSchema()); } }
                            },
                        { "updateschema=", "updates the schema to the specified xml file",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<IServer>().UpdateSchema(v)); } }
                            },
                        { "generate", "generates and compiles new data classes",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<Kistl.Generator.Compiler>().GenerateCode()); } }
                            },
                        { "fix", "[DEVEL] run ad-hoc fixes against the database",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<IServer>().RunFixes()); } }
                            },
                        { "wipe", "[DEVEL] completely wipe the contents of the database",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<IServer>().WipeDatabase()); } }
                            },
                        { "benchmark", "[DEVEL] run ad-hoc benchmarks against the database",
                            v => { if (v != null) {
                                actions.Add((c, args) => c.Resolve<IServer>().RunBenchmarks());
                				waitForKey = true;
                			} }
                            },
                        { "wait", "let the process wait for user input before exiting",
                            v => {
                                waitForKey = (v != null);
                            }},
                        { "syncidentities", "synchronices local and domain users with Kistl Identities",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<IServer>().SyncIdentities()); } }
                            },
                        { "help", "prints this help", 
                            v => { if ( v != null) { PrintHelpAndExit(); } } 
                            },
                    };

                List<string> extraArguments;
                try
                {
                    extraArguments = options.Parse(arguments);
                }
                catch (OptionException e)
                {
                    Log.Fatal("Error in commandline", e);
                    return 1;
                }

                Log.TraceTotalMemory("Before InitApplicationContext");

                var config = ExtractConfig(extraArguments);

                Log.TraceTotalMemory("After InitApplicationContext");

                AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);
                using (var container = CreateMasterContainer(config))
                {
                    // process command line
                    if (actions.Count > 0)
                    {
                        // skip config file
                        extraArguments = extraArguments.Skip(1).ToList();

                        foreach (var action in actions)
                        {
                            using (var innerContainer = container.BeginLifetimeScope())
                            {
                                action(innerContainer, extraArguments);
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
                        Log.Info("Starting ZBox Services");
                        IServiceControlManager scm = null;
                        if(container.TryResolve<IServiceControlManager>(out scm))
                        {
                            scm.Start();
                        }

                        Log.Info("Starting WCF Service");
                        var wcfServer = container.Resolve<IKistlAppDomain>();
                        wcfServer.Start(config);

                        Log.Info("Waiting for console input to shutdown");
                        Console.WriteLine("Server started, press the anykey to exit");
                        Console.ReadKey();
                        
                        Log.Info("Shutting down");
                        wcfServer.Stop();
                        if(scm != null) scm.Stop();
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

        private static KistlConfig ExtractConfig(List<string> args)
        {
            string configFilePath;
            if (args.Count > 0 && !args[0].StartsWith("-"))
            {
                configFilePath = args[0];
            }
            else
            {
                configFilePath = String.Empty;
            }
            return KistlConfig.FromFile(configFilePath, "Kistl.Server.Service.xml");
        }
    }
}
