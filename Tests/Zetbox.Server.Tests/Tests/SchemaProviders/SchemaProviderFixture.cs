
namespace Zetbox.Server.Tests.SchemaTests.SchemaProviders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API.Server;
    using NUnit.Framework;

    public abstract class SchemaProviderFixture<TProvider>
        where TProvider : class, ISchemaProvider
    {
        // TODO: move to config file
        public static readonly string PostgresqlTestDatabase = "zetbox_test";
        public static readonly string PostgresqlTestConnection = "Server=localhost;Database=zetbox_test;User Id=zetbox;Password=b-cXqMyXEYea2kkjUhkS";
        public static readonly string SqlServerTestDatabase = "zetbox_test";
        public static readonly string SqlServerTestConnection = "Data Source=.\\SQLEXPRESS;Initial Catalog=zetbox_test;Integrated Security=True;MultipleActiveResultSets=true;";

        protected TProvider Provider;

        protected abstract TProvider GetProvider();

        [SetUp]
        public void SetUp()
        {
            Provider = GetProvider();
        }

        [TearDown]
        public void TearDown()
        {
            if (Provider != null)
            {
                Provider.Dispose();
                Provider = null;
            }
        }
    }
}
