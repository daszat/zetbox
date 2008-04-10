using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;
using Kistl.API;
using Kistl.API.Client;
using System.Collections;

namespace Kistl.GUI
{
    public static class ExtensionHelper
    {
        public static string GetPropertyValue(this BaseClientDataObject obj, StringProperty prop)
        {
            return obj.GetPropertyValue<string>(prop.PropertyName);
        }

        public static void SetPropertyValue(this BaseClientDataObject obj, StringProperty prop, string value)
        {
            obj.SetPropertyValue<string>(prop.PropertyName, value);
        }

        public static DateTime? GetPropertyValue(this BaseClientDataObject obj, DateTimeProperty prop)
        {
            return obj.GetPropertyValue<DateTime?>(prop.PropertyName);
        }

        public static void SetPropertyValue(this BaseClientDataObject obj, DateTimeProperty prop, DateTime? value)
        {
            obj.SetPropertyValue<DateTime?>(prop.PropertyName, value);
        }

        public static int GetPropertyValue(this BaseClientDataObject obj, ObjectReferenceProperty prop)
        {
            return obj.GetPropertyValue<int>("fk_" + prop.PropertyName);
        }

        public static void SetPropertyValue(this BaseClientDataObject obj, ObjectReferenceProperty prop, int value)
        {
            obj.SetPropertyValue<int>("fk_" + prop.PropertyName, value);
        }
    }

    public abstract class Presenter
    {
        /// <summary>
        /// This method initializes the PropertyPresenter.
        /// MUST be called before any other operation
        /// </summary>
        public void InitializeComponent(BaseClientDataObject obj, Visual v, IBasicControl ctrl)
        {
            if (obj == null)
                throw new ArgumentNullException("null object can't be presented");

            if (v == null)
                throw new ArgumentNullException("object without visual cannot be presented");

            if (ctrl == null)
                throw new ArgumentNullException("visual without control cannot be presented");

            _Object = obj;
            _Preferences = v;
            _Control = ctrl;

            InitializeComponent();
        }

        /// <summary>
        /// internal setup routine, linking the UI Elements with the object
        /// </summary>
        protected abstract void InitializeComponent();

        private BaseClientDataObject _Object;
        public BaseClientDataObject Object { get { return _Object; } }

        private Visual _Preferences;
        public Visual Preferences { get { return _Preferences; } }

        private IBasicControl _Control;
        public IBasicControl Control { get { return _Control; } }

    }

    public class StringPresenter : Presenter
    {
        public StringPresenter() { }

        protected override void InitializeComponent()
        {
            Control.ShortLabel = Property.PropertyName;
            Control.Description = Property.AltText;
            Control.Value = Object.GetPropertyValue(Property);
            // Control.Size = Preferences.PreferredSize;
            Control.Size = FieldSize.Full;

            Control.ValueChanged += new EventHandler(Control_ValueChanged);
        }

        private void Control_ValueChanged(object sender, EventArgs e)
        {
            Object.SetPropertyValue(Property, Control.Value);
        }

        // localize type-unsafety
        public StringProperty Property { get { return (StringProperty)Preferences.Property; } }
        // fixup locally used types
        public new IStringControl Control { get { return (IStringControl)base.Control; } }

    }

    public class DateTimePresenter : Presenter
    {
        public DateTimePresenter() { }

        protected override void InitializeComponent()
        {
            Control.ShortLabel = Property.PropertyName;
            Control.Description = Property.AltText;
            Control.Value = Object.GetPropertyValue(Property);
            // Control.Size = Preferences.PreferredSize;
            Control.Size = FieldSize.Full;

            Control.ValueChanged += new EventHandler(Control_ValueChanged);
        }

        private void Control_ValueChanged(object sender, EventArgs e)
        {
            Object.SetPropertyValue(Property, Control.Value);
        }

        // localize type-unsafety
        public DateTimeProperty Property { get { return (DateTimeProperty)Preferences.Property; } }
        // fixup locally used types
        public new IDateTimeControl Control { get { return (IDateTimeControl)base.Control; } }

    }

    public class PointerPresenter : Presenter
    {
        public PointerPresenter() { }

        protected override void InitializeComponent()
        {
            Control.ShortLabel = Property.PropertyName;
            Control.Description = Property.AltText;
            Control.ObjectType = Property.ReferenceObjectClass.Type;
            Control.ItemsSource = Object.Context.GetQuery(Object.Type).ToList();
            Control.TargetID = Object.GetPropertyValue(Property);
            // Control.Size = Preferences.PreferredSize;
            Control.Size = FieldSize.Full;

            Control.TargetIDChanged += new EventHandler(Control_TargetIDChanged);
        }

        private void Control_TargetIDChanged(object sender, EventArgs e)
        {
            Object.SetPropertyValue(Property, Control.TargetID);
        }

        // localize type-unsafety
        public ObjectReferenceProperty Property { get { return (ObjectReferenceProperty)Preferences.Property; } }
        // fixup locally used types
        public new IPointerControl Control { get { return (IPointerControl)base.Control; } }

    }

    public enum FieldSize
    {
        OneThird,
        Half,
        TwoThird,
        Full,
        FitContent
    }

    public interface IBasicControl
    {
        string ShortLabel { get; set; }
        string Description { get; set; }
        FieldSize Size { get; set; }
    }

    public interface IStringControl : IBasicControl
    {
        string Value { get; set; }
        event /*StringValueChanged*/EventHandler ValueChanged;
    }

    public interface IDateTimeControl : IBasicControl
    {
        DateTime? Value { get; set; }
        event /*DateTimeValueChanged*/EventHandler ValueChanged;
    }

    public interface IPointerControl : IBasicControl
    {
        /// <summary>
        /// The ObjectType of the Target
        /// </summary>
        ObjectType ObjectType { get; set; }
        IEnumerable ItemsSource { get; set; } 
        int TargetID { get; set; }
        event /*DateTimeValueChanged*/EventHandler TargetIDChanged;
    }
}
