using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Base;
using Kistl.Client;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Kistl.IntegrationTests
{
    [TestFixture]
    public class GetListTests : AbstractIntegrationTestFixture
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Tests.Integration.GetList");


        [Test]
        public void GetList()
        {
            using (IKistlContext ctx = GetContext())
            {
                var list = ctx.GetQuery<ObjectClass>().ToList();
                Assert.That(list.Count, Is.GreaterThan(0));
            }
        }

        [Test]
        public void GetList_Twice()
        {
            using (IKistlContext ctx = GetContext())
            {
                List<ObjectClass> list1 = ctx.GetQuery<ObjectClass>().ToList();
                Assert.That(list1, Is.Not.Null);
                Assert.That(list1.Count, Is.AtLeast(2));
                list1.ForEach(obj => Assert.That(obj, Is.Not.Null));

                List<ObjectClass> list2 = ctx.GetQuery<ObjectClass>().ToList();
                Assert.That(list2, Is.Not.Null);
                Assert.That(list2.Count, Is.EqualTo(list1.Count));
                list2.ForEach(obj => Assert.That(obj, Is.Not.Null));

                for (int i = 0; i < list1.Count; i++)
                {
                    Assert.That(object.ReferenceEquals(list1[i], list2[i]), "list1[i] & list2[i] are different Objects");
                }
            }
        }

        [Test]
        public void GetObject_GetList()
        {
            using (IKistlContext ctx = GetContext())
            {
                var prop = ctx.Find<Property>(1);
                Assert.That(prop, Is.Not.Null);
                Assert.That(prop.Context, Is.EqualTo(ctx));

                var list_objclass = ctx.GetQuery<ObjectClass>().ToList();
                Assert.That(list_objclass.Count, Is.GreaterThan(0));

                var objclass = list_objclass.Single(o => o == prop.ObjectClass);
                Assert.That(objclass.Context, Is.EqualTo(ctx));
                var prop_test = objclass.Properties.Single(p => p.ID == prop.ID);
                Assert.That(prop_test.Context, Is.EqualTo(ctx));

                Assert.That(object.ReferenceEquals(prop, prop_test), "prop & prop_test are different Objects");
            }
        }

        [Test]
        public void GetList_GetOneObject()
        {
            using (IKistlContext ctx = GetContext())
            {
                var list_objclass = ctx.GetQuery<ObjectClass>().ToList();
                Assert.That(list_objclass.Count, Is.GreaterThan(0));

                var prop = ctx.Find<Property>(1);
                Assert.That(prop, Is.Not.Null);

                var objclass = list_objclass.Single(o => o == prop.ObjectClass);
                var prop_test = objclass.Properties.Single(p => p.ID == prop.ID);

                Assert.That(object.ReferenceEquals(prop, prop_test), "prop & prop_test are different Objects");
            }
        }


        [Test]
        public void GetListWithTake()
        {
            using (IKistlContext ctx = GetContext())
            {
                var list = ctx.GetQuery<ObjectClass>().Take(10).ToList();
                Assert.That(list.Count, Is.EqualTo(10));
            }
        }

        [Test]
        public void GetListWithTakeAndWhere()
        {
            using (IKistlContext ctx = GetContext())
            {
                var list = ctx.GetQuery<ObjectClass>().Where(o => o.Module.Name == "KistlBase").Take(10).ToList();
                Assert.That(list.Count, Is.EqualTo(10));
            }
        }

        [Test]
        public void GetListWithOrderBy()
        {
            using (IKistlContext ctx = GetContext())
            {
                var list = ctx.GetQuery<ObjectClass>().OrderBy(o => o.Name).ToList();
                Assert.That(list.Count, Is.GreaterThan(0));
                List<ObjectClass> result = list.ToList();
                List<ObjectClass> sorted = list.OrderBy(o => o.Name).ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    if (result[i].ID != sorted[i].ID)
                    {
                        Assert.Fail("List was not sorted");
                        break;
                    }
                }
            }
        }

        [Test]
        public void GetListByTypeWithOrderBy()
        {
            using (IKistlContext ctx = GetContext())
            {
                var list = ctx.GetQuery(ctx.GetInterfaceType(typeof(ObjectClass))).Cast<ObjectClass>()
                    .OrderBy(o => o.Name)
                    .ToList().Cast<ObjectClass>();
                Assert.That(list.Count(), Is.GreaterThan(0));
                List<ObjectClass> result = list.ToList();
                List<ObjectClass> sorted = list.ToList().OrderBy(o => o.Name).ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    if (result[i].ID != sorted[i].ID)
                    {
                        Assert.Fail("List was not sorted");
                        break;
                    }
                }
            }
        }

        [Test]
        public void GetListWithOrderByAndWhere()
        {
            using (IKistlContext ctx = GetContext())
            {
                var list = ctx.GetQuery<ObjectClass>().Where(o => o.Module.Name == "KistlBase").OrderBy(o => o.Name).ToList();
                Assert.That(list.Count, Is.GreaterThan(0));
                List<ObjectClass> result = list.ToList();
                List<ObjectClass> sorted = list.OrderBy(o => o.Name).ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    if (result[i].ID != sorted[i].ID)
                    {
                        Assert.Fail("List was not sorted");
                        break;
                    }
                }
            }
        }

        [Test]
        public void GetListWithOrderByThenOrderBy()
        {
            using (IKistlContext ctx = GetContext())
            {
                var list = ctx.GetQuery<ObjectClass>().OrderBy(o => o.Module.Name).ThenBy(o => o.Name).ToList();
                Assert.That(list.Count, Is.GreaterThan(0));
                List<ObjectClass> result = list.ToList();
                List<ObjectClass> sorted = list.OrderBy(o => o.Module.Name).ThenBy(o => o.Name).ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    if (result[i].ID != sorted[i].ID)
                    {
                        Assert.Fail("List was not sorted");
                        break;
                    }
                }
            }
        }

        [Test]
        public void GetListWithParameterLegal()
        {
            using (IKistlContext ctx = GetContext())
            {
                var test = (from m in ctx.GetQuery<Module>()
                            where
                                m.Name.StartsWith("K")
                                && m.Namespace.Length > 1
                                && m.Name == "KistlBase"
                                && m.Name.EndsWith("e")
                            select m).ToList();
                Assert.That(test.Count, Is.EqualTo(1));
                foreach (var t in test)
                {
                    Log.DebugFormat("GetListWithParameterLegal: {0}", t.Name);
                }
            }
        }

        [Test]
        [ExpectedException(typeof(System.ServiceModel.FaultException))]
        public void GetListWithParameterIllegal()
        {
            using (IKistlContext ctx = GetContext())
            {
                var test = from z in ctx.GetQuery<Kistl.App.TimeRecords.WorkEffortAccount>()
                           where z.Mitarbeiter.Select(ma => ma.Geburtstag > new DateTime(1978, 1, 1)).Count() > 0
                           select z;
                foreach (var t in test)
                {
                    Log.DebugFormat("GetListWithParameterIllegal: {0}", t.Name);
                }
            }
        }

        [Test]
        public void GetListWithEnum()
        {
            using (IKistlContext ctx = GetContext())
            {
                var q = ctx.GetQuery<Relation>().Where(r => r.Storage == StorageType.Separate).ToList();
                Assert.IsNotEmpty(q);
                foreach (var r in q)
                {
                    Assert.That(r, Is.Not.Null);
                    Assert.That(r.Storage, Is.EqualTo(StorageType.Separate));
                }
            }
        }

        [Test]
        public void GetListWithProjection()
        {
            using (IKistlContext ctx = GetContext())
            {
                var test = from z in ctx.GetQuery<Kistl.App.TimeRecords.WorkEffortAccount>()
                           select new { A = z.SpentHours, B = z.BudgetHours };
                foreach (var t in test)
                {
                    Log.DebugFormat("GetListWithProjection: {0}", t.A);
                }
            }
        }

        [Test]
        public void GetListWithSingle()
        {
            using (IKistlContext ctx = GetContext())
            {
                var guiModule = ctx.GetQuery<Module>().Where(m => m.Name == "GUI").Single();
                Assert.That(guiModule, Is.Not.Null);
            }
        }

        [Test]
        public void GetListSingle()
        {
            using (IKistlContext ctx = GetContext())
            {
                var guiModule = ctx.GetQuery<Module>().Single(m => m.Name == "GUI");
                Assert.That(guiModule, Is.Not.Null);
            }
        }

        [Test]
        public void GetListWithFirst()
        {
            using (IKistlContext ctx = GetContext())
            {
                var guiModule = ctx.GetQuery<Module>().Where(m => m.Name == "GUI").First();
                Assert.That(guiModule, Is.Not.Null);
            }
        }

        [Test]
        public void GetListFirst()
        {
            using (IKistlContext ctx = GetContext())
            {
                var guiModule = ctx.GetQuery<Module>().First(m => m.Name == "GUI");
                Assert.That(guiModule, Is.Not.Null);
            }
        }


        public class Test
        {
            public string TestProp { get; set; }
        }


        /// <summary>
        /// Case 472
        /// http://blogs.msdn.com/mattwar/archive/2007/08/01/linq-building-an-iqueryable-provider-part-iii.aspx
        /// </summary>
        [Test]
        public void GetListWithPropertyAccessor()
        {
            using (IKistlContext ctx = GetContext())
            {
                Test t = new Test();
                t.TestProp = "foo";
                var result = ctx.GetQuery<Assembly>()
                    .Where(a => a.Name == t.TestProp).ToList();
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count, Is.EqualTo(0));
            }
        }

        /// <summary>
        /// </summary>
        [Test]
        public void GetListWithPropertyObjectAccessor()
        {
            using (IKistlContext ctx = GetContext())
            {
                int mID = 1;
                var result = ctx.GetQuery<ObjectClass>().Where(c => c.Module.ID == mID).ToList();
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count, Is.GreaterThan(0));
            }
        }

        /// <summary>
        /// </summary>
        [Test]
        public void GetListWithObjectFilter()
        {
            using (IKistlContext ctx = GetContext())
            {
                var module = ctx.GetQuery<Module>().Where(m => m.Name == "KistlBase").Single();
                Assert.That(module, Is.Not.Null);
                var result = ctx.GetQuery<ObjectClass>().Where(c => c.Module == module).ToList();
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count, Is.GreaterThan(0));
            }
        }
    }
}
