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

namespace Zetbox.Client.WPF.CustomControls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.WPF.Converter;
    using Zetbox.Client.WPF.Toolkit;

    public class CommandButton
        : Button
    {
        public CommandButton()
        {
            this.SetBinding(CommandProperty, new Binding("CommandViewModel") { RelativeSource = RelativeSource.Self, Converter = new Commands.Converter() });
            this.SetBinding(ContentProperty, new Binding("CommandViewModel.Label") { RelativeSource = RelativeSource.Self });
            this.SetBinding(ToolTipProperty, new Binding("CommandViewModel.ToolTip") { RelativeSource = RelativeSource.Self });
            this.SetBinding(ImageProperty, new Binding("CommandViewModel.Icon") { RelativeSource = RelativeSource.Self, Converter = (IValueConverter)Application.Current.Resources["ImageConverter"] });
            this.SetBinding(IsDefaultProperty, new Binding("CommandViewModel.IsDefault") { RelativeSource = RelativeSource.Self });
            this.SetBinding(IsCancelProperty, new Binding("CommandViewModel.IsCancel") { RelativeSource = RelativeSource.Self });

            this.SetValue(ToolTipService.ShowOnDisabledProperty, true);

            this.Loaded += new RoutedEventHandler(CommandButton_Loaded);
        }

        void CommandButton_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                this.Content = "Command Button";
            }
            this.CommandTarget = this;
        }

        public ICommandViewModel CommandViewModel
        {
            get { return (ICommandViewModel)GetValue(CommandViewModelProperty); }
            set { SetValue(CommandViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandViewModelProperty =
            DependencyProperty.Register("CommandViewModel", typeof(ICommandViewModel), typeof(CommandButton), new PropertyMetadata(OnCommandViewModelChanged));

        private static void OnCommandViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = d as FrameworkElement;
            var value = e.NewValue as ViewModel;
            if (self != null && value != null)
            {
                WPFHelper.ApplyIsBusyBehaviour(self, value);
            }
        }

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Image.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(CommandButton));


    }
}
