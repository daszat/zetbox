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

namespace Zetbox.IntegrationTests.ObjectListModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ValueViewModels;
    using NUnit.Framework;

    public class when_moving_items : AbstractIntegrationTestFixture
    {

        [Test]
        [Ignore("Test fails")]
        public void should_persist_changes()
        {
            Property[] propList;

            using (var ctx = GetContext())
            {
                var objectClass = ctx.GetQuery<ObjectClass>().Single(oc => oc.Name == "ObjectClass");

                Console.WriteLine("========================");
                foreach (var p in objectClass.Properties)
                {
                    Console.WriteLine("ID=[{0}], name=[{1}], pos=[{2}]", p.ID, p.Name, p.GetPrivateFieldValue<int>("_Properties_pos"));
                }

                Assert.That(objectClass.Properties.Select(p => p.GetPrivateFieldValue<int>("_Properties_pos")), Is.Ordered);
                var factory = scope.Resolve<IViewModelFactory>();
                var classModel = DataObjectViewModel.Fetch(factory, ctx, null, objectClass);
                var listModel = (ObjectListViewModel)classModel.PropertyModelsByName["Properties"];
                propList = objectClass.Properties.ToArray();
                var mdlList = listModel.Value.ToArray();
                listModel.MoveItemUp(listModel.Value[3]);

                var tmpMdl = mdlList[2];
                mdlList[2] = mdlList[3];
                mdlList[3] = tmpMdl;
                var tmpProp = propList[2];
                propList[2] = propList[3];
                propList[3] = tmpProp;

                Assert.That(listModel.Value, Is.EquivalentTo(mdlList));
                Assert.That(objectClass.Properties, Is.EquivalentTo(propList));
                Assert.That(objectClass.Properties.Select(p => p.GetPrivateFieldValue<int>("_Properties_pos")).ToArray(), Is.Ordered);

                // at least one of the properties has to change
                Assert.That(new[] { propList[2].ObjectState, propList[3].ObjectState }.Count(dos => dos == DataObjectState.Modified), Is.GreaterThanOrEqualTo(1));
                // the parent object should be marked as modified too
                Assert.That(objectClass.ObjectState, Is.EqualTo(DataObjectState.Modified));

                Console.WriteLine("========================");
                foreach (var p in objectClass.Properties)
                {
                    Console.WriteLine("ID=[{0}], name=[{1}], pos=[{2}]", p.ID, p.Name, p.GetPrivateFieldValue<int>("_Properties_pos"));
                }

                // at least one object has changed
                Assert.That(ctx.SubmitChanges(), Is.GreaterThanOrEqualTo(1));

                Console.WriteLine("========================");
                foreach (var p in objectClass.Properties)
                {
                    Console.WriteLine("ID=[{0}], name=[{1}], pos=[{2}]", p.ID, p.Name, p.GetPrivateFieldValue<int>("_Properties_pos"));
                }

                CheckPropertyList(propList);
            }
        }

        private void CheckPropertyList(Property[] propList)
        {
            using (var checkCtx = GetContext())
            {
                var checkObjectClass = checkCtx.GetQuery<ObjectClass>().Single(oc => oc.Name == "ObjectClass");
                // compare by Name, since we got new instances from the checkCtx
                Assert.That(checkObjectClass.Properties.Select(p => p.Name).ToArray(), Is.EquivalentTo(propList.Select(p => p.Name).ToArray()));
                Assert.That(checkObjectClass.Properties.Select(p => p.GetPrivateFieldValue<int>("_Properties_pos")).ToArray(), Is.Ordered);
                Console.WriteLine("========================");
                foreach (var p in checkObjectClass.Properties)
                {
                    Console.WriteLine("ID=[{0}], name=[{1}], pos=[{2}]", p.ID, p.Name, p.GetPrivateFieldValue<int>("_Properties_pos"));
                }
            }
        }
    }
}
