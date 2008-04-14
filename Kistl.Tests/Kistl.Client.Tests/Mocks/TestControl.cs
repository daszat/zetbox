using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.GUI.DB;
using Kistl.GUI;

namespace Kistl.Client.Mocks
{
    public class TestStringControl : IStringControl
    {
        public TestStringControl()
        {
            Description = "TestControl Description";
            ShortLabel = "TC ShortLabel";
            Size = FieldSize.Full;
        }

        public readonly static ControlInfo Info
            = new ControlInfo()
            {
                Platform = Toolkit.TEST,
                Control = "string",
                Container = false,
                AssemblyName = "Kistl.Client.Tests, Version=1.0.0.0",
                ClassName = "Kistl.Client.Mocks.TestStringControl"
            };


        #region IBasicControl Members

        public string Description { get; set; }
        public string ShortLabel { get; set; }
        public FieldSize Size { get; set; }

        #endregion

        #region IStringControl Members

        public event EventHandler UserInput;
        public string Value { get; set; }

        #endregion
    }

}
