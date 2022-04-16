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

namespace Zetbox.Client.WPF.View.ZetboxBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using Zetbox.API;
    using Zetbox.Client.GUI;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.Client.WPF.Commands;
    using Zetbox.Client.WPF.CustomControls;
    using Zetbox.Client.WPF.Toolkit;

    /// <summary>
    /// Interaction logic for DataObjectListView.xaml
    /// </summary>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public partial class ObjectReferenceListEditor
        : PropertyEditor, IHasViewModel<ObjectListViewModel>, IDragDropTarget, IDragDropSource
    {
        public ObjectReferenceListEditor()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();

            lst.Loaded += (s, e) => { _dragDrop = new WpfDragDropHelper(lst.FindScrollContentPresenter() ?? lst, this); };
        }
        private WpfDragDropHelper _dragDrop;

        private void ItemActivatedHandler(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.Open();
            }
            e.Handled = true;
        }


        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ViewModel != null && e.Property == FrameworkElement.DataContextProperty)
            {
                ApplyColumns();
                ViewModel.DisplayedColumns.Columns.CollectionChanged += (s, ncc) => ApplyColumns();
                this.ApplyIsBusyBehaviour(ViewModel);
            }
        }

        private void ApplyColumns()
        {
            WPFHelper.RefreshGridView(lst, ViewModel.DisplayedColumns, WpfSortHelper.SortPropertyNameProperty);
        }

        #region IHasViewModel<ObjectListModel> Members

        public ObjectListViewModel ViewModel
        {
            get { return (ObjectListViewModel)WPFHelper.SanitizeDataContext(DataContext); }
        }

        #endregion

        protected override FrameworkElement MainControl
        {
            get { return lst; }
        }

        #region HeaderClickManagement

        protected void SetHeaderTemplate(DependencyObject header, DataTemplate template)
        {
            header.SetValue(GridViewColumn.HeaderTemplateProperty, template);
        }

        private WpfSortHelper _sortHelper;
        protected WpfSortHelper SortHelper
        {
            get
            {
                if (_sortHelper == null)
                {
                    _sortHelper = new WpfSortHelper(this, ViewModel, SetHeaderTemplate);
                }
                return _sortHelper;
            }
        }

        protected void ListView_HeaderClick(object sender, RoutedEventArgs e)
        {
            var header = e.OriginalSource as GridViewColumnHeader;
            if (header != null && header.Role != GridViewColumnHeaderRole.Padding)
            {
                SortHelper.ApplySort(header.Column);
            }
            e.Handled = true;
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
                // Support as may known things as possible
                // A speciallized ViewModel may handle the data
                return WpfDragDropHelper.AllAcceptableDataFormats;
            }
        }

        Task<bool> IDragDropTarget.OnDrop(string format, object data)
        {
            if (ViewModel == null) return Task.FromResult(false);
            return ViewModel.OnDrop(data);
        }
        object IDragDropSource.GetData()
        {
            return ViewModel.DoDragDrop();
        }
        #endregion
    }
}
