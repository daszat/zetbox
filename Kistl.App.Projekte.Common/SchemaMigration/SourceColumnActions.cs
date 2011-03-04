namespace ZBox.App.SchemaMigration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;

    [Implementor]
    public static class SourceColumnActions
    {
        [Invocation]
        public static void ToString(ZBox.App.SchemaMigration.SourceColumn obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = (obj.SourceTable != null ? obj.SourceTable.Name : null) ?? string.Empty;
            e.Result += "." + (!string.IsNullOrEmpty(obj.Name) ? obj.Name : "new Source Column");
        }

        [Invocation]
        public static void CreateProperty(ZBox.App.SchemaMigration.SourceColumn obj)
        {
            if (obj.SourceTable == null) throw new InvalidOperationException("Not attached to a source table");
            if (obj.SourceTable.DestinationObjectClass == null) throw new InvalidOperationException("Source table has no destination object class");
            if (obj.SourceTable.StagingDatabase == null) throw new InvalidOperationException("Not attached to a staging database");
            if (obj.SourceTable.StagingDatabase.MigrationProject == null) throw new InvalidOperationException("Not attached to a migration project");
            if (obj.SourceTable.StagingDatabase.MigrationProject.DestinationModule == null) throw new InvalidOperationException("No destination module provided");
            if (obj.DestinationProperty.Count > 0) throw new InvalidOperationException("there is already a destination object property");

            Property p = null;
            var ctx = obj.Context;

            switch (obj.DbType)
            {
                case ColumnType.AnsiString:
                case ColumnType.AnsiStringFixedLength:
                case ColumnType.String:
                case ColumnType.StringFixedLength:
                    p = ctx.Create<StringProperty>();
                    var c = ctx.Create<StringRangeConstraint>();
                    c.MaxLength = obj.Size;
                    p.Constraints.Add(c);
                    break;


                case ColumnType.Boolean:
                    p = ctx.Create<BoolProperty>();
                    break;

                case ColumnType.Date:
                case ColumnType.DateTime:
                case ColumnType.DateTime2:
                    p = ctx.Create<DateTimeProperty>();
                    break;

                case ColumnType.Single:
                case ColumnType.Double:
                    p = ctx.Create<DoubleProperty>();
                    break;

                case ColumnType.Byte:
                case ColumnType.Int16:
                case ColumnType.Int32:
                    p = ctx.Create<IntProperty>();
                    break;

                case ColumnType.Guid:
                    p = ctx.Create<GuidProperty>();
                    break;
                case ColumnType.Currency:
                case ColumnType.Decimal:
                case ColumnType.VarNumeric:
                    p = ctx.Create<DecimalProperty>();
                    ((DecimalProperty)p).Precision = (int)obj.Size;
                    ((DecimalProperty)p).Scale = 2;
                    break;

                case ColumnType.Binary:
                case ColumnType.DateTimeOffset:
                case ColumnType.Int64:
                case ColumnType.Object:
                case ColumnType.SByte:
                case ColumnType.Time:
                case ColumnType.UInt16:
                case ColumnType.UInt32:
                case ColumnType.UInt64:
                case ColumnType.Xml:
                default:
                    throw new NotSupportedException("Unknown DbType " + obj.DbType.ToString());
            }

            if (obj.IsNullable == false)
            {
                p.Constraints.Add(ctx.Create<NotNullableConstraint>());
            }

            obj.DestinationProperty.Add(p);
            p.Name = obj.Name;
            p.Description = obj.Description;
            p.ObjectClass = obj.SourceTable.DestinationObjectClass;
            p.Module = obj.SourceTable.StagingDatabase.MigrationProject.DestinationModule;
        }
    }
}
