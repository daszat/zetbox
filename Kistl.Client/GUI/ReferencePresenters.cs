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

    public abstract class ListPresenter<T, PROPERTY>
        : Presenter<IReferenceListControl>
        where T : IDataObject
        where PROPERTY : BaseProperty
    {

        private List<T> _Items;
        private PropertyChangedEventHandler _Object_PropertyChanged = null;
        private EventHandler _Control_UserAddRequest = null;
        private NotifyCollectionChangedEventHandler _Control_UserChangedListEvent = null;

        /// <summary>
        /// Override this to specify a conversion from the object's property to the value for the widget
        /// </summary>
        /// <returns>the value of the handled property in the right type for the widget</returns>
        //GetList() is only implemented for some properties, since BaseProperty doesn't know IsList.
        protected abstract IList<T> GetPropertyValue();

        #region Utilities

        // localize type-unsafety
        /// <summary>
        /// The BackReferenceProperty this Presenter presents.
        /// </summary>
        public PROPERTY Property { get { return (PROPERTY)Preferences.Property; } }

        private bool ValidateItems(IList items)
        {
            var invalid = items.Cast<T>().Where(i => !_Items.Contains(i));
            // throw new InvalidOperationException(String.Format("Invalid items: '{0}'", invalid.JoinStrings("', '")));
            return (invalid.Count() == 0);
        }

        #endregion

        #region Initialisation

        /// <summary>
        /// Setup the Control with default values from the Property. 
        /// Install EventHandlers for UserInput and PropertyChanged.
        /// </summary>
        protected override void InitializeComponent()
        {
            Control.ObjectType = Property.GetPropertyType(); // GetDataCLRType();

            Object.Context.ObjectCreated += new GenericEventHandler<IPersistenceObject>(Context_ObjectListChanged);
            Object.Context.ObjectDeleted += new GenericEventHandler<IPersistenceObject>(Context_ObjectListChanged);

            SetItemsSource();

            Control.ShortLabel = Property.PropertyName;
            Control.Description = Property.AltText;


            // To prevent resource leaks, all event handlers have to be removed
            // See DisposeManagedResources()
            {
                _Object_PropertyChanged = new System.ComponentModel.PropertyChangedEventHandler(Object_PropertyChanged);
                Object.PropertyChanged += _Object_PropertyChanged;

                _Control_UserAddRequest = new EventHandler(Control_UserAddRequest);
                Control.UserAddRequest += _Control_UserAddRequest;

                SetControlValueFromObject();
            }

            // Control.Size = Preferences.PreferredSize;
            Control.Size = FieldSize.Full;
        }

        private void SetControlValueFromObject()
        {
            if (_Control_UserChangedListEvent == null)
                _Control_UserChangedListEvent = new NotifyCollectionChangedEventHandler(Control_UserChangedListEvent);

            if (Control.Value != null)
                Control.Value.CollectionChanged -= _Control_UserChangedListEvent;

            ObservableCollection<IDataObject> controlValue = new ObservableCollection<IDataObject>(GetPropertyValue().Cast<IDataObject>().ToList());
            controlValue.CollectionChanged += _Control_UserChangedListEvent;
            Control.Value = controlValue;

            // Value is coming from object => always valid
            // TODO: validation framework might change this
            Control.IsValidValue = true;
            Control.Error = null;
        }

        private void SetItemsSource()
        {
            // remember the objects that are sent to the object
            // to facilitate validity checking
            _Items = Object.Context.GetQuery<T>().ToList();
            Control.ItemsSource = _Items.Cast<IDataObject>().ToList();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Invoked when the Object's Properties change. If the Event matches
        /// the Property, sets the value in the control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Object_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
                if (sender != Object)
                    throw new InvalidOperationException(String.Format("Resource Leak: _Object_PropertyChanged in '{0}' was called by '{1}', but should be attached to '{2}'",
                        this, sender, Object));

                // if the object has changed, unconditionally overwrite the value in the GUI
                // intelligent controls might want to show the user both the "real" and the user's value
                if (e.PropertyName == Property.PropertyName)
                    OnPropertyChanged(e);
            }
        }

        /// <summary>
        /// This method is invoked when the presented Property changes.
        /// The default implementation just sets the Value in the Control.
        /// </summary>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            SetControlValueFromObject();
        }

        private void Control_UserAddRequest(object sender, EventArgs e)
        {
            OnUserAddRequest(e);
        }

        /// <summary>
        /// This method is invoked when the user expresses his intent to add an
        /// object to this list. The default implementation uses the Renderer to 
        /// present a Dialog and adds the returned value (if any) to the Property.
        /// </summary>
        protected virtual void OnUserAddRequest(EventArgs e)
        {
            T toAdd = Manager.Renderer.ChooseObject<T>(Object.Context, "Choose object to add");

            if (toAdd != null)
            {
                // this should trigger a PropertyChanged Event and set the Control's Value 
                // via the default EventHandler, but it doesn't.
                GetPropertyValue().Add(toAdd);
                // Therefore, set it here, manually:
                SetControlValueFromObject();
                // TODO: propagate NotifyCollectionChangedEvents within the Object to PropertyChanged
            }
        }

        private void Control_UserChangedListEvent(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.NewItems != null)
                Control.IsValidValue = ValidateItems(args.NewItems);

            if (Control.IsValidValue)
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
                        Control.Value.Cast<T>().ForEach<T>(i => property.Add(i));
                        break;
                    default:
                        throw new NotImplementedException(
                            String.Format("Unknown NotifyCollectionChangedAction: {0}", args.Action));
                }

