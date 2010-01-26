using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Tests.Skeletons;
using Kistl.App.Test;
using Kistl.DalProvider.EF.Mocks;

using NUnit.Framework;
using Kistl.DalProvider.EF;

namespace Kistl.DalProvider.EF.Tests
{
    [TestFixture]
    public class BaseServerCompoundObjectTests
        : IStreamableTests<TestPhoneCompoundObject__Implementation__>
    {
        TestCustomObject__Implementation__ parent;
        TestPhoneCompoundObject__Implementation__ attachedObj;

        [SetUp]
        public void SetUpTestObject()
        {
            var testCtx = new ServerApiContextMock();

            obj = new TestPhoneCompoundObject__Implementation__() { AreaCode = "ABC", Number = "123456" };

            parent = new TestCustomObject__Implementation__();
            attachedObj = (TestPhoneCompoundObject__Implementation__)parent.PhoneNumberOffice;
            attachedObj.AreaCode = "attachedAreaCode";
            attachedObj.Number = "attachedNumber";
        }

        

        [Test]
        public void should_roudtrip_members_correctly()
        {
            var result = this.SerializationRoundtrip(obj);

            Assert.That(result.AreaCode, Is.EqualTo(obj.AreaCode));
            Assert.That(result.Number, Is.EqualTo(obj.Number));
        }

        
    }
}
