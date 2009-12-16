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
            //string basePath = @"..\..\..\..\Kistl.Server";
            Assert.That(config.Server.ConnectionString, Is.StringContaining("_test"), "test databases should be marked with '_test' in the connection string");

            try
            {
                Trace.TraceInformation("Restoring Database");
                var cb = new SqlConnectionStringBuilder(config.Server.ConnectionString);
                cb.InitialCatalog = "master";
                Trace.TraceInformation("executing on database " + cb.ToString());
                ExecuteScript(cb.ToString(), "Kistl.Server.Database.Scripts.BackupRestoreTestDatabase.sql");
                Trace.TraceInformation("Done restoring Database");
            }
            catch (Exception error)
            {
                Trace.TraceError("Error ({0}) while restoring database: {1}", error.GetType().Name, error.Message);
                Trace.TraceError(error.ToString());
                Trace.TraceError(error.StackTrace);

                throw error;
            }

            Trace.TraceInformation(Environment.CurrentDirectory);
            Trace.TraceInformation("Using config: " + config.ConfigFilePath);

            //LoadSchema(config, basePath);
            //LoadData(config, basePath);
            // Do not generate source - fullreset has done this for us
            // GenerateSource(config, basePath);
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

        //private static void LoadSchema(KistlConfig config, string basePath)
        //{
        //    AppDomain initializer = null;
        //    try
        //    {
        //        Trace.TraceInformation("Loading Schema");
        //        // initializer = AppDomain.CreateDomain("DatabaseResetup1", null, basePath, @"bin\Debug", true);
        //        initializer = AppDomain.CreateDomain("DatabaseResetup1", null, AppDomain.CurrentDomain.SetupInformation);
        //        var server = (Kistl.Server.Server)initializer.CreateInstanceAndUnwrap(typeof(Kistl.Server.Server).Assembly.FullName, typeof(Kistl.Server.Server).FullName);
        //        server.Init(config);
        //        server.UpdateSchema(basePath + @"\Database\Database.xml");
        //    }
        //    finally
        //    {
        //        if (initializer != null)
        //        {
        //            AppDomain.Unload(initializer);
        //        }
        //    }
        //}

        //private static void LoadData(KistlConfig config, string basePath)
        //{
        //    AppDomain domain = null;
        //    try
        //    {
        //        Trace.TraceInformation("Loading Data");
        //        //domain = AppDomain.CreateDomain("DatabaseResetup2", null, basePath, @"bin\Debug", true);
        //        domain = AppDomain.CreateDomain("DatabaseResetup2", null, AppDomain.CurrentDomain.SetupInformation);
        //        var server = (Kistl.Server.Server)domain.CreateInstanceAndUnwrap(typeof(Kistl.Server.Server).Assembly.FullName, typeof(Kistl.Server.Server).FullName);
        //        server.Init(config);
        //        server.Deploy(basePath + @"\Database\Database.xml");
        //        server.UpdateSchema();
        //        server.CheckSchema(false);
        //    }
        //    finally
        //    {
        //        if (domain != null)
        //        {
        //            AppDomain.Unload(domain);
        //        }
        //    }
        //}

        //private static void GenerateSource(KistlConfig config, string basePath)
        //{
        //    AppDomain domain = null;
        //    try
        //    {
        //        Trace.TraceInformation("Generating Source");
        //        //domain = AppDomain.CreateDomain("DatabaseResetup3", null, basePath, @"bin\Debug", true);
        //        domain = AppDomain.CreateDomain("DatabaseResetup3", null, AppDomain.CurrentDomain.SetupInformation);
        //        var server = (Kistl.Server.Server)domain.CreateInstanceAndUnwrap(typeof(Kistl.Server.Server).Assembly.FullName, typeof(Kistl.Server.Server).FullName);
        //        server.Init(config);
        //        server.GenerateCode();
        //    }
        //    finally
        //    {
        //        if (domain != null)
        //        {
        //            AppDomain.Unload(domain);
        //        }
        //    }
        //}
    }
}
