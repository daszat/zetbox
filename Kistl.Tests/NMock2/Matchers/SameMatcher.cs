using System.IO;

namespace NMock2.Matchers
{
	public class SameMatcher : Matcher
	{
		private object expected;

		public SameMatcher(object expected)
		{
			this.expected = expected;
		}

		public override bool Matches(object o)
		{
			return expected == o;
		}
		
		public override void DescribeTo(TextWriter writer)
		{
			writer.Write("same as ");
			writer.Write(expected);
		}
	}
}
