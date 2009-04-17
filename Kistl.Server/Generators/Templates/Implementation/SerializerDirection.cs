using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.Templates.Implementation
{
    public enum SerializerDirection
    {
        FromStream,
        ToStream,
        FromXmlStream,
        ToXmlStream,
    }

    public enum SerializerType
    {
        Binary = 0x01,
        Xml = 0x02,
        All = 0x03,
    }
}
