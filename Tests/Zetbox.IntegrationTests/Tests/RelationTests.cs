using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Zetbox.API;
using Zetbox.API.AbstractConsumerTests;
using Zetbox.API.Client;
using Zetbox.App.Base;
using Zetbox.App.Projekte;

using NUnit.Framework;

namespace Zetbox.IntegrationTests
{
    [TestFixture]
    public class RelationTests
        : ProjectDataFixture
    {
        #region Set Relation once

        #region 1:1
        [Test]
        public void Relation_1_1_Set_Left()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var prop = ctx.Create<Zetbox.App.Base.IntProperty>();
                Assert.That(prop.DefaultValue, Is.Null);

                var dv = ctx.Create<Zetbox.App.Base.NewGuidDefaultValue>();
                Assert.That(dv.Property, Is.Null);

                prop.DefaultValue = dv;

                Assert.That(prop.DefaultValue, Is.SameAs(dv));
                Assert.That(dv.Property, Is.SameAs(prop));
            }
        }

        [Test]
        public void Relation_1_1_Set_Right()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var prop = ctx.Create<Zetbox.App.Base.IntProperty>();
                Assert.That(prop.DefaultValue, Is.Null);

                var dv = ctx.Create<Zetbox.App.Base.NewGuidDefaultValue>();
                Assert.That(dv.Property, Is.Null);

                dv.Property = prop;

                Assert.That(prop.DefaultValue, Is.SameAs(dv));
                Assert.That(dv.Property, Is.SameAs(prop));
            }
        }
        #endregion

        #endregion

        #region Change Relation

        #region n:m
        [Test]
        public void Change_Relation_n_m_Set_n_With_Remove()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var prj = ctx.Create<Zetbox.App.Projekte.Projekt>();
                var ma = ctx.Create<Zetbox.App.Projekte.Mitarbeiter>();
                var ma2 = ctx.Create<Zetbox.App.Projekte.Mitarbeiter>();

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
            using (IZetboxContext ctx = GetContext())
            {
                var prj = ctx.Create<Zetbox.App.Projekte.Projekt>();
                var ma = ctx.Create<Zetbox.App.Projekte.Mitarbeiter>();
                var ma2 = ctx.Create<Zetbox.App.Projekte.Mitarbeiter>();

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
        public void Change_Relation_n_m_Set_n_By_Index()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var prj = ctx.Create<Zetbox.App.Projekte.Projekt>();
                var ma = ctx.Create<Zetbox.App.Projekte.Mitarbeiter>();
                var ma2 = ctx.Create<Zetbox.App.Projekte.Mitarbeiter>();

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
        }

        [Test]
        public void Change_Relation_n_m_Set_m_With_Remove()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var prj = ctx.Create<Zetbox.App.Projekte.Projekt>();
                var prj2 = ctx.Create<Zetbox.App.Projekte.Projekt>();
                var ma = ctx.Create<Zetbox.App.Projekte.Mitarbeiter>();

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
            using (IZetboxContext ctx = GetContext())
            {
                var prj = ctx.Create<Zetbox.App.Projekte.Projekt>();
                var prj2 = ctx.Create<Zetbox.App.Projekte.Projekt>();
                var ma = ctx.Create<Zetbox.App.Projekte.Mitarbeiter>();

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
        public void Change_Relation_n_m_Set_m_By_Index()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var prj = ctx.Create<Zetbox.App.Projekte.Projekt>();
                var prj2 = ctx.Create<Zetbox.App.Projekte.Projekt>();
                var ma = ctx.Create<Zetbox.App.Projekte.Mitarbeiter>();

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
        }
        #endregion

        #endregion

        #region Clear Relation
        #endregion

        #region Sort Relation

        [Test]
        [Ignore("Use another DataFixture - ProjectDataFixture contains only one Project")]
        public void Sort_Relation_n_m_n()
        {
            int prjID = 0;
            using (IZetboxContext ctx = GetContext())
            {
                var prj = ctx.GetQuery<Zetbox.App.Projekte.Projekt>().ToList()
                    .OrderByDescending(p => p.Mitarbeiter.Count).First();
                prjID = prj.ID;

                var tmpMitarbeiter = prj.Mitarbeiter.ToList();
                prj.Mitarbeiter.Clear();
                foreach (Zetbox.App.Projekte.Mitarbeiter m in tmpMitarbeiter
                    .OrderBy(m => m.Name))
                {
                    prj.Mitarbeiter.Add(m);
                }

                ctx.SubmitChanges();
            }

            using (IZetboxContext ctx = GetContext())
            {
                var prj = ctx.Find<Zetbox.App.Projekte.Projekt>(prjID);

                var tmpMitarbeiter = prj.Mitarbeiter.ToList();
                int i = 0;
                foreach (Zetbox.App.Projekte.Mitarbeiter m in tmpMitarbeiter
                    .OrderBy(m => m.Name))
                {
                    Assert.That(m, Is.EqualTo(tmpMitarbeiter[i++]));
                }
            }
        }

        [Test]
        [Ignore("Use another DataFixture - ProjectDataFixture contains only one Project")]
        public void Sort_Relation_n_m_m()
        {
            int maID = 0;
            using (IZetboxContext ctx = GetContext())
            {
                var ma = ctx.GetQuery<Zetbox.App.Projekte.Mitarbeiter>().ToList()
                    .OrderByDescending(p => p.Projekte.Count).First();
                maID = ma.ID;

                var tmpProjekte = ma.Projekte.ToList();
                ma.Projekte.Clear();
                foreach (Zetbox.App.Projekte.Projekt prj in tmpProjekte
                    .OrderBy(p => p.Name))
                {
                    ma.Projekte.Add(prj);
                }

                ctx.SubmitChanges();
            }

            using (IZetboxContext ctx = GetContext())
            {
                var ma = ctx.Find<Zetbox.App.Projekte.Mitarbeiter>(maID);

                var tmpProjekte = ma.Projekte.ToList();
                int i = 0;
                foreach (Zetbox.App.Projekte.Projekt prj in tmpProjekte
                    .OrderBy(p => p.Name))
                {
                    Assert.That(prj, Is.EqualTo(tmpProjekte[i++]));
                }
            }
        }

        #endregion

    }
}
