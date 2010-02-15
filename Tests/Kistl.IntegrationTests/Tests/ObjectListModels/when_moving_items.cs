
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

    public class when_moving_items
    {
        private IKistlContext ctx;

        [SetUp]
        public void SetUp()
        {
            ctx = KistlContext.GetContext();
        }

        [TearDown]
        public void TearDown()
        {
            if (ctx != null)
            {
                ctx.Dispose();
                ctx = null;
            }
        }

        [Test]
        public void should_persist_changes()
        {
            var objectClass = ctx.GetQuery<ObjectClass>().Single(oc => oc.ClassName == "ObjectClass");
            Assert.That(objectClass.Properties.Select(p => p.GetPrivateFieldValue<int>("_ObjectClass_pos")), Is.Ordered);
            var factory = new WpfModelFactory(GuiApplicationContext.Current);
            var classModel = (DataObjectModel)factory.CreateDefaultModel(ctx, objectClass);
            var listModel = (ObjectListModel)classModel.PropertyModelsByName["Properties"];
            var origList = listModel.Value.ToList();
            listModel.MoveItemUp(listModel.Value[3]);
            //Assert.That(listModel.Value[2], Is.SameAs(origList[3]));
            //Assert.That(listModel.Value[3], Is.SameAs(origList[2]));
            var tmp = origList[2];
            origList[2] = origList[3];
            origList[3] = tmp;

            Assert.That(listModel.Value, Is.EquivalentTo(origList));
            Assert.That(objectClass.Properties.Select(p => p.GetPrivateFieldValue<int>("_ObjectClass_pos")), Is.Ordered);

            ctx.SubmitChanges();

            using (var checkCtx = KistlContext.GetContext())
            {
                var checkObjectClass = ctx.GetQuery<ObjectClass>().Single(oc => oc.ClassName == "ObjectClass");
                Assert.That(checkObjectClass.Properties.Select(p => p.ID), Is.EquivalentTo(origList.Select(dom => dom.Object.ID)));
            }
        }
    }
}
