using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.API.Utils;

namespace Kistl.API.Tests.Serializables
{

    [TestFixture]
    public class MemberExpressions
    {
      
        // TODO: Discuss: do we really want to optimize Linq epxressions when serializing?
        [Test]
        public void roundtrip_MemberAccess_to_string_Length_property_should_optimize_constness()
        {
            string testString = "farglbl";
            var objExpr = Expression.Constant(testString);
            MemberExpression expr = Expression.MakeMemberAccess(objExpr, typeof(string).GetMember("Length").First());

            var result = SerializableExpression.ToExpression(SerializableExpression.FromExpression(expr));

            AssertExpressions.AreEqual(result, Expression.Constant(testString.Length));
        }
	
    }

}
