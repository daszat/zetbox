using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.API;
using System.Reflection;
using System.Linq.Expressions;

namespace API.Tests.Tests
{
    [TestFixture]
    public class CustomActionsManagerFactoryTests
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Init_Fail()
        {
            Assert.That(CustomActionsManagerFactory.Current, Is.Not.Null);
            CustomActionsManagerFactory.Init(new CustomActionsManagerAPITest());
        }
    }
}
