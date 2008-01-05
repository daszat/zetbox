using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading;
using System.Diagnostics;

namespace Kistl.Server
{
    /// <summary>
    /// Hauptprogramm
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            Kistl.API.APIInit init = new Kistl.API.APIInit();
            if (args.Length > 0 && !args[0].StartsWith("-"))
            {
                init.Init(args[0]);
            }
            else
            {
                init.Init();
            }

            Server server = new Server();

            if (args.Length > 0)
            {
                bool actiondone = false;

                try
                {
                    if (!string.IsNullOrEmpty(args.FirstOrDefault(a => a.Contains("-generate"))))
                    {
                        actiondone = true;
                        server.GenerateCode();
                    }
                    if (!string.IsNullOrEmpty(args.FirstOrDefault(a => a.Contains("-database"))))
                    {
                        actiondone = true;
                        server.GenerateDatabase();
                    }

                    if(actiondone)
                    {
                        Console.WriteLine("Hit the anykey to exit");
                        Console.ReadKey();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Helper.HandleError(ex);
                }
            }

            server.Start();
            Console.WriteLine("Server started, press the anykey to exit");
            Console.ReadKey();

            server.Stop();
        }
    }
}
