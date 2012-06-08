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
using System.Linq;
using System.Text;

using Autofac;

using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Zetbox.Server.Tests
{
    [TestFixture]
    public class InheritanceTests : AbstractServerTestFixture
    {
        private IReadOnlyZetboxContext ctx;

        public override void SetUp()
        {
            base.SetUp();
            ctx = scope.Resolve<IReadOnlyZetboxContext>();
        }

        [Test]
        public void GetListOfInheritedObjects()
        {
            bool intFound = false;
            bool stringFound = false;

            var list = ctx.GetQuery<Property>().ToList();
            Assert.That(list.Count, Is.GreaterThan(0));

            foreach (Property bp in list)
            {
                if (bp is IntProperty)
                {
                    intFound = true;
                }
                if (bp is StringProperty)
                {
                    stringFound = true;
                }
            }

            Assert.That(intFound, Is.True);
            Assert.That(stringFound, Is.True);
        }

        [Test]
        [Ignore("no test class available")]
        public void UpdateInheritedObject()
        {
            //int ID = Zetbox.API.Helper.INVALIDID;
            //double? maxStungen = 0.0;

            //using (Zetbox.API.IZetboxContext ctx = GetContext())
            //{
            //    var list = ctx.GetQuery<Zetbox.App.TimeRecords.Kostentraeger>().ToList();
            //    Assert.That(list.Count, Is.GreaterThan(0));

            //    Zetbox.App.TimeRecords.Kostentraeger k = list[0];
            //    ID = k.ID;
            //    maxStungen = k.BudgetHours = k.BudgetHours.HasValue ? k.BudgetHours.Value + 1.0 : 1.0;

            //    ctx.SubmitChanges();
            //}

            //using (Zetbox.API.IZetboxContext ctx = GetContext())
            //{
            //    var k = ctx.GetQuery<Zetbox.App.TimeRecords.Kostentraeger>().First(o => o.ID == ID);
            //    Assert.That(k, Is.Not.Null);
            //    Assert.That(k.BudgetHours.HasValue, Is.True);
            //    Assert.That(k.BudgetHours.Value, Is.EqualTo(maxStungen));
            //}
        }
    }
}
