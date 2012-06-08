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

namespace Zetbox.API.Migration
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using Zetbox.API.Server;
    using Zetbox.App.SchemaMigration;

    public delegate IMigrationTasks TaskFactory(ISchemaProvider src, ISchemaProvider dst);

    public abstract class Converter
    {
        private readonly SourceColumn _column;
        private readonly string _errorMsg;

        public SourceColumn Column { get { return _column; } }
        public string ErrorMsg { get { return _errorMsg; } }

        public abstract object Convert(IDataReader source, int fieldIndex);

        public Converter(SourceColumn column, string errorMsg)
        {
            this._column = column;
            this._errorMsg = errorMsg;
        }
    }

    public abstract class SimpleFieldConverter : Converter
    {
        public SimpleFieldConverter(SourceColumn column, string errorMsg)
            : base(column, errorMsg)
        {
        }

        public override object Convert(IDataReader source, int fieldIndex)
        {
            if (source == null) throw new ArgumentNullException("source");
            return ConvertValue(source.GetValue(fieldIndex));
        }

        public abstract object ConvertValue(object value);
    }

    public sealed class NullConverter : SimpleFieldConverter
    {
        private readonly object _nullValue;
        public object NullValue { get { return _nullValue; } }

        public NullConverter(SourceColumn column, object nullValue, string errorMsg)
            : base(column, errorMsg)
        {
            this._nullValue = nullValue;
        }

        public override object ConvertValue(object value)
        {
            return (value == null || value == DBNull.Value)
                ? NullValue
                : value;
        }
    }

    public sealed class LambdaFieldConverter : SimpleFieldConverter
    {
        private readonly Func<object, object> _converter;
        public Func<object, object> Converter { get { return _converter; } }

        public LambdaFieldConverter(SourceColumn column, Func<object, object> converter)
            : this(column, converter, string.Empty)
        {
        }

        public LambdaFieldConverter(SourceColumn column, Func<object, object> converter, string errorMsg)
            : base(column, errorMsg)
        {
            if (converter == null) throw new ArgumentNullException("converter");
            this._converter = converter;
        }

        public override object ConvertValue(object value)
        {
            return Converter(value);
        }
    }

    public sealed class LambdaConverter : Converter
    {
        private readonly Func<IDataReader, int, object> _converter;
        public Func<IDataReader, int, object> Converter { get { return _converter; } }

        public LambdaConverter(SourceColumn column, Func<IDataReader, int, object> converter)
            : this(column, converter, string.Empty)
        {
        }

        public LambdaConverter(SourceColumn column, Func<IDataReader, int, object> converter, string errorMsg)
            : base(column, errorMsg)
        {
            if (converter == null) throw new ArgumentNullException("converter");
            this._converter = converter;
        }

        public override object Convert(IDataReader source, int fieldIndex)
        {
            return Converter(source, fieldIndex);
        }
    }

    public interface IMigrationTasks
    {
        /// <summary>
        /// Removes all existing data in the destination.
        /// </summary>
        void CleanDestination(SourceTable tbl);
        /// <summary>
        /// Removes all existing data in the destination.
        /// </summary>
        void CleanDestination(TableRef tbl);

        /// <summary>
        /// Executes the basic defined migrations from the specified source table.
        /// </summary>
        void TableBaseMigration(SourceTable tbl);
        void TableBaseMigration(SourceTable tbl, params Converter[] nullConverter);
        void TableBaseMigration(SourceTable tbl, Converter[] nullConverter, Join[] additional_joins);

        InputStream ExecuteQueryStreaming(string sql);
        OutputStream WriteTableStreaming(TableRef destTable);
    }
}
