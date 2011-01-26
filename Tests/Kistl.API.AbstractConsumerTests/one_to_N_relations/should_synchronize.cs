
namespace Kistl.API.AbstractConsumerTests.one_to_N_relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Test;

    using NUnit.Framework;

    public abstract class should_synchronize : AbstractTestFixture
    {
        IKistlContext ctx;
        One_to_N_relations_One proj1;
        One_to_N_relations_One proj2;
        One_to_N_relations_N task1;
        One_to_N_relations_N task2;

        [SetUp]
        public void InitTestObjects()
        {
            ctx = GetContext();
            proj1 = ctx.Create<One_to_N_relations_One>();
            proj2 = ctx.Create<One_to_N_relations_One>();
            var proj = ctx.Create<One_to_N_relations_One>();
            task1 = ctx.Create<One_to_N_relations_N>();
            task1.OneSide = proj;
            task2 = ctx.Create<One_to_N_relations_N>();
            task2.OneSide = proj;
            ctx.SubmitChanges();
        }

        [TearDown]
        public void DisposeTestObjects()
        {
            ctx.Dispose();
        }

        [Test]
        public void init_correct()
        {
            Assert.That(proj1.NSide, Is.Empty, "new project should not have NSide");
            Assert.That(proj2.NSide, Is.Empty, "new project should not have NSide");
            Assert.That(task1.OneSide, Is.Not.SameAs(proj1), "new task should not have project");
            Assert.That(task1.OneSide, Is.Not.SameAs(proj2), "new task should not have project");
            Assert.That(task2.OneSide, Is.Not.SameAs(proj1), "new task should not have project");
            Assert.That(task2.OneSide, Is.Not.SameAs(proj2), "new task should not have project");
        }

        [Test]
        public void when_setting_1_side()
        {
            task1.OneSide = proj1;

            Assert.That(task1.OneSide, Is.SameAs(proj1), "strange reference after setting project");
            Assert.That(proj1.NSide.ToArray(), Is.EquivalentTo(new[] { task1 }));

            Assert.That(proj1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(task1.ObjectState, Is.EqualTo(DataObjectState.Modified));
        }

        [Test]
        public void when_setting_N_side()
        {
            proj1.NSide.Add(task1);

            Assert.That(task1.OneSide, Is.SameAs(proj1), "strange reference after setting project");
            Assert.That(proj1.NSide.ToArray(), Is.EquivalentTo(new[] { task1 }));

            Assert.That(proj1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(task1.ObjectState, Is.EqualTo(DataObjectState.Modified));
        }

        [Test]
        public void when_resetting_1_side()
        {
            task1.OneSide = proj1;

            Assert.That(task1.OneSide, Is.SameAs(proj1), "Setting the first property destroyed the object reference");
            Assert.That(proj1.NSide, Is.EquivalentTo(new[] { task1 }), "first task list not correct");

            task1.OneSide = proj2;

            Assert.That(task1.OneSide, Is.SameAs(proj2), "Setting the second property destroyed the object reference");
            Assert.That(proj1.NSide, Is.Empty, "first Task list was not cleared");
            Assert.That(proj2.NSide.ToArray(), Is.EquivalentTo(new[] { task1 }), "second task list not correct");

            Assert.That(proj1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(proj2.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(task1.ObjectState, Is.EqualTo(DataObjectState.Modified));
        }


        [Test]
        public void when_setting_N_side_via_index()
        {
            var cls = ctx.GetQuery<ObjectClass>().First();
            var m1 = ctx.Create<Method>();
            m1.Name = "TestMethod1";
            m1.ObjectClass = cls;
            m1.Module = cls.Module;
            var m2 = ctx.Create<Method>();
            m2.Name = "TestMethod2";
            m2.ObjectClass = cls;
            m2.Module = cls.Module;
            var p1 = ctx.Create<StringParameter>();
            p1.Name = "p1";
            p1.Method = m1;
            var p2 = ctx.Create<StringParameter>();
            p2.Name = "p1";
            p2.Method = m2;

            ctx.SubmitChanges();

            Assert.That(p1.Method, Is.SameAs(m1), "first Parent wrong");
            Assert.That(m1.Parameter.ToArray(), Is.EquivalentTo(new[] { p1 }), "first Parameter collection wrong");

            m1.Parameter[0] = p2;

            Assert.That(p1.Method, Is.Null, "first Parent not reset");
            Assert.That(p2.Method, Is.SameAs(m1), "second Parent wrong");
            Assert.That(m1.Parameter.ToArray(), Is.EquivalentTo(new[] { p2 }), "second Parameter collection wrong");

            Assert.That(m1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(p1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(p2.ObjectState, Is.EqualTo(DataObjectState.Modified));
        }

        [Test]
        public void when_setting_N_side_with_remove()
        {
            Console.WriteLine("proj1.State={0}, task1.State={1}, task2.State={2}", proj1.ObjectState, task1.ObjectState, task2.ObjectState);
            proj1.NSide.Add(task1);
            Console.WriteLine("proj1.State={0}, task1.State={1}, task2.State={2}", proj1.ObjectState, task1.ObjectState, task2.ObjectState);

            Assert.That(task1.OneSide, Is.SameAs(proj1), "first parent: strange reference");
            Assert.That(proj1.NSide.ToArray(), Is.EquivalentTo(new[] { task1 }), "collection wrong after first Add");
            Assert.That(proj1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(task1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(task2.ObjectState, Is.EqualTo(DataObjectState.Unmodified));

            proj1.NSide.Remove(task1);
            Assert.That(proj1.NSide.ToArray(), Is.Empty, "collection not empty after Remove()");
            Assert.That(proj1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(task1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(task2.ObjectState, Is.EqualTo(DataObjectState.Unmodified));

            proj1.NSide.Add(task2);

            Assert.That(task1.OneSide, Is.Null, "first parent not reset");
            Assert.That(task2.OneSide, Is.SameAs(proj1), "second parent: strange reference");
            Assert.That(proj1.NSide.ToArray(), Is.EquivalentTo(new[] { task2 }), "collection wrong after second Add");

            Assert.That(proj1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(task1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(task2.ObjectState, Is.EqualTo(DataObjectState.Modified));
        }

        [Test]
        public void when_setting_N_side_with_clear()
        {
            proj1.NSide.Add(task1);

            Assert.That(task1.OneSide, Is.SameAs(proj1), "first parent: strange reference");
            Assert.That(proj1.NSide.ToArray(), Is.EquivalentTo(new[] { task1 }), "collection wrong after first Add");

            proj1.NSide.Clear();
            Assert.That(proj1.NSide.ToArray(), Is.Empty, "collection not empty after Clear()");

            proj1.NSide.Add(task2);

            Assert.That(task1.OneSide, Is.Null, "first parent not reset");
            Assert.That(task2.OneSide, Is.SameAs(proj1), "second parent: strange reference");
            Assert.That(proj1.NSide.ToArray(), Is.EquivalentTo(new[] { task2 }), "collection wrong after second Add");

            Assert.That(proj1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(task1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(task2.ObjectState, Is.EqualTo(DataObjectState.Modified));
        }
    }
}
