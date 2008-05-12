using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;

namespace API.Server.Tests
{
    [SetUpFixture]
    public class SetUp
    {
        [SetUp]
        public void Init()
        {
            System.Diagnostics.Trace.WriteLine("Setting up Kistl");

            Kistl.API.APIInit init = new Kistl.API.APIInit();
            init.Init(@"..\..\DefaultConfig_API.Server.Tests.xml");

            Kistl.API.ObjectType.Init("API.Server.Tests");

            Kistl.API.CustomActionsManagerFactory.Init(new CustomActionsManagerAPITest());

            System.Diagnostics.Trace.WriteLine("Setting up Kistl finished");
        }
    }
}
