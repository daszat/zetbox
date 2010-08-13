
namespace Kistl.Server.SchemaTests.SchemaProviders.TableStructure
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using Kistl.API.Server;
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
            var tblName = Provider.GetQualifiedTableName(name);
            Assert.That(tblName.Database, Is.EqualTo(GetDatabaseName()));
            // TODO: fix schema handling
            Assert.That(tblName.Schema, Is.EqualTo("dbo"));
            Assert.That(tblName.Name, Is.EqualTo(name));
        }

        [Test]
        public void manipulates_tables()
        {
            var tblName = Provider.GetQualifiedTableName("ISP_Test");
            var newTblName = Provider.GetQualifiedTableName("ISP_Test2");

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
            var tblName = Provider.GetQualifiedTableName("ISP_Test");

            Assert.That(Provider.CheckTableExists(tblName), Is.False);
            Assert.That(Provider.GetTableNames(), Has.No.Member(tblName));

            // create a blank table with an ID::int column and no keys or constraints
            Provider.CreateTable(tblName, false, false);

            Provider.RenameColumn(tblName, "ID", "other_column");

            Assert.That(Provider.GetTableColumnNames(tblName), Has.Member("other_column"));
            Assert.That(Provider.GetTableColumnNames(tblName), Has.No.Member("ID"));
        }
    }
}
