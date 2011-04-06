
namespace Kistl.Client.WPF.Converter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Data;
    using Kistl.Client.Presentables;
    using System.Windows;

    [ValueConversion(typeof(Highlight), typeof(object))]
    public abstract class HighlightConverter : IValueConverter
    {
        protected abstract string GetValue(Highlight h);
        protected abstract string GetValueType();

        public object Convert(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            var h = value as Highlight;
            if (h == null) return Binding.DoNothing;
            var color = GetValue(h);
            if (!string.IsNullOrEmpty(color))
            {
                return color;
            }
            else
            {
                return Application.Current.TryFindResource(string.Format("HighlightState_{0}_{1}", h.State.ToString(), GetValueType()));
            }
        }

        public object ConvertBack(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    [ValueConversion(typeof(Highlight), typeof(object))]
    public class HighlightGridBackgroundConverter : HighlightConverter
    {
        protected override string GetValue(Highlight h)
        {
            return h.GridBackground;
        }

        protected override string GetValueType()
        {
            return "GridBackground";
        }
    }

    [ValueConversion(typeof(Highlight), typeof(object))]
    public class HighlightGridForegroundConverter : HighlightConverter
    {
        protected override string GetValue(Highlight h)
        {
            return h.GridForeground;
        }

        protected override string GetValueType()
        {
            return "GridForeground";
        }
    }

    [ValueConversion(typeof(Highlight), typeof(object))]
    public class HighlightGridFontStyleConverter : HighlightConverter
    {
        protected override string GetValue(Highlight h)
        {
            switch(h.GridFontStyle)
            {
                case System.Drawing.FontStyle.Regular:
                    return string.Empty;
                default:
                    return h.GridFontStyle.ToString();
            }
        }

        protected override string GetValueType()
        {
            return "GridFontStyle";
        }
    }

    [ValueConversion(typeof(Highlight), typeof(object))]
    public class HighlightPanelBackgroundConverter : HighlightConverter
    {
        protected override string GetValue(Highlight h)
        {
            return h.PanelBackground;
        }

        protected override string GetValueType()
        {
            return "PanelBackground";
        }
    }

}
