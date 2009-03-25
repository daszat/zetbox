using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Test;
using Kistl.Client;

using NUnit.Framework;

namespace Kistl.IntegrationTests
{
    [TestFixture]
    public class StructTests
    {
        Random rnd = new Random();
        string number;

        [SetUp]
        public void SetUp()
        {
            number = rnd.Next().ToString();
        }

        
        [Test]
        public void structs_should_always_be_initialised()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var tco = ctx.GetQuery<TestCustomObject>().FirstOrDefault();
                Assert.That(tco.PhoneNumberMobile, Is.Not.Null, "structs should be initialised");
            }
        }
	

        [Test]
        public void CreateObjectWithStruct()
        {
            int ID = Kistl.API.Helper.INVALIDID;
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var obj = ctx.Create<TestCustomObject>();
                obj.PersonName = "TestPerson " + rnd.Next();
                obj.Birthday = DateTime.Now;

                Assert.That(obj.PhoneNumberMobile, Is.Null);
                Assert.That(obj.PhoneNumberOffice, Is.Null);

                obj.PhoneNumberMobile.AreaCode = "1";
                obj.PhoneNumberMobile.Number = number;

                obj.PhoneNumberOffice.AreaCode = "1";
                obj.PhoneNumberOffice.Number = number;

                Assert.That(ctx.SubmitChanges(), Is.GreaterThan(0), "no changes were submitted");

                ID = obj.ID;
            }

            using (IKistlContext ctx = KistlContext.GetContext())
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
        public void GetObjectWithStruct()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var objList = ctx.GetQuery<TestCustomObject>();
                foreach (var obj in objList)
                {
                    Assert.That(obj.ID, Is.GreaterThan(Helper.INVALIDID), "received object with invalid id");
                }
            }
        }

        [Test]
        public void SaveObjectWithStruct()
        {
            int ID = Kistl.API.Helper.INVALIDID;
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var objList = ctx.GetQuery<TestCustomObject>();
                var obj = objList.First();
                ID = obj.ID;

                obj.PhoneNumberMobile.AreaCode = "1";
                obj.PhoneNumberMobile.Number = number;

                obj.PhoneNumberOffice.AreaCode = "1";
                obj.PhoneNumberOffice.Number = number;

                Assert.That(ctx.SubmitChanges(), Is.GreaterThan(0), "no changes were submitted");
            }
            
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var obj = ctx.Find<TestCustomObject>(ID);
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.PhoneNumberMobile, Is.Not.Null);
                Assert.That(obj.PhoneNumberOffice, Is.Not.Null);
                Assert.That(obj.PhoneNumberMobile.Number, Is.EqualTo(number));
                Assert.That(obj.PhoneNumberOffice.Number, Is.EqualTo(number));
            }
        }

        /// <summary>
        /// Tests if setting a Structmember directly does _not_ change the value.
        /// </summary>
        [Test]
        public void ChangeObjectWithStruct()
        {
            int ID = Kistl.API.Helper.INVALIDID;
            string oldNumber;

            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var testObject = ctx.GetQuery<TestCustomObject>()
                    .ToList()
                    .FirstOrDefault(obj => obj.PhoneNumberMobile != null && obj.PhoneNumberOffice != null);

                Assume.That(testObject, Is.Not.Null);

                oldNumber = testObject.PhoneNumberOffice.Number;

                testObject.PhoneNumberOffice.Number = number;
                testObject.PhoneNumberMobile.Number = number;

                Assert.That(testObject.PhoneNumberMobile.Number, Is.EqualTo(number));
                Assert.That(testObject.PhoneNumberOffice.Number, Is.EqualTo(number));

                Assert.That(ctx.SubmitChanges(), Is.GreaterThan(0), "no changes were submitted");

                ID = testObject.ID;
            }

            using (IKistlContext ctx = KistlContext.GetContext())
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
        [Ignore("Nicht fertig besprochen")]
        public void SetStructNull()
        {
            int ID = Kistl.API.Helper.INVALIDID;
            string number = new Random().Next().ToString();

            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var objList = ctx.GetQuery<TestCustomObject>();
                TestCustomObject testObject = null;
                foreach (var obj in objList)
                {
                    if (obj.PhoneNumberMobile != null && obj.PhoneNumberOffice != null)
                    {
                        testObject = obj;
                        break;
                    }
                }

                Assert.That(testObject, Is.Not.Null);

                Assert.That(ctx.SubmitChanges(), Is.GreaterThan(0), "no changes were submitted");

                ID = testObject.ID;
            }

            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var obj = ctx.Find<TestCustomObject>(ID);
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.PhoneNumberMobile, Is.Null);
                Assert.That(obj.PhoneNumberOffice, Is.Null);
            }
        }
    }
}
