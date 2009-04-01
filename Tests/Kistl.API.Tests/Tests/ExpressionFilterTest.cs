using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Kistl.API.Mocks;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Kistl.API.Tests
{
    [TestFixture]
    public class ExpressionFilterTest
    {
        TestQuery<IDataObject> ctx;

        [SetUp]
        public void SetUp()
        {
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
        public void IllegalExpression()
        {
            var list = from o in ctx
                       where o.ID == 1
                       && o.GetType().FullName.StartsWith("test")
                       select o;

            List<IllegalExpression> errorList;
            bool result = list.Expression.IsLegal(out errorList);
            System.Diagnostics.Trace.WriteLine("ErrorList for IllegalExpression:");
            errorList.ForEach(e => System.Diagnostics.Trace.WriteLine(e.ToString()));

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
