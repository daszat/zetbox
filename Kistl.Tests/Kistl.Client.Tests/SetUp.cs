using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.GUI.DB;
using Kistl.Client.Mocks;

/// <summary>
/// Assembly-global setup class which initialises the Kistl.API and primes the KistlGUIContext with test data
/// </summary>
[SetUpFixture]
public class SetUp
{
    [SetUp]
    public void Init()
    {
        System.Diagnostics.Trace.WriteLine("Setting up Kistl");

        Kistl.API.APIInit init = new Kistl.API.APIInit();
        init.Init(@"..\..\DefaultConfig_Kistl.Client.Tests.xml");

        Kistl.API.ObjectType.Init("API.Client.Tests");

        Kistl.API.CustomActionsManagerFactory.Init(new CustomActionsManagerAPITest());

        System.Diagnostics.Trace.WriteLine("Setting up Kistl finished");

        ControlInfo.Implementations.Add(TestObjectReferenceControl.Info);
        ControlInfo.Implementations.Add(TestBackReferenceControl.Info);
        ControlInfo.Implementations.Add(TestBoolControl.Info);
        ControlInfo.Implementations.Add(TestDateTimeControl.Info);
        ControlInfo.Implementations.Add(TestDoubleControl.Info);
        ControlInfo.Implementations.Add(TestIntControl.Info);
        ControlInfo.Implementations.Add(TestStringControl.Info);

    }
}
