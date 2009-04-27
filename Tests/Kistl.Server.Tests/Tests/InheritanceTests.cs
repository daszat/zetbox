using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API.Server;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Kistl.Server.Tests
{
    [TestFixture]
    public class InheritanceTests
    {
        [SetUp]
        public void SetUp()
        {
            //CacheController<Kistl.API.IDataObject>.Current.Clear();
        }

        [Test]
        public void GetListOfInheritedObjects()
        {
            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            {
                bool intFound = false;
                bool stringFound = false;

                var list = ctx.GetQuery<Kistl.App.Base.Property>().ToList();
                Assert.That(list.Count, Is.GreaterThan(0));

                foreach (Kistl.App.Base.Property bp in list)
                {
                    if (bp is Kistl.App.Base.IntProperty)
                    {
                        intFound = true;
                    }
                    if (bp is Kistl.App.Base.StringProperty)
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
            //int ID = Kistl.API.Helper.INVALIDID;
            //double? maxStungen = 0.0;

            //using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            //{
            //    var list = ctx.GetQuery<Kistl.App.Zeiterfassung.Kostentraeger>().ToList();
            //    Assert.That(list.Count, Is.GreaterThan(0));

            //    Kistl.App.Zeiterfassung.Kostentraeger k = list[0];
            //    ID = k.ID;
            //    maxStungen = k.MaxStunden = k.MaxStunden.HasValue ? k.MaxStunden.Value + 1.0 : 1.0;

            //    ctx.SubmitChanges();
            //}

            //using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            //{
            //    var k = ctx.GetQuery<Kistl.App.Zeiterfassung.Kostentraeger>().First(o => o.ID == ID);
            //    Assert.That(k, Is.Not.Null);
            //    Assert.That(k.MaxStunden.HasValue, Is.True);
            //    Assert.That(k.MaxStunden.Value, Is.EqualTo(maxStungen));
            //}
        }
    }
}
