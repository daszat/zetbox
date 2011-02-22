
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

    public sealed class PostgreSqlResetter
        : IDatabaseResetter
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Tests.PostgreSqlUtils");

        private readonly KistlConfig config;
        private readonly ISchemaProvider schemaManager;

        /// <summary>
        /// number of seconds to wait before cancelling _test reset.
        /// </summary>
        private const int RESET_TIMEOUT = 2 * 60;

        public PostgreSqlResetter(KistlConfig config, ISchemaProvider schemaManager)
        {
            this.config = config;
            this.schemaManager = schemaManager;
        }

        public void ResetDatabase()
        {
            using (Log.InfoTraceMethodCall("ResetDatabase"))
            {
                Assert.That(config.Server.ConnectionString, Is.StringContaining("_test"), "test databases should be marked with '_test' in the connection string");

                Log.InfoFormat("Current Directory=[{0}]", Environment.CurrentDirectory);
                Log.InfoFormat("Using config from [{0}]", config.ConfigFilePath);

                try
                {
                    Log.Info("Restoring Database");

                    var cb = new NpgsqlConnectionStringBuilder(config.Server.ConnectionString);
                    var srcDB = cb.Database.Substring(0, cb.Database.Length - "_test".Length);
                    var destDB = cb.Database;
                    var userCmdString = "--username=postgres --no-password";
                    var dumpFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".zbox.backup");
                    while (File.Exists(dumpFile))
                    {
                        dumpFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".zbox.backup");
                    }
                    try
                    {
                        schemaManager.Open(config.Server.ConnectionString);
                        schemaManager.DropAllObjects();

                        {
                            var pgDumpArgs = String.Format("--format c {0} --file={1} {2}", userCmdString, dumpFile, srcDB);

                            Log.InfoFormat("pgDumpArgs = {0}", pgDumpArgs);
                            var dump = RunPgUtil("pg_dump", pgDumpArgs);
                            if (dump.ExitCode != 0)
                            {
                                throw new ApplicationException(String.Format("Failed to dump database (exit={0}), maybe you need to put your password into AppData\\Roaming\\postgresql\\pgpass.conf", dump.ExitCode));
                            }
                        }
                        {
                            var pgRestoreArgs = String.Format("--format c {0} --dbname={2} {1}", userCmdString, dumpFile, destDB);
                            Log.InfoFormat("pgRestoreArgs = {0}", pgRestoreArgs);

                            var restore = RunPgUtil("pg_restore", pgRestoreArgs);
                            if (restore.ExitCode != 0)
                            {
                                Log.Warn("Retrying after failed pg_restore, since the tool can become confused by schema changes");
                                restore = RunPgUtil("pg_restore", pgRestoreArgs);

                                if (restore.ExitCode != 0)
                                    throw new ApplicationException(String.Format("Failed to restore database (exit={0})", restore.ExitCode));
                            }
                        }
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

        private static Process RunPgUtil(string util, string args)
        {
            var binPath = Path.Combine(GetPgSqlBinPath(), util);

#if !MONO
            binPath += ".exe";
#endif

            var pi = new ProcessStartInfo(binPath, args);
            pi.UseShellExecute = false;
            pi.RedirectStandardOutput = true;
            pi.RedirectStandardError = true;
            pi.ErrorDialog = false;
            pi.CreateNoWindow = true;

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
                throw new InvalidOperationException(String.Format("{0} did not completed within {0} seconds", util, RESET_TIMEOUT));
            }

            return p;
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
