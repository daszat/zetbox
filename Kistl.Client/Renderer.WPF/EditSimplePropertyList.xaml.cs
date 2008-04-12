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
                Kistl.Client.Helper.HandleError(ex);
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IList c = (IList)Value;
                Type[] types = c.GetType().GetGenericArguments();
                if (types.Length != 1) throw new InvalidOperationException("IList has more then one generic Parameter?");

                object v = Activator.CreateInstance(types[0]);
                c.Add(v);
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
