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
                if (_current == null)
                {
                    var attr = (KistlGeneratedVersionAttribute)Assembly
                                .Load(Helper.InterfaceAssembly)
                                .GetCustomAttributes(typeof(KistlGeneratedVersionAttribute), false)
                                .Single();
                    _current = attr.Version;
                }
                return _current.Value;
            }
        }
    }
}
