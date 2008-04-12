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



        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(EditPointerProperty));



        public Visibility ShowLabel
        {
            get { return (Visibility)GetValue(ShowLabelProperty); }
            set { SetValue(ShowLabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowLabel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowLabelProperty =
            DependencyProperty.Register("ShowLabel", typeof(Visibility), typeof(EditPointerProperty), new UIPropertyMetadata(Visibility.Visible));


        public int TargetID
        {
            get { return (int)Value; }
            set { Value = value; }
        }

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
                Kistl.Client.Helper.HandleError(ex);
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

        private void cbValues_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        #region IPointerControl Members

        // these two already exist correctly:
        // ObjectType IPointerControl.ObjectType { get; set; }
        // int IPointerControl.TargetID { get; set; }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == ValueProperty)
            {
                OnTargetIDChanged(e);
            }
        }

        protected virtual void OnTargetIDChanged(DependencyPropertyChangedEventArgs e)
        {
            if (_TargetIDChanged != null)
            {
                _TargetIDChanged(this, new EventArgs());
            }
        }

        private event EventHandler _TargetIDChanged;
        public event EventHandler UserInput
        {
            add { _TargetIDChanged += value; }
            remove { _TargetIDChanged -= value; }
        }

        #endregion
    }
}
