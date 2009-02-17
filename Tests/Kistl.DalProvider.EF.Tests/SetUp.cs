using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.API.Server.Mocks;
using System.IO;
using System.Data.SqlClient;
using System.Diagnostics;

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
                var databaseScript = File.ReadAllText(@"P:\Kistl\Kistl.Server\Database\Database.61.sql");
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
