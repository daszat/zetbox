using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Kistl.API.Client;
using Kistl.API;
using System.ComponentModel;

using Kistl.GUI;
using Kistl.Client;

namespace Kistl.GUI.Renderer.WPF
{
    /// <summary>
    /// Interaction logic for ObjectReferenceControl.xaml
    /// </summary>
    public partial class ObjectReferenceControl 
        : PropertyControl, IReferenceControl, ITestObjectReferenceControl
    {
        public ObjectReferenceControl()
        {
            InitializeComponent();

        }

        private bool _SupressUserInputEvent = false;
        public void SetValueNoUserInput(DependencyProperty dp, object value)
        {
            try
            {
                _SupressUserInputEvent = true;
                SetValue(dp, value);
            }
            finally
            {
                _SupressUserInputEvent = false;
            }
        }

        #region ItemsSource property 

        public IList<Kistl.API.IDataObject> ItemsSource
        {
            get { return (IList<Kistl.API.IDataObject>)GetValue(ItemsSourceProperty); }
            set
            {
                // setting the ItemsSource can cause a spurious changes to the Value
                SetValueNoUserInput(ItemsSourceProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IList<Kistl.API.IDataObject>), typeof(ObjectReferenceControl));

        #endregion

        #region ShowLabel property

        public Visibility ShowLabel
        {
            get { return (Visibility)GetValue(ShowLabelProperty); }
            set { SetValue(ShowLabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowLabel.
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowLabelProperty =
            DependencyProperty.Register("ShowLabel", typeof(Visibility), typeof(ObjectReferenceControl), new UIPropertyMetadata(Visibility.Visible));

        #endregion

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Kistl.API.IDataObject newObject = Context.Create(ObjectType);
                Value = newObject;
                GuiApplicationContext.Current.Renderer.ShowObject(newObject);
            }
            catch (Exception ex)
            {
                ClientHelper.HandleError(ex);
            }
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Value != null)
                {
                    GuiApplicationContext.Current.Renderer.ShowObject(Value);
                }
            }
            catch (Exception ex)
            {
                ClientHelper.HandleError(ex);
            }
        }

        #region IPointerControl Members

        public Type ObjectType
        {
            get { return (Type)GetValue(ObjectTypeProperty); }
            set { SetValue(ObjectTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ObjectType.
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectTypeProperty =
            DependencyProperty.Register("ObjectType",
                typeof(Type), typeof(ObjectReferenceControl),
                new PropertyMetadata(null));

        /// <summary>
        /// explicitly implement this for the framework and suppress the UserInput Event 
        /// when using this.
        /// </summary>
        Kistl.API.IDataObject IValueControl<Kistl.API.IDataObject>.Value
        {
            get { return (Kistl.API.IDataObject)GetValue(ValueProperty); }
            set { SetValueNoUserInput(ValueProperty, value); }
        }

        /// <summary>
        /// The actual Value of this Property
        /// </summary>
        public Kistl.API.IDataObject Value
        {
            get { return (Kistl.API.IDataObject)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value",
                typeof(Kistl.API.IDataObject), typeof(ObjectReferenceControl));

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == ValueProperty)
            {
                OnUserInput(e);
            }
        }

        protected virtual void OnUserInput(DependencyPropertyChangedEventArgs e)
        {
            // this is not always a noop. it happens if the Value is set 
            // programmatically, e.g. by the Presenter on initialisation
            // or when the Model's Value changes.
            cbValues.SelectedIndex = ItemsSource.IndexOf(Value);

            if (UserInput != null && !_SupressUserInputEvent)
            {
                UserInput(this, new EventArgs());
            }
        }

        public event EventHandler UserInput;

        #endregion

        #region ITestObjectReferenceControl Members

        object ITestObjectReferenceControl.ComboboxValue
        {
            get
            {
                if (cbValues.SelectedIndex < 0)
                {
                    return null;
                }
                else
                {
                    return ItemsSource[cbValues.SelectedIndex];
                }
            }
        }

        #endregion

        //private void PropertyControl_Loaded(object sender, RoutedEventArgs e)
        //{
        //    var beb = BindingOperations.GetBindingExpressionBase(cbValues, ComboBox.SelectedItemProperty);
        //    beb.UpdateSource();
        //}

    }

    public interface ITestObjectReferenceControl
    {
        object ComboboxValue { get; }
    }
}
