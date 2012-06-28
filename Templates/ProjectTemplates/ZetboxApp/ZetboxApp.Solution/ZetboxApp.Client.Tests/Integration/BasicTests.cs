
namespace $safeprojectname$.Integration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using Zetbox.App.Base;

    public class BasicTests : Zetbox.API.AbstractConsumerTests.AbstractTestFixture
    {
        [Test]
        public void ConnectionTest()
        {
            var ctx = GetContext();
            var result = ctx.GetQuery<ObjectClass>().FirstOrDefault();
            Assert.That(result, Is.Not.Null);
        }
    }
}
