
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using System.Collections;
    
    /// <summary>
    /// This interface describes common operations and properties of action which can be taken by the user.
    /// </summary>
    public interface ICommandViewModel
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

        /// <summary>
        /// Gets an optional Icon for the Command
        /// </summary>
        Kistl.App.GUI.Icon Icon { get; set; }
    }

    /// <summary>
    /// A little ViewModel to capture a simple command and provide infrastructure to give feedbck on the state of this command.
    /// </summary>
    public abstract class CommandViewModel
        : ViewModel, ICommandViewModel
    {
        /// <summary>
        /// Initializes a new instance of the CommandViewModel class.
        /// </summary>
        /// <param name="appCtx">the application context to use</param>
        /// <param name="dataCtx">the data context to use</param>
        /// <param name="label">a label for this command</param>
        /// <param name="tooltip">a tooltip for this command</param>
        protected CommandViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, string label, string tooltip)
            : base(appCtx, dataCtx)
        {
            if (label == null)
            {
                throw new ArgumentNullException("label");
            }

            this._labelCache = label;
            this._toolTipCache = tooltip;
        }

        protected CommandViewModel(bool desingMode, string label)
            : base(desingMode)
        {
            this._labelCache = label;
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

        private Kistl.App.GUI.Icon _Icon = null;
        public Kistl.App.GUI.Icon Icon
        {
            get
            {
                return _Icon;
            }
            set
            {
                if (_Icon != value)
                {
                    _Icon = value;
                    OnPropertyChanged("Icon");
                }
            }
        }

        public override string Name
        {
            get { return Label; }
        }

        #endregion
    }

    [ViewModelDescriptor]
    public sealed class SimpleCommandViewModel : CommandViewModel
    {
#if MONO
        // See https://bugzilla.novell.com/show_bug.cgi?id=660553
        public delegate SimpleCommandViewModel Factory(IKistlContext dataCtx, string label, string tooltip, Action execute, Func<bool> canExecute);
#else
        public new delegate SimpleCommandViewModel Factory(IKistlContext dataCtx, string label, string tooltip, Action execute, Func<bool> canExecute);
#endif

        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public SimpleCommandViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, string label, string tooltip, Action execute, Func<bool> canExecute)
            : base(appCtx, dataCtx, label, tooltip)
        {
            if (execute == null) throw new ArgumentNullException("execute");
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public SimpleCommandViewModel(bool desingMode, string label)
            : base(desingMode, label)
        {
        }

        public override bool CanExecute(object data)
        {
            if (canExecute == null) return true;
            return canExecute();
        }

        protected override void DoExecute(object data)
        {
            execute();
        }
    }

    public sealed class SimpleParameterCommandViewModel<T> : CommandViewModel
    {
#if MONO
        // See https://bugzilla.novell.com/show_bug.cgi?id=660553
        public delegate SimpleParameterCommandViewModel<T> Factory(IKistlContext dataCtx, string label, string tooltip, Action<T> execute, Func<T, bool> canExecute);
#else
        public new delegate SimpleParameterCommandViewModel<T> Factory(IKistlContext dataCtx, string label, string tooltip, Action<T> execute, Func<T, bool> canExecute);
#endif

        private readonly Action<T> execute;
        private readonly Func<T, bool> canExecute;

        public SimpleParameterCommandViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, string label, string tooltip, Action<T> execute, Func<T, bool> canExecute)
            : base(appCtx, dataCtx, label, tooltip)
        {
            if (execute == null) throw new ArgumentNullException("execute");
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public override bool CanExecute(object data)
        {
            if (canExecute == null) return true;
            if (data is T)
            {
                return canExecute((T)data);
            }
            else
            {
                return canExecute(default(T));
            }
        }

        protected override void DoExecute(object data)
        {
            if (data is T)
            {
                execute((T)data);
            }
            else
            {
                execute(default(T));
            }
        }
    }

    public abstract class ItemCommandViewModel<T> : CommandViewModel
    {
        public ItemCommandViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, string label, string tooltip)
            : base(appCtx, dataCtx, label, tooltip)
        {
        }

        public override bool CanExecute(object data)
        {
            if (data is IEnumerable<T>)
            {
                return ((IEnumerable<T>)data).Count() > 0;
            }
            else if (data is IEnumerable)
            {
                return ((IEnumerable)data).OfType<T>().Count() > 0;
            }
            else return (data is T);
        }

        protected override void DoExecute(object data)
        {
            IEnumerable<T> objects = null;
            if (data is IEnumerable<T>)
            {
                objects = (IEnumerable<T>)data;
            }
            else if (data is IEnumerable)
            {
                objects = ((IEnumerable)data).OfType<T>();
            }
            else if (data is T)
            {
                objects = new T[] { (T)data };
            }
            if (objects == null) return;

            DoExecute(objects);
        }

        protected abstract void DoExecute(IEnumerable<T> data);
    }

    public sealed class SimpleItemCommandViewModel<T> : ItemCommandViewModel<T>
    {
#if MONO
        // See https://bugzilla.novell.com/show_bug.cgi?id=660553
        public delegate SimpleItemCommandViewModel<T> Factory(IKistlContext dataCtx, string label, string tooltip, Action<IEnumerable<T>> execute);
#else
        public new delegate SimpleItemCommandViewModel<T> Factory(IKistlContext dataCtx, string label, string tooltip, Action<IEnumerable<T>> execute);
#endif

        private readonly Action<IEnumerable<T>> execute;

        public SimpleItemCommandViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, string label, string tooltip, Action<IEnumerable<T>> execute)
            : base(appCtx, dataCtx, label, tooltip)
        {
            this.execute = execute;
        }

        protected override void DoExecute(IEnumerable<T> data)
        {
            execute(data);
        }
    }

}
