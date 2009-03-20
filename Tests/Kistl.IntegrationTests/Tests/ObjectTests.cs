using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Base;
using Kistl.App.Projekte;
using Kistl.Client;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Kistl.IntegrationTests
{
    [TestFixture]
    public class ObjectTests
    {
        [SetUp]
        public void SetUp()
        {
            //CacheController<Kistl.API.IDataObject>.Current.Clear();
        }

        [Test]
        public void GetObject()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var obj = ctx.GetQuery<ObjectClass>().Single(o => o.ID == 2);
                Assert.That(obj.ID, Is.EqualTo(2));
            }
        }

        [Test]
        public void GetObject_Twice()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var obj1 = ctx.GetQuery<ObjectClass>().Single(o => o.ID == 2);
                Assert.That(obj1.ID, Is.EqualTo(2));

                var obj2 = ctx.GetQuery<ObjectClass>().Single(o => o.ID == 2);
                Assert.That(obj2.ID, Is.EqualTo(2));

                Assert.That(object.ReferenceEquals(obj1, obj2), "Obj1 & Obj2 are different Objects");
            }
        }


        [Test]
        public void GetListOf()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var list = ctx.GetQuery<Projekt>();
                int count = 0;
                foreach (Projekt prj in list)
                {
                    count += prj.Tasks.Count;
                }
                Assert.That(count, Is.GreaterThan(0));
            }
        }

        [Test]
        public void GetListOf_List()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var list = ctx.GetQuery<Mitarbeiter>();
                int count = 0;
                foreach (Mitarbeiter ma in list)
                {
                    count += ma.Projekte.Count;
                }
                Assert.That(count, Is.GreaterThan(0));
            }
        }

        [Test]
        public void SetObject()
        {
            double aufwand;
            int ID;
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var list = ctx.GetQuery<Kistl.App.Projekte.Task>().ToList();
                Assert.That(list.Count, Is.GreaterThan(0));
                var obj = list[0];

                ID = obj.ID;
                aufwand = (obj.Aufwand ?? 0.0) + 1.0;

                obj.Aufwand = aufwand;

                ctx.SubmitChanges();
            }

            //CacheController<Kistl.API.IDataObject>.Current.Clear();

            using (IKistlContext checkctx = KistlContext.GetContext())
            {
                var obj = checkctx.GetQuery<Task>().Single(o => o.ID == ID);
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.Aufwand, Is.EqualTo(aufwand));
            }
        }

        [Test]
        public void NewObject()
        {
            int ID;
            double aufwand = 1.0;
            DateTime datum = DateTime.Now;
            Projekt p;
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                p = ctx.GetQuery<Projekt>().ToList()[0];
                var obj = ctx.Create<Task>();

                obj.Name = "NUnit Test Task";
                obj.Aufwand = aufwand;
                obj.DatumVon = datum;
                obj.DatumBis = datum.AddDays(1);
                obj.Projekt = p;

                ctx.SubmitChanges();
                ID = obj.ID;
                Assert.That(ID, Is.Not.EqualTo(Kistl.API.Helper.INVALIDID));
            }

            //CacheController<Kistl.API.IDataObject>.Current.Clear();

            using (IKistlContext checkctx = KistlContext.GetContext())
            {
                var obj = checkctx.GetQuery<Task>().Single(o => o.ID == ID);
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.Aufwand, Is.EqualTo(aufwand));
                Assert.That(obj.Projekt.ID, Is.EqualTo(p.ID));
            }
        }
    }
}
