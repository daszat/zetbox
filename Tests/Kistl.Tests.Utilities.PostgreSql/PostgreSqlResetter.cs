
namespace Kistl.Tests.Utilities.PostgresSql
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
    using Kistl.API.Utils;
    using Npgsql;
    using NUnit.Framework;

    public sealed class PostgreSqlResetter
        : IDatabaseResetter
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Tests.PostgreSqlUtils");

        private readonly KistlConfig config;

        /// <summary>
        /// number of seconds to wait before cancelling _test reset.
        /// </summary>
        private const int RESET_TIMEOUT = 2 * 60;

        public PostgreSqlResetter(KistlConfig config)
        {
            this.config = config;
        }

        public void ResetDatabase()
        {
            using (Log.InfoTraceMethodCall("ResetDatabase"))
            {
                Assert.That(config.Server.ConnectionString, Is.StringContaining("_test"), "test databases should be marked with '_test' in the connection string");

                try
                {
                    Log.Info("Restoring Database");

                    var cb = new NpgsqlConnectionStringBuilder(config.Server.ConnectionString);
                    var srcDB = cb.Database.Substring(0, cb.Database.Length - "_test".Length);
                    var destDB = cb.Database;
                    var userCmdString = "--username=postgres --no-password";
                    var dumpFile = @"C:\temp\zbox.dump";
                    {
                        var pgDumpArgs = String.Format("--format c {0} --file={1} {2}", userCmdString, dumpFile, srcDB);

                        Log.InfoFormat("pgDumpArgs = {0}", pgDumpArgs);
                        var dump = RunPgUtil("pg_dump", pgDumpArgs);
                        if (dump.ExitCode != 0)
                        {
                            throw new ApplicationException("Failed to dump database, maybe you need to put your password into AppData\\Roaming\\postgresql\\pgpass.conf");
                        }
                    }
                    {
                        var pgRestoreArgs = String.Format("--format c --clean {0} --dbname={2} {1}", userCmdString, dumpFile, destDB);
                        Log.InfoFormat("pgRestoreArgs = {0}", pgRestoreArgs);

                        var restore = RunPgUtil("pg_restore", pgRestoreArgs);
                        if (restore.ExitCode != 0)
                        {
                            throw new ApplicationException("Failed to restore database, maybe you need to put your password into AppData\\Roaming\\postgresql\\pgpass.conf");
                        }
                    }
                    // After recreating the database, all connection pools should be cleard
                    NpgsqlConnection.ClearAllPools();
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

                Log.InfoFormat("Current Directory=[{0}]", Environment.CurrentDirectory);
                Log.InfoFormat("Using config from [{0}]", config.ConfigFilePath);
            }
        }

        private static Process RunPgUtil(string util, string args)
        {
            var binPath = Path.Combine(GetPgSqlBinPath(), String.Format("{0}.exe", util));

            var dumpInfo = new ProcessStartInfo(binPath, args);
            dumpInfo.UseShellExecute = false;
            dumpInfo.RedirectStandardOutput = true;
            dumpInfo.RedirectStandardError = true;
            dumpInfo.ErrorDialog = false;

            var p = Process.Start(dumpInfo);

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
