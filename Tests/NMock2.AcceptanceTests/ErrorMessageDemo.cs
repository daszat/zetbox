using System;
using NUnit.Framework;

namespace NMock2.AcceptanceTests
{
	[TestFixture]
	public class ErrorMessageDemo
	{
		public delegate void Action();

		public interface ISyntacticSugar
		{
			string Property { get; set; }
			string this[string s] { get; set; }
			int this[int i,string s] { get; set; }
			
			event Action Actions;
		}

		[Test, ExpectedException(typeof(NMock2.Internal.ExpectationException))]
		public void VerifyFailure()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof(IHelloWorld));
			
			Expect.Once.On(helloWorld).Method("Hello").WithNoArguments();
			Expect.Between(2,4).On(helloWorld).Method("Ask").With("What color is the fish?")
				.Will(Return.Value("purple"));
			Expect.AtLeast(1).On(helloWorld).Method("Ask").With("How big is the fish?")
				.Will(Throw.Exception(new InvalidOperationException("stop asking about the fish!")));

			helloWorld.Hello();
			helloWorld.Ask("What color is the fish?");

			mocks.VerifyAllExpectationsHaveBeenMet();
		}
		
		[Test, ExpectedException(typeof(NMock2.Internal.ExpectationException))]
		public void UnexpectedInvocation()
		{
			Mockery mocks = new Mockery();
			IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof(IHelloWorld));
			
			Expect.Once.On(helloWorld).Method("Hello").WithNoArguments();
			Expect.Between(2,4).On(helloWorld).Method("Ask").With("What color is the fish?")
				.Will(Return.Value("purple"));
			Expect.AtLeast(1).On(helloWorld).Method("Ask").With("How big is the fish?")
				.Will(Throw.Exception(new InvalidOperationException("stop asking about the fish!")));
			
			helloWorld.Hello();
			helloWorld.Ask("What color is the fish?");
			helloWorld.Ask("What color is the hippo?");
		}

		[Test, ExpectedException(typeof(NMock2.Internal.ExpectationException))]
		public void IndexerSet()
		{
			Mockery mocks = new Mockery();
			ISyntacticSugar sugar = (ISyntacticSugar) mocks.NewMock(typeof(ISyntacticSugar), "sugar");
			
			Expect.Once.On(sugar).Set[10,"goodbye"].To(12);
			
			sugar[10,"hello"] = 11;
		}

		[Test, ExpectedException(typeof(NMock2.Internal.ExpectationException))]
		public void EventAdd()
		{
			Mockery mocks = new Mockery();
			ISyntacticSugar sugar = (ISyntacticSugar) mocks.NewMock(typeof(ISyntacticSugar), "sugar");
			
			Expect.Once.On(sugar).EventRemove("Actions",Is.Anything);

			sugar.Actions += new Action(DoAction);
		}
		
		private void DoAction()
		{
			throw new NotSupportedException();
		}
	}
}
