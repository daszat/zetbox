using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.GUI.DB;

namespace Kistl.Client.Controls
{
    public class TestControl
    {
        public readonly static ControlInfo Info
            = new ControlInfo()
            {
                Platform = Toolkit.WPF,
                Control = "test",
                Container = false,
                AssemblyName = "Kistl.Client.Tests, Version=1.0.0.0",
                ClassName = "TestControl"
            };

    }
}
