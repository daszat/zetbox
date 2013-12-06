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

namespace Zetbox.API.AbstractConsumerTests.ContextTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using NUnit.Framework;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Test;

    public abstract class when_changing_one_to_n
        : AbstractTestFixture
    {
        protected IZetboxContext ctx;
        protected One_to_N_relations_One one1;
        protected One_to_N_relations_One one2;
        protected One_to_N_relations_N n1;
        protected One_to_N_relations_N n2;

        public override void SetUp()
        {
            base.SetUp();

            ctx = GetContext();

            one1 = ctx.Create<One_to_N_relations_One>();
            one2 = ctx.Create<One_to_N_relations_One>();
            n1 = ctx.Create<One_to_N_relations_N>();
            n2 = ctx.Create<One_to_N_relations_N>();

            one1.Name = "One1";
            one2.Name = "One2";
            n1.Name = "N1";
            n2.Name = "N2";

            one1.NSide.Add(n1);
            one1.NSide.Add(n2);

            ctx.SubmitChanges();
        }

        public override void TearDown()
        {
            ctx = GetContext();
            ReloadObjects(ctx);
            ctx.Delete(one1);
            ctx.Delete(one2);
            ctx.Delete(n1);
            ctx.Delete(n2);
            ctx.SubmitChanges();

            base.TearDown();
        }

        private void ReloadObjects(IZetboxContext reloadCtx)
        {
            one1 = reloadCtx.Find<One_to_N_relations_One>(one1.ID);
            one2 = reloadCtx.Find<One_to_N_relations_One>(one2.ID);
            n1 = reloadCtx.Find<One_to_N_relations_N>(n1.ID);
            n2 = reloadCtx.Find<One_to_N_relations_N>(n2.ID);
        }

        [Test]
        public void should_change_parent()
        {
            Assert.That(n1.OneSide, Is.EqualTo(one1), "init");
            n1.OneSide = one2;
            Assert.That(n1.OneSide, Is.EqualTo(one2), "change");
            ctx.SubmitChanges();
            Assert.That(n1.OneSide, Is.EqualTo(one2), "submitted");
        }

        [Test]
        public void should_change_parent_and_roundtrip_with_new_context()
        {
            Assert.That(n1.OneSide, Is.EqualTo(one1), "init");
            n1.OneSide = one2;
            Assert.That(n1.OneSide, Is.EqualTo(one2), "change");
            ctx.SubmitChanges();

            ctx = GetContext();
            ReloadObjects(ctx);

            Assert.That(n1.OneSide, Is.EqualTo(one2), "reloaded");
        }

        public abstract class on_client
            : when_changing_one_to_n
        {
            [Test]
            public void should_change_parent_and_roundtrip_with_merge_context()
            {
                var localCtx = scope.Resolve<Func<ContextIsolationLevel, IZetboxContext>>().Invoke(ContextIsolationLevel.MergeQueryData);
                var localN1 = localCtx.Find<One_to_N_relations_N>(n1.ID);

                Assert.That(localN1.OneSide.ID, Is.EqualTo(one1.ID));
                n1.OneSide = one2;
                ctx.SubmitChanges();

                localN1 = localCtx.Find<One_to_N_relations_N>(n1.ID);
                Assert.That(localN1.OneSide.ID, Is.EqualTo(one1.ID), "No changes, as Find will look up the cached version from the context");
            }

            [Test]
            public void should_change_parent_and_roundtrip_with_merge_context_and_search()
            {
                var oneSideChanged = false;
                var localCtx = scope.Resolve<Func<ContextIsolationLevel, IZetboxContext>>().Invoke(ContextIsolationLevel.MergeQueryData);
                var localN1 = localCtx.Find<One_to_N_relations_N>(n1.ID);
                localN1.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == "OneSide")
                        oneSideChanged = true;
                };

                Assert.That(localN1.OneSide.ID, Is.EqualTo(one1.ID)); // Touch navigator

                // Make changes in other context
                n1.OneSide = one2;
                ctx.SubmitChanges();

                var refresh = localCtx.GetQuery<One_to_N_relations_N>().ToList();

                Assert.That(oneSideChanged, Is.True);
                Assert.That(refresh, Has.Member(localN1));
                Assert.That(refresh.Count, Is.GreaterThanOrEqualTo(2));
                Assert.That(localN1.OneSide.ID, Is.EqualTo(one2.ID));
            }
        }

        [Test]
        [Ignore("A ApplyChanges should clear the list cache?")]
        public void should_change_list_and_roundtrip_with_merge_context_and_search()
        {
            var localCtx = scope.Resolve<Func<ContextIsolationLevel, IZetboxContext>>().Invoke(ContextIsolationLevel.MergeQueryData);
            var localOne2 = localCtx.Find<One_to_N_relations_One>(one2.ID);
            Assert.That(localOne2.NSide, Is.Empty); // Touch navigator

            // Make changes in other context
            n1.OneSide = one2;
            ctx.SubmitChanges();

            var refresh = localCtx.GetQuery<One_to_N_relations_One>().ToList();

            Assert.That(refresh, Has.Member(localOne2));
            Assert.That(refresh.Count, Is.GreaterThanOrEqualTo(2));
            Assert.That(localOne2.NSide, Is.EquivalentTo(new[] { localCtx.Find<One_to_N_relations_N>(n1.ID) }));
        }
    }
}
