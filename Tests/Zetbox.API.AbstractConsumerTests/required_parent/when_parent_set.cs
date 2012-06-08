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

    public class when_parent_set
        : RequiredParentFixture
    {
        public override void InitTestObjects()
        {
            base.InitTestObjects();
            nSide1.Parent = oneSide1;
            nSide2.Parent = oneSide1;
        }

        [Test]
        public void should_submit()
        {
            ctx.SubmitChanges();
        }

        [Test]
        public void should_delete_saved_objects()
        {
            SubmitAndReload();

            ctx.Delete(oneSide1);
            ctx.Delete(oneSide2);
            ctx.Delete(oneSide3);
            ctx.Delete(nSide1);
            ctx.Delete(nSide2);

            ctx.SubmitChanges();
        }
    }
}
