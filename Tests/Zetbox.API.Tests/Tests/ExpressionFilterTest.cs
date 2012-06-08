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
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Zetbox.API.Mocks;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Zetbox.API.Tests
{
    [TestFixture]
    public class ExpressionFilterTest : AbstractApiTestFixture
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Tests.Api.ExpressionFilter");

        TestQuery<IDataObject> ctx;

        public override void SetUp()
        {
            base.SetUp();
            ctx = new TestQuery<IDataObject>();
        }

        [Test]
        public void LegalExpression()
        {
            Expression<Func<int, int>> expression = (a) => a * 2;
            bool result = expression.IsLegal();
            Assert.That(result, Is.True);
        }

        [Test]
        [Ignore("Illegal Expression checking disabled for now")]
        public void IllegalExpression()
        {
            var list = from o in ctx
                       where o.ID == 1
                       && o.GetType().FullName.StartsWith("test")
                       select o;

            List<IllegalExpression> errorList;
            bool result = list.Expression.IsLegal(out errorList);

            if (errorList.Count > 0 && Log.IsDebugEnabled)
            {
                Log.Debug("ErrorList for IllegalExpression:");
                errorList.ForEach(e => Log.Debug(e.ToString()));
            }

            Assert.That(result, Is.False);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void IllegalExpressionClass_UnknownToString()
        {
            IllegalExpression e = new IllegalExpression();
            e.ToString();
        }
    }
}
