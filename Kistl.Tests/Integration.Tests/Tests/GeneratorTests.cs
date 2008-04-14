using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.Client;
using Kistl.API.Client;

namespace Integration.Tests.Tests
{
    [TestFixture]
    public class GeneratorTests
    {
        [Test]
        public void Generate()
        {
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                Proxy.Current.Generate();                
            }
        }
    }
}
