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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Zetbox.Client.WPF.Converter
{
    /// <summary>
    /// Converts an Zetbox.Icon (ObjectClass, IDataObject, etc) to an Image Control
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

            var result = new Image() { Source = img };
            
            var strParam = parameter as string;
            if (strParam != null)
            {
                var dim = strParam.Split(',');
                if (dim != null && dim.Length == 2)
                {
                    int w, h;
                    if (int.TryParse(dim[0], out w))
                    {
                        result.MaxWidth = w;
                    }
                    if (int.TryParse(dim[1], out h))
                    {
                        result.MaxHeight = h;
                    }
                }
            }
            
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        #endregion
    }
}
