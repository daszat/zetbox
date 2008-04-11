using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.Client;

namespace Integration.Tests
{
    [SetUpFixture]
    public class SetUp
    {
        private Client client;

        [SetUp]
        public void Init()
        {
            System.Diagnostics.Trace.WriteLine("Setting up Kistl");

            Kistl.API.APIInit init = new Kistl.API.APIInit();
            init.Init(@"..\..\DefaultConfig.xml");

            client = new Client();
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
