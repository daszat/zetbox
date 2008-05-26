using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Base;
using Kistl.GUI.DB;

namespace Kistl.GUI
{
    public interface IPresenter
    {
        void InitializeComponent(Kistl.API.IDataObject obj, Visual v, IBasicControl ctrl);
    }

    /// <summary>
    /// This abstract class implements IPresenter and IDisposable in a typs-safe, minimalist way. 
    /// Override the various provided hooks to actually implement functionality.
    /// </summary>
    /// <typeparam name="CONTROL">The type of the actual control which is used for display.</typeparam>
    public abstract class Presenter<CONTROL> : IPresenter, IDisposable
        where CONTROL : IBasicControl
    {

        /// <summary>
        /// This method initializes the PropertyPresenter.
        /// MUST be called before any other operation
        /// </summary>
        public void InitializeComponent(Kistl.API.IDataObject obj, Visual v, CONTROL ctrl)
        {
            if (_IsInitialised)
                throw new InvalidOperationException("Presenter was already initialised!");

            if (obj == null)
                throw new ArgumentNullException("obj", "obj==null cannot be presented");

            if (v == null)
                throw new ArgumentNullException("v", "object without visual cannot be presented");

            if (ctrl == null)
                throw new ArgumentNullException("ctrl", "visual without control cannot be presented");

            _IsInitialised = true;

            Object = obj;
            Preferences = v;
            Control = ctrl;

            InitializeComponent();

        }

        private bool _IsInitialised = false;

        /// <summary>
        /// internal setup routine, linking the UI Elements with the object
        /// </summary>
        protected abstract void InitializeComponent();

        public Kistl.API.IDataObject Object { get; private set; }

        public Visual Preferences { get; private set; }

        public CONTROL Control { get; private set; }

        #region IPresenter Members

        /// <summary>
        /// Implement IPresenter. Fails fast if given IBasicControl doesn't match the Presenter's expectations.
        /// </summary>
        void IPresenter.InitializeComponent(Kistl.API.IDataObject obj, Visual v, IBasicControl ctrl)
        {
            InitializeComponent(obj, v, (CONTROL)ctrl);
        }

        #endregion

        #region IDisposable Members

        // as suggested on http://msdn.microsoft.com/en-us/system.idisposable.aspx
        // adapted for easier usage when inheriting
        private bool disposed = false;
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // dispose managed resources
                    // must not be done when running from the finalizer
                    DisposeManagedResources();
                }
                // free native resources
                DisposeNativeResources();

