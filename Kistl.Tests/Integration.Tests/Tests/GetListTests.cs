using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.Client;
using Kistl.API.Client;

namespace Integration.Tests.Tests
{
    [TestFixture]
    public class GetListTests
    {
        [SetUp]
        public void SetUp()
        {
            CacheController<Kistl.API.IDataObject>.Current.Clear();
        }

        [Test]
        public void GetList()
        {
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var list = ctx.GetQuery<Kistl.App.Base.ObjectClass>().ToList();
                Assert.That(list.Count, Is.GreaterThan(0));
            }
        }

        [Test]
        public void GetListWithTop10()
        {
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var list = ctx.GetQuery<Kistl.App.Base.ObjectClass>().Take(10).ToList();
                Assert.That(list.Count, Is.EqualTo(10));
            }
        }

        [Test]
        public void GetListWithOrderBy()
        {
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
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
        public void GetListWithParameterLegal()
        {
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
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
                    System.Diagnostics.Trace.WriteLine(string.Format("GetListWithParameterLegal: {0}", t.ModuleName));
                }
            }
        }

        [Test]
        [ExpectedException(typeof(System.ServiceModel.FaultException))]
        public void GetListWithParameterIllegal()
        {
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
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
    }
}
