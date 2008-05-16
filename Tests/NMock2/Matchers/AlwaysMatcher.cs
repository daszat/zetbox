using System.IO;

namespace NMock2.Matchers
{
	public class AlwaysMatcher : Matcher
	{
		private bool matches;
		private string description;

		public AlwaysMatcher(bool matches, string description)
		{
			this.matches = matches;
			this.description = description;
		}

		public override bool Matches(object o)
		{
			return matches;
		}
		
		public override void DescribeTo(TextWriter writer)
		{
			writer.Write(description);
		}
	}
}
