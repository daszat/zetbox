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

using Zetbox.API;
using Zetbox.API.Client;
using Zetbox.App.Extensions;

using NUnit.Framework;

namespace Zetbox.IntegrationTests.TypeRefs
{
    [TestFixture]
    public class should_convert_types_correctly : AbstractIntegrationTestFixture
    {
        // These are important TypeRefs for the GUI which must exist
        // using int for "CompoundObject" types, string for "class" types
        [Datapoints]
        public static Type[] TestTypes = new[] { typeof(int), typeof(int?), typeof(string), typeof(ICollection<int>), typeof(ICollection<int?>), typeof(ICollection<string>) };

        [Theory]
        public void when_calling_ToRef_on_a_Type(Type systemType)
        {
            using (var ctx = GetContext())
            {
                var tr = systemType.ToRef(ctx);
                Assert.That(tr, Is.Not.Null);
                Assert.That(tr.AsType(true), Is.EqualTo(systemType));
            }
        }
    }
}
