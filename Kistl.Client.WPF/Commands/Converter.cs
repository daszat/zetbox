
namespace Kistl.Client.WPF.Commands
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
    public class Converter
        : IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Converts a <see cref="Kistl.Client.Presentables.ICommand"/> into a <see cref="System.Windows.Input.ICommand"/> by using the <see cref="WrappedKistlCommand"/>.
        /// </summary>
        /// <param name="value">the command to wrap</param>
        /// <param name="targetType">The parameter is not used.</param>
        /// <param name="parameter">The parameter is not used.</param>
        /// <param name="culture">The parameter is not used.</param>
        /// <returns>A new <see cref="System.Windows.Input.ICommand"/> acting like the specified <paramref name="value"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var command = value as Kistl.Client.Presentables.ICommand;
            if (command != null)
            {
                return new WrappedKistlCommand(command);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="value">This parameter is not used.</param>
        /// <param name="targetType">The parameter is not used.</param>
        /// <param name="parameter">The parameter is not used.</param>
        /// <param name="culture">The parameter is not used.</param>
        /// <returns>This method doesn't return anything.</returns>
        /// <exception cref="NotImplementedException">Always.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}