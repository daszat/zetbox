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

namespace Zetbox.API.AbstractConsumerTests.Enums
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using Zetbox.API;
    using Zetbox.App.Test;

    public class when_querying : AbstractTestFixture
    {
        [Test]
        public void should_compare_equal()
        {
            // pull out val to test SubtreeEvaluator
            var val = TestEnum.Second;
            var result = GetContext().GetQuery<TestObjClass>().Where(toc => toc.TestEnumProp == val).ToList();
            Assert.That(result, Is.Not.Null);
        }
    }
}
