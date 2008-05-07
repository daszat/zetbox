using NUnit.Framework;
using NMock2.Matchers;

namespace NMock2.Test.Matchers
{
	[TestFixture]
	public class NullMatcherTest
	{
		[Test]
		public void MatchesNullReferences()
		{
			Matcher matcher = new NullMatcher();

			Assert.IsTrue( matcher.Matches(null), "null");
			Assert.IsFalse( matcher.Matches(new object()), "not null");
		}

		[Test]
		public void ProvidesAReadableDescription()
		{
			AssertDescription.IsEqual(new NullMatcher(), "null");
		}
	}
}
