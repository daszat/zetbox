using System;

namespace NMock2.Monitoring
{
	public class Invoker : IInvokable
	{
		private readonly Type targetType;
		private readonly object target;
		private readonly IInvokable next;
		
		public Invoker(Type targetType, object target, IInvokable next)
		{
			this.targetType = targetType;
			this.target = target;
			this.next = next;
		}
		
		public void Invoke(Invocation invocation)
		{
			if (targetType == invocation.Method.DeclaringType)
			{
				invocation.InvokeOn(target);
			}
			else
			{
				next.Invoke(invocation);
			}
		}
	}
}
