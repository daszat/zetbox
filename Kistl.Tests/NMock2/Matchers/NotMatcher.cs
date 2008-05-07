using System.IO;

namespace NMock2.Matchers
{
	public class NotMatcher : Matcher
	{
		private readonly Matcher negated;

		public NotMatcher(Matcher negated)
		{
			this.negated = negated;
		}

		public override bool Matches(object o)
		{
			return !negated.Matches(o);
		}

		public override void DescribeTo(TextWriter writer)
		{
			writer.Write("not ");
			negated.DescribeTo(writer);
		}
	}
}
