using System.Reflection;

using NMock2.Actions;
using NMock2.Monitoring;

using NUnit.Framework;

namespace NMock2.Test.Actions
{
    [TestFixture]
	public class CollectActionTest
	{
        private readonly object receiver = new object();
        private readonly MethodInfo methodInfo = typeof(CollectActionTest).GetMethod("DummyMethod");

		[Test] 
        public void CollectsParameterValueAtSpecifiedIndex()
		{
            CollectAction action = new CollectAction(1);

            action.Invoke(new Invocation(receiver, methodInfo, new object[] { 123, "hello" }));

            Assert.AreEqual("hello", action.Parameter);
		}

        [Test] 
        public void ReturnsParameterValueFromMostRecentOfMultipleCalls()
        {
            CollectAction action = new CollectAction(1);

            action.Invoke(new Invocation(receiver, methodInfo, new object[] { 123, "hello" }));
            action.Invoke(new Invocation(receiver, methodInfo, new object[] { 456, "goodbye" }));

            Assert.AreEqual("goodbye", action.Parameter);
        }

        [Test]
        public void HasAReadableDescription()
        {
            IAction action = new CollectAction(37);

            AssertDescription.IsEqual(action, "collect argument at index 37");
        }

        public void DummyMethod(int i, string s) {} // MethodInfoStub breaks with non-empty parameters array
	}
}
