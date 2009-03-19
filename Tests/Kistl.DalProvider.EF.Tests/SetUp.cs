using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.DalProvider.EF.Mocks;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Kistl.DalProvider.EF.Tests
{
    [SetUpFixture]
    public class SetUp
    {

        /// <summary>
        /// resets the database to known state
        /// </summary>
        [SetUp]
        public void Init()
        {
            Trace.TraceInformation("Resetting Database");
            var appCtx = new ServerApiContextMock();
            using (var db = new SqlConnection(appCtx.Configuration.Server.ConnectionString))
            {
                db.Open();
                // TODO: don't hardcode db script here
                var databaseScript = File.ReadAllText(@"P:\Kistl\Kistl.Server\Database\Database.63.sql");
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
