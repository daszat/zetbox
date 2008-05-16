using System;
using NUnit.Framework;
using NMock2.Monitoring;

namespace NMock2.Test.Monitoring
{
	public interface A {}
	public interface B {}
	public interface C {}
    public interface A_B {}

    [TestFixture]
	public class MultiInterfaceFactoryTest
	{
		MultiInterfaceFactory factory;

		[SetUp]
		public void SetUp()
		{
			factory = new MultiInterfaceFactory("TestedMultiInterfaceFactory");
		}
		
		[Test]
		public void CreatesTypeInfoObjectsThatRepresentAnInterfaceThatExtendsAllParameterInterfaces()
		{
			Type newType = factory.GetType(typeof(A),typeof(B),typeof(C));

			Assert.IsTrue( typeof(A).IsAssignableFrom(newType), "new type is an A");
			Assert.IsTrue( typeof(B).IsAssignableFrom(newType), "new type is an B");
			Assert.IsTrue( typeof(C).IsAssignableFrom(newType), "new type is an C");
		}

		[Test]
		public void ReturnsTheSameTypeWhenPassedTheSameArguments()
		{
			Type type = factory.GetType(typeof(A),typeof(B));
			Type sameType = factory.GetType(typeof(A),typeof(B));
			Type otherType = factory.GetType(typeof(A),typeof(C));

			Assert.IsTrue( type == sameType, "should be same type");
			Assert.IsTrue( type != otherType, "should be different type");
		}

		[Test]
		public void ReturnsTheSameTypeWhenPassedTheSameArgumentsInDifferentOrder()
		{
			Type type = factory.GetType(typeof(A),typeof(B));
			Type sameType = factory.GetType(typeof(B),typeof(A));
			
			Assert.IsTrue( type == sameType, "should be same type");
		}

        [Test]
        public void AvoidsNameClash()
        {
            Type type = factory.GetType(typeof(A), typeof(B), typeof(C));
            Type otherType = factory.GetType(typeof(A_B), typeof(C));

            Assert.IsTrue(type != otherType, "types should be different");
        }
    }
}
