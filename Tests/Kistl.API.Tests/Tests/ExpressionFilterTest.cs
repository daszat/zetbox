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
    public class ExpressionFilterTest : AbstractApiTestFixture
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Tests.Api.ExpressionFilter");

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
