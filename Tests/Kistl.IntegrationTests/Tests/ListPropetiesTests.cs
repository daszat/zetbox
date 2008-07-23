using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.Client;
using Kistl.API.Client;

namespace Kistl.IntegrationTests
{
    [TestFixture]
    public class ListPropetiesTests
    {
        [SetUp]
        public void SetUp()
        {
            //CacheController<Kistl.API.IDataObject>.Current.Clear();
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
                        Assert.That(m.ID, Is.Not.EqualTo(Kistl.API.Helper.INVALIDID));
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
                    k.EMails.Add(mail);
                    break;
                }
                Assert.That(mail, Is.Not.EqualTo(""));
                int submitCount = ctx.SubmitChanges();
                Assert.That(submitCount, Is.EqualTo(1));
            }

            //CacheController<Kistl.API.IDataObject>.Current.Clear();

            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var kunde = ctx.GetQuery<Kistl.App.Projekte.Kunde>().Single(k => k.ID == ID);
                Assert.That(kunde, Is.Not.Null);
                Assert.That(kunde.EMails.Count, Is.GreaterThan(0));
                var result = kunde.EMails.SingleOrDefault(m => m == mail);
                Assert.That(result, Is.Not.Null);
            }
        }

        [Test]
        public void UpdateStringListPropertyContent()
        {
            int ID = Kistl.API.Helper.INVALIDID;
            string mail = "";
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var list = ctx.GetQuery<Kistl.App.Projekte.Kunde>();
                foreach (Kistl.App.Projekte.Kunde k in list)
                {
                    if (k.EMails.Count > 0)
                    {
                        mail = "UnitTest" + DateTime.Now + "@dasz.at";
                        k.EMails[0] = mail;
                        ID = k.ID;
                        break;
                    }
                }
                Assert.That(mail, Is.Not.EqualTo(""));
                int submitCount = ctx.SubmitChanges();
                Assert.That(submitCount, Is.EqualTo(1));
            }

            //CacheController<Kistl.API.IDataObject>.Current.Clear();

            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var kunde = ctx.GetQuery<Kistl.App.Projekte.Kunde>().Single(k => k.ID == ID);
                Assert.That(kunde, Is.Not.Null);
                Assert.That(kunde.EMails.Count, Is.GreaterThan(0));
                var result = kunde.EMails.SingleOrDefault(m => m == mail);
                Assert.That(result, Is.Not.Null);
            }
        }

        [Test]
        public void DeleteStringListPropertyContent()
        {
            int ID = Kistl.API.Helper.INVALIDID;
            int mailCount = 0;
            string mail = "";
            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var list = ctx.GetQuery<Kistl.App.Projekte.Kunde>();
                foreach (Kistl.App.Projekte.Kunde k in list)
                {
                    if (k.EMails.Count > 0)
                    {
                        mail = k.EMails[0];
                        k.EMails.RemoveAt(0);
                        mailCount = k.EMails.Count;
                        ID = k.ID;
                        break;
                    }
                }
                int submitCount = ctx.SubmitChanges();
                Assert.That(submitCount, Is.EqualTo(1));
            }

            //CacheController<Kistl.API.IDataObject>.Current.Clear();

            using (Kistl.API.IKistlContext ctx = Kistl.API.Client.KistlContext.GetContext())
            {
                var kunde = ctx.GetQuery<Kistl.App.Projekte.Kunde>().Single(k => k.ID == ID);
                Assert.That(kunde, Is.Not.Null);
                Assert.That(kunde.EMails.Count, Is.GreaterThan(0));

                var result = kunde.EMails.SingleOrDefault(m => m == mail);
                Assert.That(kunde.EMails.Count, Is.EqualTo(mailCount));
                Assert.That(result, Is.Null);
            }
        }
    }
}
