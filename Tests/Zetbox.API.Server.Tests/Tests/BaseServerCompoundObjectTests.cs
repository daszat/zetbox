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

namespace Zetbox.API.Server.Tests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API.Server.Mocks;
    using Zetbox.API.Utils;
    using NUnit.Framework;

    [TestFixture]
    public class BaseServerCompoundObjectTests : AbstractApiServerTestFixture
    {
        private TestCompoundObject obj;

        public override void SetUp()
        {
            base.SetUp();
            obj = new TestCompoundObject() { TestInt = 1, TestString = "Hello World" };
        }

        [Test]
        public void Clone()
        {
            TestCompoundObject c = (TestCompoundObject)obj.Clone();
            Assert.That(c.TestInt, Is.EqualTo(obj.TestInt));
            Assert.That(c.TestString, Is.EqualTo(obj.TestString));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToStream_Null()
        {
            obj.ToStream((ZetboxStreamWriter)null, null, false);
        }

        [Test]
        public void Stream()
        {
            var typeMap = scope.Resolve<TypeMap>();
            var ms = new MemoryStream();
            var sw = new ZetboxStreamWriter(typeMap, new BinaryWriter(ms));
            var sr = new ZetboxStreamReader(typeMap, new BinaryReader(ms));

            obj.ToStream(sw, null, false);

            Assert.That(ms.Length, Is.GreaterThan(0));

            ms.Seek(0, SeekOrigin.Begin);

            TestCompoundObject result = new TestCompoundObject();
            result.FromStream(sr);

            Assert.That(result.TestInt, Is.EqualTo(obj.TestInt));
            Assert.That(result.TestString, Is.EqualTo(obj.TestString));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FromStream_Null_StreamReader()
        {
            TestCompoundObject result = new TestCompoundObject();
            result.FromStream((ZetboxStreamReader)null);
        }

        [Test]
        public void NotifyPropertyChanged_ing()
        {
            bool hasChanged = false;
            bool hasChanging = false;

            PropertyChangedEventHandler changedHandler = new PropertyChangedEventHandler(delegate(object sender, PropertyChangedEventArgs e) { hasChanged = true; });
            PropertyChangingEventHandler changingHanlder = new PropertyChangingEventHandler(delegate(object sender, PropertyChangingEventArgs e) { hasChanging = true; });

            obj.PropertyChanged += changedHandler;
            obj.PropertyChanging += changingHanlder;

            obj.NotifyPropertyChanging("TestString", null, null);
            obj.TestString = "test";
            obj.NotifyPropertyChanged("TestString", null, null);

            Assert.That(hasChanged, Is.True);
            Assert.That(hasChanging, Is.True);

            obj.PropertyChanged -= changedHandler;
            obj.PropertyChanging -= changingHanlder;
        }
    }
}
