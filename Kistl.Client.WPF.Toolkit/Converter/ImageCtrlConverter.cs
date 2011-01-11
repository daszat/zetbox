using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Kistl.Client.WPF.Converter
{
    /// <summary>
    /// Converts an ZBox.Icon (ObjectClass, IDataObject, etc) to an Image Control
    /// </summary>
    /// <remarks>
    /// Used for bind Icons to MenuItems throug a Style. See:
    /// http://connect.microsoft.com/VisualStudio/feedback/details/497408/wpf-menuitem-icon-cannot-be-set-via-setter
    /// http://stackoverflow.com/questions/30239/wpf-setting-a-menuitem-icon-in-code
    /// http://stackoverflow.com/questions/1495489/unable-to-set-system-windows-controls-menuitem-icon-thru-a-setter
    /// </remarks>
    [ValueConversion(typeof(object), typeof(Image))]
    public class ImageCtrlConverter : IValueConverter
    {
        private readonly IconConverter _iconConverter;

        public ImageCtrlConverter(IconConverter iconConverter)
        {
            this._iconConverter = iconConverter;
        }

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var img = _iconConverter.Convert(value, targetType, parameter, culture) as BitmapImage;
            if (img == null) return Binding.DoNothing;
            return new Image() { Source = img };
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        #endregion
    }
}
