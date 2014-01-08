//---------------------------------------------------------------------
// This is an optional program to migrate existing databases to zetbox.
// If you don't need a migration, delete this project
//---------------------------------------------------------------------

namespace $safeprojectname$
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Migration;
    using Zetbox.API.Server;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.GUI;
    using Zetbox.App.SchemaMigration;
    using System.ComponentModel;

    public class Program : MigrationProgram
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("$safeprojectname$");

        private readonly static Guid MigrationProjectGUID = new Guid("????????-????-????-????-????????????");
        private readonly static Guid StagingDatabaseGUID = new Guid("????????-????-????-????-????????????");

        #region Main Program
        static int Main(string[] arguments)
        {
            try
            {
                var program = new Program("$safeprojectname$", arguments);

                program.Execute();

                Log.Warn("Migration successfull.");
                return 0;
            }
            catch (Exception ex)
            {
                Log.Error("Error in program execution", ex);
                return 1;
            }
        }

        private Program(string name, string[] arguments)
            : base(name, arguments)
        {
        }
        #endregion

        #region Private Fields
        private ISchemaProvider dst;
        private ISchemaProvider source;
        private IMigrationTasks executor;
        private Dictionary<string, SourceTable> tables;
        #endregion

        #region Create OptionSet
        private bool reloadstaging = false;
        protected override OptionSet CreateOptionSet()
        {
            var options = base.CreateOptionSet();
            options.Add("reloadstaging", (arg) => reloadstaging = true);
            return options;
        }
        #endregion

        #region Migration main function
        protected override void ExecuteCore(IZetboxServerContext ctx)
        {
            try
            {
                if (reloadstaging)
                {
                    ReloadStaging(ctx);
                }
                else
                {
                    Prepare(ctx);
                    ClearDestination(ctx);
                    CreateDefaultData(ctx);
                    Migrate(ctx);
                    UpdateImportedData(ctx);
                    CreateReportingSchema(ctx);
                }
            }
            catch (MigrationException ex)
            {
                Logging.MailNotification.Error("Migration failed", ex);
                throw;
            }
            catch (Exception ex)
            {
                Logging.MailNotification.Error("Migration failed unexpectedly", ex);
                throw;
            }
        }
        #endregion

        #region ReloadStaging
        private void ReloadStaging(IZetboxContext ctx)
        {
            using (Log.InfoTraceMethodCall("reloading staging"))
            {
                // TODO: use named objects
                var mp = ctx.FindPersistenceObject<MigrationProject>(MigrationProjectGUID);

                if (mp == null)
                {
                    throw new InvalidOperationException("Migrationsprojekt not found");
                }

                foreach (var stage in mp.StagingDatabases)
                {
                    ReloadStaging(stage);
                }
                Logging.MailNotification.Info("Staging Load finished");
            }
        }
        #endregion

        #region Prepare
        private void Prepare(IZetboxContext ctx)
        {
            var connectionString = Config.Server.GetConnectionString(Helper.ZetboxConnectionStringKey);
            dst = OpenProvider(ApplicationScope, connectionString.SchemaProvider, connectionString.ConnectionString);

            // TODO: use named objects
            var stage = ctx.FindPersistenceObject<StagingDatabase>(StagingDatabaseGUID);
            var connectionStringStage = Config.Server.GetConnectionString(stage.ConnectionStringKey);
            source = OpenProvider(ApplicationScope, connectionStringStage.SchemaProvider, connectionStringStage.ConnectionString);

            executor = ApplicationScope.Resolve<TaskFactory>().Invoke(source, dst);

            tables = stage.SourceTables.Where(t => t.DestinationObjectClass != null).ToDictionary(t => t.Name);

            // Prepare Staging Structure
            source.ExecuteSqlResource(this.GetType(), "$safeprojectname$.Scripts.CreateStagingAggregationDatabase.sql");
        }
        #endregion

        /// <summary>
        /// Clears the destination zetbox tables. Due to FKs, ordering is required.
        /// </summary>
        private void ClearDestination(IZetboxContext ctx)
        {
            foreach (var log in ctx.GetQuery<MigrationLog>())
            {
                ctx.Delete(log);
            }
            ctx.SubmitChanges();

            // required ordering            
            //executor.CleanDestination(tables["tbl_Cars"]);
            //executor.CleanDestination(tables["tbl_Customers"]);
        }

        /// <summary>
        /// Creates any default data needed by the migration program
        /// </summary>
        private void CreateDefaultData(IZetboxContext ctx)
        {
            // Create default data
            // ctx.SubmitChanges();
        }

        /// <summary>
        /// The main migration function. Ordering is required 
        /// </summary>
        private void Migrate(IZetboxContext ctx)
        {
            // required ordering
            //executor.TableBaseMigration(tables["tbl_Customers"], 
            //    new NullConverter(tables["tbl_Customers"].Column("Name"), string.Empty, "Name is missing."));
            //executor.TableBaseMigration(tables["tbl_Cars"],
            //    new NullConverter(tables["tbl_Cars"].Column("Name"), string.Empty, "Name is missing."));

            // update statistics to not blow up postgres' optimizer
            dst.RefreshDbStats();
        }

        /// <summary>
        /// Optional updates on the migrated data
        /// </summary>
        private void UpdateImportedData(IZetboxContext ctx)
        {
            dst.ExecRefreshAllRightsProcedure();
        }

        /// <summary>
        /// Optional, (re)create any views or stored procedures needed for reporting
        /// </summary>
        private void CreateReportingSchema(IZetboxServerContext ctx)
        {
            if (dst.CheckSchemaExists("Reports"))
            {
                dst.DropSchema("Reports", true);
            }

            dst.CreateSchema("Reports");
            dst.ExecuteSqlResource(this.GetType(), "$safeprojectname$.Scripts.CreateReportViews.sql");
        }

        #region Internals
        protected override void CreateReport()
        {
            if (tables != null)
            {
                CreateMigrationReport(tables.Values.Where(tbl => tbl.Status == MappingStatus.Mapped), source, dst);
            }
            Logging.MailNotification.Info("Migration finished");
        }
        #endregion
    }
}
