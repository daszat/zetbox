using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using Kistl.App.Base;

namespace Kistl.API.AbstractConsumerTests.Blobs
{
    public abstract class BlobFixture : AbstractTestFixture
    {
        protected IKistlContext ctx;
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
            using (IKistlContext ctx = GetContext())
            {
                ctx.GetQuery<at.dasz.DocumentManagement.Document>().ForEach(obj => obj.Revisions.Clear());
                ctx.GetQuery<at.dasz.DocumentManagement.File>().ForEach(obj => ctx.Delete(obj));
                // Can't delete all blobs - icons are using them
                // ctx.GetQuery<Kistl.App.Base.Blob>().ForEach(obj => ctx.Delete(obj));
                ctx.SubmitChanges();
            }
        }

        protected virtual void CreateTestData()
        {
            data = new MemoryStream();
            var sw = new StreamWriter(data);
            sw.Write(txt_data);

            using (IKistlContext ctx = GetContext())
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
