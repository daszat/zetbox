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

        public delegate TestCustomObject Factory(IKistlContext ctx);
        public class FactoryDescriptor
        {
            public Factory Factory { get; set; }
            public string Description { get; set; }
            public override string ToString()
            {
                return Description;
            }
        }


        [Datapoint]
        public static readonly FactoryDescriptor loadItem = new FactoryDescriptor()
        {
            Factory = delegate(IKistlContext ctx) { return ctx.GetQuery<TestCustomObject>().First(); },
            Description = "Load item from Context"
        };
        [Datapoint]
        public static readonly FactoryDescriptor createItem = new FactoryDescriptor()
        {
            Factory = delegate(IKistlContext ctx) { return ctx.Create<TestCustomObject>(); },
            Description = "Create new item"
        };

        [Theory]
        public void SaveObjectWithStruct(FactoryDescriptor fact)
        {
            int ID = Kistl.API.Helper.INVALIDID;

            using (var ctx = KistlContext.GetContext())
            {
                var obj = fact.Factory(ctx);

                obj.PersonName = "TestPerson " + rnd.Next();
                obj.Birthday = DateTime.Now;

                obj.PhoneNumberMobile.AreaCode = number + "am";
                obj.PhoneNumberMobile.Number = number + "nm";

                obj.PhoneNumberOffice.AreaCode = number + "ao";
                obj.PhoneNumberOffice.Number = number + "no";

                Assert.That(ctx.SubmitChanges(), Is.GreaterThan(0), "no changes were submitted");

                ID = obj.ID;
            }

            using (var ctx = KistlContext.GetContext())
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
    }
}
