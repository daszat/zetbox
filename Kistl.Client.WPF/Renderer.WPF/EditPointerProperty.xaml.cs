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
    /// Interaction logic for EditPointerProperty.xaml
    /// </summary>
    public partial class EditPointerProperty : PropertyControl, IPointerControl
    {
        public EditPointerProperty()
        {
            InitializeComponent();
        }

        public IList<string> ItemsSource
        {
            get { return (IList<string>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IList<string>), typeof(EditPointerProperty));



        public Visibility ShowLabel
        {
            get { return (Visibility)GetValue(ShowLabelProperty); }
            set { SetValue(ShowLabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowLabel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowLabelProperty =
            DependencyProperty.Register("ShowLabel", typeof(Visibility), typeof(EditPointerProperty), new UIPropertyMetadata(Visibility.Visible));

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

        // Using a DependencyProperty as the backing store for ObjectType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectTypeProperty =
            DependencyProperty.Register("ObjectType",
                typeof(ObjectType), typeof(EditPointerProperty),
                new PropertyMetadata(null));

        private bool _IsUserInput = true;
        int IValueControl<int>.Value
        {
            get { return (int)GetValue(ValueProperty); }
            set
            {
                _IsUserInput = false;
                try
                {
                    SetValue(ValueProperty, value);
                }
                finally
                {
                    _IsUserInput = true;
                }
            }
        }

        /// <summary>
        /// The actual Value of this Property
        /// </summary>
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(EditPointerProperty));


        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (_IsUserInput && e.Property == ValueProperty)
            {
                OnUserInput(e);
            }
        }

        protected virtual void OnUserInput(DependencyPropertyChangedEventArgs e)
        {
            if (UserInput != null)
            {
                UserInput(this, new EventArgs());
            }
        }

        public event EventHandler UserInput;

        #endregion

        private void cbValues_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
