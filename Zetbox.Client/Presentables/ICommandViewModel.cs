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

namespace Zetbox.Client.Presentables
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zetbox.API;

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
        System.Drawing.Image Icon { get; set; }

        bool IsDefault { get; }
        bool IsCancel { get; }
    }

    /// <summary>
    /// A little ViewModel to capture a simple command and provide infrastructure to give feedbck on the state of this command.
    /// </summary>
    [ViewModelDescriptor]
    public abstract class CommandViewModel
        : ViewModel, ICommandViewModel
    {
        /// <summary>
        /// Initializes a new instance of the CommandViewModel class.
        /// </summary>
        /// <param name="appCtx">the application context to use</param>
        /// <param name="dataCtx">the data context to use</param>
        /// <param name="parent">a ViewModel which should be notified while this command is executing</param>
        /// <param name="label">a label for this command</param>
        /// <param name="tooltip">a tooltip for this command</param>
        protected CommandViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, string label, string tooltip)
            : base(appCtx, dataCtx, parent)
        {
            if (label == null)
            {
                throw new ArgumentNullException("label");
            }

            this._labelCache = label;
            this._toolTipCache = tooltip;
        }

        protected CommandViewModel(bool designMode, ViewModel parent, string label)
            : base(designMode)
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

        private bool _useDelayedTask = true;

        /// <summary>
        /// Use a delayed Task for executing the command. In most toolkits this will delay the execution so UI Changes can be performed before execution (and after). Default is true.
        /// </summary>
        public virtual bool UseDelayedTask
        {
            get
            {
                return _useDelayedTask;
            }
            set
            {
                if (value != _useDelayedTask)
                {
                    _useDelayedTask = value;
                    OnPropertyChanged("UseDelayedTask");
                }
            }
        }

        /// <summary>
        /// Will execute the command with the given parameter. This takes care of setting the
        /// <see cref="Executing"/> property and then calls the abstract <see cref="DoExecute"/> method.
        /// </summary>
        /// <param name="data">may be <value>null</value> if no data is expected</param>
        public void Execute(object data)
        {
            if (!Executing)
            {
                if (UseDelayedTask)
                {
                    Executing = true;
                    ViewModelFactory.TriggerDelayedTask(Parent, () =>
                    {
                        try
                        {
                            DoExecute(data);
                        }
                        finally
                        {
                            Executing = false;
                        }

                        return Task.CompletedTask;
                    });
                }
                else
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

                    if (Executing)
                        SetBusy();
                    else
                        ClearBusy();
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
        public virtual string Label
        {
            get
            {
                return _labelCache;
            }
            set
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
        public virtual string ToolTip
        {
            get
            {
                return string.IsNullOrEmpty(_reason) ? _toolTipCache : _reason;
            }
            set
            {
                if (_toolTipCache != value)
                {
                    _toolTipCache = value;
                    OnPropertyChanged("ToolTip");
                }
            }
        }

        /// <summary>The backing store for the <see cref="ToolTip"/> property.</summary>
        private string _reason;

        /// <summary>
        /// Gets or sets a longer descriptive text for this command.
        /// Suitable for display in a tool tip.
        /// </summary>
        public string Reason
        {
            get
            {
                return _reason;
            }
            set
            {
                if (_reason != value)
                {
                    _reason = (value ?? string.Empty).Trim();
                    OnPropertyChanged("Reason");
                    OnPropertyChanged("ToolTip");
                }
            }
        }

        public override string Name
        {
            get { return Label; }
        }

        private bool _isDefault = false;
        public bool IsDefault
        {
            get
            {
                return _isDefault;
            }
            set
            {
                if (_isDefault != value)
                {
                    _isDefault = value;
                    OnPropertyChanged("IsDefault");
                }
            }
        }

        private bool _isCancel = false;
        public bool IsCancel
        {
            get
            {
                return _isCancel;
            }
            set
            {
                if (_isCancel != value)
                {
                    _isCancel = value;
                    OnPropertyChanged("IsCancel");
                }
            }
        }
        #endregion
    }

    [ViewModelDescriptor]
    public sealed class SimpleCommandViewModel : CommandViewModel
    {
        /// <summary>
        /// A simple command.
        /// </summary>
        /// <param name="dataCtx"></param>
        /// <param name="parent"></param>
        /// <param name="label">Label</param>
        /// <param name="tooltip">Tooltip</param>
        /// <param name="execute">The action to execure</param>
        /// <param name="canExecute">A function to determinante if the action can be executed. If null, true is assumed.</param>
        /// <param name="getReason">A function to receive a reason why the action cannot be executed. Can be null. If a function is provided, the tooltip will be overridden in case of can execute == false. Thus a simple string can always be returned.</param>
        /// <returns></returns>
        public new delegate SimpleCommandViewModel Factory(IZetboxContext dataCtx, ViewModel parent, string label, string tooltip, Func<Task> execute, Func<Task<bool>> canExecute, Func<Task<string>> getReason);

        private readonly Func<Task> execute;
        private readonly Func<Task<bool>> canExecute;
        private readonly Func<Task<string>> getReason;

        public SimpleCommandViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, string label, string tooltip, Func<Task> execute, Func<Task<bool>> canExecute, Func<Task<string>> getReason)
            : base(appCtx, dataCtx, parent, label, tooltip)
        {
            if (execute == null) throw new ArgumentNullException("execute");
            this.execute = execute;
            this.canExecute = canExecute;
            this.getReason = getReason;
        }

        public SimpleCommandViewModel(bool designMode, ViewModel progressDisplayer, string label)
            : base(designMode, progressDisplayer, label)
        {
        }

        public override bool CanExecute(object data)
        {
            if (DataContext.IsDisposed) return false;
            if (canExecute == null) return true;

            return Task.Run(async () =>
            {
                var canExec = await canExecute();
                if (getReason != null)
                {
                    base.Reason = canExec ? string.Empty : await getReason();
                }
                return canExec;
            }).Result;
        }

        protected override void DoExecute(object data)
        {
            _ = execute();
        }
    }

    public sealed class SimpleParameterCommandViewModel<T> : CommandViewModel
    {
        public new delegate SimpleParameterCommandViewModel<T> Factory(IZetboxContext dataCtx, ViewModel parent, string label, string tooltip, Func<T, Task> execute, Func<T, Task<bool>> canExecute);

        private readonly Func<T, Task> execute;
        private readonly Func<T, Task<bool>> canExecute;

        public SimpleParameterCommandViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, string label, string tooltip, Func<T, Task> execute, Func<T, Task<bool>> canExecute)
            : base(appCtx, dataCtx, parent, label, tooltip)
        {
            if (execute == null) throw new ArgumentNullException("execute");
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public override bool CanExecute(object data)
        {
            if (DataContext.IsDisposed) return false;
            if (canExecute == null) return true;

            return Task.Run(async () =>
            {
                if (data is T)
                {
                    return await canExecute((T)data);
                }
                else
                {
                    return await canExecute(default(T));
                }
            }).Result;
        }

        protected override void DoExecute(object data)
        {
            if (data is T)
            {
                _ = execute((T)data);
            }
            else
            {
                _ = execute(default(T));
            }
        }
    }

    public abstract class ItemCommandViewModel<T> : CommandViewModel
    {
        private readonly bool _ignoreOtherItemTypes;

        public ItemCommandViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel progressDisplayer, string label, string tooltip, bool ignoreOtherItemTypes = true)
            : base(appCtx, dataCtx, progressDisplayer, label, tooltip)
        {
            _ignoreOtherItemTypes = ignoreOtherItemTypes;
        }

        public override bool CanExecute(object data)
        {
            if (DataContext.IsDisposed) return false;

            if (data is IEnumerable<T>)
            {
                return ((IEnumerable<T>)data).Count() > 0;
            }
            else if (data is IEnumerable)
            {
                var numT = ((IEnumerable)data).OfType<T>().Count();
                return numT > 0 && (_ignoreOtherItemTypes || ((IEnumerable<T>)data).Count() == numT);
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
        public new delegate SimpleItemCommandViewModel<T> Factory(IZetboxContext dataCtx, ViewModel parent, string label, string tooltip, Action<IEnumerable<T>> execute);

        private readonly Action<IEnumerable<T>> execute;

        public SimpleItemCommandViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, string label, string tooltip, Action<IEnumerable<T>> execute)
            : base(appCtx, dataCtx, parent, label, tooltip)
        {
            this.execute = execute;
        }

        protected override void DoExecute(IEnumerable<T> data)
        {
            execute(data);
        }
    }

    public class ContainerCommand : CommandViewModel
    {
        public new delegate ContainerCommand Factory(IZetboxContext dataCtx, ViewModel parent, string label, string tooltip, params ICommandViewModel[] children);

        private readonly List<ICommandViewModel> _children;
        public ContainerCommand(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, string label, string tooltip, params ICommandViewModel[] children)
            : base(appCtx, dataCtx, parent, label, tooltip)
        {
            if (children != null)
            {
                _children = children.ToList();
            }
            else
            {
                _children = new List<ICommandViewModel>();
            }
        }

        public ContainerCommand(bool designMode, ViewModel progressDisplayer, string label)
            : base(designMode, progressDisplayer, label)
        {
        }

        public override bool CanExecute(object data)
        {
            return !DataContext.IsDisposed;
        }

        protected override void DoExecute(object data)
        {
            // Nothing to do, just a container
        }

        protected override async Task<System.Collections.ObjectModel.ObservableCollection<ICommandViewModel>> CreateCommands()
        {
            return new ObservableCollection<ICommandViewModel>((await base.CreateCommands()).Concat(_children));
        }
    }
}
