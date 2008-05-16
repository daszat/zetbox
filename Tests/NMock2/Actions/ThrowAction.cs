using System;
using System.IO;
using NMock2.Monitoring;

namespace NMock2.Actions
{
	public class ThrowAction : IAction
	{
		private readonly Exception exception;

		public ThrowAction(Exception exception)
		{
			this.exception = exception;
		}
		
		public void Invoke(Invocation invocation)
		{
			invocation.Exception = exception;
		}

		public void DescribeTo(TextWriter writer)
		{
			writer.Write("throw ");
			writer.Write(exception);
		}
	}
}
