using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Test;

using NUnit.Framework;

namespace Kistl.API.AbstractConsumerTests.CompoundObjects
{

    public abstract class CompoundObjectFixture
    {
        public abstract IKistlContext GetContext();


        protected IKistlContext ctx;
        protected TestCustomObject obj;

        [SetUp]
        public void InitTestObjects()
        {
            DeleteTestData();
            CreateTestData();

            ctx = GetContext();
            obj = ctx.GetQuery<TestCustomObject>().First();
        }

        protected virtual void DeleteTestData()
        {
            using (IKistlContext ctx = GetContext())
            {
                ctx.GetQuery<TestCustomObject>().ForEach(obj => ctx.Delete(obj));
                ctx.SubmitChanges();
            }
        }

        protected virtual void CreateTestData()
        {
            using (IKistlContext ctx = GetContext())
            {
                TestCustomObject i;

                i = ctx.Create<TestCustomObject>();
                i.Birthday = DateTime.Now;
                i.PersonName = "First Person";
                i.PhoneNumberMobile = ctx.CreateCompoundObject<TestPhoneCompoundObject>();
                i.PhoneNumberMobile.AreaCode = "1";
                i.PhoneNumberMobile.Number = "11111111";
                i.PhoneNumberOffice.AreaCode = "o1";
                i.PhoneNumberOffice.Number = "o11111111";

                i = ctx.Create<TestCustomObject>();
                i.Birthday = DateTime.Now;
                i.PersonName = "Second Person";
                i.PhoneNumberMobile = ctx.CreateCompoundObject<TestPhoneCompoundObject>();
                i.PhoneNumberMobile.AreaCode = "2";
                i.PhoneNumberMobile.Number = "22222222";
                i.PhoneNumberOffice.AreaCode = "o1";
                i.PhoneNumberOffice.Number = "o2222222";

                i = ctx.Create<TestCustomObject>();
                i.Birthday = DateTime.Now;
                i.PersonName = "Third Person";
                i.PhoneNumberMobile = ctx.CreateCompoundObject<TestPhoneCompoundObject>();
                i.PhoneNumberMobile.AreaCode = "3";
                i.PhoneNumberMobile.Number = "3333333";
                i.PhoneNumberOffice.AreaCode = "o3";
                i.PhoneNumberOffice.Number = "o3333333";

                ctx.SubmitChanges();
            }
        }

        [TearDown]
        public virtual void DisposeContext()
        {
            ctx.Dispose();
        }
    }

}
