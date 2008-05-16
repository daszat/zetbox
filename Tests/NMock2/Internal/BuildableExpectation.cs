using System;
using System.Collections;
using System.IO;
using NMock2.Matchers;
using NMock2.Monitoring;

namespace NMock2.Internal
{
	public class BuildableExpectation : IExpectation
	{
		private const string AddEventHandlerPrefix = "add_";
		private const string RemoveEventHandlerPrefix = "remove_";
		private int callCount = 0;
		
		private string expectationDescription;
		private Matcher requiredCountMatcher, matchingCountMatcher;
		
		private Matcher receiverMatcher = new AlwaysMatcher(true, "<any object>");
		private string methodSeparator = ".";
		private Matcher methodMatcher = new AlwaysMatcher(true, "<any method>");
		private Matcher argumentsMatcher = new AlwaysMatcher(true, "(any arguments)");
		private ArrayList extraMatchers = new ArrayList();
		private ArrayList actions = new ArrayList();
		
		public BuildableExpectation(string expectationDescription,
		                            Matcher requiredCountMatcher,
		                            Matcher matchingCountMatcher)
		{
			this.expectationDescription = expectationDescription;
			this.requiredCountMatcher = requiredCountMatcher;
			this.matchingCountMatcher = matchingCountMatcher;
		}
		
		public Matcher ReceiverMatcher
		{
			get { return receiverMatcher; }
			set { receiverMatcher = value; }
		}
		
		public Matcher MethodMatcher
		{
			get { return methodMatcher; }
			set { methodMatcher = value; }
		}

		public Matcher ArgumentsMatcher
		{
			get { return argumentsMatcher; }
			set { argumentsMatcher = value; }
		}

		public void AddInvocationMatcher(Matcher matcher )
		{
			extraMatchers.Add(matcher);
		}
		
		public void AddAction(IAction action)
		{
			actions.Add(action);
		}
		
		public bool Matches(Invocation invocation)
		{
			return IsActive
				&& receiverMatcher.Matches(invocation.Receiver)
				&& methodMatcher.Matches(invocation.Method)
				&& argumentsMatcher.Matches(invocation)
				&& ExtraMatchersMatch(invocation);
		}
		
		private bool ExtraMatchersMatch(Invocation invocation)
		{
			foreach (Matcher matcher in extraMatchers)
			{
				if (!matcher.Matches(invocation)) return false;
			}
			return true;
		}
		
		public void Perform(Invocation invocation)
		{
			callCount++;
			ProcessEventHandlers(invocation);
			foreach (IAction action in actions)
			{
				action.Invoke(invocation);
			}
		}

		private static void ProcessEventHandlers(Invocation invocation)
		{
			if (IsEventAccessorMethod(invocation))
			{
				IMockObject mockObject = invocation.Receiver as IMockObject;
				if (mockObject != null)
				{
					MockEventHandler(invocation, mockObject);
				}
			}
		}

		private static void MockEventHandler(Invocation invocation, IMockObject mockObject)
		{
			Delegate handler = invocation.Parameters[0] as Delegate;

			if (invocation.Method.Name.StartsWith(AddEventHandlerPrefix))
			{
				mockObject.AddEventHandler(
					invocation.Method.Name.Substring(AddEventHandlerPrefix.Length), handler);
			}

			if (invocation.Method.Name.StartsWith(RemoveEventHandlerPrefix))
			{
				mockObject.RemoveEventHandler(
					invocation.Method.Name.Substring(RemoveEventHandlerPrefix.Length),
					handler);
			}
		}

		private static bool IsEventAccessorMethod(Invocation invocation)
		{
			return invocation.Method.IsSpecialName &&
			       (invocation.Method.Name.StartsWith(AddEventHandlerPrefix) ||
			        invocation.Method.Name.StartsWith(RemoveEventHandlerPrefix));
		}

		public bool IsActive
		{
			get { return matchingCountMatcher.Matches(callCount+1); }
		}
		
		public bool HasBeenMet
		{
			get { return requiredCountMatcher.Matches(callCount); }
		}
		
		public void DescribeActiveExpectationsTo(TextWriter writer)
		{
			if (IsActive) DescribeTo(writer);
		}

		public void DescribeUnmetExpectationsTo(TextWriter writer)
		{
			if (!HasBeenMet) DescribeTo(writer);
		}
		
		private void DescribeTo(TextWriter writer)
		{
			writer.Write(expectationDescription);
			writer.Write(": ");
			receiverMatcher.DescribeTo(writer);
			writer.Write(methodSeparator);
			methodMatcher.DescribeTo(writer);
			argumentsMatcher.DescribeTo(writer);
			foreach (Matcher extraMatcher in extraMatchers)
			{
				writer.Write(", ");
				extraMatcher.DescribeTo(writer);
			}
			
			if (actions.Count > 0)
			{
				writer.Write(", will ");
				((IAction)actions[0]).DescribeTo(writer);
				for (int i = 1; i < actions.Count; i++)
				{
					writer.Write(", ");
					((IAction)actions[i]).DescribeTo(writer);
				}
			}
			
			writer.Write(" [called ");
			writer.Write(callCount);
			writer.Write(" time");
			if (callCount != 1) writer.Write("s");
			writer.Write("]");
		}
		
		public void DescribeAsIndexer()
		{
			methodSeparator = "";
		}
	}
}