                this.disposed = true;
            }
        }

        /// <summary>
        /// Override this to be called when Managed Resources should be disposed
        /// </summary>
        protected virtual void DisposeManagedResources() { }
        /// <summary>
        /// Override this to be called when Native Resources should be disposed
        /// </summary>
        protected virtual void DisposeNativeResources() { }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Presenter()
        {
            Dispose(false);
        }

        #endregion

    }

    /// <summary>
    /// The default presenter has infrastructure for setting IBasicControl and IValueControl members.
    /// Additionally basic change handling is implemented.
    /// </summary>
    /// <typeparam name="TYPE">The type of the handled Property's value.</typeparam>
    /// <typeparam name="PROPERTY">The type of the handled Property.</typeparam>
    /// <typeparam name="CONTROL">The type of the actual control which is used for display.</typeparam>
    public class DefaultPresenter<TYPE, PROPERTY, CONTROL> : Presenter<CONTROL>
        where PROPERTY : Property
        where CONTROL : IValueControl<TYPE>
    {
        // localize type-unsafety
        /// <summary>
        /// The Property this Presenter presents.
        /// </summary>
        public PROPERTY Property { get { return (PROPERTY)Preferences.Property; } }

        /// <summary>
        /// Override this to specify a conversion from the object's property to the value for the widget
        /// </summary>
        /// <returns>the value of the handled property in the right type for the widget</returns>
        protected virtual TYPE GetPropertyValue()
        {
            return Object.GetPropertyValue<TYPE>(Property.PropertyName);
        }

        #region Initialisation

        /// <summary>
        /// Setup the Control with default values from the Property. 
        /// Install EventHandlers for UserInput and PropertyChanged.
        /// </summary>
        protected override void InitializeComponent()
        {
            Control.ShortLabel = Property.PropertyName;
            Control.Description = Property.AltText;

            // To prevent resource leaks, all event handlers have to be removed
            // See DisposeManagedResources()
            {
                _Object_PropertyChanged = new System.ComponentModel.PropertyChangedEventHandler(Object_PropertyChanged);
                Object.PropertyChanged += _Object_PropertyChanged;

                _Control_UserInput = new EventHandler(Control_UserInput);
                Control.UserInput += _Control_UserInput;
            }

            Control.Value = GetPropertyValue();
            Control.IsValidValue = true;

            // Control.Size = Preferences.PreferredSize;
            Control.Size = FieldSize.Full;
        }

        #endregion

        #region Change Management

        private PropertyChangedEventHandler _Object_PropertyChanged = null;
        private EventHandler _Control_UserInput = null;

        private void Object_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
                if (sender != Object)
                    throw new InvalidOperationException(String.Format("Resource Leak: _Object_PropertyChanged in '{0}' was called by '{1}', but should be attached to '{2}'",
                        this, sender, Object));

                // if the object has changed, unconditionally overwrite the value in the GUI
                // intelligent controls might want to show the user both the "real" and the user's value
                if (e.PropertyName == Property.PropertyName)
                    Control.Value = GetPropertyValue();
            }
        }

        private void Control_UserInput(object sender, EventArgs e)
        {
            OnUserInput();
        }

        /// <summary>
        /// this method is called when the control submits userinput back to the presenter.
        /// the default implementation checks whether the value conforms to the nullability
        /// criteria of the Property and sets the validness of the control's value and the 
        /// property value as appropriate.
        /// 
        /// override the method to gain fine-grained control over the presenter's reaction 
        /// to user input.
        /// </summary>
        // TODO: hook up Validation here and re-check all overrider.
        protected virtual void OnUserInput()
        {
            Control.IsValidValue = (Property.IsNullable || Control.Value != null);
            if (Control.IsValidValue)
            {
                Object.SetPropertyValue<TYPE>(Property.PropertyName, Control.Value);
            }
        }

        #endregion

        #region Disposal

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();

            // To prevent resource leaks, all event handlers have to be removed
            if (Object != null)
                Object.PropertyChanged -= _Object_PropertyChanged;

            if (Control != null)
                Control.UserInput -= _Control_UserInput;
        }

        #endregion
    }

    #region Default Value Presenters

    /// <summary>
    /// The default presenter has infrastructure for setting IBasicControl and IValueControl members.
    /// Additionally basic change handling is implemented.
    /// 
    /// This derivation is only a shortcut to always specifying IValueControl&lt;TYPE&gt; as CONTROL.
    /// </summary>
    /// <typeparam name="TYPE">The type of the handled Property's value.</typeparam>
    /// <typeparam name="PROPERTY">The type of the handled Property.</typeparam>
    public class DefaultValuePresenter<TYPE, PROPERTY> : DefaultPresenter<TYPE, PROPERTY, IValueControl<TYPE>>
        where PROPERTY : Property
    {
    }

    public class BoolPresenter : DefaultValuePresenter<bool?, BoolProperty> { }
    public class DateTimePresenter : DefaultValuePresenter<DateTime?, DateTimeProperty> { }
    public class DoublePresenter : DefaultValuePresenter<double?, DoubleProperty> { }
    public class IntPresenter : DefaultValuePresenter<int?, IntProperty> { }
    public class StringPresenter : DefaultValuePresenter<string, StringProperty> { }

    #endregion

    /// <summary>
    /// Handles a widget for a group of properties
    /// </summary>
    public class GroupPresenter : Presenter<IBasicControl>
    {
        public GroupPresenter() { }

        protected override void InitializeComponent()
        {
            Control.ShortLabel = String.Format("{0} {1}", Object.Type.Classname, Object.ID);

            //TODO: Set Control.Size
        }
    }

    /// <summary>
    /// Handles the top-level widget for an object
    /// </summary>
    public class ObjectPresenter : Presenter<IObjectControl>
    {
        public ObjectPresenter() { }

        protected override void InitializeComponent()
        {
            Control.ShortLabel = Object.Type.Classname;
            Control.Description = String.Format("{0}: {1}", Object.Type.Classname, Object.ToString());
            Control.Value = Object;
            // Control.Size = Preferences.PreferredSize;
            Control.Size = FieldSize.Full;
        }

    }

    /// <summary>
    /// Presents a reference to another IDataObject of a given Type.
    /// The list of selectable Items is set from the list of available objects.
    /// Different controls may implement the selection differently: Comboboxes, Lists, etc.
    /// </summary>
    public class ObjectReferencePresenter
        : DefaultPresenter<IDataObject, ObjectReferenceProperty, IObjectReferenceControl>
    {
        public ObjectReferencePresenter() { }

        private List<IDataObject> _Items;

        protected override void InitializeComponent()
        {
            Control.ObjectType = new Kistl.API.ObjectType(Property.ReferenceObjectClass.Module.Namespace, Property.ReferenceObjectClass.ClassName);

            // remember the objects that are sent to the object
            // to facilitate validity checking
            Control.ItemsSource = _Items = Object.Context.GetQuery(Control.ObjectType).ToList();

            base.InitializeComponent();
        }

        /// <summary>
        /// Checks whether the returned value matches the Properties nullability 
        /// criteria and is one of the originally selectable items.
        /// </summary>
        protected override void OnUserInput()
        {
            if (Control.Value == null)
            {
                Control.IsValidValue = Property.IsNullable;
            }
            else
            {
                Control.IsValidValue = _Items.Contains(Control.Value);
            }


            if (Control.IsValidValue)
            {
                Object.SetPropertyValue(Property.PropertyName, Control.Value);
            }
        }
    }

    public class ObjectListPresenter
        : DefaultPresenter<IList<IDataObject>, ObjectReferenceProperty, IObjectListControl>
    {
        public ObjectListPresenter() { }

        private List<IDataObject> _Items;

        protected override void InitializeComponent()
        {
            Control.ObjectType = new Kistl.API.ObjectType(Property.ReferenceObjectClass.Module.Namespace, Property.ReferenceObjectClass.ClassName);

            // remember the objects that are sent to the object
            // to facilitate validity checking
            Control.ItemsSource = _Items = Object.Context.GetQuery(Control.ObjectType).ToList();

            base.InitializeComponent();
        }

        /// <summary>
        /// Checks whether the returned value matches the Properties nullability 
        /// criteria and is one of the originally selectable items.
        /// </summary>
        protected override void OnUserInput()
        {
            if (Control.Value == null)
            {
                Control.IsValidValue = Property.IsNullable;
            }
            else
            {
                Control.IsValidValue = Control.Value.All(v => _Items.Contains(v));
            }


            if (Control.IsValidValue)
            {
                Object.SetPropertyValue(Property.PropertyName, Control.Value);
            }
        }
    }

    public class BackReferencePresenter
        : Presenter<IObjectListControl>
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
    }

    /// <summary>
    /// Possible sizes for controls. These are measured realtively 
    /// to total horizontal space up to a useful (renderer-, medium- 
    /// and user-dependant maximum)
    /// </summary>
    public enum FieldSize
    {
        OneThird,
        Half,
        TwoThird,
        Full,
        FitContent
    }

    /// <summary>
    /// This interfaces contains properties that each Control has to provide.
    /// </summary>
    public interface IBasicControl
    {
        /// <summary>
        /// A short label describing the contents of this control.
        /// </summary>
        string ShortLabel { get; set; }
        /// <summary>
        /// A longer description of the contents of this control. 
        /// This is a suitable text for tooltips.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// The preferred display size of this control.
        /// </summary>
        FieldSize Size { get; set; }
    }

    public interface IObjectControl : IBasicControl
    {
        Kistl.API.IDataObject Value { get; set; }
        event /*UserInput<Kistl.API.IDataObject>*/EventHandler UserInput;
    }

    public interface IValueControl<TYPE> : IBasicControl
    {
        /// <summary>
        /// The actually displayed value. Setting the Value here does not count 
        /// as user-input.
        /// 
        /// If you want to be notified of all changes of the value, use the 
        /// appropriate events on the model.
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

    public interface IReferenceControl<TYPE> : IValueControl<TYPE>
    {
        /// <summary>
        /// The ObjectType of the listed objects
        /// </summary>
        ObjectType ObjectType { get; set; }
        IList<IDataObject> ItemsSource { get; set; }
    }

    public interface IObjectReferenceControl : IReferenceControl<IDataObject>
    {
    }

    public interface IObjectListControl : IReferenceControl<IList<IDataObject>>
    {
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
