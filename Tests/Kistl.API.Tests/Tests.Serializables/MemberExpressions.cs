using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Kistl.API.Utils;

using NUnit.Framework;
using NUnit.Framework.Constraints;

using Autofac;

namespace Kistl.API.Tests.Serializables
{

    [TestFixture]
    public class MemberExpressions : AbstractConsumerTests.AbstractTextFixture
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

            var result = SerializableExpression.ToExpression(SerializableExpression.FromExpression(expr, scope.Resolve<ITypeTransformations>()));

            AssertExpressions.AreEqual(result, Expression.Constant(testString.Length));
        }

    }

}
