namespace Zetbox.DalProvider.Ef.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Server;
    using Zetbox.API.Tests.Skeletons;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.DalProvider.Ef.Mocks;
    using NUnit.Framework;

    [TestFixture]
    public class BaseServerDataObjectTests
        : IDataObjectTests<ObjectClassEfImpl>
    {
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
        }

        public void InitialiseObject(IZetboxContext ctx, ObjectClass obj)
        {
            ctx.Attach(obj);
            obj.BaseObjectClass = null;
            obj.Name = "testclassname";
            obj.DefaultIcon = null;
            obj.Description = "testclassdescription";
            obj.ImplementsInterfaces.Clear();
            obj.IsFrozenObject = false;
            obj.IsSimpleObject = false;
            obj.Methods.Clear();
            obj.Module = ctx.GetQuery<Zetbox.App.Base.Module>().First();
            obj.Properties.Clear();
            obj.SubClasses.Clear();
            obj.TableName = "testtablename";
        }

        [Test]
        [Ignore("Enable if Case #1359 is fixed")]
        public void should_roundtrip_ObjectClass_attributes_correctly()
        {
            using (IZetboxContext ctx = GetContext())
            {
                InitialiseObject(ctx, obj);

                var result = SerializationRoundtrip(obj);

                Assert.That(result.BaseObjectClass, Is.EqualTo(obj.BaseObjectClass));
                Assert.That(result.Name, Is.EqualTo(obj.Name));
                Assert.That(result.DefaultIcon, Is.EqualTo(obj.DefaultIcon));
                Assert.That(result.Description, Is.EqualTo(obj.Description));
                Assert.That((ICollection)result.ImplementsInterfaces, Is.EquivalentTo((ICollection)obj.ImplementsInterfaces));
                Assert.That(result.IsFrozenObject, Is.EqualTo(obj.IsFrozenObject));
                Assert.That(result.IsSimpleObject, Is.EqualTo(obj.IsSimpleObject));
                Assert.That((ICollection)result.Methods, Is.EquivalentTo((ICollection)obj.Methods));
                // Cannot test ObjectReferences -> no Context available.
                // Assert.That(nullResult.Module, Is.EqualTo(obj.Module));
                Assert.That((ICollection)result.Properties, Is.EquivalentTo((ICollection)obj.Properties));
                Assert.That((ICollection)result.SubClasses, Is.EquivalentTo((ICollection)obj.SubClasses));
                Assert.That(result.TableName, Is.EqualTo(obj.TableName));
            }
        }

        [Test]
        public void ToStream_creates_correct_Stream()
        {
            var typeMap = scope.Resolve<TypeMap>();
            using (var ms = new MemoryStream())
            using (var sw = new ZetboxStreamWriter(typeMap, new BinaryWriter(ms)))
            using (var sr = new ZetboxStreamReader(typeMap, new BinaryReader(ms)))
            {
                InitialiseObject(ctx, obj);
                obj.ToStream(sw, null, false);

                Assert.That(ms.Length, Is.GreaterThan(0));
                ms.Seek(0, SeekOrigin.Begin);

                Assert.Ignore("need to implement mocked serialization for ObjectClass");
                //TestObjClassSerializationMock.AssertCorrectContents<TestObjClass, int>(sr);
            }
        }

        [Test]
        public void FromStream_creates_correct_Object()
        {
            var typeMap = scope.Resolve<TypeMap>();
            using (var ms = new MemoryStream())
            using (var sw = new ZetboxStreamWriter(typeMap, new BinaryWriter(ms)))
            using (var sr = new ZetboxStreamReader(typeMap, new BinaryReader(ms)))
            {
                Assert.Ignore("need to implement mocked serialization for ObjectClass");

                //TestObjClassSerializationMock.ToStream<TestObjClass, int>(sw);
                sw.Flush();

                Assert.That(ms.Length, Is.GreaterThan(0));
                ms.Seek(0, SeekOrigin.Begin);

                obj.FromStream(sr);

                //TestObjClassSerializationMock.AssertCorrectContentsInt<TestObjClass>(objImpl);
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FromStream_Attached_fails()
        {
            var typeMap = scope.Resolve<TypeMap>();
            using (var ms = new MemoryStream())
            using (var sw = new ZetboxStreamWriter(typeMap, new BinaryWriter(ms)))
            using (var sr = new ZetboxStreamReader(typeMap, new BinaryReader(ms)))
            {
                obj.ToStream(sw, null, false);

                Assert.That(ms.Length, Is.GreaterThan(0));

                ms.Seek(0, SeekOrigin.Begin);

                using (var ctx = GetContext())
                {
                    var result = ctx.Create<ObjectClass>();
                    result.FromStream(sr);
                }
            }
        }

        [Test]
        public void AttachToContext()
        {
            var local_obj = CreateObjectInstance();
            Assert.That(local_obj.Context, Is.Null);
            using (IZetboxContext ctx = GetContext())
            {
                local_obj.AttachToContext(ctx);
                Assert.That(local_obj.Context, Is.Not.Null);
                Assert.That(local_obj.EntityState, Is.EqualTo(System.Data.EntityState.Detached));
            }
        }

        [Test]
        [ExpectedException(typeof(WrongZetboxContextException))]
        public void AttachToContext_Other_fails()
        {
            var local_obj = CreateObjectInstance();
            Assert.That(local_obj.Context, Is.Null);
            using (IZetboxContext ctx = GetContext())
            {
                local_obj.AttachToContext(ctx);
                Assert.That(local_obj.Context, Is.Not.Null);
                Assert.That(local_obj.EntityState, Is.EqualTo(System.Data.EntityState.Detached));
                using (IZetboxContext ctx2 = GetContext())
                {
                    local_obj.AttachToContext(ctx2);
                    Assert.That(local_obj.Context, Is.Not.Null);
                    Assert.That(local_obj.EntityState, Is.EqualTo(System.Data.EntityState.Detached));
                }
            }
        }
    }
}
