
namespace Kistl.Client.WPF.View.ObjectBrowser
{
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

    using Kistl.API.Client;
    using Kistl.Client.Presentables;
    using Kistl.Client.GUI;
    using Kistl.Client.Presentables.ObjectBrowser;
    using Kistl.Client.WPF.CustomControls;

    /// <summary>
    /// Interaction logic for WorkspaceDisplay.xaml, a read-only display of a <see cref="Kistl.Client.Presentables.ObjectBrowser.WorkspaceViewModel"/>.
    /// </summary>
    public partial class WorkspaceDisplay : WindowView, IHasViewModel<WorkspaceViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the WorkspaceDisplay class.
        /// </summary>
        public WorkspaceDisplay()
        {
            InitializeComponent();
        }

        private void ModuleTreeSelectedItemChangedHandler(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = ObjectTree.SelectedItem as Kistl.Client.Presentables.ViewModel;
            if (item != null)
            {
                this.ViewModel.SelectedItem = item;
            }
        }

        /// <summary>
        /// Gets the displayed model. Uses the DataContext as backing store.
        /// </summary>
        public WorkspaceViewModel ViewModel
        {
            get { return (WorkspaceViewModel)DataContext; }
        }
    }
}
