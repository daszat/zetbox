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

    public abstract class when_submitting_after_write
        : AbstractTestFixture
    {
        [Test]
        public void should_persist_a_guid_value()
        {
            var ctx = GetContext();
            var obj = ctx.Create<Assembly>();
            obj.Name = "TestAssembly";

            // set value explicitly
            var expectedExportGuid = obj.ExportGuid = Guid.NewGuid();

            ctx.SubmitChanges();

            var originalId = obj.ID;

            ctx = GetContext();
            obj = ctx.Find<Assembly>(originalId);

            Assert.That(obj.ExportGuid, Is.EqualTo(expectedExportGuid));
        }

        [Test]
        public void should_persist_an_enum_value()
        {
            var ctx = GetContext();
            var obj = ctx.Create<TestObjClass>();
            obj.StringProp = "Some String";

            // set value explicitly
            var expectedEnum = obj.TestEnumWithDefault = TestEnum.Third;
            
            ctx.SubmitChanges();

            var originalId = obj.ID;

            ctx = GetContext();
            obj = ctx.Find<TestObjClass>(originalId);

            Assert.That(obj.TestEnumWithDefault, Is.EqualTo(expectedEnum));
        }
    }

    public abstract class when_submitting_without_read
           : AbstractTestFixture
    {
        [Test]
        public void should_persist_a_guid_value()
        {
            var ctx = GetContext();
            var obj = ctx.Create<Assembly>();
            obj.Name = "TestRef";
            ctx.SubmitChanges();

            // read TestEnumWithDefault after submit to test server-side initialisation
            var expectedExportGuid = obj.ExportGuid;
            var originalId = obj.ID;

            ctx = GetContext();
            obj = ctx.Find<Assembly>(originalId);

            Assert.That(obj.ExportGuid, Is.EqualTo(expectedExportGuid));
        }

        [Test]
        public void should_persist_an_enum_value()
        {
            var ctx = GetContext();
            var obj = ctx.Create<TestObjClass>();
            obj.StringProp = "Some String";

            ctx.SubmitChanges();

            // read TestEnumWithDefault after submit to test server-side initialisation
            var expectedEnum = obj.TestEnumWithDefault;
            var originalId = obj.ID;

            ctx = GetContext();
            obj = ctx.Find<TestObjClass>(originalId);

            Assert.That(obj.TestEnumWithDefault, Is.EqualTo(expectedEnum));
        }
    }

    public abstract class when_submitting_with_read
        : AbstractTestFixture
    {
        [Test]
        public void should_persist_a_guid_value()
        {
            var ctx = GetContext();
            var obj = ctx.Create<Assembly>();
            obj.Name = "TestRef";

            // read ExportGuid before submit to test client-side initialisation
            var expectedExportGuid = obj.ExportGuid;

            ctx.SubmitChanges();

            var originalId = obj.ID;

            ctx = GetContext();
            obj = ctx.Find<Assembly>(originalId);

            Assert.That(obj.ExportGuid, Is.EqualTo(expectedExportGuid));
        }

        [Test]
        public void should_persist_an_enum_value()
        {
            var ctx = GetContext();
            var obj = ctx.Create<TestObjClass>();
            obj.StringProp = "Some String";

            // read TestEnumWithDefault before submit to test client-side initialisation
            var expectedEnum = obj.TestEnumWithDefault;

            ctx.SubmitChanges();

            var originalId = obj.ID;

            ctx = GetContext();
            obj = ctx.Find<TestObjClass>(originalId);

            Assert.That(obj.TestEnumWithDefault, Is.EqualTo(expectedEnum));
        }
    }
}
