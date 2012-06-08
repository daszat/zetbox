
namespace Zetbox.Server.Tests.SchemaTests.SchemaProviders.TableStructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Server.SchemaManagement.SqlProvider;

    public sealed class Mssql
        : Fixture<SqlServer>
    {
        protected override SqlServer GetProvider()
        {
            var result = new SqlServer();
            result.Open(SchemaProviderFixture<SqlServer>.SqlServerTestConnection);
            result.BeginTransaction();
            return result;
        }

        protected override string GetDatabaseName()
        {
            return SchemaProviderFixture<SqlServer>.SqlServerTestDatabase;
        }
    }
}
