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

namespace Zetbox.API.Migration
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Autofac.Configuration;
    using Zetbox.API.Configuration;
    using Zetbox.API.Server;
    using Zetbox.API.Utils;
    using Zetbox.App.SchemaMigration;

    public abstract class MigrationProgram
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.API.Migration");
        private static bool _isInitialized = false;

        private readonly string _name;
        protected string Name { get { return _name; } }

        private readonly string[] _arguments;
        protected IEnumerable<string> Arguments { get { return _arguments; } }

        private OptionSet _options;
        protected OptionSet Options { get { return _options; } }

        private ZetboxConfig _config;
        protected ZetboxConfig Config { get { return _config; } }

        private IContainer _container;
        protected IContainer Container { get { return _container; } }

        private ILifetimeScope _applicationScope;
        protected ILifetimeScope ApplicationScope { get { return _applicationScope; } }

        protected MigrationProgram(string name, string[] arguments)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            if (arguments == null)
                throw new ArgumentNullException("arguments");

            _name = name;
            _arguments = arguments;
        }

        private void Initialize()
        {
            Logging.Configure();
            Log.InfoFormat("Starting Migration for [{0}] with arguments [{1}]", _name, String.Join("], [", _arguments));

            _options = CreateOptionSet();

            List<string> extraArguments = null;
            try
            {
                extraArguments = _options.Parse(_arguments);
            }
            catch (OptionException e)
            {
                Logging.MailNotification.Fatal("Error in commandline", e);
                PrintHelpAndExit();
            }

            _config = ReadConfig(extraArguments);

            AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, _config);

            _container = CreateMasterContainer(_config);

            API.AppDomainInitializer.InitializeFrom(_container);

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

        protected virtual ZetboxConfig ReadConfig(List<string> extraArguments)
        {
            string configFilePath = null;
            if (extraArguments == null || extraArguments.Count == 0)
            {
                configFilePath = String.Empty;
            }
            else if (extraArguments.Count == 1)
            {
                configFilePath = extraArguments[0];
            }
            else
            {
                Logging.MailNotification.FatalFormat("Unerkannte Argumente: [{0}]", String.Join("], [", extraArguments.ToArray()));
                PrintHelpAndExit();
            }

            try
            {
                return ZetboxConfig.FromFile(configFilePath, "");
            }
            catch (Exception ex)
            {
                Logging.MailNotification.Fatal(String.Format("Fehler beim Lesen der Config von [{0}]", configFilePath), ex);
                PrintHelpAndExit();
            }
            // never reached
            return null;
        }

        protected virtual void ValidateConfig() { }

        protected virtual IContainer CreateMasterContainer(ZetboxConfig config)
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

            using (Log.InfoTraceMethodCallFormat("Migration", "Executing migration for [{0}]", _name))
            {
                ExecuteCore(_applicationScope.Resolve<IZetboxServerContext>());
            }

            CreateReport();
        }

        protected abstract void CreateReport();

        protected abstract void ExecuteCore(IZetboxServerContext ctx);

        protected void ReloadStaging(StagingDatabase stage)
        {
            if (String.IsNullOrEmpty(stage.OriginConnectionStringKey))
            {
                Log.DebugFormat("Skipping staging reload for [{0}] because of empty OriginConnectionStringKey", stage.Description);
                return;
            }

            var originConnectionString = Config.Server.GetConnectionString(stage.OriginConnectionStringKey);
            var connectionString = Config.Server.GetConnectionString(stage.ConnectionStringKey);

            using (Log.InfoTraceMethodCallFormat("Reload", "Reloading staging database [{0}]", stage.Description))
            using (var reloadScope = _applicationScope.BeginLifetimeScope())
            {
                var srcSchema = OpenProvider(reloadScope, originConnectionString.SchemaProvider, originConnectionString.ConnectionString);
                var dstSchema = OpenProvider(reloadScope, connectionString.SchemaProvider, connectionString.ConnectionString);

                dstSchema.DropSchema(stage.Schema, true);
                dstSchema.CreateSchema(stage.Schema);

                foreach (var tbl in srcSchema.GetTableNames())
                {
                    Log.InfoFormat("Migrating table {0}", tbl);
                    var cols = srcSchema.GetTableColumns(tbl);
                    var dstTableRef = new TableRef(null, stage.Schema, tbl.Name);
                    dstSchema.CreateTable(dstTableRef, cols);

                    var colNames = cols.Select(i => i.Name).ToArray();

                    using (IDataReader rd = srcSchema.ReadTableData(tbl, colNames))
                    {
                        dstSchema.WriteTableData(dstTableRef, rd, colNames);
                    }
                }
            }
        }

        protected static ISchemaProvider OpenProvider(ILifetimeScope scope, string provider, string connectionString)
        {
            var srcSchema = scope.ResolveNamed<ISchemaProvider>(provider);
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

        protected static void CreateMigrationReport(IEnumerable<SourceTable> srcTables, ISchemaProvider srcSchema, ISchemaProvider dstSchema)
        {
            foreach (var srcTbl in srcTables.OrderBy(tbl => tbl.Name))
            {
                var srcCount = srcSchema.CountRows(srcSchema.GetTableName(srcTbl.StagingDatabase.Schema, srcTbl.Name));
                var dstCount = dstSchema.CountRows(dstSchema.GetTableName(srcTbl.DestinationObjectClass.Module.SchemaName, srcTbl.DestinationObjectClass.TableName));

                Log.InfoFormat("Mapped [{0}] rows from [{1}] to [{2}] [{3}] entities",
                    srcCount,
                    srcTbl.Name,
                    dstCount,
                    srcTbl.DestinationObjectClass.Name);
            }
        }

        protected void WriteLog(string srcTbl, long srcRows, string dstTbl, long dstRows)
        {
            using (var logScope = _applicationScope.BeginLifetimeScope())
            using (var logCtx = logScope.Resolve<IZetboxServerContext>())
            {
                var log = logCtx.Create<MigrationLog>();
                log.Timestamp = DateTime.Now;
                log.Source = srcTbl;
                log.SourceRows = (int)srcRows;
                log.Destination = dstTbl;
                log.DestinationRows = (int)dstRows;
                logCtx.SubmitChanges();
            }
        }
    }
}
