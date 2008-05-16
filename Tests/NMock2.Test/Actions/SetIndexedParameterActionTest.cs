using System.Reflection;
using NMock2.Actions;
using NMock2.Monitoring;
using NMock2.Test.Monitoring;
using NUnit.Framework;

namespace NMock2.Test.Actions
{
	[TestFixture]
	public class SetIndexedParameterActionTest
	{
		[Test]
		public void SetsIndexedParameterOnInvocation()
		{
			object receiver = new object();
			MethodInfoStub methodInfo = new MethodInfoStub("method",
				new ParameterInfoStub("p1",ParameterAttributes.In),
				new ParameterInfoStub("p2",ParameterAttributes.Out));
			int index = 1;
			object value = new object();
			Invocation invocation = new Invocation(receiver, methodInfo, new object[]{null,null});
			
			SetIndexedParameterAction action = new SetIndexedParameterAction(index,value);
			
			action.Invoke(invocation);
			
			Assert.AreSame( value, invocation.Parameters[index], "set value" );
		}

		[Test]
		public void HasReadableDescription()
		{
			int index = 1;
			object value = new NamedObject("value");
			
			SetIndexedParameterAction action = new SetIndexedParameterAction(index,value);
			
			AssertDescription.IsEqual(action, "set arg 1=<value>");
		}
	}
}
