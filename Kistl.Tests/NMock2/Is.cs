using System;
using System.Collections;
using NMock2.Matchers;

namespace NMock2
{
	public class Is
	{
		public static readonly Matcher Anything = new AlwaysMatcher(true, "anything");
		public static readonly Matcher Nothing = new AlwaysMatcher(false, "nothing");
		public static readonly Matcher Null = new NullMatcher();
		public static readonly Matcher NotNull = new NotMatcher(Null);
        	public static readonly Matcher Out = new ArgumentsMatcher.OutMatcher();

		public static Matcher EqualTo(object expected)
		{
			return new EqualMatcher(expected);
		}
		
		public static Matcher Same(object expected)
		{
			return new SameMatcher(expected);
		}

		public static Matcher StringContaining(string substring)
		{
			return new StringContainsMatcher(substring);
		}

		public static Matcher GreaterThan(IComparable value)
		{
			return new ComparisonMatcher(value, 1, 1);
		}

		public static Matcher AtLeast(IComparable value)
		{
			return new ComparisonMatcher(value, 0, 1);
		}

		public static Matcher LessThan(IComparable value)
		{
			return new ComparisonMatcher(value, -1, -1);
		}

		public static Matcher AtMost(IComparable value)
		{
			return new ComparisonMatcher(value, -1, 0);
		}
	
		public static Matcher In(ICollection collection)
		{
			return new ElementMatcher(collection);
		}
		
		public static Matcher OneOf(params object[] elements)
		{
			return new ElementMatcher(elements);
		}

        public static Matcher TypeOf(Type type)
        {
            return new TypeMatcher(type);
        }
	}
}

