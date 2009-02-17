using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using Kistl.API.Server.Mocks;
using Kistl.API.Tests.Skeletons;
using Kistl.App.Projekte;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Kistl.DalProvider.EF.Tests
{
    [TestFixture]
    public class BaseServerCollectionEntryTests : CollectionEntryTests<Kunde_EMailsCollectionEntry__Implementation__>
    {
        public override void SetUp()
        {
            var testCtx = new ServerApiContextMock();

            base.SetUp();
        }
    }

}
