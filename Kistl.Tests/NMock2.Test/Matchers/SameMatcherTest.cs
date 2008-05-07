using NUnit.Framework;
using NMock2.Matchers;

namespace NMock2.Test.Matchers
{
	[TestFixture]
	public class SameMatcherTest
	{
		[Test]
		public void MatchesSameObject()
		{
			object same = new object();
			object other = new object();
			Matcher matcher = new SameMatcher(same);
			
			Assert.IsTrue(matcher.Matches(same), "same");
			Assert.IsFalse(matcher.Matches(other), "other");
		}
		
		[Test]
		public void IsNullSafe()
		{
			Assert.IsTrue( new SameMatcher(null).Matches(null), "null matches null" );
			Assert.IsFalse( new SameMatcher("not null").Matches(null), "not null does not match null" );
			Assert.IsFalse( new SameMatcher(null).Matches("not null"), "null does not match not null" );
		}

		[Test]
		public void HasAReadableDescription()
		{
			object same = new object();
			AssertDescription.IsEqual(new SameMatcher(same), "same as <"+same+">");
		}
	}
}
