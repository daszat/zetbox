using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API.AbstractConsumerTests;
using NUnit.Framework;
using Zetbox.App.Test;
using Zetbox.API;

namespace Zetbox.IntegrationTests.Methods
{
    [TestFixture]
    public class when_invoking_on_server : AbstractTestFixture
    {
        private MethodTest obj;
        private IZetboxContext ctx;

        public override void SetUp()
        {
            base.SetUp();
            ctx = GetContext();
            obj = ctx.GetQuery<MethodTest>().FirstOrDefault();
            if (obj == null)
            {
                using (var createCtx = GetContext())
                {
                    var createObj = createCtx.Create<MethodTest>();
                    createCtx.SubmitChanges();
                }
                obj = ctx.GetQuery<MethodTest>().FirstOrDefault();
            }

            Assert.That(obj, Is.Not.Null);
        }

        [Test]
        [ExpectedException]
        public void should_fail_not_implemented()
        {
            obj.ServerParameterless();
        }

        [Test]
        public void should_receive_new_retval()
        {
            var result = obj.ServerObjParameter(null);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StringProp, Is.EqualTo("A"));
        }

        [Test]
        [Ignore("not implemented yet")]
        public void should_receive_sent_retval()
        {
            var newC = ctx.Create<TestObjClass>();
            newC.StringProp = "C";
            var result = obj.ServerObjParameter(newC);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.SameAs(newC));
            Assert.That(result.StringProp, Is.EqualTo("C"));
        }

        [Test]
        public void should_receive_new_object()
        {
            obj.ServerObjParameter(null);
            var b = ctx.GetQuery<TestObjClass>().FirstOrDefault(i => i.StringProp == "B");
            Assert.That(b, Is.Not.Null);
            Assert.That(b.StringProp, Is.EqualTo("B"));
        }

        [Test]
        public void should_receive_object_graph()
        {
            obj.ServerObjParameter(null);
            var b = ctx.GetQuery<TestObjClass>().FirstOrDefault(i => i.StringProp == "B");
            Assert.That(b, Is.Not.Null);
            var kunde = b.ObjectProp;
            Assert.That(kunde, Is.Not.Null);
            Assert.That(kunde.Kundenname, Is.EqualTo("Kunde"));
        }
    }
}
