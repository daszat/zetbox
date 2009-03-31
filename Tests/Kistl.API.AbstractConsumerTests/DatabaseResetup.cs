using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API.Configuration;

using NUnit.Framework;

namespace Kistl.API.AbstractConsumerTests
{
    /// <summary>
    /// Resets the database into a known state
    /// </summary>
    public abstract class DatabaseResetup
    {
        // TODO: don't hardcode db script path here
        readonly static string CurrentDbScript = @"P:\Kistl\Kistl.Server\Database\Database.66.sql";

        protected void ResetDatabase(KistlConfig config)
        {
            Assert.That(config.Server.ConnectionString, Text.Contains("_test"), "test databases should be marked with '_test' in the connection string");

            Trace.TraceInformation("Resetting Database");
            using (var db = new SqlConnection(config.Server.ConnectionString))
            {
                db.Open();
                var databaseScript = File.ReadAllText(CurrentDbScript);
                using (var tx = db.BeginTransaction())
                {
                    foreach (var cmdString in databaseScript.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        using (var cmd = new SqlCommand(cmdString, db, tx))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
            }
            Trace.TraceInformation("Done Resetting Database");

        }

    }
}
