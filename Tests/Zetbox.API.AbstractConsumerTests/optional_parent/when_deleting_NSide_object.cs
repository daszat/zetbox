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
    using Zetbox.App.Base;
    using Zetbox.App.Test;

    using NUnit.Framework;

    public abstract class when_deleting_NSide_object
        : OneNFixture
    {
        public override void InitTestObjects()
        {
            base.InitTestObjects();
            // prepare
            oneSide1.NSide.Add(nSide1);
            oneSide1.NSide.Add(nSide2);
            SubmitAndReload();
        }

        public void should_not_fail()
        {
            ctx.Delete(nSide1);
            ctx.SubmitChanges();
        }
    }
}
