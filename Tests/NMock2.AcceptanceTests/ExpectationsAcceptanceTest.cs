using NMock2.Internal;
using NUnit.Framework;

namespace NMock2.AcceptanceTests
{
	[TestFixture]
	public class ExpectationsAcceptanceTest
	{
		[Test, ExpectedException(typeof(ExpectationException))]
		public void FailsTestIfMethodExpectedOnceButNotCalled()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof(IHelloWorld));
			
			Expect.Once.On(helloWorld).Method("Hello").WithNoArguments();
			
			mocks.VerifyAllExpectationsHaveBeenMet();
		}
		
		[Test]
		public void PassesTestIfMethodExpectedOnceAndCalled()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof(IHelloWorld));
			
			Expect.Once.On(helloWorld).Method("Hello").WithNoArguments();
			
			helloWorld.Hello();

			mocks.VerifyAllExpectationsHaveBeenMet();
		}
		
		[Test, ExpectedException(typeof(ExpectationException))]
		public void FailsTestIfMethodExpectedOnceButCalledTwice()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof(IHelloWorld));
			
			Expect.Once.On(helloWorld).Method("Hello").WithNoArguments();

			helloWorld.Hello();
			helloWorld.Hello();
		}

        [Test]
        public void TestShouldPassIfExpectedExactlyNTimesAndCalledNTimes()
        {
            Mockery mocks = new Mockery();
            IHelloWorld helloWorld = (IHelloWorld)mocks.NewMock(typeof(IHelloWorld));
            Expect.Exactly(N).On(helloWorld).Method("Hello").WithNoArguments();

            for (int i = 0; i < N; i++) helloWorld.Hello();

            mocks.VerifyAllExpectationsHaveBeenMet();
        }

        [Test, ExpectedException(typeof(ExpectationException))]
        public void TestShouldFailIfExpectedExactlyNTimesAndCalledMoreThanNTimes()
        {
            Mockery mocks = new Mockery();
            IHelloWorld helloWorld = (IHelloWorld)mocks.NewMock(typeof(IHelloWorld));
            Expect.Exactly(N).On(helloWorld).Method("Hello").WithNoArguments();

            for (int i = 0; i < N+1; i++) helloWorld.Hello();

            mocks.VerifyAllExpectationsHaveBeenMet();
        }

        [Test, ExpectedException(typeof(ExpectationException))]
        public void TestShouldFailIfExpectedExactlyNTimesAndCalledLessThanNTimes()
        {
            Mockery mocks = new Mockery();
            IHelloWorld helloWorld = (IHelloWorld)mocks.NewMock(typeof(IHelloWorld));
            Expect.Exactly(N).On(helloWorld).Method("Hello").WithNoArguments();

            for (int i = 0; i < N - 1; i++) helloWorld.Hello();

            mocks.VerifyAllExpectationsHaveBeenMet();
        }

		[Test, ExpectedException(typeof(ExpectationException))]
		public void FailsTestIfMethodExpectedAtLeastNTimesButCalledLessThanNTimes()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof(IHelloWorld));

			Expect.AtLeast(N).On(helloWorld).Method("Hello").WithNoArguments();

			for (int i = 0; i < N-1; i++) helloWorld.Hello();

			mocks.VerifyAllExpectationsHaveBeenMet();
		}
		
		[Test]
		public void PassesTestIfMethodExpectedAtLeastNTimesAndCalledNTimes()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof(IHelloWorld));

			Expect.AtLeast(N).On(helloWorld).Method("Hello").WithNoArguments();

			for (int i = 0; i < N; i++) helloWorld.Hello();

			mocks.VerifyAllExpectationsHaveBeenMet();
		}
		
		[Test]
		public void PassesTestIfMethodExpectedAtLeastNTimesAndCalledMoreThanNTimes()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof(IHelloWorld));

			Expect.AtLeast(N).On(helloWorld).Method("Hello").WithNoArguments();

			for (int i = 0; i < N+1; i++) helloWorld.Hello();

			mocks.VerifyAllExpectationsHaveBeenMet();
		}
		
		[Test]
		public void PassesTestIfMethodExpectedAtMostNTimesAndCalledLessThanNTimes()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof(IHelloWorld));

			Expect.AtMost(N).On(helloWorld).Method("Hello").WithNoArguments();

			for (int i = 0; i < N-1; i++) helloWorld.Hello();
			
			mocks.VerifyAllExpectationsHaveBeenMet();
		}
		
		[Test]
		public void PassesTestIfMethodExpectedAtMostNTimesAndCalledNTimes()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof(IHelloWorld));

			Expect.AtMost(N).On(helloWorld).Method("Hello").WithNoArguments();

			for (int i = 0; i < N; i++) helloWorld.Hello();
			
			mocks.VerifyAllExpectationsHaveBeenMet();
		}
		
		[Test, ExpectedException(typeof(ExpectationException))]
		public void FailsTestIfMethodExpectedAtMostNTimesAndCalledMoreThanNTimes()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof(IHelloWorld));
			
			Expect.AtMost(N).On(helloWorld).Method("Hello").WithNoArguments();
			
			for (int i = 0; i < N+1; i++) helloWorld.Hello();
		}

		[Test, ExpectedException(typeof(ExpectationException))]
		public void FailsTestIfMethodExpectedBetweenNAndMTimesButCalledLessThanNTimes()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof(IHelloWorld));
			
			Expect.Between(N,M).On(helloWorld).Method("Hello").WithNoArguments();

			for (int i = 0; i < N-1; i++) helloWorld.Hello();

			mocks.VerifyAllExpectationsHaveBeenMet();
		}

		[Test, ExpectedException(typeof(ExpectationException))]
		public void FailsTestIfMethodExpectedBetweenNAndMTimesAndCalledMoreThanMTimes()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof(IHelloWorld));
			
			Expect.Between(N,M).On(helloWorld).Method("Hello").WithNoArguments();

			for (int i = 0; i < M; i++) helloWorld.Hello();
			helloWorld.Hello();
		}

		[Test]
		public void PassesTestIfMethodExpectedBetweenNAndMTimesAndCalledBetweenNAndMTimes()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof(IHelloWorld));
			
			Expect.Between(N,M).On(helloWorld).Method("Hello").WithNoArguments();

			for (int i = 0; i < (N+M)/2; i++) helloWorld.Hello();

			mocks.VerifyAllExpectationsHaveBeenMet();
		}

		[Test, ExpectedException(typeof(ExpectationException))]
		public void FailsTestIfMethodNeverExpectedIsCalled()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof(IHelloWorld));
			
			Expect.Never.On(helloWorld).Method("Hello").WithNoArguments();

			helloWorld.Hello();
		}

		[Test]
		public void PassesTestIfMethodNeverExpectedIsNeverCalled()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof(IHelloWorld));
			
			Expect.Never.On(helloWorld).Method("Hello").WithNoArguments();
			
			mocks.VerifyAllExpectationsHaveBeenMet();
		}
		
		[Test, ExpectedException(typeof(ExpectationException))]
		public void FailsTestIfMethodNeverExpectedIsActuallyCalled()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof(IHelloWorld));
			
			Expect.Never.On(helloWorld).Method("Hello").WithNoArguments();
			
			helloWorld.Hello();
		}

		[Test]
		public void ExpectationsCanOverrideStubs()
		{
			Mockery mocks = new Mockery();

			IHelloWorld helloWorld = (IHelloWorld)mocks.NewMock(typeof(IHelloWorld));

			Stub.On(helloWorld).Method("Ask").With("Name?").Will(Return.Value("Bill"));

			Assert.AreEqual("Bill", helloWorld.Ask("Name?"));

			Expect.Once.On(helloWorld).Method("Ask").With("Name?").Will(Return.Value("Steve"));

			Assert.AreEqual("Steve", helloWorld.Ask("Name?"));

			mocks.VerifyAllExpectationsHaveBeenMet();
		}
		
		#region Really boring stuff
		#region It really is boring stuff
		const int N = 2;
		const int M = 4;
		#endregion
		#endregion
	}
}
