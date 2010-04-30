using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autofac;

using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Kistl.Server.Tests
{
    [TestFixture]
    public class InheritanceTests
    {
        private ILifetimeScope container;
        private IReadOnlyKistlContext ctx;

        [SetUp]
        public void SetUp()
        {
            //CacheController<Kistl.API.IDataObject>.Current.Clear();
            container = Kistl.Server.Tests.SetUp.CreateInnerContainer();
            ctx = container.Resolve<IReadOnlyKistlContext>();
        }

        [TearDown]
        public void TearDown()
        {
            if (container != null)
            {
                container.Dispose();
                container = null;
            }
        }

        [Test]
        public void GetListOfInheritedObjects()
        {
            bool intFound = false;
            bool stringFound = false;

            var list = ctx.GetQuery<Property>().ToList();
            Assert.That(list.Count, Is.GreaterThan(0));

            foreach (Property bp in list)
            {
                if (bp is IntProperty)
                {
                    intFound = true;
                }
                if (bp is StringProperty)
                {
                    stringFound = true;
                }
            }

            Assert.That(intFound, Is.True);
            Assert.That(stringFound, Is.True);
        }

        [Test]
        [Ignore("no test class available")]
        public void UpdateInheritedObject()
        {
            //int ID = Kistl.API.Helper.INVALIDID;
            //double? maxStungen = 0.0;

            //using (Kistl.API.IKistlContext ctx = GetContext())
            //{
            //    var list = ctx.GetQuery<Kistl.App.TimeRecords.Kostentraeger>().ToList();
            //    Assert.That(list.Count, Is.GreaterThan(0));

            //    Kistl.App.TimeRecords.Kostentraeger k = list[0];
            //    ID = k.ID;
            //    maxStungen = k.BudgetHours = k.BudgetHours.HasValue ? k.BudgetHours.Value + 1.0 : 1.0;

            //    ctx.SubmitChanges();
            //}

            //using (Kistl.API.IKistlContext ctx = GetContext())
            //{
            //    var k = ctx.GetQuery<Kistl.App.TimeRecords.Kostentraeger>().First(o => o.ID == ID);
            //    Assert.That(k, Is.Not.Null);
            //    Assert.That(k.BudgetHours.HasValue, Is.True);
            //    Assert.That(k.BudgetHours.Value, Is.EqualTo(maxStungen));
            //}
        }
    }
}
