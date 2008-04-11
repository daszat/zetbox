using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.API;
using System.Reflection;
using System.Linq.Expressions;

namespace API.Tests.Tests
{
    [TestFixture]
    public class ExtensionHelpersTest
    {
        XMLObject obj;

        [SetUp]
        public void SetUp()
        {
            obj = new XMLObject() { BoolProperty = true, IntProperty = 1, StringProperty = "test" };
        }

        [Test]
        public void XmlString()
        {
            string xml = obj.ToXmlString();
            XMLObject result = xml.FromXmlString<XMLObject>();

            Assert.That(result, Is.EqualTo(obj));
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
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetPropertyValue_InvalidPropertyName()
        {
            obj.GetPropertyValue<int>("TestProperty");
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SetPropertyValue_InvalidPropertyName()
        {
            obj.SetPropertyValue<int>("TestProperty", 1);
        }

        [Test]
        public void ForEach_IEnumerable()
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

        [Test]
        public void Clone_List()
        {
            List<XMLObject> list = new List<XMLObject>();
            list.Add(new XMLObject() { IntProperty = 1 });
            list.Add(new XMLObject() { IntProperty = 2 });
            List<XMLObject> result = list.Clone();
            Assert.That(result, Is.EqualTo(list));
        }

        [Test]
        public void Clone_ObservableCollection()
        {
            ObservableCollection<XMLObject> list = new ObservableCollection<XMLObject>();
            list.Add(new XMLObject() { IntProperty = 1 });
            list.Add(new XMLObject() { IntProperty = 2 });
            ObservableCollection<XMLObject> result = list.Clone();
            Assert.That(result, Is.EqualTo(list));
        }

        [Test]
        public void Clone_NotifyingObservableCollection()
        {
            Kistl.App.Test.TestObjClass dataObj = new Kistl.App.Test.TestObjClass();
            NotifyingObservableCollection<XMLObject> list = new NotifyingObservableCollection<XMLObject>(dataObj, "");
            list.Add(new XMLObject() { IntProperty = 1 });
            list.Add(new XMLObject() { IntProperty = 2 });
            NotifyingObservableCollection<XMLObject> result = list.Clone(dataObj);
            Assert.That(result, Is.EqualTo(list));
        }
    }
}
