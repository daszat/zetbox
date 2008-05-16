using System;
using System.Collections;
using System.IO;

namespace NMock2.Internal
{
	public abstract class ExpectationsOrderingBase 
	{
		protected ArrayList expectations = new ArrayList();
		protected int depth = 0;
		protected string prompt;

		public void AddExpectation(IExpectation expectation)
		{
			expectations.Add(expectation);
		}

		public virtual void DescribeActiveExpectationsTo(TextWriter writer)
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

		public virtual void DescribeUnmetExpectationsTo(TextWriter writer)
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

		protected void Indent(TextWriter writer, int n)
		{
			for (int i = 0; i < n; i++)
				writer.Write("  ");
		}
	}
}
