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
    
    public sealed class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                              object parameter, CultureInfo culture)
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

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            // not implemented
            return Binding.DoNothing;
        }
    }
}
