// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.IntegrationTests.Methods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using Zetbox.API;
    using Zetbox.API.AbstractConsumerTests;
    using Zetbox.App.Test;

    [TestFixture]
    public abstract class when_invoking_on_server : AbstractTestFixture
    {
        protected MethodTest obj;
        protected IZetboxContext ctx;

        public override void SetUp()
        {
            base.SetUp();
            ctx = GetContext();
        }

        public class when_invoking_on_new_object : when_invoking_on_server
        {
            public override void SetUp()
            {
                base.SetUp();
                obj = ctx.Create<MethodTest>();
            }
        }

        public class when_invoking_on_unmodified_object : when_invoking_on_server
        {
            public override void SetUp()
            {
                base.SetUp();
                int newID = -1;
                using (var createCtx = GetContext())
                {
                    var created = createCtx.Create<MethodTest>();
                    createCtx.SubmitChanges();
                    newID = created.ID;
                }
                obj = ctx.Find<MethodTest>(newID);
            }
        }

        public class when_invoking_on_modified_object : when_invoking_on_unmodified_object
        {
            public override void SetUp()
            {
                base.SetUp();
                obj.StringProp += DateTime.Now;
            }
        }

        [Test]
        public void should_fail_not_implemented()
        {
            Assert.Throws<Exception>(() => { obj.ServerParameterless(); });
        }

        [Test]
        public void should_receive_new_retval()
        {
            var result = obj.ServerObjParameter(null);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StringProp, Is.EqualTo("A"));
        }

        [Test]
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

        [Test]
        public void should_not_reset_objectstate_on_roundtrip()
        {
            var expectedState = obj.ObjectState;

            obj.ServerMethod();

            Assert.That(obj.ObjectState, Is.EqualTo(expectedState));
        }
    }
}
