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
    using Zetbox.App.Base;
    using NUnit.Framework;

    public abstract class when_submitting_without_read
        : AbstractTestFixture
    {
        [Test]
        public void should_persist_a_value()
        {
            var ctx = GetContext();
            var obj = ctx.Create<TypeRef>();
            obj.FullName = "TestRef";
            obj.Assembly = ctx.GetQuery<Assembly>().First();
            ctx.SubmitChanges();

            // read ExportGuid after submit to not influence datastore
            var originalExportGuid = obj.ExportGuid;
            var originalId = obj.ID;

            ctx = GetContext();
            obj = ctx.Find<TypeRef>(originalId);

            Assert.That(obj.ExportGuid, Is.EqualTo(originalExportGuid));
        }
    }

    public abstract class when_submitting_with_read
        : AbstractTestFixture
    {
        [Test]
        public void should_persist_a_value()
        {
            var ctx = GetContext();
            var obj = ctx.Create<TypeRef>();
            obj.FullName = "TestRef";
            obj.Assembly = ctx.GetQuery<Assembly>().First();

            // read ExportGuid before submit to test initialisation
            var originalExportGuid = obj.ExportGuid;

            ctx.SubmitChanges();

            var originalId = obj.ID;

            ctx = GetContext();
            obj = ctx.Find<TypeRef>(originalId);

            Assert.That(obj.ExportGuid, Is.EqualTo(originalExportGuid));
        }
    }
}
