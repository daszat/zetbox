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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;

using Kistl.API;
using Kistl.Client;

namespace Kistl.GUI.Renderer.WPF
{
    /// <summary>
    /// Interaction logic for EditSimplePropertyList.xaml
    /// </summary>
    public partial class EditPointerPropertyList : PropertyControl
    {
        public EditPointerPropertyList()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        public Type ObjectType
        {
            get { return (Type)GetValue(ObjectTypeProperty); }
            set { SetValue(ObjectTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ObjectType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectTypeProperty =
            DependencyProperty.Register("ObjectType", typeof(Type), typeof(EditPointerPropertyList));

        /// <summary>
        /// The actual Value of this Property
        /// </summary>
        public IList Value
        {
            get { return (IList)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(IList), typeof(EditPointerPropertyList));


        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IList collection = Value;
                Type type = collection.GetType().GetGenericArguments().Single();

                ICollectionEntry ce = (ICollectionEntry)Activator.CreateInstance(type);
                Context.Attach(ce);
                collection.Add(ce);
                lst.Items.Refresh();
            }
            catch (Exception ex)
            {
                ClientHelper.HandleError(ex);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int ID = (int)btn.CommandParameter;

                IList c = (IList)Value;
                foreach (ICollectionEntry entry in c)
                {
                    if (entry.ID == ID)
                    {
                        c.Remove(entry);
                        break;
                    }
                }

                lst.Items.Refresh();
            }
            catch (Exception ex)
            {
                ClientHelper.HandleError(ex);
            }
        }
    }
}
