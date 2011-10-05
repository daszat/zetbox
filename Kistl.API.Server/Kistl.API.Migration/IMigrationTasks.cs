
namespace Kistl.API.Migration
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using Kistl.API.Server;
    using ZBox.App.SchemaMigration;

    public delegate IMigrationTasks TaskFactory(ISchemaProvider src, ISchemaProvider dst);

    public abstract class Converter
    {
        public SourceColumn Column { get; set; }
        public string ErrorMsg { get; set; }
        public abstract object Convert(IDataReader source);

        public Converter()
        {
        }

        public Converter(SourceColumn column, string errorMsg)
        {
            this.Column = column;
            this.ErrorMsg = errorMsg;
        }
    }

    public abstract class SimpleFieldConverter : Converter
    {
        public SimpleFieldConverter()
        {
        }

        public SimpleFieldConverter(SourceColumn column, string errorMsg)
            : base(column, errorMsg)
        {
        }

        protected int position = -1;
        public override object Convert(IDataReader source)
        {
            if (position < 0)
            {
                position = source.GetOrdinal(this.Column.Name);
            }
            return ConvertValue(source.GetValue(position));
        }
        protected abstract object ConvertValue(object value);
    }

    public class NullConverter : SimpleFieldConverter
    {
        public NullConverter()
        {
        }

        public NullConverter(SourceColumn column, object nullValue, string errorMsg)
            : base(column, errorMsg)
        {
            this.NullValue = nullValue;
        }

        public object NullValue { get; set; }

        protected override object ConvertValue(object value)
        {
            return (value == null || value == DBNull.Value)
                ? NullValue
                : value;
        }
    }

    public class LambdaFieldConverter : SimpleFieldConverter
    {
        public Func<object, object> Converter { get; set; }

        public LambdaFieldConverter(SourceColumn column, Func<object, object> converter)
            : this(column, converter, string.Empty)
        {
        }

        public LambdaFieldConverter(SourceColumn column, Func<object, object> converter, string errorMsg)
            : base(column, errorMsg)
        {
            this.Converter = converter;
        }

        protected override object ConvertValue(object value)
        {
            return Converter(value);
        }
    }

    public class LambdaConverter : Converter
    {
        public Func<IDataReader, object> Converter { get; set; }

        public LambdaConverter ()
        {
        }

        public LambdaConverter (SourceColumn column, Func<IDataReader, object> converter)
            : this(column, converter, string.Empty)
        {
        }

        public LambdaConverter(SourceColumn column, Func<IDataReader, object> converter, string errorMsg)
            : base(column, errorMsg)
        {
            this.Converter = converter;
        }

        public override object Convert(IDataReader source)
        {
            return Converter(source);
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
