
namespace Kistl.Client.WPF.Converter
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Windows.Data;

    /// <summary>
    /// Converts a Kistl ICommand to a WPF ICommand
    /// </summary>
    [ValueConversion(typeof(Kistl.Client.Presentables.ICommand), typeof(System.Windows.Input.ICommand))]
    public class CommandConverter
        : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var command = value as Kistl.Client.Presentables.ICommand;
            if (command != null)
            {
                return new CommandAdapter(command);
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
