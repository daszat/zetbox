// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.DalProvider.Ef.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.AbstractConsumerTests;
    using Zetbox.API.Server;
    using Zetbox.App.Base;
    using Zetbox.App.Projekte;
    using Zetbox.App.Test;
    using Zetbox.DalProvider.Ef.Mocks;
    using NUnit.Framework;

    [TestFixture]
    public class EfContextTests : AbstractContextTests
    {
        [Test]
        public void Attach_IDataObject_New()
        {
            using (IZetboxContext ctx = GetContext())
            {
                TestObjClass obj = new TestObjClassEfImpl(null);
                Assert.That(((TestObjClassEfImpl)obj).EntityState, Is.EqualTo(EntityState.Detached));
                ctx.Internals().AttachAsNew(obj);
                Assert.That(((TestObjClassEfImpl)obj).EntityState, Is.EqualTo(EntityState.Added));
            }
        }
        //[Test]
        //public void Attach_IDataObject_New_WithGraph()
        //{
        //    using (IZetboxContext ctx = GetContext())
        //    {
        //        TestObjClass obj = new TestObjClassImpl();
        //        obj.TestNames.Add("Test");
        //        obj.TestNames.Add("Test2");
        //        Assert.That(((TestObjClassImpl)obj).EntityState, Is.EqualTo(EntityState.Detached));
        //        ctx.Attach(obj);
        //        Assert.That(((TestObjClassImpl)obj).EntityState, Is.EqualTo(EntityState.Added));
        //        Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(3));
        //    }
        //}

        [Test]
        public void Attach_IDataObject_Existing()
        {
            using (IZetboxContext ctx = GetContext())
            {
                TestObjClass obj = new TestObjClassEfImpl(null) { ID = 3, ClientObjectState = DataObjectState.Unmodified };
                Assert.That(((TestObjClassEfImpl)obj).EntityState, Is.EqualTo(EntityState.Detached));
                ctx.Attach(obj);
                Assert.That(((TestObjClassEfImpl)obj).EntityState, Is.EqualTo(EntityState.Unchanged));
            }
        }

        [Test]
        public void Attach_IDataObject_Existing_Twice()
        {
            using (IZetboxContext ctx = GetContext())
            {
                TestObjClass obj = new TestObjClassEfImpl(null) { ID = 3, ClientObjectState = DataObjectState.Unmodified };
                Assert.That(((TestObjClassEfImpl)obj).EntityState, Is.EqualTo(EntityState.Detached));
                ctx.Attach(obj);
                Assert.That(((TestObjClassEfImpl)obj).EntityState, Is.EqualTo(EntityState.Unchanged));
                ctx.Attach(obj);
                Assert.That(((TestObjClassEfImpl)obj).EntityState, Is.EqualTo(EntityState.Unchanged));
            }
        }

        [Test]
        public void Attach_IDataObject_Existing_Twice_But_Different()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                using (IZetboxContext ctx = GetContext())
                {
                    TestObjClass obj1 = new TestObjClassEfImpl(null) { ID = 3, ClientObjectState = DataObjectState.Unmodified };
                    Assert.That(((TestObjClassEfImpl)obj1).EntityState, Is.EqualTo(EntityState.Detached));
                    ctx.Attach(obj1);
                    Assert.That(((TestObjClassEfImpl)obj1).EntityState, Is.EqualTo(EntityState.Unchanged));

                    TestObjClass obj2 = new TestObjClassEfImpl(null) { ID = 3, ClientObjectState = DataObjectState.Unmodified };
                    Assert.That(((TestObjClassEfImpl)obj2).EntityState, Is.EqualTo(EntityState.Detached));
                    ctx.Attach(obj2);
                    Assert.That(((TestObjClassEfImpl)obj2).EntityState, Is.EqualTo(EntityState.Unchanged));
                }
            });
        }

        //[Test]
        //public void Attach_ICollectionEntry_New()
        //{
        //    using (IZetboxContext ctx = GetContext())
        //    {
        //        TestObjClass_TestNameCollectionEntryImpl obj = new TestObjClass_TestNameCollectionEntryImpl();
        //        Assert.That(obj.EntityState, Is.EqualTo(EntityState.Detached));
        //        ctx.Attach(obj);
        //        Assert.That(obj.EntityState, Is.EqualTo(EntityState.Added));
        //    }
        //}

        //[Test]
        //public void Attach_ICollectionEntry_Existing()
        //{
        //    using (IZetboxContext ctx = GetContext())
        //    {
        //        TestObjClass_TestNameCollectionEntryImpl obj = new TestObjClass_TestNameCollectionEntryImpl() { ID = 15 };
        //        Assert.That(obj.EntityState, Is.EqualTo(EntityState.Detached));
        //        ctx.Attach(obj);
        //        Assert.That(obj.EntityState, Is.EqualTo(EntityState.Unchanged));
        //    }
        //}

        //[Test]
        //public void Attach_ICollectionEntry_Existing_Twice()
        //{
        //    using (IZetboxContext ctx = GetContext())
        //    {
        //        TestObjClass_TestNameCollectionEntryImpl obj = new TestObjClass_TestNameCollectionEntryImpl() { ID = 3 };
        //        Assert.That(obj.EntityState, Is.EqualTo(EntityState.Detached));
        //        ctx.Attach(obj);
        //        Assert.That(obj.EntityState, Is.EqualTo(EntityState.Unchanged));
        //        ctx.Attach(obj);
        //        Assert.That(obj.EntityState, Is.EqualTo(EntityState.Unchanged));
        //    }
        //}

        //[Test]
        //[ExpectedException(typeof(InvalidOperationException))]
        //public void Attach_ICollectionEntry_Existing_Twice_But_Different()
        //{
        //    using (IZetboxContext ctx = GetContext())
        //    {
        //        TestObjClass_TestNameCollectionEntryImpl obj1 = new TestObjClass_TestNameCollectionEntryImpl() { ID = 3 };
        //        Assert.That(obj1.EntityState, Is.EqualTo(EntityState.Detached));
        //        ctx.Attach(obj1);
        //        Assert.That(obj1.EntityState, Is.EqualTo(EntityState.Unchanged));

        //        TestObjClass_TestNameCollectionEntryImpl obj2 = new TestObjClass_TestNameCollectionEntryImpl() { ID = 3 };
        //        Assert.That(obj2.EntityState, Is.EqualTo(EntityState.Detached));
        //        ctx.Attach(obj2);
        //        Assert.That(obj2.EntityState, Is.EqualTo(EntityState.Unchanged));
        //    }
        //}

        [Test]
        public void AttachedObjects()
        {
            using (IZetboxContext ctx = GetContext())
            {
                TestObjClass obj = new TestObjClassEfImpl(null);
                ctx.Attach(obj);
                ctx.Create<TestObjClass>();

                Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(2));
            }
        }

        [Test]
        public void ContainsObject()
        {
            using (IZetboxContext ctx = GetContext())
            {
                TestObjClass obj = new TestObjClassEfImpl(null) { ID = 10 };
                ctx.Attach(obj);
                ctx.Create<TestObjClass>();
                Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(2));

                Assert.That(ctx.ContainsObject(ctx.GetInterfaceType(obj), obj.ID), Is.EqualTo(obj));
            }
        }

        [Test]
        public void ContainsObject_Not()
        {
            using (IZetboxContext ctx = GetContext())
            {
                TestObjClass obj = new TestObjClassEfImpl(null) { ID = 10 };
                ctx.Create<TestObjClass>();
                Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(1));

                Assert.That(ctx.ContainsObject(ctx.GetInterfaceType(obj), obj.ID), Is.Null);
            }
        }
        [Test]
        public void Detach_IDataObject_Failed()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                using (IZetboxContext ctx = GetContext())
                {
                    ctx.Detach(new TestObjClassEfImpl(null));
                }
            });
        }

        [Test]
        public void Detach_IDataObject()
        {
            using (IZetboxContext ctx = GetContext())
            {
                TestObjClass obj = ctx.GetQuery<TestObjClass>().First();
                ctx.Detach(obj);
                Assert.That(((TestObjClassEfImpl)obj).EntityState, Is.EqualTo(EntityState.Detached));
            }
        }


        //[Test]
        //public void Detach_ICollectionEntry()
        //{
        //    using (IZetboxContext ctx = GetContext())
        //    {
        //        var obj = ctx.Find<TestObjClass>(1);
        //        Assert.That(obj, Is.Not.Null);
        //        Assert.That(obj.TestNames.Count, Is.EqualTo(2));

        //        TestObjClass_TestNameCollectionEntryImpl c = ((TestObjClassImpl)obj).TestNamesImpl.First();
        //        ctx.Detach(c);
        //        Assert.That(c.EntityState, Is.EqualTo(EntityState.Detached));
        //        Assert.That(obj.TestNames.Count, Is.EqualTo(1));
        //    }
        //}
    }
}
