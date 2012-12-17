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

namespace Zetbox.IntegrationTests.Presentables.Properties
{
    using System;
    using System.Linq;
    using Autofac;
    using NUnit.Framework;
    using Zetbox.API;
    using Zetbox.App.Extensions;
    using Zetbox.App.Test;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ValueViewModels;

    [TestFixture]
    public class should_have_models : AbstractIntegrationTestFixture
    {
        public override void SetUp()
        {
            base.SetUp();
            using (var setupCtx = GetContext())
            {
                if (setupCtx.GetQuery<Muhblah>().FirstOrDefault() == null)
                {
                    var testObj = setupCtx.Create<Muhblah>();
                    setupCtx.SubmitChanges();
                }
            }
        }

        [Test]
        public void string_property()
        {
            var frozenCtx = scope.Resolve<IFrozenContext>();
            var stringProperty = Zetbox.NamedObjects.Base.Classes.Zetbox.App.Test.Muhblah_Properties.TestString.Find(frozenCtx);

            using (var ctx = GetContext())
            {
                var obj = ctx.GetQuery<Muhblah>().First();
                obj.TestString = "some test";
                var valueViewModel = BaseValueViewModel.Fetch(scope.Resolve<IViewModelFactory>(), ctx, null, stringProperty, stringProperty.GetPropertyValueModel(obj));

                Assert.That(valueViewModel, Is.Not.Null);
                Assert.That(valueViewModel.ValueModel, Is.Not.Null);
                Assert.That(valueViewModel.UntypedValue, Is.EqualTo(obj.TestString), "Value after initialisation");

                obj.TestString = "a new value";

                Assert.That(valueViewModel.UntypedValue, Is.EqualTo(obj.TestString), "Value after value change");
            }
        }

        [Test]
        [Ignore("not implemented")]
        public void stringCollection_property()
        {
            var frozenCtx = scope.Resolve<IFrozenContext>();
            var stringCollectionProperty = Zetbox.NamedObjects.Base.Classes.Zetbox.App.Test.Muhblah_Properties.StringCollection.Find(frozenCtx);

            using (var ctx = GetContext())
            {
                var obj = ctx.GetQuery<Muhblah>().First();
                obj.StringCollection.Add("some test");
                var valueViewModel = BaseValueViewModel.Fetch(scope.Resolve<IViewModelFactory>(), ctx, null, stringCollectionProperty, stringCollectionProperty.GetPropertyValueModel(obj));

                Assert.That(valueViewModel, Is.Not.Null);
                Assert.That(valueViewModel.ValueModel, Is.Not.Null);
                Assert.That(valueViewModel.UntypedValue, Is.EquivalentTo(obj.StringCollection), "Value after initialisation");

                obj.StringCollection.Add("a second value");

                Assert.That(valueViewModel.UntypedValue, Is.EquivalentTo(obj.StringCollection), "Value after value change");
            }
        }
    }
}
