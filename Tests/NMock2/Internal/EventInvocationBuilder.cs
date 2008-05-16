using System;
using NMock2.Syntax;

namespace NMock2.Internal
{
	public class EventInvocationBuilder : IEventSyntax, IEventArgumentSyntax
	{
		private IMockObject mock;
		private string eventName;
		
		public EventInvocationBuilder(string eventName)
		{
			this.eventName = eventName;
		}
		
		public IEventArgumentSyntax On(object o)
		{
			if (!(o is IMockObject))
			{
				throw new ArgumentException("Must be a mock object.");
			}
			mock = o as IMockObject;
			return this;
		}

		public void With(params object[] args)
		{
			mock.RaiseEvent(eventName, args);
		}
	}
}
