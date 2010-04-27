using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;


using NUnit.Framework;
using Kistl.API.Client;
using Kistl.DalProvider.ClientObjects.Mocks;

namespace Kistl.DalProvider.ClientObjects.Tests
{
    [SetUpFixture]
    public class SetUp
    {

        [SetUp]
        public void Init()
        {
            Kistl.API.Configuration.KistlConfig.FromFile("Kistl.DalProvider.ClientObjects.Tests.Config.xml");
            var appCtx = new ClientApplicationContext();
            ProxySingleton.SetProxy(new ProxyMock());
        }

    }
}
