
namespace Kistl.Server.SchemaTests.SchemaProviders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API.Server;
    using NUnit.Framework;

    public abstract class SchemaProviderFixture<TProvider>
        where TProvider : class, ISchemaProvider
    {
        // TODO: move to config file
        public static readonly string PostgresqlTestDatabase = "zbox_test";
        public static readonly string PostgresqlTestConnection = "Server=localhost;Database=zbox_test;User Id=zbox;Password=b-cXqMyXEYea2kkjUhkS";
        public static readonly string SqlServerTestDatabase = "zbox_test";
        public static readonly string SqlServerTestConnection = "Data Source=.\\SQLEXPRESS;Initial Catalog=zbox_test;Integrated Security=True;MultipleActiveResultSets=true;";

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
