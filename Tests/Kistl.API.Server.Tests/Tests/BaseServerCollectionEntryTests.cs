using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API.Tests.Skeletons;
using Kistl.API.Server.Mocks;

using NUnit.Framework;

namespace Kistl.API.Server.Tests
{

    [TestFixture]
    public class BaseServerCollectionEntryTests : CollectionEntryTests<TestObjClass_TestNameCollectionEntry__Implementation__>
    {
        public override void SetUp()
        {
            var testCtx = new ServerApiContextMock();

            base.SetUp();
        }
    }

}
