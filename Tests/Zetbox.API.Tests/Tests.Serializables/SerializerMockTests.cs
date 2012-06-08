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

namespace Zetbox.API.Tests.Serializables
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
    using Zetbox.API.AbstractConsumerTests;
    using Zetbox.API.Mocks;
    using NUnit.Framework;
    using NUnit.Framework.Constraints;

    [TestFixture]
    public class SerializerMockTests : SerializerTestFixture
    {
        interface LocalMock : TestObjClass<LocalMock, TestEnum> { }

        [Test]
        public void AssertCorrectContents_correctly_identifies_ToStream_result()
        {
            TestObjClassSerializationMock.ToStream<LocalMock, TestEnum>(sw, iftFactory);
            ms.Seek(0, SeekOrigin.Begin);
            TestObjClassSerializationMock.AssertCorrectContents<LocalMock, TestEnum>(sr, iftFactory);
            Assert.That(ms.Position, Is.EqualTo(ms.Length), "AssertCorrectContents didn't read complete stream");
        }
    }
}
