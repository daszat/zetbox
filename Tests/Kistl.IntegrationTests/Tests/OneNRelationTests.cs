
namespace Kistl.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.API.Tests;
    using Kistl.App.Base;
    using Kistl.App.GUI;

    using NUnit.Framework;

    [Ignore("takes way too long")]
    [TestFixture(0)]
    [TestFixture(1)]
    [TestFixture(10)]
    [TestFixture(50)]
    public sealed class BasicOneNRelationTests
        : BasicListTests<OneNRelationList<Property>, Property>
    {
        private static readonly Guid TestObjClassGuid = new Guid("19F38F05-E88E-44C6-BFDF-D502B3632028");
        private Guid _fixtureGuid;
        private Guid _moduleGuid;
        private Guid _valueDescGuid;
        private Guid _otherClassGuid;

        private ObjectClass _parent;
        private bool _hasCollectionChanged;
        private bool _hasParentChanged;

        public BasicOneNRelationTests(int items)
            : base(items) { }

        private void SetupFixtureObject()
        {
            using (var initCtx = GetContext())
            {
                _moduleGuid = initCtx.GetQuery<Module>().Single(m => m.Name == "KistlBase").ExportGuid;
                _valueDescGuid = initCtx.GetQuery<ViewModelDescriptor>().First().ExportGuid;

                var fixtureOC = initCtx.GetQuery<ObjectClass>().FirstOrDefault(oc => oc.Properties.Count == items)
                    ?? initCtx.GetQuery<ObjectClass>().FirstOrDefault(oc => oc.Properties.Count > items)
                    ?? initCtx.FindPersistenceObject<ObjectClass>(TestObjClassGuid);

                _fixtureGuid = fixtureOC.ExportGuid;
                var otherClass = initCtx.GetQuery<ObjectClass>().First(oc => oc.ExportGuid != _fixtureGuid);
                _otherClassGuid = otherClass.ExportGuid;

                while (fixtureOC.Properties.Count < items)
                {
                    fixtureOC.Properties.Add(CreateProperty(initCtx, NewItemNumber()));
                }

                while (fixtureOC.Properties.Count > items)
                {
                    fixtureOC.Properties[fixtureOC.Properties.Count - 1].ObjectClass = otherClass;
                }

                Assert.That(fixtureOC.Properties.Count, Is.EqualTo(items));
                initCtx.SubmitChanges();
            }
        }

        protected override void PreSetup()
        {
            SetupFixtureObject();

            _parent = ctx.FindPersistenceObject<ObjectClass>(_fixtureGuid);
            Assert.That(_parent.Properties.Count, Is.EqualTo(items));

            base.PreSetup();
        }

        protected override void PostTeardown()
        {
            base.PostTeardown();
            if (ctx != null)
            {
                ctx.Dispose();
                ctx = null;
            }
        }

        protected override Property NewItem()
        {
            return CreateProperty(ctx, NewItemNumber());
        }

        private Property CreateProperty(IKistlContext ctx, int unique)
        {
            var result = ctx.Create<IntProperty>();
            result.Module = ctx.FindPersistenceObject<Module>(_moduleGuid);
            result.Name = "property" + unique;
            result.ValueModelDescriptor = ctx.FindPersistenceObject<ViewModelDescriptor>(_valueDescGuid);
            return result;
        }

        protected override OneNRelationList<Property> CreateCollection(List<Property> items)
        {
            var result = (OneNRelationList<Property>)_parent.Properties;

            _hasCollectionChanged = false;
            result.CollectionChanged += (sender, args) => { _hasCollectionChanged = true; };

            _hasParentChanged = false;
            _parent.PropertyChanged += (sender, args) => { if (args.PropertyName == "Properties") { _hasParentChanged = true; } };

            // nothing should have changed yet!
            Assert.That(ctx.AttachedObjects.Select(p => p.ObjectState).Distinct().ToArray(), Is.All.EqualTo(DataObjectState.Unmodified));

            return result;
        }

        protected override List<Property> InitialItems()
        {
            return _parent.Properties.ToList();
        }

        protected override void AssertCollectionIsChanged()
        {
            base.AssertCollectionIsChanged();
            Assert.That(_hasCollectionChanged, "Collection was not notified");
            _hasCollectionChanged = false;

            Assert.That(_hasParentChanged, "Parent was not notified");
            _hasParentChanged = false;

            Assert.That(_parent.ObjectState, Is.EqualTo(DataObjectState.Modified).Or.EqualTo(DataObjectState.New));
            Assert.That(ctx.AttachedObjects.OfType<Property>().Select(p => p.ObjectState).Distinct().ToArray(), Is.Empty.Or.Member(DataObjectState.Modified).Or.Member(DataObjectState.New));
        }

        protected override void AssertCollectionIsUnchanged()
        {
            base.AssertCollectionIsUnchanged();
            Assert.That(!_hasCollectionChanged, "Collection was notified falsely");
            _hasCollectionChanged = false;

            Assert.That(!_hasParentChanged, "Parent was notified falsely");
            _hasParentChanged = false;
        }

        protected override void AssertInvariants(List<Property> expectedItems)
        {
            base.AssertInvariants(expectedItems);

            // we also expect the collection to be stable, i.e. not change the order of the items
            Assert.That(collection, Is.EquivalentTo(expectedItems));

            foreach (var expected in expectedItems)
            {
                //Assert.That(expected.OneSide, Is.SameAs(obj));
                Assert.That(expected.GetPrivateFieldValue<int?>("_fk_ObjectClass"), Is.EqualTo(_parent.ID));
                Assert.That(expected.GetPrivateFieldValue<int?>("_Properties_pos"), Is.Not.Null);
            }

            Assert.That(collection.Select(p => p.GetPrivateFieldValue<int?>("_Properties_pos")).ToArray(), Is.Ordered);

            ////////////////////// test roundtripping //////////////////////////////////

            var otherClass = ctx.FindPersistenceObject<ObjectClass>(_otherClassGuid);
            // first shunt unattached properties to another object class to get a valid data model
            foreach (var prop in ctx.AttachedObjects.OfType<Property>().Where(p => p.ObjectClass == null))
            {
                prop.ObjectClass = otherClass;
            }

            // then, save the stuff to the database
            ctx.SubmitChanges();

            // finally, check remaining properties for them being properly roundtripped
            var propertyNames = collection.Select(p => p.Name).ToArray();
            using (var checkCtx = GetContext())
            {
                var checkParent = checkCtx.FindPersistenceObject<ObjectClass>(_fixtureGuid);
                Assert.That(checkParent.Properties.Select(p => p.Name).ToArray(), Is.EquivalentTo(propertyNames));
            }
        }
    }

    [Ignore("takes way too long")]
    [TestFixture(0)]
    [TestFixture(1)]
    [TestFixture(10)]
    [TestFixture(50)]
    public sealed class GenericOneNRelationTests
        : GenericListTests<OneNRelationList<Property>, Property>
    {
        private static readonly Guid TestObjClassGuid = new Guid("19F38F05-E88E-44C6-BFDF-D502B3632028");
        private Guid _fixtureGuid;
        private Guid _moduleGuid;
        private Guid _valueDescGuid;
        private Guid _otherClassGuid;

        private ObjectClass _parent;
        private bool _hasCollectionChanged;
        private bool _hasParentChanged;

        public GenericOneNRelationTests(int items)
            : base(items) { }

        private void SetupFixtureObject()
        {
            using (var initCtx = GetContext())
            {
                _moduleGuid = initCtx.GetQuery<Module>().Single(m => m.Name == "KistlBase").ExportGuid;
                _valueDescGuid = initCtx.GetQuery<ViewModelDescriptor>().First().ExportGuid;

                var fixtureOC = initCtx.GetQuery<ObjectClass>().FirstOrDefault(oc => oc.Properties.Count == items);
                if (fixtureOC == null)
                {
                    fixtureOC = initCtx.FindPersistenceObject<ObjectClass>(TestObjClassGuid);
                }

                while (fixtureOC.Properties.Count < items)
                {
                    fixtureOC.Properties.Add(CreateProperty(initCtx, NewItemNumber()));
                }

                while (fixtureOC.Properties.Count > items)
                {
                    fixtureOC.Properties.RemoveAt(fixtureOC.Properties.Count - 1);
                }

                Assert.That(fixtureOC.Properties.Count, Is.EqualTo(items));
                initCtx.SubmitChanges();

                _fixtureGuid = fixtureOC.ExportGuid;
                _otherClassGuid = initCtx.GetQuery<ObjectClass>().First(oc => oc.ExportGuid != _fixtureGuid).ExportGuid;
            }
        }

        protected override void PreSetup()
        {
            SetupFixtureObject();

            _parent = ctx.FindPersistenceObject<ObjectClass>(_fixtureGuid);
            Assert.That(_parent.Properties.Count, Is.EqualTo(items));

            base.PreSetup();
        }

        protected override void PostTeardown()
        {
            base.PostTeardown();
            if (ctx != null)
            {
                ctx.Dispose();
                ctx = null;
            }
        }

        protected override Property NewItem()
        {
            return CreateProperty(ctx, NewItemNumber());
        }

        private Property CreateProperty(IKistlContext ctx, int unique)
        {
            var result = ctx.Create<IntProperty>();
            result.Module = ctx.FindPersistenceObject<Module>(_moduleGuid);
            result.Name = "property" + unique;
            result.ValueModelDescriptor = ctx.FindPersistenceObject<ViewModelDescriptor>(_valueDescGuid);
            return result;
        }

        protected override OneNRelationList<Property> CreateCollection(List<Property> items)
        {
            var result = (OneNRelationList<Property>)_parent.Properties;

            _hasCollectionChanged = false;
            result.CollectionChanged += (sender, args) => { _hasCollectionChanged = true; };

            _hasParentChanged = false;
            _parent.PropertyChanged += (sender, args) => { if (args.PropertyName == "Properties") { _hasParentChanged = true; } };

            // nothing should have changed yet!
            Assert.That(ctx.AttachedObjects.Select(p => p.ObjectState).Distinct().ToArray(), Is.All.EqualTo(DataObjectState.Unmodified));

            return result;
        }

        protected override List<Property> InitialItems()
        {
            return _parent.Properties.ToList();
        }

        protected override void AssertCollectionIsChanged()
        {
            base.AssertCollectionIsChanged();
            Assert.That(_hasCollectionChanged, "Collection was not notified");
            _hasCollectionChanged = false;

            Assert.That(_hasParentChanged, "Parent was not notified");
            _hasParentChanged = false;

            Assert.That(_parent.ObjectState, Is.EqualTo(DataObjectState.Modified).Or.EqualTo(DataObjectState.New));
            Assert.That(ctx.AttachedObjects.OfType<Property>().Select(p => p.ObjectState).Distinct().ToArray(), Is.Empty.Or.Member(DataObjectState.Modified).Or.Member(DataObjectState.New));
        }

        protected override void AssertCollectionIsUnchanged()
        {
            base.AssertCollectionIsUnchanged();
            Assert.That(!_hasCollectionChanged, "Collection was notified falsely");
            _hasCollectionChanged = false;

            Assert.That(!_hasParentChanged, "Parent was notified falsely");
            _hasParentChanged = false;
        }

        protected override void AssertInvariants(List<Property> expectedItems)
        {
            base.AssertInvariants(expectedItems);

            // we also expect the collection to be stable, i.e. not change the order of the items
            Assert.That(collection, Is.EquivalentTo(expectedItems));

            foreach (var expected in expectedItems)
            {
                //Assert.That(expected.OneSide, Is.SameAs(obj));
                Assert.That(expected.GetPrivateFieldValue<int?>("_fk_ObjectClass"), Is.EqualTo(_parent.ID));
                Assert.That(expected.GetPrivateFieldValue<int?>("_Properties_pos"), Is.Not.Null);
            }

            Assert.That(collection.Select(p => p.GetPrivateFieldValue<int?>("_Properties_pos")).ToArray(), Is.Ordered);

            ////////////////////// test roundtripping //////////////////////////////////

            var otherClass = ctx.FindPersistenceObject<ObjectClass>(_otherClassGuid);
            // first shunt unattached properties to another object class to get a valid data model
            foreach (var prop in ctx.AttachedObjects.OfType<Property>().Where(p => p.ObjectClass == null))
            {
                prop.ObjectClass = otherClass;
            }

            // then, save the stuff to the database
            ctx.SubmitChanges();

            // finally, check remaining properties for them being properly roundtripped
            var propertyNames = collection.Select(p => p.Name).ToArray();
            using (var checkCtx = GetContext())
            {
                var checkParent = checkCtx.FindPersistenceObject<ObjectClass>(_fixtureGuid);
                Assert.That(checkParent.Properties.Select(p => p.Name).ToArray(), Is.EquivalentTo(propertyNames));
            }
        }
    }
}