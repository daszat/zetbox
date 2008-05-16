using System.Collections;
using NUnit.Framework;
using NMock2.Matchers;

namespace NMock2.Test.Matchers
{
    [TestFixture]
    public class ElementMatcherTest
    {
	[Test]
	public void MatchesIfArgumentInCollection() {
	    ICollection collection = CollectionOf(1,2,3,4);
	    
	    Matcher matcher = new ElementMatcher(collection);
	    
	    Assert.IsTrue(matcher.Matches(1), "should match 1");
	    Assert.IsTrue(matcher.Matches(2), "should match 2");	    
	    Assert.IsTrue(matcher.Matches(3), "should match 3");	    
	    Assert.IsTrue(matcher.Matches(4), "should match 4");	    
	    Assert.IsFalse(matcher.Matches(0), "should not match 0");
	}
	
	[Test]
	public void IsNullSafe() {
	    ICollection collection = CollectionOf(1,2,null,4);
	    
	    Matcher matcher = new ElementMatcher(collection);
	    
	    Assert.IsTrue(matcher.Matches(1), "should match 1");
	    Assert.IsTrue(matcher.Matches(2), "should match 2");	    
	    Assert.IsTrue(matcher.Matches(null), "should match null");
	    Assert.IsTrue(matcher.Matches(4), "should match 4");	    
	    Assert.IsFalse(matcher.Matches(0), "should not match 0");
	}
	
	[Test]
	public void HasAReadableDescription()
	{
	    AssertDescription.IsEqual(
	        new ElementMatcher(CollectionOf("a","b", 1, 2)),
		"element of [\"a\", \"b\", <1>, <2>]");
	    
	    AssertDescription.IsEqual(
	        new ElementMatcher(CollectionOf()),
		"element of []");
	}
	
	private ICollection CollectionOf(params object[] elements) {
	    return elements;
	}
    }
}
