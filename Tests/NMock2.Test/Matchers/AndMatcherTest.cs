using NUnit.Framework;
using NMock2.Matchers;

namespace NMock2.Test.Matchers
{
	[TestFixture]
	public class AndMatcherTest
	{
		static readonly object ignored = new object();
		static readonly Matcher TRUE = new AlwaysMatcher(true,"TRUE");
		static readonly Matcher FALSE = new AlwaysMatcher(false, "FALSE");

		static readonly object[,] truthTable = {
			{FALSE,	FALSE,	false},
			{FALSE,	TRUE,	false},
			{TRUE,	FALSE,	false},
			{TRUE,	TRUE,	true}
		};
		
		[Test]
		public void CalculatesLogicalConjunctionOfTwoMatchers()
		{
			for (int i = 0; i < truthTable.GetLength(0); i++)
			{
				Matcher matcher = new AndMatcher((Matcher)truthTable[i,0], (Matcher)truthTable[i,1]);

				Assert.AreEqual( truthTable[i,2], matcher.Matches(ignored) );
			}
		}
		
		[Test]
		public void CanUseOperatorOverloadingAsSyntacticSugar()
		{
			for (int i = 0; i < truthTable.GetLength(0); i++)
			{
				Matcher arg1 = (Matcher)truthTable[i,0];
				Matcher arg2 = (Matcher)truthTable[i,1];
				Matcher matcher = arg1 & arg2;
				
				Assert.AreEqual( truthTable[i,2], matcher.Matches(ignored) );
			}
		}

		[Test]
		public void HasAReadableDescription()
		{
			Matcher left = new MatcherWithDescription("<left>");
			Matcher right = new MatcherWithDescription("<right>");
			
			AssertDescription.IsComposed( new AndMatcher(left,right), "`{0}' and `{1}'", left, right);
		}
	}
}
