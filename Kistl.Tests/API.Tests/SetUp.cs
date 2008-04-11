using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;

namespace API.Tests
{
    [SetUpFixture]
    public class SetUp
    {
        [SetUp]
        public void Init()
        {
            System.Diagnostics.Trace.WriteLine("Setting up Kistl");

            Kistl.API.APIInit init = new Kistl.API.APIInit();
            init.Init(@"..\..\DefaultConfig.xml");
            
            Kistl.API.ObjectType.AsClient();
            Kistl.API.CustomActionsManagerFactory.Init(new CustomActionsManagerAPITest());

            System.Diagnostics.Trace.WriteLine("Setting up Kistl finished");
        }
    }
}
