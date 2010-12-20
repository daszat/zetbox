
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

        public PostgreSqlResetter(KistlConfig config, Autofac.ILifetimeScope scope)
        {
            this.config = config;
            this.schemaProvider = scope.ResolveNamed<ISchemaProvider>("POSTGRESQL");
        }

        public void ResetDatabase()
        {
            using (Log.InfoTraceMethodCall())
            {
                Assert.That(config.Server.ConnectionString, Is.StringContaining("_test"), "test databases should be marked with '_test' in the connection string");

                try
                {
                    Log.Info("Restoring Database");
                    schemaProvider.Open(config.Server.ConnectionString);
                    schemaProvider.DropAllObjects();

                    System.Diagnostics.ProcessStartInfo pi = new System.Diagnostics.ProcessStartInfo("cmd.exe", "/C pg_dump -U postgres zbox | psql -U postgres zbox_test");
                    pi.UseShellExecute = false;
                    pi.WorkingDirectory = Environment.GetEnvironmentVariable("PGSQLBinPath").Trim('\"');
                    var p = System.Diagnostics.Process.Start(pi);
                    if (!p.WaitForExit(2 * 60 * 1000)) // wait two minutes
                    {
                        throw new InvalidOperationException("pg_dump did not completed within 30 seconds");
                    }

                    //var cb = new NpgsqlConnectionStringBuilder(config.Server.ConnectionString);
                    //Log.Error("Not resetting postgresql database");
                    //Log.InfoFormat("executing on database [{0}]", cb.ToString());
                    //ExecuteScript(cb.ToString(), "Kistl.Tests.Utilities.PostgresSql.RestoreDatabase.sql");
                    //Log.Info("Done restoring Database");

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
