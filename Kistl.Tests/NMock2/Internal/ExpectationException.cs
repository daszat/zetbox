using System;
using System.Runtime.Serialization;

namespace NMock2.Internal
{
	[Serializable]
	public class ExpectationException : Exception
	{
		public ExpectationException()
		{
		}

		public ExpectationException(string message) : base(message)
		{
		}

		public ExpectationException(string message, Exception innerException) : base(message, innerException)
		{
		}
		
		public ExpectationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
