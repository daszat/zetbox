using System.IO;
using NMock2.Monitoring;

namespace NMock2.Actions
{
	public class ReturnAction : IAction
	{
		private readonly object result;

		public ReturnAction(object result)
		{
			this.result = result;
		}
		
		public void Invoke(Invocation invocation)
		{
			invocation.Result = result;
		}

		public void DescribeTo(TextWriter writer)
		{
			writer.Write("return ");
			writer.Write(result);
		}
	}
}
