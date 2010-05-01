
namespace Kistl.Server.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Autofac;

    using Kistl.API;
    using Kistl.API.Server;
    using Kistl.App.Projekte;
    using Kistl.Server;

    using NUnit.Framework;
    using NUnit.Framework.Constraints;

    [TestFixture]
    public class ObjectTests : AbstractServerTestFixture
    {
        private IKistlContext ctx;

        public override void SetUp()
        {
            var setupCtx = GetContext();

            var ma1 = setupCtx.Create<Mitarbeiter>();
            ma1.Geburtstag = new DateTime(1970, 10, 22);
            ma1.Name = "Testmitarbeiter Blaha";

            var ma2 = setupCtx.Create<Mitarbeiter>();
            ma2.Geburtstag = new DateTime(1971, 9, 23);
            ma2.Name = "Testmitarbeiter Foobar";

            var prj1 = setupCtx.Create<Projekt>();
            prj1.Mitarbeiter.Add(ma1);
            prj1.Mitarbeiter.Add(ma2);
            prj1.Name = "blubb";

            var prj2 = setupCtx.Create<Projekt>();
            prj2.Mitarbeiter.Add(ma1);
            prj2.Name = "flubb";
            var task1 = setupCtx.Create<Task>();
            task1.Projekt = prj2;

            var task2 = setupCtx.Create<Task>();
            task2.Projekt = prj2;

            setupCtx.SubmitChanges();

            ctx = GetContext();
        }

        public override void TearDown()
        {
            var deleteCtx = GetContext();
            deleteCtx.GetQuery<Mitarbeiter>().ForEach(obj => deleteCtx.Delete(obj));
            deleteCtx.GetQuery<Projekt>().ForEach(obj => { obj.Mitarbeiter.Clear(); obj.Tasks.Clear(); deleteCtx.Delete(obj); });
            deleteCtx.GetQuery<Task>().ForEach(obj => deleteCtx.Delete(obj));
            deleteCtx.SubmitChanges();
        }

        [Test]
        public void GetObject()
        {
            var obj = ctx.GetQuery<Kistl.App.Projekte.Projekt>().First(o => o.Name == "blubb");
            Assert.That(obj.Name, Is.EqualTo("blubb"));
        }

        [Test]
        public void GetObject_Twice()
        {
            var obj1 = ctx.GetQuery<Kistl.App.Projekte.Projekt>().First(o => o.Name == "blubb");
            Assert.That(obj1.Name, Is.EqualTo("blubb"));

            var obj2 = ctx.GetQuery<Kistl.App.Projekte.Projekt>().First(o => o.Name == "blubb");
            Assert.That(obj2.Name, Is.EqualTo("blubb"));

            Assert.That(object.ReferenceEquals(obj1, obj2), "Obj1 & Obj2 are different Objects");
        }

        [Test]
        public void GetListOf()
        {
            var list = ctx.GetQuery<Kistl.App.Projekte.Projekt>();
            int count = 0;
            foreach (Kistl.App.Projekte.Projekt prj in list)
            {
                count += prj.Tasks.Count;
            }
            Assert.That(count, Is.GreaterThan(0));
        }

        [Test]
        public void GetListOf_List()
        {
            var list = ctx.GetQuery<Kistl.App.Projekte.Mitarbeiter>();
            int count = 0;
            foreach (Kistl.App.Projekte.Mitarbeiter ma in list)
            {
                count += ma.Projekte.Count;
            }
            Assert.That(count, Is.GreaterThan(0));
        }

        [Test]
        public void SetObject()
        {
            double aufwand;
            int ID;
            var list = ctx.GetQuery<Kistl.App.Projekte.Task>().ToList();
            Assert.That(list.Count, Is.GreaterThan(0));
            var obj = list[0];

            ID = obj.ID;
            aufwand = (obj.Aufwand ?? 0.0) + 1.0;

            obj.Aufwand = aufwand;

            ctx.SubmitChanges();

            IKistlContext checkctx = GetContext();
            var checkObj = checkctx.GetQuery<Kistl.App.Projekte.Task>().First(o => o.ID == ID);
            Assert.That(checkObj, Is.Not.Null);
            Assert.That(checkObj.Aufwand, Is.EqualTo(aufwand));
        }

        [Test]
        public void NewObject()
        {
            int ID;
            double aufwand = 1.0;
            DateTime datum = DateTime.Now;
            Kistl.App.Projekte.Projekt p;
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

            IKistlContext checkctx = GetContext();
            var checkObj = checkctx.GetQuery<Kistl.App.Projekte.Task>().First(o => o.ID == ID);
            Assert.That(checkObj, Is.Not.Null);
            Assert.That(checkObj.Aufwand, Is.EqualTo(aufwand));
            Assert.That(checkObj.Projekt.ID, Is.EqualTo(p.ID));
        }
    }
}
