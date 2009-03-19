using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API.Server;

using NUnit.Framework;
using NUnit.Framework.Constraints;


namespace Kistl.Server.Tests
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
            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
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
            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
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
            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
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
                ctx.SubmitChanges();
            }

            //CacheController<Kistl.API.IDataObject>.Current.Clear();

            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            {
                var kunde = ctx.GetQuery<Kistl.App.Projekte.Kunde>().First(k => k.ID == ID);
                Assert.That(kunde, Is.Not.Null);
                Assert.That(kunde.EMails.Count, Is.GreaterThan(0));
                var result = kunde.EMails.SingleOrDefault(m => m == mail);
                Assert.That(result, Is.Not.Null);
            }
        }

        [Test]
        [Ignore("Implement IsSorted on Kunde.EMails")]
        public void UpdateStringListPropertyContent()
        {
            int ID = Kistl.API.Helper.INVALIDID;
            string mail = "";
            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            {
                var list = ctx.GetQuery<Kistl.App.Projekte.Kunde>();
                foreach (Kistl.App.Projekte.Kunde k in list)
                {
                    if (k.EMails.Count > 0)
                    {
                        mail = "UnitTest" + DateTime.Now + "@dasz.at";
                        //k.EMails[0] = mail;
                        ID = k.ID;
                        break;
                    }
                }
                Assert.That(mail, Is.Not.EqualTo(""));
                ctx.SubmitChanges();
            }

            //CacheController<Kistl.API.IDataObject>.Current.Clear();

            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            {
                var kunde = ctx.GetQuery<Kistl.App.Projekte.Kunde>().First(k => k.ID == ID);
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
            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            {
                var list = ctx.GetQuery<Kistl.App.Projekte.Kunde>();
                foreach (Kistl.App.Projekte.Kunde k in list)
                {
                    if (k.EMails.Count > 0)
                    {
                        mail = k.EMails.First();
                        k.EMails.Remove(mail);
                        mailCount = k.EMails.Count;
                        ID = k.ID;
                        break;
                    }
                }
                ctx.SubmitChanges();
            }

            //CacheController<Kistl.API.IDataObject>.Current.Clear();

            using (Kistl.API.IKistlContext ctx = KistlContext.GetContext())
            {
                var kunde = ctx.GetQuery<Kistl.App.Projekte.Kunde>().First(k => k.ID == ID);
                Assert.That(kunde, Is.Not.Null);
                Assert.That(kunde.EMails.Count, Is.GreaterThan(0));

                var result = kunde.EMails.SingleOrDefault(m => m == mail);
                Assert.That(kunde.EMails.Count, Is.EqualTo(mailCount));
                Assert.That(result, Is.Null);
            }
        }
    }
}
