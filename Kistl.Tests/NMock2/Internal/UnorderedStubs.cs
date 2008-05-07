using System;
using System.IO;

namespace NMock2.Internal
{
	class UnorderedStubs : UnorderedExpectations
	{
		public UnorderedStubs()
		{
			this.depth = 0;
			this.prompt = "Stubs:";
		}

		public override void DescribeActiveExpectationsTo(TextWriter writer)
		{
			if (expectations.Count > 0)
			{
				writer.WriteLine(prompt);
				foreach (IExpectation expectation in expectations)
				{
					if (expectation.IsActive)
					{
						Indent(writer, depth + 1);
						expectation.DescribeActiveExpectationsTo(writer);
						writer.WriteLine();
					}
				}
			}
		}

		public override void DescribeUnmetExpectationsTo(TextWriter writer)
		{
			if (expectations.Count > 0)
			{
				writer.WriteLine(prompt);
				foreach (IExpectation expectation in expectations)
				{
					if (!expectation.HasBeenMet)
					{
						Indent(writer, depth + 1);
						expectation.DescribeUnmetExpectationsTo(writer);
						writer.WriteLine();
					}
				}
			}
		}
	}
}
