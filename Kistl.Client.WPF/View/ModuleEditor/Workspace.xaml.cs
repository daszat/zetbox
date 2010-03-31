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

namespace Kistl.Client.WPF.View.ModuleEditor
{
    /// <summary>
    /// Interaction logic for Workspace.xaml
    /// </summary>
    public partial class Workspace : Window
    {
        public Workspace()
        {
            InitializeComponent();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = NavTree.SelectedItem as Kistl.Client.Presentables.PresentableModel;
            if (item != null)
            {
                this.Model.SelectedItem = item;
            }
        }

        private Kistl.Client.Presentables.ModuleEditor.WorkspaceModel Model
        {
            get { return (Kistl.Client.Presentables.ModuleEditor.WorkspaceModel)DataContext; }
        }
    }
}
