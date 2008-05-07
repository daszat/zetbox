using System.IO;

namespace NMock2.Matchers
{
	public class StringContainsMatcher : Matcher
	{
		private readonly string substring;
		
		public StringContainsMatcher(string substring)
		{
			this.substring = substring;
		}
		
		public override bool Matches(object o)
		{
			return o != null
				&& o is string
				&& ((string)o).IndexOf(substring) >= 0;
		}
		
		public override void DescribeTo(TextWriter writer)
		{
			writer.Write("containing ");
			writer.Write((object)substring);
		}
	}
}
