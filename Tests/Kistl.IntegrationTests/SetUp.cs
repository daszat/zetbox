using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.Client;

namespace Kistl.IntegrationTests
{
    [SetUpFixture]
    public class SetUp
    {
        private Kistl.Client.Client client;

        [SetUp]
        public void Init()
        {
            System.Diagnostics.Trace.WriteLine("Setting up Kistl");

            Kistl.API.APIInit init = new Kistl.API.APIInit();
            init.Init(Kistl.API.HostType.Client, @"..\..\DefaultConfig_Integration.Tests.xml");

            client = new Kistl.Client.Client();
            client.Start();

            System.Diagnostics.Trace.WriteLine("Setting up Kistl finished");
        }

        [TearDown]
        public void TearDown()
        {
            System.Diagnostics.Trace.WriteLine("Shutting down Kistl");
            try
            {
                // Das geht immer noch nicht
                client.Stop();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.ToString());
            }
            client = null;
            System.Diagnostics.Trace.WriteLine("Shutting down Kistl finished");
        }
    }
}
