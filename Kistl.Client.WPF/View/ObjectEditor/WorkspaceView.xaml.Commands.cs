
namespace Kistl.Client.WPF.View
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
    using System.Windows.Shapes;

    using Kistl.Client.Presentables;
    using Kistl.Client.WPF.Commands;

    /// <summary>
    /// Interaction logic for DesktopView.xaml
    /// </summary>
    public partial class WorkspaceView : Window
    {
        // TODO: retrieve strings from DB
        // TODO: implement some bridging code to reduce code duplication on multiple commands

        #region SaveCommand

        public static readonly RoutedUICommand Save = new RoutedUICommand("Save", "save", typeof(WorkspaceView));
        public static readonly RoutedUICommand Verify = new RoutedUICommand("Verify", "verify", typeof(WorkspaceView));

        static WorkspaceView()
        {
            CommandManager.RegisterClassCommandBinding(
                typeof(WorkspaceView),
                new CommandBinding(
                    Save,
                    SaveExecuted,
                    SaveCanExecute)); 
            
            CommandManager.RegisterClassCommandBinding(
                typeof(WorkspaceView),
                new CommandBinding(
                    Verify,
                    VerifyExecuted));
        }

        private static void SaveCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var workspaceModel = (WorkspaceModel)e.Parameter;
            e.CanExecute =
                workspaceModel != null
                && workspaceModel.SaveCommand.CanExecute(null);
        }

        private static void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var workspaceModel = (WorkspaceModel)e.Parameter;
            workspaceModel.SaveCommand.Execute(null);
        }

        private static void VerifyExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var workspaceModel = (WorkspaceModel)e.Parameter;
            workspaceModel.VerifyContextCommand.Execute(null);
        }

        #endregion
    }
}