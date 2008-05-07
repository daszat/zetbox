using NUnit.Framework;
using NMock2.Matchers;

namespace NMock2.Test.Matchers
{
	[TestFixture]
	public class DescriptionOverrideTest
	{
		[Test]
		public void DelegatesMatchingToAnotherMatcher()
		{
			Matcher m1 = new DescriptionOverride("irrelevant", new AlwaysMatcher(true, "always true"));
			Matcher m2 = new DescriptionOverride("irrelevant", new AlwaysMatcher(false, "always false"));

			Assert.IsTrue(m1.Matches(new object()), "m1");
			Assert.IsFalse(m2.Matches(new object()), "m2");
		}

		[Test]
		public void OverridesDescriptionOfOtherMatcherWithThatPassedToConstructor()
		{
			Matcher m1 = new DescriptionOverride("m1", new AlwaysMatcher(true, "always true"));
			Matcher m2 = new DescriptionOverride("m2", new AlwaysMatcher(false, "always false"));

			AssertDescription.IsEqual(m1, "m1");
			AssertDescription.IsEqual(m2, "m2");
		}
	}
}
