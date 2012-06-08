
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
