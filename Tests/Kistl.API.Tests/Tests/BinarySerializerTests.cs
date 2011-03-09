
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

	using Kistl.API;
	using Kistl.API.Configuration;
	using Kistl.API.Mocks;

	using NUnit.Framework;
	using NUnit.Framework.Constraints;
    using Autofac;

	public class BinarySerializerTests : AbstractApiTestFixture
	{
		MemoryStream ms;
		BinaryWriter sw;
		BinaryReader sr;

		public override void SetUp()
		{
            base.SetUp();
            ms = new MemoryStream();
			sw = new BinaryWriter(ms);
			sr = new BinaryReader(ms);
		}

		[TestFixture]
		public class when_serializing_bools : BinarySerializerTests
		{
			[Datapoints]
			public readonly bool[] Values = new[] { true, false };

			[Theory]
			public void should_roundtrip(bool value)
			{
				BinarySerializer.ToStream(value, sw);
				ms.Seek(0, SeekOrigin.Begin);

				bool readValue;
				BinarySerializer.FromStream(out readValue, sr);
				Assert.That(readValue, Is.EqualTo(value));
			}

			[Theory]
			public void should_fail_on_writing_to_null_stream(bool value)
			{
				Assert.That(() =>  BinarySerializer.ToStream(value, null), Throws.InstanceOf<ArgumentNullException>());
			}

			[Test]
			public void should_fail_on_reading_from_null_stream()
			{
				bool value;
				Assert.That(() =>  BinarySerializer.FromStream(out value, null), Throws.InstanceOf<ArgumentNullException>());
			}
		}

		[TestFixture]
		public class when_serializing_nullable_bools : BinarySerializerTests
		{
			[Datapoints]
			public readonly bool?[] Values = new bool?[] { true, false, null };

			[Theory]
			public void should_roundtrip(bool? value)
			{
				BinarySerializer.ToStream(value, sw);
				ms.Seek(0, SeekOrigin.Begin);

				bool? readValue;
				BinarySerializer.FromStream(out readValue, sr);
				Assert.That(readValue, Is.EqualTo(value));
			}

			[Theory]
			public void should_fail_on_writing_to_null_stream(bool? value)
			{
				Assert.That(() => BinarySerializer.ToStream(value, null), Throws.InstanceOf<ArgumentNullException>());
			}

			[Test]
			public void should_fail_on_reading_from_null_stream()
			{
				bool? value;
				Assert.That(() => BinarySerializer.FromStream(out value, null), Throws.InstanceOf<ArgumentNullException>());
			}
		}

		[TestFixture]
		public class when_serializing_DateTimes : BinarySerializerTests
		{
			[Datapoints]
			public readonly DateTime[] Values = new[] { DateTime.Now, DateTime.Today, DateTime.UtcNow, DateTime.Parse("2008-01-02") };

			[Theory]
			public void should_roundtrip(DateTime value)
			{
				BinarySerializer.ToStream(value, sw);
				ms.Seek(0, SeekOrigin.Begin);

				DateTime readValue;
				BinarySerializer.FromStream(out readValue, sr);
				Assert.That(readValue, Is.EqualTo(value));
			}

			[Theory]
			public void should_fail_on_writing_to_null_stream(DateTime value)
			{
				Assert.That(() => BinarySerializer.ToStream(value, null), Throws.InstanceOf<ArgumentNullException>());
			}

			[Test]
			public void should_fail_on_reading_from_null_stream()
			{
				DateTime value;
				Assert.That(() => BinarySerializer.FromStream(out value, null), Throws.InstanceOf<ArgumentNullException>());
			}
		}

		[TestFixture]
		public class when_serializing_nullable_DateTimes : BinarySerializerTests
		{
			[Datapoints]
			public readonly DateTime?[] Values = new DateTime?[] {
				DateTime.Now, DateTime.Today, DateTime.UtcNow, DateTime.Parse("2008-01-02"),
				null };

			[Theory]
			public void should_roundtrip(DateTime? value)
			{
				BinarySerializer.ToStream(value, sw);
				ms.Seek(0, SeekOrigin.Begin);

				DateTime? readValue;
				BinarySerializer.FromStream(out readValue, sr);
				Assert.That(readValue, Is.EqualTo(value));
			}

			[Theory]
			public void should_fail_on_writing_to_null_stream(DateTime? value)
			{
				Assert.That(() => BinarySerializer.ToStream(value, null), Throws.InstanceOf<ArgumentNullException>());
			}

			[Test]
			public void should_fail_on_reading_from_null_stream()
			{
				DateTime? value;
				Assert.That(() => BinarySerializer.FromStream(out value, null), Throws.InstanceOf<ArgumentNullException>());
			}
		}

		[TestFixture]
		public class when_serializing_ints : BinarySerializerTests
		{
			[Datapoints]
			public readonly int[] Values = new[] { Int32.MinValue, -257, -256, -255, -1, 0, 1, 255, 256, 257, Int32.MaxValue };

			[Theory]
			public void should_roundtrip(int value)
			{
				BinarySerializer.ToStream(value, sw);
				ms.Seek(0, SeekOrigin.Begin);

				int readValue;
				BinarySerializer.FromStream(out readValue, sr);
				Assert.That(readValue, Is.EqualTo(value));
			}

			[Theory]
			public void should_fail_on_writing_to_null_stream(int value)
			{
				Assert.That(() => BinarySerializer.ToStream(value, null), Throws.InstanceOf<ArgumentNullException>());
			}

			[Test]
			public void should_fail_on_reading_from_null_stream()
			{
				int value;
				Assert.That(() => BinarySerializer.FromStream(out value, null), Throws.InstanceOf<ArgumentNullException>());
			}

			[Test]
			public void should_fail_on_reading_from_stream_with_null_converter()
			{
				Assert.That(() => BinarySerializer.FromStreamConverter((Action<int>)null, sr), Throws.InstanceOf<ArgumentNullException>());
			}

			[Test]
			public void should_fail_on_reading_from_null_stream_with_converter()
			{
				int value = 0;
				Assert.That(() => BinarySerializer.FromStreamConverter(i => value = i, null), Throws.InstanceOf<ArgumentNullException>());
                Console.WriteLine("ignored value: {0}", value);
			}
		}

		[TestFixture]
		public class when_serializing_nullable_ints : BinarySerializerTests
		{
			[Datapoints]
			public readonly int?[] Values = new int?[] { Int32.MinValue, -257, -256, -255, -1, 0, 1, 255, 256, 257, Int32.MaxValue, null };

			[Theory]
			public void should_roundtrip(int? value)
			{
				BinarySerializer.ToStream(value, sw);
				ms.Seek(0, SeekOrigin.Begin);

				int? readValue;
				BinarySerializer.FromStream(out readValue, sr);
				Assert.That(readValue, Is.EqualTo(value));
			}

			[Theory]
			public void should_fail_on_writing_to_null_stream(int? value)
			{
				Assert.That(() => BinarySerializer.ToStream(value, null), Throws.InstanceOf<ArgumentNullException>());
			}

			[Test]
			public void should_fail_on_reading_from_null_stream()
			{
				int? value;
				Assert.That(() => BinarySerializer.FromStream(out value, null), Throws.InstanceOf<ArgumentNullException>());
			}
		}

        [TestFixture]
        public class when_serializing_decimals : BinarySerializerTests
        {
            [Datapoints]
            public readonly decimal[] Values = new[] { decimal.MinValue, -257.12m, -256.0m, -255m, -1m, 0m, 1m, 255m, 256m, 257.092m, decimal.MaxValue };

            [Theory]
            public void should_roundtrip(decimal value)
            {
                BinarySerializer.ToStream(value, sw);
                ms.Seek(0, SeekOrigin.Begin);

                decimal readValue;
                BinarySerializer.FromStream(out readValue, sr);
                Assert.That(readValue, Is.EqualTo(value));
            }

            [Theory]
            public void should_fail_on_writing_to_null_stream(decimal value)
            {
                Assert.That(() => BinarySerializer.ToStream(value, null), Throws.InstanceOf<ArgumentNullException>());
            }

            [Test]
            public void should_fail_on_reading_from_null_stream()
            {
                decimal value;
                Assert.That(() => BinarySerializer.FromStream(out value, null), Throws.InstanceOf<ArgumentNullException>());
            }
        }

        [TestFixture]
        public class when_serializing_nullable_decimals : BinarySerializerTests
        {
            [Datapoints]
            public readonly decimal?[] Values = new decimal?[] { decimal.MinValue, -257.45m, -256m, -255.18m, -1m, 0m, 1m, 255m, 256.078m, 257m, decimal.MaxValue, null };

            [Theory]
            public void should_roundtrip(decimal? value)
            {
                BinarySerializer.ToStream(value, sw);
                ms.Seek(0, SeekOrigin.Begin);

                decimal? readValue;
                BinarySerializer.FromStream(out readValue, sr);
                Assert.That(readValue, Is.EqualTo(value));
            }

            [Theory]
            public void should_fail_on_writing_to_null_stream(decimal? value)
            {
                Assert.That(() => BinarySerializer.ToStream(value, null), Throws.InstanceOf<ArgumentNullException>());
            }

            [Test]
            public void should_fail_on_reading_from_null_stream()
            {
                decimal? value;
                Assert.That(() => BinarySerializer.FromStream(out value, null), Throws.InstanceOf<ArgumentNullException>());
            }
        }

		[TestFixture]
		public class when_serializing_floats : BinarySerializerTests
		{
			[Datapoints]
			public readonly float[] Values = new[] {
				float.Epsilon, float.MaxValue, float.MinValue, float.NaN, float.NegativeInfinity, float.PositiveInfinity,
				0.0f, 0.1f, 1.0f, 10.0f,
				-0.1f, -1.0f, -10.0f
			};

			[Theory]
			public void should_roundtrip(float value)
			{
				BinarySerializer.ToStream(value, sw);
				ms.Seek(0, SeekOrigin.Begin);

				float readValue;
				BinarySerializer.FromStream(out readValue, sr);
				Assert.That(readValue, Is.EqualTo(value));
			}

			[Theory]
			public void should_fail_on_writing_to_null_stream(float value)
			{
				Assert.That(() => BinarySerializer.ToStream(value, null), Throws.InstanceOf<ArgumentNullException>());
			}

			[Test]
			public void should_fail_on_reading_from_null_stream()
			{
				float value;
				Assert.That(() => BinarySerializer.FromStream(out value, null), Throws.InstanceOf<ArgumentNullException>());
			}
		}

		[TestFixture]
		public class when_serializing_nullable_floats : BinarySerializerTests
		{
			[Datapoints]
			public readonly float?[] Values = new float?[] {
				float.Epsilon, float.MaxValue, float.MinValue, float.NaN, float.NegativeInfinity, float.PositiveInfinity,
				0.0f, 0.1f, 1.0f, 10.0f,
				-0.1f, -1.0f, -10.0f,
				null };

			[Theory]
			public void should_roundtrip(float? value)
			{
				BinarySerializer.ToStream(value, sw);
				ms.Seek(0, SeekOrigin.Begin);

				float? readValue;
				BinarySerializer.FromStream(out readValue, sr);
				Assert.That(readValue, Is.EqualTo(value));
			}

			[Theory]
			public void should_fail_on_writing_to_null_stream(float? value)
			{
				Assert.That(() => BinarySerializer.ToStream(value, null), Throws.InstanceOf<ArgumentNullException>());
			}

			[Test]
			public void should_fail_on_reading_from_null_stream()
			{
				float? value;
				Assert.That(() => BinarySerializer.FromStream(out value, null), Throws.InstanceOf<ArgumentNullException>());
			}
		}

		[TestFixture]
		public class when_serializing_doubles : BinarySerializerTests
		{
			[Datapoints]
			public readonly double[] Values = new[] {
				double.Epsilon, double.MaxValue, double.MinValue, double.NaN, double.NegativeInfinity, double.PositiveInfinity,
				0.0, 0.1, 1.0, 10.0,
				-0.1, -1.0, -10.0,
			};

			[Theory]
			public void should_roundtrip(double value)
			{
				BinarySerializer.ToStream(value, sw);
				ms.Seek(0, SeekOrigin.Begin);

				double readValue;
				BinarySerializer.FromStream(out readValue, sr);
				Assert.That(readValue, Is.EqualTo(value));
			}

			[Theory]
			public void should_fail_on_writing_to_null_stream(double value)
			{
				Assert.That(() => BinarySerializer.ToStream(value, null), Throws.InstanceOf<ArgumentNullException>());
			}

			[Test]
			public void should_fail_on_reading_from_null_stream()
			{
				double value;
				Assert.That(() => BinarySerializer.FromStream(out value, null), Throws.InstanceOf<ArgumentNullException>());
			}
		}

		[TestFixture]
		public class when_serializing_nullable_doubles : BinarySerializerTests
		{
			[Datapoints]
			public readonly double?[] Values = new double?[] {
				double.Epsilon, double.MaxValue, double.MinValue, double.NaN, double.NegativeInfinity, double.PositiveInfinity,
				0.0, 0.1, 1.0, 10.0,
				-0.1, -1.0, -10.0,
				null };

			[Theory]
			public void should_roundtrip(double? value)
			{
				BinarySerializer.ToStream(value, sw);
				ms.Seek(0, SeekOrigin.Begin);

				double? readValue;
				BinarySerializer.FromStream(out readValue, sr);
				Assert.That(readValue, Is.EqualTo(value));
			}

			[Theory]
			public void should_fail_on_writing_to_null_stream(double? value)
			{
				Assert.That(() => BinarySerializer.ToStream(value, null), Throws.InstanceOf<ArgumentNullException>());
			}

			[Test]
			public void should_fail_on_reading_from_null_stream()
			{
				double? value;
				Assert.That(() => BinarySerializer.FromStream(out value, null), Throws.InstanceOf<ArgumentNullException>());
			}
		}

		[TestFixture]
		public class when_serializing_strings : BinarySerializerTests
		{
			[Datapoints]
			public readonly string[] Values = new[] {
				"Hello, World!", "", null
			};

			[Theory]
			public void should_roundtrip(string value)
			{
				uint preGuardValue = 0xdeadbeef;
				uint postGuardValue = 0xc0ffeeaf;
				sw.Write(preGuardValue);
				BinarySerializer.ToStream(value, sw);
				sw.Write(postGuardValue);
				ms.Seek(0, SeekOrigin.Begin);

				Assert.That(sr.ReadUInt32(), Is.EqualTo(preGuardValue), "inital guard wrong");

				string fromval;
				BinarySerializer.FromStream(out fromval, sr);

				Assert.That(sr.ReadUInt32(), Is.EqualTo(postGuardValue), "final guard wrong");
				Assert.That(fromval, Is.EqualTo(value));
			}

			[Theory]
			public void should_fail_on_writing_to_null_stream(string value)
			{
				Assert.That(() => BinarySerializer.ToStream(value, null), Throws.InstanceOf<ArgumentNullException>());
			}

			[Test]
			public void should_fail_on_reading_from_null_stream()
			{
				string value;
				Assert.That(() => BinarySerializer.FromStream(out value, null), Throws.InstanceOf<ArgumentNullException>());
			}
		}

		[TestFixture]
		public class when_serializing_SerializableTypes
			: BinarySerializerTests
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
					uint preGuardValue = 0xdeadbeef;
					uint postGuardValue = 0xc0ffeeaf;


					sw.Write(preGuardValue);
					BinarySerializer.ToStream(value, sw);
					sw.Write(postGuardValue);
					sw.Flush();

					ms.Seek(0, SeekOrigin.Begin);

					Assert.That(sr.ReadUInt32(), Is.EqualTo(preGuardValue), "inital guard wrong");

					SerializableType fromval;
					BinarySerializer.FromStream(out fromval, sr);
					Assert.That(sr.ReadUInt32(), Is.EqualTo(postGuardValue), "final guard wrong");

					Assert.That(fromval, Is.EqualTo(value));
					Assert.That(ms.Position, Is.EqualTo(ms.Length), "stream not read completely");
				}
			}

			[Test]
			public void should_not_accept_nulls()
			{
				Assert.That(() => BinarySerializer.ToStream((SerializableType)null, sw), Throws.InstanceOf<ArgumentNullException>());
			}

			[Test]
			public void should_fail_on_writing_to_null_stream()
			{
				foreach (SerializableType value in CreateSerializableTypes())
				{
					Assert.That(() => BinarySerializer.ToStream(value, null), Throws.InstanceOf<ArgumentNullException>());
				}
			}

			[Test]
			public void should_fail_on_reading_from_null_stream()
			{
				SerializableType value;
				Assert.That(() => BinarySerializer.FromStream(out value, null), Throws.InstanceOf<ArgumentNullException>());
			}
		}

		[TestFixture]
		public class when_serializing_SerializableExpressions
			: BinarySerializerTests
		{
			[Test]
			public void should_roundtrip_expressions()
			{
				var value = CreateExpression();
				
				uint preGuardValue = 0xdeadbeef;
				uint postGuardValue = 0xc0ffeeaf;

				sw.Write(preGuardValue);
				BinarySerializer.ToStream(value, sw);
				sw.Write(postGuardValue);
				sw.Flush();

				ms.Seek(0, SeekOrigin.Begin);

				Assert.That(sr.ReadUInt32(), Is.EqualTo(preGuardValue), "inital guard wrong");

				SerializableExpression fromval;
				BinarySerializer.FromStream(out fromval, sr, null);
				Assert.That(sr.ReadUInt32(), Is.EqualTo(postGuardValue), "final guard wrong");

				Assert.That(fromval, Is.InstanceOf(value.GetType()));
				Assert.That(ms.Position, Is.EqualTo(ms.Length), "stream not read completely");
			}
			
			[Test]
			public void should_roundtrip_nulls()
			{
				SerializableExpression value = null;
				
				uint preGuardValue = 0xdeadbeef;
				uint postGuardValue = 0xc0ffeeaf;

				sw.Write(preGuardValue);
				BinarySerializer.ToStream(value, sw);
				sw.Write(postGuardValue);
				sw.Flush();

				ms.Seek(0, SeekOrigin.Begin);

				Assert.That(sr.ReadUInt32(), Is.EqualTo(preGuardValue), "inital guard wrong");

				SerializableExpression fromval;
				BinarySerializer.FromStream(out fromval, sr, null);
				Assert.That(sr.ReadUInt32(), Is.EqualTo(postGuardValue), "final guard wrong");

				Assert.That(fromval, Is.Null);
				Assert.That(ms.Position, Is.EqualTo(ms.Length), "stream not read completely");
			}
			
			[Test]
			public void should_fail_on_writing_to_null_stream()
			{
				var value = CreateExpression();
				Assert.That(() => BinarySerializer.ToStream(value, null), Throws.InstanceOf<ArgumentNullException>());
			}

			[Test]
			public void should_fail_on_reading_from_null_stream()
			{
				SerializableExpression value;
                Assert.That(() => BinarySerializer.FromStream(out value, null, null), Throws.InstanceOf<ArgumentNullException>());
			}

			/// <summary>
			/// Test just serialization, not handling interfaces!
			/// </summary>
			[Test]
			public void should_serialize_complete_expressions()
			{
				var expression = CreateExpression();
				
				BinarySerializer.ToStream(expression, sw);
				ms.Seek(0, SeekOrigin.Begin);

				SerializableExpression fromval;
                BinarySerializer.FromStream(out fromval, sr, null);
				Assert.That(fromval.NodeType, Is.EqualTo(expression.NodeType));
				Assert.That((ExpressionType)expression.NodeType, Is.EqualTo(SerializableExpression.ToExpression(fromval).NodeType));
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
					select new {
					o.IntProperty,
					o.BoolProperty
				};
                return Kistl.API.SerializableExpression.FromExpression(list.Expression, iftFactory);
			}
		}

		[TestFixture]
		public class when_serializing_CollectionEntries
			: BinarySerializerTests
		{
			[Test]
			public void should_fail_on_reading_from_null_stream()
			{
				Assert.That(() => BinarySerializer.FromStreamCollectionEntries<TestCollectionEntry>(null, sr), Throws.InstanceOf<ArgumentNullException>());
			}
			
			[Test]
			public void should_fail_on_writing_to_null_stream()
			{
				var collection = new List<TestCollectionEntry>();
				Assert.That(() => BinarySerializer.ToStreamCollectionEntries<TestCollectionEntry>(collection, null), Throws.InstanceOf<ArgumentNullException>());
			}

			[Test]
			public void cannot_deserialize_collection_entries_to_null_collection()
			{
				Assert.That(() => BinarySerializer.FromStreamCollectionEntries<TestCollectionEntry>(null, sr), Throws.InstanceOf<ArgumentNullException>());
			}

			[Test]
			public void cannot_deserialize_collection_entries_from_null_stream()
			{
				var collection = new List<TestCollectionEntry>();
				Assert.That(() => BinarySerializer.FromStreamCollectionEntries(collection, null), Throws.InstanceOf<ArgumentNullException>());
			}

			[Test]
			public void can_roundtrip_list_with_no_entry()
			{
				TestToFromStreamCollectionEntries(new List<TestCollectionEntry>());
			}

			[Test]
			public void can_roundtrip_null_list()
			{
				TestToFromStreamCollectionEntries(null);
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
                    if(toval != null) toval.ForEach(ce => ctx.Attach(ce));

                    List<TestCollectionEntry> fromval;
                    BinarySerializer.ToStreamCollectionEntries(toval, sw);
                    ms.Seek(0, SeekOrigin.Begin);

                    fromval = new List<TestCollectionEntry>();
                    BinarySerializer.FromStreamCollectionEntries(fromval, sr);
                    if (toval == null)
                    {
                        Assert.That(toval, Is.Null);
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

                    BinarySerializer.ToStreamCollectionEntries(toval, sw);
                    ms.Seek(0, SeekOrigin.Begin);

                    ObservableCollection<TestCollectionEntry> fromvalobserbable = new ObservableCollection<TestCollectionEntry>();
                    BinarySerializer.FromStreamCollectionEntries(fromvalobserbable, sr);
                    Assert.That(toval, Is.EqualTo(fromvalobserbable));
                }
			}
		}

		[TestFixture]
		public class when_serializing_CompoundObjects
			: BinarySerializerTests
		{
			[Test]
			public void ICompoundObject()
			{
				const string testString = "muh";
				TestCompoundObjectImpl toval = new TestCompoundObjectImpl() { TestProperty = testString };

				BinarySerializer.ToStream(toval, sw);
				ms.Seek(0, SeekOrigin.Begin);

				TestCompoundObjectImpl fromval;
				BinarySerializer.FromStream<TestCompoundObjectImpl>(out fromval, sr);
				Assert.That(fromval.TestProperty, Is.EqualTo(fromval.TestProperty));
			}

			[Test]
			public void ICompoundObjectNull()
			{
				TestCompoundObjectImpl toval = null;

				BinarySerializer.ToStream(toval, sw);
				ms.Seek(0, SeekOrigin.Begin);

				TestCompoundObjectImpl fromval;
				BinarySerializer.FromStream<TestCompoundObjectImpl>(out fromval, sr);
				Assert.That(fromval, Is.Null);
			}

			[Test]
			public void should_fail_on_writing_to_null_stream()
			{
				const string testString = "muh";
				TestCompoundObjectImpl value = new TestCompoundObjectImpl() { TestProperty = testString };
				Assert.That(() => BinarySerializer.ToStream(value, null), Throws.InstanceOf<ArgumentNullException>());
			}

			[Test]
			public void should_fail_on_reading_from_null_stream()
			{
				TestCompoundObjectImpl value;
				Assert.That(() => BinarySerializer.FromStream(out value, null), Throws.InstanceOf<ArgumentNullException>());
			}
		}
	}
}
