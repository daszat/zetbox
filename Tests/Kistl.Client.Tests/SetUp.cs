using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.Client.Mocks;
using Kistl.GUI.DB;

using NMock2;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;

/// <summary>
/// Assembly-global setup class which initialises the Kistl.API and primes the KistlGUIContext with test data
/// </summary>
[SetUpFixture]
public class MainSetUp
{
    public static Mockery Mockery { get; private set; }
    public static IKistlContext MockContext { get; private set; }

    public static void RegisterObject(IDataObject obj)
    {
        Stub.On(MainSetUp.MockContext).
            Method("Find").
            With(obj.ID).
            Will(Return.Value(obj));
        Stub.On(MainSetUp.MockContext).
            Method("ContainsObject").
            With(obj.GetType(), obj.ID).
            Will(Return.Value(obj));
        obj.AttachToContext(MainSetUp.MockContext);
    }

    [SetUp]
    public void Init()
    {

        Kistl.Client.WPF.App app = new Kistl.Client.WPF.App();
        app.InitializeComponent();

        Mockery = new Mockery();
        MockContext = Mockery.NewMock<IKistlContext>("MockContext");

        System.Diagnostics.Trace.WriteLine("Setting up Kistl");

        Kistl.API.APIInit init = new Kistl.API.APIInit();
        init.Init(HostType.Client, @"..\..\DefaultConfig_Kistl.Client.Tests.xml");

        Kistl.API.CustomActionsManagerFactory.Init(new CustomActionsManagerAPITest());

        System.Diagnostics.Trace.WriteLine("Setting up Kistl finished");

        ControlInfo.Implementations.Add(TestObjectControl.Info);
        ControlInfo.Implementations.Add(TestObjectReferenceControl.Info);
        ControlInfo.Implementations.Add(TestObjectListControl.Info);

        ControlInfo.Implementations.Add(TestBoolControl.Info);
        ControlInfo.Implementations.Add(TestDateTimeControl.Info);
        ControlInfo.Implementations.Add(TestDoubleControl.Info);
        ControlInfo.Implementations.Add(TestIntControl.Info);
        ControlInfo.Implementations.Add(TestStringControl.Info);
        ControlInfo.Implementations.Add(TestRenderer.Info);

    }
}
