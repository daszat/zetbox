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

namespace Zetbox.Server.SchemaManagement.NpgsqlProvider
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Text.RegularExpressions;
    using Npgsql;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.API.Server;
    using Zetbox.API.Utils;

    public class Postgresql
        : AdoNetSchemaProvider<NpgsqlConnection, NpgsqlTransaction, NpgsqlCommand>
    {
        private readonly static log4net.ILog _log = log4net.LogManager.GetLogger("Zetbox.Server.Schema.Npgsql");
        protected override log4net.ILog Log { get { return _log; } }
        private readonly static log4net.ILog _queryLog = log4net.LogManager.GetLogger("Zetbox.Server.Schema.Npgsql.Queries");
        protected override log4net.ILog QueryLog { get { return _queryLog; } }

        private readonly static log4net.ILog _copyLog = log4net.LogManager.GetLogger("Zetbox.Server.Schema.Npgsql.COPY");

        #region Meta data

        public override string ConfigName { get { return "POSTGRESQL"; } }
        public override string AdoNetProvider { get { return "Npgsql"; } }
        public override string ManifestToken { get { return "8.1.3"; } }
        public override bool IsStorageProvider { get { return true; } }

        #endregion

        #region ADO.NET, Connection and Transaction Handling

        private NpgsqlConnectionStringBuilder _connectionSettings = null;

        protected override NpgsqlConnection CreateConnection(string connectionString)
        {
            _connectionSettings = new NpgsqlConnectionStringBuilder(connectionString);
            return new NpgsqlConnection(connectionString);
        }

        protected override NpgsqlTransaction CreateTransaction()
        {
            return CurrentConnection.BeginTransaction();
        }

        protected override NpgsqlCommand CreateCommand(string query)
        {
            var result = new NpgsqlCommand(query, CurrentConnection, CurrentTransaction);
            result.CommandTimeout = 0;
            return result;
        }

        private readonly Dictionary<string, string> _dblinks = new Dictionary<string, string>();

        public override void DblinkConnect(TableRef tblName)
        {
            if (tblName == null)
                throw new ArgumentNullException("tblName");

            if (tblName.Database == CurrentConnection.Database || _dblinks.ContainsKey(tblName.Database))
                return; // already connected

            ExecuteScalar("SELECT dblink_connect(@alias, @connstr)",
                new Dictionary<string, object>() {
                    { "@alias", tblName.Database},
                    { "@connstr", String.Format("dbname={0} port={1} user={2} password={3}", tblName.Database, CurrentConnection.Port, _connectionSettings.UserName, new string(Encoding.UTF8.GetChars(_connectionSettings.PasswordAsByteArray))) }
                });
            _dblinks[tblName.Database] = tblName.Database;
        }

        public override string GetSafeConnectionString(string connectionString)
        {
            var csb = new NpgsqlConnectionStringBuilder(connectionString);
            csb.Password = null;
            return csb.ToString();
        }

        #endregion

        #region Type Mapping

        public override string DbTypeToNative(DbType type)
        {
            switch (type)
            {
                case DbType.Byte:
                case DbType.Int16:
                    return "int2";
                case DbType.UInt16:
                case DbType.Int32:
                    return "int4";
                case DbType.Single:
                    return "float4";
                case DbType.Double:
                    return "float8";
                case DbType.String:
                    return "varchar";
                case DbType.Date:
                    return "date";
                case DbType.DateTime:
                case DbType.DateTime2:
                    return "timestamp";
                case DbType.Boolean:
                    return "bool";
                case DbType.Guid:
                    return "uuid";
                case DbType.Binary:
                    return "bytea";
                case DbType.Decimal:
                case DbType.VarNumeric:
                    return "numeric";
                case DbType.UInt32: // no int8 supported by Npgsql 2.0.9
                case DbType.Int64:
                case DbType.UInt64:
                default:
                    throw new ArgumentOutOfRangeException("type", string.Format("Unable to convert type '{0}' to an sql type string", type));
            }
        }

        public override DbType NativeToDbType(string type)
        {
            switch (type)
            {
                case "int2":
                    return DbType.Int16;
                case "int4":
                    return DbType.Int32;
                case "float4":
                    return DbType.Single;
                case "float8":
                    return DbType.Double;
                case "varchar":
                case "text":
                    return DbType.String;
                case "date":
                    return DbType.Date;
                case "timestamp":
                    return DbType.DateTime;
                case "bool":
                    return DbType.Boolean;
                case "uuid":
                    return DbType.Guid;
                case "bytea":
                    return DbType.Binary;
                case "numeric":
                    return DbType.Decimal;
                default:
                    throw new ArgumentOutOfRangeException("type", string.Format("Unable to convert type '{0}' to a DbType", type));
            }
        }

        #endregion

        #region SQL Infrastructure
        public static readonly int PG_MAX_IDENTIFIER_LENGTH = 63;
        public override string QuoteIdentifier(string name)
        {
            return "\"" + name.MaxLength(PG_MAX_IDENTIFIER_LENGTH) + "\"";
        }
        protected void CheckMaxIdentifierLength(string id)
        {
            if (id.Length > PG_MAX_IDENTIFIER_LENGTH)
            {
                throw new InvalidOperationException(string.Format("PG does not support Identifiers with a length more than {0} chars.", PG_MAX_IDENTIFIER_LENGTH ));
            }
        }

        private string GetColumnDefinition(Column col)
        {
            string nullable = col.IsNullable ? "NULL" : "NOT NULL";

            // hardcode special cases
            if (col.Type == DbType.String && col.Size == int.MaxValue)
            {
                // Create text columns for unlimited string length
                return String.Format("{0} text {1}", QuoteIdentifier(col.Name), nullable);
            }
            else if (col.Type == DbType.Binary && col.Size == int.MaxValue)
            {
                // Create bytea columns for unlimited blob length
                return String.Format("{0} bytea {1}", QuoteIdentifier(col.Name), nullable);
            }
            else
            {
                string size = string.Empty;
                if (col.Size > 0 && col.Type.In(DbType.String, DbType.StringFixedLength, DbType.AnsiString, DbType.AnsiStringFixedLength, DbType.Binary))
                {
                    size = String.Format("({0})", col.Size);
                }
                else if (col.Size > 0 && col.Type.In(DbType.Decimal, DbType.VarNumeric))
                {
                    size = String.Format("({0}, {1})", col.Size, col.Scale.Value);
                }

                string typeString = DbTypeToNative(col.Type) + size;
                return String.Format("{0} {1} {2}", QuoteIdentifier(col.Name), typeString, nullable);
            }
        }

        #endregion

        #region Database Management

        public override bool CheckDatabaseExists(string dbName)
        {
            if (string.IsNullOrEmpty(dbName))
                throw new ArgumentNullException("dbName");

            return (bool)ExecuteScalar("SELECT COUNT(*) > 0 FROM pg_database WHERE datname=@dbName",
                new Dictionary<string, object>()
                {
                    { "@dbName", dbName},
                });
        }

        public override void CreateDatabase(string dbName)
        {
            CheckMaxIdentifierLength(dbName);
            ExecuteNonQuery(string.Format("CREATE DATABASE {0} WITH TEMPLATE template0", QuoteIdentifier(dbName)));
        }

        #endregion

        #region Database Schemas

        public override IEnumerable<string> GetSchemaNames()
        {
            return ExecuteReader("SELECT nspname FROM pg_catalog.pg_namespace WHERE nspname NOT IN ('information_schema', 'pg_catalog', 'pg_toast', 'public') AND nspname NOT LIKE 'pg_%temp_%'")
                .Select(rd => rd.GetString(0));
        }

        public override bool CheckSchemaExists(string schemaName)
        {
            return (bool)ExecuteScalar("SELECT COUNT(*) > 0 FROM pg_catalog.pg_namespace WHERE nspname = @schemaName", new Dictionary<string, object>()
            {
                { "@schemaName", schemaName }
            });
        }

        public override void CreateSchema(string schemaName)
        {
            if (string.IsNullOrEmpty(schemaName)) throw new ArgumentNullException("schemaName");
            CheckMaxIdentifierLength(schemaName);

            ExecuteNonQuery(String.Format("CREATE SCHEMA {0}", QuoteIdentifier(schemaName)));
        }

        public override void DropSchema(string schemaName, bool force)
        {
            if (!CheckSchemaExists(schemaName))
                return;

            ExecuteNonQuery(String.Format(
                "DROP SCHEMA {0} {1}",
                QuoteIdentifier(schemaName),
                force ? "CASCADE" : "RESTRICT"));
        }

        #endregion

        #region Table Structure

        protected override string FormatFullName(DboRef dbo)
        {
            return String.Format("\"{0}\".\"{1}\".\"{2}\"", dbo.Database, dbo.Schema, dbo.Name);
        }

        protected override string FormatSchemaName(DboRef dbo)
        {
            return String.Format("\"{0}\".\"{1}\"", dbo.Schema, dbo.Name);
        }

        public override bool CheckTableExists(TableRef tblName)
        {
            if (tblName == null)
                throw new ArgumentNullException("tblName");

            return (bool)ExecuteScalar("SELECT COUNT(*) > 0 FROM pg_tables WHERE schemaname=@schema AND tablename=@table",
                new Dictionary<string, object>()
                {
                    { "@schema", tblName.Schema },
                    { "@table", tblName.Name },
                });
        }

        public override IEnumerable<TableRef> GetTableNames()
        {
            return ExecuteReader("SELECT schemaname, tablename FROM pg_tables WHERE schemaname not in ('information_schema', 'pg_catalog')")
                .Select(rd => new TableRef(CurrentConnection.Database, rd.GetString(0), rd.GetString(1)));
        }

        public override IEnumerable<TableRef> GetViewNames()
        {
            return ExecuteReader("SELECT schemaname, viewname FROM pg_views WHERE schemaname not in ('information_schema', 'pg_catalog')")
                .Select(rd => new TableRef(CurrentConnection.Database, rd.GetString(0), rd.GetString(1)));
        }

        public override string GetViewDefinition(TableRef view)
        {
            throw new NotImplementedException();
        }

        public override void CreateTable(TableRef tblName, IEnumerable<Column> cols)
        {
            if (cols == null) throw new ArgumentNullException("cols");
            if (tblName == null) throw new ArgumentNullException("tblName");
            CheckMaxIdentifierLength(tblName.Name);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("CREATE TABLE {0} (", FormatSchemaName(tblName));
            sb.AppendLine();

            sb.Append(String.Join(",\n", cols.Select(col => GetColumnDefinition(col)).ToArray()));

            sb.Append(")");
            ExecuteNonQuery(sb.ToString());
        }

        public override void CreateTable(TableRef tblName, bool idAsIdentityColumn, bool createPrimaryKey)
        {
            if (tblName == null)
                throw new ArgumentNullException("tblName");
            CheckMaxIdentifierLength(tblName.Name);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("CREATE TABLE {0} ( ", FormatSchemaName(tblName));
            if (idAsIdentityColumn)
            {
                sb.AppendLine("\"ID\" serial NOT NULL");
            }
            else
            {
                sb.AppendLine("\"ID\" integer NOT NULL");
            }

            if (createPrimaryKey)
            {
                // TODO: use Construct to create PK_{0}
                sb.AppendFormat(", CONSTRAINT \"PK_{0}\" PRIMARY KEY ( \"ID\" )", tblName.Name);
            }

            sb.AppendLine();
            sb.Append(")");

            ExecuteNonQuery(sb.ToString());
        }

        public override void RenameTable(TableRef oldTblName, TableRef newTblName)
        {
            if (oldTblName == null)
                throw new ArgumentNullException("oldTblName");
            if (newTblName == null)
                throw new ArgumentNullException("newTblName");
            if (!oldTblName.Database.Equals(newTblName.Database)) { throw new ArgumentOutOfRangeException("newTblName", "cannot rename table to different database"); }

            ExecuteNonQuery(String.Format(
                "ALTER TABLE {0} RENAME TO {1}",
                FormatSchemaName(oldTblName),
                QuoteIdentifier(newTblName.Name)));

            if (!oldTblName.Schema.Equals(newTblName.Schema))
            {
                var intermediateName = new TableRef(oldTblName.Database, oldTblName.Schema, newTblName.Name);

                ExecuteNonQuery(String.Format(
                    "ALTER TABLE {0} SET SCHEMA {1}",
                    FormatSchemaName(intermediateName),
                    QuoteIdentifier(newTblName.Schema)));
            }
        }

        public override bool CheckColumnExists(TableRef tblName, string colName)
        {
            if (tblName == null)
                throw new ArgumentNullException("tblName");
            return (bool)ExecuteScalar(@"
                SELECT COUNT(*) > 0
                FROM pg_attribute a
                    JOIN pg_class c ON c.oid = a.attrelid
                    LEFT JOIN pg_namespace n ON n.oid = c.relnamespace 
                WHERE n.nspname = @schema AND c.relname = @table AND a.attname=@name",
                new Dictionary<string, object>()
                {
                    { "@schema", tblName.Schema },
                    { "@table", tblName.Name },
                    { "@name", colName },
                });
        }

        public override IEnumerable<string> GetTableColumnNames(TableRef tbl)
        {
            if (tbl == null)
                throw new ArgumentNullException("tbl");
            return ExecuteReader(
                    @"SELECT attname
                        FROM pg_attribute
                            JOIN pg_class ON (attrelid = pg_class.oid)
                            JOIN pg_namespace ON (relnamespace = pg_namespace.oid)
                        WHERE nspname = @schema AND relname = @table and relkind in ( 'r', 'v' ) AND attnum >= 0 AND NOT attisdropped",
                    new Dictionary<string, object>() {
                        { "@schema", tbl.Schema },
                        { "@table", tbl.Name }
                    })
                .Select(rd => rd.GetString(0));
        }

        public override IEnumerable<Column> GetTableColumns(TableRef tblName)
        {
            if (tblName == null)
                throw new ArgumentNullException("tblName");

            return ExecuteReader(
                @"SELECT a.attname, t.typname, a.atttypmod - 4 as len, not a.attnotnull as nullable, t.typlen < 0 as variable_length
                    FROM pg_attribute a
                        JOIN pg_class c ON (a.attrelid = c.oid)
                        JOIN pg_namespace n ON (c.relnamespace = n.oid)
                        JOIN pg_type t ON (a.atttypid = t.oid)
                    WHERE n.nspname = @schema AND c.relname = @table and c.relkind in ( 'r', 'v' ) AND a.attnum >= 0 AND NOT attisdropped",
                new Dictionary<string, object>() {
                    { "@schema", tblName.Schema },
                    { "@table", tblName.Name }
                })
                .Select(rd => new Column()
                {
                    Name = rd.GetString(0),
                    Type = NativeToDbType(rd.GetString(1)),
                    Size = rd.GetBoolean(4) ? rd.GetInt32(2) : int.MaxValue,
                    IsNullable = rd.GetBoolean(3)
                });
        }

        protected override void DoColumn(bool add, TableRef tblName, string colName, DbType type, int size, int scale, bool isNullable, params DatabaseConstraint[] constraints)
        {
            CheckMaxIdentifierLength(colName);
            StringBuilder sb = new StringBuilder();

            if (add)
            {
                sb.AppendFormat("ALTER TABLE {0} ADD {1}",
                    FormatSchemaName(tblName),
                    GetColumnDefinition(new Column()
                    {
                        Name = colName,
                        Type = type,
                        Size = size,
                        Scale = scale,
                        IsNullable = isNullable
                    }));
            }
            else
            {
                sb.AppendFormat("ALTER TABLE {0} ALTER COLUMN {1} {2} NOT NULL",
                    FormatSchemaName(tblName),
                    QuoteIdentifier(colName),
                    isNullable ? "DROP" : "SET"
                    );
            }

            ExecuteNonQuery(sb.ToString());

            if (GetHasColumnDefaultValue(tblName, colName))
            {
                ExecuteNonQuery(String.Format("ALTER TABLE {0} ALTER COLUMN {1} DROP DEFAULT", FormatSchemaName(tblName), QuoteIdentifier(colName)));
            }

            foreach (var defConstraint in (constraints ?? DatabaseConstraint.EmptyArray).OfType<DefaultConstraint>())
            {
                string defValue;
                if (defConstraint is NewGuidDefaultConstraint)
                {
                    defValue = "uuid_generate_v4()";
                }
                else if (defConstraint is IntDefaultConstraint)
                {
                    defValue = ((IntDefaultConstraint)defConstraint).Value.ToString();
                }
                else if (defConstraint is BoolDefaultConstraint)
                {
                    defValue = ((BoolDefaultConstraint)defConstraint).Value ? "TRUE" : "FALSE";
                }
                else if (defConstraint is DateTimeDefaultConstraint)
                {
                    switch (((DateTimeDefaultConstraint)defConstraint).Precision)
                    {
                        case DateTimeDefaultConstraintPrecision.Date:
                            defValue = "CURRENT_DATE";
                            break;
                        case DateTimeDefaultConstraintPrecision.Time:
                            defValue = "now()";
                            break;
                        default:
                            throw new NotImplementedException(string.Format("Unknown DateTimeDefaultConstraintPrecision: {0}", ((DateTimeDefaultConstraint)defConstraint).Precision));
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException("constraints", "Unsupported default constraint " + defConstraint.GetType().Name);
                }
                ExecuteNonQuery(string.Format("ALTER TABLE {0} ALTER COLUMN {1} SET DEFAULT {2}", FormatSchemaName(tblName), QuoteIdentifier(colName), defValue));
            }

            foreach (var checkConstraint in (constraints ?? DatabaseConstraint.EmptyArray).OfType<CheckConstraint>())
            {
                string check;
                if (checkConstraint is BoolCheckConstraint)
                {
                    check = string.Format("({0} = '{1}')", QuoteIdentifier(colName), ((BoolCheckConstraint)checkConstraint).Value ? "t" : "f");
                }
                else
                {
                    throw new ArgumentOutOfRangeException("constraints", "Unsupported check constraint " + checkConstraint.GetType().Name);
                }
                ExecuteNonQuery(string.Format("ALTER TABLE {0} ADD CONSTRAINT {1} CHECK {2}",
                    FormatSchemaName(tblName),
                    ConstructCheckConstraintName(tblName, colName),
                    check));
            }
        }

        public override void RenameColumn(TableRef tblName, string oldColName, string newColName)
        {
            ExecuteNonQuery(String.Format(
                "ALTER TABLE {0} RENAME COLUMN {1} TO {2}",
                FormatSchemaName(tblName),
                QuoteIdentifier(oldColName),
                QuoteIdentifier(newColName)));
        }

        public override bool GetIsColumnNullable(TableRef tblName, string colName)
        {
            if (tblName == null)
                throw new ArgumentNullException("tblName");

            return (bool)ExecuteScalar(@"
                SELECT NOT a.attnotnull
                FROM pg_class c
                    LEFT JOIN pg_namespace n ON n.oid = c.relnamespace
                    LEFT JOIN pg_attribute a ON c.oid = a.attrelid
                WHERE n.nspname = @schema AND c.relname = @table AND c.relkind = 'r'
                    AND a.attnum >= 1 AND a.attname=@column",
                new Dictionary<string, object>() { 
                    { "@schema", tblName.Schema },
                    { "@table", tblName.Name }, 
                    { "@column", colName }
                });
        }

        public override bool GetHasColumnDefaultValue(TableRef tblName, string colName)
        {
            if (tblName == null)
                throw new ArgumentNullException("tblName");

            return (bool)ExecuteScalar(@"
                SELECT (d.adbin IS NOT NULL AND d.adbin <> '') as has_default
                FROM pg_class c
	                LEFT JOIN pg_namespace n ON n.oid = c.relnamespace
	                LEFT JOIN pg_attribute a ON c.oid = a.attrelid
	                LEFT JOIN pg_attrdef d ON c.oid = d.adrelid AND a.attnum = d.adnum
                WHERE n.nspname = @schema AND c.relname = @table AND a.attname = @column",
                new Dictionary<string, object>() { 
                    { "@schema", tblName.Schema },
                    { "@table", tblName.Name }, 
                    { "@column", colName }
                });
        }

        public override int GetColumnMaxLength(TableRef tblName, string colName)
        {
            if (tblName == null)
                throw new ArgumentNullException("tblName");

            return (int)ExecuteScalar(@"
                SELECT a.atttypmod - 4 -- adjust for varchar implementation, which is storing the length too
                FROM pg_class c
	                LEFT JOIN pg_namespace n ON n.oid = c.relnamespace
	                LEFT JOIN pg_attribute a ON c.oid = a.attrelid
                WHERE n.nspname = @schema AND c.relname = @table AND a.attname = @column",
                new Dictionary<string, object>() { 
                    { "@schema", tblName.Schema },
                    { "@table", tblName.Name }, 
                    { "@column", colName }
                });
        }

        #endregion

        #region Table Content

        public override bool CheckTableContainsData(TableRef tbl)
        {
            return (bool)ExecuteScalar(String.Format(
                "SELECT COUNT(*) > 0 FROM (SELECT * FROM {0} LIMIT 1) AS data",
                FormatSchemaName(tbl)));
        }

        public override bool CheckTableContainsData(TableRef tbl, IEnumerable<string> discriminatorFilter)
        {
            if (discriminatorFilter == null)
            {
                return CheckTableContainsData(tbl);
            }
            else
            {
                var parameters = ToAdoParameters(discriminatorFilter);

                return (bool)ExecuteScalar(String.Format(
                    "SELECT COUNT(*) > 0 FROM (SELECT * FROM {0} WHERE {1} IN ({2}) LIMIT 1) AS data",
                    FormatSchemaName(tbl),
                    QuoteIdentifier(TableMapper.DiscriminatorColumnName),
                    string.Join(",", parameters.Keys)),
                    parameters);
            }
        }

        public override bool CheckColumnContainsNulls(TableRef tbl, string colName)
        {
            return (bool)ExecuteScalar(String.Format(
                "SELECT COUNT(*) > 0 FROM (SELECT {1} FROM {0} WHERE {1} IS NULL LIMIT 1) AS nulls",
                FormatSchemaName(tbl),
                QuoteIdentifier(colName)));
        }

        public override bool CheckFKColumnContainsUniqueValues(TableRef tbl, string colName)
        {
            return (bool)ExecuteScalar(String.Format(
                @"SELECT COUNT(*) = 0 FROM (
                    SELECT {1} FROM {0} WHERE {1} IS NOT NULL
                    GROUP BY {1}
                    HAVING COUNT({1}) > 1 LIMIT 1) AS tbl",
                FormatSchemaName(tbl),
                QuoteIdentifier(colName)));
        }

        public override bool CheckColumnContainsValues(TableRef tbl, string colName)
        {
            return (bool)ExecuteScalar(String.Format(
                "SELECT COUNT(*) > 0 FROM (SELECT {1} FROM {0} WHERE {1} IS NOT NULL LIMIT 1) AS data",
                FormatSchemaName(tbl),
                QuoteIdentifier(colName)));
        }

        public override long CountRows(TableRef tblName)
        {
            return (long)ExecuteScalar(String.Format(
                @"SELECT COUNT(*) FROM {0}",
                FormatSchemaName(tblName)));
        }

        #endregion

        #region Constraint and Index Management

        public override bool CheckFKConstraintExists(TableRef tblName, string fkName)
        {
            if (tblName == null) throw new ArgumentNullException("tblName");
            if (string.IsNullOrEmpty(fkName)) throw new ArgumentNullException("fkName");

            return (bool)ExecuteScalar("SELECT COUNT(*) > 0 FROM pg_constraint JOIN pg_namespace n ON (connamespace = n.oid) WHERE n.nspname = @schema AND conname = @constraint_name AND contype = 'f'",
                new Dictionary<string, object>(){
                    { "@schema", tblName.Schema },
                    { "@constraint_name", fkName.MaxLength(PG_MAX_IDENTIFIER_LENGTH) }
                });
        }

        public override IEnumerable<TableConstraintNamePair> GetFKConstraintNames()
        {
            return ExecuteReader(@"
                SELECT n.nspname, c.relname, conname
                FROM pg_class c
                    LEFT JOIN pg_namespace n ON n.oid = c.relnamespace
                    LEFT JOIN pg_constraint ON conrelid = c.oid
                WHERE contype = 'f'
                ORDER BY conname")
                .Select(rd => new TableConstraintNamePair()
                {
                    ConstraintName = rd.GetString(2),
                    TableName = new TableRef(CurrentConnection.Database, rd.GetString(0), rd.GetString(1))
                })
                .ToList();
        }

        public override void CreateFKConstraint(TableRef tblName, TableRef refTblName, string colName, string newConstraintName, bool onDeleteCascade)
        {
            ExecuteNonQuery(String.Format(
                @"ALTER TABLE {0}
                    ADD CONSTRAINT {1} FOREIGN KEY({2})
                    REFERENCES {3} ({4}){5}",
                FormatSchemaName(tblName),
                QuoteIdentifier(newConstraintName),
                QuoteIdentifier(colName),
                FormatSchemaName(refTblName),
                QuoteIdentifier("ID"),
                onDeleteCascade ? @" ON DELETE CASCADE" : String.Empty));
        }

        public override void RenameFKConstraint(TableRef tblName, string oldConstraintName, TableRef refTblName, string colName, string newConstraintName, bool onDeleteCascade)
        {
            if (tblName == null) throw new ArgumentNullException("tblName");
            if (string.IsNullOrEmpty(oldConstraintName)) throw new ArgumentNullException("oldConstraintName");
            if (string.IsNullOrEmpty(newConstraintName)) throw new ArgumentNullException("newConstraintName");

            CreateFKConstraint(tblName, refTblName, colName, newConstraintName, onDeleteCascade);
            DropFKConstraint(tblName, oldConstraintName);
        }

        public override bool CheckIndexPossible(TableRef tblName, string idxName, bool unique, bool clustered, params string[] columns)
        {
            if (!unique && !clustered) return true;
            if (columns == null || columns.Length == 0)
            {
                Log.WarnFormat("Index automatically impossible for {0} without columns", idxName);
                return false;
            }

            return (bool)ExecuteScalar(
                string.Format("SELECT COUNT(*) = 0 FROM (SELECT {0} FROM {1} GROUP BY {0} HAVING COUNT(*) > 1) data",
                    String.Join(", ", columns.Select(c => QuoteIdentifier(c)).ToArray()),
                    FormatSchemaName(tblName)
                ));
        }

        public override bool CheckIndexExists(TableRef tblName, string idxName)
        {
            if (tblName == null)
                throw new ArgumentNullException("tblName");

            return (bool)ExecuteScalar(@"
                SELECT COUNT(*) > 0
                    FROM pg_index
                        JOIN pg_class idx ON (indexrelid = idx.oid)
                        JOIN pg_class tbl ON (indrelid = tbl.oid)
                        JOIN pg_namespace ON (tbl.relnamespace = pg_namespace.oid)
                    WHERE nspname = @schema AND tbl.relname = @table AND idx.relname = @index",
                new Dictionary<string, object>(){
                    { "@schema", tblName.Schema },
                    { "@table", tblName.Name },
                    { "@index", idxName.MaxLength(PG_MAX_IDENTIFIER_LENGTH) },
                });
        }

        public override void CreateIndex(TableRef tblName, string idxName, bool unique, bool clustered, params string[] columns)
        {
            if (columns == null || columns.Length == 0) throw new ArgumentOutOfRangeException("columns", string.Format("Cannot create index {0} without columns", idxName));

            ExecuteNonQuery(String.Format(
                "CREATE {0}INDEX {1} ON {2} ({3})",
                unique ? "UNIQUE " : String.Empty,
                QuoteIdentifier(idxName),
                FormatSchemaName(tblName),
                String.Join(", ", columns.Select(c => QuoteIdentifier(c)).ToArray())));

            if (clustered)
            {
                ExecuteNonQuery(String.Format("CLUSTER {0} USING {1}",
                    FormatSchemaName(tblName),
                    QuoteIdentifier(idxName)));
            }
        }

        public override void DropIndex(TableRef tblName, string idxName)
        {
            if (tblName == null)
                throw new ArgumentNullException("tblName");

            ExecuteNonQuery(String.Format(
                "DROP INDEX {0}.{1}",
                QuoteIdentifier(tblName.Schema),
                QuoteIdentifier(idxName)));
        }

        public override bool CheckCheckConstraintPossible(TableRef tblName, string colName, string newConstraintName, Dictionary<List<string>, Expression<Func<string, bool>>> checkExpressions)
        {
            return (bool)ExecuteScalar(string.Format(
                "SELECT Count(*) = 0 FROM (SELECT * FROM {0} WHERE NOT {1} LIMIT 1) AS data",
                FormatSchemaName(tblName),
                FormatCheckExpression(colName, checkExpressions)));
        }

        #endregion

        #region Other DB Objects (Views, Triggers, Procedures)

        public override bool CheckViewExists(TableRef viewName)
        {
            if (viewName == null)
                throw new ArgumentNullException("viewName");

            return (bool)ExecuteScalar(@"
                SELECT COUNT(*) > 0
                FROM pg_views
                WHERE schemaname = @schema AND viewname = @view",
                new Dictionary<string, object>() {
                    { "@schema", viewName.Schema },
                    { "@view", viewName.Name },
                });
        }

        public override bool CheckTriggerExists(TableRef objName, string triggerName)
        {
            if (objName == null)
                throw new ArgumentNullException("objName");
            return (bool)ExecuteScalar(@"
                SELECT count(*) > 0
                FROM pg_proc p
                    JOIN pg_namespace n ON p.pronamespace = n.oid
                    JOIN pg_type t ON p.prorettype = t.oid
                WHERE t.typname = 'trigger' AND n.nspname = @schema AND p.proname = @trigger",
                new Dictionary<string, object>(){
                    { "@schema", objName.Schema },
                    { "@trigger", triggerName.MaxLength(PG_MAX_IDENTIFIER_LENGTH) },
                });
        }

        public override void DropTrigger(TableRef objName, string triggerName)
        {
            if (objName == null) throw new ArgumentNullException("objName");

            ExecuteNonQuery(String.Format("DROP FUNCTION {0}.{1}() CASCADE",
                QuoteIdentifier(objName.Schema),
                QuoteIdentifier(triggerName)));
        }

        public override bool CheckProcedureExists(ProcRef procName)
        {
            if (procName == null)
                throw new ArgumentNullException("procName");
            return (bool)ExecuteScalar(@"
                SELECT count(*) > 0
                FROM pg_proc p
                    LEFT JOIN pg_namespace n ON p.pronamespace = n.oid
                WHERE n.nspname = @schema AND p.proname = @proc",
                new Dictionary<string, object>() {
                    { "@schema", procName.Schema },
                    { "@proc", procName.Name.MaxLength(PG_MAX_IDENTIFIER_LENGTH) },
                });
        }

        /// <summary>
        /// Drops all versions of the function with the specified name.
        /// </summary>
        public override void DropProcedure(ProcRef procName)
        {
            DropProcedureInternal(procName, false);
        }

        private void DropProcedureInternal(ProcRef procName, bool cascade)
        {
            foreach (var argTypes in GetParameterTypes(procName))
            {
                ExecuteNonQuery(String.Format("DROP FUNCTION {0}({1}) {2}",
                    FormatSchemaName(procName),
                    String.Join(",", argTypes),
                    cascade ? "CASCADE" : "RESTRICT"));
            }
        }

        protected virtual void DropTableCascade(TableRef tblName)
        {
            ExecuteNonQuery(String.Format("DROP TABLE {0} CASCADE", FormatSchemaName(tblName)));
        }

        protected virtual void DropViewCascade(TableRef tblName)
        {
            ExecuteNonQuery(String.Format("DROP VIEW {0} CASCADE", FormatSchemaName(tblName)));
        }

        private IEnumerable<string[]> GetParameterTypes(ProcRef procName)
        {
            List<string[]> result = new List<string[]>();
            // TODO re-enable for postgres 8.4; re-implement without generate_subscripts for 8.3
            //            string sqlQuery = @"
            //                SELECT args.proc_oid, t.typname 
            //                FROM pg_type t 
            //                    JOIN (
            //                        SELECT oid AS proc_oid, proargtypes::oid[] AS argtypes, generate_subscripts(proargtypes::oid[], 1) AS argtype_subscript 
            //                        FROM pg_proc where proname = @procName) args 
            //                    ON t.oid = args.argtypes[args.argtype_subscript] 
            //                ORDER BY args.proc_oid, args.argtype_subscript;";
            //            QueryLog.Debug(sqlQuery);

            //            long? lastProcOid = null;
            //            List<string> types = null;
            //            foreach (var rd in ExecuteReader(sqlQuery, new Dictionary<string, object>() { { "@procname", procName.Name } }))
            //            {
            //                var procOid = rd.GetInt64(0);
            //                var argType = rd.GetString(1);
            //                if (lastProcOid != procOid)
            //                {
            //                    if (types != null)
            //                    {
            //                        result.Add(types.ToArray());
            //                    }
            //                    lastProcOid = procOid;
            //                    types = new List<string>();
            //                }
            //                types.Add(argType);
            //            }
            //            if (types != null)
            //            {
            //                result.Add(types.ToArray());
            //            }
            return result;
        }

        public override void EnsureInfrastructure()
        {
        }

        public override void DropAllObjects()
        {
            foreach (var proc in GetProcedureNames().ToList())
            {
                DropProcedureInternal(proc, true);
            }

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
                        DropSchema(schema, true);
                        break;
                }
            }
        }

        public override IEnumerable<ProcRef> GetProcedureNames()
        {
            return ExecuteReader(@"
                    SELECT n.nspname, p.proname
                    FROM pg_proc p
                        JOIN pg_namespace n ON (p.pronamespace = n.oid)
                    WHERE nspname NOT IN ('pg_catalog', 'pg_toast', 'information_schema', 'public');")
                .Select(rd => new ProcRef(this.CurrentConnection.Database, rd.GetString(0), rd.GetString(1)));
        }

        #endregion

        #region Zetbox Schema Handling

        protected override string GetSchemaInsertStatement()
        {
            return "INSERT INTO \"base\".\"CurrentSchema\" (\"Version\", \"Schema\") VALUES (1, @schema)";
        }

        protected override string GetSchemaUpdateStatement()
        {
            return "UPDATE \"base\".\"CurrentSchema\" SET \"Schema\" = @schema, \"Version\" = \"Version\" + 1";
        }

        #endregion

        #region zetbox Accelerators

        protected override bool CallRepairPositionColumn(bool repair, TableRef tblName, string indexName)
        {
            return (bool)ExecuteScalar("SELECT \"dbo\".\"RepairPositionColumnValidityByTable\"(@repair, @tblName, @colName)",
                 new Dictionary<string, object>() {
                    {"@repair", repair},
                    {"@tblName", FormatSchemaName(tblName)},
                    {"@colName", indexName},
                 });
        }

        #endregion

        // --  TODO  --  Cleanup stuff below  ----------------------------------------
        // ---------------------------------------------------------------------------
        // ---------------------------------------------------------------------------
        // ---------------------------------------------------------------------------
        // ---------------------------------------------------------------------------
        // ---------------------------------------------------------------------------
        // ---------------------------------------------------------------------------
        // ---------------------------------------------------------------------------
        // ---------------------------------------------------------------------------
        // ---------------------------------------------------------------------------
        // ---------------------------------------------------------------------------
        // ---------------------------------------------------------------------------
        // ---------------------------------------------------------------------------

        public override void CopyColumnData(
            TableRef srcTblName, string srcColName,
            TableRef tblName, string colName)
        {
            ExecuteNonQuery(String.Format(
                "UPDATE {2} dest SET {3} = src.{1} FROM {0} src WHERE dest.{4} = src.{4}",
                FormatSchemaName(srcTblName),     // 0
                QuoteIdentifier(srcColName),    // 1
                FormatSchemaName(tblName),        // 2
                QuoteIdentifier(colName),       // 3
                QuoteIdentifier("ID")));        // 4
        }

        public override void CopyColumnData(TableRef srcTblName, string[] srcColName, TableRef tblName, string[] colName, string discriminatorValue)
        {
            if (srcColName == null) throw new ArgumentNullException("srcColName");
            if (colName == null) throw new ArgumentNullException("colName");
            if (srcColName.Length != colName.Length) throw new ArgumentOutOfRangeException("colName", "need the same number of columns in srcColName and colName");

            var assignments = srcColName.Zip(colName, (src, dst) => string.Format("{1} = src.{0}", QuoteIdentifier(src), QuoteIdentifier(dst))).ToList();
            if (discriminatorValue != null)
            {
                assignments.Add(string.Format("{0} = '{1}'", QuoteIdentifier(TableMapper.DiscriminatorColumnName), discriminatorValue));
            }

            if (assignments.Count > 0)
            {
                ExecuteNonQuery(string.Format(
                    "UPDATE {1} dest SET {2} FROM {0} src WHERE dest.{3} = src.{3}",
                    FormatSchemaName(srcTblName),     // 0
                    FormatSchemaName(tblName),        // 1
                    string.Join(", ", assignments),   // 2
                    QuoteIdentifier("ID")));          // 3
            }
        }

        public override void MigrateFKs(
            TableRef srcTblName, string srcColName,
            TableRef tblName, string colName)
        {
            ExecuteNonQuery(String.Format(
                "UPDATE {2} dest SET {3} = src.{4} FROM {0} src WHERE dest.{4} = src.{1}",
                FormatSchemaName(srcTblName), // 0
                QuoteIdentifier(srcColName), // 1
                FormatSchemaName(tblName),    // 2
                QuoteIdentifier(colName),    // 3
                QuoteIdentifier("ID")));     // 4
        }

        public override void InsertFKs(
            TableRef srcTblName, string srcColName,
            TableRef tblName, string colName,
            string fkColName)
        {
            ExecuteNonQuery(String.Format(
                "INSERT INTO {2} ({3}, {4}) SELECT {5}, {1} FROM {0} WHERE {1} IS NOT NULL",
                FormatSchemaName(srcTblName), // 0
                QuoteIdentifier(srcColName), // 1
                FormatSchemaName(tblName),    // 2
                QuoteIdentifier(colName),    // 3
                QuoteIdentifier(fkColName),  // 4
                QuoteIdentifier("ID")));     // 5
        }

        public override void CopyFKs(
            TableRef srcTblName, string srcColName,
            TableRef tblName, string colName,
            string srcFkColName)
        {
            ExecuteNonQuery(String.Format(
                "UPDATE {2} dest SET {3} = src.{1} FROM {0} src WHERE src.{4} = dest.{5}",
                FormatSchemaName(srcTblName), // 0
                QuoteIdentifier(srcColName), // 1
                FormatSchemaName(tblName),    // 2
                QuoteIdentifier(colName),    // 3
                QuoteIdentifier(srcFkColName),  // 4
                QuoteIdentifier("ID")));     // 5
        }

        public override void CreateUpdateRightsTrigger(string triggerName, TableRef tblName, List<RightsTrigger> tblList, List<string> dependingCols)
        {
            if (String.IsNullOrEmpty(triggerName))
                throw new ArgumentNullException("triggerName");
            if (tblName == null)
                throw new ArgumentNullException("tblName");
            if (tblList == null)
                throw new ArgumentNullException("tblList");

            StringBuilder sb = new StringBuilder();
            sb.Append("CREATE OR REPLACE FUNCTION ");
            sb.Append(QuoteIdentifier(tblName.Schema));
            sb.Append(".");
            sb.Append(QuoteIdentifier(triggerName));
            sb.Append("()");
            sb.AppendLine();
            sb.Append(@"  RETURNS trigger AS
$BODY$BEGIN
");

            // optimaziation
            if (dependingCols != null && dependingCols.Count > 0)
            {
                sb.AppendLine("  IF TG_OP = 'UPDATE' THEN");
                sb.Append("    IF ");
                sb.Append(string.Join(" AND ", dependingCols.Select(c => string.Format("\n      coalesce(OLD.{0},-1) = coalesce(NEW.{0},-1)", QuoteIdentifier(c))).ToArray()));
                sb.AppendLine("\n    THEN");
                sb.AppendLine(@"      RETURN NULL;
    END IF;
  END IF;");
            }

            foreach (var tbl in tblList)
            {
                if (tbl.Relations.Count == 0)
                {
                    sb.AppendFormat(@"
	IF TG_OP = 'DELETE' OR TG_OP = 'UPDATE' THEN
		DELETE FROM {0} WHERE {2} = OLD.{2};
	END IF;
	IF TG_OP = 'INSERT' OR TG_OP = 'UPDATE' THEN
		DELETE FROM {0} WHERE {2} = NEW.{2};
		INSERT INTO {0} ({2}, ""Identity"", ""Right"")
			SELECT {2}, ""Identity"", ""Right"" FROM {1}
			WHERE {2} = NEW.{2};
	END IF;
", FormatSchemaName(tbl.TblNameRights), FormatSchemaName(tbl.ViewUnmaterializedName), QuoteIdentifier("ID"));
                }
                else
                {
                    StringBuilder select = new StringBuilder();
                    select.AppendFormat("SELECT t1.\"ID\" FROM {0} t1", FormatSchemaName(tbl.TblName));
                    int idx = 2;
                    var lastRel = tbl.Relations.Last();
                    foreach (var rel in tbl.Relations)
                    {
                        select.AppendLine();
                        if (rel == lastRel)
                        {
                            select.AppendFormat(@"      WHERE ({0}.{1} = t{2}.{3})",
                                "{0}",
                                QuoteIdentifier(rel.JoinColumnName.Single().ColumnName),
                                idx - 1,
                                QuoteIdentifier(rel.FKColumnName.Single().ColumnName));
                        }
                        else
                        {
                            select.AppendFormat(@"      INNER JOIN {0} t{1} ON (t{1}.{2} = t{3}.{4})",
                                FormatSchemaName(rel.JoinTableName),
                                idx,
                                QuoteIdentifier(rel.JoinColumnName.Single().ColumnName),
                                idx - 1,
                                QuoteIdentifier(rel.FKColumnName.Single().ColumnName));
                        }
                        idx++;
                    }
                    string selectFormat = select.ToString();

                    sb.AppendFormat(@"
	IF TG_OP = 'DELETE' OR TG_OP = 'UPDATE' THEN
		DELETE FROM {0} WHERE ""ID"" IN ({1});
	END IF;
	IF TG_OP = 'INSERT' OR TG_OP = 'UPDATE' THEN
		DELETE FROM {0} WHERE ""ID"" IN ({2});
		INSERT INTO {0} (""ID"", ""Identity"", ""Right"")
			SELECT rights.""ID"", rights.""Identity"", rights.""Right"" FROM {3} as rights
                INNER JOIN ({2}) as acl ON (acl.""ID"" = rights.""ID"");
	END IF;
",
                        FormatSchemaName(tbl.TblNameRights),
                        String.Format(selectFormat, "OLD"),
                        String.Format(selectFormat, "NEW"),
                        FormatSchemaName(tbl.ViewUnmaterializedName));

                }
                sb.AppendLine();
                sb.AppendLine();
            }

            sb.Append(@"	RETURN NULL;
END$BODY$
  LANGUAGE 'plpgsql' VOLATILE
");
            ExecuteNonQuery(sb.ToString());
            ExecuteNonQuery(String.Format(@"
                CREATE TRIGGER {0} AFTER INSERT OR UPDATE OR DELETE
                    ON {1} FOR EACH ROW
                    EXECUTE PROCEDURE {2}.{0}()",
                QuoteIdentifier(triggerName),
                FormatSchemaName(tblName),
                QuoteIdentifier(tblName.Schema)
                ));
        }

        public override void CreateEmptyRightsViewUnmaterialized(TableRef viewName)
        {
            Log.DebugFormat("Creating *empty* unmaterialized rights view \"{0}\"", viewName);
            ExecuteNonQuery(String.Format(@"CREATE VIEW {0} AS SELECT 0 AS ""ID"", 0 AS ""Identity"", 0 AS ""Right"" WHERE 0 = 1", FormatSchemaName(viewName)));
        }

        public override void CreateRightsViewUnmaterialized(TableRef viewName, TableRef tblName, TableRef tblNameRights, IList<ACL> acls)
        {
            if (acls == null)
                throw new ArgumentNullException("acls");
            Log.DebugFormat("Creating unmaterialized rights view for \"{0}\"", tblName);

            StringBuilder view = new StringBuilder();
            view.AppendFormat(@"CREATE VIEW {0} AS
SELECT  ""ID"", ""Identity"", 
		(case SUM(""Right"" & 1) when 0 then 0 else 1 end) +
		(case SUM(""Right"" & 2) when 0 then 0 else 2 end) +
		(case SUM(""Right"" & 4) when 0 then 0 else 4 end) +
		(case SUM(""Right"" & 8) when 0 then 0 else 8 end) AS ""Right"" 
FROM (", FormatSchemaName(viewName));
            view.AppendLine();

            foreach (var acl in acls)
            {
                view.AppendFormat(@"  SELECT t1.""ID"" AS ""ID"", t{0}.{1} AS ""Identity"", {2} AS ""Right""",
                    acl.Relations.Count,
                    QuoteIdentifier(acl.Relations.Last().FKColumnName.Single().ColumnName),
                    (int)acl.Right);
                view.AppendLine();
                view.AppendFormat(@"  FROM {0} t1", FormatSchemaName(tblName));
                view.AppendLine();

                int idx = 2;
                foreach (var rel in acl.Relations.Take(acl.Relations.Count - 1))
                {
                    view.AppendFormat(@"  INNER JOIN {0} t{1} ON t{1}.{2} = t{3}.{4}",
                        FormatSchemaName(rel.JoinTableName),
                        idx,
                        QuoteIdentifier(rel.JoinColumnName.Single().ColumnName),
                        idx - 1,
                        QuoteIdentifier(rel.FKColumnName.Single().ColumnName));
                    view.AppendLine();
                    idx++;
                }
                view.AppendFormat(@"  WHERE t{0}.{1} IS NOT NULL",
                    acl.Relations.Count,
                    QuoteIdentifier(acl.Relations.Last().FKColumnName.Single().ColumnName));
                view.AppendLine();
                view.AppendLine("  UNION ALL");
            }
            view.Remove(view.Length - 12, 12);

            view.AppendLine(@") unmaterialized GROUP BY ""ID"", ""Identity""");

            ExecuteNonQuery(view.ToString());
        }

        public override void CreateRefreshRightsOnProcedure(
            ProcRef procName,
            TableRef viewUnmaterializedName,
            TableRef tblName,
            TableRef tblNameRights)
        {
            Log.DebugFormat("Creating refresh rights procedure for \"{0}\"", tblName);
            ExecuteNonQuery(String.Format(
                @"
CREATE OR REPLACE FUNCTION {0}(IN refreshID integer) RETURNS void AS
$BODY$BEGIN
    IF (refreshID IS NULL) THEN
            -- Admin Only: ALTER TABLE {1} DISABLE TRIGGER ALL;
            TRUNCATE TABLE {1};
            INSERT INTO {1} (""ID"", ""Identity"", ""Right"") SELECT ""ID"", ""Identity"", ""Right"" FROM {2};
            -- Admin Only: ALTER TABLE {1} ENABLE TRIGGER ALL;
    ELSE
            DELETE FROM {1} WHERE ""ID"" = refreshID;
            INSERT INTO {1} (""ID"", ""Identity"", ""Right"") SELECT ""ID"", ""Identity"", ""Right"" FROM {2} WHERE ""ID"" = refreshID;
    END IF;
END$BODY$
LANGUAGE 'plpgsql' VOLATILE",
                FormatSchemaName(procName),
                FormatSchemaName(tblNameRights),
                FormatSchemaName(viewUnmaterializedName)));
        }

        public override void ExecRefreshRightsOnProcedure(ProcRef procName)
        {
            Log.DebugFormat("Refreshing rights for [{0}]", procName);
            ExecuteNonQuery(String.Format(@"SELECT {0}(NULL)", FormatSchemaName(procName)));
        }

        public override void ExecRefreshAllRightsProcedure()
        {
            Log.DebugFormat("Refreshing all rights");
            ExecuteNonQuery(string.Format(@"SELECT {0}(NULL)", FormatSchemaName(GetProcedureName("dbo", Zetbox.Generator.Construct.SecurityRulesRefreshAllRightsProcedureName()))));
        }

        public override void CreateRefreshAllRightsProcedure(List<ProcRef> refreshProcNames)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("CREATE OR REPLACE FUNCTION {0}(IN refreshID integer) RETURNS void AS $BODY$BEGIN", FormatSchemaName(GetProcedureName("dbo", Zetbox.Generator.Construct.SecurityRulesRefreshAllRightsProcedureName())));
            sb.AppendLine();
            sb.Append(string.Join("\n", refreshProcNames.Select(i => string.Format("PERFORM {0}(refreshID);", FormatSchemaName(i))).ToArray()));
            sb.AppendLine();
            sb.Append("END$BODY$ LANGUAGE 'plpgsql' VOLATILE");

            ExecuteNonQuery(sb.ToString());
        }

        public override void CreatePositionColumnValidCheckProcedures(ILookup<TableRef, KeyValuePair<TableRef, string>> refSpecs)
        {
            if (refSpecs == null) { throw new ArgumentNullException("refSpecs"); }

            var procName = "RepairPositionColumnValidity";
            var tableProcName = procName + "ByTable";

            foreach (var name in new[] { procName, tableProcName })
            {
                var procRef = new ProcRef(null, "dbo", name);
                if (CheckProcedureExists(procRef))
                {
                    DropProcedure(procRef);
                }
            }

            ExecuteSqlResource(this.GetType(), String.Format(@"Zetbox.Server.SchemaManagement.NpgsqlProvider.Scripts.{0}.sql", procName));

            var createTableProcQuery = new StringBuilder();
            createTableProcQuery.AppendFormat("CREATE OR REPLACE FUNCTION \"dbo\".\"{0}\" (repair boolean, tblName text, colName text) RETURNS boolean AS $BODY$", tableProcName);
            createTableProcQuery.AppendLine();
            createTableProcQuery.AppendLine("DECLARE result boolean DEFAULT false;");
            createTableProcQuery.AppendLine("BEGIN");
            foreach (var tbl in refSpecs)
            {
                createTableProcQuery.AppendFormat("IF tblName IS NULL OR tblName = '{0}' THEN", FormatSchemaName(tbl.Key));
                createTableProcQuery.AppendLine();
                createTableProcQuery.Append("\t");
                foreach (var refSpec in tbl)
                {
                    createTableProcQuery.AppendFormat("IF colName IS NULL OR colName = '{0}{1}' THEN", refSpec.Value, Zetbox.API.Helper.PositionSuffix);
                    createTableProcQuery.AppendLine();
                    createTableProcQuery.AppendFormat(
                        // TODO: use named parameters with 9.0: "\t\tresult := \"RepairPositionColumnValidity\"(repair := repair, tblName := '{0}', refTblName := '{1}', fkColumnName := '{2}', fkPositionName := '{2}{3}');",
                        "\t\tresult := \"dbo\".\"RepairPositionColumnValidity\"(repair, '{0}', '{1}', '{2}', '{2}{3}');",
                        FormatSchemaName(tbl.Key),
                        FormatSchemaName(refSpec.Key),
                        refSpec.Value,
                        Zetbox.API.Helper.PositionSuffix);
                    createTableProcQuery.AppendLine();
                    createTableProcQuery.AppendLine("\t\tIF NOT repair AND result THEN RETURN true; END IF;");
                    createTableProcQuery.AppendFormat("\tELS");
                }

                // Complete ELS-E
                createTableProcQuery.AppendLine("E");
                createTableProcQuery.AppendLine("\t\tRAISE EXCEPTION 'Column [%].[%] not found', tblName, colName;");
                createTableProcQuery.AppendLine("\tEND IF;");

                createTableProcQuery.Append("ELS");
            }

            if (refSpecs.Count > 0)
            {
                // Complete ELS-E
                createTableProcQuery.AppendLine("E");
            }
            createTableProcQuery.AppendLine("\tRAISE EXCEPTION 'Table [%] not found', tblName;");
            if (refSpecs.Count > 0)
            {
                createTableProcQuery.AppendLine("END IF;");
            }
            createTableProcQuery.AppendLine("RETURN result;");
            createTableProcQuery.AppendLine("END;");
            createTableProcQuery.AppendLine("$BODY$ LANGUAGE 'plpgsql' VOLATILE;");
            ExecuteNonQuery(createTableProcQuery.ToString());
        }

        private const string sequenceNumberProcedure = @"CREATE OR REPLACE FUNCTION {0}(""seqNumber"" uuid, out ""result"" integer)
  AS
$BODY$
BEGIN

LOCK TABLE ""base"".""SequenceData"";
UPDATE ""base"".""SequenceData"" sd SET ""CurrentNumber"" = ""CurrentNumber"" + 1 FROM ""base"".""Sequences"" s WHERE s.""ExportGuid"" = ""seqNumber"" AND s.""ID"" = sd.""fk_Sequence"";
SELECT ""CurrentNumber"" INTO result FROM ""base"".""SequenceData"" sd JOIN ""base"".""Sequences"" s ON (s.""ID"" = sd.""fk_Sequence"") WHERE s.""ExportGuid"" = ""seqNumber"";

IF result IS NULL THEN
	result := 1;
	INSERT INTO ""base"".""SequenceData"" (""fk_Sequence"", ""CurrentNumber"")
        SELECT s.""ID"", 1 FROM ""base"".""Sequences"" s WHERE s.""ExportGuid"" = ""seqNumber"";
END IF;

END$BODY$
  LANGUAGE plpgsql VOLATILE";

        public override void CreateSequenceNumberProcedure()
        {
            ExecuteNonQuery(string.Format(sequenceNumberProcedure, FormatSchemaName(GetProcedureName("dbo", "GetSequenceNumber"))));
        }

        public override void CreateContinuousSequenceNumberProcedure()
        {
            ExecuteNonQuery(string.Format(sequenceNumberProcedure, FormatSchemaName(GetProcedureName("dbo", "GetContinuousSequenceNumber"))));
        }

        public override IDataReader ReadTableData(TableRef tbl, IEnumerable<string> colNames)
        {
            var columns = String.Join(",", colNames.Select(n => QuoteIdentifier(n)).ToArray());
            var query = String.Format("SELECT {0} FROM {1}", columns, FormatSchemaName(tbl));

            return ReadTableData(query);
        }

        public override IDataReader ReadTableData(string sql)
        {
            var cmd = CreateCommand(sql);
            return cmd.ExecuteReader();
        }

        private IEnumerable<ColumnRef> FetchColumns(Join join, Dictionary<Join, string> aliases, ref int nextIdx)
        {
            var result = join
                .JoinColumnName
                .Concat(join.FKColumnName);

            foreach (var subJoin in join.Joins)
            {
                result = result.Concat(FetchColumns(subJoin, aliases, ref nextIdx));
            }

            aliases[join] = String.Format(CultureInfo.InvariantCulture, "t{0}", nextIdx);
            nextIdx += 1;

            return result;
        }

        public override IDataReader ReadJoin(TableRef tbl, IEnumerable<ProjectionColumn> colNames, IEnumerable<Join> joins)
        {
            if (tbl == null)
                throw new ArgumentNullException("tbl");
            if (colNames == null)
                throw new ArgumentNullException("colNames");
            if (joins == null)
                throw new ArgumentNullException("joins");

            int idx = 1;
            var join_alias = new Dictionary<Join, string>();

            var allColumns = joins
                .SelectMany(j => FetchColumns(j, join_alias, ref idx));

            var allColumnsByJoin = allColumns
                .Concat(colNames.Cast<ColumnRef>())
                .ToLookup(cr => cr.Source);

            var joinQueryPart = new StringBuilder();
            foreach (var join in joins)
            {
                AddReadJoin(joinQueryPart, join, join_alias, colNames, allColumnsByJoin);
            }

            var columns = String.Join(",\n", colNames.Select(pc =>
            {
                string result = "\t";
                if (pc.Source == ColumnRef.PrimaryTable)
                    result += string.Format("t0.{0}", QuoteIdentifier(pc.ColumnName));
                else
                    result += string.Format("{0}.{1}", join_alias[pc.Source], QuoteIdentifier(pc.ColumnName));

                if (!string.IsNullOrEmpty(pc.NullValue))
                {
                    result = string.Format("COALESCE({0}, {1})", result, pc.NullValue);
                }
                if (!string.IsNullOrEmpty(pc.Alias))
                {
                    result += " AS " + QuoteIdentifier(pc.Alias);
                }
                return result;
            }).ToArray());

            var query = String.Format("SELECT \n{0} \nFROM {1} t0{2}", columns, FormatSchemaName(tbl), joinQueryPart.ToString());
            return ReadTableData(query);
        }

        private void AddReadJoin(StringBuilder query, Join join, Dictionary<Join, string> join_alias, IEnumerable<ProjectionColumn> colNames, ILookup<Join, ColumnRef> allColumnsByJoin)
        {
            if (join.JoinColumnName.Length != join.FKColumnName.Length)
                throw new ArgumentException(string.Format("Column count on Join '{0}' does not match", join), "join");

            foreach (var j in join.Joins)
            {
                AddReadJoin(query, j, join_alias, colNames, allColumnsByJoin);
            }

            var alias = join_alias[join];

            // Select data and join-id columns for dblink
            var joinColumns = allColumnsByJoin[join]
                .Concat(join.JoinColumnName.Where(cr => cr.Source == ColumnRef.Local))
                .Concat(join.FKColumnName.Where(cr => cr.Source == ColumnRef.Local))
                .Select(cr => new KeyValuePair<string, DbType>(cr.ColumnName, cr.Type.Value))
                .Distinct()
                .ToList();

            if (join.JoinTableName.Database != CurrentConnection.Database)
            {
                // need dblink call, yay!
                DblinkConnect(join.JoinTableName);

                query.AppendFormat("\n  {0} JOIN dblink('{1}', 'SELECT {2} FROM {3}') AS {4}({5}) ON ",
                    join.Type.ToString().ToUpper(),
                    join.JoinTableName.Database,
                    String.Join(",", joinColumns.Select(cn => QuoteIdentifier(cn.Key)).ToArray()),
                    FormatSchemaName(join.JoinTableName),
                    alias,
                    String.Join(",", joinColumns.Select(cn => QuoteIdentifier(cn.Key) + " " + DbTypeToNative(cn.Value)).ToArray())
                    );
            }
            else
            {
                query.AppendFormat("\n  {2} JOIN {0} {1} ON ", FormatSchemaName(join.JoinTableName), alias, join.Type.ToString().ToUpper());
            }

            for (int i = 0; i < join.JoinColumnName.Length; i++)
            {
                var joinColumn = string.Format("{0}.{1}",
                    join.JoinColumnName[i].Source == ColumnRef.PrimaryTable ? "t0" : (join.JoinColumnName[i].Source == ColumnRef.Local ? alias : join_alias[join.JoinColumnName[i].Source]),
                    QuoteIdentifier(join.JoinColumnName[i].ColumnName));
                var fkColumn = string.Format("{0}.{1}",
                    join.FKColumnName[i].Source == ColumnRef.PrimaryTable ? "t0" : (join.FKColumnName[i].Source == ColumnRef.Local ? alias : join_alias[join.FKColumnName[i].Source]),
                    QuoteIdentifier(join.FKColumnName[i].ColumnName));

                if (join.CompareNullsAsEqual[i])
                {
                    query.AppendFormat("({0} = {1} OR ({0} IS NULL AND {1} IS NULL))",
                        joinColumn,
                        fkColumn);
                }
                else
                {
                    query.AppendFormat("{0} = {1}",
                        joinColumn,
                        fkColumn);
                }

                if (i < join.JoinColumnName.Length - 1)
                {
                    query.Append(" AND ");
                }
            }
        }

        private const string COPY_SEPARATOR = "|";
        private const string COPY_NULL = @"\N";

        public override void WriteTableData(TableRef destTbl, IDataReader source, IEnumerable<string> colNames)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (colNames == null)
                throw new ArgumentNullException("colNames");

            ExecuteNonQuery(String.Format("ALTER TABLE {0} DISABLE TRIGGER USER", FormatSchemaName(destTbl)));

            var cols = colNames.Select(n => QuoteIdentifier(n)).ToArray();
            var query = String.Format("COPY {0} ({1}) FROM STDIN WITH DELIMITER '{2}' NULL E'{3}'", FormatSchemaName(destTbl), String.Join(",", cols), COPY_SEPARATOR, COPY_NULL.Replace(@"\", @"\\"));
            _log.DebugFormat("Copy from: [{0}]", query);
            _copyLog.Info(query);
            var bulkCopy = new NpgsqlCopyIn(query, CurrentConnection);

            try
            {
                bulkCopy.Start();
                // explicitly use Npgsql's default encoding, without BOM
                using (var dst = new StreamWriter(bulkCopy.CopyStream, new System.Text.UTF8Encoding(false)))
                {
                    // normal windows newline confuses npgsql
                    dst.NewLine = "\n";

                    while (source.Read())
                    {
                        var vals = new string[cols.Length];

                        for (int srcIdx = 0; srcIdx < cols.Length; srcIdx++)
                        {
                            object val = null;
                            if (source.IsDBNull(srcIdx) || (val = source.GetValue(srcIdx)) == null)
                            {
                                vals[srcIdx] = COPY_NULL;
                            }
                            else
                            {
                                var date = val as DateTime?;
                                if (date != null)
                                {
                                    vals[srcIdx] = date.Value.ToString("s", CultureInfo.InvariantCulture);
                                }
                                else
                                {
                                    var dec = val as decimal?;
                                    if (dec != null)
                                    {
                                        vals[srcIdx] = dec.Value.ToString(CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        var dbl = val as double?;
                                        if (dbl != null)
                                        {
                                            vals[srcIdx] = dbl.Value.ToString(CultureInfo.InvariantCulture);
                                        }
                                        else
                                        {
                                            var flt = val as float?;
                                            if (flt != null)
                                            {
                                                vals[srcIdx] = flt.Value.ToString(CultureInfo.InvariantCulture);
                                            }
                                            else
                                            {
                                                var lng = val as long?;
                                                if (lng != null)
                                                {
                                                    vals[srcIdx] = lng.Value.ToString(CultureInfo.InvariantCulture);
                                                }
                                                else
                                                {
                                                    var integ = val as int?;
                                                    if (integ != null)
                                                    {
                                                        vals[srcIdx] = integ.Value.ToString(CultureInfo.InvariantCulture);
                                                    }
                                                    else
                                                    {
                                                        var shrt = val as short?;
                                                        if (shrt != null)
                                                        {
                                                            vals[srcIdx] = shrt.Value.ToString(CultureInfo.InvariantCulture);
                                                        }
                                                        else
                                                        {
                                                            var boolean = val as bool?;
                                                            if (boolean != null)
                                                            {
                                                                vals[srcIdx] = boolean.Value.ToString(CultureInfo.InvariantCulture);
                                                            }
                                                            else
                                                            {
                                                                var str = val as string;
                                                                if (str != null)
                                                                {
                                                                    vals[srcIdx] = str;
                                                                }
                                                                else
                                                                {
                                                                    // error out
                                                                    var strVal = val.ToString();
                                                                    if (strVal.Length > 100)
                                                                    {
                                                                        str = str.Substring(0, 100);
                                                                        str += " ...";
                                                                    }
                                                                    throw new NotSupportedException(String.Format("Cannot transform [{0}] of Type [{1}] for WriteTableData", strVal, val.GetType()));
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                vals[srcIdx] = CopyEscape(vals[srcIdx]);
                            }
                        }
                        string line = String.Join(COPY_SEPARATOR, vals);
                        _copyLog.Debug(line);
                        dst.WriteLine(line);
                    }
                }
                bulkCopy.End();
            }
            catch (Exception ex)
            {
                Log.Error("Error bulk writing to destination", ex);
                bulkCopy.Cancel("Aborting COPY operation");
                throw;
            }
            finally
            {
                ExecuteNonQuery(String.Format("ALTER TABLE {0} ENABLE TRIGGER USER", FormatSchemaName(destTbl)));
            }
        }

        private static string CopyEscape(string p)
        {
            // TODO: remove \0 replacement. should be done by migration transformations/data cleaner
            return p.Replace("\\", "\\\\").Replace("\r", "\\r").Replace("\n", "\\n").Replace(COPY_SEPARATOR, "\\" + COPY_SEPARATOR).Replace("\0", "");
        }

        public override void WriteTableData(TableRef destTbl, IEnumerable<string> colNames, System.Collections.IEnumerable values)
        {
            throw new NotImplementedException();
        }

        public override void WriteDefaultValue(TableRef tblName, string colName, object value)
        {
            ExecuteNonQuery(String.Format("UPDATE {0} SET {1} = @val WHERE {1} IS NULL;",
                                FormatSchemaName(tblName),
                                QuoteIdentifier(colName)),
                             new Dictionary<string, object>() { { "@val", value } });
        }

        public override void WriteDefaultValue(TableRef tblName, string colName, object value, IEnumerable<string> discriminatorFilter)
        {
            if (discriminatorFilter == null)
            {
                WriteDefaultValue(tblName, colName, value);
            }
            else
            {
                var parameters = ToAdoParameters(discriminatorFilter);
                var discriminatorParams = string.Join(",", parameters.Keys);
                parameters["@val"] = value;

                ExecuteNonQuery(String.Format(
                    "UPDATE {0} SET {1} = @val WHERE {1} IS NULL AND {2} IN ({3})",
                        FormatSchemaName(tblName),
                        QuoteIdentifier(colName),
                        QuoteIdentifier(TableMapper.DiscriminatorColumnName),
                        discriminatorParams),
                    parameters);
            }
        }

        public override void WriteGuidDefaultValue(TableRef tblName, string colName)
        {
            ExecuteNonQuery(String.Format("UPDATE {0} SET {1} = uuid_generate_v4() WHERE {1} IS NULL;",
                                FormatSchemaName(tblName),
                                QuoteIdentifier(colName)));
        }

        public override void WriteGuidDefaultValue(TableRef tblName, string colName, IEnumerable<string> discriminatorFilter)
        {
            if (discriminatorFilter == null)
            {
                WriteGuidDefaultValue(tblName, colName);
            }
            else
            {
                var parameters = ToAdoParameters(discriminatorFilter);

                ExecuteNonQuery(String.Format(
                    "UPDATE {0} SET {1} = uuid_generate_v4() WHERE {1} IS NULL AND {2} IN ({3})",
                        FormatSchemaName(tblName),
                        QuoteIdentifier(colName),
                        QuoteIdentifier(TableMapper.DiscriminatorColumnName),
                        string.Join(",", parameters.Keys)),
                    parameters);
            }
        }

        public override void RefreshDbStats()
        {
            Log.Info("Vacuuming database");
            ExecuteNonQuery("VACUUM ANALYZE");
        }

        public override IEnumerable<ProcRef> GetFunctionNames()
        {
            throw new NotImplementedException();
        }

        public override bool CheckFunctionExists(ProcRef funcName)
        {
            throw new NotImplementedException();
        }

        public override void DropFunction(ProcRef funcName)
        {
            throw new NotImplementedException();
        }
    }
}
