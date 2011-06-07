
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Common;
    using Kistl.App.Base;
    using Kistl.App.GUI;
    using Kistl.Client.Presentables.ValueViewModels;

    public interface IViewModelDependencies
    {
        /// <summary>
        /// The <see cref="ViewModelFactory"/> of this GUI.
        /// </summary>
        IViewModelFactory Factory { get; }

        /// <summary>
        /// A <see cref="IThreadManager"/> for the UI Thread
        /// </summary>
        IUiThreadManager UiThread { get; }
        /// <summary>
        /// A <see cref="IThreadManager"/> for asynchronous Tasks
        /// </summary>
        IAsyncThreadManager AsyncThread { get; }

        /// <summary>
        /// FrozenContext for resolving meta data
        /// </summary>
        IFrozenContext FrozenContext { get; }

        /// <summary>
        /// The current Identity Resolver
        /// </summary>
        IIdentityResolver IdentityResolver { get; }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ViewModelDescriptorAttribute : Attribute
    {
        public ViewModelDescriptorAttribute()
        {
        }
    }

    /// <summary>
    /// A base class for implementing the ViewModel pattern. This class proxies the actual
    /// data model into a non-blocking, view-state holding entity. Unless noted differently, members
    /// are not thread-safe and may only be called from the UI thread.
    /// </summary>
    /// See http://blogs.msdn.com/dancre/archive/2006/10/11/datamodel-view-viewmodel-pattern-series.aspx
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public delegate ViewModel Factory(IKistlContext dataCtx, ViewModel parent);

        private readonly IViewModelDependencies _dependencies;

        /// <summary>
        /// A <see cref="IThreadManager"/> for the UI Thread
        /// </summary>
        protected IUiThreadManager UI { get { return _dependencies.UiThread; } }
        /// <summary>
        /// A <see cref="IThreadManager"/> for asynchronous Tasks
        /// </summary>
        protected IAsyncThreadManager Async { get { return _dependencies.AsyncThread; } }

        /// <summary>
        /// FrozenContext for resolving meta data
        /// </summary>
        protected IFrozenContext FrozenContext { get { return _dependencies.FrozenContext; } }

        /// <summary>
        /// The factory from where new models should be created
        /// </summary>
        public IViewModelFactory ViewModelFactory { get { return _dependencies.Factory; } }

        /// <summary>
        /// A <see cref="IKistlContext"/> to access the current user's data
        /// </summary>
        protected IKistlContext DataContext { get; private set; }

        private Identity _CurrentIdentity = null;
        public Identity CurrentIdentity
        {
            get
            {
                if (_CurrentIdentity == null)
                {
                    _CurrentIdentity = _dependencies.IdentityResolver.GetCurrent();
                }
                return _CurrentIdentity;
            }
        }

        private readonly CalculatedProperties _calcProperties;

        /// <param name="dependencies">The <see cref="IViewModelDependencies"/> to access the current application context</param>
        /// <param name="dataCtx">The <see cref="IKistlContext"/> to access the current user's data session</param>
        /// <param name="parent">The parent <see cref="ViewModel"/> to ...</param>
        protected ViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent)
        {
            _parent = parent;
            IsInDesignMode = false;
            _dependencies = dependencies;
            DataContext = dataCtx;
            _calcProperties = new CalculatedProperties(dataCtx, this);

            if (_parent != null) _parent.PropertyChanged += (s, e) => { if (e.PropertyName == "Highlight") OnPropertyChanged("Highlight"); };
        }

        #region Public interface

        private readonly ViewModel _parent;
        public ViewModel Parent
        {
            get
            {
                return _parent;
            }
        }

        /// <summary>
        /// Used to override DefaultKind in code
        /// </summary>
        private ControlKind _RequestedKind;
        public virtual ControlKind RequestedKind
        {
            get { return _RequestedKind; }
            set { _RequestedKind = value; OnPropertyChanged("ControlKind"); }
        }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged("IsBusy");
                }
            }
        }

        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    OnPropertyChanged("IsEnabled");
                    OnPropertyChanged("Highlight");
                }
            }
        }

        /// <summary>
        /// A common "name" of this Model. May be used for generic filtering or displaying.
        /// </summary>
        public abstract string Name { get; }

        public IDictionary<string, ViewModel> CalculatedPropertyModelsByName
        {
            get
            {
                return _calcProperties;
            }
        }

        protected CalculatedProperties CalculatedProperties
        {
            get
            {
                return _calcProperties;
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        private event PropertyChangedEventHandler _PropertyChangedEvent;
        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                _PropertyChangedEvent += value;
            }
            remove
            {
                _PropertyChangedEvent -= value;
            }
        }

        /// <summary>
        /// Notifies all listeners of PropertyChanged about a change in a property
        /// </summary>
        /// <param name="propertyName">the changed property</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (_PropertyChangedEvent != null)
                _PropertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Design Mode

        /// <summary>
        /// Creates a ViewModel in "design" mode
        /// </summary>
        /// <param name="designMode">always must be true</param>
        /// <seealso cref="IsInDesignMode"/>
        protected ViewModel(bool designMode)
        {
            if (!designMode)
            {
                throw new ArgumentOutOfRangeException("designMode", "always has to be true");
            }

            IsInDesignMode = true;
            _dependencies = new DesignerDependencies();
        }

        /// <summary>
        /// Signifies that a model is in "design" mode or really accessing the data store.
        /// </summary>
        /// In design mode, no data store is used and only mock data is shown. 
        /// No <see cref="IKistlContext"/>s or <see cref="IThreadManager"/>s are available.
        public bool IsInDesignMode { get; private set; }

        #endregion

        #region Commands

        protected ObservableCollection<ICommandViewModel> commandsStore;
        public ObservableCollection<ICommandViewModel> Commands
        {
            get
            {
                if (commandsStore == null)
                {
                    commandsStore = CreateCommands();
                }
                return commandsStore;
            }
        }

        protected virtual ObservableCollection<ICommandViewModel> CreateCommands()
        {
            return new ObservableCollection<ICommandViewModel>();
        }
        
        #endregion

        #region ColorManagement/Icon
        /// <summary>
        /// Override in custom ViewModels to provide an information about hightlighting
        /// </summary>
        public virtual Highlight Highlight
        {
            get
            {
                if (Parent != null && Parent.Highlight != null) return Parent.Highlight;
                if (!IsEnabled) return Highlight.Deactivated;
                return null;
            }
        }

        private Icon _icon;
        public virtual Icon Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                if (_icon != value)
                {
                    _icon = value;
                    OnPropertyChanged("Icon");
                }
            }
        }
        #endregion

    }

    public class Highlight : INotifyPropertyChanged
    {
        #region Static Highlight States
        public static readonly Highlight Good = new Highlight(HighlightState.Good, true);
        public static readonly Highlight Neutral = new Highlight(HighlightState.Neutral, true);
        public static readonly Highlight Bad = new Highlight(HighlightState.Bad, true);
        public static readonly Highlight Archived = new Highlight(HighlightState.Archived, true);
        public static readonly Highlight Deactivated = new Highlight(HighlightState.Deactivated, true);
        public static readonly Highlight Active = new Highlight(HighlightState.Active, true);
        public static readonly Highlight Output = new Highlight(HighlightState.Output, true);
        public static readonly Highlight Input = new Highlight(HighlightState.Input, true);
        public static readonly Highlight Calculation = new Highlight(HighlightState.Calculation, true);
        public static readonly Highlight Info = new Highlight(HighlightState.Info, true);
        public static readonly Highlight Warning = new Highlight(HighlightState.Warning, true);
        public static readonly Highlight Error = new Highlight(HighlightState.Error, true);
        public static readonly Highlight Fatal = new Highlight(HighlightState.Fatal, true);
        public static readonly Highlight ActionRequired = new Highlight(HighlightState.ActionRequired, true);
        public static readonly Highlight Note = new Highlight(HighlightState.Note, true);
        #endregion

        public Highlight()
            : this(0)
        {
        }

        public Highlight(HighlightState state)
        {
            this.stateStore = state;
        }

        private Highlight(HighlightState state, bool freeze)
        {
            this.stateStore = state;
            this._fozen = freeze;
        }

        private bool _fozen = false;

        protected HighlightState stateStore;
        public virtual HighlightState State
        {
            get
            {
                // return an ZToolBox Enum
                // Translated into a Workspace Backround and List Back/Foreground & Font Style
                return stateStore;
            }
            set
            {
                if (_fozen) throw new InvalidOperationException("Cannot change a constant Highlight");
                if (stateStore != value)
                {
                    stateStore = value;
                    OnPropertyChanged("State");
                }
            }
        }

        protected string gridBackgroundStore;
        public virtual string GridBackground
        {
            get
            {
                return gridBackgroundStore;
            }
            set
            {
                if (_fozen) throw new InvalidOperationException("Cannot change a constant Highlight");
                if (gridBackgroundStore != value)
                {
                    gridBackgroundStore = value;
                    OnPropertyChanged("GridBackground");
                }
            }
        }

        protected string gridForegroundStore;
        public virtual string GridForeground
        {
            get
            {
                return gridForegroundStore;
            }
            set
            {
                if (_fozen) throw new InvalidOperationException("Cannot change a constant Highlight");
                if (gridForegroundStore != value)
                {
                    gridForegroundStore = value;
                    OnPropertyChanged("GridForeground");
                }
            }
        }

        protected System.Drawing.FontStyle gridFontStyleStore;
        public virtual System.Drawing.FontStyle GridFontStyle
        {
            get
            {
                return gridFontStyleStore;
            }
            set
            {
                if (_fozen) throw new InvalidOperationException("Cannot change a constant Highlight");
                if (gridFontStyleStore != value)
                {
                    gridFontStyleStore = value;
                    OnPropertyChanged("GridFontStyle");
                }
            }
        }

        protected string panelBackgroundStore;
        public virtual string PanelBackground
        {
            get
            {
                return panelBackgroundStore;
            }
            set
            {
                if (_fozen) throw new InvalidOperationException("Cannot change a constant Highlight");
                if (panelBackgroundStore != value)
                {
                    panelBackgroundStore = value;
                    OnPropertyChanged("PanelBackground");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string p)
        {
            var temp = PropertyChanged;
            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(p));
            }
        }
    }

    internal class DesignerDependencies : IViewModelDependencies
    {
        public IViewModelFactory Factory
        {
            get { throw new NotImplementedException(); }
        }

        private IUiThreadManager _thread = new SynchronousThreadManager();
        public IUiThreadManager UiThread
        {
            get { return _thread; }
        }

        public IAsyncThreadManager AsyncThread
        {
            get { throw new InvalidOperationException("No asynchronous operations allowed in Design mode"); }
        }

        public IFrozenContext FrozenContext
        {
            get { throw new NotImplementedException(); }
        }

        public IIdentityResolver IdentityResolver
        {
            get { throw new NotImplementedException(); }
        }
    }
}
