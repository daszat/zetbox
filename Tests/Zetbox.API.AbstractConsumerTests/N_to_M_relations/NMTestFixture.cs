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

namespace Zetbox.API.AbstractConsumerTests.N_to_M_relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Test;
    using NUnit.Framework;

    public abstract class NMTestFixture
        : AbstractTestFixture
    {
        protected IZetboxContext ctx;
        protected N_to_M_relations_A aSide1;
        protected N_to_M_relations_A aSide2;
        protected N_to_M_relations_B bSide1;
        protected N_to_M_relations_B bSide2;

        [SetUp]
        public virtual void InitTestObjects()
        {
            ctx = GetContext();
            aSide1 = ctx.Create<N_to_M_relations_A>();
            aSide2 = ctx.Create<N_to_M_relations_A>();
            bSide1 = ctx.Create<N_to_M_relations_B>();
            bSide2 = ctx.Create<N_to_M_relations_B>();
        }

        [TearDown]
        public virtual void ForgetTestObjects()
        {
            aSide1 = aSide2 = null;
            bSide1 = bSide2 = null;
            ctx = null;
        }

        protected virtual void SubmitAndReload()
        {
            ctx.SubmitChanges();
            ctx = GetContext();
            aSide1 = ctx.Find<N_to_M_relations_A>(aSide1.ID);
            aSide2 = ctx.Find<N_to_M_relations_A>(aSide2.ID);
            bSide1 = ctx.Find<N_to_M_relations_B>(bSide1.ID);
            bSide2 = ctx.Find<N_to_M_relations_B>(bSide2.ID);
        }
    }
}
