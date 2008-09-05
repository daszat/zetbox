using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Kistl.App.Base;
using Kistl.GUI.DB;
using Kistl.App.GUI;

namespace Kistl.GUI.Mocks
{
    /// <summary>
    /// Contains all setup code for testing a Control
    /// </summary>
    public class ControlHarness<CONTROL>
        where CONTROL : IBasicControl
    {

        public ControlHarness(Visual visual, Toolkit toolKit)
        {
            Toolkit = toolKit;
            Visual = visual;
        }

        public void SetUp()
        {
            ControlInfo = KistlGUIContext.FindControlInfo(Toolkit, Visual);
            Widget = (CONTROL)KistlGUIContext.CreateControl(ControlInfo);
        }

        public Visual Visual { get; private set; }
        public ControlInfo ControlInfo { get; private set; }
        public Toolkit Toolkit { get; private set; }
        public CONTROL Widget { get; private set; }

        public void TestSetUpCorrect()
        {
            Assert.IsNotNull(Visual, "visual should have been initialised");
            Assert.IsNotNull(ControlInfo, "cInfo should have been initialised");
            Assert.IsNotNull(Widget, "widget should have been initialised");
        }
    }
}
