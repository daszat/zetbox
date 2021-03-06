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

namespace Zetbox.API.AbstractConsumerTests.optional_parent.with_persistent_order
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Test;

    using NUnit.Framework;

    public abstract class OrderedOneNFixture
        : AbstractTestFixture
    {
        protected IZetboxContext ctx;
        protected OrderedOneEnd oneSide1;
        protected OrderedOneEnd oneSide2;
        protected OrderedOneEnd oneSide3;
        protected OrderedNEnd nSide1;
        protected OrderedNEnd nSide2;
        protected OrderedNEnd nSide3;

        [SetUp]
        public virtual void InitTestObjects()
        {
            ctx = GetContext();
            oneSide1 = ctx.Create<OrderedOneEnd>();
            oneSide2 = ctx.Create<OrderedOneEnd>();
            oneSide3 = ctx.Create<OrderedOneEnd>();
            nSide1 = ctx.Create<OrderedNEnd>();
            nSide2 = ctx.Create<OrderedNEnd>();
            nSide3 = ctx.Create<OrderedNEnd>();
        }

        [TearDown]
        public void ForgetTestObjects()
        {
            ctx = null;
            oneSide1 = oneSide2 = oneSide3 = null;
            nSide1 = nSide2 = nSide3 = null;
        }

        protected void SubmitAndReload()
        {
            ctx.SubmitChanges();
            ctx = GetContext();
            oneSide1 = ctx.Find<OrderedOneEnd>(oneSide1.ID);
            oneSide2 = ctx.Find<OrderedOneEnd>(oneSide2.ID);
            oneSide3 = ctx.Find<OrderedOneEnd>(oneSide3.ID);
            nSide1 = ctx.Find<OrderedNEnd>(nSide1.ID);
            nSide2 = ctx.Find<OrderedNEnd>(nSide2.ID);
            nSide3 = ctx.Find<OrderedNEnd>(nSide3.ID);
        }
    }
}
