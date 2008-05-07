using NMock2.Matchers;

using NUnit.Framework;

namespace NMock2.Test.Matchers
{
    [TestFixture]
	public class TypeMatcherTest
	{
		[Test] public void MatchesValueOfAssignableType()
		{
            Matcher m = new TypeMatcher(typeof(B));

            Assert.IsTrue(m.Matches(new B()), "should match B");
            Assert.IsTrue(m.Matches(new D()), "should match D");
		}

        [Test] public void DoesNotMatchValueOfNonAssignableType()
        {
            Matcher m = new TypeMatcher(typeof(D));

            Assert.IsFalse(m.Matches(new B()), "should not match B");
            Assert.IsFalse(m.Matches(123), "should not match B");
            Assert.IsFalse(m.Matches("hello, world"), "should not match B");
        }

        private class B {}
        private class D : B {}
	}
}
