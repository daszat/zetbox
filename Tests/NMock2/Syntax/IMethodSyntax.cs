using System;
using System.Reflection;

namespace NMock2.Syntax
{
	public interface IMethodSyntax
	{
		IArgumentSyntax Method(string name);
		IArgumentSyntax Method(Matcher nameMatcher);
		IArgumentSyntax Method(MethodInfo method);
		
		IValueSyntax SetProperty(string name);
		IMatchSyntax GetProperty(string name);
		
		IGetIndexerSyntax Get { get; }
		ISetIndexerSyntax Set { get; }
		
		IMatchSyntax EventAdd(string eventName, Matcher listenerMatcher);
		IMatchSyntax EventAdd(string eventName, Delegate equalListener);
		IMatchSyntax EventRemove(string eventName, Matcher listenerMatcher);
		IMatchSyntax EventRemove(string eventName, Delegate equalListener);
	}
}
