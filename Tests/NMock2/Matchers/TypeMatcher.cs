using System;
using System.IO;

namespace NMock2.Matchers
{
	public class TypeMatcher : Matcher
	{
        private readonly Type type;

		public TypeMatcher(Type type)
		{
            this.type = type;
		}

	    public override bool Matches(object o)
	    {
	        return type.IsAssignableFrom(o.GetType()); 
	    }

	    public override void DescribeTo(TextWriter writer)
	    {
	        writer.Write("type assignable to ");
            writer.Write(type);
	    }
	}
}
