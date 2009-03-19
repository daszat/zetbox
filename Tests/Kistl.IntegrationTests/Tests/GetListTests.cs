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
    public class GetListTests
    {
        [SetUp]
        public void SetUp()
        {
            //CacheController<IDataObject>.Current.Clear();
        }

        [Test]
        public void GetList()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var list = ctx.GetQuery<ObjectClass>().ToList();
                Assert.That(list.Count, Is.GreaterThan(0));
            }
        }

        [Test]
        public void GetList_Twice()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
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
            using (IKistlContext ctx = KistlContext.GetContext())
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
        public void GetList_GetObject()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
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
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var list = ctx.GetQuery<ObjectClass>().Take(10).ToList();
                Assert.That(list.Count, Is.EqualTo(10));
            }
        }

        [Test]
        public void GetListWithTakeAndWhere()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var list = ctx.GetQuery<ObjectClass>().Where(o => o.Module.ModuleName == "KistlBase").Take(10).ToList();
                Assert.That(list.Count, Is.EqualTo(10));
            }
        }

        [Test]
        public void GetListWithOrderBy()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var list = ctx.GetQuery<ObjectClass>().OrderBy(o => o.ClassName).ToList();
                Assert.That(list.Count, Is.GreaterThan(0));
                List<ObjectClass> result = list.ToList();
                List<ObjectClass> sorted = list.OrderBy(o => o.ClassName).ToList();

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
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var list = ctx.GetQuery(new InterfaceType(typeof(ObjectClass))).Cast<ObjectClass>()
                    .OrderBy(o => o.ClassName)
                    .ToList().Cast<ObjectClass>();
                Assert.That(list.Count(), Is.GreaterThan(0));
                List<ObjectClass> result = list.ToList();
                List<ObjectClass> sorted = list.ToList().OrderBy(o => o.ClassName).ToList();

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
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var list = ctx.GetQuery<ObjectClass>().Where(o => o.Module.ModuleName == "KistlBase").OrderBy(o => o.ClassName).ToList();
                Assert.That(list.Count, Is.GreaterThan(0));
                List<ObjectClass> result = list.ToList();
                List<ObjectClass> sorted = list.OrderBy(o => o.ClassName).ToList();

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
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var list = ctx.GetQuery<ObjectClass>().OrderBy(o => o.Module.ModuleName).ThenBy(o => o.ClassName).ToList();
                Assert.That(list.Count, Is.GreaterThan(0));
                List<ObjectClass> result = list.ToList();
                List<ObjectClass> sorted = list.OrderBy(o => o.Module.ModuleName).ThenBy(o => o.ClassName).ToList();

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
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var test = (from m in ctx.GetQuery<Module>()
                            where
                                m.ModuleName.StartsWith("K")
                                && m.Namespace.Length > 1
                                && m.ModuleName == "KistlBase"
                                && m.ModuleName.EndsWith("e")
                            select m).ToList();
                Assert.That(test.Count, Is.EqualTo(1));
                foreach (var t in test)
                {
                    System.Diagnostics.Trace.WriteLine(string.Format("GetListWithParameterLegal: {0}", t.ModuleName));
                }
            }
        }

        [Test]
        [ExpectedException(typeof(System.ServiceModel.FaultException))]
        public void GetListWithParameterIllegal()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var test = from z in ctx.GetQuery<Kistl.App.Zeiterfassung.Zeitkonto>()
                           where z.Taetigkeiten.Select(tt => tt.Mitarbeiter.Geburtstag > new DateTime(1978, 1, 1)).Count() > 0
                           select z;
                foreach (var t in test)
                {
                    System.Diagnostics.Trace.WriteLine(string.Format("GetListWithParameterIllegal: {0}", t.Kontoname));
                }
            }
        }

        [Test]
        public void GetListWithProjection()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var test = from z in ctx.GetQuery<Kistl.App.Zeiterfassung.Zeitkonto>()
                           select new { A = z.AktuelleStunden, B = z.MaxStunden };
                foreach (var t in test)
                {
                    Trace.WriteLine(string.Format("GetListWithProjection: {0}", t.A));
                }
            }
        }

        [Test]
        public void GetListWithSingle()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var guiModule = ctx.GetQuery<Module>().Where(m => m.ModuleName == "GUI").Single();
                Assert.That(guiModule, Is.Not.Null);
            }
        }

        [Test]
        public void GetListSingle()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var guiModule = ctx.GetQuery<Module>().Single(m => m.ModuleName == "GUI");
                Assert.That(guiModule, Is.Not.Null);
            }
        }

        [Test]
        public void GetListWithFirst()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var guiModule = ctx.GetQuery<Module>().Where(m => m.ModuleName == "GUI").First();
                Assert.That(guiModule, Is.Not.Null);
            }
        }

        [Test]
        public void GetListFirst()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var guiModule = ctx.GetQuery<Module>().First(m => m.ModuleName == "GUI");
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
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                Test t = new Test();
                t.TestProp = "foo";
                var result = ctx.GetQuery<Assembly>()
                    .Where(a => a.AssemblyName == t.TestProp).ToList();
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count, Is.EqualTo(0));
            }
        }

        /// <summary>
        /// </summary>
        [Test]
        public void GetListWithPropertyObjectAccessor()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                int mID = 1;
                var result = ctx.GetQuery<ObjectClass>().Where(c => c.Module.ID == mID).ToList();
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count, Is.GreaterThan(0));
            }
        }
    }
}
