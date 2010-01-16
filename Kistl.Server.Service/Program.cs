
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

    /// <summary>
    /// Mainprogramm
    /// </summary>
    public static class Program
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Service");

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

        static int Main(string[] args)
        {
            Logging.Configure();

            Log.InfoFormat("Starting Kistl Server with args [{0}]", String.Join(" ", args));

            bool waitForKey = false;
            try
            {
                Log.TraceTotalMemory("Before InitApplicationContext");

                var config = InitApplicationContext(args);

                using (var container = CreateMasterContainer(config))
                {
                    Log.TraceTotalMemory("After InitApplicationContext");

                    var server = container.Resolve<Server>();
                    bool actiondone = false;
                    foreach (CmdLineArg arg in SplitCommandLine(args))
                    {
                        if (arg.Command == "-export" && arg.Arguments.Count > 1)
                        {
                            Log.Debug("Prepare for exporting");
                            DefaultInitialisation();
                            string file = arg.Arguments[0];
                            List<string> namespaceList = new List<string>();
                            for (int i = 1; i < arg.Arguments.Count; i++)
                            {
                                namespaceList.Add(arg.Arguments[i]);
                            }
                            var namespaces = namespaceList.ToArray();
                            server.Export(file, namespaces);
                            actiondone = true;
                            Log.Debug("Finished exporting");
                        }
                        else if (arg.Command == "-import" && arg.Arguments.Count == 1)
                        {
                            Log.Debug("Prepare for importing");
                            DefaultInitialisation();
                            string file = arg.Arguments[0];
                            server.Import(file);
                            actiondone = true;
                            Log.Debug("Finished importing");
                        }
                        else if (arg.Command == "-publish" && arg.Arguments.Count > 1)
                        {
                            Log.Debug("Prepare for publish");
                            DefaultInitialisation();
                            string file = arg.Arguments[0];
                            List<string> namespaces = new List<string>();
                            for (int i = 1; i < arg.Arguments.Count; i++)
                            {
                                namespaces.Add(arg.Arguments[i]);
                            }
                            server.Publish(file, namespaces.ToArray());
                            actiondone = true;
                            Log.Debug("Finished publish");
                        }
                        else if (arg.Command == "-deploy" && arg.Arguments.Count == 1)
                        {
                            Log.Debug("Prepare for deploy");
                            string file = arg.Arguments[0];

                            XmlFallbackInitialisation(file, container.Resolve<Func<MemoryContext>>());

                            server.Deploy(file);
                            actiondone = true;
                            Log.Debug("Finished deploy");
                        }
                        else if (arg.Command == "-checkschema")
                        {
                            Log.Debug("Prepare for checkschema");
                            DefaultInitialisation();

                            if (arg.Arguments.Count == 0)
                            {
                                server.CheckSchema(false);
                            }
                            else if (arg.Arguments.Count == 1)
                            {
                                if (arg.Arguments[0] == "meta")
                                {
                                    server.CheckSchemaFromCurrentMetaData(false);
                                }
                                else
                                {
                                    string file = arg.Arguments[0];
                                    server.CheckSchema(file, false);
                                }
                            }
                            else
                            {
                                PrintHelp();
                                return 1;
                            }
                            actiondone = true;
                            Log.Debug("Finished checkschema");
                        }
                        else if (arg.Command == "-repairschema" && arg.Arguments.Count == 0)
                        {
                            Log.Debug("Prepare for repairschema");
                            DefaultInitialisation();

                            server.CheckSchema(true);
                            actiondone = true;
                            Log.Debug("Finished repairschema");
                        }
                        else if (arg.Command == "-updateschema")
                        {
                            Log.Debug("Prepare for updateschema");
                            if (arg.Arguments.Count == 0)
                            {
                                DefaultInitialisation();
                                server.UpdateSchema();
                            }
                            else if (arg.Arguments.Count == 1)
                            {
                                string file = arg.Arguments[0];
                                XmlFallbackInitialisation(file, container.Resolve<Func<MemoryContext>>());
                                server.UpdateSchema(file);
                            }
                            else
                            {
                                PrintHelp();
                                return 1;
                            }
                            actiondone = true;
                            Log.Debug("Finished updateschema");
                        }
                        else if (arg.Command == "-generate" && arg.Arguments.Count == 0)
                        {
                            Log.Debug("Prepare for generate");
                            DbFallbackInitialisation();
                            server.GenerateCode();
                            actiondone = true;
                            Log.Debug("Finished generate");
                        }
                        else if (arg.Command == "-fix" && arg.Arguments.Count == 0)
                        {
                            Log.Debug("Prepare for fix");
                            DefaultInitialisation();
                            // hidden command to execute ad-hoc fixes against the database
                            server.RunFixes();
                            actiondone = true;
                            Log.Debug("Finished fix");
                        }
                        else if (arg.Command == "-wait")
                        {
                            Log.Info("will wait for user input before exiting");
                            waitForKey = true;
                        }
                        else
                        {
                            Log.FatalFormat("Unrecognised commandline argument [{0}]. Exiting.", arg.Command);
                            PrintHelp();
                            return 1;
                        }
                    }

                    if (actiondone)
                    {
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
                        Log.TraceTotalMemory("Before DefaultInitialisation()");
                        DefaultInitialisation();
                        Log.TraceTotalMemory("After DefaultInitialisation()");

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

        internal static IContainer CreateMasterContainer(KistlConfig config)
        {
            var builder = new ContainerBuilder();

            // register components from most general to most specific source
            // default server stuff
            builder.RegisterModule(new ServerModule());
            // register the datastore provider
            builder.RegisterModule((IModule)Activator.CreateInstance(Type.GetType(config.Server.StoreProvider, true)));
            // register the provider for frozen objects
            // if there's a generated frozencontext, this'll override the store's default
            builder.RegisterModule((IModule)Activator.CreateInstance(Type.GetType(config.FrozenProvider, true)));
            // register deployment-specific components
            builder.RegisterModule(new ConfigurationSettingsReader("servercomponents"));

            var container = builder.Build();
            AutofacServiceHostFactory.Container = container;
            return container;
        }

        private static void XmlFallbackInitialisation(string file, Func<MemoryContext> createCtx)
        {
            ServerApplicationContext.Current.LoadNoopActionsManager();

            var memCtx = createCtx();

            // register empty context first, to avoid errors when trying to load defaultvalues
            FrozenContext.RegisterFallback(memCtx);
            Packaging.Importer.LoadFromXml(memCtx, file);

            ServerApplicationContext.Current.LoadDefaultActionsManager();
        }

        private static void DbFallbackInitialisation()
        {
            ServerApplicationContext.Current.LoadNoopActionsManager();
            FrozenContext.RegisterFallback(KistlContext.GetContext());
            ServerApplicationContext.Current.LoadDefaultActionsManager();
        }

        internal static void DefaultInitialisation()
        {
            // TODO: Remove the fallback registration after Case 1211 is fixed
            ServerApplicationContext.Current.LoadNoopActionsManager();
            FrozenContext.RegisterFallback(KistlContext.GetContext());
            // end-TODO
            ServerApplicationContext.Current.LoadDefaultActionsManager();
        }

        private static KistlConfig InitApplicationContext(string[] args)
        {
            string configFilePath;
            if (args.Length > 0 && !args[0].StartsWith("-"))
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
