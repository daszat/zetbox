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
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Wrap a Zetbox <see cref="Zetbox.Client.Presentables.ICommandViewModel"/> into a SmartRoutedUICommand.
    /// </summary>
    public class WrappedZetboxCommand
        : SmartRoutedUICommand
    {
        /// <summary>
        /// Initializes a new instance of the WrappedZetboxCommand class.
        /// </summary>
        /// <param name="cmd">the command to wrap</param>
        public WrappedZetboxCommand(Zetbox.Client.Presentables.ICommandViewModel cmd)
            : base(cmd == null ? String.Empty : cmd.Label, typeof(WrappedZetboxCommand))
        {
            if (cmd == null) { throw new ArgumentNullException("cmd", "No command to wrap"); }

            _command = cmd;
            _command.CanExecuteChanged += (sender, args) => CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// The wrapped command.
        /// </summary>
        private Zetbox.Client.Presentables.ICommandViewModel _command;

        /// <inheritdoc/>
        protected override bool CanExecuteCore(object parameter)
        {
            return _command.CanExecute(parameter);
        }

        /// <inheritdoc/>
        protected override void ExecuteCore(object parameter)
        {
            _command.Execute(parameter);
        }
    }
}
