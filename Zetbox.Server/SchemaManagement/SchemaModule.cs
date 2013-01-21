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

namespace Zetbox.Server.SchemaManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Common;
    using Zetbox.API.Configuration;
    using Zetbox.API.SchemaManagement;
    using Zetbox.API.Server;

    // No feature, loaded by server module
    public class SchemaModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register((c, p) =>
                {
                    ZetboxConfig cfg = c.Resolve<ZetboxConfig>();
                    IZetboxContext ctx = c.Resolve<BaseMemoryContext>();
                    var connectionString = cfg.Server.GetConnectionString(Zetbox.API.Helper.ZetboxConnectionStringKey);
                    ISchemaProvider schemaProvider = c.ResolveNamed<ISchemaProvider>(connectionString.SchemaProvider);
                    schemaProvider.Open(connectionString.ConnectionString);
                    SchemaManagement.SchemaManager.LoadSavedSchemaInto(schemaProvider, ctx);

                    var globalMigrationFragments = c.Resolve<IEnumerable<IGlobalMigrationFragment>>();
                    var migrationFragments = c.Resolve<IEnumerable<IMigrationFragment>>();

                    return new SchemaManagement.SchemaManager(
                        schemaProvider,
                        p.Named<IZetboxContext>("newSchema"),
                        ctx,
                        cfg,
                        globalMigrationFragments,
                        migrationFragments);
                })
                .InstancePerDependency();

            builder
                .Register(c => new SchemaManagement.LoggingSchemaProviderAdapter(new SchemaManagement.OleDbProvider.OleDb()))
                .As<ISchemaProvider>()
                .Named<ISchemaProvider>("OLEDB")
                .InstancePerDependency();
            builder
                .Register(c => new SchemaManagement.LoggingSchemaProviderAdapter(new SchemaManagement.SqlProvider.SqlServer()))
                .As<ISchemaProvider>()
                .Named<ISchemaProvider>("MSSQL")
                .InstancePerDependency();
            builder
                .Register(c => new SchemaManagement.LoggingSchemaProviderAdapter(new SchemaManagement.NpgsqlProvider.Postgresql()))
                .As<ISchemaProvider>()
                .Named<ISchemaProvider>("POSTGRESQL")
                .InstancePerDependency();
        }
    }
}
