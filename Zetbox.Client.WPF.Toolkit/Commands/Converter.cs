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

namespace Zetbox.Client.WPF.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Windows.Data;

    /// <summary>
    /// Converts a Zetbox ICommandViewModel to a WPF ICommand
    /// </summary>
    [ValueConversion(typeof(Zetbox.Client.Presentables.ICommandViewModel), typeof(System.Windows.Input.ICommand))]
    public class Converter
        : IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Converts a <see cref="Zetbox.Client.Presentables.ICommandViewModel"/> into a <see cref="System.Windows.Input.ICommand"/> by using the <see cref="WrappedZetboxCommand"/>.
        /// </summary>
        /// <param name="value">the command to wrap</param>
        /// <param name="targetType">The parameter is not used.</param>
        /// <param name="parameter">The parameter is not used.</param>
        /// <param name="culture">The parameter is not used.</param>
        /// <returns>A new <see cref="System.Windows.Input.ICommand"/> acting like the specified <paramref name="value"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var command = value as Zetbox.Client.Presentables.ICommandViewModel;
            if (command != null)
            {
                return new WrappedZetboxCommand(command);
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