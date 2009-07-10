using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Test;

using NUnit.Framework;
using Kistl.API.Client;

namespace Kistl.IntegrationTests.Structs
{

    [TestFixture]
    [Ignore("To be implemented")]    
    public class when_changing_a_struct_member
        : Kistl.API.AbstractConsumerTests.Structs.when_changing_a_struct_member
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
