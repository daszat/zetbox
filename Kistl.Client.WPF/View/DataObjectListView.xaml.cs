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
    public partial class DataObjectListView : UserControl, IView
    {

        public DataObjectListView()
        {
            InitializeComponent();
        }

        private void AddNewHandler(object sender, RoutedEventArgs e)
        {
            var model = DataContext as ObjectListModel;
            model.CreateNewElement(newitem => { if (newitem != null) model.AddItem(newitem); });
        }

        private void RemoveHandler(object sender, RoutedEventArgs e)
        {
            var model = DataContext as ObjectListModel;
            if (model.SelectedItem != null)
            {
                model.RemoveItem(model.SelectedItem);
            }
        }

        private void ItemActivatedHandler(object sender, MouseButtonEventArgs e)
        {
            var model = DataContext as ObjectListModel;
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
