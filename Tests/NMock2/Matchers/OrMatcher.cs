using System.IO;

namespace NMock2.Matchers
{
	public class OrMatcher : BinaryOperator
	{
		public OrMatcher(Matcher left, Matcher right) : base(left,right)
		{
		}
		
		public override bool Matches(object o)
		{
			return left.Matches(o) || right.Matches(o);
		}

		public override void DescribeTo(TextWriter writer)
		{
			writer.Write("`");
			left.DescribeTo(writer);
			writer.Write("' or `");
			right.DescribeTo(writer);
			writer.Write("'");
		}
	}
}
