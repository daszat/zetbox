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

namespace Zetbox.Server.Tests.SchemaTests.SchemaProviders.TableStructure
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using Zetbox.API.Server;
    using NUnit.Framework;

    public abstract class Fixture<TProvider>
        : SchemaProviderFixture<TProvider>
        where TProvider : class, ISchemaProvider
    {
        protected abstract string GetDatabaseName();

        [Test]
        public void get_qualified_table_name()
        {
            var name = "TestTable";
            var tblName = Provider.GetTableName("muh", name);
            Assert.That(tblName.Database, Is.EqualTo(GetDatabaseName()));
            Assert.That(tblName.Schema, Is.EqualTo("muh"));
            Assert.That(tblName.Name, Is.EqualTo(name));
        }

        [Test]
        public void manipulates_tables()
        {
            var tblName = Provider.GetTableName("schema1", "ISP_Test");
            var newTblName = Provider.GetTableName("schema2", "ISP_Test2");

            if (!Provider.CheckSchemaExists(tblName.Schema)) Provider.CreateSchema(tblName.Schema);
            if (!Provider.CheckSchemaExists(newTblName.Schema)) Provider.CreateSchema(newTblName.Schema);

            Assert.That(Provider.CheckTableExists(tblName), Is.False);
            Assert.That(Provider.GetTableNames(), Has.No.Member(tblName));

            // create a blank table with an ID::int column and no keys or constraints
            Provider.CreateTable(tblName, false, false);

            Assert.That(Provider.CheckTableExists(tblName), Is.True);
            Assert.That(Provider.GetTableNames(), Has.Member(tblName));

            // test correct table shape
            Assert.That(Provider.CheckColumnExists(tblName, "ID"), Is.True);
            Assert.That(Provider.CheckColumnExists(tblName, "blah"), Is.False);
            Assert.That(Provider.GetTableColumnNames(tblName), Is.EquivalentTo(new[] { "ID" }));
            var colDesc = Provider.GetTableColumns(tblName).Single();
            Assert.That(colDesc.IsNullable, Is.False);
            Assert.That(colDesc.Name, Is.EqualTo("ID"));
            Assert.That(colDesc.Scale, Is.Null);
            // TODO: consider using Int64 as type, and null, 4, or 8 as size
            Assert.That(colDesc.Size, Is.EqualTo(Int32.MaxValue));
            Assert.That(colDesc.Type, Is.EqualTo(DbType.Int32));

            Provider.RenameTable(tblName, newTblName);

            Assert.That(Provider.CheckTableExists(tblName), Is.False);
            Assert.That(Provider.GetTableNames(), Has.No.Member(tblName));

            Assert.That(Provider.CheckTableExists(newTblName), Is.True);
            Assert.That(Provider.GetTableNames(), Has.Member(newTblName));

            Provider.DropTable(newTblName);

            Assert.That(Provider.CheckTableExists(newTblName), Is.False);
            Assert.That(Provider.GetTableNames(), Has.No.Member(newTblName));
        }

        [Test]
        public void creates_table_with_columns()
        {
            var tblName = Provider.GetTableName("schema1", "ISP_Test");
            if (!Provider.CheckSchemaExists(tblName.Schema)) Provider.CreateSchema(tblName.Schema);

            Assert.That(Provider.CheckTableExists(tblName), Is.False);
            Assert.That(Provider.GetTableNames(), Has.No.Member(tblName));

            // create a blank table with an ID::int column and no keys or constraints
            Provider.CreateTable(tblName, false, false);

            Provider.RenameColumn(tblName, "ID", "other_column");

            Assert.That(Provider.GetTableColumnNames(tblName), Has.Member("other_column"));
            Assert.That(Provider.GetTableColumnNames(tblName), Has.No.Member("ID"));
        }

        [Test]
        public void move_table_to_other_schema()
        {
            var tblName = Provider.GetTableName("schema1", "ISP_TestMoveSchema");
            var newTblName = Provider.GetTableName("schema2", "ISP_TestMoveSchema");
            if (!Provider.CheckSchemaExists("schema1")) Provider.CreateSchema("schema1");
            if (!Provider.CheckSchemaExists("schema2")) Provider.CreateSchema("schema2");

            if (!Provider.CheckTableExists(tblName)) Provider.CreateTable(tblName, true);
            if (Provider.CheckTableExists(newTblName)) Provider.DropTable(newTblName);

            Provider.RenameTable(tblName, newTblName);

            Assert.That(Provider.CheckTableExists(tblName), Is.False);
            Assert.That(Provider.CheckTableExists(newTblName), Is.True);
        }
    }
}
