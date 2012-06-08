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

namespace Zetbox.API.AbstractConsumerTests.one_to_N_relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;

    using NUnit.Framework;

    public abstract class should_obey_order : AbstractTestFixture
    {
        private static List<BaseParameter> CanonicalOrdering(IEnumerable<BaseParameter> input)
        {
            return input.OrderBy(p => p.IsReturnParameter)
                    .ThenBy(p => p.Name).ToList();
        }

        [Test]
        public void when_ordering_items()
        {
            int methodID = Helper.INVALIDID;

            using (IZetboxContext ctx = GetContext())
            {
                var method = ctx.GetQuery<Method>().ToList().Where(m => m.Module.Name == "Projekte")
                    .OrderByDescending(m => m.Parameter.Count).First();

                Assert.That(method.Parameter.Count, Is.GreaterThan(1), "Test data failure: needs more than one Parameter to test ordering");
                Assert.That(method.ObjectState, Is.EqualTo(DataObjectState.Unmodified), "Test data failure: method should be unmodified at start of test");

                methodID = method.ID;

                var parameters = method.Parameter.ToList();
                method.Parameter.Clear();

                Assert.That(method.ObjectState, Is.EqualTo(DataObjectState.Modified));

                foreach (BaseParameter p in CanonicalOrdering(parameters))
                {
                    method.Parameter.Add(p);
                }

                Assert.That(ctx.SubmitChanges(), Is.GreaterThan(0));
            }

            using (IZetboxContext ctx = GetContext())
            {
                var method = ctx.Find<Method>(methodID);

                var parameters = method.Parameter.ToList();
                Assert.That(parameters, Is.EquivalentTo(CanonicalOrdering(parameters)), "mismatch in retrieved parameters");
            }
        }
    }
}
