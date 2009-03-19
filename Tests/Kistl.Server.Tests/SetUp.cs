using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Configuration;
using Kistl.App.GUI;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Kistl.Server.Tests
{
    [SetUpFixture]
    public class SetUp : IDisposable
    {
        private Server manager;

        [SetUp]
        public void Init()
        {
            System.Diagnostics.Trace.WriteLine("Setting up Kistl");

            var config = KistlConfig.FromFile("DefaultConfig_Server.Tests.xml");

            manager = new Server();
            manager.Start(config);

            Trace.TraceInformation("Resetting Database");
            using (var db = new SqlConnection(ApplicationContext.Current.Configuration.Server.ConnectionString))
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

            System.Diagnostics.Trace.WriteLine("Setting up Kistl finished");
        }

        [TearDown]
        public void TearDown()
        {
            lock (typeof(SetUp))
            {
                if (manager != null)
                {
                    System.Diagnostics.Trace.WriteLine("Shutting down Kistl");
                    manager.Stop();
                    manager = null;
                    System.Diagnostics.Trace.WriteLine("Shutting down Kistl finished");
                }
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            TearDown();
        }

        #endregion
    }
}
