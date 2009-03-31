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

using Kistl.Client.GUI;
using Kistl.Client.Presentables;

namespace Kistl.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for DataObjectListView.xaml
    /// </summary>
    public partial class DataObjectListView : PropertyView, IView
    {

        public DataObjectListView()
        {
            InitializeComponent();
        }

        private void AddNewHandler(object sender, RoutedEventArgs e)
        {
            var model = (ObjectListModel)DataContext;
            model.CreateNewItem(newitem =>
            {
                if (newitem != null)
                {
                    model.AddItem(newitem);
                    model.SelectedItem = newitem;
                    model.ActivateItem(model.SelectedItem, true);
                }
            });
        }

        private void AddExistingItemHandler(object sender, RoutedEventArgs e)
        {
            var model = (ObjectListModel)DataContext;
            model.AddExistingItem();
        }

        private void RemoveHandler(object sender, RoutedEventArgs e)
        {
            var model = (ObjectListModel)DataContext;
            if (model.SelectedItem != null)
            {
                model.RemoveItem(model.SelectedItem);
            }
        }

        private void DeleteHandler(object sender, RoutedEventArgs e)
        {
            var model = (ObjectListModel)DataContext;
            if (model.SelectedItem != null)
            {
                model.DeleteItem(model.SelectedItem);
            }
        }

        private void ItemActivatedHandler(object sender, MouseButtonEventArgs e)
        {
            var model = (ObjectListModel)DataContext;
            if (model.SelectedItem != null)
            {
                model.ActivateItem(model.SelectedItem, true);
            }
        }

        #region IView Members

        public void SetModel(PresentableModel mdl)
        {
            DataContext = (ObjectListModel)mdl;
        }

        #endregion


    }
}
