using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.Client;

namespace Kistl.Tests.IntegrationTests
{
    [TestFixture]
    public class LinqToKistlTests
    {
        [Test]
        public void GetObject()
        {
            using (Kistl.API.Client.KistlContext ctx = new Kistl.API.Client.KistlContext())
            {
                var obj = ctx.GetQuery < Kistl.App.Base.ObjectClass>().Single(o => o.ID == 2);
                Assert.That(obj.ID, Is.EqualTo(2));
            }
        }

        [Test]
        public void GetList()
        {
            using (Kistl.API.Client.KistlContext ctx = new Kistl.API.Client.KistlContext())
            {
                var list = ctx.GetQuery<Kistl.App.Base.ObjectClass>().ToList();
                Assert.That(list.Count, Is.GreaterThan(0));
            }
        }

        [Test]
        public void GetListWithTop10()
        {
            using (Kistl.API.Client.KistlContext ctx = new Kistl.API.Client.KistlContext())
            {
                var list = ctx.GetQuery<Kistl.App.Base.ObjectClass>().Take(10).ToList();
                Assert.That(list.Count, Is.EqualTo(10));
            }
        }

        [Test]
        public void SetObject()
        {
            double aufwand;
            int ID;
            using (Kistl.API.Client.KistlContext ctx = new Kistl.API.Client.KistlContext())
            {
                var list = ctx.GetQuery<Kistl.App.Projekte.Task>().ToList();
                Assert.That(list.Count, Is.GreaterThan(0));
                var obj = list[0];

                ID = obj.ID;
                aufwand = (obj.Aufwand ?? 0.0) + 1.0;

                obj.Aufwand = aufwand;

                ctx.SubmitChanges();
            }

            using (Kistl.API.Client.KistlContext checkctx = new Kistl.API.Client.KistlContext())
            {
                var obj = checkctx.GetQuery<Kistl.App.Projekte.Task>().Single(o => o.ID == ID);
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.Aufwand, Is.EqualTo(aufwand));
            }
        }
        
        [Test]
        public void NewObject()
        {
            int ID;
            double aufwand;
            DateTime datum;
            Kistl.App.Projekte.Projekt p;
            using (Kistl.API.Client.KistlContext ctx = new Kistl.API.Client.KistlContext())
            {
                p = ctx.GetQuery<Kistl.App.Projekte.Projekt>().ToList()[0];
                var obj = ctx.Create<Kistl.App.Projekte.Task>();

                obj.Name = "NUnit Test Task";
                obj.Aufwand = aufwand = 1.0;
                obj.DatumVon = datum = DateTime.Now;
                obj.DatumBis = datum.AddDays(1);
                obj.Projekt = p;

                ctx.SubmitChanges();
                ID = obj.ID;
                Assert.That(ID, Is.Not.EqualTo(Kistl.API.Helper.INVALIDID));
            }

            using (Kistl.API.Client.KistlContext checkctx = new Kistl.API.Client.KistlContext())
            {
                var obj = checkctx.GetQuery<Kistl.App.Projekte.Task>().Single(o => o.ID == ID);
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.Aufwand, Is.EqualTo(aufwand));
                Assert.That(obj.DatumVon, Is.EqualTo(datum));
                Assert.That(obj.DatumBis, Is.EqualTo(datum.AddDays(1)));
                Assert.That(obj.Projekt.ID, Is.EqualTo(p.ID));
            }
        }

        [Test]
        public void GetListWithParameterLegal()
        {
            using (Kistl.API.Client.KistlContext ctx = new Kistl.API.Client.KistlContext())
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
            using (Kistl.API.Client.KistlContext ctx = new Kistl.API.Client.KistlContext())
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
