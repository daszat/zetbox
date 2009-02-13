using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.Server;
using Kistl.API.Server;

namespace Kistl.Server.Tests
{
    //[TestFixture]
    public class GeneratorTests
    {
        [SetUp]
        public void SetUp()
        {
            System.IO.Directory.CreateDirectory(@"C:\temp\KistlCodeGen\bin\");
            System.IO.Directory.CreateDirectory(@"C:\temp\KistlCodeGen\bin\Debug");
            System.IO.Directory.GetFiles(@"C:\temp\KistlCodeGen\bin\", "Kistl.Objects.*")
                .ToList().ForEach(f => System.IO.File.Delete(f));
        }

        [TearDown]
        public void TearDown()
        {
            System.IO.Directory.GetFiles(@"C:\temp\KistlCodeGen\bin\Debug", "Kistl.Objects.*")
                .ToList().ForEach(f => System.IO.File.Delete(f));
        }


        [Test]
        public void Generate()
        {
            //using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            //{
            //    Proxy.Current.Generate();                
            //}
        }
    }
}
