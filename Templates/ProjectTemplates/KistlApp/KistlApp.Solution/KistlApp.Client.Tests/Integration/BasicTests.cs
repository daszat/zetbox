
namespace KistlApp.Solution.KistlApp.Client.Tests.Integration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.App.Base;
    using NUnit.Framework;

    public class BasicTests : Kistl.API.AbstractConsumerTests.AbstractTestFixture
    {
        [Test]
        public void ConnectionTest()
        {
            var ctx = GetClientContext();
            var result = ctx.GetQuery<ObjectClass>().FirstOrDefault();
            Assert.That(result, Is.Not.Null);
        }
    }
}
