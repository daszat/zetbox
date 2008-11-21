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
    }
}
