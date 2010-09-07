
namespace Kistl.Server.SchemaManagement
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Server;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.App.Packaging;
    using Kistl.Server.Generators;
    using Kistl.Server.Generators.Extensions;

    public partial class SchemaManager
        : IDisposable
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Schema");

        #region Fields
        private IKistlContext schema;
        private ISchemaProvider db;
        private bool repair = false;
        private readonly Cases Case;
        private readonly KistlConfig config;
        #endregion

        #region Constructor

        public SchemaManager(ISchemaProvider provider, IKistlContext schema, IKistlContext savedSchema, KistlConfig config)
        {
            this.config = config;
            this.schema = schema;
            this.db = provider;
            this.Case = new Cases(schema, provider, savedSchema);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // Do not dispose "schema" -> passed to this class
            if (Case != null) Case.Dispose();
            if (db != null) db.Dispose();
        }

        #endregion

        #region Private Functions

        private void WriteReportHeader(string reportName)
        {
            Log.InfoFormat("== {0} ==", reportName);
            Log.InfoFormat("Date: {0}", DateTime.Now);
            Log.InfoFormat("Database: {0}", config.Server.ConnectionString);
            Log.Info(String.Empty);
        }

        #endregion

        #region CreateJoinList
        [Serializable]
        public class JoinListException : Exception
        {
            public JoinListException()
            {
            }

            public JoinListException(string message)
                : base(message)
            {
            }

            public JoinListException(string message, Exception innerException)
                : base(message, innerException)
            {
            }
            protected JoinListException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }

        public static IList<Join> CreateJoinList(ISchemaProvider db, ObjectClass objClass, IEnumerable<Relation> relations)
        {
            return CreateJoinList(db, objClass, relations, null);
        }

        public static IList<Join> CreateJoinList(ISchemaProvider db, ObjectClass objClass, IEnumerable<Relation> relations, Relation until)
        {
            if (db == null) throw new ArgumentNullException("db");
            if (objClass == null) throw new ArgumentNullException("objClass");
            if (relations == null) throw new ArgumentNullException("relations");

            List<Join> result = new List<Join>();
            string lastColumName = "ID";
            Join lastJoin = null;
            ObjectClass lastType = objClass;
            foreach (var rel in relations)
            {
                RelationEnd lastRelEnd;
                RelationEnd nextRelEnd;

                if (rel.A.Type == lastType)
                {
                    lastRelEnd = rel.A;
                    nextRelEnd = rel.B;
                }
                else if (rel.B.Type == lastType)
                {
                    lastRelEnd = rel.B;
                    nextRelEnd = rel.A;
                }
                else
                {
                    throw new JoinListException(string.Format("Unable to create JoinList: Unable to navigate from '{0}' over '{1}' to next type", lastType.Name, rel.ToString()));
                }

                if (rel.GetRelationType() == RelationType.n_m)
                {
                    var viewRel = new Join();
                    result.Add(viewRel);
                    viewRel.JoinTableName = db.GetQualifiedTableName(rel.GetRelationTableName());
                    viewRel.JoinColumnName = new[] { new ColumnRef(Construct.ForeignKeyColumnName(lastRelEnd), ColumnRef.Local) };
                    viewRel.FKColumnName = new[] { new ColumnRef(lastColumName, lastJoin) };
                    lastJoin = viewRel;

                    viewRel = new Join();
                    result.Add(viewRel);
                    viewRel.JoinTableName = db.GetQualifiedTableName(nextRelEnd.Type.TableName);
                    viewRel.JoinColumnName = new[] { new ColumnRef("ID", ColumnRef.Local) };
                    viewRel.FKColumnName = new[] { new ColumnRef(Construct.ForeignKeyColumnName(nextRelEnd), lastJoin) };

                    lastColumName = viewRel.FKColumnName.Single().ColumnName;
                    lastJoin = viewRel;
                }
                else
                {
                    var viewRel = new Join();
                    result.Add(viewRel);
                    viewRel.JoinTableName = db.GetQualifiedTableName(nextRelEnd.Type.TableName);
                    string localCol;
                    string fkCol;
                    if (nextRelEnd == rel.A && rel.Storage == StorageType.MergeIntoB)
                    {
                        localCol = "ID";
                        fkCol = Construct.ForeignKeyColumnName(nextRelEnd);
                    }
                    else if (nextRelEnd == rel.A && rel.Storage == StorageType.MergeIntoA)
                    {
                        localCol = Construct.ForeignKeyColumnName(lastRelEnd);
                        fkCol = "ID";
                    }
                    else if (nextRelEnd == rel.B && rel.Storage == StorageType.MergeIntoB)
                    {
                        localCol = "ID";
                        fkCol = Construct.ForeignKeyColumnName(lastRelEnd);
                    }
                    else if (nextRelEnd == rel.B && rel.Storage == StorageType.MergeIntoA)
                    {
                        localCol = Construct.ForeignKeyColumnName(nextRelEnd);
                        fkCol = "ID";
                    }
                    else
                    {
                        throw new NotSupportedException(string.Format("StorageType {0} is not supported", rel.Storage));
                    }
                    viewRel.JoinColumnName = new[] { new ColumnRef(localCol, ColumnRef.Local) };
                    viewRel.FKColumnName = new[] { new ColumnRef(fkCol, lastJoin) };
                    lastColumName = localCol;
                    lastJoin = viewRel;
                }

                lastType = nextRelEnd.Type;
                if (rel == until) return result;
            }
            return result;
        }
        #endregion

        #region Helper
        public static DefaultConstraint GetDefaultContraint(Property prop)
        {
            if (prop == null) throw new ArgumentNullException("prop");
            if (prop.DefaultValue is Kistl.App.Base.NewGuidDefaultValue)
            {
                return new NewGuidDefaultConstraint();
            }
            else if (prop.DefaultValue is Kistl.App.Base.CurrentDateTimeDefaultValue)
            {
                return new DateTimeDefaultConstraint();
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region SavedSchema

        public static void LoadSavedSchemaInto(ISchemaProvider provider, IKistlContext targetCtx)
        {
            if (provider == null) { throw new ArgumentNullException("provider"); }
            if (targetCtx == null) { throw new ArgumentNullException("targetCtx"); }

            string schema = provider.GetSavedSchema().TrimEnd((char)0); // Trim possible C++/Database/whatever ending 0 char
            if (!string.IsNullOrEmpty(schema))
            {
                using (var ms = new MemoryStream(ASCIIEncoding.Default.GetBytes(schema)))
                {
                    Importer.LoadFromXml(targetCtx, ms);
                }
            }
        }

        private void SaveSchema(IKistlContext schema)
        {
            using (Logging.Log.DebugTraceMethodCall())
            {
                using (var ms = new MemoryStream())
                {
                    Exporter.PublishFromContext(schema, ms, new string[] { "*" });
                    string schemaStr = ASCIIEncoding.Default.GetString(ms.GetBuffer()).TrimEnd((char)0); // Trim possible C++/Database/whatever ending 0 char
                    db.SaveSchema(schemaStr);
                }
            }
        }
        #endregion
    }
}
