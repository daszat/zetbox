using System.IO;
using NMock2.Internal;
using NMock2.Matchers;

namespace NMock2
{
	public abstract class Matcher : ISelfDescribing
	{
		public abstract bool Matches(object o);
		public abstract void DescribeTo(TextWriter writer);

		public override string ToString()
		{
			DescriptionWriter writer = new DescriptionWriter();
			DescribeTo(writer);
			return writer.ToString();
		}

		public static Matcher operator& (Matcher m1, Matcher m2)
		{
			return new AndMatcher(m1,m2);
		}
		
		public static Matcher operator| (Matcher m1, Matcher m2)
		{
			return new OrMatcher(m1,m2);
		}

		public static Matcher operator! (Matcher m)
		{
			return new NotMatcher(m);
		}
	}
}

