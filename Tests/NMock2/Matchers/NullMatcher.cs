using System.IO;

namespace NMock2.Matchers
{
	public class NullMatcher : Matcher
	{
		public override bool Matches(object o)
		{
			return o == null;
		}
		
		public override void DescribeTo(TextWriter writer)
		{
			writer.Write("null");
		}
	}
}
