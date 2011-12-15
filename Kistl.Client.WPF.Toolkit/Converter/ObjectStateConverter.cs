using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Kistl.API;
using System.Windows;

namespace Kistl.Client.WPF.Converter
{
    [ValueConversion(typeof(DataObjectState), typeof(string))]
    public sealed class ObjectStateToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Binding.DoNothing;
            switch ((DataObjectState)value)
            {
                case DataObjectState.New:
                    return "+";
                case DataObjectState.Modified:
                    return "*";
                case DataObjectState.Deleted:
                    return "//";
                default:
                    return Binding.DoNothing;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    [ValueConversion(typeof(DataObjectState), typeof(Visibility))]
    public sealed class ObjectStateToTextVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Binding.DoNothing;
            switch ((DataObjectState)value)
            {
                case DataObjectState.New:
                case DataObjectState.Modified:
                case DataObjectState.Deleted:
                    return Visibility.Visible;
                default:
                    return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
