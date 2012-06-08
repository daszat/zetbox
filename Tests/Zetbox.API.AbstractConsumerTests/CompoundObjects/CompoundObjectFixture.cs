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

namespace Zetbox.API.AbstractConsumerTests.CompoundObjects
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Test;
    using NUnit.Framework;

    public abstract class CompoundObjectFixture
        : AbstractTestFixture
    {
        protected const int MINLISTCOUNT = 5;
        protected const string TEST_MOBILE_NUMBER = "m123456";
        protected Random rnd = new Random();
        protected string testNumber;
        protected IZetboxContext ctx;
        protected TestCustomObject obj;

        [SetUp]
        public void InitTestObjects()
        {
            DeleteTestData();
            CreateTestData();

            ctx = GetContext();
            obj = ctx.GetQuery<TestCustomObject>().First();

            testNumber = "TestNumber " + rnd.NextDouble().ToString(CultureInfo.InvariantCulture);
        }

        protected virtual void DeleteTestData()
        {
            using (IZetboxContext ctx = GetContext())
            {
                ctx.GetQuery<TestCustomObject>().ForEach(obj => ctx.Delete(obj));
                ctx.SubmitChanges();
            }
        }

        protected virtual void CreateTestData()
        {
            using (IZetboxContext create_ctx = GetContext())
            {
                TestCustomObject create_obj;

                create_obj = create_ctx.Create<TestCustomObject>();
                create_obj.Birthday = DateTime.Now;
                create_obj.PersonName = "First Person";
                create_obj.PhoneNumberMobile = create_ctx.CreateCompoundObject<TestPhoneCompoundObject>();
                create_obj.PhoneNumberMobile.AreaCode = "1";
                create_obj.PhoneNumberMobile.Number = "11111111";
                create_obj.PhoneNumberOffice.AreaCode = "o1";
                create_obj.PhoneNumberOffice.Number = "o11111111";
                AddPhoneNumberOther(create_ctx, create_obj, MINLISTCOUNT);

                create_obj = create_ctx.Create<TestCustomObject>();
                create_obj.Birthday = DateTime.Now;
                create_obj.PersonName = "Second Person";
                create_obj.PhoneNumberMobile = create_ctx.CreateCompoundObject<TestPhoneCompoundObject>();
                create_obj.PhoneNumberMobile.AreaCode = "2";
                create_obj.PhoneNumberMobile.Number = TEST_MOBILE_NUMBER;
                create_obj.PhoneNumberOffice.AreaCode = "o1";
                create_obj.PhoneNumberOffice.Number = "o2222222";
                AddPhoneNumberOther(create_ctx, create_obj, MINLISTCOUNT + 5);

                create_obj = create_ctx.Create<TestCustomObject>();
                create_obj.Birthday = DateTime.Now;
                create_obj.PersonName = "No Mobile";
                create_obj.PhoneNumberOffice.AreaCode = "o1";
                create_obj.PhoneNumberOffice.Number = "o2222222";
                AddPhoneNumberOther(create_ctx, create_obj, MINLISTCOUNT + 2);

                create_obj = create_ctx.Create<TestCustomObject>();
                create_obj.Birthday = DateTime.Now;
                create_obj.PersonName = "Third Person";
                create_obj.PhoneNumberMobile = create_ctx.CreateCompoundObject<TestPhoneCompoundObject>();
                create_obj.PhoneNumberMobile.AreaCode = "3";
                create_obj.PhoneNumberMobile.Number = "3333333";
                create_obj.PhoneNumberOffice.AreaCode = "o3";
                create_obj.PhoneNumberOffice.Number = "o3333333";
                AddPhoneNumberOther(create_ctx, create_obj, MINLISTCOUNT + 10);

                create_ctx.SubmitChanges();
            }
        }

        private void AddPhoneNumberOther(IZetboxContext create_ctx, TestCustomObject obj, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var c = create_ctx.CreateCompoundObject<TestPhoneCompoundObject>();
                c.AreaCode = "123";
                c.Number = rnd.Next(int.MaxValue).ToString();
                obj.PhoneNumbersOther.Add(c);
            }
        }

        [TearDown]
        public virtual void ForgetContext()
        {
            ctx = null;
            obj = null;
        }
    }

}
