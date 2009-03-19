using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API.Client.Mocks;
using Kistl.API.Tests.Skeletons;

using NUnit.Framework;

namespace Kistl.API.Client.Tests
{
    [TestFixture]
    public class BaseClientCollectionEntryTests : CollectionEntryTests<TestObjClass_TestNameCollectionEntry>
    {
        public override void SetUp()
        {
            var testCtx = new ClientApplicationContextMock();

            base.SetUp();
        }
    }

}
