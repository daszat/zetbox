using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.API;
using System.Reflection;
using System.IO;

namespace API.Tests.Tests
{
    [TestFixture]
    public class AssemblyLoaderTests
    {
        [Test]
        public void Load()
        {
            Assembly a = AssemblyLoader.Load("Kistl.App.Projekte.Client");
            Assert.That(a, Is.Not.Null);
        }

        [Test]
        public void ReflectionOnlyLoadFrom()
        {
            Assembly a = AssemblyLoader.ReflectionOnlyLoadFrom("Kistl.App.Projekte.Client");
            Assert.That(a, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void AssemblyResolve()
        {
            Assembly a = Assembly.Load("test");
            Assert.That(a, Is.Null);
        }
    }
}
