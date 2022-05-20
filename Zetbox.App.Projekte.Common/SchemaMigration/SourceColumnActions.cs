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
namespace Zetbox.App.SchemaMigration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;

    [Implementor]
    public static class SourceColumnActions
    {
        [Invocation]
        public static System.Threading.Tasks.Task ToString(Zetbox.App.SchemaMigration.SourceColumn obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = (obj.SourceTable != null ? obj.SourceTable.Name : null) ?? string.Empty;
            e.Result += "." + (!string.IsNullOrEmpty(obj.Name) ? obj.Name : "new Source Column");

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task CreateProperty(Zetbox.App.SchemaMigration.SourceColumn obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            var srcTbl = obj.SourceTable;
            if (srcTbl == null) throw new InvalidOperationException("Not attached to a source table");
            if (srcTbl.DestinationObjectClass == null) throw new InvalidOperationException("Source table has no destination object class");
            if (srcTbl.StagingDatabase == null) throw new InvalidOperationException("Not attached to a staging database");
            if (srcTbl.StagingDatabase.MigrationProject == null) throw new InvalidOperationException("Not attached to a migration project");
            if (srcTbl.StagingDatabase.MigrationProject.DestinationModule == null) throw new InvalidOperationException("No destination module provided");
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
            p.ObjectClass = srcTbl.DestinationObjectClass;
            p.Module = srcTbl.StagingDatabase.MigrationProject.DestinationModule;

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
