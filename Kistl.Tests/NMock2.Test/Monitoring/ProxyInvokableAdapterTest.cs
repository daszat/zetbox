using System;
using System.Reflection;
using NUnit.Framework;
using NMock2.Monitoring;

namespace NMock2.Test.Monitoring
{
	public interface ProxiedInterface
	{
		string Return();
		void Throw();
		void InOut( string inArg, ref string refArg, out string outArg );
	}
	
	[TestFixture]
	public class ProxyInvokableAdapterTest
	{
		static readonly MethodInfo RETURN_METHOD = typeof(ProxiedInterface).GetMethod("Return");
		static readonly MethodInfo INOUT_METHOD = typeof(ProxiedInterface).GetMethod("InOut");
		
		MockInvokable invokable;
		ProxyInvokableAdapter adapter;
		ProxiedInterface transparentProxy;

		[SetUp]
		public void SetUp()
		{
			invokable = new MockInvokable();
			adapter = new ProxyInvokableAdapter( typeof(ProxiedInterface), invokable );
			transparentProxy = (ProxiedInterface)adapter.GetTransparentProxy();
		}

		[Test]
		public void CapturesInvocationsOnTransparentProxyAndForwardsToInvokableObject()
		{
			invokable.Expected = new Invocation( transparentProxy, RETURN_METHOD, new object[0] );
			
			transparentProxy.Return();

			Assert.IsNotNull(invokable.Actual, "received invocation");
		}
		
		[Test]
		public void ReturnsResultFromInvocationToCallerOfProxy()
		{
			invokable.ResultSetOnInvocation = "result";

			Assert.AreEqual( invokable.ResultSetOnInvocation, transparentProxy.Return() );
		}
		
		class TestException : Exception {}

		[Test, ExpectedException(typeof(TestException))]
		public void ThrowsExceptionFromInvocationToCallerOfProxy()
		{
			invokable.ExceptionSetOnInvocation = new TestException();

			transparentProxy.Throw();
		}
		
		[Test]
		public void ReturnsOutAndRefParametersFromInvocationToCallerOfProxy()
		{
			invokable.Expected = new Invocation(transparentProxy,INOUT_METHOD,new object[]{"inArg","refArg",null});
			invokable.Outputs = new object[]{null, "returnedRefArg", "returnedOutArg"};
			
			string refArg = "refArg";
			string outArg;
			
			transparentProxy.InOut("inArg", ref refArg, out outArg);

			Assert.AreEqual("returnedRefArg", refArg);
			Assert.AreEqual("returnedOutArg", outArg);
		}

		[Test, ExpectedException(typeof(TestException))]
		public void PassesExceptionThrownByInvokableToCallerOfProxy()
		{
			invokable.ThrownException = new TestException();

			transparentProxy.Throw();
		}
	}
}
