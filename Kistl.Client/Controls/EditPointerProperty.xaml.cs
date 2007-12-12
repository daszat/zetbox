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
    public partial class EditPointerProperty : UserControl
    {
        public EditPointerProperty()
        {
            InitializeComponent();
            Label = "Label";
            Value = API.Helper.INVALIDID;
        }

        public ObjectType ObjectType { get; set; }

        /// <summary>
        /// Bezeichnung der Eigenschaft
        /// </summary>
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Label.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(EditPointerProperty));

        /// <summary>
        /// Wert der Eigenschaft
        /// </summary>
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(EditPointerProperty));

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
                wnd.ObjectID = this.Value;

                wnd.Show();
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
            }
        }

    }
}
