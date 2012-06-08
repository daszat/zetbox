using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Kistl.App.Base;
using System.IO;

namespace Kistl.API.AbstractConsumerTests.Blobs
{
    public abstract class when_using : BlobFixture
    {
        [Test]
        public void should_be_created()
        {
            var id = ctx.CreateBlob(data, filename, mimetype);
            Assert.That(id, Is.GreaterThan(0));
        }

        [Test]
        public void should_not_be_created_trough_create()
        {
            Assert.That(() => ctx.Create<Blob>(), Throws.InstanceOf<InvalidOperationException>());
        }

        [Test]
        public void should_be_created_and_retreived()
        {
            var id = ctx.CreateBlob(data, filename2, mimetype);
            Assert.That(id, Is.GreaterThan(0));
            var blob = ctx.Find<Blob>(id);
            Assert.That(blob, Is.Not.Null);
            Assert.That(blob.OriginalName, Is.EqualTo(filename2));
            Assert.That(blob.MimeType, Is.EqualTo(mimetype));
        }

        [Test]
        public void should_be_deleted()
        {
            var blob = ctx.Find<Blob>(blob_id);
            Assert.That(blob, Is.Not.Null);
            ctx.Delete(blob);
            ctx.SubmitChanges();

            using (var test_ctx = GetContext())
            {
                var test_blob = test_ctx.GetQuery<Blob>().FirstOrDefault(o => o.ID == blob_id);
                Assert.That(test_blob, Is.Null);
            }
        }

        [Test]
        [ExpectedException()] // Any, since could be executed on server, client or integration tests
        public void should_not_be_changed()
        {
            var blob = ctx.Find<Blob>(blob_id);
            Assert.That(blob, Is.Not.Null);
            blob.OriginalName = "test.txt";
            ctx.SubmitChanges();
        }

        public void should_content_be_the_original_content()
        {
            var blob = ctx.Find<Blob>(blob_id);
            Assert.That(blob, Is.Not.Null);
            var s = blob.GetStream();
            var sr = new StreamReader(s);
            var txt = sr.ReadToEnd();
            Assert.That(txt, Is.EqualTo(txt_data));
        }
    }
}
