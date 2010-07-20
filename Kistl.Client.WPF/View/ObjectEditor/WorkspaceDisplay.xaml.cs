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

using Kistl.App.GUI;
using Kistl.Client.GUI;
using Kistl.Client.Presentables;
using Kistl.Client.Presentables.ObjectEditor;

namespace Kistl.Client.WPF.View.ObjectEditor
{
    /// <summary>
    /// Interaction logic for DesktopView.xaml
    /// </summary>
    public partial class WorkspaceDisplay : Window, IHasViewModel<WorkspaceViewModel>
    {
        public WorkspaceDisplay()
        {
            InitializeComponent();
        }

        public WorkspaceViewModel ViewModel
        {
            get { return (WorkspaceViewModel)this.DataContext; }
        }
    }
}
