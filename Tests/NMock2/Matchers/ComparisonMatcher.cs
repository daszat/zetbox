using System;
using System.IO;

namespace NMock2.Matchers
{
	public class ComparisonMatcher : Matcher
	{
		private readonly IComparable value;
		private readonly int minComparisonResult;
		private readonly int maxComparisonResult;

		public ComparisonMatcher(IComparable value, int comparisonResult1, int comparisonResult2)
		{
			this.value = value;
			this.minComparisonResult = Math.Min(comparisonResult1, comparisonResult2) ;
			this.maxComparisonResult = Math.Max(comparisonResult1, comparisonResult2) ;
			
			if (minComparisonResult == -1 && maxComparisonResult == 1)
			{
				throw new ArgumentException("comparison result range too large",
											"comparisonResult1, comparisonResult2");
			}
		}
		
		public override bool Matches(object o)
		{
			if (o.GetType() == value.GetType())
			{
				int comparisonResult = -(value.CompareTo(o));
				return comparisonResult >= minComparisonResult
					&& comparisonResult <= maxComparisonResult;
			}
			else
			{
				return false;
			}
		}
	
		public override void DescribeTo(TextWriter writer)
		{
			writer.Write("? ");
			if (minComparisonResult == -1) writer.Write("<");
			if (maxComparisonResult == 1) writer.Write(">");
			if (minComparisonResult == 0 || maxComparisonResult == 0) writer.Write("=");
			writer.Write(" ");
			writer.Write(value);
		}
	}
}
