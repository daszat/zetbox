using System;
using NUnit.Framework;
using NMock2.Actions;
using NMock2.Monitoring;
using NMock2.Test.Monitoring;

namespace NMock2.Test.Actions
{
	[TestFixture]
	public class ThrowActionTest
	{
		[Test]
		public void SetsExceptionOfInvocation()
		{
			Exception exception = new Exception();
			ThrowAction action = new ThrowAction(exception);
			
			object receiver = new object();
			MethodInfoStub methodInfo = new MethodInfoStub("method");
			Invocation invocation = new Invocation(receiver, methodInfo, new object[0]);

			action.Invoke(invocation);

			Assert.AreSame( exception, invocation.Exception, "exception" );
		}

		[Test]
		public void HasReadableDescription()
		{
			Exception exception = new Exception();
			ThrowAction action = new ThrowAction(exception);
			
			AssertDescription.IsEqual(action, "throw <"+exception.ToString()+">");
		}
	}
}
