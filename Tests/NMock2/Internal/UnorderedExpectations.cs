using System;
using System.Collections;
using NMock2.Monitoring;

namespace NMock2.Internal
{
	public class UnorderedExpectations : ExpectationsOrderingBase, IExpectationOrdering
	{
		public UnorderedExpectations()
		{
			this.depth = 0;
			this.prompt = "Expected:";
		}

		public UnorderedExpectations(int depth)
		{
			this.depth = depth;
			this.prompt = "Unordered:";
		}

		public bool IsActive
		{
			get
			{
				foreach (IExpectation e in expectations)
				{
					if (e.IsActive) return true;
				}
				return false;
			}
		}
		
		public bool HasBeenMet
		{
			get
			{
				foreach (IExpectation e in expectations)
				{
					if (!e.HasBeenMet) return false;
				}
				return true;
			}
		}
		
		public bool Matches(Invocation invocation)
		{
			foreach (IExpectation e in expectations)
			{
				if (e.Matches(invocation)) return true;
			}
			return false;
		}
		
		public void Perform(Invocation invocation)
		{
			foreach (IExpectation e in expectations)
			{
				if (e.Matches(invocation))
				{
					e.Perform(invocation);
					return;
				}
			}

			throw new InvalidOperationException("No matching expectation");
		}
	}
}
