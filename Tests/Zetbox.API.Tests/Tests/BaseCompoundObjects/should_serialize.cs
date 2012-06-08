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

namespace Zetbox.API.Tests.BaseCompoundObjects
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API.AbstractConsumerTests;
    using Zetbox.API.Mocks;
    using NUnit.Framework;
    
    [TestFixture]
    public class should_serialize : SerializerTestFixture
    {
        TestCompoundObjectImpl test;

        public override void SetUp()
        {
            base.SetUp();
            test = new TestCompoundObjectImpl();
        }

        /// <summary>
        /// Rewinds all streams to their start
        /// </summary>
        private void RewindStreams()
        {
            ms.Seek(0, SeekOrigin.Begin);
        }

        [Test]
        public void without_exceptions()
        {
            test.ToStream(sw, null, false);
            RewindStreams();

            Assert.DoesNotThrow(() =>
            {
                test.FromStream(sr);
            });
        }

        // just test the mock
        [Test]
        public void and_keep_TestProperty()
        {
            const string val = "muh";
            test.TestProperty = val;
            test.ToStream(sw, null, false);
            test.TestProperty = null;
            
            RewindStreams();
            test.FromStream(sr);

            Assert.That(test.TestProperty, Is.EqualTo(val), "To/FromStream of the mock didn't transport TestProperty");
        }
    }
}
