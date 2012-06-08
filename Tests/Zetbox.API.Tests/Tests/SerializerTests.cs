
namespace Kistl.API.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Kistl.API.AbstractConsumerTests;
    using Kistl.API.Configuration;
    using Kistl.API.Mocks;
    using Kistl.API.Utils;
    using NUnit.Framework;
    using NUnit.Framework.Constraints;

    public static class SerializerTests
    {
        public class RoundtripSerializerTests : SerializerTestFixture
        {
            [Test]
            public void should_roundtrip_Boolean()
            {
                TestStream<bool>(v => sw.Write(v),
                    () => sr.ReadBoolean(),
                    true, false);
            }

            [Test]
            public void should_roundtrip_NullableBoolean()
            {
                TestStream<bool?>(v => sw.Write(v),
                    () => sr.ReadNullableBoolean(),
                    true, false, null);
            }

            [Test]
            public void should_roundtrip_DateTime()
            {
                TestStream<DateTime>(
                    v => sw.Write(v),
                    () => sr.ReadDateTime(),
                    DateTime.Now, DateTime.Today, DateTime.UtcNow, DateTime.Parse("2008-01-02"));
            }

            [Test]
            public void should_roundtrip_NullableDateTime()
            {
                TestStream<DateTime?>(
                    v => sw.Write(v),
                    () => sr.ReadNullableDateTime(),
                    DateTime.Now, DateTime.Today, DateTime.UtcNow, DateTime.Parse("2008-01-02"), null);
            }

            [Test]
            public void should_roundtrip_int()
            {
                TestStream<int>(v => sw.Write(v),
                    () => sr.ReadInt32(),
                    Int32.MinValue, -257, -256, -255, -1, 0, 1, 255, 256, 257, Int32.MaxValue);
            }

            [Test]
            public void should_fail_on_reading_from_stream_with_null_converter()
            {
                Assert.That(() => sr.ReadConverter((Action<int>)null), Throws.InstanceOf<ArgumentNullException>());
            }

            [Test]
            public void should_roundtrip_nullable_int()
            {
                TestStream<int?>(v => sw.Write(v),
                    () => sr.ReadNullableInt32(),
                    Int32.MinValue, -257, -256, -255, -1, 0, 1, 255, 256, 257, Int32.MaxValue, null);
            }

            [Test]
            public void should_roundtrip_decimals()
            {
                TestStream<decimal>(v => sw.Write(v),
                    () => sr.ReadDecimal(),
                    decimal.MinValue, -257.12m, -256.0m, -255m, -1m, 0m, 1m, 255m, 256m, 257.092m, decimal.MaxValue);
            }

            [Test]
            public void should_roundtrip_nullable_decimals()
            {
                TestStream<decimal?>(v => sw.Write(v),
                    () => sr.ReadNullableDecimal(),
                    decimal.MinValue, -257.45m, -256m, -255.18m, -1m, 0m, 1m, 255m, 256.078m, 257m, decimal.MaxValue, null);
            }

            [Test]
            public void should_roundtrip_doubles()
            {
                TestStream<double>(v => sw.Write(v),
                    () => sr.ReadDouble(),
                    double.NegativeInfinity, double.MinValue, -257.12d, -256.0d, -255d, -1d, 0d, double.Epsilon, 1d, 255d, 256d, 257.092d, double.MaxValue, double.PositiveInfinity, double.NaN);
            }

            [Test]
            public void should_roundtrip_nullable_doubles()
            {
                TestStream<double?>(v => sw.Write(v),
                    () => sr.ReadNullableDouble(),
                    double.NegativeInfinity, double.MinValue, -257.12d, -256.0d, -255d, -1d, 0d, double.Epsilon, 1d, 255d, 256d, 257.092d, double.MaxValue, double.PositiveInfinity, double.NaN, null);
            }

            [Test]
            public void should_roundtrip_floats()
            {
                TestStream<float>(v => sw.Write(v),
                    () => sr.ReadFloat(),
                    float.Epsilon, float.MaxValue, float.MinValue, float.NaN, float.NegativeInfinity, float.PositiveInfinity,
                    0.0f, 0.1f, 1.0f, 10.0f,
                    -0.1f, -1.0f, -10.0f);
            }

            [Test]
            public void should_roundtrip_nullable_floats()
            {
                TestStream<float?>(v => sw.Write(v),
                    () => sr.ReadNullableFloat(),
                    float.Epsilon, float.MaxValue, float.MinValue, float.NaN, float.NegativeInfinity, float.PositiveInfinity,
                    0.0f, 0.1f, 1.0f, 10.0f,
                    -0.1f, -1.0f, -10.0f, null);
            }

            [Test]
            public void should_roundtrip_strings()
            {
                TestStream<string>(v => sw.Write(v),
                    () => sr.ReadString(),
                    "Hello, World!", "", null);
            }
        }

        [TestFixture]
        public class when_serializing_SerializableTypes : SerializerTestFixture
        {
            // InterfaceType needs configured AppCtx, thus cannot be created by NUnit Runner before
            // executing the tests (whose setup will create the AppCtx)
            public IEnumerable<SerializableType> CreateSerializableTypes()
            {
                return new[] {
					iftFactory(typeof(TestDataObject)).ToSerializableType()
				};
            }

            [Test]
            public void should_roundtrip()
            {
                foreach (SerializableType value in CreateSerializableTypes())
                {
                    int preGuardValue = 0x0eadbeef;
                    int postGuardValue = 0x00c0ffee;

                    sw.Write(preGuardValue);
                    sw.Write(value);
                    sw.Write(postGuardValue);
                    sw.Flush();

                    ms.Seek(0, SeekOrigin.Begin);

                    Assert.That(sr.ReadInt32(), Is.EqualTo(preGuardValue), "inital guard wrong");

                    SerializableType fromval;
                    sr.Read(out fromval);
                    Assert.That(sr.ReadInt32(), Is.EqualTo(postGuardValue), "final guard wrong");

                    Assert.That(fromval, Is.EqualTo(value));
                    Assert.That(ms.Position, Is.EqualTo(ms.Length), "stream not read completely");
                }
            }

            [Test]
            public void should_not_accept_nulls()
            {
                Assert.That(() => sw.Write((SerializableType)null), Throws.InstanceOf<ArgumentNullException>());
            }
        }

        [TestFixture]
        [Ignore("Unable to serialize a IQueryable, extract the where clause and try again")]
        public class when_serializing_SerializableExpressions : SerializerTestFixture
        {
            [Test]
            public void should_roundtrip_expressions()
            {
                var value = CreateExpression();

                int preGuardValue = 0x0eadbeef;
                int postGuardValue = 0x00c0ffee;

                sw.Write(preGuardValue);
                sw.Write(value);
                sw.Write(postGuardValue);
                sw.Flush();

                ms.Seek(0, SeekOrigin.Begin);

                Assert.That(sr.ReadInt32(), Is.EqualTo(preGuardValue), "inital guard wrong");

                SerializableExpression fromval;
                sr.Read(out fromval, null);
                Assert.That(sr.ReadInt32(), Is.EqualTo(postGuardValue), "final guard wrong");

                Assert.That(fromval, Is.InstanceOf(value.GetType()));
                Assert.That(ms.Position, Is.EqualTo(ms.Length), "stream not read completely");
            }

            [Test]
            public void should_roundtrip_nulls()
            {
                SerializableExpression value = null;

                int preGuardValue = 0x0eadbeef;
                int postGuardValue = 0x00c0ffee;

                sw.Write(preGuardValue);
                sw.Write(value);
                sw.Write(postGuardValue);
                sw.Flush();

                ms.Seek(0, SeekOrigin.Begin);

                Assert.That(sr.ReadInt32(), Is.EqualTo(preGuardValue), "inital guard wrong");

                SerializableExpression fromval;
                sr.Read(out fromval, null);
                Assert.That(sr.ReadInt32(), Is.EqualTo(postGuardValue), "final guard wrong");

                Assert.That(fromval, Is.Null);
                Assert.That(ms.Position, Is.EqualTo(ms.Length), "stream not read completely");
            }

            /// <summary>
            /// Test just serialization, not handling interfaces!
            /// </summary>
            [Test]
            public void should_serialize_complete_expressions()
            {
                TestStream<SerializableExpression>(
                    v => sw.Write(v),
                    () => sr.ReadSerializableExpression(null),
                    CreateExpression());
            }

            private SerializableExpression CreateExpression()
            {
                // Cannot use another KistlObject because in this tests we do not do any type transformations
                //TestDataObject obj = new TestDataObjectImpl();
                TestObj obj2 = new TestObj();
                TestQuery<TestDataObject> ctx = new TestQuery<TestDataObject>();
                var list = from o in ctx
                           where o.IntProperty == 1
                           && o.IntProperty != 2
                           && o.IntProperty > 3
                               //&& o.IntProperty == obj.ID
                           && o.StringProperty == obj2.TestField
                               //&& o.StringProperty == obj.StringProperty
                           && o.StringProperty.StartsWith(obj2.TestField)
                           && (o.StringProperty.StartsWith("test") || o.StringProperty == "test")
                           && !o.BoolProperty
                           select new
                           {
                               o.IntProperty,
                               o.BoolProperty
                           };
                return Kistl.API.SerializableExpression.FromExpression(list.Expression, iftFactory);
            }
        }

        [TestFixture]
        public class when_serializing_CollectionEntries : SerializerTestFixture
        {
            [Test]
            public void can_roundtrip_list_with_no_entry()
            {
                TestToFromStreamCollectionEntries(new List<TestCollectionEntry>());
            }

            [Test]
            public void can_roundtrip_list_with_single_entry()
            {
                TestToFromStreamCollectionEntries(new List<TestCollectionEntry>(){
				                                  	new TestCollectionEntry() { ID = 10, TestName = "Test" }
				                                  });
            }

            [Test]
            public void can_roundtrip_list_with_multiple_entries()
            {
                TestToFromStreamCollectionEntries(new List<TestCollectionEntry>(){
				                                  	new TestCollectionEntry() { ID = 10, TestName = "Test1" },
				                                  	new TestCollectionEntry() { ID = 20, TestName = "Test2" },
				                                  	new TestCollectionEntry() { ID = 30, TestName = "Test3" },
				                                  	new TestCollectionEntry() { ID = 40, TestName = "Test4" }
				                                  });
            }

            private void TestToFromStreamCollectionEntries(List<TestCollectionEntry> toval)
            {
                using (var ctx = GetContext())
                {
                    if (toval != null) toval.ForEach(ce => ctx.Attach(ce));
                    var fromval = new List<TestCollectionEntry>();

                    TestStream<IEnumerable<TestCollectionEntry>>(
                        v => sw.WriteCollectionEntries(v),
                        () =>
                        {
                            sr.ReadCollectionEntries(null, fromval);
                            return fromval;
                        },
                        toval);

                    if (toval == null)
                    {
                        Assert.That(fromval, Is.Null);
                    }
                    else
                    {
                        Assert.That(fromval, Is.EquivalentTo(toval));
                    }
                }
            }

            [Test]
            public void can_roundtrip_observablelist()
            {
                List<TestCollectionEntry> toval;
                toval = new List<TestCollectionEntry>();
                toval.Add(new TestCollectionEntry() { ID = 20, TestName = "Hello" });

                using (var ctx = GetContext())
                {
                    toval.ForEach(ce => ctx.Attach(ce));
                    var fromvalobservable = new ObservableCollection<TestCollectionEntry>();

                    TestStream<IEnumerable<TestCollectionEntry>>(
                        v => sw.WriteCollectionEntries(v),
                        () =>
                        {
                            sr.ReadCollectionEntries(null, fromvalobservable);
                            return fromvalobservable;
                        },
                        toval);
                }
            }
        }

        [TestFixture]
        public class when_serializing_CompoundObjects : SerializerTestFixture
        {
            [Test]
            public void should_roundtrip()
            {
                const string testString = "muh";
                TestStream<TestCompoundObjectImpl>(
                    v => sw.Write(v),
                    () => sr.ReadCompoundObject<TestCompoundObjectImpl>(),
                    new TestCompoundObjectImpl() { TestProperty = testString }, null);
            }
        }
    }
}
