
namespace Kistl.API.Migration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.Serialization;

    [Serializable]
    public class MigrationException
        : Exception
    {
        public MigrationException() : base() { }
        public MigrationException(string message) : base(message) { }
        protected MigrationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public MigrationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
