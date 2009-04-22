using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.Client.Presentables
{
    public interface ICommand : INotifyPropertyChanged
    {
        /// <summary>
        /// Whether or not this Command is applicable to the current state.
        /// </summary>
        /// <param name="data">may be <value>null</value> if no data is expected</param>
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
        /// Whether or not this command is currently executing.
        /// </summary>
        bool Executing { get; }
        /// <summary>
        /// Occurs when the <see cref="Executing"/> state has changed.
        /// </summary>
        event EventHandler ExecutingChanged;

        /// <summary>
        /// A short descriptive label for this command.
        /// Suitable for display on a button or menu item.
        /// </summary>
        string Label { get; }
        /// <summary>
        /// A longer descriptive text for this command.
        /// Suitable for display in a tool tip.
        /// </summary>
        string ToolTip { get; }
    }

    public abstract class CommandModel : PresentableModel, ICommand
    {
        protected CommandModel(IGuiApplicationContext appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
        }

        #region ICommand Members

        public abstract bool CanExecute(object data);

        public event EventHandler CanExecuteChanged;

        public abstract void Execute(object data);

        private bool _executingCache = false;
        public bool Executing
        {
            get { return _executingCache; }
            protected set
            {
                if (_executingCache != value)
                {
                    _executingCache = value;
                    OnExecutingChanged();
                    OnPropertyChanged("Executing");
                }
            }
        }

        public event EventHandler ExecutingChanged;

        private string _labelCache;
        public string Label
        {
            get { return _labelCache; }
            protected set
            {
                if (_labelCache != value)
                {
                    _labelCache = value;
                    OnPropertyChanged("Label");
                }
            }
        }

        private string _toolTipCache;
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

        #endregion

        protected virtual void OnCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        protected virtual void OnExecutingChanged()
        {
            if (ExecutingChanged != null)
            {
                ExecutingChanged(this, EventArgs.Empty);
            }
        }

    }

}
