using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.Client.Mocks
{
    public interface TestObject : IDataObject
    {
        string TestString { get; set; }
        ICollection<TestObject> TestCollection { get; set; }
    }
}
