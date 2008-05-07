using System.Reflection;

using NMock2.Actions;
using NMock2.Monitoring;

using NUnit.Framework;

namespace NMock2.Test.Actions
{
	[TestFixture]
    public class FireActionTest
	{
        public delegate void BellListener(string who);
        private void Salivate(string who) { dog = who; }
        private string dog;

        public interface IBell
        {
            void Ring();
            event BellListener Listeners;
        }

        [Test] 
        public void FiresEventOnInvocationReceiver()
        {
            Mockery mockery = new Mockery();
            IBell receiver = (IBell) mockery.NewMock(typeof(IBell));
            MethodInfo methodInfo = typeof(IBell).GetMethod("Ring");

            Expect.Once.On(receiver).EventAdd("Listeners", new BellListener(Salivate));

            IAction fireEvent = new FireAction("Listeners", "Rover");
            receiver.Listeners += new BellListener(Salivate);
            fireEvent.Invoke(new Invocation(receiver, methodInfo, new object[] { "unused" }));

            Assert.AreEqual("Rover", dog);
            mockery.VerifyAllExpectationsHaveBeenMet();
        }

        [Test]
        public void HasAReadableDescription()
        {
            IAction action = new FireAction("MyEvent", 123);

            AssertDescription.IsEqual(action, "fire MyEvent");
        }
	}
}
