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

namespace Zetbox.Server.Tests.SchemaTests.SchemaProviders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using Zetbox.Server.SchemaManagement.OleDbProvider;

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
