using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API.Client.Mocks;

using NUnit.Framework;

namespace Kistl.DalProvider.ClientObjects.Tests
{
    [SetUpFixture]
    public class SetUp
    {

        [SetUp]
        public void Init()
        {
            var appCtx = new ClientApplicationContextMock();
        }

    }
}
