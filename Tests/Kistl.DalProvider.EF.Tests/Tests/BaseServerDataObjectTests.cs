using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using Kistl.API.Tests.Skeletons;
using Kistl.App.Base;
using Kistl.DalProvider.EF.Mocks;

using NUnit.Framework;

namespace Kistl.DalProvider.EF.Tests
{
    [TestFixture]
    public class BaseServerDataObjectTests 
        : IDataObjectTests<ObjectClass__Implementation__>
    {
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
        }

        public void InitialiseObject(IKistlContext ctx, ObjectClass obj)
        {
            ctx.Attach(obj);
            obj.BaseObjectClass = null;
            obj.Name = "testclassname";
            obj.DefaultIcon = null;
            obj.Description = "testclassdescription";
            obj.ImplementsInterfaces.Clear();
            obj.IsFrozenObject = false;
            obj.IsSimpleObject = false;
            obj.MethodInvocations.Clear();
            obj.Methods.Clear();
            obj.Module = ctx.GetQuery<Module>().First();
            obj.Properties.Clear();
            obj.SubClasses.Clear();
            obj.TableName = "testtablename";
        }

        [Test]
        [Ignore("Enable if Case #1359 is fixed")]
        public void should_roundtrip_ObjectClass_attributes_correctly()
        {
            using (IKistlContext ctx = GetContext())
            {
                MemoryStream ms = new MemoryStream();
                BinaryWriter sw = new BinaryWriter(ms);
                BinaryReader sr = new BinaryReader(ms);

                InitialiseObject(ctx, obj);

                var result = SerializationRoundtrip(obj);

                Assert.That(result.BaseObjectClass, Is.EqualTo(obj.BaseObjectClass));
                Assert.That(result.Name, Is.EqualTo(obj.Name));
                Assert.That(result.DefaultIcon, Is.EqualTo(obj.DefaultIcon));
                Assert.That(result.Description, Is.EqualTo(obj.Description));
                Assert.That((ICollection)result.ImplementsInterfaces, Is.EquivalentTo((ICollection)obj.ImplementsInterfaces));
                Assert.That(result.IsFrozenObject, Is.EqualTo(obj.IsFrozenObject));
                Assert.That(result.IsSimpleObject, Is.EqualTo(obj.IsSimpleObject));
                Assert.That((ICollection)result.MethodInvocations, Is.EquivalentTo((ICollection)obj.MethodInvocations));
                Assert.That((ICollection)result.Methods, Is.EquivalentTo((ICollection)obj.Methods));
                // Cannot test ObjectReferences -> no Context available.
                // Assert.That(result.Module, Is.EqualTo(obj.Module));
                Assert.That((ICollection)result.Properties, Is.EquivalentTo((ICollection)obj.Properties));
                Assert.That((ICollection)result.SubClasses, Is.EquivalentTo((ICollection)obj.SubClasses));
                Assert.That(result.TableName, Is.EqualTo(obj.TableName));

            }
        }

        [Test]
        public void ToStream_creates_correct_Stream()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter sw = new BinaryWriter(ms);
            BinaryReader sr = new BinaryReader(ms);

            InitialiseObject(ctx, obj);
            obj.ToStream(sw, null, false);

            Assert.That(ms.Length, Is.GreaterThan(0));
            ms.Seek(0, SeekOrigin.Begin);

            Assert.Ignore("need to implement mocked serialization for ObjectClass");
            //TestObjClassSerializationMock.AssertCorrectContents<TestObjClass, int>(sr);
        }

        [Test]
        public void FromStream_creates_correct_Object()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter sw = new BinaryWriter(ms, UTF8Encoding.UTF8);
            BinaryReader sr = new BinaryReader(ms, UTF8Encoding.UTF8);

            Assert.Ignore("need to implement mocked serialization for ObjectClass");

            //TestObjClassSerializationMock.ToStream<TestObjClass, int>(sw);
            sw.Flush();

            Assert.That(ms.Length, Is.GreaterThan(0));
            ms.Seek(0, SeekOrigin.Begin);

            obj.FromStream(sr);

            //TestObjClassSerializationMock.AssertCorrectContentsInt<TestObjClass>(objImpl);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FromStream_Attached_fails()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter sw = new BinaryWriter(ms);
            BinaryReader sr = new BinaryReader(ms);

            obj.ToStream(sw, null, false);

            Assert.That(ms.Length, Is.GreaterThan(0));

            ms.Seek(0, SeekOrigin.Begin);

            using (IKistlContext ctx = GetContext())
            {
                var result = ctx.Create<ObjectClass>();
                result.FromStream(sr);
            }
        }

        [Test]
        public void AttachToContext()
        {
            var local_obj = CreateObjectInstance();
            Assert.That(local_obj.Context, Is.Null);
            using (IKistlContext ctx = GetContext())
            {
                local_obj.AttachToContext(ctx);
                Assert.That(local_obj.Context, Is.Not.Null);
                Assert.That(local_obj.EntityState, Is.EqualTo(System.Data.EntityState.Detached));
            }
        }

        [Test]
        [ExpectedException(typeof(WrongKistlContextException))]
        public void AttachToContext_Other_fails()
        {
            var local_obj = CreateObjectInstance();
            Assert.That(local_obj.Context, Is.Null);
            using (IKistlContext ctx = GetContext())
            {
                local_obj.AttachToContext(ctx);
                Assert.That(local_obj.Context, Is.Not.Null);
                Assert.That(local_obj.EntityState, Is.EqualTo(System.Data.EntityState.Detached));
                using (IKistlContext ctx2 = GetContext())
                {
                    local_obj.AttachToContext(ctx2);
                    Assert.That(local_obj.Context, Is.Not.Null);
                    Assert.That(local_obj.EntityState, Is.EqualTo(System.Data.EntityState.Detached));
                }
            }
        }

        [Test]
        public void DetachFromContext()
        {
            Assert.That(obj.Context, Is.Not.Null);
            obj.ID = 10;
            obj.ClientObjectState = DataObjectState.Unmodified;
            obj.DetachFromContext(ctx);
            Assert.That(obj.Context, Is.Null);
            Assert.That(obj.EntityState, Is.EqualTo(System.Data.EntityState.Added));
        }

        [Test]
        [ExpectedException(typeof(WrongKistlContextException))]
        public void DetachFromContext_Other()
        {
            Assert.That(obj.Context, Is.Not.Null);
            obj.ID = 10;
            obj.ClientObjectState = DataObjectState.Unmodified;
            obj.DetachFromContext(ctx);
            Assert.That(obj.Context, Is.Null);
            Assert.That(obj.EntityState, Is.EqualTo(System.Data.EntityState.Added));

            using (IKistlContext ctx2 = GetContext())
            {
                obj.DetachFromContext(ctx2);
                Assert.That(obj.Context, Is.Null);
                Assert.That(obj.EntityState, Is.EqualTo(System.Data.EntityState.Added));
            }
        }
    }
}
