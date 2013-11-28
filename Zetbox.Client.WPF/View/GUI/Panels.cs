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

namespace Zetbox.Client.WPF.View.GUI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using Zetbox.Client.GUI;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.Client.WPF.Toolkit;

    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public class StackPanelView : ItemsControl, IHasViewModel<StackPanelViewModel>
    {
        public StackPanelView()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;

            BindingOperations.SetBinding(this, ItemsControl.ItemsSourceProperty, new Binding("Children") { Mode = BindingMode.OneWay });
            this.ItemsPanel = (ItemsPanelTemplate)FindResource("itemsPanelStackPanelTemplate");
            this.ItemContainerStyle = (Style)FindResource("stackPanelViewItemContainerStyle");
            this.ItemTemplate = (DataTemplate)FindResource("labeledViewContentPresenterTemplate");
        }

        public StackPanelViewModel ViewModel
        {
            get { return (StackPanelViewModel)DataContext; }
        }
    }

    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public class DockPanelView : ItemsControl, IHasViewModel<DockPanelViewModel>
    {
        public DockPanelView()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;

            BindingOperations.SetBinding(this, ItemsControl.ItemsSourceProperty, new Binding("Children") { Mode = BindingMode.OneWay });
            this.ItemsPanel = (ItemsPanelTemplate)FindResource("itemsPanelDockPanelTemplate");
            this.ItemContainerStyle = (Style)FindResource("dockPanelViewItemContainerStyle");
            this.ItemTemplate = (DataTemplate)FindResource("labeledViewContentPresenterTemplate");
        }

        public DockPanelViewModel ViewModel
        {
            get { return (DockPanelViewModel)DataContext; }
        }
    }

    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public class GridPanelView : ItemsControl, IHasViewModel<GridPanelViewModel>
    {
        public GridPanelView()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;

            BindingOperations.SetBinding(this, ItemsControl.ItemsSourceProperty, new Binding("Cells") { Mode = BindingMode.OneWay });
            this.ItemsPanel = (ItemsPanelTemplate)FindResource("itemsPanelGridTemplate");
            this.ItemContainerStyle = (Style)FindResource("gridPanelViewItemContainerStyle");
            this.ItemTemplate = (DataTemplate)FindResource("gridPanelViewItemTemplate");

            this.DataContextChanged += (s, e) => InitGrid(GetItemsPanel(), ViewModel);
            this.Loaded += new RoutedEventHandler(GridPanelView_Loaded);
        }

        void GridPanelView_Loaded(object sender, RoutedEventArgs e)
        {
            InitGrid(GetItemsPanel(), ViewModel);
        }

        private Grid GetItemsPanel()
        {
            ItemsPresenter itemsPresenter = this.FindVisualChild<ItemsPresenter>();
            if (itemsPresenter == null) return null;
            if (System.Windows.Media.VisualTreeHelper.GetChildrenCount(itemsPresenter) == 0) return null;
            return System.Windows.Media.VisualTreeHelper.GetChild(itemsPresenter, 0) as Grid;
        }

        private static void InitGrid(Grid grid, GridPanelViewModel vm)
        {
            if (vm != null && grid != null)
            {
                grid.RowDefinitions.Clear();
                grid.ColumnDefinitions.Clear();
                foreach (var row in vm.Rows)
                {
                    grid.RowDefinitions.Add(new RowDefinition());
                }
                foreach (var col in vm.Columns)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                }

                grid.InvalidateArrange();
                grid.UpdateLayout();
            }
        }

        public GridPanelViewModel ViewModel
        {
            get { return (GridPanelViewModel)DataContext; }
        }
    }

    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public class GroupBoxView : GroupBox, IHasViewModel<GroupBoxViewModel>
    {
        public GroupBoxView()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;

            BindingOperations.SetBinding(this, HeaderProperty, new Binding("Title") { Mode = BindingMode.OneWay });

            var items = new ItemsControl();
            BindingOperations.SetBinding(items, ItemsControl.ItemsSourceProperty, new Binding("Children") { Mode = BindingMode.OneWay });
            items.ItemTemplate = (DataTemplate)FindResource("labeledViewContentPresenterTemplate");
            this.Content = items;
        }

        public GroupBoxViewModel ViewModel
        {
            get { return (GroupBoxViewModel)DataContext; }
        }
    }

    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public class TabControlView : TabControl, IHasViewModel<TabControlViewModel>
    {
        public TabControlView()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            BindingOperations.SetBinding(this, TabControl.ItemsSourceProperty, new Binding("Children") { Mode = BindingMode.OneWay });
            this.ItemTemplate = (DataTemplate)FindResource("titleCellTemplate");
            this.SelectedIndex = 0;
        }

        public TabControlViewModel ViewModel
        {
            get { return (TabControlViewModel)DataContext; }
        }
    }

    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public class TabItemView : ItemsControl, IHasViewModel<TabItemViewModel>
    {
        public TabItemView()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;

            BindingOperations.SetBinding(this, ItemsControl.ItemsSourceProperty, new Binding("Children") { Mode = BindingMode.OneWay });
            this.ItemTemplate = (DataTemplate)FindResource("labeledViewContentPresenterTemplate");
        }

        public TabItemViewModel ViewModel
        {
            get { return (TabItemViewModel)DataContext; }
        }
    }
}
