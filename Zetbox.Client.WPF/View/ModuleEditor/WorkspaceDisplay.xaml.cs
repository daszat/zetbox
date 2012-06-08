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
using System.Windows.Shapes;
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables.ModuleEditor;
using Zetbox.Client.WPF.CustomControls;

namespace Zetbox.Client.WPF.View.ModuleEditor
{
    /// <summary>
    /// Interaction logic for Workspace.xaml
    /// </summary>
    public partial class WorkspaceDisplay : WindowView, IHasViewModel<WorkspaceViewModel>
    {
        public WorkspaceDisplay()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = NavTree.SelectedItem as Zetbox.Client.Presentables.ViewModel;
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
