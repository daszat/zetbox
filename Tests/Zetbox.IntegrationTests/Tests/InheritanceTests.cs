using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Zetbox.API.Client;
using Zetbox.Client;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Zetbox.IntegrationTests
{
    [TestFixture]
    public class InheritanceTests : AbstractIntegrationTestFixture
    {
        [Test]
        public void GetListOfInheritedObjects()
        {
            using (Zetbox.API.IZetboxContext ctx = GetContext())
            {
                bool intFound = false;
                bool stringFound = false;

                var list = ctx.GetQuery<Zetbox.App.Base.Property>().ToList();
                Assert.That(list.Count, Is.GreaterThan(0));

                foreach (Zetbox.App.Base.Property bp in list)
                {
                    if (bp is Zetbox.App.Base.IntProperty)
                    {
                        intFound = true;
                    }
                    if (bp is Zetbox.App.Base.StringProperty)
                    {
                        stringFound = true;
                    }
                }

                Assert.That(intFound, Is.True);
                Assert.That(stringFound, Is.True);
            }
        }

        [Test]
        [Ignore("no test class available")]
        public void UpdateInheritedObject()
        {
            //int ID = Zetbox.API.Helper.INVALIDID;
            //double? maxStungen = 0.0;

            //using (Zetbox.API.IZetboxContext ctx = GetContext())
            //{
            //    var list = ctx.GetQuery<Zetbox.App.TimeRecords.Kostentraeger>().ToList();
            //    Assert.That(list.Count, Is.GreaterThan(0));

            //    Zetbox.App.TimeRecords.Kostentraeger k = list[0];
            //    ID = k.ID;
            //    maxStungen = k.BudgetHours = k.BudgetHours.HasValue ? k.BudgetHours.Value + 1.0 : 1.0;

            //    ctx.SubmitChanges();
            //}

            //using (Zetbox.API.IZetboxContext ctx = GetContext())
            //{
            //    var k = ctx.GetQuery<Zetbox.App.TimeRecords.Kostentraeger>().First(o => o.ID == ID);
            //    Assert.That(k, Is.Not.Null);
            //    Assert.That(k.BudgetHours.HasValue, Is.True);
            //    Assert.That(k.BudgetHours.Value, Is.EqualTo(maxStungen));
            //}
        }
    }
}
