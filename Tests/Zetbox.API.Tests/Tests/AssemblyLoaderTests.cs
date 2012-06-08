using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Zetbox.API.Mocks;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Zetbox.API.Tests
{
    [TestFixture]
    public class AssemblyLoaderTests : AbstractApiTestFixture
    {

        // TODO: Load AssemblyLoader in AppDomain to _really_ test this stuff
        //[Test]
        //public void Load()
        //{
        //    Assembly a = AssemblyLoader.Load("Zetbox.API");
        //    Assert.That(a, Is.Not.Null);
        //}

        //[Test]
        //public void ReflectionOnlyLoadFrom()
        //{
        //    Assembly a = AssemblyLoader.ReflectionOnlyLoadFrom("Zetbox.API");
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
