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

using Kistl.Client.Presentables;
using Kistl.Client.GUI;

namespace Kistl.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for SelectionDialog.xaml
    /// </summary>
    public partial class SelectionDialog : Window, IView
    {
        public SelectionDialog()
        {
            InitializeComponent();
        }

        private void ChooseClickHandler(object sender, RoutedEventArgs e)
        {
            var model = (DataObjectSelectionTaskModel)DataContext;
            var choosen = (DataObjectModel)choicesList.SelectedItem;
            model.Choose(choosen);
            this.Close();
        }

        private void CancelClickHandler(object sender, RoutedEventArgs e)
        {
            var model = (DataObjectSelectionTaskModel)DataContext;
            model.Choose(null);
            this.Close();            
        }

        #region IView Members

        public void SetModel(PresentableModel mdl)
        {
            DataContext = (DataObjectSelectionTaskModel)mdl;
        }

        #endregion
    }
}
