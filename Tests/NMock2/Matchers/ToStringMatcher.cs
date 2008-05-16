using System.IO;

namespace NMock2.Matchers
{
	public class ToStringMatcher : Matcher
	{
		private readonly Matcher stringMatcher;

		public ToStringMatcher(Matcher stringMatcher)
		{
			this.stringMatcher = stringMatcher;
		}

		public override bool Matches(object o)
		{
			return stringMatcher.Matches(o.ToString());
		}
		
		public override void DescribeTo(TextWriter writer)
		{
			writer.Write("an object with a string representation that is ");
			stringMatcher.DescribeTo(writer);
		}
	}
}
