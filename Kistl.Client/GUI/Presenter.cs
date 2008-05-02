using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;
using Kistl.API;
using Kistl.API.Client;
using System.Collections;
using Kistl.GUI.DB;

namespace Kistl.GUI
{

    public abstract class Presenter
    {
        /// <summary>
        /// This method initializes the PropertyPresenter.
        /// MUST be called before any other operation
        /// </summary>
        public void InitializeComponent(Kistl.API.IDataObject obj, Visual v, IBasicControl ctrl)
        {
            if (obj == null)
                throw new ArgumentNullException("obj", "obj==null cannot be presented");

            if (v == null)
                throw new ArgumentNullException("v", "object without visual cannot be presented");

            if (ctrl == null)
                throw new ArgumentNullException("ctrl", "visual without control cannot be presented");

            _Object = obj;
            _Preferences = v;
            _Control = ctrl;

            InitializeComponent();
        }

        /// <summary>
        /// internal setup routine, linking the UI Elements with the object
        /// </summary>
        protected abstract void InitializeComponent();

        private Kistl.API.IDataObject _Object;
        public Kistl.API.IDataObject Object { get { return _Object; } }

        private Visual _Preferences;
        public Visual Preferences { get { return _Preferences; } }

        private IBasicControl _Control;
        public IBasicControl Control { get { return _Control; } }

    }

    public class DefaultPresenter<TYPE, PROPERTY> : Presenter
        where PROPERTY : Property
    {
        public DefaultPresenter() { }

        protected override void InitializeComponent()
        {
            Control.ShortLabel = Property.PropertyName;
            Control.Description = Property.AltText;
            Control.Value = Object.GetPropertyValue<TYPE>(Property.PropertyName);
            // Control.Size = Preferences.PreferredSize;
            Control.Size = FieldSize.Full;

            Control.UserInput += new EventHandler(Control_UserInput);
        }

        private void Control_UserInput(object sender, EventArgs e)
        {
            Control.IsValidValue = (Property.IsNullable || Control.Value != null);
            if (Control.IsValidValue)
            {
                Object.SetPropertyValue<TYPE>(Property.PropertyName, Control.Value);
            }
        }

        // localize type-unsafety
        public PROPERTY Property { get { return (PROPERTY)Preferences.Property; } }
        // fixup locally used types
        public new IValueControl<TYPE> Control { get { return (IValueControl<TYPE>)base.Control; } }

    }

    public class BoolPresenter : DefaultPresenter<bool?, BoolProperty> { }
    public class DateTimePresenter : DefaultPresenter<DateTime?, DateTimeProperty> { }
    public class DoublePresenter : DefaultPresenter<double?, DoubleProperty> { }
    public class IntPresenter : DefaultPresenter<int?, IntProperty> { }
    public class StringPresenter : DefaultPresenter<string, StringProperty> { }

    /// <summary>
    /// Handles a widget for a group of property
    /// </summary>
    public class GroupPresenter : Presenter
    {
        public GroupPresenter() { }

        protected override void InitializeComponent()
        {
            Control.ShortLabel = String.Format("{0} {1}", Object.Type.Classname, Object.ID);
        }
    }

    /// <summary>
    /// Handles the top-level widget for an object
    /// </summary>
    public class ObjectPresenter : Presenter
    {
        public ObjectPresenter() { }

        protected override void InitializeComponent()
        {
            Control.ShortLabel = Object.Type.Classname;
            Control.Description = Object.Type.Classname;
            Control.Value = Object;
            // Control.Size = Preferences.PreferredSize;
            Control.Size = FieldSize.Full;
        }

        // fixup locally used types
        public new IObjectControl Control { get { return (IObjectControl)base.Control; } }
    }

    public class PointerPresenter : Presenter
    {
        public PointerPresenter() { }

