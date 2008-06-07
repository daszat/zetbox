using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NMock2;
using NUnit.Framework;

using Kistl.App.Base;
using Kistl.API;
using Kistl.Client.Mocks;
using Kistl.GUI.DB;

namespace Kistl.GUI.Mocks
{
    public class PresenterHarness<OBJECT, CONTROL, PRESENTER>
        where OBJECT : IDataObject
        where CONTROL : IBasicControl
        where PRESENTER : IPresenter
    {

        public PresenterHarness(ObjectHarness<OBJECT> objectHarness, Type propertyType, ControlHarness<CONTROL> controlHarness)
        {
            ObjectHarness = objectHarness;
            ControlHarness = controlHarness;
            PropertyType = propertyType;
        }

        public void SetUp()
        {
            PresenterInfo = KistlGUIContext.FindPresenterInfo(ControlHarness.Visual, PropertyType);
            Presenter = (PRESENTER)KistlGUIContext.CreatePresenter(PresenterInfo, ObjectHarness.Instance, ControlHarness.Visual, ControlHarness.Widget);
        }

        public ControlHarness<CONTROL> ControlHarness { get; private set; }
        public ObjectHarness<OBJECT> ObjectHarness { get; private set; }
        public Type PropertyType { get; private set; }

        public PresenterInfo PresenterInfo { get; private set; }
        public PRESENTER Presenter { get; private set; }

        public void TestSetUpCorrect()
        {
            Assert.IsNotNull(ControlHarness, "ControlHarness should have been initialised");
            ControlHarness.TestSetUpCorrect();

            Assert.IsNotNull(ObjectHarness, "ObjectHarness should have been initialised");
            ObjectHarness.TestSetUpCorrect();

            Assert.IsNotNull(PresenterInfo, "pInfo should have been initialised");
            Assert.IsNotNull(Presenter, "presenter should have been initialised");
        }
    }
}
