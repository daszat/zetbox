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
using NUnit.Framework;
using Zetbox.App.Projekte;

namespace Zetbox.Server.Tests.Security
{
    [TestFixture]
    public class when_changing : SecurityDataFixture
    {
        [Test]
        public void should_refresh_rights_on_add_mitarbeiter()
        {
            Assert.That(id2Ctx.GetQuery<Projekt>().Count(), Is.EqualTo(id2ProjectCount));
            
            Assert.That(prj1.Mitarbeiter.Count, Is.EqualTo(1));
            prj1.Mitarbeiter.Add(id1Ctx.Find<Mitarbeiter>(ma2.ID));
            id1Ctx.SubmitChanges();
            Assert.That(prj1.Mitarbeiter.Count, Is.EqualTo(2));

            Assert.That(id2Ctx.GetQuery<Projekt>().Count(), Is.EqualTo(id2ProjectCount + 1));
            Assert.That(id2Ctx.GetQuery<Task>().Count(), Is.EqualTo((id2ProjectCount + 1) * task_projectCount));
        }

        [Test]
        [Ignore("Case 3013: Add trigger for security tables also on n_m tables")]
        public void should_refresh_rights_on_remove_mitarbeiter()
        {
            // Add
            Assert.That(id2Ctx.GetQuery<Projekt>().Count(), Is.EqualTo(id2ProjectCount));
            Assert.That(id2Ctx.GetQuery<Task>().Count(), Is.EqualTo(id2ProjectCount * task_projectCount));

            prj1.Mitarbeiter.Add(id1Ctx.Find<Mitarbeiter>(ma2.ID));
            id1Ctx.SubmitChanges();

            Assert.That(id2Ctx.GetQuery<Projekt>().Count(), Is.EqualTo(id2ProjectCount + 1));
            Assert.That(id2Ctx.GetQuery<Task>().Count(), Is.EqualTo((id2ProjectCount + 1) * task_projectCount));

            // And now the the remove
            prj1.Mitarbeiter.Remove(id1Ctx.Find<Mitarbeiter>(ma2.ID));
            id1Ctx.SubmitChanges();
            Assert.That(prj1.Mitarbeiter.Count, Is.EqualTo(1));

            Assert.That(id2Ctx.GetQuery<Projekt>().Count(), Is.EqualTo(id2ProjectCount));
            Assert.That(id2Ctx.GetQuery<Task>().Count(), Is.EqualTo(id2ProjectCount * task_projectCount));
        }
    }
}
