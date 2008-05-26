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
    public partial class ObjectReferenceControl : PropertyControl, IObjectReferenceControl, ITestObjectReferenceControl
    {
        public ObjectReferenceControl()
        {
            InitializeComponent();
        }

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

        public IList<Kistl.API.IDataObject> ItemsSource
        {
            get { return (IList<Kistl.API.IDataObject>)GetValue(ItemsSourceProperty); }
            set
            {
                // This can cause a unrelated change to the Value
                SetValueNoUserInput(ItemsSourceProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IList<Kistl.API.IDataObject>), typeof(ObjectReferenceControl));

        public Visibility ShowLabel
        {
            get { return (Visibility)GetValue(ShowLabelProperty); }
            set { SetValue(ShowLabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowLabel.
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowLabelProperty =
            DependencyProperty.Register("ShowLabel", typeof(Visibility), typeof(ObjectReferenceControl), new UIPropertyMetadata(Visibility.Visible));

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
#if false
                ObjectWindow wnd = new ObjectWindow();
                wnd.ObjectType = this.ObjectType;
                wnd.ObjectID = API.Helper.INVALIDID;

                wnd.ShowDialog();

                if (wnd.ObjectID != API.Helper.INVALIDID)
                {
                    LoadList();
                    this.Value = wnd.ObjectID;
                }
#endif
            }
            catch (Exception ex)
            {
                ClientHelper.HandleError(ex);
            }
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
#if false
            try
            {
                ObjectWindow wnd = new ObjectWindow();
                wnd.ObjectType = this.ObjectType;
                wnd.ObjectID = this.TargetID;

                wnd.Show();
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
            }
#endif
        }

        #region IPointerControl Members

        public ObjectType ObjectType
        {
            get { return (ObjectType)GetValue(ObjectTypeProperty); }
            set { SetValue(ObjectTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ObjectType.
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectTypeProperty =
            DependencyProperty.Register("ObjectType",
                typeof(ObjectType), typeof(ObjectReferenceControl),
                new PropertyMetadata(null));

        private bool _SupressUserInputEvent = false;
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

        private void cbValues_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbValues.SelectedIndex < 0)
            {
                Value = null;
            }
            else
            {
                Value = ItemsSource[cbValues.SelectedIndex];
            }
        }

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

    }

    public interface ITestObjectReferenceControl
    {
        object ComboboxValue { get; }
    }
}
