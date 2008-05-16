using System;
using System.Collections;
using NMock2.Monitoring;

namespace NMock2.Internal
{
	public class OrderedExpectations : ExpectationsOrderingBase, IExpectationOrdering
	{
		private int current = 0;
		
		public OrderedExpectations(int depth)
		{
			this.prompt = "Ordered:";
			this.depth = depth;
		}
        
		public bool IsActive
		{
			get { return CurrentExpectation.IsActive; }
		}
		
		public bool HasBeenMet
		{
			get { return CurrentExpectation.HasBeenMet && NextExpectationHasBeenMet(); }
		}
		
		public bool Matches(Invocation invocation)
		{
			return CurrentExpectation.Matches(invocation) ||
			       (CurrentExpectation.HasBeenMet && NextExpectationMatches(invocation));
		}
		
		public void Perform(Invocation invocation)
		{
			// If the current expectation doesn't match, it must have been met, by the contract
			// for the IExpectation interface and due to the implementation of this.Matches
			
			if (!CurrentExpectation.Matches(invocation)) current++;
			CurrentExpectation.Perform(invocation);
		}

		private bool NextExpectationMatches(Invocation invocation)
		{
			return HasNextExpectation && NextExpectation.Matches(invocation);
		}
				
		private IExpectation CurrentExpectation
		{
			get { return (IExpectation)expectations[current]; }
		}

		private bool NextExpectationHasBeenMet()
		{
			return (!HasNextExpectation) || NextExpectation.HasBeenMet;
		}

		private bool HasNextExpectation
		{
			get { return current < expectations.Count-1; }
		}
        	
		private IExpectation NextExpectation
		{
			get { return (IExpectation)expectations[current+1]; }
		}        	
	}
}
