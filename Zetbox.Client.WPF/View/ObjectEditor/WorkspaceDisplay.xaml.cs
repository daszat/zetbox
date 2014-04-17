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

namespace Zetbox.Client.WPF.View.ObjectEditor
{
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
    using Zetbox.API;
    using Zetbox.App.GUI;
    using Zetbox.Client.GUI;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ObjectEditor;
    using Zetbox.Client.WPF.CustomControls;
    using Zetbox.Client.WPF.Toolkit;

    /// <summary>
    /// Interaction logic for DesktopView.xaml
    /// </summary>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public partial class WorkspaceDisplay : WindowView, IHasViewModel<WorkspaceViewModel>, IDragDropTarget, IDragDropSource
    {
        public WorkspaceDisplay()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }
        private WpfDragDropHelper _dragDrop;

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.lstItems.Loaded += (s, e2) => { _dragDrop = new WpfDragDropHelper(lstItems.FindScrollContentPresenter() ?? this.lstItems, this); };
        }

        public WorkspaceViewModel ViewModel
        {
            get { return (WorkspaceViewModel)WPFHelper.SanitizeDataContext(DataContext); }
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

        #region IDragDrop*
        bool IDragDropTarget.CanDrop
        {
            get { return ViewModel != null && ViewModel.CanDrop; }
        }

        string[] IDragDropTarget.AcceptableDataFormats
        {
            get
            {
                return WpfDragDropHelper.ZetboxObjectDataFormats;
            }
        }

        bool IDragDropTarget.OnDrop(string format, object data)
        {
            if (ViewModel == null) return false;
            return ViewModel.OnDrop(data);
        }

        object IDragDropSource.GetData()
        {
            return ViewModel.DoDragDrop();
        }
        #endregion
    }
}
