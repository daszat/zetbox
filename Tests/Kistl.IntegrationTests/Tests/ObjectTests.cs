using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.Client;
using Kistl.API.Client;

namespace Integration.Tests.Tests
{
    [TestFixture]
    public class ObjectTests
    {
        [SetUp]
        public void SetUp()
        {
            CacheController<Kistl.API.IDataObject>.Current.Clear();
        }

        [Test]
        public void GetObject()
        {
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var obj = ctx.GetQuery<Kistl.App.Base.ObjectClass>().Single(o => o.ID == 2);
                Assert.That(obj.ID, Is.EqualTo(2));
            }
        }

        [Test]
        public void GetObject_Twice()
        {
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var obj1 = ctx.GetQuery<Kistl.App.Base.ObjectClass>().Single(o => o.ID == 2);
                Assert.That(obj1.ID, Is.EqualTo(2));

                var obj2 = ctx.GetQuery<Kistl.App.Base.ObjectClass>().Single(o => o.ID == 2);
                Assert.That(obj2.ID, Is.EqualTo(2));

                Assert.That(object.ReferenceEquals(obj1, obj2), "Obj1 & Obj2 are different Objects");
            }
        }


        [Test]
        public void GetListOf()
        {
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var list = ctx.GetQuery<Kistl.App.Projekte.Projekt>();
                int count = 0;
                foreach (Kistl.App.Projekte.Projekt prj in list)
                {
                    count += prj.Tasks.Count;
                }
                Assert.That(count, Is.GreaterThan(0));
            }
        }

        [Test]
        public void GetListOf_List()
        {
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var list = ctx.GetQuery<Kistl.App.Projekte.Mitarbeiter>();
                int count = 0;
                foreach (Kistl.App.Projekte.Mitarbeiter ma in list)
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
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var list = ctx.GetQuery<Kistl.App.Projekte.Task>().ToList();
                Assert.That(list.Count, Is.GreaterThan(0));
                var obj = list[0];

                ID = obj.ID;
                aufwand = (obj.Aufwand ?? 0.0) + 1.0;

                obj.Aufwand = aufwand;

                ctx.SubmitChanges();
            }

            CacheController<Kistl.API.IDataObject>.Current.Clear();

            using (Kistl.API.IKistlContext checkctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var obj = checkctx.GetQuery<Kistl.App.Projekte.Task>().Single(o => o.ID == ID);
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
            Kistl.App.Projekte.Projekt p;
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                p = ctx.GetQuery<Kistl.App.Projekte.Projekt>().ToList()[0];
                var obj = ctx.Create<Kistl.App.Projekte.Task>();

                obj.Name = "NUnit Test Task";
                obj.Aufwand = aufwand;
                obj.DatumVon = datum;
                obj.DatumBis = datum.AddDays(1);
                obj.Projekt = p;

                ctx.SubmitChanges();
                ID = obj.ID;
                Assert.That(ID, Is.Not.EqualTo(Kistl.API.Helper.INVALIDID));
            }

            CacheController<Kistl.API.IDataObject>.Current.Clear();

            using (Kistl.API.IKistlContext checkctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var obj = checkctx.GetQuery<Kistl.App.Projekte.Task>().Single(o => o.ID == ID);
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.Aufwand, Is.EqualTo(aufwand));
                Assert.That(obj.Projekt.ID, Is.EqualTo(p.ID));
            }
        }
    }
}
