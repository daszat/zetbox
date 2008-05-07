using System.Collections;
using NUnit.Framework;
using NMock2.Matchers;

namespace NMock2.Test.Matchers
{
	[TestFixture]
	public class EqualMatcherTest
	{
		const string EXPECTED = "expected";
		
		[Test]
		public void ComparesArgumentForEqualityToExpectedObject()
		{
			Matcher matcher = new EqualMatcher(EXPECTED);
			
			Assert.IsTrue( matcher.Matches(EXPECTED), "same object" );
			Assert.IsTrue( matcher.Matches(EXPECTED.Clone()), "equal object" );
			Assert.IsFalse( matcher.Matches("not expected"), "unequal object" );
		}
		
		[Test]
		public void IsNullSafe()
		{
			Assert.IsTrue( new EqualMatcher(null).Matches(null), "null matches null" );
			Assert.IsFalse( new EqualMatcher("not null").Matches(null), "not null does not match null" );
			Assert.IsFalse( new EqualMatcher(null).Matches("not null"), "null does not match not null" );
		}
		
		[Test]
		public void ComparesArraysForEqualityByContents()
		{
			int[] expected = {1,2};
			int[] equal = {1,2};
			int[] inequal = {2,3};
			int[] longer = {1,2,3};
			int[] shorter = {1};
			int[] empty = {};
			int[,] otherRank = {{1,2},{3,4}};
			Matcher matcher = new EqualMatcher(expected);
			
			Assert.IsTrue(matcher.Matches(expected), "same array");
			Assert.IsTrue(matcher.Matches(equal), "same contents");
			Assert.IsFalse(matcher.Matches(inequal), "different contents");
			Assert.IsFalse(matcher.Matches(longer), "longer");
			Assert.IsFalse(matcher.Matches(shorter), "shorter");
			Assert.IsFalse(matcher.Matches(empty), "empty");
			Assert.IsFalse(matcher.Matches(otherRank), "other rank");
		}
	        
		[Test]
		public void ComparesMultidimensionalArraysForEquality()
		{
			int[,] expected = {{1,2},{3,4}};
			int[,] equal = {{1,2},{3,4}};
			int[,] inequal = {{3,4},{5,6}};
			int[,] empty = new int[0,0];
			int[] otherRank = {1,2};
			Matcher matcher = new EqualMatcher(expected);
			
			Assert.IsTrue(matcher.Matches(expected), "same array");
			Assert.IsTrue(matcher.Matches(equal), "same contents");
			Assert.IsFalse(matcher.Matches(inequal), "different contents");
			Assert.IsFalse(matcher.Matches(empty), "empty");
			Assert.IsFalse(matcher.Matches(otherRank), "other rank");
		}
	        
		[Test]
		public void RecursivelyComparesArrayContentsOfNestedArrays()
		{
			int[][] expected = new int[][] {new int[]{1,2},new int[]{3,4}};
			int[][] equal = new int[][] {new int[]{1,2},new int[]{3,4}};
			int[][] inequal = new int[][] {new int[]{2,3},new int[]{4,5}};
			Matcher matcher = new EqualMatcher(expected);
			
			Assert.IsTrue(matcher.Matches(expected), "same array");
			Assert.IsTrue(matcher.Matches(equal), "same contents");
			Assert.IsFalse(matcher.Matches(inequal), "different contents");
		}

		[Test]
		public void RecursivelyComparesContentsOfNestedLists()
		{
		    ArrayList expected = new ArrayList(new ArrayList[]{
			new ArrayList(new int[]{1,2}),
			new ArrayList(new int[]{3,4})});
		    ArrayList equal = new ArrayList(new ArrayList[]{
			new ArrayList(new int[]{1,2}),
			new ArrayList(new int[]{3,4})});
		    ArrayList inequal = new ArrayList(new ArrayList[]{
			new ArrayList(new int[]{2,3}),
			new ArrayList(new int[]{4,5})});
		    Matcher matcher = new EqualMatcher(expected);
		    
		    Assert.IsTrue(matcher.Matches(expected), "same array");
		    Assert.IsTrue(matcher.Matches(equal), "same contents");
		    Assert.IsFalse(matcher.Matches(inequal), "different contents");
		}
	        
		[Test]
		public void CanCompareAutoboxedValues()
		{
			Matcher matcher = new EqualMatcher(1);
			Assert.IsTrue(matcher.Matches(1), "equal value");
			Assert.IsFalse(matcher.Matches(2), "other value");
		}

		[Test]
		public void HasAReadableDescription()
		{
			NamedObject value = new NamedObject("value");
			AssertDescription.IsEqual(new EqualMatcher(value), "equal to <"+value+">");
		}
	}
}
