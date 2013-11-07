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
    using System.Linq;
    using System.Text;
    using Zetbox.Client.GUI;
    using System.Windows.Controls;
    using Zetbox.Client.Presentables.GUI;
    using System.ComponentModel;
    using System.Windows.Data;
    using System.Windows;

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