#if DEBUG
                // Check whether the Presenter has synced the two lists correctly
                if (property.Count != Control.Value.Count)
                {
                    throw new InvalidOperationException("Error when synchronising Values in ObjectListPresenter: number of elements mismatch");
                }
                for (int i = 0; i < property.Count; i++)
                {
                    if (!property[i].Equals(Control.Value[i]))
                    {
                        throw new InvalidOperationException("Error when synchronising Values in ObjectListPresenter: element mismatch");
                    }
                }
#endif

            }
        }

        private void Context_ObjectListChanged(object sender, GenericEventArgs<IPersistenceObject> e)
        {
            if (Control.ObjectType.IsAssignableFrom(e.Data.GetInterfaceType()))
                SetItemsSource();
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
            {
                Control.UserAddRequest -= Control_UserAddRequest;
                if (Control.Value != null)
                {
                    Control.Value.CollectionChanged -= _Control_UserChangedListEvent;
                }
            }
        }

        #endregion

    }

    public class BackReferencePresenter<T>
        : ListPresenter<T, BackReferenceProperty>
        where T : IDataObject
    {
        protected override IList<T> GetPropertyValue()
        {
            return Object.GetList<T>(Property);
        }

        // obsolete?
        private void Control_UserAddRequest(object sender, EventArgs e)
        {
            IDataObject toAdd = Manager.Renderer.ChooseObject(Object.Context, Control.ObjectType, String.Format("Choose {0} to add", Control.ObjectType.Name));
            if (toAdd == null)
                return;
            toAdd.SetPropertyValue(Property.ReferenceProperty, Object.ID);
        }

    }


    public class ObjectListPresenter<T>
        : ListPresenter<T, ObjectReferenceProperty>
        where T : IDataObject
    {
        protected override IList<T> GetPropertyValue()
        {
            return Object.GetList<T>(Property);
        }

    }

    /// <summary>
    /// Presents a reference to another IDataObject of a given Type.
    /// The list of selectable Items is set from the list of available objects.
    /// Different controls may implement the selection differently: Comboboxes, Lists, etc.
    /// </summary>
    public class ObjectReferencePresenter<T>
        : DefaultPresenter<IDataObject, ObjectReferenceProperty, IDataObject, IReferenceControl>
        where T : IDataObject
    {
        public ObjectReferencePresenter() { }

        private List<T> _Items;

        protected override void InitializeComponent()
        {
            Control.ObjectType = Property.ReferenceObjectClass.GetDataType();
            Object.Context.ObjectCreated += new GenericEventHandler<IPersistenceObject>(Context_ObjectListChanged);
            Object.Context.ObjectDeleted += new GenericEventHandler<IPersistenceObject>(Context_ObjectListChanged);

            SetItemsSource();

            base.InitializeComponent();
        }

        private void Context_ObjectListChanged(object sender, GenericEventArgs<IPersistenceObject> e)
        {
            if (Control.ObjectType.IsAssignableFrom(e.Data.GetInterfaceType()))
                SetItemsSource();
        }

        private void SetItemsSource()
        {
            // remember the objects that are sent to the object
            // to facilitate validity checking

            // TODO: constrain query on property constraints to only get valid items
            _Items = Object.Context.GetQuery<T>().ToList();
            Control.ItemsSource = _Items.Cast<IDataObject>().ToList();
        }

        protected override IDataObject MungeFromControl(IDataObject value)
        {
            return value;
        }

        protected override IDataObject MungeFromObject(IDataObject value)
        {
            return value;
        }
    }


}
