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
using Kistl.Client.Presentables;

namespace Kistl.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for ListValueView.xaml
    /// </summary>
    public partial class ListValueView : PropertyView
    {
        public ListValueView()
        {
            InitializeComponent();
        }

        private void AddNewHandler(object sender, RoutedEventArgs e)
        {
            var model = (IValueListModel<string>)DataContext;
            model.AddItem("");
        }

        private void RemoveHandler(object sender, RoutedEventArgs e)
        {
            var model = (IValueListModel<string>)DataContext;
            model.RemoveItem(model.SelectedItem);
        }

    }
}
