using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Kistl.App.Projekte;

namespace Kistl.Server.Tests.Security
{
    [TestFixture]
    public class when_changing : SecurityDataFixture
    {
        [Test]
        [Ignore("Not implemented yet")]
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
        [Ignore("Not implemented yet")]
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
