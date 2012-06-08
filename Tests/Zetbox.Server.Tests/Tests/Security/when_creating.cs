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

namespace Zetbox.Server.Tests.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.App.Projekte;

    using NUnit.Framework;

    [TestFixture]
    public class when_creating : SecurityDataFixture
    {
        [Test]
        public void project_should_throw_exception_with_no_privileges()
        {
            Assert.That(() => id3Ctx_low.Create<Projekt>(), Throws.InstanceOf<System.Security.SecurityException>());
        }

        [Test]
        public void project_should_be_created_by_identity()
        {
            var newProj = id1Ctx.Create<Projekt>();
            newProj.Name = "Test";
            id1Ctx.SubmitChanges();

            Assert.That(srvCtx.GetQuery<Projekt>().Count(), Is.EqualTo(projectCount + 1));
            Assert.That(id1Ctx.GetQuery<Projekt>().Count(), Is.EqualTo(id1ProjectCount + 1));
            Assert.That(id2Ctx.GetQuery<Projekt>().Count(), Is.EqualTo(id2ProjectCount));
            Assert.That(id3Ctx_low.GetQuery<Projekt>().Count(), Is.EqualTo(0));
        }

        [Test]
        public void project_should_be_created_by_admin()
        {
            var newProj = srvCtx.Create<Projekt>();
            newProj.Name = "Test";
            srvCtx.SubmitChanges();

            Assert.That(srvCtx.GetQuery<Projekt>().Count(), Is.EqualTo(projectCount + 1));
            Assert.That(id1Ctx.GetQuery<Projekt>().Count(), Is.EqualTo(id1ProjectCount));
            Assert.That(id2Ctx.GetQuery<Projekt>().Count(), Is.EqualTo(id2ProjectCount));
            Assert.That(id3Ctx_low.GetQuery<Projekt>().Count(), Is.EqualTo(0));
        }

        [Test]
        public void task_should_be_created_by_identity()
        {
            var newTask = id1Ctx.Create<Task>();
            newTask.Name = "Test";
            prj1.Tasks.Add(newTask);
            id1Ctx.SubmitChanges();

            Assert.That(srvCtx.GetQuery<Task>().Count(), Is.EqualTo(projectCount * task_projectCount + 1));
            Assert.That(id1Ctx.GetQuery<Task>().Count(), Is.EqualTo(id1ProjectCount * task_projectCount + 1));
            Assert.That(id2Ctx.GetQuery<Task>().Count(), Is.EqualTo(id2ProjectCount * task_projectCount));
            Assert.That(id3Ctx_low.GetQuery<Task>().Count(), Is.EqualTo(0));
        }

        [Test]
        public void task_should_be_created_by_admin()
        {
            var newTask = srvCtx.Create<Task>();
            newTask.Name = "Test";
            srvCtx.Find<Projekt>(prj1.ID).Tasks.Add(newTask);
            srvCtx.SubmitChanges();

            Assert.That(srvCtx.GetQuery<Task>().Count(), Is.EqualTo(projectCount * task_projectCount + 1));
            Assert.That(id1Ctx.GetQuery<Task>().Count(), Is.EqualTo(id1ProjectCount * task_projectCount + 1));
            Assert.That(id2Ctx.GetQuery<Task>().Count(), Is.EqualTo(id2ProjectCount * task_projectCount));
            Assert.That(id3Ctx_low.GetQuery<Task>().Count(), Is.EqualTo(0));
        }

    }
}
