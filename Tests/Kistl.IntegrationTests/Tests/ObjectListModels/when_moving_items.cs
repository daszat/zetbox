
namespace Kistl.IntegrationTests.ObjectListModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.App.Base;
    using Kistl.Client.Presentables;
    using Kistl.Client.WPF;

    using NUnit.Framework;
    using Kistl.Client;

    using Autofac;
    using Kistl.Client.Presentables.ValueViewModels;

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
                var classModel = DataObjectViewModel.Fetch(factory, ctx, objectClass);
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
