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
using Kistl.Client.Presentables.ModuleEditor;
using QuickGraph;
using Kistl.App.Base;
using GraphSharp.Controls;

namespace Kistl.Client.WPF.View.ModuleEditor
{
    /// <summary>
    /// Interaction logic for DiagramDisplay.xaml
    /// </summary>
    [ViewDescriptor(Kistl.App.GUI.Toolkit.WPF)]
    public partial class ControlKindHierarchyDisplay : UserControl, IHasViewModel<ControlKindHierarchyViewModel>
    {
        public ControlKindHierarchyDisplay()
        {
            InitializeComponent();
        }

        public ControlKindHierarchyViewModel ViewModel
        {
            get { return (ControlKindHierarchyViewModel)DataContext; }
        }

        private void NavTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = NavTree.SelectedItem as Kistl.Client.Presentables.ViewModel;
            if (item != null)
            {
                this.ViewModel.SelectedItem = item;
            }
        }
    }
}
