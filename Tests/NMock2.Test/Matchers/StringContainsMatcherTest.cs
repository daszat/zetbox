using NUnit.Framework;
using NMock2.Matchers;

namespace NMock2.Test.Matchers
{
	[TestFixture]
	public class StringContainsMatcherTest
	{
		[Test]
		public void MatchesIfStringArgumentContainsGivenSubstring()
		{
			string substring = "SUBSTRING";
			Matcher matcher = new StringContainsMatcher(substring);
			
			Assert.IsTrue(matcher.Matches(substring), "arg is substring");
			Assert.IsTrue(matcher.Matches(substring + "X"), "arg starts with substring");
			Assert.IsTrue(matcher.Matches("X" + substring), "arg ends with substring");
			Assert.IsTrue(matcher.Matches("X" + substring + "X"), "arg contains substring");
			Assert.IsFalse(matcher.Matches("XX"), "arg does not contain substring");
			Assert.IsFalse(matcher.Matches(null), "arg is null");
			Assert.IsFalse(matcher.Matches(new object()), "arg is not a string");
		}
		
		[Test]
		public void HasAReadableDescription()
		{
			string substring = "substring";
			AssertDescription.IsEqual(new StringContainsMatcher(substring),
									  "containing \"" + substring + "\"");
		}
	}
}
