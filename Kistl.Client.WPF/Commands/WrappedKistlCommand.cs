
namespace Kistl.Client.WPF.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Wrap a Kistl <see cref="Kistl.Client.Presentables.ICommand"/> into a SmartRoutedUICommand.
    /// </summary>
    public class WrappedKistlCommand
        : SmartRoutedUICommand
    {
        /// <summary>
        /// Initializes a new instance of the WrappedKistlCommand class.
        /// </summary>
        /// <param name="cmd">the command to wrap</param>
        public WrappedKistlCommand(Kistl.Client.Presentables.ICommand cmd)
            : base(cmd.Label, typeof(WrappedKistlCommand))
        {
            _command = cmd;
        }

        /// <summary>
        /// The wrapped command.
        /// </summary>
        private Kistl.Client.Presentables.ICommand _command;

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
