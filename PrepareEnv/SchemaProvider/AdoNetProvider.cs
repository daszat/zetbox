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
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace PrepareEnv.SchemaProvider
{
    abstract class AdoNetProvider<TConnection, TTransaction, TCommand>
        : ISchemaProvider
        where TConnection : class, IDbConnection
        where TTransaction : class, IDbTransaction
        where TCommand : class, IDbCommand
    {
        private TConnection db;
        protected TConnection CurrentConnection { get { return db; } }

        private TTransaction tx;
        protected TTransaction CurrentTransaction { get { return tx; } }

        private string currentConnectionString;
        public void Open(string connectionString)
        {
            if (db != null)
                throw new InvalidOperationException("Database already opened");
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString");

            currentConnectionString = connectionString;
            db = CreateConnection(currentConnectionString);
            db.Open();
        }

        public void BeginTransaction()
        {
            if (tx != null)
                throw new InvalidOperationException("Transaction already in progress");
            tx = CreateTransaction();
        }

        public void CommitTransaction()
        {
            if (tx != null)
            {
                tx.Commit();
                tx = null;
            }
        }

        public void RollbackTransaction()
        {
            if (tx != null)
            {
                tx.Rollback();
                tx = null;
            }
        }

        public void Dispose()
        {
            RollbackTransaction();
            if (db != null)
            {
                db.Close();
                db = null;
            }
        }

        protected abstract TCommand CreateCommand(string query);
        protected abstract TConnection CreateConnection(string connectionString);
        protected abstract TTransaction CreateTransaction();

        protected abstract string FormatSchemaName(DboRef tblName);
        protected abstract string QuoteIdentifier(string name);

        protected IEnumerable<IDataRecord> ExecuteReader(string query)
        {
            return ExecuteReader(query, null);
        }

        protected IEnumerable<IDataRecord> ExecuteReader(string query, IDictionary<string, object> args)
        {
            using (var cmd = CreateCommand(query))
            {
                if (args != null)
                {
                    foreach (var pair in args)
                    {
                        var param = cmd.CreateParameter();
                        param.ParameterName = pair.Key;
                        param.Value = pair.Value;
                        cmd.Parameters.Add(param);
                    }
                }
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        yield return rd;
                    }
                }
            }
        }

        protected object ExecuteScalar(string query)
        {
            return ExecuteScalar(query, null);
        }

        protected object ExecuteScalar(string query, IDictionary<string, object> args)
        {
            using (var cmd = CreateCommand(query))
            {
                if (args != null)
                {
                    foreach (var pair in args)
                    {
                        var param = cmd.CreateParameter();
                        param.ParameterName = pair.Key;
                        param.Value = pair.Value;
                        cmd.Parameters.Add(param);
                    }
                }
                return cmd.ExecuteScalar();
            }
        }

        protected void ExecuteNonQuery(string query)
        {
            ExecuteNonQuery(query, null);
        }

        protected void ExecuteNonQuery(string query, IDictionary<string, object> args)
        {
            using (var cmd = CreateCommand(query))
            {
                if (args != null)
                {
                    foreach (var pair in args)
                    {
                        var param = cmd.CreateParameter();
                        param.ParameterName = pair.Key;
                        param.Value = pair.Value;
                        cmd.Parameters.Add(param);
                    }
                }

                cmd.ExecuteNonQuery();
            }

        }

        public abstract void DropAllObjects();
        public abstract void Copy(string source, string dest);

        public static string GetBackupFile()
        {
            var dumpFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".backup");
            while (File.Exists(dumpFile))
            {
                dumpFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".backup");
            }
            return dumpFile;
        }
    }
}
