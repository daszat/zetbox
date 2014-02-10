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
using System.IO;
using System.Linq;
using System.Text;
using doc = at.dasz.DocumentManagement;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Zetbox.App.Base;


namespace Zetbox.API.AbstractConsumerTests.Blobs
{
    public abstract class when_using : BlobFixture
    {
        [Test]
        public void should_not_be_created_trough_create()
        {
            Assert.That(() => ctx.Create<Blob>(), Throws.InstanceOf<InvalidOperationException>());
        }

        /// <summary>
        /// Hook to model the client/server differences. See CreateBlob comments.
        /// </summary>
        /// <returns></returns>
        protected abstract IResolveConstraint CreateBlobIdConstraint();

        [Test]
        public void should_be_created()
        {
            var id = ctx.CreateBlob(data, filename, mimetype);
            Assert.That(id, CreateBlobIdConstraint());
        }

        [Test]
        public void should_be_created_and_retreived()
        {
            var id = ctx.CreateBlob(data, filename2, mimetype);
            Assert.That(id, CreateBlobIdConstraint());
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

        [Test]
        public void should_content_be_the_original_content()
        {
            var blob = ctx.Find<Blob>(blob_id);
            Assert.That(blob, Is.Not.Null);
            var s = blob.GetStream();
            var sr = new StreamReader(s);
            var txt = sr.ReadToEnd();
            Assert.That(txt, Is.EqualTo(txt_data));
        }

        [Test]
        public void should_be_usable_as_File()
        {
            var staticFile = ctx.Create<doc.File>();
            staticFile.Blob = ctx.Find<Blob>(blob_id);
            ctx.SubmitChanges();

            var checkCtx = GetContext();
            var checkStaticFile = checkCtx.Find<doc.File>(staticFile.ID);
            Assert.That(checkStaticFile.Blob.ID, Is.EqualTo(blob_id));

            checkCtx.Delete(checkStaticFile);
            Assert.That(() => checkCtx.SubmitChanges(), Throws.Nothing);
        }

        [Test]
        public void should_be_usable_as_new_File()
        {
            var staticFile = ctx.Create<doc.File>();
            staticFile.Blob = ctx.Find<Blob>(blob_id);

            ctx.Delete(staticFile);
            Assert.That(() => ctx.SubmitChanges(), Throws.Nothing);
        }
    }
}
