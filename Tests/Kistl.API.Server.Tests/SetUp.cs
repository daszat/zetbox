using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;

namespace Kistl.API.Server.Tests
{
    [SetUpFixture]
    public class SetUp
    {
        [SetUp]
        public void Init()
        {
            System.Diagnostics.Trace.WriteLine("Setting up Kistl");

            Kistl.API.APIInit init = new Kistl.API.APIInit();
            init.Init(HostType.Server, @"DefaultConfig_API.Server.Tests.xml");
            Kistl.API.APIInit.ImplementationAssembly = this.GetType().Assembly.FullName;

            Kistl.API.CustomActionsManagerFactory.Init(new CustomActionsManagerAPITest());

            System.Diagnostics.Trace.WriteLine("Setting up Kistl finished");
        }
    }
}
