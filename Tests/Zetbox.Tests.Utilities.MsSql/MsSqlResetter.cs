
namespace Zetbox.Tests.Utilities.MsSql
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Zetbox.API.AbstractConsumerTests;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using NUnit.Framework;

    public sealed class MsSqlResetter
        : IDatabaseResetter
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Tests.MsSqlUtils");

        private readonly ZetboxConfig config;

        public MsSqlResetter(ZetboxConfig config)
        {
            this.config = config;
        }

        public string ForceTestDB(string connectionString)
        {
            var cb = new SqlConnectionStringBuilder(connectionString);
            if (!cb.InitialCatalog.EndsWith("_test"))
            {
                cb.InitialCatalog += "_test";
            }

            return cb.ToString();
        }

        public void ResetDatabase()
        {
            using (Log.InfoTraceMethodCall("ResetDatabase"))
            {
                var connectionString = config.Server.GetConnectionString(Zetbox.API.Helper.ZetboxConnectionStringKey);
                Assert.That(connectionString.ConnectionString, Is.StringContaining("_test"), "test databases should be marked with '_test' in the connection string");

                try
                {
                    Log.Info("Restoring Database");
                    var cb = new SqlConnectionStringBuilder(connectionString.ConnectionString);
                    cb.InitialCatalog = "master";
                    Log.InfoFormat("executing on database [{0}]", cb.ToString());
                    ExecuteScript(cb.ToString(), "Zetbox.Tests.Utilities.MsSql.BackupRestoreTestDatabase.sql");
                    Log.Info("Done restoring Database");

                    // After recreating the database, all connection pools should be cleard
                    SqlConnection.ClearAllPools();
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
            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                var scriptStream = new StreamReader(this.GetType().Assembly.GetManifestResourceStream(scriptResource));
                var databaseScript = scriptStream.ReadToEnd();
                foreach (var cmdString in Regex.Split(databaseScript, "\r?\nGO\r?\n").Where(s => !String.IsNullOrEmpty(s)))
                {
                    using (var cmd = new SqlCommand(cmdString, db))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
