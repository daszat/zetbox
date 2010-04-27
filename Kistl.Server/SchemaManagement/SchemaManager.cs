using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using Kistl.API.Utils;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using System.Runtime.Serialization;
using Kistl.API.Configuration;

namespace Kistl.Server.SchemaManagement
{
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
        
        public static IList<Join> CreateJoinList(ObjectClass objClass, IEnumerable<Relation> relations)
        {
            return CreateJoinList(objClass, relations, null);
        }

        public static IList<Join> CreateJoinList(ObjectClass objClass, IEnumerable<Relation> relations, Relation until)
        {
            if (objClass == null) throw new ArgumentNullException("objClass");
            if (relations == null) throw new ArgumentNullException("relations");

            List<Join> result = new List<Join>();
            string lastColumName = "ID";
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
                    viewRel.JoinTableName = rel.GetRelationTableName();
                    viewRel.JoinColumnName = Construct.ForeignKeyColumnName(lastRelEnd);
                    viewRel.FKColumnName = lastColumName;

                    viewRel = new Join();
                    result.Add(viewRel);
                    viewRel.JoinTableName = nextRelEnd.Type.TableName;
                    viewRel.JoinColumnName = "ID";
                    viewRel.FKColumnName = Construct.ForeignKeyColumnName(nextRelEnd);

                    lastColumName = viewRel.FKColumnName;
                }
                else
                {
                    var viewRel = new Join();
                    result.Add(viewRel);
                    viewRel.JoinTableName = nextRelEnd.Type.TableName;
                    viewRel.JoinColumnName = "ID";
                    viewRel.FKColumnName = Construct.ForeignKeyColumnName(nextRelEnd);

                    lastColumName = "ID";
                }

                lastType = nextRelEnd.Type;
                if (rel == until) return result;
            }
            return result;
        }        
        #endregion

        #region GetDbType
        internal static System.Data.DbType GetDbType(Property p)
        {
            if (p is ObjectReferenceProperty)
            {
                return System.Data.DbType.Int32;
            }
            else if (p is EnumerationProperty)
            {
                return System.Data.DbType.Int32;
            }
            else if (p is IntProperty)
            {
                return System.Data.DbType.Int32;
            }
            else if (p is StringProperty)
            {
                return System.Data.DbType.String;
            }
            else if (p is DoubleProperty)
            {
                return System.Data.DbType.Double;
            }
            else if (p is BoolProperty)
            {
                return System.Data.DbType.Boolean;
            }
            else if (p is DateTimeProperty)
            {
                return System.Data.DbType.DateTime;
            }
            else if (p is GuidProperty)
            {
                return System.Data.DbType.Guid;
            }
            else
            {
                throw new DBTypeNotFoundException(p);
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
                    Packaging.Importer.LoadFromXml(targetCtx, ms);
                }
            }
        }

        private void SaveSchema(IKistlContext schema)
        {
            using (Logging.Log.DebugTraceMethodCall())
            {
                using (var ms = new MemoryStream())
                {
                    Packaging.Exporter.PublishFromContext(schema, ms, new string[] { "*" });
                    string schemaStr = ASCIIEncoding.Default.GetString(ms.GetBuffer());
                    db.SaveSchema(schemaStr);
                }
            }
        }
        #endregion
    }
}
