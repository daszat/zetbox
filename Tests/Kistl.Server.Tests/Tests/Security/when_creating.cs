using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Kistl.App.Projekte;

namespace Kistl.Server.Tests.Security
{
    [TestFixture]
    public class when_creating : SecurityDataFixture
    {
        [Test]
        [ExpectedException(typeof(System.Security.SecurityException))]
        public void project_should_throw_exception_with_no_privileges()
        {
            var newProj = id3Ctx_low.Create<Projekt>();
            newProj.Name = "Test";
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

            Assert.That(srvCtx.GetQuery<Projekt>().Count(), Is.EqualTo(projectCount));
            Assert.That(id1Ctx.GetQuery<Projekt>().Count(), Is.EqualTo(id1ProjectCount + 1));
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
