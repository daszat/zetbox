using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NMock2;
using Kistl.App.Base;

namespace Kistl.Client.Objects.Tests
{
    [TestFixture]
    public class BasicTests
    {
        [Test]
        public void TestEqualsSimple()
        {
            DataType t10 = new DataType() { ID = 10 };
            DataType t10_clone = (DataType)t10.Clone();
            DataType t10_manual = new DataType() { ID = 10 };
            DataType t20 = new DataType() { ID = 20 };

            Assert.IsTrue(t10.Equals(t10), "Equals should be reflective");
            Assert.IsTrue(t10.Equals(t10_clone), "Equals should accept cloned objects");
            Assert.IsTrue(t10.Equals(t10_manual), "Equals should accept objects with the same ID");

            Assert.IsFalse(t10.Equals(t20), "Equals should reject objects with different IDs");
        }

        [Test]
        public void TestEqualsHard()
        {
            DataType t10 = new DataType() { ID = 10 };
            DataType t10_clone = (DataType)t10.Clone();
            DataType t10_manual = new DataType() { ID = 10 };
            DataType t20 = new DataType() { ID = 20 };

            t10_clone.ClassName = "Other Name";

            Assert.IsTrue(t10.Equals(t10), "Equals should be reflective");
            Assert.IsTrue(t10.Equals(t10_clone), "Equals should accept cloned objects");
            Assert.IsTrue(t10.Equals(t10_manual), "Equals should accept objects with the same ID");

            Assert.IsFalse(t10.Equals(t20), "Equals should reject objects with different IDs");
        }
    }
}
