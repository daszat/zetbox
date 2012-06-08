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
using Autofac;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Projekte;

namespace Zetbox.Server.Tests.Security
{
    [TestFixture]
    public class when_selected : SecurityDataFixture
    {
        [Test]
        public void project_count_should_be_correct()
        {
            Assert.That(srvCtx.GetQuery<Projekt>().Count(), Is.EqualTo(projectCount));
            Assert.That(id1Ctx.GetQuery<Projekt>().Count(), Is.EqualTo(id1ProjectCount));
            Assert.That(id2Ctx.GetQuery<Projekt>().Count(), Is.EqualTo(id2ProjectCount));
            Assert.That(id3Ctx_low.GetQuery<Projekt>().Count(), Is.EqualTo(0));
        }

        [Test]
        public void task_count_should_be_correct()
        {
            Assert.That(srvCtx.GetQuery<Task>().Count(), Is.EqualTo(projectCount * task_projectCount));
            Assert.That(id1Ctx.GetQuery<Task>().Count(), Is.EqualTo(id1ProjectCount * task_projectCount));
            Assert.That(id2Ctx.GetQuery<Task>().Count(), Is.EqualTo(id2ProjectCount * task_projectCount));
            Assert.That(id3Ctx_low.GetQuery<Task>().Count(), Is.EqualTo(0));
        }
    }
}
