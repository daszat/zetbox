using System;
using NUnit.Framework;

namespace NMock2.AcceptanceTests
{
	[TestFixture]
	public class ErrorCheckingAcceptanceTest
	{
		[Test, ExpectedException(typeof(ArgumentException))]
		public void CannotExpectAMethodOnAnRealObject()
		{
			object realObject = new object();
			Expect.Once.On(realObject).Method(Is.Anything);
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void CannotExpectAMethodThatDoesNotExistInTheMockedType()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof (IHelloWorld));

			Expect.Once.On(helloWorld).Method("NonexistentMethod");
		}
		
		[Test, ExpectedException(typeof(ArgumentException))]
		public void CannotExpectAMethodWithAnInvalidName()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof (IHelloWorld));

			Expect.Once.On(helloWorld).Method("Invalid Name!");
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void CannotExpectGetOfAnInvalidProperty()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof (IHelloWorld));

			Expect.Once.On(helloWorld).GetProperty("NonexistentProperty");
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void CannotExpectSetOfAnInvalidProperty()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof (IHelloWorld));

			Expect.Once.On(helloWorld).SetProperty("NonexistentProperty").To("something");
		}
		
		[Test, ExpectedException(typeof(ArgumentException))]
		public void CannotExpectGetOfIndexerIfNoIndexerInMockedType()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof (IHelloWorld));
			
			Expect.Once.On(helloWorld).Get["arg"].Will(Return.Value("something"));
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void CannotExpectSetOfIndexerIfNoIndexerInMockedType()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof (IHelloWorld));

			Expect.Once.On(helloWorld).Set["arg"].To("something");
		}

		
		[Test, ExpectedException(typeof(ArgumentException))]
		public void CannotExpectAddToNonexistentEvent()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof (IHelloWorld));
			
			Expect.Once.On(helloWorld).EventAdd("NonexistentEvent", Is.Anything);
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void CannotExpectRemoveFromNonexistentEvent()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof (IHelloWorld));

			Expect.Once.On(helloWorld).EventRemove("NonexistentEvent", Is.Anything);
		}
	}
}
