using NUnit.Framework;

namespace NMock2.AcceptanceTests
{
	[TestFixture]
	public class StubAcceptanceTest
	{
		[Test]
		public void StubsDoNotHaveToBeCalled()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof(IHelloWorld));
			
			Stub.On(helloWorld).Method("Hello").WithAnyArguments();
			
			mocks.VerifyAllExpectationsHaveBeenMet();
		}
		
		[Test]
		public void StubsCanBeCalledAnyNumberOfTimes()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof(IHelloWorld));
			
			Stub.On(helloWorld).Method("Hello").WithAnyArguments();
			
			for (int i = 0; i < ANY_NUMBER; i++) helloWorld.Hello();
			
			mocks.VerifyAllExpectationsHaveBeenMet();
		}
		
		[Test]
		public void StubsMatchArgumentsAndPerformActionsJustLikeAnExpectation()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof(IHelloWorld));

			Stub.On(helloWorld).Method("Ask").With("Name").Will(Return.Value("Bob"));
			Stub.On(helloWorld).Method("Ask").With("Age").Will(Return.Value("30"));
			
			for (int i = 0; i < ANY_NUMBER; i++)
			{
				Verify.That(helloWorld.Ask("Name"), Is.EqualTo("Bob"), "Name");
				Verify.That(helloWorld.Ask("Age"), Is.EqualTo("30"), "Age");
			}
		}
		
		#region Don't look in here...
		#region	I told you not to look in here!
		const int ANY_NUMBER = 10;
		#endregion
		#endregion
	}
}
