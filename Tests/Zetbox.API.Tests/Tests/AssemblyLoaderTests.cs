// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
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
