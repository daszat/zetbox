using System.Reflection;
using NUnit.Framework;
using NMock2.Actions;
using NMock2.Monitoring;
using NMock2.Test.Monitoring;

namespace NMock2.Test.Actions
{
	[TestFixture]
	public class SetNamedParameterActionTest
	{
		[Test]
		public void SetsNamedParameterOnInvocation()
		{
			object receiver = new object();
			MethodInfoStub methodInfo = new MethodInfoStub("method",
			                                               new ParameterInfoStub("p1",ParameterAttributes.In),
			                                               new ParameterInfoStub("p2",ParameterAttributes.Out));
			string name = "p2";
			object value = new object();
			Invocation invocation = new Invocation(receiver, methodInfo, new object[]{null,null});
			
			SetNamedParameterAction action = new SetNamedParameterAction(name,value);
			
			action.Invoke(invocation);
			
			Assert.AreSame( value, invocation.Parameters[1], "set value" );
		}

		[Test]
		public void HasReadableDescription()
		{
			string name = "param";
			object value = new NamedObject("value");
			
			SetNamedParameterAction action = new SetNamedParameterAction(name,value);
			
			AssertDescription.IsEqual(action, "set param=<value>");
		}
	}
}
