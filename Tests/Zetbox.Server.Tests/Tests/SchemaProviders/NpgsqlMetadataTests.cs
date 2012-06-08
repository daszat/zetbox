
namespace Zetbox.Server.Tests.SchemaTests.SchemaProviders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using Zetbox.Server.SchemaManagement.NpgsqlProvider;

    public class NpgsqlMetadataTests
        : SchemaProviderFixture<Postgresql>
    {
        protected override Postgresql GetProvider()
        {
            return new Postgresql();
        }

        [Test]
        public void has_correct_metadata()
        {
            Assert.That(Provider.AdoNetProvider, Is.EqualTo("Npgsql"));
            Assert.That(Provider.ConfigName, Is.EqualTo("POSTGRESQL"));
            Assert.That(Provider.ManifestToken, Is.EqualTo("8.1.3"));
            Assert.That(Provider.IsStorageProvider, Is.True);
        }
    }
}
