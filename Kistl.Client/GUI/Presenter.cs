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
using System.Reflection;
using Kistl.Client;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace Kistl.GUI
{
    public interface IPresenter
    {
        void InitializeComponent(Kistl.API.IDataObject obj, Visual v, IBasicControl ctrl);
    }

    /// <summary>
    /// This abstract class implements IPresenter and IDisposable in a type-safe, minimalist way. 
    /// Override the various provided hooks to actually implement functionality.
    /// </summary>
    /// <typeparam name="CONTROL">The type of the actual control which is used for display.</typeparam>
    public abstract class Presenter<CONTROL> : IPresenter, IDisposable
        where CONTROL : IBasicControl
    {

        /// <summary>
        /// This method initializes the PropertyPresenter.
        /// MUST be called exactly once AND before any other operation
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
        // adapted for easier usage when inheriting, by naming the functions appropriately
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

    public class StringListPresenter : Presenter<IListControl<string>>
    {
        public IList<string> GetPropertyValue()
        {
            return Object.GetPropertyValue<IList<string>>(Preferences.Property.PropertyName);
        }
        protected override void InitializeComponent()
        {
            Object.PropertyChanged += new PropertyChangedEventHandler(Object_PropertyChanged);
            Control.UserChangedList += new NotifyCollectionChangedEventHandler(Control_UserChangedList);
        }

        private void Control_UserChangedList(object sender, NotifyCollectionChangedEventArgs args)
        {
            IList<string> property = GetPropertyValue();
            // Modify the Object's Value according to the changes in the Control
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    property.InsertRange(args.NewStartingIndex, args.NewItems);
                    break;
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Move:
                    foreach (var i in args.OldItems)
                        property.RemoveAt(args.OldStartingIndex);
                    property.InsertRange(args.NewStartingIndex, args.NewItems);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var i in args.OldItems)
                        property.RemoveAt(args.OldStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    property.Clear();
                    Control.Values.Cast<string>().ForEach<string>(i => property.Add(i));
                    break;
                default:
                    throw new NotImplementedException(
                        String.Format("Unknown NotifyCollectionChangedAction: {0}", args.Action));
            }
#if DEBUG
            // Check whether the Presenter has synced the two lists correctly
            if (property.Count != Control.Values.Count)
            {
                throw new InvalidOperationException("Error when synchronising Values in ObjectListPresenter: number of elements mismatch");
            }
            for (int i = 0; i < property.Count; i++)
            {
                if (!property[i].Equals(Control.Values[i]))
                {
                    throw new InvalidOperationException("Error when synchronising Values in ObjectListPresenter: element mismatch");
                }
            }
#endif

        }


        void Object_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Control.Values.Clear();
            foreach (string s in GetPropertyValue())
            {
                Control.Values.Add(s);
            }
        }
    }

    #region Default Value Presenters

    /// <summary>
    /// The default presenter has infrastructure for setting IBasicControl and IValueControl members.
    /// Additionally basic change handling is implemented.
    /// 
    /// This derivation is only a shortcut to always specifying IValueControl&lt;TYPE?&gt; as CONTROL.
    /// </summary>
    /// <typeparam name="TYPE">The type of the handled Property's value.</typeparam>
    /// <typeparam name="PROPERTY">The type of the handled Property.</typeparam>
    public class DefaultStructPresenter<TYPE> : DefaultPresenter<TYPE?, ValueTypeProperty, IValueControl<TYPE?>>
        where TYPE : struct
    {
    }

    /// <summary>
    /// The default presenter has infrastructure for setting IBasicControl and IValueControl members.
    /// Additionally basic change handling is implemented.
    /// 
    /// This derivation is only a shortcut to always specifying IValueControl&lt;TYPE&gt; as CONTROL.
    /// </summary>
    /// <typeparam name="TYPE">The type of the handled Property's value.</typeparam>
    /// <typeparam name="PROPERTY">The type of the handled Property.</typeparam>
    public class DefaultValuePresenter<TYPE> : DefaultPresenter<TYPE, ValueTypeProperty, IValueControl<TYPE>>
        where TYPE : class
    {
    }



    #endregion

    /// <summary>
    /// Handles a widget for a group of properties
    /// </summary>
    public class GroupPresenter : Presenter<IBasicControl>
    {
        public GroupPresenter() { }

        protected override void InitializeComponent()
        {
            Control.ShortLabel = String.Format("{0} {1}", Object.GetType().Name, Object.ID);

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
            SetLabels();
            Control.Value = Object;
            Control.UserSaveRequest += new EventHandler(Control_UserSaveRequest);
            Control.UserDeleteRequest += new EventHandler(Control_UserDeleteRequest);
            Object.PropertyChanged += new PropertyChangedEventHandler(Object_PropertyChanged);
        }

        private void SetLabels()
        {
            Control.ShortLabel = Object.ToString(); // Object.GetType().Name;
            Control.Description = String.Format("{0}: {1}", Object.GetType().Name, Object.ToString());
            // TODO: Control.Size = Preferences.PreferredSize;
            Control.Size = FieldSize.Full;
        }

        #region Event Handler

        private void Control_UserDeleteRequest(object sender, EventArgs e)
        {
            // TODO: Die Kontextfrage klären
            Object.Context.Delete(Object);
            Object.Context.SubmitChanges();
        }

        private void Control_UserSaveRequest(object sender, EventArgs e)
        {
            // TODO: Die Kontextfrage klären
            Object.Context.SubmitChanges();
        }

        private void Object_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SetLabels();
        }

        #endregion

    }

    public class WorkspacePresenter : IPresenter
    {
        private static int _workspaceCount = 0;

        protected IWorkspaceControl Control { get; set; }
        protected virtual void InitializeComponent(IWorkspaceControl iWorkspaceControl)
        {
            Control = iWorkspaceControl;

            Control.ShortLabel = String.Format("Workspace {0}", ++_workspaceCount);
            Control.Description = "All objects in this Workspace will be saved together";

            Control.UserSaveRequest += new EventHandler(Control_UserSaveRequest);
            Control.UserAbortRequest += new EventHandler(Control_UserAbortRequest);
            Control.UserNewObjectRequest += new EventHandler(Control_UserNewObjectRequest);
        }

        #region Behaviours

        /// <summary>
        /// Is called when the window wants to save all work.
        /// </summary>
        protected virtual void OnSave()
        {
            Control.Context.SubmitChanges();
        }

        /// <summary>
        /// Is called when the window wants to throwaway all work.
        /// </summary>
        protected virtual void OnAbort()
        {
            if (Control.Context != null)
                Control.Context.Dispose();
        }

        protected virtual void OnNew()
        {
            ObjectClass klass = Manager.Renderer.ChooseObject<ObjectClass>(Control.Context);
            if (klass != null)
            {
                Kistl.API.IDataObject newObject = Control.Context.Create(klass.GetDataCLRType());

                var template = newObject.FindTemplate(TemplateUsage.EditControl);
                IObjectControl ctrl = (IObjectControl)Manager.Renderer.CreateControl(newObject, template.VisualTree);

                Control.ShowObject(newObject, ctrl);
            }
        }

        #endregion

        #region Event Handlers

        void Control_UserSaveRequest(object sender, EventArgs e)
        {
            OnSave();
        }

        void Control_UserAbortRequest(object sender, EventArgs e)
        {
            OnAbort();
        }

        void Control_UserNewObjectRequest(object sender, EventArgs e)
        {
            OnNew();
        }

        #endregion

        #region IPresenter Member

        public void InitializeComponent(IDataObject obj, Visual v, IBasicControl ctrl)
        {
            InitializeComponent((IWorkspaceControl)ctrl);
        }

        #endregion

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

        public static IList<T> GetList<T>(this IDataObject obj, ObjectReferenceProperty prop)
            where T : IDataObject
        {
            return obj.GetPropertyValue<IList<T>>(prop.PropertyName);
        }

        public static IList<T> GetList<T>(this IDataObject obj, BackReferenceProperty prop)
            where T : IDataObject
        {
            return obj.GetPropertyValue<IList<T>>(prop.PropertyName);
        }
    }
}
