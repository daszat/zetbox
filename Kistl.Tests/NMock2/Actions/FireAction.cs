using System.IO;

using NMock2.Internal;
using NMock2.Monitoring;

namespace NMock2.Actions
{
    public class FireAction : IAction
    {
        private readonly string eventName;
        private readonly object[] eventArgs;

        public FireAction(string eventName, params object[] eventArgs)
        {
            this.eventName = eventName;
            this.eventArgs = eventArgs;
        }

        public void Invoke(Invocation invocation)
        {
            ((IMockObject) invocation.Receiver).RaiseEvent(eventName, eventArgs);
        }

        public void DescribeTo(TextWriter writer)
        {
            writer.Write("fire ");
            writer.Write(eventName);
        }
    }
}
