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

    public class HelpButton
        : Button
    {
        static HelpButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(HelpButton),
                new FrameworkPropertyMetadata(typeof(HelpButton)));
        }

        public HelpButton()
        {
            //this.SetBinding(CommandProperty, new Binding("HelpCommand")
            //{
            //    Converter = new Commands.Converter()
            //});
            // Override default command behaviour
            this.Click += new RoutedEventHandler(HelpButton_Click);

            this.SetBinding(VisibilityProperty, new Binding("HasHelpText")
            {
                Converter = (IValueConverter)Application.Current.Resources["BooleanToVisibilityConverter"]
            });

            this.SetBinding(ToolTipProperty, new Binding("HelpCommand.ToolTip"));

            this.SetValue(ToolTipService.ShowOnDisabledProperty, true);
            this.Loaded += new RoutedEventHandler(CommandButton_Loaded);
        }

        void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            var vmdl = DataContext as ViewModel;
            if (vmdl != null && vmdl.HasHelpText)
            {
                var html = new WebBrowser();
                var header = "<html><body style=\"background:#FFFFE0;font-family: Verdana, Arial; font-size: 0.8em;\">";
                var footer = "</body></html>";
                html.NavigateToString(header + vmdl.HelpText + footer);

                var wnd = new Window();
                wnd.Style = Application.Current.Resources["PopupWindow"] as Style;
                wnd.Title = vmdl.HelpCommand.Label;
                wnd.Content = html;

                wnd.Show();
            }
        }

        void CommandButton_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                this.Content = "Help Button";
            }
            this.CommandTarget = this;
        }
    }
}
