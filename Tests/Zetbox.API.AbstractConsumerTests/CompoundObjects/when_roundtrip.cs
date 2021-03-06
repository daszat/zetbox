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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Zetbox.App.Test;

namespace Zetbox.API.AbstractConsumerTests.CompoundObjects
{
    public abstract class when_roundtrip : CompoundObjectFixture
    {
        [Test]
        public void should_getobject()
        {
            using (IZetboxContext ctx = GetContext())
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
            int ID = Zetbox.API.Helper.INVALIDID;
            using (Zetbox.API.IZetboxContext ctx = GetContext())
            {
                var obj = ctx.Create<Zetbox.App.Test.TestCustomObject>();
                obj.PersonName = "TestPerson " + rnd.Next();
                obj.Birthday = DateTime.Now;

                Assert.That(obj.PhoneNumberMobile, Is.Not.Null);
                Assert.That(obj.PhoneNumberOffice, Is.Not.Null);

                obj.PhoneNumberMobile = ctx.CreateCompoundObject<TestPhoneCompoundObject>();
                obj.PhoneNumberMobile.AreaCode = "1";
                obj.PhoneNumberMobile.Number = testNumber;

                obj.PhoneNumberOffice.AreaCode = "1";
                obj.PhoneNumberOffice.Number = testNumber;

                Assert.That(ctx.SubmitChanges(), Is.EqualTo(1));

                ID = obj.ID;
            }

            using (Zetbox.API.IZetboxContext ctx = GetContext())
            {
                var obj = ctx.Find<Zetbox.App.Test.TestCustomObject>(ID);
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.PhoneNumberMobile, Is.Not.Null);
                Assert.That(obj.PhoneNumberOffice, Is.Not.Null);
                Assert.That(obj.PhoneNumberMobile.Number, Is.EqualTo(testNumber));
                Assert.That(obj.PhoneNumberOffice.Number, Is.EqualTo(testNumber));
            }
        }

