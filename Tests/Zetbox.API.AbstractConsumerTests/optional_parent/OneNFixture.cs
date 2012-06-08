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

namespace Zetbox.API.AbstractConsumerTests.optional_parent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Test;

    using NUnit.Framework;

    public abstract class OneNFixture
        : AbstractTestFixture
    {
        protected IZetboxContext ctx;
        protected One_to_N_relations_One oneSide1;
        protected One_to_N_relations_One oneSide2;
        protected One_to_N_relations_One oneSide3;
        protected One_to_N_relations_N nSide1;
        protected One_to_N_relations_N nSide2;
        protected bool hasSubmitted = false;

        [SetUp]
        public virtual void InitTestObjects()
        {
            ctx = GetContext();
            oneSide1 = ctx.Create<One_to_N_relations_One>();
            oneSide2 = ctx.Create<One_to_N_relations_One>();
            oneSide3 = ctx.Create<One_to_N_relations_One>();
            nSide1 = ctx.Create<One_to_N_relations_N>();
            nSide2 = ctx.Create<One_to_N_relations_N>();
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
            oneSide1 = ctx.Find<One_to_N_relations_One>(oneSide1.ID);
            oneSide2 = ctx.Find<One_to_N_relations_One>(oneSide2.ID);
            oneSide3 = ctx.Find<One_to_N_relations_One>(oneSide3.ID);
            nSide1 = ctx.Find<One_to_N_relations_N>(nSide1.ID);
            nSide2 = ctx.Find<One_to_N_relations_N>(nSide2.ID);
        }
    }
}
