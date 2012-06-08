using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Zetbox.API.Tests.Skeletons;
using Zetbox.API.Server.Mocks;

using NUnit.Framework;

namespace Zetbox.API.Server.Tests
{

    [TestFixture]
    public class BaseServerCollectionEntryTests
        : CollectionEntryTests<TestObjClass_TestNameCollectionEntryImpl>
    {
    }

}
