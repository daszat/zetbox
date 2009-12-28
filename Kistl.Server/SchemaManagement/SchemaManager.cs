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
        #endregion

        #region Constructor
        public SchemaManager(IKistlContext schema)
        {
            this.schema = schema;
            db = GetProvider();
            Case = new Cases(schema, db);
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
        private static ISchemaProvider GetProvider()
        {
            return new SchemaProvider.SQLServer.SchemaProvider();
        }

        private void WriteReportHeader(string reportName)
        {
            Log.InfoFormat("== {0} ==", reportName);
            Log.InfoFormat("Date: {0}", DateTime.Now);
            Log.InfoFormat("Database: {0}", ApplicationContext.Current.Configuration.Server.ConnectionString);
            Log.Info(String.Empty);
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
        public static IKistlContext GetSavedSchema()
        {
            IKistlContext ctx = new MemoryContext();
            using (ISchemaProvider db = GetProvider())
            {
                string schema = db.GetSavedSchema().TrimEnd((char)0); // Trim possible C++/Database/whatever ending 0 char
                if (!string.IsNullOrEmpty(schema))
                {
                    using (var ms = new MemoryStream(ASCIIEncoding.Default.GetBytes(schema)))
                    {
                        Packaging.Importer.LoadFromXml(ctx, ms);
                    }
                }
            }
            return ctx;
        }

        private void SaveSchema(IKistlContext schema)
        {
            using (Logging.Log.DebugTraceMethodCall())
            {
                using (var ms = new MemoryStream())
                {
                    Packaging.Exporter.Publish(schema, ms, new string[] { "*" });
                    string schemaStr = ASCIIEncoding.Default.GetString(ms.GetBuffer());
                    db.SaveSchema(schemaStr);
                }
            }
        }
        #endregion
    }
}
