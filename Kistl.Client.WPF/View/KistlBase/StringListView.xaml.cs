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
using Kistl.Client.WPF.View.KistlBase;
using Kistl.Client.Presentables.ValueViewModels;
using Kistl.API.Utils;

namespace Kistl.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for StringListView.xaml
    /// </summary>
    public partial class StringListView : PropertyEditor
    {
        public StringListView()
        {
            InitializeComponent();
        }

        private void AddNewHandler(object sender, RoutedEventArgs e)
        {
            var model = (IValueListViewModel<string, IReadOnlyList<string>>)DataContext;
            model.AddItem(String.Empty);
            PART_ItemEditBox.Focus();
        }

        private void RemoveHandler(object sender, RoutedEventArgs e)
        {
            var model = (IValueListViewModel<string, IReadOnlyList<string>>)DataContext;
            model.RemoveItem(model.SelectedItem);
        }


        protected override FrameworkElement MainControl
        {
            get { return lst; }
        }
    }
}
