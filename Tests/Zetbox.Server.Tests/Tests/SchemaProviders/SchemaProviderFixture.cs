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
