
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
    public class HighlightGridBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            var h = value as Highlight;
            if (h == null) return Binding.DoNothing;
            if(!string.IsNullOrEmpty(h.GridBackground))
            {
                return h.GridBackground;
            }
            else
            {
                return Application.Current.TryFindResource(h.State + "_HighlightGridBackground");
            }
        }

        public object ConvertBack(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
