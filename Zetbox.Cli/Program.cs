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

namespace Zetbox.Cli
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Autofac.Configuration;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;

    /// <summary>
    /// Mainprogramm
    /// </summary>
    public static class Program
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Cli");

        public static int Main(string[] arguments)
        {
            Logging.Configure();

            Log.InfoFormat("Starting Zetbox CLI with args [{0}]", String.Join(" ", arguments));

            try
            {
                bool loadGeneratedAssemblies;
                var config = ExtractConfig(ref arguments, out loadGeneratedAssemblies);

                AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config, loadGeneratedAssemblies);

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

                }
                Log.Info("Finished without errors.");
                return 0;
            }
            catch (Exception ex)
            {
                Log.Error("Server CLI failed", ex);
                return 1;
            }
        }

        internal static IContainer CreateMasterContainer(ZetboxConfig config)
        {
            var modules = config.Server != null && config.Server.Modules != null
                ? config.Server.Modules
                : config.Client != null && config.Client.Modules != null
                    ? config.Client.Modules
                    : new string[0];

            var builder = Zetbox.API.Utils.AutoFacBuilder.CreateContainerBuilder(config, modules);

            // register deployment-specific components
            builder.RegisterModule(new ConfigurationSettingsReader("clicomponents"));

            var container = builder.Build();
            API.AppDomainInitializer.InitializeFrom(container);
            return container;
        }

        private static ZetboxConfig ExtractConfig(ref string[] args, out bool loadGeneratedAssemblies)
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

            var fallbackOpts = new[] { "-fallback", "--fallback", "/fallback" };
            var localArgs = args;
            loadGeneratedAssemblies = !fallbackOpts.Any(opt => localArgs.Contains(opt));
            if (!loadGeneratedAssemblies)
            {
                args = args.Except(fallbackOpts).ToArray();
            }

            return ZetboxConfig.FromFile(configFilePath, "Zetbox.Cli.xml");
        }
    }
}
