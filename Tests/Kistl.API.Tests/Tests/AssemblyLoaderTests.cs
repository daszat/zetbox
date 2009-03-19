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
            var textCtx = new TestApplicationContext();
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
        [ExpectedException(typeof(FileNotFoundException))]
        public void AssemblyResolve()
        {
            Assembly a = Assembly.Load("test");
            Assert.That(a, Is.Null);
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void AssemblyResolveReflection()
        {
            Assembly a = Assembly.ReflectionOnlyLoad("test");
            Assert.That(a, Is.Null);
        }
    }
}
