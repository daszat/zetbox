
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

    public abstract class should_synchronize
        : AbstractTestFixture
    {
        IKistlContext ctx;
        One_to_N_relations_One oneSide1;
        One_to_N_relations_One oneSide2;
        One_to_N_relations_One oneSide3;
        One_to_N_relations_N nSide1;
        One_to_N_relations_N nSide2;

        [SetUp]
        public void InitTestObjects()
        {
            ctx = GetContext();
            oneSide1 = ctx.Create<One_to_N_relations_One>();
            oneSide2 = ctx.Create<One_to_N_relations_One>();
            oneSide3 = ctx.Create<One_to_N_relations_One>();
            nSide1 = ctx.Create<One_to_N_relations_N>();
            nSide1.OneSide = oneSide3;
            nSide2 = ctx.Create<One_to_N_relations_N>();
            nSide2.OneSide = oneSide3;
            ctx.SubmitChanges();
            ctx = GetContext();
            oneSide1 = ctx.Find<One_to_N_relations_One>(oneSide1.ID);
            oneSide2 = ctx.Find<One_to_N_relations_One>(oneSide2.ID);
            oneSide3 = ctx.Find<One_to_N_relations_One>(oneSide3.ID);
            nSide1 = ctx.Find<One_to_N_relations_N>(nSide1.ID);
            nSide2 = ctx.Find<One_to_N_relations_N>(nSide2.ID);
        }

        [TearDown]
        public void DisposeTestObjects()
        {
            ctx = null;
            oneSide1 = oneSide2 = oneSide3 = null;
            nSide1 = nSide2 = null;
        }

        [Test]
        public void init_correct()
        {
            Assert.That(oneSide1.NSide, Is.Empty, "new OneSide should not have NSide");
            Assert.That(oneSide2.NSide, Is.Empty, "new OneSide should not have NSide");
            Assert.That(nSide1.OneSide, Is.Not.SameAs(oneSide1), "new NSide should not have OneSide");
            Assert.That(nSide1.OneSide, Is.Not.SameAs(oneSide2), "new NSide should not have OneSide");
            Assert.That(nSide2.OneSide, Is.Not.SameAs(oneSide1), "new NSide should not have OneSide");
            Assert.That(nSide2.OneSide, Is.Not.SameAs(oneSide2), "new NSide should not have OneSide");
        }

        [Test]
        public void when_setting_1_side()
        {
            nSide1.OneSide = oneSide1;

            Assert.That(nSide1.OneSide, Is.SameAs(oneSide1), "strange reference after setting OneSide");
            Assert.That(oneSide1.NSide.ToArray(), Is.EquivalentTo(new[] { nSide1 }));

            Assert.That(oneSide1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(nSide1.ObjectState, Is.EqualTo(DataObjectState.Modified));
        }

        [Test]
        public void when_setting_N_side()
        {
            oneSide1.NSide.Add(nSide1);

            Assert.That(nSide1.OneSide.ID, Is.EqualTo(oneSide1.ID), "has not set OneSide correctly");
            Assert.That(nSide1.OneSide, Is.SameAs(oneSide1), "strange reference after setting OneSide");
            Assert.That(oneSide1.NSide.ToArray(), Is.EquivalentTo(new[] { nSide1 }));

            Assert.That(oneSide1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(nSide1.ObjectState, Is.EqualTo(DataObjectState.Modified));
        }

        [Test]
        public void when_resetting_1_side()
        {
            nSide1.OneSide = oneSide1;

            Assert.That(nSide1.OneSide, Is.SameAs(oneSide1), "Setting the first property destroyed the object reference");
            Assert.That(oneSide1.NSide, Is.EquivalentTo(new[] { nSide1 }), "first NSide list not correct");

            nSide1.OneSide = oneSide2;

            Assert.That(nSide1.OneSide, Is.SameAs(oneSide2), "Setting the second property destroyed the object reference");
            Assert.That(oneSide1.NSide, Is.Empty, "first NSide list was not cleared");
            Assert.That(oneSide2.NSide.ToArray(), Is.EquivalentTo(new[] { nSide1 }), "second NSide list not correct");

            Assert.That(oneSide1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(oneSide2.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(nSide1.ObjectState, Is.EqualTo(DataObjectState.Modified));
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
            Console.WriteLine("oneSide1.State={0}, nSide1.State={1}, nSide2.State={2}", oneSide1.ObjectState, nSide1.ObjectState, nSide2.ObjectState);
            oneSide1.NSide.Add(nSide1);
            Console.WriteLine("oneSide1.State={0}, nSide1.State={1}, nSide2.State={2}", oneSide1.ObjectState, nSide1.ObjectState, nSide2.ObjectState);

            Assert.That(nSide1.OneSide, Is.SameAs(oneSide1), "first parent: strange reference");
            Assert.That(oneSide1.NSide.ToArray(), Is.EquivalentTo(new[] { nSide1 }), "collection wrong after first Add");
            Assert.That(oneSide1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(nSide1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(nSide2.ObjectState, Is.EqualTo(DataObjectState.Unmodified));

            oneSide1.NSide.Remove(nSide1);

            Assert.That(nSide1.OneSide, Is.Null, "first parent not reset");
            Assert.That(oneSide1.NSide.ToArray(), Is.Empty, "collection not empty after Remove()");
            Assert.That(oneSide1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(nSide1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(nSide2.ObjectState, Is.EqualTo(DataObjectState.Unmodified));

            oneSide1.NSide.Add(nSide2);

            Assert.That(nSide1.OneSide, Is.Null, "first parent not reset");
            Assert.That(nSide2.OneSide, Is.SameAs(oneSide1), "second parent: strange reference");
            Assert.That(oneSide1.NSide.ToArray(), Is.EquivalentTo(new[] { nSide2 }), "collection wrong after second Add");

            Assert.That(oneSide1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(nSide1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(nSide2.ObjectState, Is.EqualTo(DataObjectState.Modified));
        }

        [Test]
        public void when_setting_N_side_with_clear()
        {
            oneSide1.NSide.Add(nSide1);

            Assert.That(nSide1.OneSide, Is.SameAs(oneSide1), "first parent: strange reference");
            Assert.That(oneSide1.NSide.ToArray(), Is.EquivalentTo(new[] { nSide1 }), "collection wrong after first Add");

            oneSide1.NSide.Clear();

            Assert.That(nSide1.OneSide, Is.Null, "first parent not reset");
            Assert.That(oneSide1.NSide.ToArray(), Is.Empty, "collection not empty after Clear()");

            oneSide1.NSide.Add(nSide2);

            Assert.That(nSide1.OneSide, Is.Null, "first parent not reset");
            Assert.That(nSide2.OneSide, Is.SameAs(oneSide1), "second parent: strange reference");
            Assert.That(oneSide1.NSide.ToArray(), Is.EquivalentTo(new[] { nSide2 }), "collection wrong after second Add");

            Assert.That(oneSide1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(nSide1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(nSide2.ObjectState, Is.EqualTo(DataObjectState.Modified));
        }
    }
}
