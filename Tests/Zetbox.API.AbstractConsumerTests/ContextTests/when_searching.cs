
namespace Zetbox.API.AbstractConsumerTests.ContextTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Test;
    using NUnit.Framework;

    public class when_searching
        : AbstractTestFixture
    {
        [Test]
        public void multiple_guids_should_return_all_objects()
        {
            var ctx = GetContext();
            var objs = ctx.GetQuery<ObjectClass>().Take(10).ToList();
            var guids = objs.Select(cls => cls.ExportGuid);

            ctx = GetContext();

            var foundGuids = ctx.FindPersistenceObjects<ObjectClass>(guids).ToList().Select(cls => cls.ExportGuid);
            Assert.That(foundGuids, Is.EquivalentTo(guids));
        }

        [Test]
        public void multiple_guids_should_not_return_nulls()
        {
            var ctx = GetContext();
            var objs = ctx.GetQuery<ObjectClass>().Take(10).ToList();
            var guids = objs.Select(cls => cls.ExportGuid);
            var lookupGuids = guids.Concat(new[] { Guid.Empty }); // add illegal value

            ctx = GetContext();

            var foundObjs = ctx.FindPersistenceObjects<ObjectClass>(lookupGuids).ToList();
            Assert.That(foundObjs, Has.No.Member(null), "FindPersistenceObjects returned a null entry");

            var foundGuids = foundObjs.Select(cls => cls.ExportGuid);
            Assert.That(foundGuids, Is.EquivalentTo(guids));
        }

        [Test]
        [Ignore("Case 2686: implement client-side Skip/Take")]
        public void and_skipping()
        {
            var ctx = GetContext();
            var baseQuery = ctx.GetQuery<ObjectClass>().OrderBy(o => o.ID);
            var objs = baseQuery.Take(10).ToList();
            Assert.That(objs.Count, Is.EqualTo(10), "Did not receive correct amount of objects after Take(10)");
            var firstHalf = baseQuery.Take(5).ToList();
            Assert.That(firstHalf.Count, Is.EqualTo(5), "Did not receive correct amount of objects after Take(5)");
            var secondHalf = baseQuery.Skip(5).Take(5).ToList();
            Assert.That(secondHalf.Count, Is.EqualTo(5), "Did not receive correct amount of objects after Skip(5).Take(5)");

            Assert.That(firstHalf[0], Is.SameAs(objs[0]), "0");
            Assert.That(firstHalf[1], Is.SameAs(objs[1]), "1");
            Assert.That(firstHalf[2], Is.SameAs(objs[2]), "2");
            Assert.That(firstHalf[3], Is.SameAs(objs[3]), "3");
            Assert.That(firstHalf[4], Is.SameAs(objs[4]), "4");
            Assert.That(secondHalf[0], Is.SameAs(objs[5]), "5");
            Assert.That(secondHalf[1], Is.SameAs(objs[6]), "6");
            Assert.That(secondHalf[2], Is.SameAs(objs[7]), "7");
            Assert.That(secondHalf[3], Is.SameAs(objs[8]), "8");
            Assert.That(secondHalf[4], Is.SameAs(objs[9]), "9");
        }
    }
}
