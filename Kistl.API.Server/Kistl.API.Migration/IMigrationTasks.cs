
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

    public class NullConverter
    {
        public SourceColumn Column { get; set; }
        public object NullValue { get; set; }
        public string ErrorMsg { get; set; }
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
        void TableBaseMigration(SourceTable tbl, NullConverter[] nullConverter);
        void TableBaseMigration(SourceTable tbl, NullConverter[] nullConverter, Join[] additional_joins);

        InputStream ExecuteQueryStreaming(string sql);

        OutputStream WriteTableStreaming(string destTable);
    }
}
