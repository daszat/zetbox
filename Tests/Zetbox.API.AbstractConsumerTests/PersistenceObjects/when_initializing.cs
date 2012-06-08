
namespace Zetbox.API.AbstractConsumerTests.PersistenceObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.App.Base;
    using NUnit.Framework;

    public abstract class when_initializing
        : AbstractTestFixture
    {
        private IZetboxContext ctx;

        [SetUp]
        public new void SetUp()
        {
            ctx = GetContext();
        }

        [Test]
        public void should_have_New_ObjectState()
        {
            var m1 = ctx.Create<Method>();
            Assert.That(m1.ObjectState, Is.EqualTo(DataObjectState.New));
        }

        [Test]
        public void ift_should_have_New_ObjectState()
        {
            var m1 = ctx.Create(scope.Resolve<InterfaceType.Factory>().Invoke(typeof(Method)));
            Assert.That(m1.ObjectState, Is.EqualTo(DataObjectState.New));
        }

        [Test]
        public void unattached_should_have_Detached_ObjectState()
        {
            var m1 = ctx.Internals().CreateUnattached<Method>();
            Assert.That(m1.ObjectState, Is.EqualTo(DataObjectState.Detached));
        }

        [Test]
        public void unattached_ift_should_have_Detached_ObjectState()
        {
            var m1 = ctx.Internals().CreateUnattached(scope.Resolve<InterfaceType.Factory>().Invoke(typeof(Method)));
            Assert.That(m1.ObjectState, Is.EqualTo(DataObjectState.Detached));
        }

        [Test]
        public void should_set_non_null_ExportGuid()
        {
            var m1 = ctx.Create<Method>();
            Assert.That(m1.ExportGuid, Is.Not.Null);
        }

        [Test]
        public void should_set_non_empty_ExportGuid()
        {
            var m1 = ctx.Create<Method>();
            Assert.That(m1.ExportGuid, Is.Not.EqualTo(Guid.Empty));
            var m2 = ctx.Create<Method>();
            Assert.That(m1.ExportGuid, Is.Not.EqualTo(m2.ExportGuid));
        }

        [Test]
        public void should_have_unique_ExportGuid()
        {
            var m1 = ctx.Create<Method>();
            var m2 = ctx.Create<Method>();
            Assert.That(m1.ExportGuid, Is.Not.EqualTo(m2.ExportGuid));
        }
    }
}
