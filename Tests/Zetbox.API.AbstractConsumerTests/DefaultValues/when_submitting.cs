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

namespace Zetbox.API.AbstractConsumerTests.DefaultValues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using Zetbox.App.Base;
    using Zetbox.App.Test;

    public abstract class when_persisting_guids
        : AbstractTestFixture
    {
        protected IZetboxContext ctx;
        protected Assembly obj;
        protected int originalId;

        public override void SetUp()
        {
            base.SetUp();
            ctx = GetContext();
            obj = ctx.Create<Assembly>();
            obj.Name = "TestAssembly";
            obj.Module = ctx.GetQuery<Module>().First(m => m.Name == "TestModule");
        }

        public override void TearDown()
        {
            ctx = GetContext();
            var tdObj = ctx.GetQuery<Assembly>().Where(a => a.ID == originalId);
            foreach (var o in tdObj.ToList())
            {
                ctx.Delete(o);
            }
            ctx.SubmitChanges();
            base.TearDown();
        }

        private void SubmitAndTest(Guid expectedExportGuid)
        {
            ctx.SubmitChanges();
            originalId = obj.ID;

            var fresh = GetContext().Find<Assembly>(originalId);
            Assert.That(fresh.ExportGuid, Is.EqualTo(expectedExportGuid));
        }

        [Test]
        public void should_persist_a_written_value()
        {
            // set value explicitly
            var expectedExportGuid = obj.ExportGuid = Guid.NewGuid();

            SubmitAndTest(expectedExportGuid);
        }

        [Test]
        public void should_initialize_the_value_on_reading()
        {
            // read ExportGuid before submit to test client-side initialisation
            var expectedExportGuid = obj.ExportGuid;

            SubmitAndTest(expectedExportGuid);
        }

        [Test]
        public void should_initialize_the_value_without_reading()
        {
            ctx.SubmitChanges();
            originalId = obj.ID;

            // read TestEnumWithDefault after submit to test server-side initialisation
            var expectedExportGuid = obj.ExportGuid;

            var fresh = GetContext().Find<Assembly>(originalId);
            Assert.That(fresh.ExportGuid, Is.EqualTo(expectedExportGuid));
        }
    }

    public abstract class when_persisting_enums
        : AbstractTestFixture
    {
        protected IZetboxContext ctx;
        protected TestObjClass obj;
        protected int originalId;

        public override void SetUp()
        {
            base.SetUp();
            ctx = GetContext();
            obj = ctx.Create<TestObjClass>();
            obj.StringProp = "Some String";
        }

        public override void TearDown()
        {
            ctx = GetContext();
            var tdObj = ctx.GetQuery<TestObjClass>().Where(a => a.ID == originalId);
            foreach (var o in tdObj.ToList())
            {
                ctx.Delete(o);
            }
            base.TearDown();
        }

        private void SubmitAndTest(TestEnum expectedEnum)
        {
            ctx.SubmitChanges();
            originalId = obj.ID;

            var fresh = GetContext().Find<TestObjClass>(originalId);
            Assert.That(fresh.TestEnumWithDefault, Is.EqualTo(expectedEnum));
        }

        [Test]
        public void should_persist_a_written_value()
        {
            // set value explicitly
            var expectedEnum = obj.TestEnumWithDefault = TestEnum.Third;

            SubmitAndTest(expectedEnum);
        }

        [Test]
        public void should_initialize_the_value_without_reading()
        {
            ctx.SubmitChanges();
            originalId = obj.ID;

            // read TestEnumWithDefault after submit to test server-side initialisation
            var expectedEnum = obj.TestEnumWithDefault;

            var fresh = GetContext().Find<TestObjClass>(originalId);
            Assert.That(fresh.TestEnumWithDefault, Is.EqualTo(expectedEnum));
        }

        [Test]
        public void should_persist_an_enum_value()
        {
            // read TestEnumWithDefault before submit to test client-side initialisation
            var expectedEnum = obj.TestEnumWithDefault;

            SubmitAndTest(expectedEnum);
        }
    }
}
