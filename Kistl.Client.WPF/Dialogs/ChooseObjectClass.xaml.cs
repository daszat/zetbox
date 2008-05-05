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

namespace Kistl.Client.WPF.Dialogs
{
    /// <summary>
    /// Interaction logic for ChooseObjectClass.xaml
    /// </summary>
    public partial class ChooseObjectClass : Window
    {
        public ChooseObjectClass()
        {
            InitializeComponent();
        }

        private void lst_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Choose();
            }
            catch (Exception ex)
            {
                ClientHelper.HandleError(ex);
            }
        }

        private void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Choose();
            }
            catch (Exception ex)
            {
                ClientHelper.HandleError(ex);
            }
        }

        private void Choose()
        {
            if (lst.SelectedItem != null)
            {
                ResultObjectClass = (Kistl.App.Base.ObjectClass)lst.SelectedItem;
                this.DialogResult = true;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Kistl.App.Base.ObjectClass> objectClasses = new List<Kistl.App.Base.ObjectClass>();

                AddObjectClasses(BaseObjectClass, objectClasses);

                this.DataContext = objectClasses;
            }
            catch (Exception ex)
            {
                ClientHelper.HandleError(ex);
            }
        }

        private void AddObjectClasses(Kistl.App.Base.ObjectClass objClass, List<Kistl.App.Base.ObjectClass> objectClasses)
        {
            objectClasses.Add(objClass);

            foreach (Kistl.App.Base.ObjectClass sub in objClass.SubClasses)
            {
                AddObjectClasses(sub, objectClasses);
            }
        }

        public Kistl.App.Base.ObjectClass ResultObjectClass
        {
            get { return (Kistl.App.Base.ObjectClass)GetValue(ResultObjectClassProperty); }
            set { SetValue(ResultObjectClassProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ResultObjectClass.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResultObjectClassProperty =
            DependencyProperty.Register("ResultObjectClass", typeof(Kistl.App.Base.ObjectClass), typeof(ChooseObjectClass));


        public Kistl.App.Base.ObjectClass BaseObjectClass
        {
            get { return (Kistl.App.Base.ObjectClass)GetValue(BaseObjectClassProperty); }
            set { SetValue(BaseObjectClassProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BaseObjectClass.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BaseObjectClassProperty =
            DependencyProperty.Register("BaseObjectClass", typeof(Kistl.App.Base.ObjectClass), typeof(ChooseObjectClass));
    }
}
