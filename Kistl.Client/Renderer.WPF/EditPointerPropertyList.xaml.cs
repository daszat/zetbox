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

        public ObjectType ObjectType
        {
            get { return (ObjectType)GetValue(ObjectTypeProperty); }
            set { SetValue(ObjectTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ObjectType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectTypeProperty =
            DependencyProperty.Register("ObjectType", typeof(ObjectType), typeof(EditPointerPropertyList));

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IList collection = (IList)Value;
                Type[] types = collection.GetType().GetGenericArguments();
                if (types.Length != 1) throw new InvalidOperationException("IList has more then one generic Parameter?");

                ICollectionEntry ce = (ICollectionEntry)Activator.CreateInstance(types[0]);
                Context.Attach(ce);
                collection.Add(ce);
                lst.Items.Refresh();
            }
            catch (Exception ex)
            {
                Kistl.Client.Helper.HandleError(ex);
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
                Kistl.Client.Helper.HandleError(ex);
            }
        }
    }
}
