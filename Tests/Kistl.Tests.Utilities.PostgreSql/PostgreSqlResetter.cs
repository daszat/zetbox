
namespace Kistl.Tests.Utilities.PostgreSql
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Autofac;
    using Kistl.API.AbstractConsumerTests;
    using Kistl.API.Configuration;
    using Kistl.API.Server;
    using Kistl.API.Utils;
    using Npgsql;
    using NUnit.Framework;
    using Kistl.API;

    public sealed class PostgreSqlResetter
        : IDatabaseResetter
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Tests.PostgreSqlUtils");

        private readonly KistlConfig config;
        private readonly ISchemaProvider schemaManager;
        private readonly ITempFileService tmpService;

        /// <summary>
        /// number of seconds to wait before cancelling _test reset.
        /// </summary>
        private const int RESET_TIMEOUT = 4 * 60;

        public PostgreSqlResetter(KistlConfig config, ISchemaProvider schemaManager, ITempFileService tmpService)
        {
            this.config = config;
            this.schemaManager = schemaManager;
            this.tmpService = tmpService;
        }

        public string ForceTestDB(string connectionString)
        {
            var cb = new NpgsqlConnectionStringBuilder(connectionString);
            if (!cb.Database.EndsWith("_test"))
            {
                cb.Database += "_test";
            }

            return cb.ToString();
        }

        public void ResetDatabase()
        {
            using (Log.InfoTraceMethodCall("ResetDatabase"))
            {
                var connectionString = config.Server.GetConnectionString(Kistl.API.Helper.KistlConnectionStringKey);
                Assert.That(connectionString.ConnectionString, Is.StringContaining("_test"), "test databases should be marked with '_test' in the connection string");

                Log.InfoFormat("Current Directory=[{0}]", Environment.CurrentDirectory);
                Log.InfoFormat("Using config from [{0}]", config.ConfigFilePath);

                try
                {
                    Log.Info("Restoring Database");

                    var cb = new NpgsqlConnectionStringBuilder(connectionString.ConnectionString);
                    var srcDB = cb.Database.Substring(0, cb.Database.Length - "_test".Length);
                    var destDB = cb.Database;
                    var userCmdString = "--username=sa --no-password";
                    var dumpFile = tmpService.CreateWithExtension(".zbox.backup");

                    try
                    {
                        var exitCode = RunPgUtil("pg_dump", String.Format("--format c {0} --file={1} {2}", userCmdString, dumpFile, srcDB));
                        if (exitCode != 0)
                        {
                            throw new ApplicationException(String.Format("Failed to dump database (exit={0}), maybe you need to put your password into AppData\\Roaming\\postgresql\\pgpass.conf", exitCode));
                        }

                        var admin = new NpgsqlConnectionStringBuilder(connectionString.ConnectionString);
                        var dbName = admin.Database;

                        using (Log.InfoTraceMethodCall("DropCreateDatabase", string.Format("Recreating database {0}", dbName)))
                        {
                            admin.Database = "postgres"; // use "default" database to connect, when trying to drop "dbName"
                            schemaManager.Open(admin.ConnectionString);
                            if (schemaManager.CheckDatabaseExists(dbName))
                            {
                                schemaManager.DropDatabase(dbName);
                            }

                            schemaManager.CreateDatabase(dbName);
                        }

                        exitCode = RunPgUtil("pg_restore", String.Format("--format c {0} --dbname={2} {1}", userCmdString, dumpFile, destDB));
                        if (exitCode != 0)
                        {
                            throw new ApplicationException(String.Format("Failed to restore database (exit={0})", exitCode));
                        }

                        schemaManager.RefreshDbStats();
                    }
                    finally
                    {
                        // cleanup
                        File.Delete(dumpFile);
                        // After recreating the database, all connection pools should be cleard
                        NpgsqlConnection.ClearAllPools();
                    }
                }
                catch (ApplicationException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    Log.Error("Error while restoring database", ex);
                    throw;
                }
            }
        }

        private static int RunPgUtil(string util, string args)
        {
            var binPath = Path.Combine(GetPgSqlBinPath(), util);

            if (!File.Exists(binPath) && File.Exists(binPath + ".exe"))
            {
                binPath += ".exe";
            }

            var pi = new ProcessStartInfo(binPath, args);
            pi.UseShellExecute = false;
            pi.RedirectStandardOutput = true;
            pi.RedirectStandardError = true;
            pi.ErrorDialog = false;
            pi.CreateNoWindow = true;

            Log.InfoFormat("Calling [{0}] with arguments [{1}]", binPath, args);
            var p = Process.Start(pi);

            p.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
                if (!String.IsNullOrEmpty(e.Data))
                    Log.Error(e.Data);
            };
            p.BeginErrorReadLine();

            p.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
                if (!String.IsNullOrEmpty(e.Data))
                    Log.Info(e.Data);
            };
            p.BeginOutputReadLine();

            if (!p.WaitForExit(RESET_TIMEOUT * 1000))
            {
                p.Kill();
                throw new InvalidOperationException(String.Format("{0} did not complete within {1} seconds", util, RESET_TIMEOUT));
            }

            return p.ExitCode;
        }

        private static string GetPgSqlBinPath()
        {
            var pgSQLBinPath = Environment.GetEnvironmentVariable("PGSQLBinPath");
            if (string.IsNullOrEmpty(pgSQLBinPath))
            {
                throw new InvalidOperationException("Environment Variable PGSQLBinPath is not set, unable to reset test database");
            }
            else
            {
                Log.Info("Using binaries from PGSQLBinPath=" + pgSQLBinPath);
            }
            return pgSQLBinPath.Trim('\"');
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
