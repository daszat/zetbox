using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Projekte;

using NUnit.Framework;

namespace Kistl.API.AbstractConsumerTests.one_to_N_relations
{

    public abstract class should_synchronize
    {
        protected abstract IKistlContext GetContext();
        
        IKistlContext ctx;
        Projekt proj1;
        Projekt proj2;
        Task task1;
        Task task2;

        [SetUp]
        public void InitTestObjects()
        {
            ctx = GetContext();
            proj1 = ctx.Create<Projekt>();
            proj2 = ctx.Create<Projekt>();
            task1 = ctx.Create<Task>();
            task2 = ctx.Create<Task>();
        }

        [TearDown]
        public void DisposeTestObjects()
        {
            ctx.Dispose();
        }

        [Test]
        public void init_correct()
        {
            Assert.That(proj1.Tasks, Is.Empty, "new project should not have tasks");
            Assert.That(proj2.Tasks, Is.Empty, "new project should not have tasks");
            Assert.That(task1.Projekt, Is.Null, "new task should not have project");
            Assert.That(task2.Projekt, Is.Null, "new task should not have project");
        }

        [Test]
        public void when_setting_1_side()
        {
            task1.Projekt = proj1;

            Assert.That(task1.Projekt, Is.SameAs(proj1), "strange reference after setting project");
            Assert.That(proj1.Tasks.ToArray(), Is.EquivalentTo(new[] { task1 }));
        }

        [Test]
        public void when_setting_N_side()
        {
            proj1.Tasks.Add(task1);

            Assert.That(task1.Projekt, Is.SameAs(proj1), "strange reference after setting project");
            Assert.That(proj1.Tasks.ToArray(), Is.EquivalentTo(new[] { task1 }));
        }

        [Test]
        public void when_resetting_1_side()
        {
            task1.Projekt = proj1;

            Assert.That(task1.Projekt, Is.SameAs(proj1), "Setting the first property destroyed the object reference");
            Assert.That(proj1.Tasks, Is.EquivalentTo(new[] { task1 }), "first task list not correct");

            task1.Projekt = proj2;

            Assert.That(task1.Projekt, Is.SameAs(proj2), "Setting the second property destroyed the object reference");
            Assert.That(proj1.Tasks, Is.Empty, "first Task list was not cleared");
            Assert.That(proj2.Tasks.ToArray(), Is.EquivalentTo(new[] { task1 }), "second task list not correct");
        }


        [Test]
        public void when_setting_N_side_via_index()
        {
            var m = ctx.Create<Method>();
            var p1 = ctx.Create<StringParameter>();
            var p2 = ctx.Create<StringParameter>();

            m.Parameter.Add(p1);

            Assert.That(p1.Method, Is.SameAs(m), "first Parent wrong");
            Assert.That(m.Parameter.ToArray(), Is.EquivalentTo(new[] { p1 }), "first Parameter collection wrong");

            m.Parameter[0] = p2;

            Assert.That(p1.Method, Is.Null, "first Parent not reset");
            Assert.That(p2.Method, Is.SameAs(m), "second Parent wrong");
            Assert.That(m.Parameter.ToArray(), Is.EquivalentTo(new[] { p2 }), "second Parameter collection wrong");
        }

        [Test]
        public void when_setting_N_side_with_remove()
        {
            proj1.Tasks.Add(task1);

            Assert.That(task1.Projekt, Is.SameAs(proj1), "first parent: strange reference");
            Assert.That(proj1.Tasks.ToArray(), Is.EquivalentTo(new[] { task1 }), "collection wrong after first Add");

            proj1.Tasks.Remove(task1);
            Assert.That(proj1.Tasks.ToArray(), Is.Empty, "collection not empty after Remove()");

            proj1.Tasks.Add(task2);

            Assert.That(task1.Projekt, Is.Null, "first parent not reset");
            Assert.That(task2.Projekt, Is.SameAs(proj1), "second parent: strange reference");
            Assert.That(proj1.Tasks.ToArray(), Is.EquivalentTo(new[] { task2 }), "collection wrong after second Add");
        }

        [Test]
        public void when_setting_N_side_with_clear()
        {
            proj1.Tasks.Add(task1);

            Assert.That(task1.Projekt, Is.SameAs(proj1), "first parent: strange reference");
            Assert.That(proj1.Tasks.ToArray(), Is.EquivalentTo(new[] { task1 }), "collection wrong after first Add");

            proj1.Tasks.Clear();
            Assert.That(proj1.Tasks.ToArray(), Is.Empty, "collection not empty after Clear()");

            proj1.Tasks.Add(task2);

            Assert.That(task1.Projekt, Is.Null, "first parent not reset");
            Assert.That(task2.Projekt, Is.SameAs(proj1), "second parent: strange reference");
            Assert.That(proj1.Tasks.ToArray(), Is.EquivalentTo(new[] { task2 }), "collection wrong after second Add");
        }

    }

}
