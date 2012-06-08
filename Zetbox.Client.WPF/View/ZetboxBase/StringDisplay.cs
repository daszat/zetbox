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
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using Zetbox.Client.GUI;
    using Zetbox.Client.WPF.CustomControls;
    using Zetbox.Client.WPF.Converter;
    
    public class StringDisplay : TextBlock // Simplyfiy, often used Control
    {
//    <TextBlock x:Name="txtStringDisplay"
//               Text="{Binding FormattedValue, Mode=OneWay}"
//               ToolTip="{Binding ToolTip}" />
        private static readonly HighlightGridBackgroundConverter _highlightGridBackgroundConverter = new HighlightGridBackgroundConverter();
        private static readonly HighlightGridForegroundConverter _highlightGridForegroundConverter = new HighlightGridForegroundConverter();
        private static readonly HighlightGridFontStyleConverter _highlightGridFontStyleConverter = new HighlightGridFontStyleConverter();
        private static readonly HighlightGridFontWeightConverter _highlightGridFontWeightConverter = new HighlightGridFontWeightConverter();


        public StringDisplay()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;

            // InitializeComponent
            this.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            {
                var b = new Binding("FormattedValue");
                b.Mode = BindingMode.OneWay;
                BindingOperations.SetBinding(this, TextBlock.TextProperty, b);
            }

            {
                var b = new Binding("ToolTip");
                b.Mode = BindingMode.OneWay;
                BindingOperations.SetBinding(this, TextBlock.ToolTipProperty, b);
            }

            {
                var b = new Binding("Highlight");
                b.Mode = BindingMode.OneWay;
                b.Converter = _highlightGridBackgroundConverter;
                BindingOperations.SetBinding(this, TextBlock.BackgroundProperty, b);
            }
            {
                var b = new Binding("Highlight");
                b.Mode = BindingMode.OneWay;
                b.Converter = _highlightGridForegroundConverter;
                BindingOperations.SetBinding(this, TextBlock.ForegroundProperty, b);
            }
            {
                var b = new Binding("Highlight");
                b.Mode = BindingMode.OneWay;
                b.Converter = _highlightGridFontStyleConverter;
                BindingOperations.SetBinding(this, TextBlock.FontStyleProperty, b);
            }
            {
                var b = new Binding("Highlight");
                b.Mode = BindingMode.OneWay;
                b.Converter = _highlightGridFontWeightConverter;
                BindingOperations.SetBinding(this, TextBlock.FontWeightProperty, b);
            }
        }
    }
}
