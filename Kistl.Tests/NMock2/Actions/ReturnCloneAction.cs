using System;
using System.IO;
using NMock2.Monitoring;

namespace NMock2.Actions
{
	public class ReturnCloneAction : IAction
	{
		private readonly ICloneable prototype;
		
		public ReturnCloneAction(ICloneable prototype)
		{
			this.prototype = prototype;
		}

		public void Invoke(Invocation invocation)
		{
			invocation.Result = prototype.Clone();
		}

		public void DescribeTo(TextWriter writer)
		{
			writer.Write("a clone of ");
			writer.Write(prototype);
		}
	}
}
