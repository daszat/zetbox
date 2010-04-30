using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Kistl.API.Mocks;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Kistl.API.Tests.Serializables
{
    [TestFixture]
    public class SerializerMockTests : AbstractApiTextFixture
    {
        MemoryStream ms;
        BinaryWriter sw;
        BinaryReader sr;

        interface LocalMock : TestObjClass<LocalMock, TestEnum> { }

        public override void SetUp()
        {
            ms = new MemoryStream();
            sw = new BinaryWriter(ms);
            sr = new BinaryReader(ms);
        }
        
        [Test]
        public void AssertCorrectContents_correctly_identifies_ToStream_result()
        {
            TestObjClassSerializationMock.ToStream<LocalMock, TestEnum>(sw, typeTrans);
            ms.Seek(0, SeekOrigin.Begin);
            TestObjClassSerializationMock.AssertCorrectContents<LocalMock, TestEnum>(sr, typeTrans);
            Assert.That(ms.Position, Is.EqualTo(ms.Length), "AssertCorrectContents didn't read complete stream");
        }
	
    }
}
