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
