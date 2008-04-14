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
    public class ListPropetiesTests
    {
        [SetUp]
        public void SetUp()
        {
            CacheController<Kistl.API.IDataObject>.Current.Clear();
        }

        [Test]
        public void GetStringListPropertyContent()
        {
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var list = ctx.GetQuery<Kistl.App.Projekte.Kunde>();
                int count = 0;
                foreach (Kistl.App.Projekte.Kunde k in list)
                {
                    count += k.EMails.Count;
                    foreach (var e in k.EMails)
                    {
                        Assert.That(e.Parent.ID, Is.EqualTo(k.ID));
                    }
                }
                Assert.That(count, Is.GreaterThan(0));
            }
        }

        [Test]
        public void GetPointerListPropertyContent()
        {
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var list = ctx.GetQuery<Kistl.App.Projekte.Projekt>();
                int count = 0;
                foreach (Kistl.App.Projekte.Projekt prj in list)
                {
                    count += prj.Mitarbeiter.Count;
                    foreach (var m in prj.Mitarbeiter)
                    {
                        Assert.That(m.Parent.ID, Is.EqualTo(prj.ID));
                        Assert.That(m.Value.ID, Is.Not.EqualTo(Kistl.API.Helper.INVALIDID));
                    }
                }
                Assert.That(count, Is.GreaterThan(0));
            }
        }

        [Test]
        public void AddStringListPropertyContent()
        {
            int ID = Kistl.API.Helper.INVALIDID;
            string mail = "";
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var list = ctx.GetQuery<Kistl.App.Projekte.Kunde>();
                foreach (Kistl.App.Projekte.Kunde k in list)
                {
                    mail = "UnitTest" + DateTime.Now + "@dasz.at";
                    ID = k.ID;
                    k.EMails.Add(new Kistl.App.Projekte.Kunde_EMailsCollectionEntry() { Value = mail });
                    break;
                }
                Assert.That(mail, Is.Not.EqualTo(""));
                ctx.SubmitChanges();
            }

            CacheController<Kistl.API.IDataObject>.Current.Clear();

            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var kunde = ctx.GetQuery<Kistl.App.Projekte.Kunde>().Single(k => k.ID == ID);
                Assert.That(kunde, Is.Not.Null);
                Assert.That(kunde.EMails.Count, Is.GreaterThan(0));
                var result = kunde.EMails.SingleOrDefault(m => m.Value == mail);
                Assert.That(result, Is.Not.Null);
            }
        }
    }
}
