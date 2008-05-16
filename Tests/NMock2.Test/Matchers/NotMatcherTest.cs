using NUnit.Framework;
using NMock2.Matchers;

namespace NMock2.Test.Matchers
{
	[TestFixture]
	public class NotMatcherTest
	{
		static readonly object ignored = new object();
		static readonly Matcher TRUE = new AlwaysMatcher(true,"TRUE");
		static readonly Matcher FALSE = new AlwaysMatcher(false, "FALSE");

		[Test]
		public void CalculatesTheLogicalNegationOfAMatcher()
		{
			Assert.IsTrue( new NotMatcher(FALSE).Matches(ignored), "not false" );
			Assert.IsFalse( new NotMatcher(TRUE).Matches(ignored), "not true" );
		}
		
		[Test]
		public void CanUseOperatorOverloadingAsSyntacticSugar()
		{
			Assert.IsTrue( (!FALSE).Matches(ignored), "not false" );
			Assert.IsFalse( (!TRUE).Matches(ignored), "not true" );
		}
		
		[Test]
		public void HasAReadableDescription()
		{
			Matcher negated = new MatcherWithDescription("<negated>");
			NotMatcher notMatcher = new NotMatcher(negated);
			AssertDescription.IsComposed(notMatcher, "not {0}", negated );
		}
	}
}

