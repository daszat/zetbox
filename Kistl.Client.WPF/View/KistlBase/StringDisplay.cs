
namespace Kistl.Client.WPF.View.KistlBase
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
    using Kistl.Client.GUI;
    using Kistl.Client.WPF.CustomControls;
    using Kistl.Client.WPF.Converter;
    
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
