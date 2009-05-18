
namespace Kistl.Client.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// This class is a simple adapter between the Kistl ICommand and the 
    /// neccessary System.Windows.Input.ICommand for WPF. Requests are simply 
    /// passed through to the underlying command.
    /// </summary>
    public class CommandAdapter
        : System.Windows.Input.ICommand
    {
        /// <summary>
        /// Gets the underlying command object.
        /// </summary>
        public Kistl.Client.Presentables.ICommand AdaptedCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the CommandAdapter class, setting the underlying command.
        /// </summary>
        /// <param name="cmd">the command to adapt</param>
        public CommandAdapter(Kistl.Client.Presentables.ICommand cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException("cmd");
            }
            AdaptedCommand = cmd;
        }

        #region ICommand Members

        /// <summary>
        /// Delegates the <code>bool CanExecute(object)</code> method to the adapted ICommand.
        /// </summary>
        /// <param name="parameter">the data to pass on</param>
        /// <returns>a value indicating whether or not the underlying command is able to execute</returns>
        public bool CanExecute(object parameter)
        {
            return AdaptedCommand.CanExecute(parameter);
        }

        /// <summary>
        /// Passes EventHandler on to the underlying ICommand.CanExecuteChanged event.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { AdaptedCommand.CanExecuteChanged += value; }
            remove { AdaptedCommand.CanExecuteChanged -= value; }
        }

        /// <summary>
        /// Delegates the <code>Execute(object)</code> method to the adapted ICommand.
        /// </summary>
        /// <param name="parameter">the data to pass on</param>
        public void Execute(object parameter)
        {
            AdaptedCommand.Execute(parameter);
        }

        #endregion
    }
}
