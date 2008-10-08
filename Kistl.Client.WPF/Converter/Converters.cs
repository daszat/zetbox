using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Kistl.Client.WPF.Converter
{
    /// <summary>
    /// Converts a number N to (bool)(N &lt; parameter)
    /// </summary>
    [ValueConversion(typeof(object), typeof(bool))]
    public class LessThanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            return Double.Parse(value.ToString()) < Double.Parse(parameter.ToString());
        }

        public object ConvertBack(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            // Readonly
            return Binding.DoNothing;
        }
    }

    /// <summary>
    /// divides a number N by a constant
    /// </summary>
    [ValueConversion(typeof(object), typeof(double))]
    public class DividingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                            object parameter, CultureInfo culture)
        {
            return Double.Parse(value.ToString()) / Double.Parse(parameter.ToString(), CultureInfo.InvariantCulture.NumberFormat);
        }

        public object ConvertBack(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            // Readonly
            return Binding.DoNothing;
        }
    }

    /// <summary>
    /// Converts a number N to (bool)(N &gt; parameter)
    /// </summary>
    [ValueConversion(typeof(object), typeof(bool))]
    public class GreaterThanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            return Double.Parse(value.ToString()) > Double.Parse(parameter.ToString());
        }

        public object ConvertBack(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            // Readonly
            return Binding.DoNothing;
        }
    }

    /**
     * As described on http://learnwpf.com/Posts/Post.aspx?postId=05229e33-fcd4-44d5-9982-a002f2250a64
     */
    [ValueConversion(typeof(object), typeof(string))]
    class FormattingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            string formatString = parameter as string;
            if (formatString != null)
            {
                return string.Format(culture, formatString, value);
            }
            else
            {
                return value != null ? value.ToString() : "";
            }
        }
        public object ConvertBack(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(targetType);
            object targetValue = null;

            try
            {
                if (converter.CanConvertFrom(value.GetType()))
                {
                    targetValue = converter.ConvertFrom(null, culture, value);
                }
            }
            catch
            {
                return null;
            }

            return targetValue;
        }
    }

    /// <summary>
    /// Converts a object to GetType
    /// </summary>
    [ValueConversion(typeof(object), typeof(Type))]
    public class GetTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Binding.DoNothing;
            return value.GetType();
        }

        public object ConvertBack(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            // Readonly
            return Binding.DoNothing;
        }
    }

    /// <summary>
    /// Converts a boolean value of validity into an appropriate background color
    /// </summary>
    [ValueConversion(typeof(object), typeof(Brush))]
    public class ValidityToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value == true) return UserControl.BackgroundProperty.DefaultMetadata.DefaultValue;
            else return Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            // Readonly
            return Binding.DoNothing;
        }
    }

    [ValueConversion(typeof(object), typeof(object))]
    public class DebugConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
