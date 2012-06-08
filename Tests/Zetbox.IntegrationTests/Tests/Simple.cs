
namespace Zetbox.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.AbstractConsumerTests;
    using Zetbox.App.Test;
    using NUnit.Framework;

    [Category("Smoke Tests")]
    public sealed class Simple
        : AbstractTestFixture
    {
        [SetUp]
        public void CleanTestDB()
        {
            var deleteCtx = scope.Resolve<IZetboxContext>();
            foreach (var obj in deleteCtx.GetQuery<ANewObjectClass>())
            {
                deleteCtx.Delete(obj);
            }
            foreach (var obj in deleteCtx.GetQuery<Muhblah>())
            {
                deleteCtx.Delete(obj);
            }
            foreach (var obj in deleteCtx.GetQuery<TestCustomObject>())
            {
                deleteCtx.Delete(obj);
            }
            deleteCtx.SubmitChanges();
        }

        [Test]
        public void CanWorkWithObjects()
        {
            {
                var ctx = scope.Resolve<IZetboxContext>();
                var item = ctx.Create<ANewObjectClass>();
                item.TestString = "TestName";
                ctx.SubmitChanges();
            }

            {
                var checkCtx = scope.Resolve<IZetboxContext>();
                var result = checkCtx.GetQuery<ANewObjectClass>().Single();
                Assert.That(result.TestString, Is.EqualTo("TestName"));
            }
        }

        [Test]
        public void CanWorkWithLists()
        {
            {
                var ctx = scope.Resolve<IZetboxContext>();
                var item = ctx.Create<Muhblah>();
                var member1 = ctx.Create<TestCustomObject>();
                member1.PersonName = "Person1";
                item.TestCustomObjects_List_Nav.Add(member1);
                var member2 = ctx.Create<TestCustomObject>();
                member2.PersonName = "Person2";
                item.TestCustomObjects_List_Nav.Add(member2);
                var member3 = ctx.Create<TestCustomObject>();
                member3.PersonName = "Person3";
                item.TestCustomObjects_List_Nav.Add(member3);
                ctx.SubmitChanges();
            }

            {
                var checkCtx = scope.Resolve<IZetboxContext>();
                var result = checkCtx.GetQuery<Muhblah>().Single();
                Assert.That(result.TestCustomObjects_List_Nav, Has.Count.EqualTo(3));
            }
        }

        [Test]
        public void CanWorkWithCompounds()
        {
            {
                var ctx = scope.Resolve<IZetboxContext>();
                var item = ctx.Create<TestCustomObject>();
                item.PersonName = "Person1";
                item.PhoneNumberMobile = ctx.CreateCompoundObject<TestPhoneCompoundObject>();
                item.PhoneNumberMobile.AreaCode = "43664";
                item.PhoneNumberMobile.Number = "664";
                ctx.SubmitChanges();
            }

            {
                var checkCtx = scope.Resolve<IZetboxContext>();
                var result = checkCtx.GetQuery<TestCustomObject>().Single();
                Assert.That(result.PhoneNumberMobile, Is.Not.Null);
                Assert.That(result.PhoneNumberMobile.Number, Is.EqualTo("664"));
            }
        }

        [Test]
        public void CanWorkWithCompoundLists()
        {
            {
                var ctx = scope.Resolve<IZetboxContext>();
                var item = ctx.Create<TestCustomObject>();
                item.PersonName = "Person1";
                {
                    var compound1 = ctx.CreateCompoundObject<TestPhoneCompoundObject>();
                    compound1.AreaCode = "43664";
                    compound1.Number = "664";
                    item.PhoneNumbersOther.Add(compound1);
                }
                {
                    var compound2 = ctx.CreateCompoundObject<TestPhoneCompoundObject>();
                    compound2.AreaCode = "436642";
                    compound2.Number = "6642";
                    item.PhoneNumbersOther.Add(compound2);
                }
                ctx.SubmitChanges();
            }

            {
                var checkCtx = scope.Resolve<IZetboxContext>();
                var result = checkCtx.GetQuery<TestCustomObject>().Single();
                Assert.That(result.PhoneNumbersOther, Is.Not.Null);
                Assert.That(result.PhoneNumbersOther, Has.Count.EqualTo(2));
                Assert.That(result.PhoneNumbersOther.Select(pn => pn.AreaCode).ToList(), Has.Member("43664"));
                Assert.That(result.PhoneNumbersOther.Select(pn => pn.AreaCode).ToList(), Has.Member("436642"));
            }
        }
    }
}
