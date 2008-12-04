using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Arebis.CodeGenerator.Templated
{
	[Serializable]
	public class RuntimeException : ApplicationException
	{
		public RuntimeException(Exception innerException)
			: base("A runtime exception occured.", innerException)
		{ }

		protected RuntimeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{ }

}
}
