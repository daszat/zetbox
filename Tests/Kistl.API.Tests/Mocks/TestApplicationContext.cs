using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Mocks
{
    public class TestApplicationContext : ApplicationContext
    {
        public TestApplicationContext()
            : base(HostType.None, "")
        { }
    }
}
