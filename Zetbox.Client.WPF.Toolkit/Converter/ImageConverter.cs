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
namespace Zetbox.Client.WPF.Converter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Data;
    using System.Windows.Media.Imaging;
    using System.Globalization;
    using System.IO;
    using Zetbox.API.Utils;
    
    public sealed class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                              object parameter, CultureInfo culture)
        {
            try
            {
                if (value is string)
                {
                    return new BitmapImage(new Uri((string)value));
                }
                else if (value is System.IO.Stream)
                {
                    var bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.StreamSource = (System.IO.Stream)value;
                    bmp.EndInit();
                    return bmp;
                }
                else if (value is System.Drawing.Image)
                {
                    var bmp = new BitmapImage();
                    bmp.BeginInit();
                    var ms = new MemoryStream();
                    var img = (System.Drawing.Image)value;
                    img.Save(ms, img.RawFormat);
                    ms.Seek(0, SeekOrigin.Begin);
                    bmp.StreamSource = ms;
                    bmp.EndInit();
                    return bmp;
                }
                else
                {
                    return Binding.DoNothing;
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Info("Error while converting image", ex);
                return Binding.DoNothing;
            }
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            // not implemented
            return Binding.DoNothing;
        }
    }
}
