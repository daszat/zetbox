using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Kistl.API.Mocks;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Kistl.API.Tests
{
	[TestFixture]
	public class ExtensionHelpersTest
	{
		TestDataObject obj;

		[SetUp]
		public void SetUp()
		{
			obj = new TestDataObject__Implementation__() { BoolProperty = true, IntProperty = 1, StringProperty = "test" };
		}

		[Test]
		public void XmlString()
		{
			string xml = obj.ToXmlString();
			TestDataObject result = xml.FromXmlString<TestDataObject__Implementation__>();

			Assert.That(result, Is.Not.Null);
			Assert.That(result.ID, Is.EqualTo(obj.ID));
			Assert.That(result.BoolProperty, Is.EqualTo(obj.BoolProperty));
			Assert.That(result.IntProperty, Is.EqualTo(obj.IntProperty));
			Assert.That(result.StringProperty, Is.EqualTo(obj.StringProperty));
		}

		[Test]
		public void In()
		{
			DayOfWeek day = DayOfWeek.Thursday;
			bool result = day.In(
				DayOfWeek.Monday,
				DayOfWeek.Thursday,
				DayOfWeek.Wednesday,
				DayOfWeek.Thursday,
				DayOfWeek.Friday);

			Assert.That(result, Is.True);
		}

		[Test]
		public void InFalse()
		{
			DayOfWeek day = DayOfWeek.Sunday;
			bool result = day.In(
				DayOfWeek.Monday,
				DayOfWeek.Thursday,
				DayOfWeek.Wednesday,
				DayOfWeek.Thursday,
				DayOfWeek.Friday);

			Assert.That(result, Is.False);
		}

		[Test]
		public void GetPropertyValue()
		{
			Assert.That(obj.GetPropertyValue<int>("IntProperty"), Is.EqualTo(obj.IntProperty));
			Assert.That(obj.GetPropertyValue<bool>("BoolProperty"), Is.EqualTo(obj.BoolProperty));
			Assert.That(obj.GetPropertyValue<string>("StringProperty"), Is.EqualTo(obj.StringProperty));
		}

		[Test]
		public void SetPropertyValue()
		{
			obj.SetPropertyValue<int>("IntProperty", 2);
			obj.SetPropertyValue<bool>("BoolProperty", false);
			obj.SetPropertyValue<string>("StringProperty", "Hello");

			Assert.That(obj.IntProperty, Is.EqualTo(2));
			Assert.That(obj.BoolProperty, Is.EqualTo(false));
			Assert.That(obj.StringProperty, Is.EqualTo("Hello"));
		}

		[Test]
		public void GetPropertyValue_InvalidPropertyName()
		{
			Assert.That(() => obj.GetPropertyValue<int>("TestProperty"), Throws.InstanceOf<ArgumentOutOfRangeException>());
		}

		[Test]
		public void SetPropertyValue_InvalidPropertyName()
		{
			Assert.That(() => obj.SetPropertyValue<int>("TestProperty", 1), Throws.InstanceOf<ArgumentOutOfRangeException>());
		}

		[Test]
		public void GetPrivateFieldValue()
		{
			Assert.That(obj.GetPrivateFieldValue<int>("_IntProperty"), Is.EqualTo(obj.IntProperty));
			Assert.That(obj.GetPrivateFieldValue<bool>("_BoolProperty"), Is.EqualTo(obj.BoolProperty));
			Assert.That(obj.GetPrivateFieldValue<string>("_StringProperty"), Is.EqualTo(obj.StringProperty));
		}

		[Test]
		public void GetPrivateFieldValue_InvalidPropertyName()
		{
			Assert.That(() => obj.GetPrivateFieldValue<int>("_TestProperty"), Throws.InstanceOf<ArgumentOutOfRangeException>());
		}

		[Test]
		public void GetPrivatePropertyValue()
		{
			Assert.That(obj.GetPrivatePropertyValue<int>("PrivateIntProperty"), Is.EqualTo(0));
		}

		[Test]
		public void SetPrivatePropertyValue()
		{
			obj.SetPrivatePropertyValue<int>("PrivateIntProperty", 2);
		}

		[Test]
		public void GetPrivatePropertyValue_InvalidPropertyName()
		{
			Assert.That(() => obj.GetPrivatePropertyValue<int>("_TestProperty"), Throws.InstanceOf<ArgumentOutOfRangeException>());
		}

		[Test]
		public void SetPrivatePropertyValue_InvalidPropertyName()
		{
			Assert.That(() => obj.SetPrivatePropertyValue<int>("_TestProperty", 1), Throws.InstanceOf<ArgumentOutOfRangeException>());
		}

		[Test]
		public void ForEach_IEnumerable()
		{
			IEnumerable list = new List<int>(new int[] { 1, 2, 3, 4 });
			int result = 0;
			list.ForEach<int>(i => result += i);
			Assert.That(result, Is.EqualTo(10));
		}

		[Test]
		public void ForEach_List()
		{
			List<int> list = new List<int>(new int[] { 1, 2, 3, 4 });
			int result = 0;
			list.ForEach<int>(i => result += i);
			Assert.That(result, Is.EqualTo(10));
		}

		[Test]
		public void ForEach_ObservableCollection()
		{
			ObservableCollection<int> list = new ObservableCollection<int>(new int[] { 1, 2, 3, 4 }.ToList());
			int result = 0;
			list.ForEach<int>(i => result += i);
			Assert.That(result, Is.EqualTo(10));
		}
	}
}
