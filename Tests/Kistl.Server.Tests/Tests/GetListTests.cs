using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Kistl.Server.Tests
{
    [TestFixture]
    public class GetListTests
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Tests.Server.GetList");

        [SetUp]
        public void SetUp()
        {
            //CacheController<Kistl.API.IDataObject>.Current.Clear();
        }

        [Test]
        public void GetList()
        {
            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            {
                var list = ctx.GetQuery<Kistl.App.Base.ObjectClass>().ToList();
                Assert.That(list.Count, Is.GreaterThan(0));
            }
        }

        [Test]
        public void GetList_Twice()
        {
            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            {
                List<Kistl.App.Base.ObjectClass> list1 = ctx.GetQuery<Kistl.App.Base.ObjectClass>().ToList();
                Assert.That(list1, Is.Not.Null);
                Assert.That(list1.Count, Is.AtLeast(2));
                list1.ForEach(obj => Assert.That(obj, Is.Not.Null));

                List<Kistl.App.Base.ObjectClass> list2 = ctx.GetQuery<Kistl.App.Base.ObjectClass>().ToList();
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
            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            {
                var prop = ctx.Find<Kistl.App.Base.Property>(1);
                Assert.That(prop, Is.Not.Null);
                Assert.That(prop.Context, Is.EqualTo(ctx));

                var list_objclass = ctx.GetQuery<Kistl.App.Base.ObjectClass>().ToList();
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
            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            {
                var list_objclass = ctx.GetQuery<Kistl.App.Base.ObjectClass>().ToList();
                Assert.That(list_objclass.Count, Is.GreaterThan(0));

                var prop = ctx.Find<Kistl.App.Base.Property>(1);
                Assert.That(prop, Is.Not.Null);

                var objclass = list_objclass.Single(o => o == prop.ObjectClass);
                var prop_test = objclass.Properties.Single(p => p.ID == prop.ID);

                Assert.That(object.ReferenceEquals(prop, prop_test), "prop & prop_test are different Objects");
            }
        }


        [Test]
        public void GetListWithTake()
        {
            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            {
                var list = ctx.GetQuery<Kistl.App.Base.ObjectClass>().Take(10).ToList();
                Assert.That(list.Count, Is.EqualTo(10));
            }
        }

        [Test]
        public void GetListWithTakeAndWhere()
        {
            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            {
                var list = ctx.GetQuery<Kistl.App.Base.ObjectClass>().Where(o => o.Module.ModuleName == "KistlBase").Take(10).ToList();
                Assert.That(list.Count, Is.EqualTo(10));
            }
        }

        [Test]
        public void GetListWithOrderBy()
        {
            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            {
                var list = ctx.GetQuery<Kistl.App.Base.ObjectClass>().OrderBy(o => o.ClassName).ToList();
                Assert.That(list.Count, Is.GreaterThan(0));
                List<Kistl.App.Base.ObjectClass> result = list.ToList();
                List<Kistl.App.Base.ObjectClass> sorted = list.OrderBy(o => o.ClassName).ToList();

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
        [Ignore("Case 617")]
        public void GetListByTypeWithOrderBy()
        {
            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            {
                var list = ctx.GetQuery(new InterfaceType(typeof(ObjectClass)))
                    .OrderBy<IDataObject, string>(o => ((ObjectClass)o).ClassName)
                    .ToList().Cast<ObjectClass>();
                Assert.That(list.Count(), Is.GreaterThan(0));
                List<Kistl.App.Base.ObjectClass> result = list.ToList();
                List<Kistl.App.Base.ObjectClass> sorted = list.ToList().OrderBy(o => o.ClassName).ToList();

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
            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            {
                var list = ctx.GetQuery<Kistl.App.Base.ObjectClass>().Where(o => o.Module.ModuleName == "KistlBase").OrderBy(o => o.ClassName).ToList();
                Assert.That(list.Count, Is.GreaterThan(0));
                List<Kistl.App.Base.ObjectClass> result = list.ToList();
                List<Kistl.App.Base.ObjectClass> sorted = list.OrderBy(o => o.ClassName).ToList();

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
        [Ignore("Case 634")]
        public void GetListWithOrderByThenOrderBy()
        {
            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            {
                var list = ctx.GetQuery<Kistl.App.Base.ObjectClass>().OrderBy(o => o.Module.ModuleName).ThenBy(o => o.ClassName).ToList();
                Assert.That(list.Count, Is.GreaterThan(0));
                List<Kistl.App.Base.ObjectClass> result = list.ToList();
                List<Kistl.App.Base.ObjectClass> sorted = list.OrderBy(o => o.Module.ModuleName).ThenBy(o => o.ClassName).ToList();

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
            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            {
                var test = (from m in ctx.GetQuery<Kistl.App.Base.Module>()
                            where
                                m.ModuleName.StartsWith("K")
                                && m.Namespace.Length > 1
                                && m.ModuleName == "KistlBase"
                                && m.ModuleName.EndsWith("e")
                            select m).ToList();
                Assert.That(test.Count, Is.EqualTo(1));
                foreach (var t in test)
                {
                    Log.DebugFormat("GetListWithParameterLegal: {0}", t.ModuleName);
                }
            }
        }

        //[Test]
        //[Ignore("System.InvalidOperationException : The operands for operator 'GreaterThan' do not match the parameters of method 'op_GreaterThan'.")]
        //public void GetListWithParameterIllegal()
        //{
        //    using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
        //    {
        //        var test = from z in ctx.GetQuery<Kistl.App.TimeRecords.WorkEffortAccount>()
        //                   where z.Taetigkeiten.Select(tt => tt.Mitarbeiter.Geburtstag.HasValue && 
        //                       ((DateTime)tt.Mitarbeiter.Geburtstag) > new DateTime(1978, 1, 1)).Count() > 0
        //                   select z;
        //        foreach (var t in test)
        //        {
        //            Log.DebugFormat("GetListWithParameterIllegal: {0}", t.Name);
        //        }
        //    }
        //}

        [Test]
        public void GetListWithProjection()
        {
            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
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
        [ExpectedException()]
        public void GetListWithSingle()
        {
            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            {
                var guiModule = ctx.GetQuery<Kistl.App.Base.Module>().Where(m => m.ModuleName == "GUI").Single();
                Assert.That(guiModule, Is.Not.Null);
            }
        }

        [Test]
        [ExpectedException]
        public void GetListSingle()
        {
            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            {
                var guiModule = ctx.GetQuery<Kistl.App.Base.Module>().Single(m => m.ModuleName == "GUI");
                Assert.That(guiModule, Is.Not.Null);
            }
        }

        [Test]
        public void GetListWithFirst()
        {
            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            {
                var guiModule = ctx.GetQuery<Kistl.App.Base.Module>().Where(m => m.ModuleName == "GUI").First();
                Assert.That(guiModule, Is.Not.Null);
            }
        }

        [Test]
        public void GetListFirst()
        {
            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            {
                var guiModule = ctx.GetQuery<Kistl.App.Base.Module>().First(m => m.ModuleName == "GUI");
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
            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            {
                Test t = new Test();
                t.TestProp = "foo";
                var result = ctx.GetQuery<Kistl.App.Base.Assembly>()
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
            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            {
                int mID = 1;
                var result = ctx.GetQuery<Kistl.App.Base.ObjectClass>().Where(c => c.Module.ID == mID).ToList();
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count, Is.GreaterThan(0));
            }
        }
    }
}
