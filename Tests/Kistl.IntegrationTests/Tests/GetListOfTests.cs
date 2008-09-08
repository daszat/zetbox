using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.API;
using Kistl.Client;
using Kistl.API.Client;

namespace Kistl.IntegrationTests
{
    [TestFixture]
    public class GetListOfTests
    {
        [SetUp]
        public void SetUp()
        {
            //CacheController<Kistl.API.IDataObject>.Current.Clear();
        }

        [Test]
        public void GetListOf()
        {
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var prj = ctx.GetQuery<Kistl.App.Projekte.Projekt>().Where(o => o.Name == "Kistl").Single();
                var list = prj.Tasks;
                Assert.That(list.Count, Is.GreaterThan(0));
            }
        }

        [Test]
        public void GetListOf_Twice()
        {
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var prj1 = ctx.GetQuery<Kistl.App.Projekte.Projekt>().Where(o => o.Name == "Kistl").Single();
                var list1 = prj1.Tasks;
                Assert.That(list1.Count, Is.GreaterThan(0));

                var prj2 = ctx.GetQuery<Kistl.App.Projekte.Projekt>().Where(o => o.Name == "Kistl").Single();
                var list2 = prj2.Tasks;

                Assert.That(list2.Count, Is.GreaterThan(0));
                Assert.That(list2.Count, Is.EqualTo(list1.Count));
            }
        }

        [Test]
        public void GetObject_GetListOf()
        {
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var task = ctx.GetQuery<Kistl.App.Projekte.Task>().Where(t => t.Projekt.Name == "Kistl").First();
                Assert.That(task, Is.Not.Null);
                Assert.That(task.Context, Is.EqualTo(ctx));

                var prj = ctx.GetQuery<Kistl.App.Projekte.Projekt>().Where(p => p.Name == "Kistl").Single();
                var task_test = prj.Tasks.Single(t => t.ID == task.ID);

                Assert.That(object.ReferenceEquals(task, task_test), "task & task_test are different Objects");
            }
        }

        [Test]
        public void GetListOf_GetObject()
        {
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var prj = ctx.GetQuery<Kistl.App.Projekte.Projekt>().Where(p => p.Name == "Kistl").Single();
                var task = prj.Tasks.First();
                Assert.That(task, Is.Not.Null);

                var task_test = ctx.Find<Kistl.App.Projekte.Task>(task.ID);

                Assert.That(object.ReferenceEquals(task, task_test), "task & task_test are different Objects");
            }
        }
    }
}
