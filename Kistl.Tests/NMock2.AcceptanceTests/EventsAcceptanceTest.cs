using NUnit.Framework;

namespace NMock2.AcceptanceTests
{
	[TestFixture]
	public class EventsAcceptanceTest
	{
		private string listenerMessage = null;

		public delegate void Listener(string message);
		
		public interface Announcer
		{
			event Listener Listeners;
		}

		private void DummyListener(string message)
		{
			listenerMessage = message;
		}

		[Test]
		public void CanExpectEventAdd()
		{
			Mockery mocks = new Mockery();
			Announcer announcer = (Announcer) mocks.NewMock(typeof(Announcer));

			Expect.Once.On(announcer).EventAdd("Listeners", new Listener(DummyListener));
			
			announcer.Listeners += new Listener(DummyListener);

			mocks.VerifyAllExpectationsHaveBeenMet();
		}
		
		[Test]
		public void CanExpectEventRemove()
		{
			Mockery mocks = new Mockery();
			Announcer announcer = (Announcer) mocks.NewMock(typeof(Announcer));
			
			Expect.Once.On(announcer).EventRemove("Listeners", new Listener(DummyListener));
			
			announcer.Listeners -= new Listener(DummyListener);
			
			mocks.VerifyAllExpectationsHaveBeenMet();
		}
		
		[Test]
		public void DelegatesCanBeComparedToEquality()
		{
			Listener l1 = new Listener(DummyListener);
			Listener l2 = new Listener(DummyListener);

			Assert.AreEqual(l1, l2);
		}
		
		[Test]
		public void EventsCanBeRaisedDuringTests()
		{
			listenerMessage = null;
			Mockery mocks = new Mockery();
			Announcer announcer = (Announcer) mocks.NewMock(typeof(Announcer));
			
			Expect.Once.On(announcer).EventAdd("Listeners", new Listener(DummyListener));
			
			announcer.Listeners += new Listener(DummyListener);
			
			Fire.Event("Listeners").On(announcer).With("Test Message");
			
			Assert.AreEqual("Test Message", listenerMessage);
            mocks.VerifyAllExpectationsHaveBeenMet();
		}

        [Test]
        public void EventHandlingByMocksRespectsAdditionAndRemovalOfListeners()
        {
            listenerMessage = "original";
            Mockery mocks = new Mockery();
            Announcer announcer = (Announcer) mocks.NewMock(typeof(Announcer));
			
            Fire.Event("Listeners").On(announcer).With("this should do nothing");

            Assert.AreEqual("original", listenerMessage);

            Expect.Once.On(announcer).EventAdd("Listeners", new Listener(DummyListener));
            announcer.Listeners += new Listener(DummyListener);

			Fire.Event("Listeners").On(announcer).With("changed");

            Assert.AreEqual("changed", listenerMessage);

            Expect.Once.On(announcer).EventRemove("Listeners", new Listener(DummyListener));
            announcer.Listeners -= new Listener(DummyListener);

            Fire.Event("Listeners").On(announcer).With("something completely different");

            Assert.AreEqual("changed", listenerMessage);

            mocks.VerifyAllExpectationsHaveBeenMet();
        }

		private class OneShotListener
		{
			Announcer m_announcer;

			public OneShotListener(Announcer announcer)
			{
				m_announcer = announcer;
			}

			public void RemovalListener(string message)
			{
				m_announcer.Listeners -= new Listener(this.RemovalListener);
			}
		}

		[Test]
		public void EventHandlersCanBeRemovedDuringEventHandling()
		{
			Mockery mocks = new Mockery();
			Announcer announcer = (Announcer) mocks.NewMock(typeof(Announcer));

			OneShotListener listener = new OneShotListener(announcer);

			Expect.Once.On(announcer).EventAdd("Listeners", new Listener(listener.RemovalListener));
			announcer.Listeners += new Listener(listener.RemovalListener);
			mocks.VerifyAllExpectationsHaveBeenMet();

			Expect.Once.On(announcer).EventRemove("Listeners", new Listener(listener.RemovalListener));
			
			Fire.Event("Listeners").On(announcer).With("changed");
			
			mocks.VerifyAllExpectationsHaveBeenMet();
		}
	}
}
