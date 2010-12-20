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
using Kistl.Client.WPF.CustomControls;
using Kistl.Client.WPF.Toolkit;

namespace Kistl.Client.WPF.View.ObjectEditor
{
    /// <summary>
    /// Interaction logic for DesktopView.xaml
    /// </summary>
    public partial class WorkspaceDisplay : WindowView, IHasViewModel<WorkspaceViewModel>
    {
        public WorkspaceDisplay()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ViewModel != null && e.Property == FrameworkElement.DataContextProperty)
            {
                ViewModel.UpdateFromUI += new EventHandler(ViewModel_UpdateFromUI);
            }
        }

        void ViewModel_UpdateFromUI(object sender, EventArgs e)
        {
            WPFHelper.MoveFocus();
        }

        public WorkspaceViewModel ViewModel
        {
            get { return (WorkspaceViewModel)this.DataContext; }
        }
    }
}
