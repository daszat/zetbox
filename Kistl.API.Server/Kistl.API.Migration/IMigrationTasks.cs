
namespace Kistl.API.Migration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API.Server;
    using ZBox.App.SchemaMigration;

    public delegate IMigrationTasks TaskFactory(ISchemaProvider src, ISchemaProvider dst);

    public interface IMigrationTasks
    {
        /// <summary>
        /// Removes all existing data in the destination.
        /// </summary>
        void CleanDestination(SourceTable tbl);

        /// <summary>
        /// Executes the basic defined migrations from the specified source table.
        /// </summary>
        void TableBaseMigration(SourceTable tbl);
    }
}
