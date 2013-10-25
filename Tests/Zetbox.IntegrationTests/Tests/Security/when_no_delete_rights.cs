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

namespace Zetbox.IntegrationTests.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    using Autofac;
    using NUnit.Framework;
    using Zetbox.API;
    using Zetbox.API.AbstractConsumerTests;
    using Zetbox.API.Common;
    using Zetbox.App.Base;
    using Zetbox.App.Test;
    using Zetbox.Client.Presentables;
    using System.IO;

    public class when_no_delete_rights : AbstractSecurityTest
    {
        [Test]
        public virtual void should_delete_before_submit()
        {
            var obj = ctx.Create<SecurityChangeRight>();
            obj.Name = "foo";
            Assert.That(obj.CurrentAccessRights.HasDeleteRights(), Is.True);
        }

        [Test]
        public virtual void should_fail_delete_after_submit()
        {
            var obj = ctx.Create<SecurityChangeRight>();
            obj.Name = "foo";
            Assert.That(obj.CurrentAccessRights.HasDeleteRights(), Is.True);
            ctx.SubmitChanges();
            Assert.That(obj.CurrentAccessRights.HasDeleteRights(), Is.False);
        }

        [Test]
        public virtual void should_fail_delete_after_reload()
        {
            int id;
            var obj = ctx.Create<SecurityChangeRight>();
            obj.Name = "foo";
            Assert.That(obj.CurrentAccessRights.HasDeleteRights(), Is.True);
            ctx.SubmitChanges();
            id = obj.ID;

            Reload();

            obj = ctx.Find<SecurityChangeRight>(id);
            Assert.That(obj.CurrentAccessRights.HasDeleteRights(), Is.False);
        }
    }
}
