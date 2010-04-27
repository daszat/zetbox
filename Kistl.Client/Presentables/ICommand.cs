
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    
    /// <summary>
    /// This interface describes common operations and properties of action which can be taken by the user.
    /// </summary>
    public interface ICommand
        : INotifyPropertyChanged
    {
        /// <summary>
        /// Whether or not this Command is applicable to the current state.
        /// </summary>
        /// <param name="data">may be <value>null</value> if no data is expected</param>
        /// <returns>true if the command can execute with this <paramref name="data"/></returns>
        bool CanExecute(object data);

        /// <summary>
        /// Occurs when the <see cref="CanExecute"/> state has changed.
        /// </summary>
        event EventHandler CanExecuteChanged;

        /// <summary>
        /// Will execute the command with the given parameter.
        /// </summary>
        /// <param name="data">may be <value>null</value> if no data is expected</param>
        void Execute(object data);

        /// <summary>
        /// Gets a value indicating whether or not this command is currently executing.
        /// </summary>
        bool Executing { get; }

        /// <summary>
        /// Occurs when the <see cref="Executing"/> state has changed.
        /// </summary>
        event EventHandler ExecutingChanged;

        /// <summary>
        /// Gets a short descriptive label for this command.
        /// Suitable for display on a button or menu item.
        /// </summary>
        string Label { get; }

        /// <summary>
        /// Gets a longer descriptive text for this command.
        /// Suitable for display in a tool tip.
        /// </summary>
        string ToolTip { get; }
    }

    /// <summary>
    /// A little ViewModel to capture a simple command and provide infrastructure to give feedbck on the state of this command.
    /// </summary>
    public abstract class CommandModel
        : ViewModel, ICommand
    {
        /// <summary>
        /// Initializes a new instance of the CommandModel class.
        /// </summary>
        /// <param name="appCtx">the application context to use</param>
        /// <param name="dataCtx">the data context to use</param>
        /// <param name="label">a label for this command</param>
        /// <param name="tooltip">a tooltip for this command</param>
        protected CommandModel(IViewModelDependencies appCtx, IKistlContext dataCtx, string label, string tooltip)
            : base(appCtx, dataCtx)
        {
            if (label == null)
            {
                throw new ArgumentNullException("label");
            }

            this._labelCache = label;
            this._toolTipCache = tooltip;
        }

        #region ICommand Members

        #region CanExecute handling

        /// <summary>
        /// Whether or not this Command is applicable to the current state.
        /// </summary>
        /// <param name="data">may be <value>null</value> if no data is expected</param>
        /// <returns>true if the command can execute with this <paramref name="data"/></returns>
        public abstract bool CanExecute(object data);

        /// <summary>
        /// Occurs when the <see cref="CanExecute"/> state has changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Is called to invoke the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Execution

        /// <summary>
        /// Will execute the command with the given parameter. This takes care of setting the
        /// <see cref="Executing"/> property and then calls the abstract <see cref="DoExecute"/> method.
        /// </summary>
        /// <param name="data">may be <value>null</value> if no data is expected</param>
        public void Execute(object data)
        {
            Executing = true;
            try
            {
                DoExecute(data);
            }
            finally
            {
                Executing = false;
            }
        }

        /// <summary>
        /// This method should be implemented to actually do the neccessary work of this command.
        /// It will be called by <see cref="Execute"/> after indicating the executing state to clients.
        /// </summary>
        /// <param name="data">may be <value>null</value> if no data is expected</param>
        protected abstract void DoExecute(object data);

        /// <summary>The backing store for the <see cref="Executing"/> property.</summary>
        private bool _executingCache = false;

        /// <summary>
        /// Gets a value indicating whether or not this command is currently executing.
        /// </summary>
        public bool Executing
        {
            get
            {
                return _executingCache;
            }
            private set
            {
                if (_executingCache != value)
                {
                    _executingCache = value;
                    OnExecutingChanged();
                }
            }
        }

        /// <summary>
        /// Occurs when the <see cref="Executing"/> state has changed.
        /// </summary>
        public event EventHandler ExecutingChanged;

        /// <summary>
        /// Is called to invoke the <see cref="ExecutingChanged"/> event.
        /// </summary>
        protected virtual void OnExecutingChanged()
        {
            if (ExecutingChanged != null)
            {
                ExecutingChanged(this, EventArgs.Empty);
            }
            OnPropertyChanged("Executing");
        }

        #endregion

        /// <summary>The backing store for the <see cref="Label"/> property.</summary>
        private string _labelCache;

        /// <summary>
        /// Gets or sets a short descriptive label for this command.
        /// Suitable for display on a button or menu item.
        /// </summary>
        public string Label
        {
            get
            {
                return _labelCache;
            }
            protected set
            {
                if (_labelCache != value)
                {
                    _labelCache = value;
                    OnPropertyChanged("Label");
                }
            }
        }

        /// <summary>The backing store for the <see cref="ToolTip"/> property.</summary>
        private string _toolTipCache;

        /// <summary>
        /// Gets or sets a longer descriptive text for this command.
        /// Suitable for display in a tool tip.
        /// </summary>
        public string ToolTip
        {
            get
            {
                return _toolTipCache;
            }
            protected set
            {
                if (_toolTipCache != value)
                {
                    _toolTipCache = value;
                    OnPropertyChanged("ToolTip");
                }
            }
        }

        public override string Name
        {
            get { return Label; }
        }

        #endregion

    }
}
