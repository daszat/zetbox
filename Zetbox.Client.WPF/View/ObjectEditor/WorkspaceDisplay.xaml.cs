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
using Zetbox.App.GUI;
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables;
using Zetbox.Client.Presentables.ObjectEditor;
using Zetbox.Client.WPF.CustomControls;
using Zetbox.Client.WPF.Toolkit;

namespace Zetbox.Client.WPF.View.ObjectEditor
{
    /// <summary>
    /// Interaction logic for DesktopView.xaml
    /// </summary>
    public partial class WorkspaceDisplay : WindowView, IHasViewModel<WorkspaceViewModel>
    {
        public WorkspaceDisplay()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        public WorkspaceViewModel ViewModel
        {
            get { return (WorkspaceViewModel)this.DataContext; }
        }

        #region Expander
        private GridLength? _columnWidth;
        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            column0.Width = _columnWidth ?? new GridLength(150);
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            _columnWidth = column0.Width;
            column0.Width = GridLength.Auto;
        }
        #endregion
    }
}
