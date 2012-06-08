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
using System.IO;
using Zetbox.App.Base;

namespace Zetbox.API.AbstractConsumerTests.Blobs
{
    public abstract class BlobFixture : AbstractTestFixture
    {
        protected IZetboxContext ctx;
        protected MemoryStream data;
        protected static readonly string txt_data = "Hello Blob!\nI'm a test file";
        protected int blob_id;
        protected static readonly string mimetype = "text/plain";
        protected static readonly string filename = "hello.txt";
        protected static readonly string filename2 = "hello2.txt";

        [SetUp]
        public void InitTestObjects()
        {
            DeleteTestData();
            CreateTestData();

            ctx = GetContext();
        }

        protected virtual void DeleteTestData()
        {
            using (IZetboxContext ctx = GetContext())
            {
                ctx.GetQuery<at.dasz.DocumentManagement.Document>().ForEach(obj => obj.Revisions.Clear());
                ctx.GetQuery<at.dasz.DocumentManagement.File>().ForEach(obj => ctx.Delete(obj));
                // Can't delete all blobs - icons are using them
                // ctx.GetQuery<Zetbox.App.Base.Blob>().ForEach(obj => ctx.Delete(obj));
                ctx.SubmitChanges();
            }
        }

        protected virtual void CreateTestData()
        {
            data = new MemoryStream();
            var sw = new StreamWriter(data);
            sw.Write(txt_data);

            using (IZetboxContext ctx = GetContext())
            {
                var blob = ctx.Find<Blob>(ctx.CreateBlob(data, filename, mimetype));
                ctx.SubmitChanges();
                blob_id = blob.ID;
            }
        }

        [TearDown]
        public virtual void DisposeContext()
        {
            ctx.Dispose();
        }
    }
}
