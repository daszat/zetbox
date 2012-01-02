using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Kistl.API
{
    /// <summary>
    /// Identifies the generated code by a GUID
    /// </summary>
    /// <remarks>
    /// [assembly: Kistl.API.KistlGeneratedVersion("")]
    /// </remarks>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class KistlGeneratedVersionAttribute : Attribute
    {
        private static readonly object _lock = new object();

        private readonly Guid _version;
        public KistlGeneratedVersionAttribute(Guid version)
        {
            _version = version;
        }
        public KistlGeneratedVersionAttribute(string version)
        {
            _version = new Guid(version);
        }

        public Guid Version { get { return _version; } }

        private static Guid? _current = null;
        public static Guid Current
        {
            get
            {
                lock (_lock)
                {
                    if (_current == null)
                    {
                        var attr = (KistlGeneratedVersionAttribute)Assembly
                                    .Load(Helper.InterfaceAssembly)
                                    .GetCustomAttributes(typeof(KistlGeneratedVersionAttribute), false)
                                    .Single();
                        _current = attr.Version;
                    }
                }
                return _current.Value;
            }
        }

        public static void Check(Guid versionToCheck)
        {
            if (Current != versionToCheck)
                throw new InvalidKistlGeneratedVersionException(string.Format("Invalid Kistl Generated Version. Current: {0}, to check: {1}", Current, versionToCheck));
        }
    }

    [Serializable]
    public class InvalidKistlGeneratedVersionException : Exception
    {
        public InvalidKistlGeneratedVersionException() : this("Invalid Kistl Generated Version") { }
        public InvalidKistlGeneratedVersionException(string message) : base(message) { }
        public InvalidKistlGeneratedVersionException(string message, Exception inner) : base(message, inner) { }
        protected InvalidKistlGeneratedVersionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
