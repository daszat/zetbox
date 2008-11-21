using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Kistl.API;
using Kistl.API.Client;
using NUnit.Framework.SyntaxHelpers;

namespace Kistl.IntegrationTests
{
    [TestFixture]
    public class RelationTests
    {
        [SetUp]
        public void SetUp()
        {
        }

        #region Set Relation once

        #region 1:n
        [Test]
        public void Relation_1_n_Set_1()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
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
        }

        [Test]
        public void Relation_1_n_Set_n()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
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
        }
        #endregion

        #region n:m
        [Test]
        public void Relation_n_m_Set_n()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
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
        }

        [Test]
        public void Relation_n_m_Set_m()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
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
        }
        #endregion

        #region 1:1
        [Test]
        public void Relation_1_1_Set_Left()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var rel = ctx.Create<Kistl.App.Base.Relation>();
                Assert.That(rel.LeftPart, Is.Null);

                var prop = ctx.Create<Kistl.App.Base.ObjectReferenceProperty>();
                Assert.That(prop.LeftOf, Is.Null);

                rel.LeftPart = prop;

                Assert.That(rel.LeftPart, Is.SameAs(prop));
                Assert.That(prop.LeftOf, Is.SameAs(rel));
            }
        }

        [Test]
        public void Relation_1_1_Set_Right()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var rel = ctx.Create<Kistl.App.Base.Relation>();
                Assert.That(rel.LeftPart, Is.Null);

                var prop = ctx.Create<Kistl.App.Base.ObjectReferenceProperty>();
                Assert.That(prop.LeftOf, Is.Null);

                prop.LeftOf = rel;

                Assert.That(rel.LeftPart, Is.SameAs(prop));
                Assert.That(prop.LeftOf, Is.SameAs(rel));
            }
        }
        #endregion

        #endregion

        #region Change Relation

        #region 1:n
        [Test]
        public void Change_Relation_1_n_Set_1()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
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
        }

        [Test]
        public void Change_Relation_1_n_Set_n_With_Remove()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
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
        }

        [Test]
        public void Change_Relation_1_n_Set_n_With_Clear()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
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
        }
        [Test]
        [Ignore("Sortable Collections mus be implemented. Then IList can be returned")]
        public void Change_Relation_1_n_Set_n_By_Index()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var m = ctx.Create<Kistl.App.Base.Method>();
                var p = ctx.Create<Kistl.App.Base.StringParameter>();
                var p2 = ctx.Create<Kistl.App.Base.StringParameter>();

                m.Parameter.Add(p);

                Assert.That(p.Method, Is.Not.Null);
                Assert.That(m.Parameter.Count, Is.EqualTo(1));
                Assert.That(m.Parameter.First(), Is.SameAs(p));

                // m.Parameter[0] = p2;

                Assert.That(p.Method, Is.Null);
                Assert.That(p2.Method, Is.Not.Null);
                Assert.That(m.Parameter.Count, Is.EqualTo(1));
                Assert.That(m.Parameter.First(), Is.SameAs(p2));
            }
        }
        #endregion

        #region n:m
        [Test]
        public void Change_Relation_n_m_Set_n_With_Remove()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
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
        }

        [Test]
        public void Change_Relation_n_m_Set_n_With_Clear()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
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
        }

        [Test]
        [Ignore("Sortable Collections mus be implemented. Then IList can be returned")]
        public void Change_Relation_n_m_Set_n_By_Index()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var prj = ctx.Create<Kistl.App.Projekte.Projekt>();
                var ma = ctx.Create<Kistl.App.Projekte.Mitarbeiter>();
                var ma2 = ctx.Create<Kistl.App.Projekte.Mitarbeiter>();

                prj.Mitarbeiter.Add(ma);

                Assert.That(prj.Mitarbeiter.Count, Is.EqualTo(1));
                Assert.That(prj.Mitarbeiter.First(), Is.SameAs(ma));
                Assert.That(ma.Projekte.Count, Is.EqualTo(1));
                Assert.That(ma.Projekte.First(), Is.SameAs(prj));

                // prj.Mitarbeiter[0] = ma2;

                Assert.That(prj.Mitarbeiter.Count, Is.EqualTo(1));
                Assert.That(prj.Mitarbeiter.First(), Is.SameAs(ma2));
                Assert.That(ma.Projekte.Count, Is.EqualTo(0));
                Assert.That(ma2.Projekte.Count, Is.EqualTo(1));
                Assert.That(ma2.Projekte.First(), Is.SameAs(prj));
            }
        }

        [Test]
        public void Change_Relation_n_m_Set_m_With_Remove()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
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
        }
        [Test]
        public void Change_Relation_n_m_Set_m_With_Clear()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
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
        }        
        [Test]
        [Ignore("Sortable Collections mus be implemented. Then IList can be returned")]
        public void Change_Relation_n_m_Set_m_By_Index()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var prj = ctx.Create<Kistl.App.Projekte.Projekt>();
                var prj2 = ctx.Create<Kistl.App.Projekte.Projekt>();
                var ma = ctx.Create<Kistl.App.Projekte.Mitarbeiter>();

                ma.Projekte.Add(prj);

                Assert.That(prj.Mitarbeiter.Count, Is.EqualTo(1));
                Assert.That(prj.Mitarbeiter.First(), Is.SameAs(ma));
                Assert.That(ma.Projekte.Count, Is.EqualTo(1));
                Assert.That(ma.Projekte.First(), Is.SameAs(prj));

                //ma.Projekte[0] = prj2;

                Assert.That(prj.Mitarbeiter.Count, Is.EqualTo(0));
                Assert.That(prj2.Mitarbeiter.Count, Is.EqualTo(1));
                Assert.That(prj2.Mitarbeiter.First(), Is.SameAs(ma));
                Assert.That(ma.Projekte.Count, Is.EqualTo(1));
                Assert.That(ma.Projekte.First(), Is.SameAs(prj2));
            }
        }               
        #endregion

        #endregion

        #region Clear Relation
        #endregion
    }
}
