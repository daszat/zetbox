using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;

using NUnit.Framework;
using Kistl.App.Test;

namespace Kistl.API.AbstractConsumerTests.PersistenceObjects
{
    public abstract class navigator_context_checks : ObjectLoadFixture
    {
        [Test]
        public void set_1_N_same_context()
        {
            var a = ctx.Create<TestCustomObject>();
            var b = ctx.Create<Muhblah>();
            a.MubBlah_Nav = b;
        }

        [Test]
        [ExpectedException(typeof(WrongKistlContextException))]
        public void set_1_N_wrong_context()
        {
            var otherCtx = GetContext();
            Assert.That(ctx, Is.Not.SameAs(otherCtx));
            var a = ctx.Create<TestCustomObject>();
            var b = otherCtx.Create<Muhblah>();
            a.MubBlah_Nav = b;
        }

        [Test]
        public void set_N_1_same_context()
        {
            var a = ctx.Create<TestCustomObject>();
            var b = ctx.Create<Muhblah>();
            b.TestCustomObjects_List_Nav.Add(a);
        }

        [Test]
        [ExpectedException(typeof(WrongKistlContextException))]
        public void set_N_1_wrong_context()
        {
            var otherCtx = GetContext();
            Assert.That(ctx, Is.Not.SameAs(otherCtx));
            var a = ctx.Create<TestCustomObject>();
            var b = otherCtx.Create<Muhblah>();
            b.TestCustomObjects_List_Nav.Add(a);
        }

        [Test]
        public void set_N_M_same_context()
        {
            var a = ctx.Create<TestCustomObject>();
            var b = ctx.Create<Muhblah>();
            b.TestCustomObjects_ManyList_Nav.Add(a);
        }

        [Test]
        [ExpectedException(typeof(WrongKistlContextException))]
        public void set_N_M_wrong_context()
        {
            var otherCtx = GetContext();
            Assert.That(ctx, Is.Not.SameAs(otherCtx));
            var a = ctx.Create<TestCustomObject>();
            var b = otherCtx.Create<Muhblah>();
            b.TestCustomObjects_ManyList_Nav.Add(a);
        }

        [Test]
        public void set_1_1_same_context()
        {
            var a = ctx.Create<TestCustomObject>();
            var b = ctx.Create<Muhblah>();
            a.MuhBlah_One_Nav = b;
        }

        [Test]
        [ExpectedException(typeof(WrongKistlContextException))]
        public void set_1_1_wrong_context()
        {
            var otherCtx = GetContext();
            Assert.That(ctx, Is.Not.SameAs(otherCtx));
            var a = ctx.Create<TestCustomObject>();
            var b = otherCtx.Create<Muhblah>();
            a.MuhBlah_One_Nav = b;
        }
    }
}
