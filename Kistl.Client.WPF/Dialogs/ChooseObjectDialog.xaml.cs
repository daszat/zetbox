using System;
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
using System.Windows.Shapes;

using Kistl.API;
using Kistl.API.Client;

namespace Kistl.Client.WPF.Dialogs
{
    /// <summary>
    /// Interaktionslogik für ChooseObject.xaml
    /// </summary>
    public partial class ChooseObjectDialog : Window
    {
        public ChooseObjectDialog()
        {
            InitializeComponent();
        }

        public IKistlContext Context
        {
            get { return (IKistlContext)GetValue(ContextProperty); }
            set { SetValue(ContextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Context.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContextProperty =
            DependencyProperty.Register("Context", typeof(IKistlContext), typeof(ChooseObjectDialog), new PropertyMetadata());

        public ObjectType ObjectType
        {
            get { return (ObjectType)GetValue(ObjectTypeProperty); }
            set { SetValue(ObjectTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ObjectType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectTypeProperty =
            DependencyProperty.Register("ObjectType", typeof(ObjectType), typeof(ChooseObjectDialog), new PropertyMetadata());

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // load all available Objects of this type
            // TODO: implement paging/searching in order to reduce server load
            lstObjects.ItemsSource = Context.GetQuery(ObjectType).ToList();
        }

        public Kistl.API.IDataObject Result
        {
            get
            {
                return (Kistl.API.IDataObject)lstObjects.SelectedItem;
            }
        }

        private void lstObjects_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
