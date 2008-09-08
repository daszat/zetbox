using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.Client;
using Kistl.API.Client;

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
        [Ignore]
        public void CreateObjectWithStruct()
        {
            int ID = Kistl.API.Helper.INVALIDID;
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var obj = ctx.Create<Kistl.App.Test.TestCustomObject>();
                obj.PersonName = "TestPerson " + rnd.Next();
                obj.Birthday = DateTime.Now;

                Assert.That(obj.PhoneNumberMobile, Is.Not.Null);
                Assert.That(obj.PhoneNumberOffice, Is.Not.Null);

                //obj.PhoneNumberMobile = new Kistl.App.Test.TestPhoneStruct() { AreaCode = "1", Number = number };
                //obj.PhoneNumberOffice = new Kistl.App.Test.TestPhoneStruct() { AreaCode = "1", Number = number };

                Assert.That(ctx.SubmitChanges(), Is.EqualTo(1));

                ID = obj.ID;
            }

            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
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
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var objList = ctx.GetQuery<Kistl.App.Test.TestCustomObject>();
                foreach (var obj in objList)
                {
                    Assert.That(obj.ID, Is.GreaterThan(Kistl.API.Helper.INVALIDID));
                }
            }
        }

        [Test]
        [Ignore]
        public void SaveObjectWithStruct()
        {
            int ID = Kistl.API.Helper.INVALIDID;
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var objList = ctx.GetQuery<Kistl.App.Test.TestCustomObject>();
                var obj = objList.First();
                ID = obj.ID;

                //obj.PhoneNumberMobile = new Kistl.App.Test.TestPhoneStruct() { AreaCode = "1", Number = number };
                //obj.PhoneNumberOffice = new Kistl.App.Test.TestPhoneStruct() { AreaCode = "1", Number = number };

                Assert.That(ctx.SubmitChanges(), Is.EqualTo(1));
            }
            
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var obj = ctx.Find<Kistl.App.Test.TestCustomObject>(ID);
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.PhoneNumberMobile, Is.Not.Null);
                Assert.That(obj.PhoneNumberOffice, Is.Not.Null);
                Assert.That(obj.PhoneNumberMobile.Number, Is.EqualTo(number));
                Assert.That(obj.PhoneNumberOffice.Number, Is.EqualTo(number));
            }
        }

        /// <summary>
        /// Tests if setting a Structmember directy does _not_ change the value.
        /// </summary>
        [Test]
        public void ChangeObjectWithStruct()
        {
            int ID = Kistl.API.Helper.INVALIDID;
            string oldNumber;

            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var objList = ctx.GetQuery<Kistl.App.Test.TestCustomObject>();
                Kistl.App.Test.TestCustomObject testObject = null;
                foreach (var obj in objList)
                {
                    if (obj.PhoneNumberMobile != null && obj.PhoneNumberOffice != null)
                    {
                        testObject = obj;
                        break;
                    }
                }

                Assert.That(testObject, Is.Not.Null);

                oldNumber = testObject.PhoneNumberOffice.Number;

                testObject.PhoneNumberOffice.Number = number;
                testObject.PhoneNumberMobile.Number = number;

                Assert.That(testObject.PhoneNumberMobile.Number, Is.EqualTo(oldNumber));
                Assert.That(testObject.PhoneNumberOffice.Number, Is.EqualTo(oldNumber));

                Assert.That(ctx.SubmitChanges(), Is.EqualTo(0));

                ID = testObject.ID;
            }

            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var obj = ctx.Find<Kistl.App.Test.TestCustomObject>(ID);
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.PhoneNumberMobile, Is.Not.Null);
                Assert.That(obj.PhoneNumberOffice, Is.Not.Null);
                Assert.That(obj.PhoneNumberMobile.Number, Is.EqualTo(oldNumber));
                Assert.That(obj.PhoneNumberOffice.Number, Is.EqualTo(oldNumber));
            }
        }

        [Test]
        public void SetStructNull()
        {
            int ID = Kistl.API.Helper.INVALIDID;
            string number = new Random().Next().ToString();

            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var objList = ctx.GetQuery<Kistl.App.Test.TestCustomObject>();
                Kistl.App.Test.TestCustomObject testObject = null;
                foreach (var obj in objList)
                {
                    if (obj.PhoneNumberMobile != null && obj.PhoneNumberOffice != null)
                    {
                        testObject = obj;
                        break;
                    }
                }

                Assert.That(testObject, Is.Not.Null);
                testObject.PhoneNumberOffice = null;
                testObject.PhoneNumberMobile = null;

                Assert.That(ctx.SubmitChanges(), Is.EqualTo(1));

                ID = testObject.ID;
            }

            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var obj = ctx.Find<Kistl.App.Test.TestCustomObject>(ID);
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.PhoneNumberMobile.Number, Is.Null);
                Assert.That(obj.PhoneNumberOffice.Number, Is.Null);
            }
        }
    }
}
