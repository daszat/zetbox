using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Kistl.API.Client.Tests
{
    [SetUpFixture]
    public class SetUp
    {
        [SetUp]
        public void Init()
        {
            System.Diagnostics.Trace.WriteLine("Setting up Kistl");
/*
            Kistl.API.APIInit init = new Kistl.API.APIInit();
            init.Init(Kistl.API.HostType.Client, @"..\..\DefaultConfig_API.Client.Tests.xml");
            Kistl.API.APIInit.ImplementationAssembly = this.GetType().Assembly.FullName;
            Kistl.API.APIInit.InterfaceAssembly = this.GetType().Assembly.FullName;

            Kistl.API.CustomActionsManagerFactory.Init(new CustomActionsManagerAPITest());
            */
            System.Diagnostics.Trace.WriteLine("Setting up Kistl finished");
        }
    }
}
