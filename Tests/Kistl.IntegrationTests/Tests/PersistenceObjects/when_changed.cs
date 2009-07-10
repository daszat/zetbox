using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;

using NUnit.Framework;
using Kistl.App.Test;

namespace Kistl.IntegrationTests.PersistenceObjects
{

    [TestFixture]
    public class when_changed
        : Kistl.API.AbstractConsumerTests.PersistenceObjects.when_changed
    {
        public override Kistl.App.Test.TestCustomObject GetObject()
        {
            using (IKistlContext ctx = GetContext())
            {
                var obj = ctx.Create<TestCustomObject>();
                obj.Birthday = DateTime.Today;
                obj.PersonName = "Person";

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
