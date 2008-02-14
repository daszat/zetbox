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
using Kistl.API.Client;
using Kistl.API;

namespace Kistl.Client.Controls
{
    /// <summary>
    /// Interaction logic for EditPointerProperty.xaml
    /// </summary>
    public partial class EditPointerProperty : PropertyControl
    {
        public EditPointerProperty()
        {
            InitializeComponent();
            Value = API.Helper.INVALIDID;
        }

        public ObjectType ObjectType { get; set; }

        public int TargetID
        {
            get { return (int)Value; }
            set { Value = value; }
        }

        private void LoadList()
        {
            using (KistlContext ctx = new KistlContext())
            {
                cbValues.ItemsSource = ctx.GetQuery(ObjectType).ToList();
            }
        }

        private void PointerCtrl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadList();
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ObjectWindow wnd = new ObjectWindow();
                wnd.ObjectType = this.ObjectType;
                wnd.ObjectID = API.Helper.INVALIDID;

                wnd.ShowDialog();

                if (wnd.ObjectID != API.Helper.INVALIDID)
                {
                    LoadList();
                    this.Value = wnd.ObjectID;
                }
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
            }
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
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
        }

    }
}