        [Test]
        public void should_createobject_nullable()
        {
            int ID = Zetbox.API.Helper.INVALIDID;
            using (Zetbox.API.IZetboxContext ctx = GetContext())
            {
                var obj = ctx.Create<Zetbox.App.Test.TestCustomObject>();
                obj.PersonName = "TestPerson " + rnd.Next();
                obj.Birthday = DateTime.Now;

                Assert.That(obj.PhoneNumberMobile, Is.Not.Null);
                Assert.That(obj.PhoneNumberOffice, Is.Not.Null);

                obj.PhoneNumberOffice.AreaCode = "2";
                obj.PhoneNumberOffice.Number = testNumber;

                Assert.That(ctx.SubmitChanges(), Is.EqualTo(1));

                ID = obj.ID;
            }

            using (Zetbox.API.IZetboxContext ctx = GetContext())
            {
                var obj = ctx.Find<Zetbox.App.Test.TestCustomObject>(ID);
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.PhoneNumberMobile, Is.Not.Null);
                Assert.That(obj.PhoneNumberOffice, Is.Not.Null);
                Assert.That(obj.PhoneNumberOffice.Number, Is.EqualTo(testNumber));
            }
        }

        [Test]
        public void should_saveobject()
        {
            int ID = Zetbox.API.Helper.INVALIDID;

            using (var ctx = GetContext())
            {
                var obj = ctx.GetQuery<TestCustomObject>().First();

                obj.PersonName = "TestPerson " + rnd.Next();
                obj.Birthday = DateTime.Now;

                obj.PhoneNumberMobile.AreaCode = testNumber + "am";
                obj.PhoneNumberMobile.Number = testNumber + "nm";

                obj.PhoneNumberOffice.AreaCode = testNumber + "ao";
                obj.PhoneNumberOffice.Number = testNumber + "no";

                Assert.That(ctx.SubmitChanges(), Is.GreaterThan(0), "no changes were submitted");

                ID = obj.ID;
            }

            using (var ctx = GetContext())
            {
                var obj = ctx.Find<TestCustomObject>(ID);
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.PhoneNumberMobile, Is.Not.Null);
                Assert.That(obj.PhoneNumberOffice, Is.Not.Null);
                Assert.That(obj.PhoneNumberMobile.AreaCode, Is.EqualTo(testNumber + "am"));
                Assert.That(obj.PhoneNumberOffice.AreaCode, Is.EqualTo(testNumber + "ao"));
                Assert.That(obj.PhoneNumberMobile.Number, Is.EqualTo(testNumber + "nm"));
                Assert.That(obj.PhoneNumberOffice.Number, Is.EqualTo(testNumber + "no"));
            }
        }

        [Test]
        public void should_add_to_list()
        {
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            Assert.That(obj.PhoneNumbersOther.Count, Is.GreaterThanOrEqualTo(MINLISTCOUNT));
            int count = obj.PhoneNumbersOther.Count;

            var c = ctx.CreateCompoundObject<TestPhoneCompoundObject>();
            c.AreaCode = "123";
            c.Number = testNumber;
            obj.PhoneNumbersOther.Add(c);

            Assert.That(obj.PhoneNumbersOther.Count, Is.EqualTo(count + 1));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Modified));

            Assert.That(ctx.SubmitChanges(), Is.GreaterThan(0));

            Assert.That(obj.PhoneNumbersOther.Count, Is.EqualTo(count + 1));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));

            using (var testCtx = GetContext())
            {
                var testObj = testCtx.GetQuery<TestCustomObject>().First(i => i.ID == obj.ID);
                Assert.That(testObj.ID == obj.ID);
                Assert.That(obj.PhoneNumbersOther.Count, Is.EqualTo(count + 1));
                Assert.That(obj.PhoneNumbersOther.Count(i => i.Number == testNumber), Is.EqualTo(1));
            }
        }

        [Test]
        public void should_remove_from_list()
        {
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            Assert.That(obj.PhoneNumbersOther.Count, Is.GreaterThanOrEqualTo(MINLISTCOUNT));
            int count = obj.PhoneNumbersOther.Count;

            var c = obj.PhoneNumbersOther.Skip(2).First();
            obj.PhoneNumbersOther.Remove(c);

            Assert.That(obj.PhoneNumbersOther.Count, Is.EqualTo(count - 1));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Modified));

            Assert.That(ctx.SubmitChanges(), Is.GreaterThan(0));

            Assert.That(obj.PhoneNumbersOther.Count, Is.EqualTo(count - 1));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));

            using (var testCtx = GetContext())
            {
                var testObj = testCtx.GetQuery<TestCustomObject>().First(i => i.ID == obj.ID);
                Assert.That(testObj.ID == obj.ID);
                Assert.That(obj.PhoneNumbersOther.Count, Is.EqualTo(count - 1));
                Assert.That(obj.PhoneNumbersOther.Count(i => i.Number == testNumber), Is.EqualTo(0));
            }
        }

        [Test]
        [Ignore("Tests weakly defined semantics")]
        public void should_change_a_list()
        {
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            Assert.That(obj.PhoneNumbersOther.Count, Is.GreaterThanOrEqualTo(MINLISTCOUNT));
            int count = obj.PhoneNumbersOther.Count;

            var c = obj.PhoneNumbersOther.Skip(2).First();
            c.Number = testNumber;

            Assert.That(obj.PhoneNumbersOther.Count, Is.EqualTo(count));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified)); // TODO: should this be Modified???

            Assert.That(ctx.SubmitChanges(), Is.GreaterThan(0));

            Assert.That(obj.PhoneNumbersOther.Count, Is.EqualTo(count));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));

            using (var testCtx = GetContext())
            {
                var testObj = testCtx.GetQuery<TestCustomObject>().First(i => i.ID == obj.ID);
                Assert.That(testObj.ID == obj.ID);
                Assert.That(obj.PhoneNumbersOther.Count, Is.EqualTo(count));
                Assert.That(obj.PhoneNumbersOther.Count(i => i.Number == testNumber), Is.EqualTo(1));
            }
        }
    }
}
