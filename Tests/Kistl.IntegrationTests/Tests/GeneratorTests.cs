using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API.Client;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Kistl.IntegrationTests
{
    [TestFixture]
    public class GeneratorTests
    {
        [SetUp]
        public void SetUp()
        {
            System.IO.Directory.CreateDirectory(@"C:\temp\KistlCodeGen\bin\");
            System.IO.Directory.CreateDirectory(@"C:\temp\KistlCodeGen\bin\Debug");
            System.IO.Directory.GetFiles(@"C:\temp\KistlCodeGen\bin\Debug", "Kistl.Objects.*")
                .ToList().ForEach(f => System.IO.File.Delete(f));
        }

        [TearDown]
        public void TearDown()
        {
            System.IO.Directory.GetFiles(@"C:\temp\KistlCodeGen\bin\Debug", "Kistl.Objects.*")
                .ToList().ForEach(f => System.IO.File.Delete(f));
        }


        [Test]
        [Ignore("times out after 30s due to WCF policy")]
        public void Generate()
        {
            Proxy.Current.Generate();
        }
    }
}
