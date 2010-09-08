
namespace Kistl.API.Migration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API.Server;
    using ZBox.App.SchemaMigration;
    using System.Data;

    public delegate IMigrationTasks TaskFactory(ISchemaProvider src, ISchemaProvider dst);

    public abstract class Converter
    {
        public SourceColumn Column { get; set; }
        public string ErrorMsg { get; set; }
    }

    public class NullConverter : Converter
    {
        public NullConverter()
        {
        }

        public NullConverter(SourceColumn column, object nullValue, string errorMsg)
        {
            this.Column = column;
            this.NullValue = nullValue;
            this.ErrorMsg = errorMsg;
        }

        public object NullValue { get; set; }
    }

    public class FieldConverter : Converter
    {
        public FieldConverter()
        {
        }

        public FieldConverter(SourceColumn column, Func<IDataReader, object> converter)
        {
            this.Column = column;
            this.Converter = converter;
        }

        public FieldConverter(SourceColumn column, Func<IDataReader, object> converter, string errorMsg)
        {
            this.Column = column;
            this.Converter = converter;
            this.ErrorMsg = errorMsg;
        }

        public Func<IDataReader, object> Converter { get; set; }
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
        OutputStream WriteTableStreaming(string destTable);
    }
}
