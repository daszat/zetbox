using System;
using System.Collections;
using System.IO;


namespace NMock2.Matchers
{
    public class ElementMatcher : Matcher
    {
	private ICollection collection;
	
	public ElementMatcher(ICollection collection) 
	{
	    this.collection = collection;
	}
	
	public override bool Matches(object actual)
	{
	    foreach (object element in collection) 
	    {
		if (Equals(element, actual)) return true;
	    }
	    return false;
	}
	
	public override void DescribeTo(TextWriter writer)
	{
	    writer.Write("element of [");
	    
	    bool separate = false;
	    foreach (object element in collection) 
	    {
		if (separate) writer.Write(", ");
		writer.Write(element);
		separate = true;
	    }

	    writer.Write("]");
	}
    }
}
