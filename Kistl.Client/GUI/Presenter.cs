using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Base;
using Kistl.App.GUI;
using Kistl.Client;
using Kistl.GUI.DB;

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
    /// <typeparam name="PROPERTYTYPE">The type of the handled Property's value.</typeparam>
    /// <typeparam name="PROPERTY">The type of the handled Property.</typeparam>
    /// <typeparam name="CONTROLTYPE">The type which is handled by the control.</typeparam>
    /// <typeparam name="CONTROL">The type of the actual control which is used for display.</typeparam>
    public abstract class DefaultPresenter<PROPERTYTYPE, PROPERTY, CONTROLTYPE, CONTROL> : Presenter<CONTROL>
        where PROPERTY : Property
        where CONTROL : IValueControl<CONTROLTYPE>
    {
        // localize type-unsafety
        /// <summary>
        /// The Property this Presenter presents.
        /// </summary>
        public PROPERTY Property { get { return (PROPERTY)Preferences.Property; } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The Property's value</returns>
        protected PROPERTYTYPE GetPropertyValue()
        {
            return Object.GetPropertyValue<PROPERTYTYPE>(Property.PropertyName);
        }

        protected void SetPropertyValue(PROPERTYTYPE value)
        {
            Object.SetPropertyValue<PROPERTYTYPE>(Property.PropertyName, value);
        }

        /// <summary>
        /// This method converts the Value read from the Control into the type used by the Property
        /// </summary>
        protected abstract PROPERTYTYPE MungeFromControl(CONTROLTYPE value);

        /// <summary>
        /// This method converts the Value read from the Property into the type used by the Control
        /// </summary>
        protected abstract CONTROLTYPE MungeFromObject(PROPERTYTYPE value);


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

            SetControlValueFromObject();

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
                    SetControlValueFromObject();
            }
        }

        private void SetControlValueFromObject()
        {
            var objValue = GetPropertyValue();

            // Always set value from object to reflect "reality", even if the 
            // value is currently not valid. This is needed to show 
            // intermediate results.
            Control.Value = MungeFromObject(objValue);

            Control.IsValidValue = Property.Constraints.All(c => c.IsValid(objValue));
            if (Control.IsValidValue)
            {
                Control.Error = null;
            }
            else
            {
                Control.Error = String.Join("\n",
                    Property.Constraints.Where(c => !c.IsValid(objValue)).Select(c => c.GetErrorText(objValue)).ToArray());
            }
        }

        private void SetPropertyFromControl()
        {
            var mungedValue = MungeFromControl(Control.Value);
            Control.IsValidValue = Property.Constraints.All(c => c.IsValid(mungedValue));
            if (Control.IsValidValue)
            {
                SetPropertyValue( mungedValue);
                Control.Error = null;
            }
            else
            {
                Control.Error = String.Join("\n",
                    Property.Constraints.Where(c => !c.IsValid(mungedValue)).Select(c => c.GetErrorText(mungedValue)).ToArray());
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
        protected virtual void OnUserInput()
        {
            SetPropertyFromControl();
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

    public class DefaultListPresenter<T> : Presenter<IListControl<T>>
    {
        public IList<T> GetPropertyValue()
        {
            return Object.GetPropertyValue<IList<T>>(Preferences.Property.PropertyName);
        }

        protected override void InitializeComponent()
        {
            Object.PropertyChanged += new PropertyChangedEventHandler(Object_PropertyChanged);
            Control.UserChangedList += new NotifyCollectionChangedEventHandler(Control_UserChangedList);
            OnResetControlValues();
        }

        #region Behaviours

        protected virtual void OnResetControlValues()
        {
            Control.Values.Clear();
            foreach (T s in GetPropertyValue())
            {
                Control.Values.Add(s);
            }
        }

        #endregion

        #region Event Handlers

        private void Control_UserChangedList(object sender, NotifyCollectionChangedEventArgs args)
        {
            IList<T> property = GetPropertyValue();
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
                    Control.Values.ForEach<T>(i => property.Add(i));
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
            OnResetControlValues();
        }

        #endregion
    }

    #region Default Value Presenters

    /// <summary>
    /// The default presenter has infrastructure for setting IBasicControl 
    /// and IValueControl members.
    /// Additionally basic change handling is implemented. Values are 
    /// transferred without change between property and control.
    /// </summary>
    /// <typeparam name="TYPE">The type of the handled Property's value.</typeparam>
    /// <typeparam name="PROPERTY">The type of the handled Property.</typeparam>
    public class DefaultStructPresenter<TYPE>
        : DefaultPresenter<TYPE?, ValueTypeProperty, TYPE?, IValueControl<TYPE?>>
        where TYPE : struct
    {
        protected override TYPE? MungeFromControl(TYPE? value)
        {
            return value;
        }

        protected override TYPE? MungeFromObject(TYPE? value)
        {
            return value;
        }
    }

    /// <summary>
    /// The default presenter has infrastructure for setting IBasicControl 
    /// and IValueControl members.
    /// Additionally basic change handling is implemented. Values are 
    /// transferred without change between property and control.
    /// 
    /// This derivation is only a shortcut to always specifying IValueControl&lt;TYPE&gt; as CONTROL.
    /// </summary>
    /// <typeparam name="TYPE">The type of the handled Property's value.</typeparam>
    /// <typeparam name="PROPERTY">The type of the handled Property.</typeparam>
    public class DefaultValuePresenter<TYPE>
        : DefaultPresenter<TYPE, ValueTypeProperty, TYPE, IValueControl<TYPE>>
        where TYPE : class
    {
        protected override TYPE MungeFromControl(TYPE value)
        {
            return value;
        }

        protected override TYPE MungeFromObject(TYPE value)
        {
            return value;
        }
    }

    #endregion

    /// <summary>
    /// Handles the specialities around Enumerations
    /// </summary>
    /// <typeparam name="ENUM"></typeparam>
    // TODO: Test when enumeration properties create actual enumeration CLR properties, see Case 478
    public class EnumerationPresenter<ENUM>
        : DefaultPresenter<ENUM, EnumerationProperty, EnumerationEntry, IEnumControl>
        where ENUM : struct // actually where ENUM: System.Enum, but see http://bytes.com/forum/post1821322-8.html 
    {

        public EnumerationPresenter()
        {
            if (!typeof(ENUM).IsEnum)
                throw new ArgumentOutOfRangeException("ENUM", "MUST be an enumeration");

        }

        protected override void InitializeComponent()
        {
            Enumeration = Object.Context
                .GetQuery<Enumeration>()
                .Where(e => e.ClassName == typeof(ENUM).Name)
                .Single();

            Control.ItemsSource = Enumeration.EnumerationEntries.ToList();
            base.InitializeComponent();
        }

        public Enumeration Enumeration { get; private set; }

        protected override ENUM MungeFromControl(EnumerationEntry value)
        {
            return (ENUM)Enum.ToObject(Property.GetPropertyType(), value.Value);
        }

        protected override EnumerationEntry MungeFromObject(ENUM value)
        {
            return Enumeration.EnumerationEntries.Where(entry => entry.Name == Enum.GetName(Property.GetPropertyType(), value)).Single();
        }
    }

    /// <summary>
    /// Handles a widget for a tool bar
    /// </summary>
    public class ToolBarPresenter : Presenter<IBasicControl>
    {
        public ToolBarPresenter() { }

        protected override void InitializeComponent()
        {
            Control.ShortLabel = String.Format("{0} {1}", Object.GetType().Name, Object.ID);

            //TODO: Set Control.Size
        }
    }

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
    /// Handles a widget for a group of properties
    /// </summary>
    public class ActionPresenter : Presenter<IActionControl>
    {
        public ActionPresenter() { }

        protected override void InitializeComponent()
        {
            Control.ShortLabel = Preferences.Method.MethodName;
            Control.ActionActivatedEvent += new EventHandler(Control_ActionActivatedEvent);
            //TODO: Set Control.Size
        }

        void Control_ActionActivatedEvent(object sender, EventArgs e)
        {
            Manager.Renderer.ShowMessage("Button clicked");
            Object.GetType().InvokeMember(Preferences.Method.MethodName, BindingFlags.InvokeMethod, null, Object, new object[] { });
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
            switch (Object.ObjectState)
            {
                case DataObjectState.New:
                    Control.ShortLabel = String.Format("* {0} (new)", Object.ToString());
                    break;
                case DataObjectState.Modified:
                    Control.ShortLabel = String.Format("* {0}", Object.ToString());
                    break;
                case DataObjectState.Deleted:
                    Control.ShortLabel = String.Format("// {0}", Object.ToString());
                    break;
                case DataObjectState.Unmodified:
                default:
                    Control.ShortLabel = String.Format("{0}", Object.ToString());
                    break;
            }
            Control.Description = String.Format("{0}: {1} ({2})", Object.GetType().Name, Object.ToString(), Object.ObjectState);
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

        protected IKistlContext Context { get { return Control.Context; } }

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
            Control.UserDeleteObjectRequest += new EventHandler<GenericEventArgs<IDataObject>>(Control_UserDeleteObjectRequest);

            Context.ObjectDeleted += new GenericEventHandler<IPersistenceObject>(Context_ObjectDeleted);
        }

        private void OnObjectDeleted(IPersistenceObject obj)
        {
            IDataObject dataObject = obj as IDataObject;
            if (dataObject != null)
                Control.RemoveObject(dataObject);
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
            ObjectClass klass = Manager.Renderer.ChooseObject<ObjectClass>(Control.Context, "Choose object class to create");
            if (klass != null)
            {
                Kistl.API.IDataObject newObject = Control.Context.Create(klass.GetDataType());

                var template = newObject.FindTemplate(TemplateUsage.EditControl);
                IObjectControl ctrl = (IObjectControl)Manager.Renderer.CreateControl(newObject, template.VisualTree);

                Control.ShowObject(newObject, ctrl);
            }
        }

        private void OnDelete(IDataObject obj)
        {
            obj.Context.Delete(obj);
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

        void Control_UserDeleteObjectRequest(object sender, GenericEventArgs<IDataObject> e)
        {
            OnDelete(e.Data);
        }

        private void Context_ObjectDeleted(object sender, GenericEventArgs<IPersistenceObject> e)
        {
            OnObjectDeleted(e.Data);
        }

        #endregion

        #region IPresenter Member

        // TODO: this smells, the IPresenter needs to be improved for this use case
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

