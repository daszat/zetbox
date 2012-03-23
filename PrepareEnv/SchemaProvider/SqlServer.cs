using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;

namespace PrepareEnv.SchemaProvider
{
    class SqlServer : AdoNetProvider<SqlConnection, SqlTransaction, SqlCommand>
    {
        public override void Copy(string source, string dest)
        {
            var srcBuilder = new SqlConnectionStringBuilder(source);
            var destBuilder = new SqlConnectionStringBuilder(dest);
            var srcDB = srcBuilder.InitialCatalog;
            var destDB = destBuilder.InitialCatalog;
            destBuilder.InitialCatalog = "master";
            var backupFile = GetBackupFile();
            try
            {
                string dbname;
                string logname;
                // Backup
                using (var db = new SqlConnection(source))
                {
                    db.Open();
                    var cmd = new SqlCommand(string.Format("BACKUP DATABASE [{0}] TO DISK = '{1}' WITH FORMAT", srcDB, backupFile), db);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand(string.Format("SELECT name FROM [{0}].[sys].[sysfiles] where filename like '%.mdf'", srcDB), db);
                    dbname = (string)cmd.ExecuteScalar();

                    cmd = new SqlCommand(string.Format("SELECT name FROM [{0}].[sys].[sysfiles] where filename like '%.ldf'", srcDB), db);
                    logname = (string)cmd.ExecuteScalar();
                }

                // Restore
                string dbfile;
                string logfile;
                using (var db = new SqlConnection(destBuilder.ToString()))
                {
                    db.Open();

                    var cmd = new SqlCommand(string.Format("SELECT filename FROM [{0}].[sys].[sysfiles] where filename like '%.mdf'", destDB), db);
                    dbfile = (string)cmd.ExecuteScalar();

                    cmd = new SqlCommand(string.Format("SELECT filename FROM [{0}].[sys].[sysfiles] where filename like '%.ldf'", destDB), db);
                    logfile = (string)cmd.ExecuteScalar();
                }

                #region Kill existing user
                using (var db = new SqlConnection(destBuilder.ToString()))
                {
                    db.Open();
                    var cmd = new SqlCommand(string.Format(@"
-- Kill existing users
DECLARE @dbname varchar(40)
select @dbname = '{0}'
DECLARE @strSQL varchar(255)

CREATE table #tmpUsers(
spid int,
eid int,
status varchar(30),
loginname varchar(50),
hostname varchar(50),
blk int,
dbname varchar(50),
cmd varchar(30),
request_id int )

INSERT INTO #tmpUsers EXEC SP_WHO


DECLARE LoginCursor CURSOR
READ_ONLY
FOR SELECT spid FROM #tmpUsers WHERE cmd != 'CHECKPOINT' AND dbname = @dbname

DECLARE @spid varchar(30)
OPEN LoginCursor

FETCH NEXT FROM LoginCursor INTO @spid
WHILE (@@fetch_status <> -1)
BEGIN
IF (@@fetch_status <> -2)
BEGIN
PRINT 'Killing ' + @spid
SET @strSQL ='KILL ' + @spid
EXEC (@strSQL)
END
FETCH NEXT FROM LoginCursor INTO @spid
END

CLOSE LoginCursor
DEALLOCATE LoginCursor

DROP table #tmpUsers", destDB), db);
                    cmd.ExecuteNonQuery();
                }

                SqlConnection.ClearAllPools();
                #endregion

                using (var db = new SqlConnection(destBuilder.ToString()))
                {
                    db.Open();
                    var cmd = new SqlCommand(string.Format(@"RESTORE DATABASE [{0}] FROM DISK = '{1}' 
	                                                        WITH RECOVERY, REPLACE, 
	                                                        MOVE '{2}' to '{3}',
	                                                        MOVE '{4}' to '{5}'",
                              destDB, backupFile, 
                              dbname, dbfile, 
                              logname, logfile), db);
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                // cleanup
                File.Delete(backupFile);
            }
        }

        public override void DropAllObjects()
        {
            foreach (var rel in GetFKConstraintNames().ToList())
            {
                DropFKConstraint(rel.TableName, rel.ConstraintName);
            }

            foreach (var tbl in GetTableNames().ToList())
            {
                DropTable(tbl);
            }

            foreach (var v in GetViewNames().ToList())
            {
                DropView(v);
            }

            foreach (var sp in GetProcedureNames().ToList())
            {
                DropProcedure(sp);
            }

            foreach (var sp in GetFunctionNames().ToList())
            {
                DropFunction(sp);
            }

            foreach (var s in GetSchemaNames().ToList())
            {
                // Do not drop schema dbo!
                if (!string.Equals(s, "dbo", StringComparison.InvariantCultureIgnoreCase))
                {
                    DropSchema(s);
                }
            }
        }

        #region get names
        private IEnumerable<TableConstraintNamePair> GetFKConstraintNames()
        {
            return ExecuteReader("SELECT c.name, t.name, s.name FROM sys.objects c INNER JOIN sys.sysobjects t ON t.id = c.parent_object_id INNER JOIN sys.schemas s on c.schema_id = s.schema_id WHERE c.type IN (N'F') ORDER BY c.name")
                .Select(rd => new TableConstraintNamePair()
                {
                    ConstraintName = rd.GetString(0),
                    TableName = new TableRef("", rd.GetString(2), rd.GetString(1))
                });
        }

        private IEnumerable<TableRef> GetTableNames()
        {
            return ExecuteReader("SELECT s.name, o.name FROM sys.objects o JOIN sys.schemas s ON o.schema_id = s.schema_id WHERE o.type = N'U' AND o.name <> 'sysdiagrams'")
                .Select(rd => new TableRef("", rd.GetString(0), rd.GetString(1)));
        }

        private IEnumerable<TableRef> GetViewNames()
        {
            return ExecuteReader("SELECT s.name, o.name FROM sys.objects o JOIN sys.schemas s ON o.schema_id = s.schema_id WHERE o.type = N'V' AND o.name <> 'sysdiagrams'")
                .Select(rd => new TableRef("", rd.GetString(0), rd.GetString(1)));
        }

        private IEnumerable<ProcRef> GetProcedureNames()
        {
            return ExecuteReader("SELECT s.name, c.name FROM sys.objects c INNER JOIN sys.schemas s on c.schema_id = s.schema_id WHERE c.type IN (N'P') ORDER BY c.name")
                .Select(rd => new ProcRef("", rd.GetString(0), rd.GetString(1)));
        }

        private IEnumerable<ProcRef> GetFunctionNames()
        {
            return ExecuteReader("SELECT s.name, c.name FROM sys.objects c INNER JOIN sys.schemas s on c.schema_id = s.schema_id WHERE c.type IN (N'FN') ORDER BY c.name")
                .Select(rd => new ProcRef("", rd.GetString(0), rd.GetString(1)));
        }

        private IEnumerable<string> GetSchemaNames()
        {
            // Exclude all schemas defined in model database but include dbo
            return ExecuteReader("select s.name from sys.schemas s where not exists (select * from model.sys.schemas mdl where mdl.name = s.name and s.name <> 'dbo')")
                .Select(rd => rd.GetString(0));
        }
        #endregion

        #region drop things
        private void DropFKConstraint(TableRef tblName, string constraintName)
        {
            ExecuteNonQuery(String.Format("ALTER TABLE {0} DROP CONSTRAINT {1}",
                FormatSchemaName(tblName),
                QuoteIdentifier(constraintName)));
        }
        private void DropTable(TableRef tblName)
        {
            ExecuteNonQuery(String.Format("DROP TABLE {0}", FormatSchemaName(tblName)));
        }
        private void DropView(TableRef viewName)
        {
            ExecuteNonQuery(String.Format("DROP VIEW {0}",
                FormatSchemaName(viewName)));
        }
        private void DropProcedure(ProcRef procName)
        {
            ExecuteNonQuery(string.Format("DROP PROCEDURE {0}", FormatSchemaName(procName)));
        }
        private void DropFunction(ProcRef funcName)
        {
            ExecuteNonQuery(string.Format("DROP FUNCTION {0}", FormatSchemaName(funcName)));
        }
        private void DropSchema(string schemaName)
        {
            ExecuteNonQuery(string.Format("DROP SCHEMA {0}", QuoteIdentifier(schemaName)));
        }
        #endregion

        protected override SqlCommand CreateCommand(string query)
        {
            return new SqlCommand(query, CurrentConnection, CurrentTransaction);
        }

        protected override string FormatSchemaName(DboRef dbo)
        {
            return string.Format("[{0}].[{1}]", dbo.Schema, dbo.Name);
        }

        protected override string QuoteIdentifier(string name)
        {
            return "[" + name + "]";
        }

        protected override SqlConnection CreateConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        protected override SqlTransaction CreateTransaction()
        {
            return CurrentConnection.BeginTransaction();
        }
    }
}
