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
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Zetbox.App.GUI;
using Zetbox.Client.WPF.Toolkit;
using System.Windows;

namespace Zetbox.Client.WPF.Converter
{
    /// <summary>
    /// This converter does nothing
    /// </summary>
    [ValueConversion(typeof(object), typeof(object))]
    public class NoopConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    /// <summary>
    /// Converts a number N to (bool)(N &lt; parameter)
    /// </summary>
    [ValueConversion(typeof(object), typeof(bool))]
    public class LessThanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) { throw new ArgumentNullException("value"); }
            if (parameter == null) { throw new ArgumentNullException("parameter"); }

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
            if (value == null) { throw new ArgumentNullException("value"); }
            if (parameter == null) { throw new ArgumentNullException("parameter"); }

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
    /// multiplies a number N by a constant
    /// </summary>
    [ValueConversion(typeof(object), typeof(double))]
    public class MultiplyingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                            object parameter, CultureInfo culture)
        {
            if (value == null) { throw new ArgumentNullException("value"); }
            if (parameter == null) { throw new ArgumentNullException("parameter"); }

            return Double.Parse(value.ToString()) * Double.Parse(parameter.ToString(), CultureInfo.InvariantCulture.NumberFormat);
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
            if (value == null) { throw new ArgumentNullException("value"); }
            if (parameter == null) { throw new ArgumentNullException("parameter"); }

            return Double.Parse(value.ToString()) > Double.Parse(parameter.ToString());
        }

        public object ConvertBack(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            // Readonly
            return Binding.DoNothing;
        }
    }

    /// <summary>
    /// As described on http://learnwpf.com/Posts/Post.aspx?postId=05229e33-fcd4-44d5-9982-a002f2250a64
    /// </summary>
    [ValueConversion(typeof(object), typeof(string))]
    public class FormattingConverter : IValueConverter
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
                return value != null ? value.ToString() : String.Empty;
            }
        }
        public object ConvertBack(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) { throw new ArgumentNullException("value"); }

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

    /// <summary>
    /// Converts a number of hours into a readable string
    /// </summary>
    [ValueConversion(typeof(object), typeof(string))]
    public class HourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            double hours = (double)value;
            return String.Format("{0:00}:{1:00}:{2:00}", (int)hours, ((int)(hours * 60)) % 60, ((int)(hours * 3600)) % 60);
        }

        public object ConvertBack(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            // Readonly
            return Binding.DoNothing;
        }
    }

    /// <summary>
    /// Inverts a bool
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class InvertedBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null ? !(bool?)value : null;
        }

        public object ConvertBack(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null ? !(bool?)value : null;
        }
    }

    
    /// <summary>
    /// 
    /// </summary>
    [ValueConversion(typeof(bool), typeof(SelectionMode))]
    public class BooleanMultiselectToSelectionModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            return value is bool && (bool)value ? SelectionMode.Extended : SelectionMode.Single;
        }

        public object ConvertBack(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ValueConversion(typeof(bool), typeof(SelectionMode))]
    public class SystemDrawingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Binding.DoNothing;
            var type = value.GetType();

            if (type == typeof(System.Drawing.Point))
            {
                var p = (System.Drawing.Point)value;
                return new System.Windows.Thickness(p.X, p.Y, 0, 0);
            }
            else if (type == typeof(System.Drawing.PointF))
            {
                var p = (System.Drawing.PointF)value;
                return new System.Windows.Thickness(p.X, p.Y, 0, 0);
            }
            else if (type == typeof(System.Drawing.Rectangle))
            {
                var p = (System.Drawing.Rectangle)value;
                return new System.Windows.Thickness(p.X, p.Y, p.Right, p.Bottom);
            }
            else if (type == typeof(System.Drawing.RectangleF))
            {
                var p = (System.Drawing.RectangleF)value;
                return new System.Windows.Thickness(p.X, p.Y, p.Right, p.Bottom);
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    public abstract class AbstractShadeConverter : IValueConverter
    {
        protected abstract Color ShadeColor(Color value, float amount = 0.5f);

        public object Convert(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Binding.DoNothing;
            var type = value.GetType();
            Color color;
            float amount;

            if (!(parameter is string) || !float.TryParse((string)parameter, NumberStyles.Any, CultureInfo.GetCultureInfo("en-us"), out amount))
            {
                amount = 0.5f;
            }

            if (type == typeof(string))
            {
                var str = (string)value;
                if (string.IsNullOrWhiteSpace(str)) return Binding.DoNothing;
                color = (Color)ColorConverter.ConvertFromString(str);
            }
            else if (type == typeof(Color))
            {
                color = (Color)value;
            }
            else
            {
                return value;
            }

            color = ShadeColor(color, amount);
            if (typeof(Color).IsAssignableFrom(targetType))
            {
                return color;
            }
            else if (typeof(Brush).IsAssignableFrom(targetType))
            {
                return new SolidColorBrush(color);
            }
            else
            {
                return Binding.DoNothing;
            }
        }

        public object ConvertBack(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    [ValueConversion(typeof(object), typeof(object))]
    public class LighterShadeConverter : AbstractShadeConverter
    {
        public static Color MakeLighter(Color value, float lighter = 0.5f)
        {
            return Color.FromScRgb(value.ScA,
                (1.0f - lighter) * value.ScR + lighter,
                (1.0f - lighter) * value.ScG + lighter,
                (1.0f - lighter) * value.ScB + lighter);
        }

        protected override Color ShadeColor(Color value, float lighter = 0.5f)
        {
            return MakeLighter(value, lighter);
        }
    }

    [ValueConversion(typeof(object), typeof(object))]
    public class DarkerShadeConverter : AbstractShadeConverter
    {
        public static Color MakeDarker(Color value, float darker = 0.5f)
        {
            return Color.FromScRgb(value.ScA,
                (1.0f - darker) * value.ScR,
                (1.0f - darker) * value.ScG,
                (1.0f - darker) * value.ScB);
        }

        protected override Color ShadeColor(Color value, float lighter = 0.5f)
        {
            return MakeDarker(value, lighter);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ValueConversion(typeof(bool), typeof(System.Windows.Visibility))]
    public class BooleanToVisibilityHiddenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            return value is bool && (bool)value ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ValueConversion(typeof(bool), typeof(System.Windows.Visibility))]
    public class BooleanToInvisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            return value is bool && (bool)value ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    /// <summary>
    /// Convert a WidthHint into a DIP count
    /// </summary>
    [ValueConversion(typeof(WidthHint), typeof(double))]
    public class WidthHintConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                            object parameter, CultureInfo culture)
        {
            return WPFHelper.TranslateWidth(value as WidthHint?);
        }

        public object ConvertBack(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            // Readonly
            return Binding.DoNothing;
        }
    }

    /// <summary>
    /// Convert a WidthHint into a DIP count
    /// </summary>
    [ValueConversion(typeof(object), typeof(System.Windows.Controls.Dock))]
    public class DockConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                            object parameter, CultureInfo culture)
        {
            if (value is Zetbox.Client.Presentables.GUI.DockPanelViewModel.Dock)
            {
                switch((Zetbox.Client.Presentables.GUI.DockPanelViewModel.Dock)value)
                {
                    case Presentables.GUI.DockPanelViewModel.Dock.Left:
                        return System.Windows.Controls.Dock.Left;
                    case Presentables.GUI.DockPanelViewModel.Dock.Right:
                        return System.Windows.Controls.Dock.Right;
                    case Presentables.GUI.DockPanelViewModel.Dock.Top:
                        return System.Windows.Controls.Dock.Top;
                    case Presentables.GUI.DockPanelViewModel.Dock.Bottom:
                        return System.Windows.Controls.Dock.Bottom;
                    case Presentables.GUI.DockPanelViewModel.Dock.Fill:
                        return null;
                }
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            // Readonly
            return Binding.DoNothing;
        }
    }

    /// <summary>
    /// Convert a WidthHint into a DIP count
    /// </summary>
    [ValueConversion(typeof(WidthHint), typeof(double))]
    public class PercentToGridLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                            object parameter, CultureInfo culture)
        {
            var dbl = value as double?;
            if (dbl != null)
            {
                return new GridLength(dbl.Value, GridUnitType.Star);
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            // Readonly
            return Binding.DoNothing;
        }
    }
}
