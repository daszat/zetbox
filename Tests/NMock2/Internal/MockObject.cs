using System;
using System.Collections;

using NMock2.Monitoring;

namespace NMock2.Internal
{
    public class MockObject : IInvokable, IMockObject
    {
        private readonly Mockery mockery;
        private readonly Type mockedType;
        private readonly string name;
        private readonly IDictionary eventHandlers;

        protected MockObject(Mockery mockery, Type mockedType, string name)
        {
            this.mockery = mockery;
            this.mockedType = mockedType;
            this.name = name;
            this.eventHandlers = new Hashtable();
        }

        public override string ToString()
        {
            return name;
        }

        public void Invoke(Invocation invocation)
        {
            mockery.Dispatch(invocation);
        }

        public bool HasMethodMatching(Matcher methodMatcher)
        {
            return mockery.TypeHasMethodMatching(mockedType, methodMatcher);
        }

        public void AddExpectation(IExpectation expectation)
        {
            mockery.AddExpectation(expectation);
        }

		  public void AddStub(IExpectation expectation)
		  {
			  mockery.AddStub(expectation);
		  }

		  public void AddEventHandler(string eventName, Delegate handler)
        {
            ArrayList handlers = (ArrayList) eventHandlers[eventName];

            if (handlers == null)
            {
                handlers = new ArrayList();
                eventHandlers.Add(eventName, handlers);
            }

            if (! handlers.Contains(handler)) { handlers.Add(handler); }
        }

        public void RaiseEvent(string eventName, params object[] args)
        {
            ArrayList handlers = (ArrayList) eventHandlers[eventName];
	
            if (handlers != null)
            {
                foreach (Delegate d in (IEnumerable)handlers.Clone()) { d.DynamicInvoke(args); }
            }
        }

        public void RemoveEventHandler(string eventName, Delegate handler)
        {
            ArrayList handlers = (ArrayList) eventHandlers[eventName];

            if (handlers != null)
            {
                handlers.Remove(handler);
                if (handlers.Count == 0)
                {
                    eventHandlers.Remove(eventName);
                }
            }
        }
    }
}
