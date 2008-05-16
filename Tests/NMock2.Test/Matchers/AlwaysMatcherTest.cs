using NUnit.Framework;
using NMock2.Matchers;

namespace NMock2.Test.Matchers
{
	[TestFixture]
	public class AlwaysMatcherTest
	{
		[Test]
		public void AlwaysReturnsFixedBooleanValueFromMatchesMethod()
		{
			Matcher matcher = new AlwaysMatcher(true, "");
			Assert.IsTrue(matcher.Matches("something"));
			Assert.IsTrue(matcher.Matches("something else"));
			Assert.IsTrue(matcher.Matches(null));
			Assert.IsTrue(matcher.Matches(1));
			Assert.IsTrue(matcher.Matches(1.0));
			Assert.IsTrue(matcher.Matches(new object()));

			matcher = new AlwaysMatcher(false, "");
			Assert.IsFalse(matcher.Matches("something"));
			Assert.IsFalse(matcher.Matches("something else"));
			Assert.IsFalse(matcher.Matches(null));
			Assert.IsFalse(matcher.Matches(1));
			Assert.IsFalse(matcher.Matches(1.0));
			Assert.IsFalse(matcher.Matches(new object()));
		}
		
		[Test]
		public void IsGivenADescription()
		{
			string description = "DESCRIPTION";
			bool irrelevantFlag = false;

			AssertDescription.IsEqual(new AlwaysMatcher(irrelevantFlag, description), description);
		}
	}
}
