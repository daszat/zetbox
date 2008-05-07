using System;
using System.Reflection;

namespace NMock2.Monitoring
{
	public class ProxiedObjectIdentity : IInvokable
	{
		private static readonly MethodInfo EQUALS_METHOD =
			typeof(object).GetMethod("Equals",new Type[]{typeof(object)});
		
		private readonly object identityProvider;
		private readonly IInvokable next;

		public ProxiedObjectIdentity(object identityProvider, IInvokable next)
		{
			this.identityProvider = identityProvider;
			this.next = next;
		}
		
		public void Invoke(Invocation invocation)
		{
			if (invocation.Method.DeclaringType == typeof(object))
			{
				if (invocation.Method.Equals(EQUALS_METHOD))
				{
					invocation.Result = Object.ReferenceEquals(invocation.Receiver, invocation.Parameters[0]);
				}
				else
				{
					invocation.InvokeOn(identityProvider);
				}
			}
			else
			{
				next.Invoke(invocation);
			}
		}
	}
}
