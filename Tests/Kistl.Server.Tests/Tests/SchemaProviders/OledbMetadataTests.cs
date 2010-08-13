
namespace Kistl.Server.SchemaTests.SchemaProviders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using Kistl.Server.SchemaManagement.OleDbProvider;

    public class OledbMetadataTests
        : SchemaProviderFixture<OleDb>
    {
        protected override OleDb GetProvider()
        {
            return new OleDb();
        }

        [Test]
        public void has_correct_metadata()
        {
            Assert.That(Provider.AdoNetProvider, Is.Null);
            Assert.That(Provider.ConfigName, Is.EqualTo("OLEDB"));
            Assert.That(Provider.ManifestToken, Is.Null);
            Assert.That(Provider.IsStorageProvider, Is.False);
        }
    }
}
