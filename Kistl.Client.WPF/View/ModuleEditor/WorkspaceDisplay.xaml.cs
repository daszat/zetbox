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
using Kistl.Client.GUI;
using Kistl.Client.Presentables.ModuleEditor;

namespace Kistl.Client.WPF.View.ModuleEditor
{
    /// <summary>
    /// Interaction logic for Workspace.xaml
    /// </summary>
    public partial class WorkspaceDisplay : Window, IHasViewModel<WorkspaceViewModel>
    {
        public WorkspaceDisplay()
        {
            InitializeComponent();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = NavTree.SelectedItem as Kistl.Client.Presentables.PresentableModel;
            if (item != null)
            {
                this.ViewModel.SelectedItem = item;
            }
        }

        public WorkspaceViewModel ViewModel
        {
            get { return (WorkspaceViewModel)DataContext; }
        }
    }
}
