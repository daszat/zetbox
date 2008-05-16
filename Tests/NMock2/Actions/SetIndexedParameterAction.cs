using System.IO;
using NMock2.Monitoring;

namespace NMock2.Actions
{
	public class SetIndexedParameterAction : IAction
	{
		private readonly int index;
		private readonly object value;

		public SetIndexedParameterAction(int index, object value)
		{
			this.index = index;
			this.value = value;
		}
		
		public void Invoke(Invocation invocation)
		{
			invocation.Parameters[index] = value;
		}

		public void DescribeTo(TextWriter writer)
		{
			writer.Write("set arg ");
			writer.Write(index);
			writer.Write("=");
			writer.Write(value);
		}
	}
}
