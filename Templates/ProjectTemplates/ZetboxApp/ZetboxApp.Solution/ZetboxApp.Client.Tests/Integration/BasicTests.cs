
namespace $safeprojectname$.Integration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;

    public class BasicTests : Zetbox.API.AbstractConsumerTests.AbstractTestFixture
    {
        [Test]
        public void ConnectionTest()
        {
             var ctx = GetContext();
             var result = ctx.GetQuery<Zetbox.App.Base.ObjectClass>().FirstOrDefault();
             Assert.That(result, Is.Not.Null);
        }
    }
}
