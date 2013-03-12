// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Npgsql;

namespace PrepareEnv.SchemaProvider
{
    class Postgresql : AdoNetProvider<NpgsqlConnection, NpgsqlTransaction, NpgsqlCommand>
    {
        public override void Copy(string source, string dest)
        {
            var srcBuilder = new NpgsqlConnectionStringBuilder(source);
            var srcUserCmdString = string.Format("--host={0} --port={1} --username={2} --no-password", srcBuilder.Host, srcBuilder.Port, srcBuilder.UserName);
            var srcDB = srcBuilder.Database;

            var destBuilder = new NpgsqlConnectionStringBuilder(dest);
            var destUserCmdString = string.Format("--host={0} --port={1} --username={2} --no-password", destBuilder.Host, destBuilder.Port, destBuilder.UserName);
            var destDB = destBuilder.Database;

            var dumpFile = GetBackupFile();

            try
            {
                var exitCode = RunPgUtil("pg_dump", String.Format("--exclude-schema=public --format c {0} --file={1} {2}", srcUserCmdString, dumpFile, srcDB));
                if (exitCode != 0)
                {
                    throw new InvalidOperationException(string.Format("Failed to dump database (exit={0}), maybe you need to put your password into AppData\\Roaming\\postgresql\\pgpass.conf", exitCode));
                }

                exitCode = RunPgUtil("pg_restore", String.Format("--format c {0} --dbname={2} {1}", destUserCmdString, dumpFile, destDB));
                if (exitCode != 0)
                {
                    // throw new InvalidOperationException(string.Format("Failed to restore database (exit={0})", exitCode));
                    // Unable to restore language
                    Console.WriteLine(string.Format("Failed to restore database (exit={0})", exitCode));
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

        private const int RESET_TIMEOUT = 4 * 60;
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

            var p = Process.Start(pi);

            p.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
                if (!String.IsNullOrEmpty(e.Data))
                    Console.WriteLine(e.Data);
            };
            p.BeginErrorReadLine();

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
                throw new InvalidOperationException("Environment variable PGSQLBinPath is not set, unable to locate database tools");
            }
            return pgSQLBinPath.Trim('\"');
        }

        public override void DropAllObjects()
        {
            // Do not optimize this
            // drop cascade will drop dependent views
            TableRef view;
            while (null != (view = GetViewNames().FirstOrDefault()))
            {
                DropViewCascade(view);
            }

            foreach (var tbl in GetTableNames().ToList())
            {
                DropTableCascade(tbl);
            }

            foreach (var schema in GetSchemaNames().ToList())
            {
                switch (schema)
                {
                    // DB infrastructure
                    case "pg_temp_1":
                    case "pg_toast_temp_1":
                    case "public":
                        break;
                    default:
                        DropSchema(schema);
                        break;
                }
            }
        }

        #region get objects
        private IEnumerable<ProcRef> GetProcedureNames()
        {
            return ExecuteReader(@"
                    SELECT n.nspname, p.proname
                    FROM pg_proc p
                        JOIN pg_namespace n ON (p.pronamespace = n.oid)
                    WHERE nspname NOT IN ('pg_catalog', 'pg_toast', 'information_schema', 'public');")
                .Select(rd => new ProcRef(this.CurrentConnection.Database, rd.GetString(0), rd.GetString(1)));
        }
        private IEnumerable<TableRef> GetViewNames()
        {
            return ExecuteReader("SELECT schemaname, viewname FROM pg_views WHERE schemaname not in ('information_schema', 'pg_catalog')")
                .Select(rd => new TableRef(CurrentConnection.Database, rd.GetString(0), rd.GetString(1)));
        }
        private IEnumerable<TableRef> GetTableNames()
        {
            return ExecuteReader("SELECT schemaname, tablename FROM pg_tables WHERE schemaname not in ('information_schema', 'pg_catalog')")
                .Select(rd => new TableRef(CurrentConnection.Database, rd.GetString(0), rd.GetString(1)));
        }
        private IEnumerable<string> GetSchemaNames()
        {
            return ExecuteReader("SELECT nspname FROM pg_catalog.pg_namespace WHERE nspname NOT IN ('information_schema', 'pg_catalog', 'pg_toast', 'public') AND nspname NOT LIKE 'pg_%temp_%'")
                .Select(rd => rd.GetString(0));
        }
        #endregion

        #region drop things
        private void DropViewCascade(TableRef tblName)
        {
            ExecuteNonQuery(String.Format("DROP VIEW {0} CASCADE", FormatSchemaName(tblName)));
        }
        private void DropTableCascade(TableRef tblName)
        {
            ExecuteNonQuery(String.Format("DROP TABLE {0} CASCADE", FormatSchemaName(tblName)));
        }
        private void DropSchema(string schemaName)
        {
            ExecuteNonQuery(String.Format("DROP SCHEMA {0} CASCADE", QuoteIdentifier(schemaName)));
        }
        #endregion

        #region Suff
        protected override NpgsqlConnection CreateConnection(string connectionString)
        {
            return new NpgsqlConnection(connectionString);
        }

        protected override NpgsqlCommand CreateCommand(string query)
        {
            var result = new NpgsqlCommand(query, CurrentConnection, CurrentTransaction);
            result.CommandTimeout = 0;
            return result;
        }

        protected override NpgsqlTransaction CreateTransaction()
        {
            return CurrentConnection.BeginTransaction();
        }

        protected override string FormatSchemaName(DboRef dbo)
        {
            return String.Format("\"{0}\".\"{1}\"", dbo.Schema, dbo.Name);
        }

        protected override string QuoteIdentifier(string name)
        {
            return "\"" + name + "\"";
        }
        #endregion
    }
}
