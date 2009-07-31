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
            Assert.That(config.Server.ConnectionString, Text.Contains("_test"), "test databases should be marked with '_test' in the connection string");

            try
            {
                Trace.TraceInformation("Killing Database");
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
                Trace.TraceInformation("Done Killing Database");
            }
            catch (Exception error)
            {
                Trace.TraceError("Error ({0}) while killing database: {1}", error.GetType().Name, error.Message);
                Trace.TraceError(error.ToString());
                Trace.TraceError(error.StackTrace);

                throw error;
            }

            Trace.TraceInformation(Environment.CurrentDirectory);
            Trace.TraceInformation("Using config: " + config.ConfigFilePath);

            LoadSchema(config, basePath);
            LoadData(config, basePath);
            GenerateSource(config, basePath);
        }

        private static void LoadSchema(KistlConfig config, string basePath)
        {
            AppDomain initializer = null;
            try
            {
                Trace.TraceInformation("Loading Schema");
                initializer = AppDomain.CreateDomain("DatabaseResetup1", null, basePath, @"bin\Debug", true);
                var server = (Kistl.Server.Server)initializer.CreateInstanceAndUnwrap(typeof(Kistl.Server.Server).Assembly.FullName, typeof(Kistl.Server.Server).FullName);
                server.Init(config);
                server.UpdateSchema(basePath + @"\Database\Database.xml");
            }
            finally
            {
                if (initializer != null)
                {
                    AppDomain.Unload(initializer);
                }
            }
        }

        private static void LoadData(KistlConfig config, string basePath)
        {
            AppDomain domain = null;
            try
            {
                Trace.TraceInformation("Loading Data");
                domain = AppDomain.CreateDomain("DatabaseResetup2", null, basePath, @"bin\Debug", true);
                var server = (Kistl.Server.Server)domain.CreateInstanceAndUnwrap(typeof(Kistl.Server.Server).Assembly.FullName, typeof(Kistl.Server.Server).FullName);
                server.Init(config);
                server.Deploy(basePath + @"\Database\Database.xml");
                server.UpdateSchema();
                server.CheckSchema(false);
            }
            finally
            {
                if (domain != null)
                {
                    AppDomain.Unload(domain);
                }
            }
        }

        private static void GenerateSource(KistlConfig config, string basePath)
        {
            AppDomain domain = null;
            try
            {
                Trace.TraceInformation("Generating Source");
                domain = AppDomain.CreateDomain("DatabaseResetup3", null, basePath, @"bin\Debug", true);
                var server = (Kistl.Server.Server)domain.CreateInstanceAndUnwrap(typeof(Kistl.Server.Server).Assembly.FullName, typeof(Kistl.Server.Server).FullName);
                server.Init(config);
                server.GenerateCode();
            }
            finally
            {
                if (domain != null)
                {
                    AppDomain.Unload(domain);
                }
            }
        }
    }
}
