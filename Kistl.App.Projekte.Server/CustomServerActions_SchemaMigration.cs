using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API.Server;
using Kistl.App.Extensions;
using Kistl.API.Utils;
using Kistl.API;

namespace ZBox.App.SchemaMigraion
{
    public class CustomServerActions_SchemaMigration
    {
        private static SchemaProviderFactory sqlFactory;
        private static SchemaProviderFactory postgresFactory;
        private static SchemaProviderFactory oledbFactory;

        public CustomServerActions_SchemaMigration(SchemaProviderFactory sql, SchemaProviderFactory postgres, SchemaProviderFactory oledb)
        {
            sqlFactory = sql;
            postgresFactory = postgres;
            oledbFactory = oledb;
        }

        public static void OnUpdateFromSourceSchema_MigrationProject(ZBox.App.SchemaMigration.MigrationProject obj)
        {

        }
    }
}
