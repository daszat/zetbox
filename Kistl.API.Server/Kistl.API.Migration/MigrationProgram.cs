
namespace Kistl.API.Migration
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Autofac.Configuration;
    using Kistl.API.Configuration;
    using Kistl.API.Server;
    using Kistl.API.Utils;
    using ZBox.App.SchemaMigration;

    public abstract class MigrationProgram
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.API.Migration");
        private readonly static object _lock = new object();
        private static bool _isInitialized = false;

        private readonly string _name;
        protected string Name { get { return _name; } }

        private readonly string[] _arguments;
        protected IEnumerable<string> Arguments { get { return _arguments; } }

        private OptionSet _options;
        protected OptionSet Options { get { return _options; } }

        private KistlConfig _config;
        protected KistlConfig Config { get { return _config; } }

        private IContainer _container;
        protected IContainer Container { get { return _container; } }

        private ILifetimeScope _applicationScope;
        protected ILifetimeScope ApplicationScope { get { return _applicationScope; } }

        protected MigrationProgram(string name, string[] arguments)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            if (arguments == null) throw new ArgumentNullException("arguments");

            _name = name;
            _arguments = arguments;
        }

        private void Initialize()
        {
            Logging.Configure();
            Log.InfoFormat("Starting Migration for [{0}] with arguments [{0}]", _name, String.Join("], [", _arguments));

            _options = CreateOptionSet();

            List<string> extraArguments = null;
            try
            {
                extraArguments = _options.Parse(_arguments);
            }
            catch (OptionException e)
            {
                Log.Fatal("Error in commandline", e);
                PrintHelpAndExit();
            }

            _config = ReadConfig(extraArguments);

            AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, _config);

            _container = CreateMasterContainer(_config);

            _applicationScope = _container.BeginLifetimeScope();

            ValidateConfig();

            _isInitialized = true;
        }

        protected virtual void PrintHelpAndExit()
        {
            PrintHelp();
            Environment.Exit(1);
        }

        protected virtual void PrintHelp()
        {
            if (_options != null)
            {
                _options.WriteOptionDescriptions(Console.Out);
            }
            else
            {
                Log.Fatal("Error while generating commandline help");
            }
        }

        protected virtual OptionSet CreateOptionSet()
        {
            return new OptionSet();
        }

        protected virtual KistlConfig ReadConfig(List<string> extraArguments)
        {
            string configFilePath = null;
            if (extraArguments == null)
            {
                configFilePath = "DefaultConfig.xml";
            }
            else if (extraArguments != null && extraArguments.Count == 1)
            {
                configFilePath = extraArguments.Single();
            }
            else
            {
                Log.FatalFormat("Unrecognized Arguments: [{0}]", String.Join("], [", extraArguments.ToArray()));
                PrintHelpAndExit();
            }

            Log.DebugFormat("Trying to load config from [{0}]", configFilePath);
            try
            {
                return KistlConfig.FromFile(configFilePath);
            }
            catch (Exception ex)
            {
                Log.Fatal(String.Format("Fehler beim Lesen der Config von [{0}]", configFilePath), ex);
                PrintHelpAndExit();
            }
            // never reached
            return null;
        }

        protected virtual void ValidateConfig() { }

        protected virtual IContainer CreateMasterContainer(KistlConfig config)
        {
            var builder = AutoFacBuilder.CreateContainerBuilder(config, config.Server.Modules);

            ConfigureBuilder(builder);

            // register deployment-specific components
            builder.RegisterModule(new ConfigurationSettingsReader("migrationcomponents"));

            return builder.Build();
        }

        protected virtual void ConfigureBuilder(ContainerBuilder builder) { }

        protected void Execute()
        {
            if (!_isInitialized)
            {
                Initialize();
            }
            else
            {
                throw new InvalidOperationException("Already executed");
            }

            using (Log.InfoTraceMethodCallFormat("Executing migration for [{0}]", _name))
            {
                ExecuteCore(_applicationScope.Resolve<IKistlContext>());
            }
        }

        protected abstract void ExecuteCore(IKistlContext ctx);

        protected void ReloadStaging(StagingDatabase stage)
        {
            if (String.IsNullOrEmpty(stage.OriginProvider) || String.IsNullOrEmpty(stage.OriginConnectionString))
            {
                Log.DebugFormat("Skipping staging reload for [{0}] because of empty Origin", stage.Description);
                return;
            }

            using (Log.InfoTraceMethodCallFormat("Reloading staging database [{0}]", stage.Description))
            using (var reloadScope = _applicationScope.BeginLifetimeScope())
            {
                var srcSchema = OpenSource(reloadScope, stage.OriginProvider, stage.OriginConnectionString);
                var dstSchema = OpenSource(reloadScope, stage.Provider, stage.ConnectionString);

                dstSchema.DropAllObjects();

                foreach (var tbl in srcSchema.GetTableNames())
                {
                    Log.InfoFormat("Migrating table {0}", tbl);
                    var cols = srcSchema.GetTableColumns(tbl);
                    dstSchema.CreateTable(tbl, cols);

                    var colNames = cols.Select(i => i.Name).ToArray();

                    using (IDataReader rd = srcSchema.ReadTableData(tbl, colNames))
                    {
                        dstSchema.WriteTableData(tbl, rd, colNames);
                    }
                }
            }
        }

        protected static ISchemaProvider OpenSource(ILifetimeScope scope, string provider, string connectionString)
        {
            var srcSchema = scope.Resolve<ISchemaProvider>(provider);
            try
            {
                srcSchema.Open(connectionString);
            }
            catch (Exception ex)
            {
                Log.Fatal(String.Format("Error while opening origin [{0}] for re-staging", connectionString), ex);
                throw;
            }
            return srcSchema;
        }
    }
}
