// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
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
            if (gridSplitter != null) gridSplitter.IsEnabled = true;
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            _columnWidth = column0.Width;
            column0.Width = GridLength.Auto;
            gridSplitter.IsEnabled = false;
        }
        #endregion
    }
}
