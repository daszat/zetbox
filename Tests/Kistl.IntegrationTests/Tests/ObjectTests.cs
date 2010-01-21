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
        private const string ProjectName1 = "Project 1";
        private const string ProjectName2 = "Project 2";
        private const int Project1TaskCount = 10;
        private const int Project2TaskCount = 5;
        private const int TaskCount = Project1TaskCount + Project2TaskCount;

        private int Project1ID = -1;
        private int Project2ID = -1;

        [SetUp]
        public void SetUp()
        {
            DeleteObjects();

            using (IKistlContext ctx = KistlContext.GetContext())
            {
                Projekt prj1 = ctx.Create<Projekt>();
                prj1.Name = ProjectName1;

                Projekt prj2 = ctx.Create<Projekt>();
                prj2.Name = ProjectName2;

                for (int i = 0; i < Project1TaskCount; i++)
                {
                    SetUpCreateTask(ctx, prj1, i);
                }

                for (int i = 0; i < Project2TaskCount; i++)
                {
                    SetUpCreateTask(ctx, prj2, i);
                }

                ctx.SubmitChanges();

                Project1ID = prj1.ID;
                Project2ID = prj2.ID;
            }
        }

        private void SetUpCreateTask(IKistlContext ctx, Projekt prj, int i)
        {
            Task t = ctx.Create<Task>();
            t.Name = prj.Name + " - Task " + (i + 1);
            t.DatumVon = DateTime.Today.AddDays(i);
            t.DatumBis = DateTime.Today.AddDays(2 * i);
            t.Aufwand = (i + 1);
            prj.Tasks.Add(t);
        }

        [TearDown]
        public void TearDown()
        {
            DeleteObjects();
        }

        private static void DeleteObjects()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                // TODO: remove obj.Mitarbeiter.Clear() after fixing Case 1369 and marking the Mitarbeiter RelationEnd properly
                ctx.GetQuery<Projekt>().ForEach(obj => { obj.Mitarbeiter.Clear(); ctx.Delete(obj); });
                ctx.GetQuery<Task>().ForEach(obj => ctx.Delete(obj));
                ctx.SubmitChanges();
            }
        }

        [Test]
        public void GetObject()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var obj = ctx.GetQuery<Projekt>().Single(o => o.ID == Project1ID);
                Assert.That(obj.Name, Is.EqualTo(ProjectName1));
            }
        }

        [Test]
        public void GetObject_Twice()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var obj1 = ctx.GetQuery<Projekt>().Single(o => o.ID == Project1ID);
                Assert.That(obj1.Name, Is.EqualTo(ProjectName1));

                var obj2 = ctx.GetQuery<Projekt>().Single(o => o.ID == Project1ID);
                Assert.That(obj2.Name, Is.EqualTo(ProjectName1));

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
                Assert.That(count, Is.EqualTo(TaskCount));
            }
        }

        [Test]
        public void SetObject()
        {
            double aufwand;
            int ID;
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var list = ctx.GetQuery<Task>().ToList();
                Assert.That(list.Count, Is.GreaterThan(0));
                var obj = list[0];

                ID = obj.ID;
                aufwand = (obj.Aufwand ?? 0.0) + 1.0;

                obj.Aufwand = aufwand;

                ctx.SubmitChanges();
            }

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
                p = ctx.GetQuery<Projekt>().First(prj => prj.Name == ProjectName1);
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
