
namespace Kistl.API.AbstractConsumerTests.ContextTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Test;
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
    }
}
