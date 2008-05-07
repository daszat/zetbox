using NMock2.Matchers;

namespace NMock2
{
	public class Has
	{
		public static Matcher ToString(Matcher stringMatcher)
		{
			return new ToStringMatcher(stringMatcher);
		}

		public static Matcher Property(string propertyName, Matcher valueMatcher)
		{
			return new PropertyMatcher(propertyName, valueMatcher);
		}

        public static Matcher Field(string fieldName, Matcher valueMatcher)
        {
            return new FieldMatcher(fieldName, valueMatcher);
        }
	}
}

