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
using Kistl.Client.Presentables;
using Kistl.Client.Presentables.ModuleEditor;

namespace Kistl.Client.WPF.View.ModuleEditor
{
    /// <summary>
    /// Interaction logic for AppLauncherDisplay.xaml
    /// </summary>
    public partial class AppLauncherDisplay : UserControl
    {
        public AppLauncherDisplay()
        {
            InitializeComponent();
        }

        private void ModuleList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listItem = sender as ListViewItem;
            if (listItem == null)
            {
                return;
            }

            var dataObject = listItem.Content as AppLauncherModuleModel;
            if (dataObject == null)
            {
                return;
            }

            // only react to left mouse button double clicks
            if (e.ChangedButton != MouseButton.Left)
            {
                return;
            }

            dataObject.ShowWorkspace();
            e.Handled = true;
        }
    }
}
