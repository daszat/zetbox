using System.Reflection;
using NUnit.Framework;
using NMock2.Monitoring;
using NMock2.Test.Monitoring;

namespace NMock2.Test.Monitoring
{
	[TestFixture]
	public class InvokerTest
	{
		public interface IFoo
		{
			void Foo();
		}

		public interface IOther
		{
			void Other();
		}

		public class MockFoo : IFoo
		{
			public bool FooWasInvoked = false;

			public void Foo()
			{
				FooWasInvoked = true;
			}
		}

		public static readonly MethodInfo FOO_METHOD = typeof(IFoo).GetMethod("Foo");
		public static readonly MethodInfo OTHER_METHOD = typeof(IOther).GetMethod("Other");
		
		
		private object receiver;
		private MockFoo target;
		private MockInvokable next;
		private Invoker invoker;
		
		[SetUp]
		public void SetUp()
		{
			receiver = new object();
			target = new MockFoo();
			next = new MockInvokable();
			invoker = new Invoker(typeof(IFoo), this.target, this.next);
		}

		[Test]
		public void InvokesMethodOnObjectIfMethodIsDeclaredInSpecifiedType()
		{
			Invocation invocation = new Invocation(receiver, FOO_METHOD, new object[0]);
			
			invoker.Invoke(invocation);

			Assert.IsTrue(target.FooWasInvoked, "Foo should have been invoked on target");
		}

		[Test]
		public void ForwardsInvocationsToNextIfMethodIsNotDeclaredInSpecifiedType()
		{
			Invocation invocation = new Invocation(receiver, OTHER_METHOD, new object[0]);
			
			next.Expected = invocation;
			
			invoker.Invoke(invocation);

			Assert.IsFalse(target.FooWasInvoked, "should not have invoked method on target");
			Assert.AreSame(invocation, next.Actual, "should have passed invocation to next in chain");
		}
	}
}
