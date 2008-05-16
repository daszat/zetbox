using System;
using NMock2.Internal;
using NMock2.Syntax;

namespace NMock2
{
	public sealed class Fire
	{
		public static IEventSyntax Event(string eventName)
		{
			return new EventInvocationBuilder(eventName);
		}
	}
}
