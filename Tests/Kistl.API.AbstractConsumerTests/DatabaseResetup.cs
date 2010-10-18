
namespace Kistl.API.AbstractConsumerTests
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    using Kistl.API.Configuration;
    using Kistl.API.Utils;

    using NUnit.Framework;
    
    /// <summary>
    /// Resets the database into a known state
    /// </summary>
    public abstract class DatabaseResetup : AbstractSetUpFixture
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Tests.Api.AbstractConsumer");

        protected void ResetDatabase(KistlConfig config)
        {
            using (Log.InfoTraceMethodCall())
            {
                Assert.That(config.Server.ConnectionString, Is.StringContaining("_test"), "test databases should be marked with '_test' in the connection string");

                try
                {
                    Log.Info("Restoring Database");
                    var cb = new SqlConnectionStringBuilder(config.Server.ConnectionString);
                    cb.InitialCatalog = "master";
                    Log.InfoFormat("executing on database [{0}]", cb.ToString());
                    ExecuteScript(cb.ToString(), "Kistl.Server.Database.Scripts.BackupRestoreTestDatabase.sql");
                    Log.Info("Done restoring Database");

                    // After recreating the database, all connection pools should be cleard
                    System.Data.SqlClient.SqlConnection.ClearAllPools();
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
        /// <param name="useTransaction"></param>
        private static void ExecuteScript(string connectionString, string scriptResource)
        {
            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                var scriptStream = new StreamReader(typeof(Kistl.Server.Helper).Assembly.GetManifestResourceStream(scriptResource));
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
