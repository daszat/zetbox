
namespace Zetbox.DalProvider.Client.Tests.ZetboxContextTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Utils;
    using Zetbox.App.Test;
    using NUnit.Framework;

    [TestFixture]
    public class when_using_ClientIsolationLevel_MergeServerData : Zetbox.API.AbstractConsumerTests.AbstractTestFixture
    {
        private IZetboxContext ctx;

        public override void SetUp()
        {
            base.SetUp();

            Logging.Log.Info("ZetboxContextTests.SetUp() is called");

            ctx = scope.Resolve<Func<ClientIsolationLevel, IZetboxContext>>().Invoke(ClientIsolationLevel.MergeServerData);
        }

        [Test]
        public void should_not_change_on_Find()
        {
            TestObjClass obj = ctx.Find<TestObjClass>(1);
            Assert.That(obj.StringProp, Is.EqualTo("String 1"));
            obj.StringProp = "Changed";
            TestObjClass obj_test = ctx.Find<TestObjClass>(1);
            Assert.That(obj_test.StringProp, Is.EqualTo("Changed"));
            Assert.That(obj.StringProp, Is.EqualTo("Changed"));
            Assert.That(obj, Is.SameAs(obj_test));
        }

        [Test]
        public void should_not_change_on_First()
        {
            TestObjClass obj = ctx.GetQuery<TestObjClass>().First(i => i.ID == 1);
            Assert.That(obj.StringProp, Is.EqualTo("String 1"));
            obj.StringProp = "Changed";
            TestObjClass obj_test = ctx.GetQuery<TestObjClass>().First(i => i.ID == 1);
            Assert.That(obj_test.StringProp, Is.EqualTo("Changed"));
            Assert.That(obj.StringProp, Is.EqualTo("Changed"));
            Assert.That(obj, Is.SameAs(obj_test));
        }

        [Test]
        public void should_change_on_GetList()
        {
            TestObjClass obj = ctx.GetQuery<TestObjClass>().First(i => i.ID == 1);
            Assert.That(obj.StringProp, Is.EqualTo("String 1"));
            obj.StringProp = "Changed";
            var lst = ctx.GetQuery<TestObjClass>().ToList();
            TestObjClass obj_test = ctx.Find<TestObjClass>(1);
            Assert.That(lst.Count, Is.GreaterThan(0));
            Assert.That(obj.StringProp, Is.EqualTo("String 1"));
            Assert.That(obj_test.StringProp, Is.EqualTo("String 1"));
            Assert.That(obj, Is.SameAs(obj_test));
        }
    }
}
