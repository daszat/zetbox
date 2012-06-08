
namespace Zetbox.DalProvider.NHibernate.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.App.Test;
    using NUnit.Framework;

    [TestFixture]
    public class NhContextTests
        : Zetbox.API.AbstractConsumerTests.AbstractContextTests
    {
        [Test]
        public void should_be_NHibernateContext()
        {
            Assert.That(GetContext(), Is.InstanceOf<NHibernateContext>());
        }
    }
}
