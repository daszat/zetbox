using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading;
using System.Diagnostics;

using Kistl.API.Configuration;
using Kistl.API.Server;
using System.Collections;

namespace Kistl.Server
{
    /// <summary>
    /// Mainprogramm
    /// </summary>
    public static class Program
    {
        private static void PrintHelp()
        {
            Console.WriteLine("Use: Kistl.Server <configfile.xml>");
            Console.WriteLine("                  -generate");
            Console.WriteLine("                  -publish <destfile.xml> <namespace> [<namespace> ...]");
            Console.WriteLine("                  -deploy <sourcefile.xml");
            Console.WriteLine("                  -export <destfile.xml> <namespace> [<namespace> ...]");
            Console.WriteLine("                  -import <sourcefile.xml>");
            Console.WriteLine("                  -checkschema [meta | <schema.xml>]");
            Console.WriteLine("                  -repairschema");
            Console.WriteLine("                  -updateschema [<schema.xml>]");
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

        static void Main(string[] args)
        {
            try
            {
                var config = InitApplicationContext(args);

                Server server = new Server();
                bool actiondone = false;
                foreach (CmdLineArg arg in SplitCommandLine(args))
                {
                    if (arg.Command == "-export" && arg.Arguments.Count > 1)
                    {
                        string file = arg.Arguments[0];
                        List<string> namespaces = new List<string>();
                        for (int i = 1; i < arg.Arguments.Count; i++)
                        {
                            namespaces.Add(arg.Arguments[i]);
                        }
                        server.Export(file, namespaces.ToArray());
                        actiondone = true;
                    }
                    else if (arg.Command == "-import" && arg.Arguments.Count == 1)
                    {
                        string file = arg.Arguments[0];
                        server.Import(file);
                        actiondone = true;
                    }
                    else if (arg.Command == "-publish" && arg.Arguments.Count > 1)
                    {
                        string file = arg.Arguments[0];
                        List<string> namespaces = new List<string>();
                        for (int i = 1; i < arg.Arguments.Count; i++)
                        {
                            namespaces.Add(arg.Arguments[i]);
                        }
                        server.Publish(file, namespaces.ToArray());
                        actiondone = true;
                    }
                    else if (arg.Command == "-deploy" && arg.Arguments.Count == 1)
                    {
                        string file = arg.Arguments[0];
                        server.Deploy(file);
                        actiondone = true;
                    }
                    else if (arg.Command == "-checkschema")
                    {
                        string file = "";
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
                            return;
                        }
                        actiondone = true;
                    }
                    else if (arg.Command == "-repairschema" && arg.Arguments.Count == 0)
                    {
                        server.CheckSchema(true);
                        actiondone = true;
                    }
                    else if (arg.Command == "-updateschema")
                    {
                        string file = "";
                        if (arg.Arguments.Count == 0)
                        {
                            server.UpdateSchema();
                        }
                        else if (arg.Arguments.Count == 1)
                        {
                            file = arg.Arguments[0];
                            server.UpdateSchema(file);
                        }
                        else
                        {
                            PrintHelp();
                            return;
                        }
                        actiondone = true;
                    }
                    else if (arg.Command == "-generate" && arg.Arguments.Count == 0)
                    {
                        server.GenerateCode();
                        actiondone = true;
                    }
                    else
                    {
                        PrintHelp();
                        return;
                    }
                }

                if (actiondone)
                {
                    //Console.WriteLine("Hit the anykey to exit");
                    //Console.ReadKey();
                    Console.WriteLine("Shutting down");
                }
                else
                {
                    server.Start(config);

                    Console.WriteLine("Server started, press the anykey to exit");
                    Console.ReadKey();
                    Console.WriteLine("Shutting down");

                    server.Stop();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Server Application failed: \n" + ex.ToString());
            }
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
                configFilePath = "";
            }
            var config = KistlConfig.FromFile(configFilePath);
            var appCtx = new ServerApplicationContext(config);
            return config;
        }
    }
}
