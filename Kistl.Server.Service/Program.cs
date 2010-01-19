
namespace Kistl.Server.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Autofac;
    using Autofac.Builder;
    using Autofac.Configuration;
    using Autofac.Integration.Wcf;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Server;
    using Kistl.API.Utils;
    using Kistl.App.Extensions;
    using NDesk.Options;

    /// <summary>
    /// Mainprogramm
    /// </summary>
    public static class Program
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Service");

        private static void PrintHelpAndExit()
        {
            PrintHelp();
            Environment.Exit(1);
        }

        private static void PrintHelp()
        {
            Log.Info("Use: Kistl.Server <configfile.xml>");
            Log.Info("                  -generate");
            Log.Info("                  -publish <destfile.xml> <namespace> [<namespace> ...]");
            Log.Info("                  -deploy <sourcefile.xml");
            Log.Info("                  -export <destfile.xml> <namespace> [<namespace> ...]");
            Log.Info("                  -import <sourcefile.xml>");
            Log.Info("                  -checkschema [meta | <schema.xml>]");
            Log.Info("                  -repairschema");
            Log.Info("                  -updateschema [<schema.xml>]");
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

        private static IEnumerable<CmdLineArg> SplitCommandLine(string[] args)
        {
            List<CmdLineArg> result = new List<CmdLineArg>();

            CmdLineArg current = null;
            foreach (string a in args)
            {
                if (a.StartsWith("-"))
                {
                    if (current != null) result.Add(current);
                    current = new CmdLineArg(a);
                }
                else if (current != null)
                {
                    current.Arguments.Add(a);
                }
            }
            if (current != null) result.Add(current);

            return result;
        }

        static int Main(string[] arguments)
        {
            Logging.Configure();

            Log.InfoFormat("Starting Kistl Server with args [{0}]", String.Join(" ", arguments));

            bool waitForKey = false;
            try
            {
                List<Action<IContainer, List<string>>> actions = new List<Action<IContainer, List<string>>>();
                string dataSourceXmlFile = null;

                var optionParser = new OptionSet()
                    {
                        { "export=", "export the database to the specified xml file", 
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<Server>().Export(v, args.ToArray())); } }
                            },
                        { "import=", "import the database from the specified xml file",
                            v => {
                                if (v == null) { return; }

                                if (dataSourceXmlFile != null && !dataSourceXmlFile.Equals(v))
                                { 
                                    Log.Fatal("Inconsistent XML Source found on command line");
                                    PrintHelpAndExit(); 
                                }
                                dataSourceXmlFile = v;
                                actions.Add((c, args) => c.Resolve<Server>().Import(v));
                            }},
                        { "publish=", "publish the specified modules to this xml file",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<Server>().Publish(v, args.ToArray())); } }
                            },
                        { "deploy=", "deploy the database frpm the specified xml file",
                            v => {
                                if (v == null) { return; }

                                if (dataSourceXmlFile != null && !dataSourceXmlFile.Equals(v))
                                { 
                                    Log.Fatal("Inconsistent XML Source found on command line");
                                    PrintHelpAndExit(); 
                                }
                                dataSourceXmlFile = v;
                                actions.Add((c, args) => c.Resolve<Server>().Deploy(v));
                            }},
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
                        { "repairschema=", "checks the schema against the deployed schema and tries to correct it",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<Server>().CheckSchema(true)); } }
                            },
                        { "updatedeployedschema", "updates the schema to the current metadata",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<Server>().UpdateSchema()); } }
                            },
                        { "updateschema=", "updates the schema to the specified xml file",
                            v => {
                                if (v != null) 
                                {
                                    if (dataSourceXmlFile != null && !dataSourceXmlFile.Equals(v))
                                    { 
                                        Log.Fatal("Inconsistent XML Source found on command line");
                                        PrintHelpAndExit(); 
                                    }
                                    dataSourceXmlFile = v;
                                    actions.Add((c, args) => c.Resolve<Server>().UpdateSchema(v));
                                }
                            }},
                        { "generate", "generates and compiles new data classes",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<Server>().GenerateCode()); } }
                            },
                        { "fix", "[DEVEL] run ad-hoc fixes against the database",
                            v => { if (v != null) { actions.Add((c, args) => c.Resolve<Server>().RunFixes()); } }
                            },
                        { "wait", "let the process wait for user input before exiting",
                            v => {
                                waitForKey = (v != null);
                            }},
                    };

                List<string> extraArguments;
                try
                {
                    extraArguments = optionParser.Parse(arguments);
                }
                catch (OptionException e)
                {
                    Log.Fatal("Error in commandline", e);
                    return 1;
                }

                Action<ContainerBuilder> registerXmlFallback = null;
                if (dataSourceXmlFile != null)
                {
                    registerXmlFallback = container =>
                    {

                    };
                }

                Log.TraceTotalMemory("Before InitApplicationContext");

                var config = InitApplicationContext(extraArguments);

                Log.TraceTotalMemory("After InitApplicationContext");

                using (var container = CreateMasterContainer(config, dataSourceXmlFile))
                {
                    DefaultInitialisation(dataSourceXmlFile, container);

                    // process command line
                    if (actions.Count > 0)
                    {
                        // skip config file
                        extraArguments = extraArguments.Skip(1).ToList();

                        foreach (var action in actions)
                        {
                            using (var innerContainer = container.CreateInnerContainer())
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

        internal static void DefaultInitialisation(string dataSourceXmlFile, IContainer container)
        {
            Log.TraceTotalMemory("Before DefaultInitialisation()");
            // TODO: remove, this should be default when using the container.
            if (dataSourceXmlFile == null) { FrozenContext.RegisterFallback(container.Resolve<IReadOnlyKistlContext>()); }

            // initialise custom actions manager
            var cams = container.Resolve<BaseCustomActionsManager>();
            Log.TraceTotalMemory("After DefaultInitialisation()");
        }

        internal static IContainer CreateMasterContainer(KistlConfig config, string dataSourceXmlFile)
        {
            AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);

            var builder = new ContainerBuilder();

            // register the configuration
            builder
                .Register(config)
                .ExternallyOwned()
                .SingletonScoped();
            // register components from most general to most specific source
            // default server stuff
            builder.RegisterModule(new ServerModule());
            // register the datastore provider
            builder.RegisterModule((IModule)Activator.CreateInstance(Type.GetType(config.Server.StoreProvider, true)));
            // register the provider for frozen objects
            // if there's a generated frozencontext, this'll override the store's default
            builder.RegisterModule((IModule)Activator.CreateInstance(Type.GetType(config.FrozenProvider, true)));
            // if there is a data source xml file, use it instead of a (possibly missing) frozen context
            if (dataSourceXmlFile != null)
            {
                builder.Register(c =>
                    {
                        var memCtx = c.Resolve<MemoryContext>();
                        // register empty context first, to avoid errors when trying to load defaultvalues
                        // TODO: remove, this should not be needed when using the container.
                        FrozenContext.RegisterFallback(memCtx);
                        Packaging.Importer.LoadFromXml(memCtx, dataSourceXmlFile);
                        return memCtx;
                    })
                    .As<IReadOnlyKistlContext>()
                    .SingletonScoped();
            }
            // register deployment-specific components
            builder.RegisterModule(new ConfigurationSettingsReader("servercomponents"));

            var container = builder.Build();
            AutofacServiceHostFactory.Container = container;
            return container;
        }

        private static KistlConfig InitApplicationContext(List<string> args)
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
            var config = KistlConfig.FromFile(configFilePath);
            var appCtx = new ServerApplicationContext(config);
            return config;
        }
    }
}
