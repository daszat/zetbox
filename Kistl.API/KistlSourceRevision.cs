
namespace Kistl.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Shows the version of the source from which a given assembly was built.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class KistlSourceRevisionAttribute : Attribute
    {
        private readonly string _revision;
        public KistlSourceRevisionAttribute(string revision)
        {
            _revision = revision;
        }

        public String Revision { get { return _revision; } }
    }
}
