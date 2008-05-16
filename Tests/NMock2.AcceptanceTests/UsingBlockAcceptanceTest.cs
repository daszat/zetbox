using NMock2.Internal;
using NUnit.Framework;

namespace NMock2.AcceptanceTests
{
	[TestFixture]
	public class UsingBlockAcceptanceTest
	{
		[Test, ExpectedException(typeof(ExpectationException))]
		public void AssertsExpectationsAreMetAtEndOfUsingBlock()
		{
			using(Mockery mocks = new Mockery())
			{
				IHelloWorld helloWorld = (IHelloWorld) mocks.NewMock(typeof(IHelloWorld));
				
				Expect.Once.On(helloWorld).Method("Hello").WithNoArguments();
			}
		}
	}
}
