using System.IO;

namespace NMock2.Matchers
{
	public class IndexGetterArgumentsMatcher : ArgumentsMatcher
	{
		public IndexGetterArgumentsMatcher(params Matcher[] valueMatchers) : base(valueMatchers)
		{
		}
		
		public override void DescribeTo(TextWriter writer)
		{
			writer.Write("[");
			WriteListOfMatchers(MatcherCount(), writer);
			writer.Write("]");
		}
	}
}
