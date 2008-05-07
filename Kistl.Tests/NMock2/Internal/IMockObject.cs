using System;

namespace NMock2.Internal
{
	public interface IMockObject
	{
		bool HasMethodMatching(Matcher methodMatcher);
		void AddExpectation(IExpectation expectation);
		void AddStub(IExpectation expectation);
		void AddEventHandler(string eventName, Delegate handler);
		void RemoveEventHandler(string eventName, Delegate handler);
		void RaiseEvent(string eventName, params object[] args);
	}
}
