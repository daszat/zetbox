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
using GraphSharp.Controls;
using Kistl.App.Base;
using Kistl.Client.GUI;
using Kistl.Client.Presentables.GUI;
using Kistl.Client.Presentables.ModuleEditor;
using QuickGraph;

namespace Kistl.Client.WPF.View.ModuleEditor
{
    /// <summary>
    /// Interaction logic for DiagramDisplay.xaml
    /// </summary>
    [ViewDescriptor(Kistl.App.GUI.Toolkit.WPF)]
    public partial class NavigationScreenHierarchyDisplay : UserControl, IHasViewModel<NavigationScreenHierarchyViewModel>
    {
        public NavigationScreenHierarchyDisplay()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        public NavigationScreenHierarchyViewModel ViewModel
        {
            get { return (NavigationScreenHierarchyViewModel)DataContext; }
        }

        private void NavTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = NavTree.SelectedItem as NavigationEntryViewModel;
            if (item != null)
            {
                this.ViewModel.SelectedItem = item;
            }
        }

        private void NavTree_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.ViewModel.OpenCommand.Execute(this.ViewModel.SelectedItem);
        }
    }
}
