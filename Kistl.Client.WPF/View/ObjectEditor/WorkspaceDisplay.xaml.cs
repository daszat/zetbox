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

using Kistl.App.GUI;
using Kistl.Client.GUI;
using Kistl.Client.Presentables;

namespace Kistl.Client.WPF.View.ObjectEditor
{
    /// <summary>
    /// Interaction logic for DesktopView.xaml
    /// </summary>
    public partial class WorkspaceDisplay : Window, IHasViewModel<WorkspaceModel>
    {
        public WorkspaceDisplay()
        {
            InitializeComponent();
        }

        private void DeleteHandler(object sender, RoutedEventArgs e)
        {
            var item = ViewModel.SelectedItem as DataObjectModel;
            if (item != null)
            {
                item.Delete();
                ViewModel.HistoryTouch(item);
            }
        }

        public WorkspaceModel ViewModel
        {
            get { return (WorkspaceModel)this.DataContext; }
        }
    }
}
