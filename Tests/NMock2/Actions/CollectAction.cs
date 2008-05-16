using System.IO;

using NMock2.Monitoring;

namespace NMock2.Actions
{
	public class CollectAction : IAction
	{
        private readonly int argumentIndex;
        private object collectedArgumentValue = null;

		public CollectAction(int argumentIndex)
		{
            this.argumentIndex = argumentIndex;
        }

        public void Invoke(Invocation invocation)
        {
            collectedArgumentValue = invocation.Parameters[argumentIndex];
        }

        public void DescribeTo(TextWriter writer)
        {
            writer.Write("collect argument at index ");
            writer.Write(argumentIndex);
        }

        public object Parameter
        {
            get { return collectedArgumentValue; }
        }
    }
}
