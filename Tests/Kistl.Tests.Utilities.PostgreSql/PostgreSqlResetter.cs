
namespace Kistl.Tests.Utilities.PostgresSql
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Kistl.API.AbstractConsumerTests;
    using Kistl.API.Configuration;
    using Kistl.API.Utils;
    using Npgsql;
    using NUnit.Framework;
    using Kistl.API.Server;
    using Autofac;

    public sealed class PostgreSqlResetter
        : IDatabaseResetter
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Tests.PostgreSqlUtils");

        private readonly KistlConfig config;
        private readonly ISchemaProvider schemaProvider;

        private const int RESET_TIMEOUT = 2 * 60;

        public PostgreSqlResetter(KistlConfig config, Autofac.ILifetimeScope scope)
        {
            this.config = config;
            this.schemaProvider = scope.ResolveNamed<ISchemaProvider>("POSTGRESQL");
        }

        public void ResetDatabase()
        {
            using (Log.InfoTraceMethodCall("ResetDatabase"))
            {
                Assert.That(config.Server.ConnectionString, Is.StringContaining("_test"), "test databases should be marked with '_test' in the connection string");

                try
                {
                    Log.Info("Restoring Database");
                    schemaProvider.Open(config.Server.ConnectionString);
                    schemaProvider.DropAllObjects();

                    var cb = new NpgsqlConnectionStringBuilder(config.Server.ConnectionString);
                    var srcDB = cb.Database.Substring(0, cb.Database.Length - "_test".Length);
                    var destDB = cb.Database;
                    var userCmdString = "-U postgres -w";
                    var args = string.Format("/C pg_dump {0} {1} | psql {0} {2}", userCmdString, srcDB, destDB);
                    System.Diagnostics.ProcessStartInfo pi = new System.Diagnostics.ProcessStartInfo("cmd.exe", args);
                    pi.UseShellExecute = false;
                    var pgSQLBinPath = Environment.GetEnvironmentVariable("PGSQLBinPath");
                    if (string.IsNullOrEmpty(pgSQLBinPath))
                    {
                        throw new InvalidOperationException("Environment Variable PGSQLBinPath is not set, unable to reset test database");
                    }
                    pi.WorkingDirectory = pgSQLBinPath.Trim('\"');
                    var p = System.Diagnostics.Process.Start(pi);
                    if (!p.WaitForExit(RESET_TIMEOUT * 1000))
                    {
                        throw new InvalidOperationException(string.Format("pg_dump did not completed within {0} seconds", RESET_TIMEOUT));
                    }

                    // After recreating the database, all connection pools should be cleard
                    NpgsqlConnection.ClearAllPools();
                }
                catch (Exception ex)
                {
                    Log.Error("Error while restoring database", ex);
                    throw;
                }

                Log.InfoFormat("Current Directory=[{0}]", Environment.CurrentDirectory);
                Log.InfoFormat("Using config from [{0}]", config.ConfigFilePath);
            }
        }

        /// <summary>
        /// Transactions should be used within the SQL Script
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="scriptResource"></param>
        private void ExecuteScript(string connectionString, string scriptResource)
        {
            using (var db = new NpgsqlConnection(connectionString))
            {
                db.Open();
                var scriptStream = new StreamReader(this.GetType().Assembly.GetManifestResourceStream(scriptResource));
                var databaseScript = scriptStream.ReadToEnd();
                foreach (var cmdString in Regex.Split(databaseScript, "\r?\nGO\r?\n").Where(s => !String.IsNullOrEmpty(s)))
                {
                    using (var cmd = new NpgsqlCommand(cmdString, db))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
