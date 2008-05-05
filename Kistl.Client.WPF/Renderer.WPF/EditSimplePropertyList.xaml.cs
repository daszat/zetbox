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
    public partial class EditSimplePropertyList : PropertyControl
    {
        public EditSimplePropertyList()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                ClientHelper.HandleError(ex);
            }
        }

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
            DependencyProperty.Register("Value", typeof(IList), typeof(EditSimplePropertyList));


        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IList c = Value;
                Type type = c.GetType().GetGenericArguments().Single();

                object v = Activator.CreateInstance(type);
                c.Add(v);
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

                IList c = Value;
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
