using NUnit.Framework;

namespace NMock2.AcceptanceTests
{
    [TestFixture]
	public class ArgumentMatchingAcceptanceTest
	{
        public interface IDemanding
        {
            void Take(int foo, int bar, int baz);
        }

        [Test]
		public void CanMixMatcherAndNonMatcherExpectedArguments()
		{
            Mockery mocks = new Mockery();
            IDemanding demanding = (IDemanding) mocks.NewMock(typeof(IDemanding));

            Expect.Once.On(demanding).Method("Take").With(1, 2, 3);
            Expect.Once.On(demanding).Method("Take").With(Is.EqualTo(10), Is.AtLeast(11), Is.AtMost(12));
            Expect.Once.On(demanding).Method("Take").With(20, Is.AtLeast(19), 22);

            demanding.Take(1, 2, 3);
            demanding.Take(10, 11, 12);
            demanding.Take(20, 21, 22);

            mocks.VerifyAllExpectationsHaveBeenMet();
		}
	}
}
