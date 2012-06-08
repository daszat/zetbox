using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Zetbox.API.Utils;
using Zetbox.Client.Presentables;
using Zetbox.Client.Presentables.ValueViewModels;
using Zetbox.Client.WPF.CustomControls;
using Zetbox.Client.WPF.View.ZetboxBase;

namespace Zetbox.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for StringListView.xaml
    /// </summary>
    public partial class StringListView : PropertyEditor
    {
        public StringListView()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
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
