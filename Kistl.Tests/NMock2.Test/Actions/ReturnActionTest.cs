using NUnit.Framework;
using NMock2.Actions;
using NMock2.Monitoring;
using NMock2.Test.Monitoring;

namespace NMock2.Test.Actions
{
	[TestFixture]
	public class ReturnActionTest
	{
		[Test]
		public void SetsReturnValueOfInvocation()
		{
			object result = new NamedObject("result");
			ReturnAction action = new ReturnAction(result);

			Assert.AreSame( result, ResultOfAction(action), "result" );
		}

		[Test]
		public void HasAReadableDescription()
		{
			object result = new NamedObject("result");
			ReturnAction action = new ReturnAction(result);

			AssertDescription.IsEqual(action, "return <result>");
		}

		public static object ResultOfAction(IAction action)
		{
			object receiver = new NamedObject("receiver");
			MethodInfoStub methodInfo = new MethodInfoStub("method");
			Invocation invocation = new Invocation(receiver, methodInfo, new object[0]);
	
			action.Invoke(invocation);

			return invocation.Result;
		}
	}
}
