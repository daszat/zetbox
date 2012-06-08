// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Zetbox.API
{
    /// <summary>
    /// Identifies the generated code by a GUID
    /// </summary>
    /// <remarks>
    /// [assembly: Zetbox.API.ZetboxGeneratedVersion("")]
    /// </remarks>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class ZetboxGeneratedVersionAttribute : Attribute
    {
        private static readonly object _lock = new object();

        private readonly Guid _version;
        public ZetboxGeneratedVersionAttribute(Guid version)
        {
            _version = version;
        }
        public ZetboxGeneratedVersionAttribute(string version)
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
                        var attr = (ZetboxGeneratedVersionAttribute)Assembly
                                    .Load(Helper.InterfaceAssembly)
                                    .GetCustomAttributes(typeof(ZetboxGeneratedVersionAttribute), false)
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
                throw new InvalidZetboxGeneratedVersionException(string.Format("Invalid Zetbox Generated Version. Current: {0}, to check: {1}", Current, versionToCheck));
        }
    }

    [Serializable]
    public class InvalidZetboxGeneratedVersionException : Exception
    {
        public InvalidZetboxGeneratedVersionException() : this("Invalid Zetbox Generated Version") { }
        public InvalidZetboxGeneratedVersionException(string message) : base(message) { }
        public InvalidZetboxGeneratedVersionException(string message, Exception inner) : base(message, inner) { }
        protected InvalidZetboxGeneratedVersionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
