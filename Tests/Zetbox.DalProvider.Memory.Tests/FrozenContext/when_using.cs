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

namespace Zetbox.DalProvider.Memory.Tests.FrozenContext
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.AbstractConsumerTests;
    using Zetbox.App.Base;
    using NUnit.Framework;

    public sealed class when_using
        : AbstractTestFixture
    {
        [Test]
        public void should_have_references()
        {
            var ctx = scope.Resolve<IFrozenContext>();
            Assert.That(ctx, Is.Not.Null);
            Assert.That(ctx.GetQuery<ObjectClass>().Where(oc => oc.BaseObjectClass != null).ToList(), Is.Not.Empty);
            Assert.That(ctx.GetQuery<ObjectClass>().Where(oc => oc.DefaultViewModelDescriptor != null).ToList(), Is.Not.Empty);
        }

        [Test]
        [Ignore("Not implemented")]
        public void should_reject_modifications()
        {
            var ctx = scope.Resolve<IFrozenContext>();
            Assert.That(() =>
                {
                    ctx.GetQuery<ObjectClass>().Where(oc => oc.BaseObjectClass != null).First().BaseObjectClass = null;
                },
                Throws.InstanceOf<ReadOnlyObjectException>());
        }
    }
}
