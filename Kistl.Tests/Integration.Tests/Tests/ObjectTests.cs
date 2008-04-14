using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.Client;

namespace Integration.Tests.Tests
{
    [TestFixture]
    public class ObjectTests
    {
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
            double aufwand;
            DateTime datum;
            Kistl.App.Projekte.Projekt p;
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                p = ctx.GetQuery<Kistl.App.Projekte.Projekt>().ToList()[0];
                var obj = ctx.Create<Kistl.App.Projekte.Task>();

                obj.Name = "NUnit Test Task";
                obj.Aufwand = aufwand = 1.0;
                obj.DatumVon = datum = DateTime.Now;
                obj.DatumBis = datum.AddDays(1);
                obj.Projekt = p;

                ctx.SubmitChanges();
                ID = obj.ID;
                Assert.That(ID, Is.Not.EqualTo(Kistl.API.Helper.INVALIDID));
            }

            using (Kistl.API.IKistlContext checkctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var obj = checkctx.GetQuery<Kistl.App.Projekte.Task>().Single(o => o.ID == ID);
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.Aufwand, Is.EqualTo(aufwand));
                Assert.That(obj.DatumVon, Is.EqualTo(datum));
                Assert.That(obj.DatumBis, Is.EqualTo(datum.AddDays(1)));
                Assert.That(obj.Projekt.ID, Is.EqualTo(p.ID));
            }
        }
    }
}
