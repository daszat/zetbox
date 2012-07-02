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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Zetbox.API;
using Zetbox.API.Client;
using Zetbox.App.Base;
using Zetbox.Client;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Zetbox.IntegrationTests
{
    [TestFixture]
    public class GetListTests : AbstractIntegrationTestFixture
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Tests.Integration.GetList");


        [Test]
        public void GetList()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var list = ctx.GetQuery<ObjectClass>().ToList();
                Assert.That(list.Count, Is.GreaterThan(0));
            }
        }

        [Test]
        public void GetList_Twice()
        {
            using (IZetboxContext ctx = GetContext())
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
        public void GetList_Twice_on_same_query()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var query = ctx.GetQuery<ObjectClass>();
                List<ObjectClass> list1 = query.ToList();
                Assert.That(list1, Is.Not.Null);
                Assert.That(list1.Count, Is.AtLeast(2));
                list1.ForEach(obj => Assert.That(obj, Is.Not.Null));

                List<ObjectClass> list2 = query.ToList();
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
            using (IZetboxContext ctx = GetContext())
            {
                var prop = ctx.Find<Property>(1);
                Assert.That(prop, Is.Not.Null);
                Assert.That(prop.Context, Is.EqualTo(ctx));

                var list_objclass = ctx.GetQuery<DataType>().ToList();
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
            using (IZetboxContext ctx = GetContext())
            {
                var list_objclass = ctx.GetQuery<DataType>().ToList();
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
            using (IZetboxContext ctx = GetContext())
            {
                var list = ctx.GetQuery<ObjectClass>().Take(10).ToList();
                Assert.That(list.Count, Is.EqualTo(10));
            }
        }

        [Test]
        public void GetListWithTakeAndWhere()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var list = ctx.GetQuery<ObjectClass>().Where(o => o.Module.Name == "ZetboxBase").Take(10).ToList();
                Assert.That(list.Count, Is.EqualTo(10));
            }
        }

        [Test]
        public void GetListWithTakeAndWhere_Twice()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var query = ctx.GetQuery<ObjectClass>();
                var list1 = query.Where(o => o.Module.Name == "ZetboxBase").Take(10).ToList();
                var list2 = query.Where(o => o.Module.Name == "ZetboxBase").Take(10).ToList();
                Assert.That(list1.Count, Is.EqualTo(10));
                Assert.That(list2.Count, Is.EqualTo(10));
            }
        }

        [Test]
        public void GetListWithTakeAndMultipleWhere()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var list = ctx.GetQuery<ObjectClass>().Where(o => o.Module.Name == "ZetboxBase").Where(o => o.Name.Contains("a")).Take(10).ToList();
                Assert.That(list.Count, Is.EqualTo(10));
            }
        }

        [Test]
        public void GetListWithOrderBy()
        {
            using (IZetboxContext ctx = GetContext())
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
        public void GetListWithOrderByAndWhere()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var list = ctx.GetQuery<ObjectClass>().Where(o => o.Module.Name == "ZetboxBase").OrderBy(o => o.Name).ToList();
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
            using (IZetboxContext ctx = GetContext())
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
            using (IZetboxContext ctx = GetContext())
            {
                var test = (from m in ctx.GetQuery<Module>()
                            where
                                m.Name.StartsWith("Z")
                                && m.Namespace.Length > 1
                                && m.Name == "ZetboxBase"
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
        [Ignore("Illegal Expression checking disabled for now")]
        public void GetListWithParameterIllegalAggreggation()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var test = from z in ctx.GetQuery<ObjectClass>()
                           where z.Properties.Count() > 0
                           select z;

                foreach (var t in test)
                {
                    Log.DebugFormat("GetListWithParameterIllegalAggreggation: {0}", t.Name);
                }
            }
        }

        [Test]
        public void GetListWithEnum()
        {
            using (IZetboxContext ctx = GetContext())
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
            using (IZetboxContext ctx = GetContext())
            {
                var test = from z in ctx.GetQuery<ObjectClass>()
                           select new { A = z.Name, B = z.TableName };
                foreach (var t in test)
                {
                    Log.DebugFormat("GetListWithProjection: {0}", t.A);
                }
            }
        }

        [Test]
        public void GetListWithSingle()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var guiModule = ctx.GetQuery<Module>().Where(m => m.Name == "GUI").Single();
                Assert.That(guiModule, Is.Not.Null);
            }
        }

        [Test]
        public void GetListSingle()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var guiModule = ctx.GetQuery<Module>().Single(m => m.Name == "GUI");
                Assert.That(guiModule, Is.Not.Null);
            }
        }

        [Test]
        public void GetListWithFirst()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var guiModule = ctx.GetQuery<Module>().Where(m => m.Name == "GUI").First();
                Assert.That(guiModule, Is.Not.Null);
            }
        }

        [Test]
        public void GetListFirst()
        {
            using (IZetboxContext ctx = GetContext())
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
            using (IZetboxContext ctx = GetContext())
            {
                Test t = new Test();
                t.TestProp = "foo";
                var result = ctx.GetQuery<Assembly>()
                    .Where(a => a.Name == t.TestProp).ToList();
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count, Is.EqualTo(0));
            }
        }

        [Test]
        public void GetListWithPropertyObjectAccessor()
        {
            using (IZetboxContext ctx = GetContext())
            {
                int mID = ctx.GetQuery<Zetbox.App.Base.ObjectClass>().First().Module.ID;
                using (var otherCtx = GetContext())
                {
                    var result = otherCtx.GetQuery<Zetbox.App.Base.ObjectClass>().Where(c => c.Module.ID == mID).ToList();
                    Assert.That(result, Is.Not.Null);
                    Assert.That(result.Count, Is.GreaterThan(0));
                }
            }
        }

        [Test]
        public void GetListWithObjectFilter()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var module = ctx.GetQuery<Module>().Where(m => m.Name == "ZetboxBase").Single();
                Assert.That(module, Is.Not.Null);
                var result = ctx.GetQuery<ObjectClass>().Where(c => c.Module == module).ToList();
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count, Is.GreaterThan(0));
            }
        }

        [Test]
        public void GetListWithObjectFilterAndCast()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var module = ctx.GetQuery<Module>().Where(m => m.Name == "ZetboxBase").Single();
                Assert.That(module, Is.Not.Null);
                var result = ctx.GetQuery<ObjectClass>().Where(c => c.Module == module).Cast<IDataObject>().ToList();
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count, Is.GreaterThan(0));
            }
        }
    }
}
