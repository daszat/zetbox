using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.Client.Presentables;

using NUnit.Framework;

namespace Kistl.Client.Tests
{
    [TestFixture]
    public class DataObjectModelTests
    {
        [Test]
        public void TestDesignMock()
        {
            DataObjectModel.CreateDesignMock();
        }
    }
}
