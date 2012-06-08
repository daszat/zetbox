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

namespace Zetbox.API.AbstractConsumerTests.ContextTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Test;

    using NUnit.Framework;

    public abstract class when_deleting_n_m
        : AbstractTestFixture
    {
        IZetboxContext ctx;
        N_to_M_relations_A a1;
        N_to_M_relations_B b1;
        N_to_M_relations_B b2;

        public override void SetUp()
        {
            base.SetUp();

            ctx = GetContext();

            a1 = ctx.Create<N_to_M_relations_A>();
            b1 = ctx.Create<N_to_M_relations_B>();
            b2 = ctx.Create<N_to_M_relations_B>();

            a1.BSide.Add(b1);
            a1.BSide.Add(b2);

            ctx.SubmitChanges();
        }

        [Test]
        public void should_remove_n_m()
        {
            ctx.Delete(a1);
            ctx.Delete(b1);
            ctx.Delete(b2);

            Assert.That(a1.ObjectState, Is.EqualTo(DataObjectState.Deleted));
            Assert.That(b1.ObjectState, Is.EqualTo(DataObjectState.Deleted));
            Assert.That(b2.ObjectState, Is.EqualTo(DataObjectState.Deleted));

            ctx.SubmitChanges();
        }
    }
}
