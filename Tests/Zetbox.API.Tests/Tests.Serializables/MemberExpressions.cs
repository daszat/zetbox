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
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Zetbox.API.Utils;

using NUnit.Framework;
using NUnit.Framework.Constraints;

using Autofac;

namespace Zetbox.API.Tests.Serializables
{

    [TestFixture]
    public class MemberExpressions : AbstractApiTestFixture
    {

        public override void SetUp()
        {
            base.SetUp();
        }

        // TODO: Discuss: do we really want to optimize Linq epxressions when serializing?
        //       currently, this seems to be a side-effect of the ExpressionEvaluator
        //       in the far future, we might want to extract expression optimisation into a
        //       (mode) standalone component
        [Test]
        public void roundtrip_MemberAccess_to_string_Length_property_should_optimize_constness()
        {
            string testString = "farglbl";
            var objExpr = Expression.Constant(testString);
            MemberExpression expr = Expression.MakeMemberAccess(objExpr, typeof(string).GetMember("Length").First());

            var result = SerializableExpression.ToExpression(SerializableExpression.FromExpression(expr, scope.Resolve<InterfaceType.Factory>()));

            AssertExpressions.AreEqual(result, Expression.Constant(testString.Length));
        }

    }

}
