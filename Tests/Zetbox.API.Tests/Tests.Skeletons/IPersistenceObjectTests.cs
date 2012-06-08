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
using System.Text;

using NUnit.Framework;

namespace Zetbox.API.Tests.Skeletons
{
    public abstract class IPersistenceObjectTests<T>
        : IStreamableTests<T>
        where T : IPersistenceObject, new()
    {
        protected IZetboxContext ctx;

        public override void SetUp()
        {
            base.SetUp();
            ctx = GetContext();
            ctx.Attach(obj);

        }
        public override void TearDown()
        {
            if (ctx != null)
            {
                ctx.Dispose();
                ctx = null;
            }
            base.TearDown();
        }

        [Test]
        public void should_roundtrip_IPersistenceObject_correctly()
        {
            T result = SerializationRoundtrip(obj);

            Assert.That(result.ID, Is.EqualTo(obj.ID));
        }
    }

}
