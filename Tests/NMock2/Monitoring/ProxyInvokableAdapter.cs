using System;
using System.Collections;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace NMock2.Monitoring
{
	public class ProxyInvokableAdapter : RealProxy
	{
		private readonly IInvokable invokable;

		public ProxyInvokableAdapter(Type proxyType, IInvokable invokable ) :
			base(proxyType)
		{
			this.invokable = invokable;
		}
		
		public override IMessage Invoke(IMessage msg)
		{
			MethodCall call = new MethodCall(msg);
			ParameterInfo[] parameters = call.MethodBase.GetParameters();
			Invocation invocation = new Invocation(GetTransparentProxy(),
												   (MethodInfo)call.MethodBase,
											       call.Args);
			
			invokable.Invoke(invocation);
			
			if (invocation.IsThrowing)
			{
				//TODO: it is impossible to set output parameters and throw an exception,
				//      even though this is allowed by .NET method call semantics.
				return new ReturnMessage(invocation.Exception, call);
			}
			else
			{
				object[] outArgs = CollectOutputArguments(invocation, call, parameters);
				return new ReturnMessage( invocation.Result, outArgs, outArgs.Length,
				                          call.LogicalCallContext, call );
			}
		}
		
		private static object[] CollectOutputArguments(Invocation invocation,
			                                           MethodCall call,
			                                           ParameterInfo[] parameters)
		{
			ArrayList outArgs = new ArrayList(call.ArgCount);
			
			for (int i = 0; i < call.ArgCount; i++)
			{
				if (!parameters[i].IsIn) outArgs.Add(invocation.Parameters[i]);
			}
			
			return outArgs.ToArray();
		}
	}
}
