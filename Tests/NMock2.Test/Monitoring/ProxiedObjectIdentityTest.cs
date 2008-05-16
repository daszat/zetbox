using System;
using System.Reflection;
using NUnit.Framework;
using NMock2.Monitoring;
using NMock2.Test.Monitoring;

namespace NMock2.Test.Monitoring
{
	[TestFixture]
	public class ProxiedObjectIdentityTest
	{
		private static readonly MethodInfo EQUALS_METHOD =
			typeof(object).GetMethod("Equals",new Type[]{typeof(object)});
		private static readonly MethodInfo TOSRING_METHOD =
			typeof(object).GetMethod("ToString",new Type[0]);
		private static readonly MethodInfo GETHASHCODE_METHOD =
			typeof(object).GetMethod("GetHashCode",new Type[0]);
		
		private object receiver;
		private object identityProvider;
		private MockInvokable next;
		private IInvokable id;
		
		[SetUp]
		public void SetUp()
		{
			receiver = new NamedObject("receiver");
			identityProvider = new NamedObject("identityProvider");
			next = new MockInvokable();
			id = new ProxiedObjectIdentity(identityProvider, next);
		}
		
		[Test]
		public void ImplementsEqualsByComparingInvocationReceiversForIdentity()
		{
			next.ExpectNotCalled();

			Invocation equalInvocation = new Invocation(receiver, EQUALS_METHOD, new object[]{receiver});
			id.Invoke(equalInvocation);
			Assert.IsTrue((bool)equalInvocation.Result, "should have returned true");

			object other = new NamedObject("other");
			Invocation notEqualInvocation = new Invocation(receiver, EQUALS_METHOD, new object[]{other});
			id.Invoke(notEqualInvocation);
			Assert.IsFalse((bool)notEqualInvocation.Result, "should not have returned true");
		}

		[Test]
		public void ForwardsToStringToIdentityProvider()
		{
			Invocation toStringInvocation = new Invocation(receiver, TOSRING_METHOD, new object[0]);
			id.Invoke(toStringInvocation);

			Assert.AreEqual(identityProvider.ToString(), toStringInvocation.Result, "ToString()");
		}

	
		[Test]
		public void ForwardsGetHashCodeToIdentityProvider()
		{
			Invocation getHashCodeInvocation = new Invocation(receiver, GETHASHCODE_METHOD, new object[0]);
			id.Invoke(getHashCodeInvocation);
			
			Assert.AreEqual(identityProvider.GetHashCode(), getHashCodeInvocation.Result, "GetHashCode()");
		}

		[Test]
		public void ForwardsInvocationsOfOtherMethodsToNextInChain()
		{
			Invocation invocation = new Invocation(receiver, typeof(ICloneable).GetMethod("Clone"), new object[0]);

			id.Invoke(invocation);

			Assert.AreSame(invocation, next.Actual, "should have forwarded invocation to next in chain");
		}
	}
}
