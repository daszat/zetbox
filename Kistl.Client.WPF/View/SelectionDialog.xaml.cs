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
    public partial class SelectionDialog : Window
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

        private void ChoiceActivated(object sender, MouseButtonEventArgs e)
        {
            var model = (DataObjectSelectionTaskModel)DataContext;
            var choosen = (DataObjectModel)choicesList.SelectedItem;
            
            // ignore double clicks without selection
            if (choosen != null)
            {
                model.Choose(choosen);
                this.Close();
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            filterTextBox.Focus();
        }
    }
}
