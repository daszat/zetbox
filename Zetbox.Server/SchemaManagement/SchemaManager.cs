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

namespace Zetbox.Server.SchemaManagement
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.API.SchemaManagement;
    using Zetbox.API.Server;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.Packaging;
    using Zetbox.Generator;
    using Zetbox.Generator.Extensions;

    public partial class SchemaManager
        : IDisposable
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger(typeof(SchemaManager));

        #region Fields
        private IZetboxContext schema;
        private ISchemaProvider db;
        private bool repair = false;
        private readonly Cases Case;
        private readonly ZetboxConfig config;
        private readonly IEnumerable<IGlobalMigrationFragment> _globalMigrationFragments;
        #endregion

        #region Constructor

        public SchemaManager(ISchemaProvider provider, IZetboxContext schema, IZetboxContext savedSchema, ZetboxConfig config, IEnumerable<IGlobalMigrationFragment> globalMigrationFragments, IEnumerable<IMigrationFragment> migrationFragments)
        {
            this.config = config;
            this.schema = schema;
            this.db = provider;
            this._globalMigrationFragments = globalMigrationFragments;
            this.Case = new Cases(schema, provider, savedSchema, migrationFragments);
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
            var headerLog = log4net.LogManager.GetLogger("Zetbox", "Zetbox.Server.Schema.Report.Header");
            var connectionString = config.Server.GetConnectionString(Helper.ZetboxConnectionStringKey);

            headerLog.WarnFormat("== {0} ==", reportName);
            headerLog.WarnFormat("Date: {0}", DateTime.Now);
            headerLog.WarnFormat("Database: {0}", db.GetSafeConnectionString(connectionString.ConnectionString));
            headerLog.Warn(String.Empty);
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

        /// <summary>
        /// Creates a list of joins linking objects according to the specified relations list.
        /// </summary>
        /// <param name="db">the underlying schema provider</param>
        /// <param name="objClass">the starting point</param>
        /// <param name="relations">the relations to follow</param>
        /// <returns>a list of joins</returns>
        public static IList<Join> CreateJoinList(ISchemaProvider db, ObjectClass objClass, IEnumerable<Relation> relations)
        {
            if (db == null) throw new ArgumentNullException("db");
            if (objClass == null) throw new ArgumentNullException("objClass");
            if (relations == null) throw new ArgumentNullException("relations");

            List<Join> result = new List<Join>();
            Join lastJoin = ColumnRef.PrimaryTable;
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
                    viewRel.JoinTableName = db.GetTableName(rel.Module.SchemaName, rel.GetRelationTableName());
                    viewRel.JoinColumnName = new[] { new ColumnRef(Construct.ForeignKeyColumnName(lastRelEnd), ColumnRef.Local) };
                    viewRel.FKColumnName = new[] { new ColumnRef("ID", lastJoin) };
                    lastJoin = viewRel;

                    viewRel = new Join();
                    result.Add(viewRel);
                    viewRel.JoinTableName = nextRelEnd.Type.GetTableRef(db);
                    viewRel.JoinColumnName = new[] { new ColumnRef("ID", ColumnRef.Local) };
                    viewRel.FKColumnName = new[] { new ColumnRef(Construct.ForeignKeyColumnName(nextRelEnd), lastJoin) };

                    lastJoin = viewRel;
                }
                else
                {
                    var viewRel = new Join();
                    result.Add(viewRel);
                    viewRel.JoinTableName = nextRelEnd.Type.GetTableRef(db);
                    string localCol = string.Empty;
                    string fkCol = string.Empty;
                    if (nextRelEnd == rel.A && rel.Storage == StorageType.MergeIntoA)
                    {
                        localCol = Construct.ForeignKeyColumnName(lastRelEnd);
                        fkCol = "ID";
                    }
                    else if (nextRelEnd == rel.A && rel.Storage == StorageType.MergeIntoB)
                    {
                        localCol = "ID";
                        fkCol = Construct.ForeignKeyColumnName(nextRelEnd);
                    }
                    else if (nextRelEnd == rel.B && rel.Storage == StorageType.MergeIntoA)
                    {
                        localCol = "ID";
                        fkCol = Construct.ForeignKeyColumnName(nextRelEnd);
                    }
                    else if (nextRelEnd == rel.B && rel.Storage == StorageType.MergeIntoB)
                    {
                        localCol = Construct.ForeignKeyColumnName(lastRelEnd);
                        fkCol = "ID";
                    }
                    else
                    {
                        throw new NotSupportedException(string.Format("StorageType {0} is not supported", rel.Storage));
                    }
                    viewRel.JoinColumnName = new[] { new ColumnRef(localCol, ColumnRef.Local) };
                    viewRel.FKColumnName = new[] { new ColumnRef(fkCol, lastJoin) };
                    lastJoin = viewRel;
                }

                lastType = nextRelEnd.Type;
            }
            return result;
        }
        #endregion

        #region Helper
        public static DefaultConstraint GetDefaultConstraint(Property prop)
        {
            if (prop == null) throw new ArgumentNullException("prop");
            var objClass = prop.ObjectClass as ObjectClass;

            // Only TPT or TPH base columns can have default constraints
            // And only member of object classes - no CP objects!
            var defaultPossibleValue = objClass != null && (objClass.GetTableMapping() == TableMapping.TPT || objClass.BaseObjectClass == null);
            if (!defaultPossibleValue) return null;

            var defValue = prop.DefaultValue;
            if (defValue is Zetbox.App.Base.NewGuidDefaultValue)
            {
                return new NewGuidDefaultConstraint();
            }
            else if (defValue is Zetbox.App.Base.CurrentDateTimeDefaultValue)
            {
                var dtProp = (DateTimeProperty)prop;
                return new DateTimeDefaultConstraint() { Precision = dtProp.DateTimeStyle == DateTimeStyles.Date ? DateTimeDefaultConstraintPrecision.Date : DateTimeDefaultConstraintPrecision.Time };
            }
            else if (defValue is Zetbox.App.Base.BoolDefaultValue)
            {
                return new BoolDefaultConstraint() { Value = ((Zetbox.App.Base.BoolDefaultValue)defValue).BoolValue };
            }
            else if (defValue is Zetbox.App.Base.IntDefaultValue)
            {
                return new IntDefaultConstraint() { Value = ((Zetbox.App.Base.IntDefaultValue)defValue).IntValue };
            }
            else if (defValue is Zetbox.App.Base.DecimalDefaultValue)
            {
                return new DecimalDefaultConstraint() { Value = ((Zetbox.App.Base.DecimalDefaultValue)defValue).DecimalValue };
            }
            else if (defValue is Zetbox.App.Base.EnumDefaultValue)
            {
                return new IntDefaultConstraint() { Value = ((Zetbox.App.Base.EnumDefaultValue)defValue).EnumValue.Value };
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region SavedSchema

        public static void LoadSavedSchemaInto(ISchemaProvider provider, IZetboxContext targetCtx)
        {
            if (provider == null) { throw new ArgumentNullException("provider"); }
            if (targetCtx == null) { throw new ArgumentNullException("targetCtx"); }

            string schema = provider.GetSavedSchema().TrimEnd((char)0); // Trim possible C++/Database/whatever ending 0 char
            if (!string.IsNullOrEmpty(schema))
            {
                // Migration from Kist -> Zetbox
                schema = schema.Replace("Kistl", "Zetbox");

                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(schema)))
                {
                    Importer.LoadFromXml(targetCtx, ms, "saved schema from " + provider.GetSafeConnectionString());
                }
            }
        }

        private void SaveSchema(IZetboxContext schema)
        {
            using (Logging.Log.DebugTraceMethodCall("SaveSchema"))
            using (var ms = new MemoryStream())
            {
                Exporter.PublishFromContext(schema, ms, Exporter.Filter.Schema, new string[] { "*" }, "in-memory buffer for SaveSchema");
                string schemaStr = Encoding.UTF8.GetString(ms.GetBuffer()).TrimEnd((char)0); // Trim possible C++/Database/whatever ending 0 char
                db.SaveSchema(schemaStr);
            }
        }
        #endregion
    }
}
