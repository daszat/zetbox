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

namespace Zetbox.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Tests;
    using Zetbox.App.Test;
    using Zetbox.DalProvider.Base.RelationWrappers;

    [TestFixture(0)]
    [TestFixture(1)]
    [TestFixture(5)]
    public sealed class BasicOneNRelationTests
        : BasicListTests<OneNRelationList<One_to_N_relations_OrderedN>, One_to_N_relations_OrderedN>
    {
        private int _parentId;
        private One_to_N_relations_One _parent;
        private bool _hasCollectionChanged;
        private bool _hasParentChanged;

        public BasicOneNRelationTests(int items)
            : base(items) { }

        private void SetupFixtureObject()
        {
            using (var initCtx = GetContext())
            {
                var fixtureOC = initCtx.Create<One_to_N_relations_One>();
                for (int i = 0; i < items; i++)
                {
                    fixtureOC.OrderedNSide.Add(CreateNSide(initCtx, NewItemNumber()));
                }

                Assert.That(fixtureOC.OrderedNSide.Count, Is.EqualTo(items));
                initCtx.SubmitChanges();

                _parentId = fixtureOC.ID;
            }
        }

        protected override void PreSetup()
        {
            SetupFixtureObject();

            _parent = ctx.Find<One_to_N_relations_One>(_parentId);
            Assert.That(_parent.OrderedNSide.Count, Is.EqualTo(items));

            base.PreSetup();
        }

        protected override void PostTeardown()
        {
            if (_parent != null)
            {
                foreach (var n in _parent.OrderedNSide)
                {
                    ctx.Delete(n);
                }
                ctx.Delete(_parent);
            }
            base.PostTeardown();
        }

        protected override One_to_N_relations_OrderedN NewItem()
        {
            return CreateNSide(ctx, NewItemNumber());
        }

        private One_to_N_relations_OrderedN CreateNSide(IZetboxContext ctx, int unique)
        {
            var result = ctx.Create<One_to_N_relations_OrderedN>();
            result.Name = "ONRT" + unique;
            return result;
        }

        protected override OneNRelationList<One_to_N_relations_OrderedN> CreateCollection(List<One_to_N_relations_OrderedN> items)
        {
            var result = (OneNRelationList<One_to_N_relations_OrderedN>)_parent.OrderedNSide;

            _hasCollectionChanged = false;
            result.CollectionChanged += (sender, args) => { _hasCollectionChanged = true; };

            _hasParentChanged = false;
            _parent.PropertyChanged += (sender, args) => { if (args.PropertyName == "OrderedNSide") { _hasParentChanged = true; } };

            // nothing should have changed yet!
            Assert.That(ctx.AttachedObjects.Select(p => p.ObjectState).Distinct().ToArray(), Is.All.EqualTo(DataObjectState.Unmodified));

            return result;
        }

        protected override List<One_to_N_relations_OrderedN> InitialItems()
        {
            return _parent.OrderedNSide.ToList();
        }

        protected override void AssertCollectionIsChanged()
        {
            base.AssertCollectionIsChanged();
            Assert.That(_hasCollectionChanged, "Collection was not notified");
            _hasCollectionChanged = false;

            Assert.That(_hasParentChanged, "Parent was not notified");
            _hasParentChanged = false;

            Assert.That(_parent.ObjectState, Is.EqualTo(DataObjectState.Unmodified).Or.EqualTo(DataObjectState.New));
            Assert.That(ctx.AttachedObjects.OfType<One_to_N_relations_OrderedN>().Select(p => p.ObjectState).Distinct().ToArray(), Is.Empty.Or.Member(DataObjectState.Modified).Or.Member(DataObjectState.New));
        }

        protected override void AssertCollectionIsUnchanged()
        {
            base.AssertCollectionIsUnchanged();
            Assert.That(!_hasCollectionChanged, "Collection was notified falsely");
            _hasCollectionChanged = false;

            Assert.That(!_hasParentChanged, "Parent was notified falsely");
            _hasParentChanged = false;
        }

        protected override void AssertInvariants(List<One_to_N_relations_OrderedN> expectedItems)
        {
            base.AssertInvariants(expectedItems);

            // we also expect the collection to be stable, i.e. not change the order of the items
            Assert.That(collection, Is.EquivalentTo(expectedItems));

            foreach (var expected in expectedItems)
            {
                //Assert.That(expected.OneSide, Is.SameAs(obj));
                Assert.That(expected.GetPrivateFieldValue<int?>("__fk_OneSideCache"), Is.EqualTo(_parent.ID), "__fk_OneSideCache");
                Assert.That(expected.GetPrivateFieldValue<int?>("_OrderedNSide_pos"), Is.Not.Null, "_OrderedNSide_pos");
            }

            Assert.That(collection.Select(p => p.GetPrivateFieldValue<int?>("_OrderedNSide_pos")).ToArray(), Is.Ordered, "_OrderedNSide_pos Is.Ordered");

            ////////////////////// test roundtripping //////////////////////////////////

            // save the stuff to the database
            ctx.SubmitChanges();

            // finally, check remaining properties for them being properly roundtripped
            var nsideNames = collection.Select(p => p.Name).ToArray();
            using (var checkCtx = GetContext())
            {
                var checkParent = checkCtx.FindPersistenceObject<One_to_N_relations_One>(_parentId);
                Assert.That(checkParent.OrderedNSide.Select(p => p.Name).ToArray(), Is.EquivalentTo(nsideNames));
            }
        }
    }

    [TestFixture(0)]
    [TestFixture(1)]
    [TestFixture(5)]
    public sealed class GenericOneNRelationTests
        : GenericListTests<OneNRelationList<One_to_N_relations_OrderedN>, One_to_N_relations_OrderedN>
    {
        private int _parentId;
        private One_to_N_relations_One _parent;
        private bool _hasCollectionChanged;
        private bool _hasParentChanged;

        public GenericOneNRelationTests(int items)
            : base(items) { }

        private void SetupFixtureObject()
        {
            using (var initCtx = GetContext())
            {
                var fixtureOC = initCtx.Create<One_to_N_relations_One>();
                for (int i = 0; i < items; i++)
                {
                    fixtureOC.OrderedNSide.Add(CreateNSide(initCtx, NewItemNumber()));
                }

                Assert.That(fixtureOC.OrderedNSide.Count, Is.EqualTo(items));
                initCtx.SubmitChanges();

                _parentId = fixtureOC.ID;
            }
        }

        protected override void PreSetup()
        {
            SetupFixtureObject();

            _parent = ctx.Find<One_to_N_relations_One>(_parentId);
            Assert.That(_parent.OrderedNSide.Count, Is.EqualTo(items));

            base.PreSetup();
        }

        protected override void PostTeardown()
        {
            if (_parent != null)
            {
                foreach (var n in _parent.OrderedNSide)
                {
                    ctx.Delete(n);
                }
                ctx.Delete(_parent);
            }
            base.PostTeardown();
        }

        protected override One_to_N_relations_OrderedN NewItem()
        {
            return CreateNSide(ctx, NewItemNumber());
        }

        private One_to_N_relations_OrderedN CreateNSide(IZetboxContext ctx, int unique)
        {
            var result = ctx.Create<One_to_N_relations_OrderedN>();
            result.Name = "GONRT" + unique;
            return result;
        }

        protected override OneNRelationList<One_to_N_relations_OrderedN> CreateCollection(List<One_to_N_relations_OrderedN> items)
        {
            var result = (OneNRelationList<One_to_N_relations_OrderedN>)_parent.OrderedNSide;

            _hasCollectionChanged = false;
            result.CollectionChanged += (sender, args) => { _hasCollectionChanged = true; };

            _hasParentChanged = false;
            _parent.PropertyChanged += (sender, args) => { if (args.PropertyName == "OrderedNSide") { _hasParentChanged = true; } };

            // nothing should have changed yet!
            Assert.That(ctx.AttachedObjects.Select(p => p.ObjectState).Distinct().ToArray(), Is.All.EqualTo(DataObjectState.Unmodified));

            return result;
        }

        protected override List<One_to_N_relations_OrderedN> InitialItems()
        {
            return _parent.OrderedNSide.ToList();
        }

        protected override void AssertCollectionIsChanged()
        {
            base.AssertCollectionIsChanged();
            Assert.That(_hasCollectionChanged, "Collection was not notified");
            _hasCollectionChanged = false;

            Assert.That(_hasParentChanged, "Parent was not notified");
            _hasParentChanged = false;

            Assert.That(_parent.ObjectState, Is.EqualTo(DataObjectState.Unmodified).Or.EqualTo(DataObjectState.New));
            Assert.That(ctx.AttachedObjects.OfType<One_to_N_relations_OrderedN>().Select(p => p.ObjectState).Distinct().ToArray(), Is.Empty.Or.Member(DataObjectState.Modified).Or.Member(DataObjectState.New));
        }

        protected override void AssertCollectionIsUnchanged()
        {
            base.AssertCollectionIsUnchanged();
            Assert.That(!_hasCollectionChanged, "Collection was notified falsely");
            _hasCollectionChanged = false;

            Assert.That(!_hasParentChanged, "Parent was notified falsely");
            _hasParentChanged = false;
        }

        protected override void AssertInvariants(List<One_to_N_relations_OrderedN> expectedItems)
        {
            base.AssertInvariants(expectedItems);

            // we also expect the collection to be stable, i.e. not change the order of the items
            Assert.That(collection, Is.EquivalentTo(expectedItems));

            foreach (var expected in expectedItems)
            {
                //Assert.That(expected.OneSide, Is.SameAs(obj));
                Assert.That(expected.GetPrivateFieldValue<int?>("__fk_OneSideCache"), Is.EqualTo(_parent.ID), "__fk_OneSideCache");
                Assert.That(expected.GetPrivateFieldValue<int?>("_OrderedNSide_pos"), Is.Not.Null, "_OrderedNSide_pos");
            }

            Assert.That(collection.Select(p => p.GetPrivateFieldValue<int?>("_OrderedNSide_pos")).ToArray(), Is.Ordered, "_OrderedNSide_pos Is.Ordered");

            ////////////////////// test roundtripping //////////////////////////////////

            // save the stuff to the database
            ctx.SubmitChanges();

            // finally, check remaining properties for them being properly roundtripped
            var nsideNames = collection.Select(p => p.Name).ToArray();
            using (var checkCtx = GetContext())
            {
                var checkParent = checkCtx.FindPersistenceObject<One_to_N_relations_One>(_parentId);
                Assert.That(checkParent.OrderedNSide.Select(p => p.Name).ToArray(), Is.EquivalentTo(nsideNames));
            }
        }
    }
}