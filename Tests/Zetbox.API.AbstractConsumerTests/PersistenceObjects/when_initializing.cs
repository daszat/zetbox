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

namespace Zetbox.API.AbstractConsumerTests.PersistenceObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.App.Base;
    using NUnit.Framework;

    public abstract class when_initializing
        : AbstractTestFixture
    {
        private IZetboxContext ctx;

        [SetUp]
        public new void SetUp()
        {
            ctx = GetContext();
        }

        [Test]
        public void should_have_New_ObjectState()
        {
            var m1 = ctx.Create<Method>();
            Assert.That(m1.ObjectState, Is.EqualTo(DataObjectState.New));
        }

        [Test]
        public void ift_should_have_New_ObjectState()
        {
            var m1 = ctx.Create(scope.Resolve<InterfaceType.Factory>().Invoke(typeof(Method)));
            Assert.That(m1.ObjectState, Is.EqualTo(DataObjectState.New));
        }

        [Test]
        public void unattached_should_have_Detached_ObjectState()
        {
            var m1 = ctx.Internals().CreateUnattached<Method>();
            Assert.That(m1.ObjectState, Is.EqualTo(DataObjectState.Detached));
        }

        [Test]
        public void unattached_ift_should_have_Detached_ObjectState()
        {
            var m1 = ctx.Internals().CreateUnattached(scope.Resolve<InterfaceType.Factory>().Invoke(typeof(Method)));
            Assert.That(m1.ObjectState, Is.EqualTo(DataObjectState.Detached));
        }

        [Test]
        public void should_set_non_null_ExportGuid()
        {
            var m1 = ctx.Create<Method>();
            Assert.That(m1.ExportGuid, Is.Not.Null);
        }

        [Test]
        public void should_set_non_empty_ExportGuid()
        {
            var m1 = ctx.Create<Method>();
            Assert.That(m1.ExportGuid, Is.Not.EqualTo(Guid.Empty));
            var m2 = ctx.Create<Method>();
            Assert.That(m1.ExportGuid, Is.Not.EqualTo(m2.ExportGuid));
        }

        [Test]
        public void should_have_unique_ExportGuid()
        {
            var m1 = ctx.Create<Method>();
            var m2 = ctx.Create<Method>();
            Assert.That(m1.ExportGuid, Is.Not.EqualTo(m2.ExportGuid));
        }
    }
}
