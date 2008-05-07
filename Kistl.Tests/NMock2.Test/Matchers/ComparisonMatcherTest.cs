using System;
using NUnit.Framework;
using NMock2.Matchers;

namespace NMock2.Test.Matchers
{
	[TestFixture]
	public class ComparisonMatcherTest
	{
		[Test]
		public void MatchesAComparisonOfAComparableValue()
		{
			Matcher matcher;
			
			matcher = new ComparisonMatcher(10, -1, 0);
			Assert.IsTrue(matcher.Matches(9), "less");
			Assert.IsTrue(matcher.Matches(10), "equal");
			Assert.IsFalse(matcher.Matches(11), "greater");

			matcher = new ComparisonMatcher(10, -1, -1 );
			Assert.IsTrue(matcher.Matches(9), "less");
			Assert.IsFalse(matcher.Matches(10), "equal");
			Assert.IsFalse(matcher.Matches(11), "greater");

			matcher = new ComparisonMatcher(10, 0, 1 );
			Assert.IsFalse(matcher.Matches(9), "less");
			Assert.IsTrue(matcher.Matches(10), "equal");
			Assert.IsTrue(matcher.Matches(11), "greater");
		}

		[Test]
		public void DoesNotMatchObjectOfDifferentType()
		{
			Assert.IsFalse((new ComparisonMatcher(10,0,0)).Matches("a string"));
		}

		[Test,ExpectedException(typeof(ArgumentException))]
		public void CannotCreateComparisonThatMatchesAnything()
		{
			new ComparisonMatcher(10, -1, 1);
		}
		
		[Test]
		public void CanSpecifyMinAndMaxComparisonResultInAnyOrder()
		{
			Matcher matcher = new ComparisonMatcher(10, 0, -1);
			Assert.IsTrue(matcher.Matches(9), "less");
			Assert.IsTrue(matcher.Matches(10), "equal");
			Assert.IsFalse(matcher.Matches(11), "greater");
		}
		
		[Test]
		public void HasReadableDescription()
		{
			AssertDescription.IsEqual(new ComparisonMatcher(10, -1, -1), "? < <10>");
			AssertDescription.IsEqual(new ComparisonMatcher(10, -1, 0), "? <= <10>");
			AssertDescription.IsEqual(new ComparisonMatcher(10, 0, 0), "? = <10>");
			AssertDescription.IsEqual(new ComparisonMatcher(10, 0, 1), "? >= <10>");
			AssertDescription.IsEqual(new ComparisonMatcher(10, 1, 1), "? > <10>");
		}
	}
}
