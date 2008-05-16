using System.IO;

namespace NMock2.Matchers
{
	public class IndexSetterArgumentsMatcher : ArgumentsMatcher
	{
		public IndexSetterArgumentsMatcher(params Matcher[] valueMatchers) : base(valueMatchers)
		{
		}

		public override void DescribeTo(TextWriter writer)
		{
			writer.Write("[");
			WriteListOfMatchers(MatcherCount()-1, writer);
			writer.Write("] = (");
			LastMatcher().DescribeTo(writer);
			writer.Write(")");
		}
	}
}
