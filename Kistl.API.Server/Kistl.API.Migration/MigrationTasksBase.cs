
namespace Kistl.API.Migration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API.Server;
    using ZBox.App.SchemaMigration;
    using Kistl.App.Base;

    public class MigrationTasksBase : IMigrationTasks
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.API.Migration");

        private readonly ISchemaProvider _src;
        private readonly ISchemaProvider _dst;
        public MigrationTasksBase(ISchemaProvider src, ISchemaProvider dst)
        {
            if (src == null) throw new ArgumentNullException("src");
            if (dst == null) throw new ArgumentNullException("dst");

            _src = src;
            _dst = dst;
        }

        public InputStream ExecuteQueryStreaming(string sql)
        {
            return new InputStream(_src.ReadTableData(sql));
        }

        public OutputStream WriteTableStreaming(string destTable)
        {
            return new OutputStream(_dst.GetQualifiedTableName(destTable), _dst);
        }

        public void CleanDestination(SourceTable tbl)
        {
            if (tbl == null) throw new ArgumentNullException("tbl");
            if (tbl.DestinationObjectClass == null)
            {
                Log.InfoFormat("Skipping cleaning of unmapped table [{0}]", tbl.Name);
                return;
            }
            CleanDestination(_dst.GetQualifiedTableName(tbl.DestinationObjectClass.TableName));
        }

        public void CleanDestination(TableRef tbl)
        {
            _dst.TruncateTable(tbl);
        }

        private static string GetColName(Kistl.App.Base.Property prop)
        {
            if (prop is Kistl.App.Base.ObjectReferenceProperty)
            {
                var orp = (Kistl.App.Base.ObjectReferenceProperty)prop;
                return "fk_" + orp.RelationEnd.Parent.GetOtherEnd(orp.RelationEnd).RoleName;
            }
            else
            {
                return prop.Name;
            }
        }

        public void TableBaseMigration(SourceTable tbl, NullConverter[] nullConverter, Join[] additional_joins)
        {
            if (tbl == null) throw new ArgumentNullException("tbl");

            if (tbl.DestinationObjectClass == null)
            {
                Log.InfoFormat("Skipping base migration of unmapped table [{0}]", tbl.Name);
                return;
            }

            Log.InfoFormat("Migrating {0} to {1}", tbl.Name, tbl.DestinationObjectClass.Name);

            var srcColumns = tbl.SourceColumn
                .Where(c => c.DestinationProperty != null)
                .OrderBy(c => c.Name)
                .ToList();
            var dstColumnNames = srcColumns.Select(c => GetColName(c.DestinationProperty)).ToList();

            // Error Col
            if (typeof(IMigrationInfo).IsAssignableFrom(tbl.DestinationObjectClass.GetDataType()))
            {
                dstColumnNames.Add("MigrationErrors");
            }

            var referringCols = srcColumns.Where(c => c.References != null).ToList();
            if (referringCols.Count == 0 && (additional_joins == null || additional_joins.Length == 0))
            {
                var srcColumnNames = srcColumns.Select(c => c.Name).ToArray();
                // no fk mapping required
                using (var srcReader = _src.ReadTableData(_src.GetQualifiedTableName(tbl.Name), srcColumnNames))
                using (var translator = new Translator(tbl, srcReader, srcColumns, nullConverter))
                {
                    _dst.WriteTableData(_dst.GetQualifiedTableName(tbl.DestinationObjectClass.TableName), translator, dstColumnNames);
                }
            }
            else
            {
                // could automatically create needed indices
                var ref_joins = referringCols.Select(reference =>
                {
                    var referencedTable = _dst.GetQualifiedTableName(reference.References.SourceTable.DestinationObjectClass.TableName);
                    return new Join()
                    {
                        JoinTableName = referencedTable,
                        Type = JoinType.Left,
                        JoinColumnName = reference.References.DestinationProperty.Name,
                        FKColumnName = reference.Name
                    };
                });
                // Add manual joins
                IEnumerable<Join> joins;
                if (additional_joins != null)
                {
                    joins = ref_joins.Union(additional_joins);
                }
                else
                {
                    joins = ref_joins;
                }

                var srcColumnNames = srcColumns.Select(c => {
                    var orp = c.DestinationProperty as ObjectReferenceProperty;
                    if (c.References != null)
                    {
                        return new ProjectionColumn()
                        {
                            ColumnName = "ID",
                            Alias = c.DestinationProperty.Name,
                            TableName = _dst.GetQualifiedTableName(c.References.SourceTable.DestinationObjectClass.TableName)
                        };
                    }
                    else if (c.References == null
                        && c.DestinationProperty is ObjectReferenceProperty
                        && additional_joins != null
                        && additional_joins.Count(i => i.JoinTableName == _dst.GetQualifiedTableName(orp.RelationEnd.Parent.GetOtherEnd(orp.RelationEnd).Type.TableName)) > 0)
                    {
                        return new ProjectionColumn()
                        {
                            ColumnName = "ID",
                            Alias = c.DestinationProperty.Name,
                            TableName = _dst.GetQualifiedTableName(orp.RelationEnd.Parent.GetOtherEnd(orp.RelationEnd).Type.TableName)
                        };
                    }
                    else
                    {
                        return new ProjectionColumn()
                        {
                            ColumnName = c.Name,
                            TableName = _src.GetQualifiedTableName(tbl.Name)
                        };
                    }
                }).ToArray();

                using (var srcReader = _src.ReadJoin(_src.GetQualifiedTableName(tbl.Name), srcColumnNames, joins))
                using (var translator = new Translator(tbl, srcReader, srcColumns, nullConverter))
                {
                    _dst.WriteTableData(_dst.GetQualifiedTableName(tbl.DestinationObjectClass.TableName), translator, dstColumnNames);
                }
            }
        }
    }
}
