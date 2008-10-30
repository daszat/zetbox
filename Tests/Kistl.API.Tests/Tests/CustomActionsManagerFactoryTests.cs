using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Kistl.API;

using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;

namespace Kistl.API.Tests
{
    [TestFixture]
    public class CustomActionsManagerFactoryTests
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Init_Fail()
        {
            Assert.That(ApplicationContext.Current.CustomActionsManager, Is.Not.Null);
        }
    }
}
