using System;
using System.IO;

namespace NMock2.Test.Matchers
{
	public class MatcherWithDescription : Matcher
	{
		private readonly string description;

		public MatcherWithDescription(string description)
		{
			this.description = description;
		}

		public override bool Matches(object o)
		{
			throw new NotImplementedException();
		}

		public override void DescribeTo(TextWriter writer)
		{
			writer.Write(description);
		}
	}
}
