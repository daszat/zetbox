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

namespace Zetbox.API.AbstractConsumerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Test;

    using NUnit.Framework;

    public abstract class AbstractReadonlyListPropertiesTests
        : AbstractTestFixture
    {
        [Test]
        public void navigating_a_fk_list_property_should_yield_related_objects()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var cls = ctx.GetQuery<ObjectClass>().Where(o => o.Name == "TestObjClass").First();
                var navigatedList = cls.Properties;

                Assume.That(navigatedList.Count, Is.GreaterThan(0));

                var manualList = ctx.GetQuery<Property>().Where(t => t.ObjectClass.ID == cls.ID).ToList();
                Assert.That(navigatedList, Is.EquivalentTo(manualList));
            }
        }

        [Test]
        [Ignore("Needs N:M relation between frozen objects with navigators in both directions")]
        public void navigating_a_nm_list_property_should_yield_related_objects()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var cls = ctx.GetQuery<ObjectClass>().Where(o => o.Name == "TestObjClass").First();
                var navigatedList = cls.ImplementsInterfaces;

                Assume.That(navigatedList.Count, Is.GreaterThan(0));

                //var manualList = ctx.GetQuery<Interface>().Where(t => t.ImplementedBy.Contains(cls)).ToList();
                //Assert.That(navigatedList, Is.EquivalentTo(manualList));
            }
        }

        [Test]
        public void retrieving_a_fk_list_twice_should_yield_same_list()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var prj1 = ctx.GetQuery<ObjectClass>().Where(o => o.Name == "TestObjClass").ToList().Single();
                var list1 = prj1.Properties;
                Assume.That(list1.Count, Is.GreaterThan(0));

                var prj2 = ctx.GetQuery<ObjectClass>().Where(o => o.Name == "TestObjClass").ToList().Single();
                var list2 = prj2.Properties;

                Assert.That(list2, Is.SameAs(list1));
            }
        }

        [Test]
        public void retrieving_a_nm_list_twice_should_yield_same_list()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var prj1 = ctx.GetQuery<ObjectClass>().Where(o => o.Name == "TestObjClass").ToList().Single();
                var list1 = prj1.ImplementsInterfaces;
                Assume.That(list1.Count, Is.GreaterThan(0));

                var prj2 = ctx.GetQuery<ObjectClass>().Where(o => o.Name == "TestObjClass").ToList().Single();
                var list2 = prj2.ImplementsInterfaces;

                Assert.That(list2, Is.SameAs(list1));
            }
        }

        [Test]
        public void list_and_query_should_yield_consistent_results()
        {
            var ctx = GetContext();

            var oneSide = ctx.Create<One_to_N_relations_One>();
            var name = oneSide.Name = "oneSide." + new Random().Next();

            var nSide1 = ctx.Create<One_to_N_relations_N>();
            nSide1.Name = "nSide1";
            nSide1.OneSide = oneSide;
            var nSide2 = ctx.Create<One_to_N_relations_N>();
            nSide2.Name = "nSide2";
            nSide2.OneSide = oneSide;

            ctx.SubmitChanges();

            // test cross-navigator queries
            var ns = ctx.GetQuery<One_to_N_relations_N>().Where(t => t.OneSide.Name == name).ToList();
            Assert.That(ns, Is.EquivalentTo(oneSide.NSide), "mismatch of query and navigator");
            Assert.That(ns.Select(t => t.OneSide), Has.All.EqualTo(oneSide), "NSide has wrong parent OneSide");

            var one = ctx.GetQuery<One_to_N_relations_One>().Where(p => p.Name == name).ToList().SingleOrDefault();
            Assert.That(one, Is.Not.Null);
        }


        // check whether a IsOrdered list's items are ordered correctly
        // not a very "good" test due to its dependency on "real" data
        [Test]
        public void list_should_be_sorted_correctly()
        {
            using (IZetboxContext ctx = GetContext())
            {
                ObjectClass cls = ctx.GetQuery<ObjectClass>().Where(c => c.Name == "Constraint").ToList().Single();
                Method isValid = cls.Methods.Where(m => m.Name == "IsValid").ToList().Single();

                AssertThatIsCorrectParameterOrder(isValid.Parameter);

                var resultOfEnumeration = isValid.Parameter.AsEnumerable().ToList();
                AssertThatIsCorrectParameterOrder(resultOfEnumeration);
            }
        }

        private static void AssertThatIsCorrectParameterOrder(IList<BaseParameter> parameter)
        {
            Assert.That(parameter[0].IsReturnParameter, Is.True, "return parameter should be first");
            Assert.That(parameter[1].Name, Is.EqualTo("constrainedObject"));
            Assert.That(parameter[2].Name, Is.EqualTo("constrainedValue"));
        }

    }

    // TODO: create Test object with value list
    public abstract class AbstractListPropertiesTests
        : AbstractReadonlyListPropertiesTests
    {
    //    [Test]
    //    public void AddStringListPropertyContent()
    //    {
    //        int ID = Zetbox.API.Helper.INVALIDID;
    //        string mail = String.Empty;
    //        using (IZetboxContext ctx = GetContext())
    //        {
    //            var k = ctx.GetQuery<Kunde>().First();
    //            Assert.That(k.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
    //            mail = "UnitTest" + DateTime.Now + "@example.com";
    //            ID = k.ID;
    //            k.EMails.Add(mail);
    //            //Assert.That(k.ObjectState, Is.EqualTo(DataObjectState.Modified));
    //            Assert.That(mail, Is.Not.EqualTo(String.Empty));
    //            Assert.That(ctx.SubmitChanges(), Is.GreaterThan(0));
    //        }

    //        using (IZetboxContext ctx = GetContext())
    //        {
    //            var kunde = ctx.Find<Kunde>(ID);
    //            Assert.That(kunde, Is.Not.Null);
    //            Assert.That(kunde.EMails.Count, Is.GreaterThan(0));
    //            var result = kunde.EMails.SingleOrDefault(m => m == mail);
    //            Assert.That(result, Is.Not.Null);
    //        }
    //    }

    //    [Test]
    //    public void UpdateStringListPropertyContent()
    //    {
    //        int ID = Zetbox.API.Helper.INVALIDID;
    //        string mail = String.Empty;
    //        using (IZetboxContext ctx = GetContext())
    //        {
    //            var list = ctx.GetQuery<Kunde>();
    //            bool set = false;
    //            foreach (Kunde k in list)
    //            {
    //                if (k.EMails.Count > 0)
    //                {
    //                    Assert.That(k.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
    //                    mail = "UnitTest" + DateTime.Now + "@example.com";
    //                    // TODO: Set IsSorted on Kunde.EMails
    //                    //k.EMails[0] = mail;
    //                    k.EMails.Clear();
    //                    k.EMails.Add(mail);
    //                    ID = k.ID;
    //                    set = true;
    //                    //Assert.That(k.ObjectState, Is.EqualTo(DataObjectState.Modified));
    //                    break;
    //                }
    //            }
    //            Assert.That(set, "No usable test object found");
    //            Assert.That(mail, Is.Not.EqualTo(String.Empty));
    //            Assert.That(ctx.SubmitChanges(), Is.GreaterThan(0));
    //        }

    //        using (IZetboxContext ctx = GetContext())
    //        {
    //            var kunde = ctx.Find<Kunde>(ID);
    //            Assert.That(kunde, Is.Not.Null);
    //            Assert.That(kunde.EMails.Count, Is.GreaterThan(0));
    //            var result = kunde.EMails.SingleOrDefault(m => m == mail);
    //            Assert.That(result, Is.Not.Null);
    //        }
    //    }

    //    [Test]
    //    public void DeleteStringListPropertyContent()
    //    {
    //        int ID = Zetbox.API.Helper.INVALIDID;
    //        int mailCount = 0;
    //        string mail = String.Empty;
    //        using (IZetboxContext ctx = GetContext())
    //        {
    //            var list = ctx.GetQuery<Kunde>();
    //            foreach (Kunde k in list)
    //            {
    //                if (k.EMails.Count > 2)
    //                {
    //                    Assert.That(k.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
    //                    mail = k.EMails.First();
    //                    k.EMails.Remove(mail);
    //                    mailCount = k.EMails.Count;
    //                    ID = k.ID;
    //                    //Assert.That(k.ObjectState, Is.EqualTo(DataObjectState.Modified));
    //                    break;
    //                }
    //            }
    //            Assert.That(ctx.SubmitChanges(), Is.GreaterThan(0));
    //        }

    //        using (IZetboxContext ctx = GetContext())
    //        {
    //            var kunde = ctx.Find<Kunde>(ID);
    //            Assert.That(kunde, Is.Not.Null);
    //            Assert.That(kunde.EMails.Count, Is.GreaterThan(0));
    //            Assert.That(kunde.EMails.Count, Is.EqualTo(mailCount));

    //            var result = kunde.EMails.SingleOrDefault(m => m == mail);
    //            Assert.That(result, Is.Null);
    //        }
    //    }
    }
}
