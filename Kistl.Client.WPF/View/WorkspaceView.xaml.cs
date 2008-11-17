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

namespace Kistl.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for DesktopView.xaml
    /// </summary>
    public partial class WorkspaceView : Window
    {
        public WorkspaceView()
        {
            InitializeComponent();
        }

        private void DataObjectActivated(object sender, MouseButtonEventArgs e)
        {
            var view = (FrameworkElement)sender;
            var dataModel = (DataObjectModel)view.DataContext;
            var workspaceModel = (WorkspaceModel)this.DataContext;

            if (!workspaceModel.OpenObjects.Contains(dataModel))
                workspaceModel.OpenObjects.Add(dataModel);
        }

        // TODO: implement some bridging code to reduce code duplication on multiple commands
        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var workspaceModel = (WorkspaceModel)this.DataContext;
            e.CanExecute =
                workspaceModel != null
                && workspaceModel.SaveCommand.CanExecute(e.Parameter);
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var workspaceModel = (WorkspaceModel)this.DataContext;
            workspaceModel.SaveCommand.Execute(e.Parameter);
        }

    }
}
