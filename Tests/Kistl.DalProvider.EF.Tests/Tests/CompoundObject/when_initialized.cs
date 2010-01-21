using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;

using NUnit.Framework;
using Kistl.App.Test;

namespace Kistl.DalProvider.EF.Tests.CompoundObjects
{
    [TestFixture]
    public class when_initialized
        : Kistl.API.AbstractConsumerTests.CompoundObjects.when_initialized
    {
        public override Kistl.App.Test.TestCustomObject GetObject()
        {
            using (IKistlContext ctx = GetContext())
            {
                var obj = ctx.Create<TestCustomObject>();
                obj.Birthday = DateTime.Today;
                obj.PersonName = "Person";

                obj.PhoneNumberMobile.AreaCode = "43 664";
                obj.PhoneNumberMobile.Number = "12345";
                obj.PhoneNumberOffice.AreaCode = "43 1";
                obj.PhoneNumberOffice.Number = "12345";

                ctx.SubmitChanges();
            }
            return base.GetObject();
        }

        public override void DisposeContext()
        {
            using (IKistlContext ctx = GetContext())
            {
                ctx.GetQuery<TestCustomObject>().ForEach(obj => ctx.Delete(obj));
                ctx.SubmitChanges();
            }

            base.DisposeContext();
        }

        public override IKistlContext GetContext()
        {
            return KistlContext.GetContext();
        }
    }
}
