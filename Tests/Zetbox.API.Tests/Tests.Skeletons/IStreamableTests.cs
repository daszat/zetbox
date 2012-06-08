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

namespace Zetbox.API.Tests.Skeletons
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API.AbstractConsumerTests;
    using Zetbox.API.Utils;
    using NUnit.Framework;

    public class IStreamableTests<T> : SerializerTestFixture
        where T : IStreamable, new()
    {
        protected T obj;

        public override void SetUp()
        {
            base.SetUp();
            obj = CreateObjectInstance();
        }

        protected T CreateObjectInstance()
        {
            return new T();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void fails_on_serializing_to_null_stream()
        {
            obj.ToStream((ZetboxStreamWriter)null, null, false);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void fails_on_serializing_from_null_stream()
        {
            obj.FromStream((ZetboxStreamReader)null);
        }

        [Test]
        public void should_roundtrip_without_errors()
        {
            SerializationRoundtrip(obj);
        }

        protected T SerializationRoundtrip(T obj)
        {
            obj.ToStream(sw, null, false);

            Assert.That(ms.Length, Is.GreaterThan(0));

            ms.Seek(0, SeekOrigin.Begin);

            if (!typeof(T).IsICompoundObject())
            {
                var t = sr.ReadSerializableType();
                Assert.That(t.GetSystemType().IsAssignableFrom(typeof(T)), string.Format("{0} not assignable to {1}", typeof(T), t));
            }

            T result = new T();
            result.FromStream(sr);
            return result;
        }
    }
}
