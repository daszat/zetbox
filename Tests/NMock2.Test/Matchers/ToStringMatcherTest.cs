using NUnit.Framework;
using NMock2.Matchers;

namespace NMock2.Test.Matchers
{
	[TestFixture]
	public class ToStringMatcherTest
	{
		NamedObject arg;
		MockMatcher stringMatcher;
		Matcher matcher;

		[SetUp]
		public void SetUp()
		{
			arg = new NamedObject("arg");
			stringMatcher = new MockMatcher();
			matcher = new ToStringMatcher(this.stringMatcher);
		}
		
		[Test]
		public void PassesResultOfToStringToOtherMatcher()
		{
			stringMatcher.ExpectedMatchesArg = arg.ToString();
			stringMatcher.MatchesResult = true;
			
			Assert.AreEqual(stringMatcher.MatchesResult, matcher.Matches(arg), "result");
			stringMatcher.AssertMatchesCalled("should have passed string representation to stringMatcher");
		}

		[Test]
		public void ReturnsAReadableDescription()
		{
			stringMatcher.DescribeToOutput = "<stringMatcher description>";
			AssertDescription.IsComposed(matcher, "an object with a string representation that is {0}", stringMatcher);
		}
	}
}
