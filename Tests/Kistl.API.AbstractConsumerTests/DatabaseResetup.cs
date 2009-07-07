using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Kistl.API.Configuration;

using NUnit.Framework;

namespace Kistl.API.AbstractConsumerTests
{
    /// <summary>
    /// Resets the database into a known state
    /// </summary>
    public abstract class DatabaseResetup
    {
        protected void ResetDatabase(KistlConfig config)
        {
            string basePath = @"..\..\..\..\Kistl.Server";
            Console.WriteLine("Killing Database");
            Assert.That(config.Server.ConnectionString, Text.Contains("_test"), "test databases should be marked with '_test' in the connection string");

            try
            {
                Trace.TraceInformation("Resetting Database");
                using (var db = new SqlConnection(config.Server.ConnectionString))
                {
                    db.Open();
                    var scriptStream = new StreamReader(typeof(Kistl.Server.Helper).Assembly.GetManifestResourceStream("Kistl.Server.Database.Scripts.DropTables.sql"));
                    var databaseScript = scriptStream.ReadToEnd();
                    using (var tx = db.BeginTransaction())
                    {
                        foreach (var cmdString in Regex.Split(databaseScript, "\r?\nGO\r?\n").Where(s => !String.IsNullOrEmpty(s)))
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
            catch (Exception error)
            {
                Trace.TraceError("Error ({0}) while resetting database: {1}", error.GetType().Name, error.Message);
                Trace.TraceError(error.ToString());
                Trace.TraceError(error.StackTrace);

                throw error;
            }

            Console.WriteLine("Filling Database");
            Console.WriteLine(Environment.CurrentDirectory);
            Process.Start(basePath + @"\bin\Debug\Kistl.Server.exe", String.Format(@"{0} -updateschema {1}\Database\Database.xml -import {1}\Database\Database.xml -checkschema", config.ConfigFilePath, basePath)).WaitForExit();
            Console.WriteLine("Generate Source");
            Process.Start(basePath + @"\bin\Debug\Kistl.Server.exe", String.Format(@"{0} -generate", config.ConfigFilePath)).WaitForExit();
        }

    }
}
