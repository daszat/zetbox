
namespace Kistl.API.Tests.Serializables
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
    using Kistl.API.AbstractConsumerTests;
    using Kistl.API.Mocks;
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
