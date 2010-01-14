using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API.Configuration;
using Kistl.API.Utils;
using Kistl.API;
using Kistl.API.Server;

namespace Kistl.Server.Service
{
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

                Log.TraceTotalMemory("After InitApplicationContext");

                using (var server = new Server())
                {
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

                            XmlFallbackInitialisation(file);

                            server.Deploy(file);
                            actiondone = true;
                            Log.Debug("Finished deploy");
                        }
                        else if (arg.Command == "-checkschema")
                        {
                            Log.Debug("Prepare for checkschema");
                            DefaultInitialisation();

                            string file = String.Empty;
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
                                    file = arg.Arguments[0];
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
                            string file = String.Empty;
                            if (arg.Arguments.Count == 0)
                            {
                                DefaultInitialisation();
                                server.UpdateSchema();
                            }
                            else if (arg.Arguments.Count == 1)
                            {
                                file = arg.Arguments[0];
                                XmlFallbackInitialisation(file);
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

                        server.Start(config);

                        Log.Info("Waiting for console input to shutdown");
                        Console.WriteLine("Server started, press the anykey to exit");
                        Console.ReadKey();
                        Log.Info("Shutting down");

                        server.Stop();
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


        private static void XmlFallbackInitialisation(string file)
        {
            ServerApplicationContext.Current.LoadNoopActionsManager();

            var memCtx = new MemoryContext();
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

        private static void DefaultInitialisation()
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
