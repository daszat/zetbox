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
        private CustomActionsManagerAPITest currentCustomActionsManager;

        [SetUp]
        public override void SetUp()
        {

            currentCustomActionsManager = (CustomActionsManagerAPITest)ApplicationContext.Current.CustomActionsManager;
            currentCustomActionsManager.Reset();

            base.SetUp();
        }

        public void InitialiseObject(IKistlContext ctx, ObjectClass obj)
        {
            obj.BaseObjectClass = null;
            obj.ClassName = "testclassname";
            obj.DefaultIcon = null;
            obj.DefaultModel = null;
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
        public void should_have_events_attached_after_init()
        {
            Assert.That(currentCustomActionsManager.IsObjectAttached(obj), Is.True);
        }

        // TODO: WTF? Please explain
        [Test]
        [Ignore("Wrong test, ObjectState is managed by EF")]
        public void ObjectState_should_be_Unmodified_after_setting_ID()
        {
            obj.ID = 10;
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
        }

        [Test]
        [Ignore("Wrong test, ObjectState is managed by EF")]
        public void ObjectState_ObjectWithID_Modified()
        {
            obj.ID = 10;
            obj.ClientObjectState = DataObjectState.Unmodified;
            obj.NotifyPropertyChanged("test", null, null);
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
        }

        [Test]
        public void NotifyPropertyChanged_ing()
        {
            obj.NotifyPropertyChanging("StringProp", null, null);
            obj.ClassName = "test";
            obj.NotifyPropertyChanged("StringProp", null, null);
        }

        [Test]
        public void should_roundtrip_ObjectClass_attributes_correctly()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                MemoryStream ms = new MemoryStream();
                BinaryWriter sw = new BinaryWriter(ms);
                BinaryReader sr = new BinaryReader(ms);

                InitialiseObject(ctx, obj);

                var result = SerializationRoundtrip(obj);

                Assert.That(result.BaseObjectClass, Is.EqualTo(obj.BaseObjectClass));
                Assert.That(result.ClassName, Is.EqualTo(obj.ClassName));
                Assert.That(result.DefaultIcon, Is.EqualTo(obj.DefaultIcon));
                Assert.That(result.DefaultModel, Is.EqualTo(obj.DefaultModel));
                Assert.That(result.Description, Is.EqualTo(obj.Description));
                Assert.That((ICollection)result.ImplementsInterfaces, Is.EquivalentTo((ICollection)obj.ImplementsInterfaces));
                Assert.That(result.IsFrozenObject, Is.EqualTo(obj.IsFrozenObject));
                Assert.That(result.IsSimpleObject, Is.EqualTo(obj.IsSimpleObject));
                Assert.That((ICollection)result.MethodInvocations, Is.EquivalentTo((ICollection)obj.MethodInvocations));
                Assert.That((ICollection)result.Methods, Is.EquivalentTo((ICollection)obj.Methods));
                Assert.That(result.fk_Module, Is.EqualTo(obj.fk_Module));
                Assert.That((ICollection)result.Properties, Is.EquivalentTo((ICollection)obj.Properties));
                Assert.That((ICollection)result.SubClasses, Is.EquivalentTo((ICollection)obj.SubClasses));
                Assert.That(result.TableName, Is.EqualTo(obj.TableName));

            }
        }

        [Test]
        public void ToStream_creates_correct_Stream()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                MemoryStream ms = new MemoryStream();
                BinaryWriter sw = new BinaryWriter(ms);
                BinaryReader sr = new BinaryReader(ms);

                InitialiseObject(ctx, obj);
                obj.ToStream(sw);

                Assert.That(ms.Length, Is.GreaterThan(0));
                ms.Seek(0, SeekOrigin.Begin);

                Assert.Ignore("need to implement mocked serialization for ObjectClass");
                //TestObjClassSerializationMock.AssertCorrectContents<TestObjClass, int>(sr);
            }
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

            obj.ToStream(sw);

            Assert.That(ms.Length, Is.GreaterThan(0));

            ms.Seek(0, SeekOrigin.Begin);

            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var result = ctx.Create<ObjectClass>();
                result.FromStream(sr);
            }
        }

        [Test]
        public void AttachToContext()
        {
            Assert.That(obj.Context, Is.Null);
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                obj.AttachToContext(ctx);
                Assert.That(obj.Context, Is.Not.Null);
                Assert.That(obj.EntityState, Is.EqualTo(System.Data.EntityState.Detached));
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AttachToContext_Other_fails()
        {
            Assert.That(obj.Context, Is.Null);
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                obj.AttachToContext(ctx);
                Assert.That(obj.Context, Is.Not.Null);
                Assert.That(obj.EntityState, Is.EqualTo(System.Data.EntityState.Detached));
                using (IKistlContext ctx2 = KistlContext.GetContext())
                {
                    obj.AttachToContext(ctx2);
                    Assert.That(obj.Context, Is.Not.Null);
                    Assert.That(obj.EntityState, Is.EqualTo(System.Data.EntityState.Detached));
                }
            }
        }

        [Test]
        public void DetachFromContext()
        {
            Assert.That(obj.Context, Is.Null);
            obj.ID = 10;
            obj.ClientObjectState = DataObjectState.Unmodified;
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                ctx.Attach(obj);
                Assert.That(obj.Context, Is.Not.Null);

                obj.DetachFromContext(ctx);
                Assert.That(obj.Context, Is.Null);

                Assert.That(obj.EntityState, Is.EqualTo(System.Data.EntityState.Unchanged));
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DetachFromContext_Other()
        {
            Assert.That(obj.Context, Is.Null);
            obj.ID = 10;
            obj.ClientObjectState = DataObjectState.Unmodified;
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                ctx.Attach(obj);
                Assert.That(obj.Context, Is.Not.Null);

                obj.DetachFromContext(ctx);
                Assert.That(obj.Context, Is.Null);
                Assert.That(obj.EntityState, Is.EqualTo(System.Data.EntityState.Unchanged));

                using (IKistlContext ctx2 = KistlContext.GetContext())
                {
                    obj.DetachFromContext(ctx2);
                    Assert.That(obj.Context, Is.Null);
                    Assert.That(obj.EntityState, Is.EqualTo(System.Data.EntityState.Unchanged));
                }
            }
        }
    }
}