        protected override void InitializeComponent()
        {
            Control.ShortLabel = Property.PropertyName;
            Control.Description = Property.AltText;
            Control.ObjectType = new Kistl.API.ObjectType(Property.GetDataType());
            Control.ItemsSource = Object.Context.GetQuery(Control.ObjectType).ToList();
            Control.TargetID = Object.GetPropertyValue(Property);
            // Control.Size = Preferences.PreferredSize;
            Control.Size = FieldSize.Full;

            Control.UserInput += new EventHandler(Control_TargetIDChanged);
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

    public class ObjectListPresenter : Presenter
    {
        public ObjectListPresenter() { }

        protected override void InitializeComponent()
        {
            Control.ShortLabel = Property.PropertyName;
            Control.Description = Property.AltText;
            // Control.ObjectType = new Kistl.API.ObjectType(Property.GetDataType());
            Control.Value = Object.GetList(Property);
            // Control.Size = Preferences.PreferredSize;
            Control.Size = FieldSize.Full;

            Control.UserInput += new EventHandler(Control_UserInput);
        }

        private void Control_UserInput(object sender, EventArgs e)
        {
            IList<Kistl.API.IDataObject> list = Object.GetList(Property);
            list.Clear();
            foreach (var i in Control.Value)
            {
                list.Add(i);
            }
        }

        // localize type-unsafety
        public ObjectReferenceProperty Property { get { return (ObjectReferenceProperty)Preferences.Property; } }
        // fixup locally used types
        public new IObjectListControl Control { get { return (IObjectListControl)base.Control; } }
    }

    public class BackReferencePresenter : Presenter
    {
        public BackReferencePresenter() { }

        protected override void InitializeComponent()
        {
            Control.ShortLabel = Property.PropertyName;
            Control.Description = Property.AltText;
            Control.Value = Object.GetPropertyValue<IEnumerable>(Property.PropertyName).OfType<IDataObject>().ToList();
            // Control.Size = Preferences.PreferredSize;
            Control.Size = FieldSize.Full;
        }

        // localize type-unsafety
        public BackReferenceProperty Property { get { return (BackReferenceProperty)Preferences.Property; } }
        // fixup locally used types
        public new IObjectListControl Control { get { return (IObjectListControl)base.Control; } }

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

    public interface IValueControl<TYPE> : IBasicControl
    {
        /// <summary>
        /// The actually displayed value
        /// </summary>
        TYPE Value { get; set; }

        /// <summary>
        /// Whether the displayed value is valid. This is set by the Presenter, 
        /// when Value changes.
        /// </summary>
        bool IsValidValue { get; set; }

        /// <summary>
        /// This event is triggered after UserInput. The Presenter will then 
        /// fetch the Value and do Validity checks.
        /// </summary>
        event /*UserInput<TYPE>*/EventHandler UserInput;
    }

    public interface ISelectControl : IBasicControl
    {
        IEnumerable ItemsSource { get; set; }
    }

    public interface IPointerControl : ISelectControl
    {
        /// <summary>
        /// The ObjectType of the listed Objects
        /// </summary>
        ObjectType ObjectType { get; set; }
        int TargetID { get; set; }
        event /*UserInput<int>*/EventHandler UserInput;
    }

    public interface IObjectControl : IBasicControl
    {
        Kistl.API.IDataObject Value { get; set; }
        event /*UserInput<Kistl.API.IDataObject>*/EventHandler UserInput;
    }

    public interface IObjectListControl : IBasicControl
    {
        IList<Kistl.API.IDataObject> Value { get; set; }
        event /*UserInput<IList<Kistl.API.IDataObject>>*/EventHandler UserInput;
    }

    /// <summary>
    /// Some extension functions to help with accessing the Objects
    /// </summary>
    public static class ExtensionHelper
    {
        public static string GetPropertyValue(this IDataObject obj, StringProperty prop)
        {
            return obj.GetPropertyValue<string>(prop.PropertyName);
        }

        public static void SetPropertyValue(this IDataObject obj, StringProperty prop, string value)
        {
            obj.SetPropertyValue<string>(prop.PropertyName, value);
        }

        public static double? GetPropertyValue(this IDataObject obj, DoubleProperty prop)
        {
            return obj.GetPropertyValue<double?>(prop.PropertyName);
        }

        public static void SetPropertyValue(this IDataObject obj, DoubleProperty prop, double? value)
        {
            obj.SetPropertyValue<double?>(prop.PropertyName, value);
        }

        public static int? GetPropertyValue(this IDataObject obj, IntProperty prop)
        {
            return obj.GetPropertyValue<int?>(prop.PropertyName);
        }

        public static void SetPropertyValue(this IDataObject obj, IntProperty prop, int? value)
        {
            obj.SetPropertyValue<int?>(prop.PropertyName, value);
        }

        public static DateTime? GetPropertyValue(this IDataObject obj, DateTimeProperty prop)
        {
            return obj.GetPropertyValue<DateTime?>(prop.PropertyName);
        }

        public static void SetPropertyValue(this IDataObject obj, DateTimeProperty prop, DateTime? value)
        {
            obj.SetPropertyValue<DateTime?>(prop.PropertyName, value);
        }

        public static bool? GetPropertyValue(this IDataObject obj, BoolProperty prop)
        {
            return obj.GetPropertyValue<bool?>(prop.PropertyName);
        }

        public static void SetPropertyValue(this IDataObject obj, BoolProperty prop, bool? value)
        {
            obj.SetPropertyValue<bool?>(prop.PropertyName, value);
        }

        public static int GetPropertyValue(this IDataObject obj, ObjectReferenceProperty prop)
        {
            return obj.GetPropertyValue<int>("fk_" + prop.PropertyName);
        }

        public static void SetPropertyValue(this IDataObject obj, ObjectReferenceProperty prop, int value)
        {
            obj.SetPropertyValue<int>("fk_" + prop.PropertyName, value);
        }

        public static IList<Kistl.API.IDataObject> GetList(this IDataObject obj, ObjectReferenceProperty prop)
        {
            return obj.GetPropertyValue<IEnumerable>(prop.PropertyName).OfType<IDataObject>().ToList();
        }

        public static void SetList(this IDataObject obj, ObjectReferenceProperty prop, IList<Kistl.API.IDataObject> value)
        {
            obj.SetPropertyValue<IList<Kistl.API.IDataObject>>(prop.PropertyName, value);
        }
    }

}
