using System.IO;
using System.Reflection;
using NMock2.Monitoring;

namespace NMock2.Matchers
{
    public class ArgumentsMatcher : Matcher
    {
        private static readonly object OUT_PARAMETER = new object();
		
        private Matcher[] valueMatchers;

        public ArgumentsMatcher(params Matcher[] valueMatchers)
        {
			this.valueMatchers = valueMatchers;
        }
        
        public override bool Matches(object o)
        {
            return o is Invocation
                && MatchesArguments((Invocation)o);
        }
		
        private bool MatchesArguments(Invocation invocation)
        {
            return invocation.Parameters.Count == valueMatchers.Length
                && MatchesArgumentValues(invocation);
        }
		
        private bool MatchesArgumentValues(Invocation invocation)
        {
        	ParameterInfo[] paramsInfo = invocation.Method.GetParameters();
            
            for (int i = 0; i < invocation.Parameters.Count; i++)
            {
                object value = paramsInfo[i].IsOut ? OUT_PARAMETER : invocation.Parameters[i];

                if (!valueMatchers[i].Matches(value)) return false;
            }

            return true;
        }
        
        public override void DescribeTo(TextWriter writer)
        {
            writer.Write("(");
        	WriteListOfMatchers(MatcherCount(), writer);
        	writer.Write(")");
        }
		
    	protected int MatcherCount()
    	{
    		return valueMatchers.Length;
    	}
		
		protected Matcher LastMatcher()
		{
			return valueMatchers[valueMatchers.Length-1];
		}
		
    	protected void WriteListOfMatchers(int listLength, TextWriter writer)
    	{
    		for (int i = 0; i < listLength; i++)
    		{
    			if (i > 0) writer.Write(", ");
    			valueMatchers[i].DescribeTo(writer);
    		}
    	}
		
    	public class OutMatcher : Matcher
        {
            public override bool Matches(object o)
            {
                return o == OUT_PARAMETER;
            }

            public override void DescribeTo(TextWriter writer)
            {
                writer.Write("out");
            }
        }
    }
}
