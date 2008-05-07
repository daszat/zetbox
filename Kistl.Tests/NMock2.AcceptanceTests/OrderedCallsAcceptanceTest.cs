using NMock2.Internal;
using NUnit.Framework;

namespace NMock2.AcceptanceTests
{
	[TestFixture]
	public class OrderedCallsAcceptanceTest
	{
		private Mockery mocks;
		private IHelloWorld helloWorld;
		
		[SetUp]
		public void SetUp()
		{
			mocks = new Mockery();
			helloWorld = (IHelloWorld) mocks.NewMock(typeof(IHelloWorld));
		}
		
		[Test]
		public void DoesNotEnforceTheOrderOfCallsByDefault()
		{
			Expect.Once.On(helloWorld).Method("Hello");
			Expect.Once.On(helloWorld).Method("Goodbye");

			helloWorld.Goodbye();
			helloWorld.Hello();

			mocks.VerifyAllExpectationsHaveBeenMet();
		}
		
		[Test]
		public void UnorderedExpectationsMatchInOrderOfSpecification()
		{
			Expect.Once.On(helloWorld).Method("Ask").With(Is.Anything).Will(Return.Value("1"));
			Expect.Once.On(helloWorld).Method("Ask").With(Is.Anything).Will(Return.Value("2"));

			Assert.AreEqual("1", helloWorld.Ask("ignored"), "first call");
			Assert.AreEqual("2", helloWorld.Ask("ignored"), "second call");

			mocks.VerifyAllExpectationsHaveBeenMet();
		}

		[Test, ExpectedException(typeof(ExpectationException))]
		public void EnforcesTheOrderOfCallsWithinAnInOrderBlock()
		{
			using (mocks.Ordered)
			{
				Expect.Once.On(helloWorld).Method("Hello");
				Expect.Once.On(helloWorld).Method("Goodbye");
			}
			
			helloWorld.Goodbye();
			helloWorld.Hello();
		}
		
		[Test]
		public void AllowsCallsIfCalledInSameOrderAsExpectedWithinAnInOrderBlock()
		{
			using (mocks.Ordered)
			{
				Expect.Once.On(helloWorld).Method("Hello");
				Expect.Once.On(helloWorld).Method("Goodbye");
			}
			
			helloWorld.Hello();
			helloWorld.Goodbye();

			mocks.VerifyAllExpectationsHaveBeenMet();
		}
		
		[Test]
		public void CanExpectUnorderedCallsWithinAnOrderedSequence()
		{
			using (mocks.Ordered)
			{
				Expect.Once.On(helloWorld).Method("Hello");
				using (mocks.Unordered)
				{
					Expect.Once.On(helloWorld).Method("Umm");
					Expect.Once.On(helloWorld).Method("Err");
				}
				Expect.Once.On(helloWorld).Method("Goodbye");
			}

			helloWorld.Hello();
			helloWorld.Err();
			helloWorld.Umm();
			helloWorld.Goodbye();

			mocks.VerifyAllExpectationsHaveBeenMet();
		}

		[Test, ExpectedException(typeof(ExpectationException))]
		public void UnorderedCallsWithinAnInOrderedBlockCannotBeCalledBeforeTheStartOfTheUnorderedExpectations()
		{
			using (mocks.Ordered)
			{
				Expect.Once.On(helloWorld).Method("Hello");
				using (mocks.Unordered)
				{
					Expect.Once.On(helloWorld).Method("Umm");
					Expect.Once.On(helloWorld).Method("Err");
				}
				Expect.Once.On(helloWorld).Method("Goodbye");
			}

			helloWorld.Err();
			helloWorld.Hello();
			helloWorld.Umm();
			helloWorld.Goodbye();
		}

		[Test, ExpectedException(typeof(ExpectationException))]
		public void UnorderedCallsWithinAnInOrderedBlockCannotBeCalledAfterTheEndOfTheUnorderedExpectations()
		{
			using (mocks.Ordered)
			{
				Expect.Once.On(helloWorld).Method("Hello");
				using (mocks.Unordered)
				{
					Expect.Once.On(helloWorld).Method("Umm");
					Expect.Once.On(helloWorld).Method("Err");
				}
				Expect.Once.On(helloWorld).Method("Goodbye");
			}

			helloWorld.Hello();
			helloWorld.Err();
			helloWorld.Goodbye();
			helloWorld.Umm();
		}
		
		[Test]
		public void CallsWithinAnInOrderedBlockCanBeExpectedMoreThanOnce()
		{
			using (mocks.Ordered)
			{
				Expect.Once.On(helloWorld).Method("Hello");
				Expect.AtLeastOnce.On(helloWorld).Method("Err");
				Expect.Once.On(helloWorld).Method("Goodbye");
			}
			
			helloWorld.Hello();
			helloWorld.Err();
			helloWorld.Err();
			helloWorld.Goodbye();
		}
		
		[Test]
		[ExpectedException(typeof(ExpectationException))]
		public void CallsInOrderedBlocksThatAreNotMatchedFailVerification()
		{
			using (mocks.Ordered)
			{
				Expect.Once.On(helloWorld).Method("Hello");
				Expect.Once.On(helloWorld).Method("Goodbye");
			}
			
			helloWorld.Hello();

			mocks.VerifyAllExpectationsHaveBeenMet();
		}
	}
}
