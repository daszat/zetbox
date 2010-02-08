using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Kistl.API.Mocks;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Kistl.API.Tests
{
    [TestFixture]
    public class AssemblyLoaderTests
    {
        [SetUp]
        public void SetUp()
        {
            var textCtx = new TestApplicationContext("Kistl.API.Tests.Config.xml");
            textCtx.Configuration.SourceFileLocation = new string[] { "." };
        }

        // TODO: Load AssemblyLoader in AppDomain to _really_ test this stuff
        //[Test]
        //public void Load()
        //{
        //    Assembly a = AssemblyLoader.Load("Kistl.API");
        //    Assert.That(a, Is.Not.Null);
        //}

        //[Test]
        //public void ReflectionOnlyLoadFrom()
        //{
        //    Assembly a = AssemblyLoader.ReflectionOnlyLoadFrom("Kistl.API");
        //    Assert.That(a, Is.Not.Null);
        //}

        [Test]
        public void AssemblyResolve()
        {
        	Assert.That(() => Assembly.Load("test"), Throws.InstanceOf<FileNotFoundException>());
        }

        [Test]
        public void AssemblyResolveReflection()
        {
        	Assert.That(() => Assembly.ReflectionOnlyLoad("test"), Throws.InstanceOf<FileNotFoundException>());
        }
    }
}
