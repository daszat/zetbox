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

namespace Zetbox.API.AbstractConsumerTests.required_parent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Test;

    using NUnit.Framework;

    public abstract class RequiredParentFixture
        : AbstractTestFixture
    {
        protected IZetboxContext ctx;
        protected RequiredParent oneSide1;
        protected RequiredParent oneSide2;
        protected RequiredParent oneSide3;
        protected RequiredParentChild nSide1;
        protected RequiredParentChild nSide2;
        protected bool hasSubmitted = false;

        [SetUp]
        public virtual void InitTestObjects()
        {
            ctx = GetContext();
            oneSide1 = ctx.Create<RequiredParent>();
            oneSide2 = ctx.Create<RequiredParent>();
            oneSide3 = ctx.Create<RequiredParent>();
            nSide1 = ctx.Create<RequiredParentChild>();
            nSide2 = ctx.Create<RequiredParentChild>();
        }

        [TearDown]
        public void ForgetTestObjects()
        {
            ctx = null;
            oneSide1 = oneSide2 = oneSide3 = null;
            nSide1 = nSide2 = null;
            hasSubmitted = false;
        }

        protected void SubmitAndReload()
        {
            ctx.SubmitChanges();
            hasSubmitted = true;
            ctx = GetContext();
            oneSide1 = ctx.Find<RequiredParent>(oneSide1.ID);
            oneSide2 = ctx.Find<RequiredParent>(oneSide2.ID);
            oneSide3 = ctx.Find<RequiredParent>(oneSide3.ID);
            nSide1 = ctx.Find<RequiredParentChild>(nSide1.ID);
            nSide2 = ctx.Find<RequiredParentChild>(nSide2.ID);
        }
    }
}
