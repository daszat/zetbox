
namespace Kistl.DalProvider.NHibernate.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Kistl.App.Test;
    using NUnit.Framework;

    [TestFixture]
    public class NhContextTests
        : Kistl.API.AbstractConsumerTests.AbstractContextTests
    {
        [Test]
        public void should_be_NHibernateContext()
        {
            Assert.That(GetContext(), Is.InstanceOf<NHibernateContext>());
        }
    }
}
