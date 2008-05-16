using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.API;
using System.Reflection;

namespace Kistl.API.Tests
{
    [TestFixture]
    public class APIInitTest
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SecondInitFail()
        {
            Kistl.API.APIInit init = new Kistl.API.APIInit();
            init.Init(@"..\..\DefaultConfig_API.Tests.xml");
        }
    }
}
