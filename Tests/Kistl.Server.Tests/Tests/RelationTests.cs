using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autofac;

using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Projekte;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Kistl.Server.Tests
{
    [TestFixture]
    public class RelationTests
    {
        private IContainer container;
        private IKistlContext ctx;

        [SetUp]
        public void SetUp()
        {
            container = Kistl.Server.Tests.SetUp.CreateInnerContainer();

            var setupCtx = container.Resolve<IKistlContext>();
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

            setupCtx.SubmitChanges();
            ctx = container.Resolve<IKistlContext>();
        }

        [TearDown]
        public void TearDown()
        {
            var deleteCtx = container.Resolve<IKistlContext>();
            deleteCtx.GetQuery<Mitarbeiter>().ForEach(obj => deleteCtx.Delete(obj));
            deleteCtx.GetQuery<Projekt>().ForEach(obj => { obj.Mitarbeiter.Clear(); deleteCtx.Delete(obj); });

            if (container != null)
            {
                container.Dispose();
                container = null;
            }
        }

        #region Set Relation once

        #region 1:n
        [Test]
        public void Relation_1_n_Set_1()
        {
            var prj = ctx.Create<Kistl.App.Projekte.Projekt>();
            Assert.That(prj.Tasks, Is.Not.Null);
            Assert.That(prj.Tasks.Count, Is.EqualTo(0));

            var task = ctx.Create<Kistl.App.Projekte.Task>();
            Assert.That(task.Projekt, Is.Null);

            task.Projekt = prj;

            Assert.That(task.Projekt, Is.Not.Null);
            Assert.That(prj.Tasks.Count, Is.EqualTo(1));
            Assert.That(prj.Tasks.First(), Is.SameAs(task));
        }

        [Test]
        public void Relation_1_n_Set_n()
        {
            var prj = ctx.Create<Kistl.App.Projekte.Projekt>();
            Assert.That(prj.Tasks, Is.Not.Null);
            Assert.That(prj.Tasks.Count, Is.EqualTo(0));

            var task = ctx.Create<Kistl.App.Projekte.Task>();
            Assert.That(task.Projekt, Is.Null);

            prj.Tasks.Add(task);

            Assert.That(task.Projekt, Is.Not.Null);
            Assert.That(prj.Tasks.Count, Is.EqualTo(1));
            Assert.That(prj.Tasks.First(), Is.SameAs(task));
        }
        #endregion

        #region n:m
        [Test]
        public void Relation_n_m_Set_n()
        {
            var prj = ctx.Create<Kistl.App.Projekte.Projekt>();
            Assert.That(prj.Mitarbeiter, Is.Not.Null);
            Assert.That(prj.Mitarbeiter.Count, Is.EqualTo(0));

            var ma = ctx.Create<Kistl.App.Projekte.Mitarbeiter>();
            Assert.That(ma.Projekte, Is.Not.Null);
            Assert.That(ma.Projekte.Count, Is.EqualTo(0));

            prj.Mitarbeiter.Add(ma);

            Assert.That(prj.Mitarbeiter.Count, Is.EqualTo(1));
            Assert.That(prj.Mitarbeiter.First(), Is.SameAs(ma));
            Assert.That(ma.Projekte.Count, Is.EqualTo(1));
            Assert.That(ma.Projekte.First(), Is.SameAs(prj));
        }

        [Test]
        public void Relation_n_m_Set_m()
        {
            var prj = ctx.Create<Kistl.App.Projekte.Projekt>();
            Assert.That(prj.Mitarbeiter, Is.Not.Null);
            Assert.That(prj.Mitarbeiter.Count, Is.EqualTo(0));

            var ma = ctx.Create<Kistl.App.Projekte.Mitarbeiter>();
            Assert.That(ma.Projekte, Is.Not.Null);
            Assert.That(ma.Projekte.Count, Is.EqualTo(0));

            ma.Projekte.Add(prj);

            Assert.That(prj.Mitarbeiter.Count, Is.EqualTo(1));
            Assert.That(prj.Mitarbeiter.First(), Is.SameAs(ma));
            Assert.That(ma.Projekte.Count, Is.EqualTo(1));
            Assert.That(ma.Projekte.First(), Is.SameAs(prj));
        }
        #endregion

        //#region 1:1
        //[Test]
        //public void Relation_1_1_Set_Left()
        //{
        //    using (IKistlContext ctx = KistlContext.GetContext())
        //    {
        //        var rel = ctx.Create<Kistl.App.Base.Relation>();
        //        Assert.That(rel.LeftPart, Is.Null);

        //        var prop = ctx.Create<Kistl.App.Base.ObjectReferenceProperty>();
        //        Assert.That(prop.LeftOf, Is.Null);

        //        rel.LeftPart = prop;

        //        Assert.That(rel.LeftPart, Is.SameAs(prop));
        //        Assert.That(prop.LeftOf, Is.SameAs(rel));
        //    }
        //}

        //[Test]
        //public void Relation_1_1_Set_Right()
        //{
        //    using (IKistlContext ctx = KistlContext.GetContext())
        //    {
        //        var rel = ctx.Create<Kistl.App.Base.Relation>();
        //        Assert.That(rel.LeftPart, Is.Null);

        //        var prop = ctx.Create<Kistl.App.Base.ObjectReferenceProperty>();
        //        Assert.That(prop.LeftOf, Is.Null);

        //        prop.LeftOf = rel;

        //        Assert.That(rel.LeftPart, Is.SameAs(prop));
        //        Assert.That(prop.LeftOf, Is.SameAs(rel));
        //    }
        //}
        //#endregion

        #endregion

        #region Change Relation

        #region 1:n
        [Test]
        public void Change_Relation_1_n_Set_1()
        {
            var prj = ctx.Create<Kistl.App.Projekte.Projekt>();
            var prj2 = ctx.Create<Kistl.App.Projekte.Projekt>();
            var task = ctx.Create<Kistl.App.Projekte.Task>();

            task.Projekt = prj;

            Assert.That(task.Projekt, Is.Not.Null);
            Assert.That(prj.Tasks.Count, Is.EqualTo(1));
            Assert.That(prj.Tasks.First(), Is.SameAs(task));

            task.Projekt = prj2;

            Assert.That(task.Projekt, Is.Not.Null);
            Assert.That(prj.Tasks.Count, Is.EqualTo(0));
            Assert.That(prj2.Tasks.Count, Is.EqualTo(1));
            Assert.That(prj2.Tasks.First(), Is.SameAs(task));
        }

        [Test]
        public void Change_Relation_1_n_Set_n_With_Remove()
        {
            var prj = ctx.Create<Kistl.App.Projekte.Projekt>();
            var task = ctx.Create<Kistl.App.Projekte.Task>();
            var task2 = ctx.Create<Kistl.App.Projekte.Task>();

            prj.Tasks.Add(task);

            Assert.That(task.Projekt, Is.Not.Null);
            Assert.That(prj.Tasks.Count, Is.EqualTo(1));
            Assert.That(prj.Tasks.First(), Is.SameAs(task));

            prj.Tasks.Remove(task);
            prj.Tasks.Add(task2);

            Assert.That(task.Projekt, Is.Null);
            Assert.That(task2.Projekt, Is.Not.Null);
            Assert.That(prj.Tasks.Count, Is.EqualTo(1));
            Assert.That(prj.Tasks.First(), Is.SameAs(task2));
        }

        [Test]
        public void Change_Relation_1_n_Set_n_With_Clear()
        {
            var prj = ctx.Create<Kistl.App.Projekte.Projekt>();
            var task = ctx.Create<Kistl.App.Projekte.Task>();
            var task2 = ctx.Create<Kistl.App.Projekte.Task>();

            prj.Tasks.Add(task);

            Assert.That(task.Projekt, Is.Not.Null);
            Assert.That(prj.Tasks.Count, Is.EqualTo(1));
            Assert.That(prj.Tasks.First(), Is.SameAs(task));

            prj.Tasks.Clear();
            prj.Tasks.Add(task2);

            Assert.That(task.Projekt, Is.Null);
            Assert.That(task2.Projekt, Is.Not.Null);
            Assert.That(prj.Tasks.Count, Is.EqualTo(1));
            Assert.That(prj.Tasks.First(), Is.SameAs(task2));
        }
        [Test]
        public void Change_Relation_1_n_Set_n_By_Index()
        {
            var m = ctx.Create<Kistl.App.Base.Method>();
            var p = ctx.Create<Kistl.App.Base.StringParameter>();
            var p2 = ctx.Create<Kistl.App.Base.BoolParameter>();

            m.Parameter.Add(p);

            Assert.That(p.Method, Is.Not.Null);
            Assert.That(m.Parameter.Count, Is.EqualTo(1));
            Assert.That(m.Parameter.First(), Is.SameAs(p));

            m.Parameter[0] = p2;

            Assert.That(p.Method, Is.Null);
            Assert.That(p2.Method, Is.Not.Null);
            Assert.That(m.Parameter.Count, Is.EqualTo(1));
            Assert.That(m.Parameter.First(), Is.SameAs(p2));
        }
        #endregion

        #region n:m
        [Test]
        public void Change_Relation_n_m_Set_n_With_Remove()
        {
            var prj = ctx.Create<Kistl.App.Projekte.Projekt>();
            var ma = ctx.Create<Kistl.App.Projekte.Mitarbeiter>();
            var ma2 = ctx.Create<Kistl.App.Projekte.Mitarbeiter>();

            prj.Mitarbeiter.Add(ma);

            Assert.That(prj.Mitarbeiter.Count, Is.EqualTo(1));
            Assert.That(prj.Mitarbeiter.First(), Is.SameAs(ma));
            Assert.That(ma.Projekte.Count, Is.EqualTo(1));
            Assert.That(ma.Projekte.First(), Is.SameAs(prj));

            prj.Mitarbeiter.Remove(ma);
            prj.Mitarbeiter.Add(ma2);

            Assert.That(prj.Mitarbeiter.Count, Is.EqualTo(1));
            Assert.That(prj.Mitarbeiter.First(), Is.SameAs(ma2));
            Assert.That(ma.Projekte.Count, Is.EqualTo(0));
            Assert.That(ma2.Projekte.Count, Is.EqualTo(1));
            Assert.That(ma2.Projekte.First(), Is.SameAs(prj));
        }

        [Test]
        public void Change_Relation_n_m_Set_n_With_Clear()
        {
            var prj = ctx.Create<Kistl.App.Projekte.Projekt>();
            var ma = ctx.Create<Kistl.App.Projekte.Mitarbeiter>();
            var ma2 = ctx.Create<Kistl.App.Projekte.Mitarbeiter>();

            prj.Mitarbeiter.Add(ma);

            Assert.That(prj.Mitarbeiter.Count, Is.EqualTo(1));
            Assert.That(prj.Mitarbeiter.First(), Is.SameAs(ma));
            Assert.That(ma.Projekte.Count, Is.EqualTo(1));
            Assert.That(ma.Projekte.First(), Is.SameAs(prj));

            prj.Mitarbeiter.Clear();
            prj.Mitarbeiter.Add(ma2);

            Assert.That(prj.Mitarbeiter.Count, Is.EqualTo(1));
            Assert.That(prj.Mitarbeiter.First(), Is.SameAs(ma2));
            Assert.That(ma.Projekte.Count, Is.EqualTo(0));
            Assert.That(ma2.Projekte.Count, Is.EqualTo(1));
            Assert.That(ma2.Projekte.First(), Is.SameAs(prj));
        }

        [Test]
        public void Change_Relation_n_m_Set_n_By_Index()
        {
            var prj = ctx.Create<Kistl.App.Projekte.Projekt>();
            var ma = ctx.Create<Kistl.App.Projekte.Mitarbeiter>();
            var ma2 = ctx.Create<Kistl.App.Projekte.Mitarbeiter>();

            prj.Mitarbeiter.Add(ma);

            Assert.That(prj.Mitarbeiter.Count, Is.EqualTo(1));
            Assert.That(prj.Mitarbeiter.First(), Is.SameAs(ma));
            Assert.That(ma.Projekte.Count, Is.EqualTo(1));
            Assert.That(ma.Projekte.First(), Is.SameAs(prj));

            prj.Mitarbeiter[0] = ma2;

            Assert.That(prj.Mitarbeiter.Count, Is.EqualTo(1));
            Assert.That(prj.Mitarbeiter.First(), Is.SameAs(ma2));
            Assert.That(ma.Projekte.Count, Is.EqualTo(0));
            Assert.That(ma2.Projekte.Count, Is.EqualTo(1));
            Assert.That(ma2.Projekte.First(), Is.SameAs(prj));
        }

        [Test]
        public void Change_Relation_n_m_Set_m_With_Remove()
        {
            var prj = ctx.Create<Kistl.App.Projekte.Projekt>();
            var prj2 = ctx.Create<Kistl.App.Projekte.Projekt>();
            var ma = ctx.Create<Kistl.App.Projekte.Mitarbeiter>();

            ma.Projekte.Add(prj);

            Assert.That(prj.Mitarbeiter.Count, Is.EqualTo(1));
            Assert.That(prj.Mitarbeiter.First(), Is.SameAs(ma));
            Assert.That(ma.Projekte.Count, Is.EqualTo(1));
            Assert.That(ma.Projekte.First(), Is.SameAs(prj));

            ma.Projekte.Remove(prj);
            ma.Projekte.Add(prj2);

            Assert.That(prj.Mitarbeiter.Count, Is.EqualTo(0));
            Assert.That(prj2.Mitarbeiter.Count, Is.EqualTo(1));
            Assert.That(prj2.Mitarbeiter.First(), Is.SameAs(ma));
            Assert.That(ma.Projekte.Count, Is.EqualTo(1));
            Assert.That(ma.Projekte.First(), Is.SameAs(prj2));
        }
        [Test]
        public void Change_Relation_n_m_Set_m_With_Clear()
        {
            var prj = ctx.Create<Kistl.App.Projekte.Projekt>();
            var prj2 = ctx.Create<Kistl.App.Projekte.Projekt>();
            var ma = ctx.Create<Kistl.App.Projekte.Mitarbeiter>();

            ma.Projekte.Add(prj);

            Assert.That(prj.Mitarbeiter.Count, Is.EqualTo(1));
            Assert.That(prj.Mitarbeiter.First(), Is.SameAs(ma));
            Assert.That(ma.Projekte.Count, Is.EqualTo(1));
            Assert.That(ma.Projekte.First(), Is.SameAs(prj));

            ma.Projekte.Clear();
            ma.Projekte.Add(prj2);

            Assert.That(prj.Mitarbeiter.Count, Is.EqualTo(0));
            Assert.That(prj2.Mitarbeiter.Count, Is.EqualTo(1));
            Assert.That(prj2.Mitarbeiter.First(), Is.SameAs(ma));
            Assert.That(ma.Projekte.Count, Is.EqualTo(1));
            Assert.That(ma.Projekte.First(), Is.SameAs(prj2));
        }
        [Test]
        public void Change_Relation_n_m_Set_m_By_Index()
        {
            var prj = ctx.Create<Kistl.App.Projekte.Projekt>();
            var prj2 = ctx.Create<Kistl.App.Projekte.Projekt>();
            var ma = ctx.Create<Kistl.App.Projekte.Mitarbeiter>();

            ma.Projekte.Add(prj);

            Assert.That(prj.Mitarbeiter.Count, Is.EqualTo(1));
            Assert.That(prj.Mitarbeiter.First(), Is.SameAs(ma));
            Assert.That(ma.Projekte.Count, Is.EqualTo(1));
            Assert.That(ma.Projekte.First(), Is.SameAs(prj));

            ma.Projekte[0] = prj2;

            Assert.That(prj.Mitarbeiter.Count, Is.EqualTo(0));
            Assert.That(prj2.Mitarbeiter.Count, Is.EqualTo(1));
            Assert.That(prj2.Mitarbeiter.First(), Is.SameAs(ma));
            Assert.That(ma.Projekte.Count, Is.EqualTo(1));
            Assert.That(ma.Projekte.First(), Is.SameAs(prj2));
        }
        #endregion

        #endregion

        #region Clear Relation
        #endregion

        #region Sort Relation
        [Test]
        public void Sort_Relation_1_n()
        {
            int methodID = 0;
            var method = ctx.GetQuery<Kistl.App.Base.Method>().ToList().Where(m => m.Module.ModuleName == "Projekte")
                .OrderByDescending(m => m.Parameter.Count).First();
            methodID = method.ID;

            var tmpParameter = method.Parameter.ToList();
            method.Parameter.Clear();
            foreach (Kistl.App.Base.BaseParameter p in tmpParameter
                .OrderBy(p => p.IsReturnParameter).ThenBy(p => p.Name))
            {
                method.Parameter.Add(p);
            }

            ctx.SubmitChanges();

            var checkCtx = container.Resolve<IKistlContext>();
            var checkMethod = checkCtx.Find<Kistl.App.Base.Method>(methodID);
            var checkParameter = checkMethod.Parameter.ToList();

            Assert.That(
                checkParameter
                    .OrderBy(p => p.IsReturnParameter)
                    .ThenBy(p => p.Name)
                    .ToList(),
                Is.EquivalentTo(checkParameter)
                );
        }

        [Test]
        public void Sort_Relation_n_m_n()
        {
            int prjID = 0;
            var prj = ctx.GetQuery<Kistl.App.Projekte.Projekt>().ToList()
                .OrderByDescending(p => p.Mitarbeiter.Count).First();
            prjID = prj.ID;

            var tmpMitarbeiter = prj.Mitarbeiter.ToList();
            prj.Mitarbeiter.Clear();
            foreach (Kistl.App.Projekte.Mitarbeiter m in tmpMitarbeiter
                .OrderBy(m => m.Name))
            {
                prj.Mitarbeiter.Add(m);
            }

            ctx.SubmitChanges();

            var checkCtx = container.Resolve<IKistlContext>();
            var checkPrj = checkCtx.Find<Kistl.App.Projekte.Projekt>(prjID);
            var checkMitarbeiter = checkPrj.Mitarbeiter.ToList();

            int i = 0;
            foreach (Kistl.App.Projekte.Mitarbeiter m in checkMitarbeiter
                .OrderBy(m => m.Name))
            {
                Assert.That(m, Is.EqualTo(checkMitarbeiter[i++]));
            }
        }

        [Test]
        public void Sort_Relation_n_m_m()
        {
            int maID = 0;
            var ma = ctx.GetQuery<Kistl.App.Projekte.Mitarbeiter>().ToList()
                .OrderByDescending(p => p.Projekte.Count).First();
            maID = ma.ID;

            var tmpProjekte = ma.Projekte.ToList();
            ma.Projekte.Clear();
            foreach (Kistl.App.Projekte.Projekt prj in tmpProjekte
                .OrderBy(p => p.Name))
            {
                ma.Projekte.Add(prj);
            }

            ctx.SubmitChanges();

            var checkCtx = container.Resolve<IKistlContext>();
            var checkMitarbeiter = checkCtx.Find<Kistl.App.Projekte.Mitarbeiter>(maID);
            var checkProjekte = checkMitarbeiter.Projekte.ToList();

            int i = 0;
            foreach (Kistl.App.Projekte.Projekt prj in checkProjekte
                .OrderBy(p => p.Name))
            {
                Assert.That(prj, Is.EqualTo(checkProjekte[i++]));
            }
        }

        #endregion
    }
}
