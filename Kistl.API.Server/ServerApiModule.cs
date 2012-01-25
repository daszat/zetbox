
namespace Kistl.API.Server
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API.Configuration;
    using Kistl.API.Server.PerfCounter;
    using Kistl.API.Utils;

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
                .RegisterModule<Log4NetAppender.Module>();

            builder
                .RegisterType<PerfCounterDispatcher>()
                .As<IPerfCounter>()
                .OnActivated(args => args.Instance.Initialize(args.Context.Resolve<IFrozenContext>()))
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
                .RegisterCmdLineAction("deploy=", "deploy the database from the specified xml file",
                (scope, arg) =>
                {
                    scope.Resolve<IServer>().Deploy(arg);
                });

            builder
                .RegisterCmdLineAction("deploy-local:", "deploy all modules from the local Modules directory",
                (scope, arg) =>
                {
                    var srv = scope.Resolve<IServer>();
                    Directory.GetFiles(arg ?? "Modules", "*.xml", SearchOption.TopDirectoryOnly)
                        .ForEach(srv.Deploy);
                });

            builder
                .RegisterCmdLineAction("import=", "import the database from the specified xml file",
                (scope, arg) =>
                {
                    scope.Resolve<IServer>().Import(arg);
                });

            builder
                .RegisterCmdLineAction("export=", "export the database to the specified xml file. Select the exported data with -schemamodules and -ownermodules",
                (scope, arg) =>
                {
                    var config = scope.Resolve<KistlConfig>();
                    string[] schemaModulesArray;
                    string[] ownerModulesArray;
                    ParseModules(config, out schemaModulesArray, out ownerModulesArray);
                    scope.Resolve<IServer>().Export(
                        arg,
                        schemaModulesArray,
                        ownerModulesArray);
                });

            builder
                .RegisterCmdLineAction("publish=", "publish the specified modules to this xml file. Select the exported data with -ownermodules",
                (scope, arg) =>
                {
                    var config = scope.Resolve<KistlConfig>();
                    string[] schemaModulesArray;
                    string[] ownerModulesArray;
                    ParseModules(config, out schemaModulesArray, out ownerModulesArray);
                    scope.Resolve<IServer>().Publish(
                        arg,
                        ownerModulesArray);
                });

            builder
                .RegisterCmdLineAction("checkdeployedschema", "checks the sql schema against the deployed schema",
                (scope, arg) =>
                {
                    scope.Resolve<IServer>().CheckSchema(false);
                });

            builder
                .RegisterCmdLineAction("checkschema=", "checks the sql schema against the metadata (parameter 'meta') or a specified xml file",
                (scope, arg) =>
                {
                    if (arg == "meta")
                    {
                        scope.Resolve<IServer>().CheckSchemaFromCurrentMetaData(false);
                    }
                    else
                    {
                        scope.Resolve<IServer>().CheckSchema(arg, false);
                    }
                });

            builder
                .RegisterCmdLineAction("repairschema", "checks the schema against the deployed schema and tries to correct deviations",
                (scope, arg) =>
                {
                    scope.Resolve<IServer>().CheckSchema(true);
                });

            builder
               .RegisterCmdLineAction("updatedeployedschema", "updates the schema to the current metadata",
               (scope, arg) =>
               {
                   scope.Resolve<IServer>().UpdateSchema();
               });

            builder
               .RegisterCmdLineAction("updateschema=", "updates the schema to the specified xml file(s)",
               (scope, arg) =>
               {
                   scope.Resolve<IServer>().UpdateSchema(arg.Split(";:".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
               });

            builder
                .RegisterCmdLineAction("syncidentities", "synchronices local and domain users with Kistl Identities",
                (scope, arg) =>
                {
                    scope.Resolve<IServer>().SyncIdentities();
                });

            builder
                .RegisterCmdLineAction("analyze=", "analyzes the configured database",
                (scope, arg) =>
                {
                    scope.Resolve<IServer>().AnalyzeDatabase(arg, File.CreateText(string.Format("{0} Report.txt", arg)));
                });

            builder
                .RegisterCmdLineAction("installperfcounter", "Installs/Reinstalls the perfomance counters",
                (scope, arg) =>
                {
                    scope.Resolve<IPerfCounter>().Install();
                });

            builder
                .RegisterCmdLineAction("uninstallperfcounter", "Uninstalls the perfomance counters",
                (scope, arg) =>
                {
                    scope.Resolve<IPerfCounter>().Uninstall();
                });

            builder
                .RegisterCmdLineAction("benchmark", "[DEVEL] run ad-hoc benchmarks against the database",
                (scope, arg) =>
                {
                    scope.Resolve<IServer>().RunBenchmarks();
                });

            builder
                .RegisterCmdLineAction("fix", "[DEVEL] run ad-hoc fixes against the database",
                (scope, arg) =>
                {
                    scope.Resolve<IServer>().RunFixes();
                });

            builder
                .RegisterCmdLineAction("wipe", "[DEVEL] completely wipe the contents of the database",
                (scope, arg) =>
                {
                    scope.Resolve<IServer>().WipeDatabase();
                });
        }

        private static void ParseModules(KistlConfig config, out string[] schemaModulesArray, out string[] ownerModulesArray)
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
