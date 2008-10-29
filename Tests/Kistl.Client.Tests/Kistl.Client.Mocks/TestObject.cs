using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.Client.Mocks
{
    public interface TestObject : IDataObject
    {
        string TestString { get; set; }
    }
}
