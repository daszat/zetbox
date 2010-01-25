using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Kistl.App.Test;

namespace Kistl.API.AbstractConsumerTests.CompoundObjects
{
    public abstract class when_roundtrip : CompoundObjectFixture
    {
        Random rnd = new Random();
        string number;

        [SetUp]
        public void SetUp()
        {
            number = rnd.Next().ToString();
        }

        [Test]
        public void should_getobject()
        {
            using (IKistlContext ctx = GetContext())
            {
                var objList = ctx.GetQuery<TestCustomObject>();
                foreach (var obj in objList)
                {
                    Assert.That(obj.ID, Is.GreaterThan(Helper.INVALIDID), "received object with invalid id");
                }
            }
        }

        [Test]
        public void should_createobject()
        {
            int ID = Kistl.API.Helper.INVALIDID;
            using (Kistl.API.IKistlContext ctx = GetContext())
            {
                var obj = ctx.Create<Kistl.App.Test.TestCustomObject>();
                obj.PersonName = "TestPerson " + rnd.Next();
                obj.Birthday = DateTime.Now;

                Assert.That(obj.PhoneNumberMobile, Is.Null);
                Assert.That(obj.PhoneNumberOffice, Is.Not.Null);

                obj.PhoneNumberMobile = ctx.CreateCompoundObject<TestPhoneCompoundObject>();
                obj.PhoneNumberMobile.AreaCode = "1";
                obj.PhoneNumberMobile.Number = number;

                obj.PhoneNumberOffice.AreaCode = "1";
                obj.PhoneNumberOffice.Number = number;

                Assert.That(ctx.SubmitChanges(), Is.EqualTo(1));

                ID = obj.ID;
            }

            using (Kistl.API.IKistlContext ctx = GetContext())
            {
                var obj = ctx.Find<Kistl.App.Test.TestCustomObject>(ID);
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.PhoneNumberMobile, Is.Not.Null);
                Assert.That(obj.PhoneNumberOffice, Is.Not.Null);
                Assert.That(obj.PhoneNumberMobile.Number, Is.EqualTo(number));
                Assert.That(obj.PhoneNumberOffice.Number, Is.EqualTo(number));
            }
        }

        [Test]
        public void should_createobject_nullable()
        {
            int ID = Kistl.API.Helper.INVALIDID;
            using (Kistl.API.IKistlContext ctx = GetContext())
            {
                var obj = ctx.Create<Kistl.App.Test.TestCustomObject>();
                obj.PersonName = "TestPerson " + rnd.Next();
                obj.Birthday = DateTime.Now;

                Assert.That(obj.PhoneNumberMobile, Is.Null);
                Assert.That(obj.PhoneNumberOffice, Is.Not.Null);

                obj.PhoneNumberOffice.AreaCode = "2";
                obj.PhoneNumberOffice.Number = number;

                Assert.That(ctx.SubmitChanges(), Is.EqualTo(1));

                ID = obj.ID;
            }

            using (Kistl.API.IKistlContext ctx = GetContext())
            {
                var obj = ctx.Find<Kistl.App.Test.TestCustomObject>(ID);
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.PhoneNumberMobile, Is.Null);
                Assert.That(obj.PhoneNumberOffice, Is.Not.Null);
                Assert.That(obj.PhoneNumberOffice.Number, Is.EqualTo(number));
            }
        }

        [Test]
        public void should_saveobject()
        {
            int ID = Kistl.API.Helper.INVALIDID;

            using (var ctx = GetContext())
            {
                var obj = ctx.GetQuery<TestCustomObject>().First();

                obj.PersonName = "TestPerson " + rnd.Next();
                obj.Birthday = DateTime.Now;

                obj.PhoneNumberMobile.AreaCode = number + "am";
                obj.PhoneNumberMobile.Number = number + "nm";

                obj.PhoneNumberOffice.AreaCode = number + "ao";
                obj.PhoneNumberOffice.Number = number + "no";

                Assert.That(ctx.SubmitChanges(), Is.GreaterThan(0), "no changes were submitted");

                ID = obj.ID;
            }

            using (var ctx = GetContext())
            {
                var obj = ctx.Find<TestCustomObject>(ID);
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.PhoneNumberMobile, Is.Not.Null);
                Assert.That(obj.PhoneNumberOffice, Is.Not.Null);
                Assert.That(obj.PhoneNumberMobile.AreaCode, Is.EqualTo(number + "am"));
                Assert.That(obj.PhoneNumberOffice.AreaCode, Is.EqualTo(number + "ao"));
                Assert.That(obj.PhoneNumberMobile.Number, Is.EqualTo(number + "nm"));
                Assert.That(obj.PhoneNumberOffice.Number, Is.EqualTo(number + "no"));
            }
        }

        [Test]
        public void should_save_nullable()
        {
            Assert.That(obj.PhoneNumberMobile, Is.Not.Null);
            obj.PhoneNumberMobile = null;
            Assert.That(obj.PhoneNumberMobile, Is.Null);
            ctx.SubmitChanges();
            Assert.That(obj.PhoneNumberMobile, Is.Null);

            using (var testCtx = GetContext())
            {
                var testObj = testCtx.GetQuery<TestCustomObject>().First(i => i.ID == obj.ID);
                Assert.That(testObj.ID == obj.ID);
                Assert.That(testObj.PhoneNumberMobile, Is.Null);
            }
        }
    }
}
