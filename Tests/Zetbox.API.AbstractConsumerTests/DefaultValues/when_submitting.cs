
namespace Kistl.API.AbstractConsumerTests.DefaultValues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.App.Base;
    using NUnit.Framework;

    public abstract class when_submitting_without_read
        : AbstractTestFixture
    {
        [Test]
        public void should_persist_a_value()
        {
            var ctx = GetContext();
            var obj = ctx.Create<TypeRef>();
            obj.FullName = "TestRef";
            obj.Assembly = ctx.GetQuery<Assembly>().First();
            ctx.SubmitChanges();

            // read ExportGuid after submit to not influence datastore
            var originalExportGuid = obj.ExportGuid;
            var originalId = obj.ID;

            ctx = GetContext();
            obj = ctx.Find<TypeRef>(originalId);

            Assert.That(obj.ExportGuid, Is.EqualTo(originalExportGuid));
        }
    }

    public abstract class when_submitting_with_read
        : AbstractTestFixture
    {
        [Test]
        public void should_persist_a_value()
        {
            var ctx = GetContext();
            var obj = ctx.Create<TypeRef>();
            obj.FullName = "TestRef";
            obj.Assembly = ctx.GetQuery<Assembly>().First();

            // read ExportGuid before submit to test initialisation
            var originalExportGuid = obj.ExportGuid;

            ctx.SubmitChanges();

            var originalId = obj.ID;

            ctx = GetContext();
            obj = ctx.Find<TypeRef>(originalId);

            Assert.That(obj.ExportGuid, Is.EqualTo(originalExportGuid));
        }
    }
}
