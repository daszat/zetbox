using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Generators;
// TODO: Das gehört angeschaut.
using Kistl.Server.Generators.EntityFramework.Implementation;

namespace Kistl.Server.SchemaManagement
{
    public partial class SchemaManager : IDisposable
    {
        #region Fields
        private IKistlContext schema;
        private ISchemaProvider db;
        private TextWriter report;
        private bool repair = false;
        private Cases Case { get; set; }
        #endregion

        #region Constructor
        public SchemaManager(IKistlContext schema, Stream reportStream)
        {
            this.schema = schema;
            report = new StreamWriter(reportStream);
            db = GetProvider();
            Case = new Cases(schema, db, report);
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // Do not dispose "schema" -> passed to this class
            if (Case != null) Case.Dispose();
            if (db != null) db.Dispose();
            if (report != null) report.Dispose();
        }

        #endregion

        #region Private Functions
        private static ISchemaProvider GetProvider()
        {
            return new SchemaProvider.SQLServer.SchemaProvider();
        }

        private void WriteReportHeader(string reportName)
        {
            report.WriteLine("== {0} ==", reportName);
            report.WriteLine("Date: {0}", DateTime.Now);
            report.WriteLine("Database: {0}", ApplicationContext.Current.Configuration.Server.ConnectionString);
            report.WriteLine();
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
                        Packaging.Importer.Import(ctx, ms);
                    }
                }
            }
            return ctx;
        }

        private void SaveSchema(IKistlContext schema)
        {
            using (var ms = new MemoryStream())
            {
                Packaging.Exporter.Export(schema, ms, new string[] { "Kistl.App.Base" });
                string schemaStr = ASCIIEncoding.Default.GetString(ms.GetBuffer());
                db.SaveSchema(schemaStr);
            }
        }
        #endregion
    }
}
