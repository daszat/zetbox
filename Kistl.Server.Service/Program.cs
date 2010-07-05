
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

        private class CmdLineArg
        {
            public CmdLineArg(string cmd)
            {
                Command = cmd;
                Arguments = new List<string>();
            }

            public string Command { get; set; }
            public IList<string> Arguments { get; private set; }
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
                        { "export=", "export the database to the specified xml file. Any extra argument is used as namespace", 
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<Server>().Export(v, args.ToArray())); } }
                            },
                        { "import=", "import the database from the specified xml file",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<Server>().Import(v)); } }
                            },
                        { "publish=", "publish the specified modules to this xml file. Any extra argument is used as namespace",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<Server>().Publish(v, args.ToArray())); } }
                            },
                        { "deploy=", "deploy the database from the specified xml file",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<Server>().Deploy(v)); } }
                            },
                        { "checkdeployedschema", "checks the sql schema against the deployed schema",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<Server>().CheckSchema(false)); } } 
                            },
                        { "checkschema=", "checks the sql schema against the metadata (parameter 'meta') or a specified xml file",
                            v => {
                                if (v == null) { return; }
                                
                                if (v.Equals("meta"))
                                {
                                    actions.Add((c, args) => c.Resolve<Server>().CheckSchemaFromCurrentMetaData(false));
                                } 
                                else 
                                {
                                    actions.Add((c, args) => c.Resolve<Server>().CheckSchema(v, false));
                                }
                            }},
                        { "repairschema", "checks the schema against the deployed schema and tries to correct it",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<Server>().CheckSchema(true)); } }
                            },
                        { "updatedeployedschema", "updates the schema to the current metadata",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<Server>().UpdateSchema()); } }
                            },
                        { "updateschema=", "updates the schema to the specified xml file",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<Server>().UpdateSchema(v)); } }
                            },
                        { "generate", "generates and compiles new data classes",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<Server>().GenerateCode()); } }
                            },
                        { "fix", "[DEVEL] run ad-hoc fixes against the database",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<Server>().RunFixes()); } }
                            },
                        { "benchmark", "[DEVEL] run ad-hoc benchmarks against the database",
                            v => { if (v != null) {
                				actions.Add((c, args) => c.Resolve<Server>().RunBenchmarks());
                				waitForKey = true;
                			} }
                            },
                        { "wait", "let the process wait for user input before exiting",
                            v => {
                                waitForKey = (v != null);
                            }},
                        { "syncidentities", "synchronices local and domain users with Kistl Identities",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<Server>().SyncIdentities()); } }
                            },
                        { "copydb", "copies a database to a staging database. Extra Arguments are: [SRCPROVIDER] [SRC ConnectionString] [DESTPROVIDER] [DEST ConnectionString]. Valid Providers are: MSSQL, POSTGRESQL, OLEDB. WARNING: All tables in the destination database will be dropped!",
                            v => { if (v != null) { 
                                actions.Add((c, args) => { 
                                    if (args == null) PrintHelpAndExit();
                                    if (args.Count != 4) PrintHelpAndExit();

                                    var srcProvider = c.Resolve<ISchemaProvider>(args[0]);
                                    srcProvider.Open(args[1]);
                                    
                                    var dstProvider = c.Resolve<ISchemaProvider>(args[2]);
                                    dstProvider.Open(args[3]);
                                    
                                    c.Resolve<Server>().CopyDatabase(srcProvider, dstProvider);
                                }); 
                            } }
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
                using (var container = CreateMasterContainer(config, null))
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
                        var wcfServer = container.Resolve<IKistlAppDomain>();
                        wcfServer.Start(config);

                        Log.Info("Waiting for console input to shutdown");
                        Console.WriteLine("Server started, press the anykey to exit");
                        Console.ReadKey();
                        Log.Info("Shutting down");

                        wcfServer.Stop();
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

        internal static IContainer CreateMasterContainer(KistlConfig config, string dataSourceXmlFile)
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
            return KistlConfig.FromFile(configFilePath);
        }
    }
}
