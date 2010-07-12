using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using Kistl.API;
using Kistl.App.Base;

namespace ZBox.App.SchemaMigration
{
    public static class CustomCommonActions_SchemaMigration
    {
        public static void OnToString_MigrationProject(ZBox.App.SchemaMigration.MigrationProject obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = !string.IsNullOrEmpty(obj.Description) ? obj.Description : "new Migration Project";
        }

        public static void OnToString_SourceColumn(ZBox.App.SchemaMigration.SourceColumn obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = (obj.SourceTable != null ? obj.SourceTable.Name : null) ?? string.Empty;
            e.Result += !string.IsNullOrEmpty(obj.Name) ? obj.Name : "new Source Column";
        }

        public static void OnToString_SourceTable(ZBox.App.SchemaMigration.SourceTable obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = !string.IsNullOrEmpty(obj.Name) ? obj.Name : "new Source Table";
        }

        public static void OnCreateObjectClass_SourceTable(ZBox.App.SchemaMigration.SourceTable obj)
        {
            if (obj.StagingDatabase == null) throw new InvalidOperationException("Not attached to a staging database");
            if (obj.StagingDatabase.MigrationProject == null) throw new InvalidOperationException("Not attached to a migration project");
            if (obj.StagingDatabase.MigrationProject.DestinationModule == null) throw new InvalidOperationException("No destination module provided");
            if (obj.DestinationObjectClass != null) throw new InvalidOperationException("there is already a destination object class");

            obj.DestinationObjectClass = obj.Context.Create<ObjectClass>();
            obj.DestinationObjectClass.Name = obj.Name;
            obj.DestinationObjectClass.Module = obj.StagingDatabase.MigrationProject.DestinationModule;
            obj.DestinationObjectClass.Description = obj.Description;
        }

        public static void OnCreateProperty_SourceColumn(ZBox.App.SchemaMigration.SourceColumn obj)
        {
            if (obj.SourceTable == null) throw new InvalidOperationException("Not attached to a source table");
            if (obj.SourceTable.DestinationObjectClass == null) throw new InvalidOperationException("Source table has no destination object class");
            if (obj.SourceTable.StagingDatabase == null) throw new InvalidOperationException("Not attached to a staging database");
            if (obj.SourceTable.StagingDatabase.MigrationProject == null) throw new InvalidOperationException("Not attached to a migration project");
            if (obj.SourceTable.StagingDatabase.MigrationProject.DestinationModule == null) throw new InvalidOperationException("No destination module provided");
            if (obj.DestinationProperty != null) throw new InvalidOperationException("there is already a destination object property");

            Property p = null;

            switch(obj.DbType)
            {
                case ColumnType.AnsiString:
                case ColumnType.AnsiStringFixedLength:
                case ColumnType.String:
                case ColumnType.StringFixedLength:
                    p = obj.Context.Create<StringProperty>();
                    var c = obj.Context.Create<StringRangeConstraint>();
                    c.MaxLength = obj.Size;
                    p.Constraints.Add(c);
                    break;


                case ColumnType.Boolean:
                    p = obj.Context.Create<BoolProperty>();
                    break;

                case ColumnType.Date:
                case ColumnType.DateTime:
                case ColumnType.DateTime2:
                    p = obj.Context.Create<DateTimeProperty>();
                    break;

                case ColumnType.Single:
                case ColumnType.Double:
                    p = obj.Context.Create<DoubleProperty>();
                    break;
                
                case ColumnType.Byte:
                case ColumnType.Int16:
                case ColumnType.Int32:
                    p = obj.Context.Create<IntProperty>();
                    break;

                case ColumnType.Guid:
                    p = obj.Context.Create<GuidProperty>();
                    break;

                case ColumnType.Binary:
                case ColumnType.Currency:
                case ColumnType.DateTimeOffset:
                case ColumnType.Decimal:
                case ColumnType.Int64:
                case ColumnType.Object:
                case ColumnType.SByte:
                case ColumnType.Time:
                case ColumnType.UInt16:
                case ColumnType.UInt32:
                case ColumnType.UInt64:
                case ColumnType.VarNumeric:
                case ColumnType.Xml:
                default:
                    throw new NotSupportedException("Unknown DbType " + obj.DbType);
            }

            if (obj.IsNullable == false)
            {
                p.Constraints.Add(obj.Context.Create<NotNullableConstraint>());
            }

            obj.DestinationProperty = p;
            p.Name = obj.Name;
            p.Description = obj.Description;
            p.ObjectClass = obj.SourceTable.DestinationObjectClass;
            p.Module = obj.SourceTable.StagingDatabase.MigrationProject.DestinationModule;
        }

    }
}
