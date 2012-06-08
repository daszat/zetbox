
namespace Zetbox.Server.Tests.SchemaTests.SchemaProviders.TableStructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Server.SchemaManagement.NpgsqlProvider;

    public sealed class Npgsql
        : Fixture<Postgresql>
    {
        protected override Postgresql GetProvider()
        {
            var result = new Postgresql();
            result.Open(SchemaProviderFixture<Postgresql>.PostgresqlTestConnection);
            result.BeginTransaction();
            return result;
        }

        protected override string GetDatabaseName()
        {
            return SchemaProviderFixture<Postgresql>.PostgresqlTestDatabase;
        }
    }
}
