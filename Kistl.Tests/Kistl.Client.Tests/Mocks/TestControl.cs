using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.GUI.DB;
using Kistl.GUI;
using Kistl.API;

namespace Kistl.Client.Mocks
{
    public class TestBackReferenceControl : IObjectListControl
    {
        public TestBackReferenceControl()
        {
            Description = "TestBackReferenceControl Description";
            ShortLabel = "TBRC ShortLabel";
            Size = FieldSize.Full;
            HasValidValue = true;
        }

        public readonly static ControlInfo Info
            = new ControlInfo()
            {
                Platform = Toolkit.TEST,
                Control = "list",
                Container = false,
                AssemblyName = "Kistl.Client.Tests, Version=1.0.0.0",
                ClassName = "Kistl.Client.Mocks.TestBackReferenceControl"
            };


        #region IBasicControl Members

        public string Description { get; set; }
        public string ShortLabel { get; set; }
        public FieldSize Size { get; set; }

        #endregion

        #region IBackReferenceControl Members

        public event EventHandler UserInput;
        public IList<IDataObject> Value { get; set; }

        public bool HasValidValue { get; set; }
        public void FlagValidity(bool valid)
        {
            HasValidValue = valid;
        }

        #endregion

    }

    public class TestBoolControl : IBoolControl
    {
        public TestBoolControl()
        {
            Description = "TestBoolControl Description";
            ShortLabel = "TBC ShortLabel";
            Size = FieldSize.Full;
            HasValidValue = true;
        }

        public readonly static ControlInfo Info
            = new ControlInfo()
            {
                Platform = Toolkit.TEST,
                Control = "bool",
                Container = false,
                AssemblyName = "Kistl.Client.Tests, Version=1.0.0.0",
                ClassName = "Kistl.Client.Mocks.TestBoolControl"
            };


        #region IBasicControl Members

        public string Description { get; set; }
        public string ShortLabel { get; set; }
        public FieldSize Size { get; set; }

        #endregion

        #region IBoolControl Members

        public event EventHandler UserInput;
        public bool? Value { get; set; }

        public bool HasValidValue { get; set; }
        public void FlagValidity(bool valid)
        {
            HasValidValue = valid;
        }

        #endregion

        internal void SimulateUserInput(bool? newBoolValue)
        {
            Value = newBoolValue;
            if (UserInput != null)
                UserInput(this, new EventArgs());
        }
    }

    public class TestDateTimeControl : IDateTimeControl
    {
        public TestDateTimeControl()
        {
            Description = "TestDateTimeControl Description";
            ShortLabel = "TBC ShortLabel";
            Size = FieldSize.Full;
            HasValidValue = true;
        }

        public readonly static ControlInfo Info
            = new ControlInfo()
            {
                Platform = Toolkit.TEST,
                Control = "date",
                Container = false,
                AssemblyName = "Kistl.Client.Tests, Version=1.0.0.0",
                ClassName = "Kistl.Client.Mocks.TestDateTimeControl"
            };


        #region IBasicControl Members

        public string Description { get; set; }
        public string ShortLabel { get; set; }
        public FieldSize Size { get; set; }

        #endregion

        #region IDateTimeControl Members

        public event EventHandler UserInput;
        public DateTime? Value { get; set; }

        public bool HasValidValue { get; set; }
        public void FlagValidity(bool valid)
        {
            HasValidValue = valid;
        }

        #endregion

        internal void SimulateUserInput(DateTime? newDateTimeValue)
        {
            Value = newDateTimeValue;
            if (UserInput != null)
                UserInput(this, new EventArgs());
        }
    }

    public class TestIntControl : IIntControl
    {
        public TestIntControl()
        {
            Description = "TestIntControl Description";
            ShortLabel = "TIC ShortLabel";
            Size = FieldSize.Full;
            HasValidValue = true;
        }

        public readonly static ControlInfo Info
            = new ControlInfo()
            {
                Platform = Toolkit.TEST,
                Control = "int",
                Container = false,
                AssemblyName = "Kistl.Client.Tests, Version=1.0.0.0",
                ClassName = "Kistl.Client.Mocks.TestIntControl"
            };


        #region IBasicControl Members

        public string Description { get; set; }
        public string ShortLabel { get; set; }
        public FieldSize Size { get; set; }

        #endregion

        #region IIntControl Members

        public event EventHandler UserInput;
        public int? Value { get; set; }

        public bool HasValidValue { get; set; }
        public void FlagValidity(bool valid)
        {
            HasValidValue = valid;
        }

        #endregion

        internal void SimulateUserInput(int? newIntValue)
        {
            Value = newIntValue;
            if (UserInput != null)
                UserInput(this, new EventArgs());
        }
    }

    public class TestStringControl : IStringControl
    {
        public TestStringControl()
        {
            Description = "TestStringControl Description";
            ShortLabel = "TSC ShortLabel";
            Size = FieldSize.Full;
            HasValidValue = true;
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

        public bool HasValidValue { get; set; }
        public void FlagValidity(bool valid)
        {
            HasValidValue = valid;
        }

        #endregion

        internal void SimulateUserInput(string newStringValue)
        {
            Value = newStringValue;
            if (UserInput != null)
                UserInput(this, new EventArgs());
        }
    }

}
