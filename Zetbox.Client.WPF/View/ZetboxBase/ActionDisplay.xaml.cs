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
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables;
using Zetbox.Client.WPF.CustomControls;
using Microsoft.Windows.Controls;

namespace Zetbox.Client.WPF.View.ZetboxBase
{
    /// <summary>
    /// Interaction logic for ActionView.xaml
    /// </summary>
    public partial class ActionDisplay : CommandButton, IHasViewModel<ActionViewModel>
    {
        public ActionDisplay()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(ActionDisplay_Loaded);
        }

        // unset IsTabStop on the GridCell containing us.
        // this is a bad hack to workaround Case 2602
        void ActionDisplay_Loaded(object sender, RoutedEventArgs e)
        {
            var vis = sender as Visual;
            while (vis != null)
            {
                var cell = vis as DataGridCell;
                if (cell != null)
                {
                    cell.IsTabStop = false;
                    break;
                }
                vis = VisualTreeHelper.GetParent(vis) as Visual;
            }
        }

        public ActionViewModel ViewModel
        {
            get { return (ActionViewModel)DataContext; }
        }
    }
}
