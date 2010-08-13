
namespace Kistl.Server.SchemaTests.SchemaProviders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using Kistl.Server.SchemaManagement.SqlProvider;

    public class MssqlMetadataTests
        : SchemaProviderFixture<SqlServer>
    {
        protected override SqlServer GetProvider()
        {
            return new SqlServer();
        }

        [Test]
        public void has_correct_metadata()
        {
            Assert.That(Provider.AdoNetProvider, Is.EqualTo("System.Data.SqlClient"));
            Assert.That(Provider.ConfigName, Is.EqualTo("MSSQL"));
            Assert.That(Provider.ManifestToken, Is.EqualTo("2008"));
            Assert.That(Provider.IsStorageProvider, Is.True);
        }
    }
}
