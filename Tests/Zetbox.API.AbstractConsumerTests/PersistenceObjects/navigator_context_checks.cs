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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Zetbox.API;

using NUnit.Framework;
using Zetbox.App.Test;

namespace Zetbox.API.AbstractConsumerTests.PersistenceObjects
{
    public abstract class navigator_context_checks : ObjectLoadFixture
    {
        [Test]
        public void set_1_N_same_context()
        {
            var a = ctx.Create<TestCustomObject>();
            var b = ctx.Create<Muhblah>();
            a.MubBlah_Nav = b;
        }

        [Test]
        [ExpectedException(typeof(WrongZetboxContextException))]
        public void set_1_N_wrong_context()
        {
            var otherCtx = GetContext();
            Assert.That(ctx, Is.Not.SameAs(otherCtx));
            var a = ctx.Create<TestCustomObject>();
            var b = otherCtx.Create<Muhblah>();
            a.MubBlah_Nav = b;
        }

        [Test]
        public void set_N_1_same_context()
        {
            var a = ctx.Create<TestCustomObject>();
            var b = ctx.Create<Muhblah>();
            b.TestCustomObjects_List_Nav.Add(a);
        }

        [Test]
        [ExpectedException(typeof(WrongZetboxContextException))]
        public void set_N_1_wrong_context()
        {
            var otherCtx = GetContext();
            Assert.That(ctx, Is.Not.SameAs(otherCtx));
            var a = ctx.Create<TestCustomObject>();
            var b = otherCtx.Create<Muhblah>();
            b.TestCustomObjects_List_Nav.Add(a);
        }

        [Test]
        public void set_N_M_same_context()
        {
            var a = ctx.Create<TestCustomObject>();
            var b = ctx.Create<Muhblah>();
            b.TestCustomObjects_ManyList_Nav.Add(a);
        }

        [Test]
        [ExpectedException(typeof(WrongZetboxContextException))]
        public void set_N_M_wrong_context()
        {
            var otherCtx = GetContext();
            Assert.That(ctx, Is.Not.SameAs(otherCtx));
            var a = ctx.Create<TestCustomObject>();
            var b = otherCtx.Create<Muhblah>();
            b.TestCustomObjects_ManyList_Nav.Add(a);
        }

        [Test]
        public void set_1_1_same_context()
        {
            var a = ctx.Create<TestCustomObject>();
            var b = ctx.Create<Muhblah>();
            a.MuhBlah_One_Nav = b;
        }

        [Test]
        [ExpectedException(typeof(WrongZetboxContextException))]
        public void set_1_1_wrong_context()
        {
            var otherCtx = GetContext();
            Assert.That(ctx, Is.Not.SameAs(otherCtx));
            var a = ctx.Create<TestCustomObject>();
            var b = otherCtx.Create<Muhblah>();
            a.MuhBlah_One_Nav = b;
        }
    }
}
