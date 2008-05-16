using System.IO;
using NUnit.Framework;

namespace NMock2.Test.Matchers
{
	class MockMatcher : Matcher
	{
		public object ExpectedMatchesArg = null;
		public bool MatchesResult = false;
		public int MatchesCallCount = 0;

		public override bool Matches(object o)
		{
			MatchesCallCount++;
			Assert.AreEqual(ExpectedMatchesArg, o, "Matches arg");
			return MatchesResult;
		}

		public void AssertMatchesCalled(string messageFormat, params object[] formatArgs)
		{
			AssertMatchesCalled(1, messageFormat, formatArgs);
		}
		
		public void AssertMatchesCalled(int times, string messageFormat, params object[] formatArgs)
		{
			Assert.AreEqual(times, MatchesCallCount, messageFormat, formatArgs);
		}
		
		public TextWriter ExpectedDescribeToWriter = null;
		public string DescribeToOutput = "";
		public int DescribeToCallCount = 0;
				
		public override void DescribeTo(TextWriter writer)
		{
			DescribeToCallCount++;
			if (ExpectedDescribeToWriter != null)
			{
				Assert.AreSame(ExpectedDescribeToWriter, writer, "DescribeTo writer");
			}
			writer.Write(DescribeToOutput);
		}
	}
}
