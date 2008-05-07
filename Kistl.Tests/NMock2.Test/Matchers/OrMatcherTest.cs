using NUnit.Framework;
using NMock2.Matchers;

namespace NMock2.Test.Matchers
{
	[TestFixture]
	public class OrMatcherTest
	{
		static readonly object ignored = new object();
		static readonly Matcher TRUE = new AlwaysMatcher(true,"TRUE");
		static readonly Matcher FALSE = new AlwaysMatcher(false, "FALSE");

		static readonly object[,] truthTable = {
			{FALSE,	FALSE,	false},
			{FALSE,	TRUE,	true},
			{TRUE,	FALSE,	true},
			{TRUE,	TRUE,	true}
		};
		
		[Test]
		public void CalculatesLogicalDisjunctionOfTwoMatchers()
		{
			for (int i = 0; i < truthTable.GetLength(0); i++)
			{
				Matcher arg1 = (Matcher)truthTable[i,0];
				Matcher arg2 = (Matcher)truthTable[i,1];
				Matcher matcher = new OrMatcher(arg1, arg2);

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
				Matcher matcher = arg1 | arg2;
				
				Assert.AreEqual( truthTable[i,2], matcher.Matches(ignored) );
			}
		}

		[Test]
		public void HasAReadableDescription()
		{
			Matcher left = new MatcherWithDescription("<left>");
			Matcher right = new MatcherWithDescription("<right>");
			
			AssertDescription.IsComposed( new OrMatcher(left,right), "`{0}' or `{1}'", left, right);
		}
	}
}
