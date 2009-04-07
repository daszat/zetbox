using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Projekte;

using NUnit.Framework;

namespace Kistl.API.AbstractConsumerTests.N_to_M_relations
{

    public abstract class should_synchronize
    {
        protected abstract IKistlContext GetContext();

        IKistlContext ctx;
        Projekt proj1;
        Projekt proj2;
        Mitarbeiter emp1;
        Mitarbeiter emp2;

        [SetUp]
        public void InitTestObjects()
        {
            ctx = GetContext();
            proj1 = ctx.Create<Projekt>();
            proj2 = ctx.Create<Projekt>();
            emp1 = ctx.Create<Mitarbeiter>();
            emp2 = ctx.Create<Mitarbeiter>();
        }

        [TearDown]
        public void DisposeTestObjects()
        {
            ctx.Dispose();
        }

        [Test]
        public void init_correct()
        {
            Assert.That(proj1.Mitarbeiter, Is.Empty);
            Assert.That(proj2.Mitarbeiter, Is.Empty);
            Assert.That(emp1.Projekte, Is.Empty);
            Assert.That(emp2.Projekte, Is.Empty);
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(4));
        }

        [Test]
        public void when_setting_N_side_one_item()
        {
            proj1.Mitarbeiter.Add(emp1);

            Assert.That(proj1.Mitarbeiter, Is.EquivalentTo(new[] { emp1 }));
            Assert.That(emp1.Projekte, Is.EquivalentTo(new[] { proj1 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(5));
        }

        [Test]
        public void when_setting_M_side_one_item()
        {
            emp1.Projekte.Add(proj1);

            Assert.That(proj1.Mitarbeiter, Is.EquivalentTo(new[] { emp1 }));
            Assert.That(emp1.Projekte, Is.EquivalentTo(new[] { proj1 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(5));
        }

        [Test]
        public void when_setting_both_sides_one_item()
        {
            proj1.Mitarbeiter.Add(emp1);
            emp1.Projekte.Add(proj1);

            Assert.That(proj1.Mitarbeiter, Is.EquivalentTo(new[] { emp1, emp1 }));
            Assert.That(emp1.Projekte, Is.EquivalentTo(new[] { proj1, proj1 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(6));
        }

        [Test]
        public void when_setting_N_side_two_items()
        {
            proj1.Mitarbeiter.Add(emp1);
            proj1.Mitarbeiter.Add(emp2);

            Assert.That(proj1.Mitarbeiter, Is.EquivalentTo(new[] { emp1, emp2 }));
            Assert.That(emp1.Projekte, Is.EquivalentTo(new[] { proj1 }));
            Assert.That(emp2.Projekte, Is.EquivalentTo(new[] { proj1 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(6));
        }

        [Test]
        public void when_setting_M_side_two_items()
        {
            emp1.Projekte.Add(proj1);
            emp1.Projekte.Add(proj2);

            Assert.That(emp1.Projekte, Is.EquivalentTo(new[] { proj1, proj2 }));
            Assert.That(proj1.Mitarbeiter, Is.EquivalentTo(new[] { emp1 }));
            Assert.That(proj2.Mitarbeiter, Is.EquivalentTo(new[] { emp1 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(6));
        }

        [Test]
        public void when_setting_both_sides_two_items()
        {
            emp1.Projekte.Add(proj1);
            emp1.Projekte.Add(proj2);

            proj1.Mitarbeiter.Add(emp1);
            proj2.Mitarbeiter.Add(emp1);

            Assert.That(emp1.Projekte, Is.EquivalentTo(new[] { proj1, proj2, proj1, proj2 }));
            Assert.That(proj1.Mitarbeiter, Is.EquivalentTo(new[] { emp1, emp1 }));
            Assert.That(proj2.Mitarbeiter, Is.EquivalentTo(new[] { emp1, emp1 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(8));
        }

        [Test]
        public void when_setting_N_side_matrix()
        {
            proj1.Mitarbeiter.Add(emp1);
            proj1.Mitarbeiter.Add(emp2);

            proj2.Mitarbeiter.Add(emp1);
            proj2.Mitarbeiter.Add(emp2);


            Assert.That(proj1.Mitarbeiter, Is.EquivalentTo(new[] { emp1, emp2 }));
            Assert.That(proj2.Mitarbeiter, Is.EquivalentTo(new[] { emp1, emp2 }));
            Assert.That(emp1.Projekte, Is.EquivalentTo(new[] { proj1, proj2 }));
            Assert.That(emp2.Projekte, Is.EquivalentTo(new[] { proj1, proj2 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(8));
        }

        [Test]
        public void when_setting_M_side_matrix()
        {
            emp1.Projekte.Add(proj1);
            emp1.Projekte.Add(proj2);

            emp2.Projekte.Add(proj1);
            emp2.Projekte.Add(proj2);

            Assert.That(emp1.Projekte, Is.EquivalentTo(new[] { proj1, proj2 }));
            Assert.That(emp2.Projekte, Is.EquivalentTo(new[] { proj1, proj2 }));
            Assert.That(proj1.Mitarbeiter, Is.EquivalentTo(new[] { emp1, emp2 }));
            Assert.That(proj2.Mitarbeiter, Is.EquivalentTo(new[] { emp1, emp2 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(8));
        }

        [Test]
        public void when_setting_both_sides_matrix()
        {
            emp1.Projekte.Add(proj1);
            emp1.Projekte.Add(proj2);

            emp2.Projekte.Add(proj1);
            emp2.Projekte.Add(proj2);

            proj1.Mitarbeiter.Add(emp1);
            proj1.Mitarbeiter.Add(emp2);

            proj2.Mitarbeiter.Add(emp1);
            proj2.Mitarbeiter.Add(emp2);

            Assert.That(emp1.Projekte, Is.EquivalentTo(new[] { proj1, proj2, proj1, proj2 }));
            Assert.That(emp2.Projekte, Is.EquivalentTo(new[] { proj1, proj2, proj1, proj2 }));
            Assert.That(proj1.Mitarbeiter, Is.EquivalentTo(new[] { emp1, emp2, emp1, emp2 }));
            Assert.That(proj2.Mitarbeiter, Is.EquivalentTo(new[] { emp1, emp2, emp1, emp2 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(12));
        }
    }
}
