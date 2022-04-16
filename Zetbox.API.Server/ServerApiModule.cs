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

namespace Zetbox.API.Server
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Autofac;
    using Zetbox.API.Configuration;
    using Zetbox.API.Server.PerfCounter;
    using Zetbox.API.Utils;

    public sealed class ServerApiModule
        : Autofac.Module
    {
        public static object SchemaModulesKey { get { return "schemamodules"; } }
        public static object OwnerModulesKey { get { return "ownermodules"; } }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register<ServerDeploymentRestrictor>(c => new ServerDeploymentRestrictor())
                .As<IDeploymentRestrictor>()
                .SingleInstance();

            builder
                .RegisterType<Migration.MigrationTasksBase>()
                .As<Migration.IMigrationTasks>()
                .InstancePerDependency();

            builder
                .RegisterType<PerfCounterDispatcher>()
                .As<IPerfCounter>()
                .OnRelease(obj => obj.Dump())
                .SingleInstance();

            builder
                .RegisterAssemblyTypes(typeof(ServerApiModule).Assembly)
                .AssignableTo<Option>()
                .As<Option>()
                .InstancePerLifetimeScope();

            builder
                .RegisterCmdLineDataOption("schemamodules=", "A semicolon-separated list of schema-defining modules to export", SchemaModulesKey);
            builder
                .RegisterCmdLineDataOption("ownermodules=", "A semicolon-separated list of data-owning modules to export", OwnerModulesKey);

            builder
                .RegisterCmdLineAction("foce", "Force a operation. When using with Schema Management, every error will be ignored. Use --checkdeployedschema and --repairschema to ensure a valid schema.",
                (scope, arg) =>
                {
                    var config = scope.Resolve<ZetboxConfig>();
                    config.Force = true;
                    return Task.CompletedTask;
                });


            builder
                .RegisterCmdLineAction("deploy=", "deploy the database from the specified xml file",
                (scope, arg) =>
                {
                    Logging.Server.Warn("Option deploy with arguments is depricated, migrate to new deployment strategy");
                    scope.Resolve<IServer>().Deploy(arg);
                    return Task.CompletedTask;
                });

            builder
                .RegisterCmdLineAction("deploy-local:", "deploy all modules from the local Modules directory",
                (scope, arg) =>
                {
                    var srv = scope.Resolve<IServer>();
                    srv.Deploy(Directory.GetFiles(arg ?? "Modules", "*.xml", SearchOption.TopDirectoryOnly));
                    return Task.CompletedTask;
                });

            builder
                .RegisterCmdLineAction("deploy-update", "update the database from all files in Module directory, deployes them and generates",
                (scope, arg) =>
                {
                    scope.Resolve<IServer>().Deploy();
                    return Task.CompletedTask;
                });

            builder
                .RegisterCmdLineAction("import=", "import the database from the specified xml file",
                (scope, arg) =>
                {
                    scope.Resolve<IServer>().Import(arg);
                    return Task.CompletedTask;
                });

            builder
                .RegisterCmdLineAction("export=", "export the database to the specified xml file. Select the exported data with -schemamodules and -ownermodules",
                (scope, arg) =>
                {
                    var config = scope.Resolve<ZetboxConfig>();
                    string[] schemaModulesArray;
                    string[] ownerModulesArray;
                    ParseModules(config, out schemaModulesArray, out ownerModulesArray);
                    scope.Resolve<IServer>().Export(
                        arg,
                        schemaModulesArray,
                        ownerModulesArray);
                    return Task.CompletedTask;
                });

            builder
                .RegisterCmdLineAction("publish=", "publish the specified modules to this xml file. Select the exported data with -ownermodules",
                (scope, arg) =>
                {
                    var config = scope.Resolve<ZetboxConfig>();
                    string[] schemaModulesArray;
                    string[] ownerModulesArray;
                    ParseModules(config, out schemaModulesArray, out ownerModulesArray);
                    scope.Resolve<IServer>().Publish(
                        arg,
                        ownerModulesArray);
                    return Task.CompletedTask;
                });

            builder
                .RegisterCmdLineAction("deletemodule=", "delete the specified module from the database. Use updatedeployedschema to apply schema changes.",
                (scope, arg) =>
                {
                    var config = scope.Resolve<ZetboxConfig>();
                    scope.Resolve<IServer>().DeleteModule(arg);
                    return Task.CompletedTask;
                });

            builder
                .RegisterCmdLineAction("checkdeployedschema", "checks the sql schema against the deployed schema",
                scope =>
                {
                    scope.Resolve<IServer>().CheckSchema(false);
                    return Task.CompletedTask;
                });

            builder
                .RegisterCmdLineAction("checkschema=", "checks the sql schema against the metadata (parameter 'meta') or a specified xml file",
                (scope, arg) =>
                {
                    if (arg == "meta")
                    {
                        scope.Resolve<IServer>().CheckSchemaFromCurrentMetaData(false);
                    }
                    else if (!string.IsNullOrWhiteSpace(arg))
                    {
                        scope.Resolve<IServer>().CheckSchema(new[] { arg }, false);
                    }
                    else
                    {
                        Logging.Server.Error("the command checkschema requieres a valid argument ('meta' or a xml file)");
                    }
                    return Task.CompletedTask;
                });

            builder
                .RegisterCmdLineAction("repairschema", "checks the schema against the deployed schema and tries to correct deviations",
                async scope =>
                {
                    await scope.Resolve<IServer>().CheckSchema(true);
                });

            builder
               .RegisterCmdLineAction("updatedeployedschema", "updates the schema to the current metadata",
                scope =>
                {
                    scope.Resolve<IServer>().UpdateSchema();
                    return Task.CompletedTask;
                });

            builder
               .RegisterCmdLineListAction("updateschema=", "updates the schema to the specified xml file(s)",
               (scope, args) =>
               {
                   scope.Resolve<IServer>().UpdateSchema(args);
                    return Task.CompletedTask;
               });

            builder
                .RegisterCmdLineAction("syncidentities:", "synchronices local and domain users with Zetbox Identities. Optionally a implementation-defined source can be specified",
                (scope, args) =>
                {
                    scope.Resolve<IServer>().SyncIdentities(args);
                    return Task.CompletedTask;
                });

            builder
                .RegisterCmdLineAction("analyze=", "analyzes the configured database",
                (scope, arg) =>
                {
                    scope.Resolve<IServer>().AnalyzeDatabase(arg, File.CreateText(string.Format("{0} Report.txt", arg)));
                    return Task.CompletedTask;
                });

            builder
                .RegisterCmdLineAction("installperfcounter", "Installs/Reinstalls the perfomance counters",
                scope =>
                {
                    scope.Resolve<IPerfCounter>().Install();
                    return Task.CompletedTask;
                });

            builder
                .RegisterCmdLineAction("uninstallperfcounter", "Uninstalls the perfomance counters",
                scope =>
                {
                    scope.Resolve<IPerfCounter>().Uninstall();
                    return Task.CompletedTask;
                });

            builder
                .RegisterCmdLineAction("benchmark", "[DEVEL] run ad-hoc benchmarks against the database",
                scope =>
                {
                    scope.Resolve<IServer>().RunBenchmarks();
                    return Task.CompletedTask;
                });

            builder
                .RegisterCmdLineAction("fix", "[DEVEL] run ad-hoc fixes against the database",
                scope =>
                {
                    scope.Resolve<IServer>().RunFixes();
                    return Task.CompletedTask;
                });

            builder
                .RegisterCmdLineAction("wipe", "[DEVEL] completely wipe the contents of the database",
                scope =>
                {
                    scope.Resolve<IServer>().WipeDatabase();
                    return Task.CompletedTask;
                });
            builder
                .RegisterCmdLineAction("wait-for-database", "Waits until the database is online. This is useful in a Docker scenario.",
                scope =>
                {
                    scope.Resolve<IServer>().WaitForDatabase();
                    return Task.CompletedTask;
                });

            builder
                .RegisterCmdLineListAction("recalc-all:", "Recalculate calculated properties. This may be needed if the implementation has changed and no proper migration is in place. If no ; seperated list of properties is provided, all properties will be recalculated. e.g -recalc-all or -recalc-all=module.objclass.prop;module.objclass.prop2",
                 (scope, args) =>
                 {
                     if (args == null || args.Length == 0)
                     {
                         // recalculate all
                         scope.Resolve<IServer>().RecalculateProperties(null);
                     }
                     else
                     {
                         var ctx = scope.Resolve<IZetboxServerContext>();
                         var properties = ParseProperties(args, ctx);
                         scope.Resolve<IServer>().RecalculateProperties(properties.ToArray());
                     }
                    return Task.CompletedTask;
                 });

            builder
                .RegisterCmdLineAction("refresh-rights", "Recalculate all right tables in the database",
                scope =>
                {
                    scope.Resolve<IServer>().RefreshRights();
                    return Task.CompletedTask;
                });
        }

        private static List<App.Base.Property> ParseProperties(string[] args, IZetboxServerContext ctx)
        {
            var properties = new List<App.Base.Property>();
            foreach (var prop in args)
            {
                var parts = prop.Split('.');
                if (parts.Length != 3)
                {
                    Logging.Log.ErrorFormat("Argument '{0}' is not in the right format. Format is ModuleName.ObjectClass.PropertyName", prop);
                    continue;
                }

                var propName = parts[2];
                var clsName = parts[1];
                var moduleName = parts[0];
                var obj = ctx.GetQuery<App.Base.Property>().FirstOrDefault(p => p.Name == propName && p.ObjectClass.Name == clsName && p.ObjectClass.Module.Name == moduleName);
                if (obj == null)
                {
                    if (!ctx.GetQuery<App.Base.Module>().Any(m => m.Name == moduleName))
                    {
                        Logging.Log.ErrorFormat("Module '{0}' not found", moduleName);
                    }
                    else if (!ctx.GetQuery<App.Base.ObjectClass>().Any(cls => cls.Name == clsName && cls.Module.Name == moduleName))
                    {
                        Logging.Log.ErrorFormat("ObjectClass '{0}' not found in Module '{1}'", clsName, moduleName);
                    }
                    else
                    {
                        Logging.Log.ErrorFormat("Property '{0}' was not found in ObjectClass '{1}' in Module '{2}'", propName, clsName, moduleName);
                    }
                }
                else
                {
                    properties.Add(obj);
                }
            }
            return properties;
        }

        private static void ParseModules(ZetboxConfig config, out string[] schemaModulesArray, out string[] ownerModulesArray)
        {
            List<string> schemaModules;
            if (config.AdditionalCommandlineOptions.TryGetValue(SchemaModulesKey, out schemaModules))
            {
                schemaModulesArray = schemaModules.SelectMany(s => s.Split(new char[] { Path.PathSeparator }, StringSplitOptions.RemoveEmptyEntries)).ToArray();
            }
            else
            {
                schemaModulesArray = new string[] { "*" };
            }

            List<string> ownerModules;
            if (config.AdditionalCommandlineOptions.TryGetValue(OwnerModulesKey, out ownerModules))
            {
                ownerModulesArray = ownerModules.SelectMany(s => s.Split(new char[] { Path.PathSeparator }, StringSplitOptions.RemoveEmptyEntries)).ToArray();
            }
            else
            {
                ownerModulesArray = new string[] { "*" };
            }
        }
    }
}
