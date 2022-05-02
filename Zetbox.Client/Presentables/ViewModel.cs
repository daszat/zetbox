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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using log4net;
    using Zetbox.API;
    using Zetbox.API.Common;
    using Zetbox.API.Common.GUI;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.GUI;
    using Zetbox.Client.GUI;

    public interface IViewModelDependencies
    {
        /// <summary>
        /// The <see cref="ViewModelFactory"/> of this GUI.
        /// </summary>
        IViewModelFactory Factory { get; }

        /// <summary>
        /// FrozenContext for resolving meta data
        /// </summary>
        IFrozenContext FrozenContext { get; }

        /// <summary>
        /// The current Principal Resolver
        /// </summary>
        IPrincipalResolver PrincipalResolver { get; }

        /// <summary>
        /// IIconConverter instance
        /// </summary>
        IIconConverter IconConverter { get; }

        /// <summary>
        /// IAssetManager instance
        /// </summary>
        IAssetsManager Assets { get; }

        /// <summary>
        /// IValidationManager instance
        /// </summary>
        IValidationManager ValidationManager { get; }

        Zetbox.API.Client.PerfCounter.IPerfCounter PerfCounter { get; }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ViewModelDescriptorAttribute : Attribute
    {
        public ViewModelDescriptorAttribute()
        {
        }
    }

    /// <summary>
    /// This is a marker attribute to document the fact that the attributed class does not need a VMD in the database.
    /// </summary>
    /// <remarks>
    /// Useful when extending a existing ViewModel, or for internally used ViewModels.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class IgnoreViewModelDescriptorAttribute : Attribute
    {
        public IgnoreViewModelDescriptorAttribute()
        {
        }
    }

    /// <summary>
    /// A base class for implementing the ViewModel pattern. This class should proxy the actual
    /// data model into a non-blocking, view-state holding entity. Unless noted differently, members
    /// are not thread-safe and may only be called from the UI thread.
    /// </summary>
    /// <remarks>
    /// See http://blogs.msdn.com/dancre/archive/2006/10/11/datamodel-view-viewmodel-pattern-series.aspx and various others.
    /// </remarks>
    public abstract class ViewModel : INotifyPropertyChanged, IDisposable
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(ViewModel));

        public delegate ViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        private readonly IViewModelDependencies _dependencies;

        /// <summary>
        /// FrozenContext for resolving meta data
        /// </summary>
        protected IFrozenContext FrozenContext { get { return _dependencies.FrozenContext; } }

        /// <summary>
        /// The factory from where new models should be created
        /// </summary>
        public IViewModelFactory ViewModelFactory { get { return _dependencies.Factory; } }

        private UICreator _uiCreator;
        /// <summary>
        /// A helper for easier working with the ViewModelFactory
        /// </summary>
        public UICreator UICreator
        {
            get
            {
                if (_uiCreator == null)
                {
                    _uiCreator = new UICreator(ViewModelFactory, DataContext, this);
                }
                return _uiCreator;
            }
        }


        /// <summary>
        /// The IconConverter instance
        /// </summary>
        public IIconConverter IconConverter { get { return _dependencies.IconConverter; } }

        public IAssetsManager Assets { get { return _dependencies.Assets; } }

        public IValidationManager ValidationManager { get { return _dependencies.ValidationManager; } }

        /// <summary>
        /// A <see cref="IZetboxContext"/> to access the current user's data
        /// </summary>
        protected IZetboxContext DataContext { get; private set; }

        public ViewModel GetWorkspace()
        {
            return ViewModelFactory.GetWorkspace(DataContext);
        }

        private ZetboxPrincipal _CurrentPrincipal = null;
        public ZetboxPrincipal CurrentPrincipal
        {
            get
            {
                if (_CurrentPrincipal == null)
                {
                    // Ensure not null
                    _CurrentPrincipal = new ZetboxPrincipal(0, string.Empty, string.Empty, Enumerable.Empty<ZetboxPrincipalGroup>());
                    var task = Task.Run(async () => _CurrentPrincipal = await _dependencies.PrincipalResolver.GetCurrent());
                    _ = task.ContinueWith(t => OnPropertyChanged(nameof(CurrentPrincipal)), TaskScheduler.Default);
                }
                return _CurrentPrincipal;
            }
        }

        /// <param name="dependencies">The <see cref="IViewModelDependencies"/> to access the current application context</param>
        /// <param name="dataCtx">The <see cref="IZetboxContext"/> to access the current user's data session</param>
        /// <param name="parent">The parent <see cref="ViewModel"/> to ...</param>
        protected ViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent)
        {
            _errorCache = new ValidationError(this);
            _errorCache.Errors.CollectionChanged += (s, e) => OnErrorChanged();
            _errorCache.Children.CollectionChanged += (s, e) => OnErrorChanged();

            _parent = parent;
            IsInDesignMode = false;
            _dependencies = dependencies;
            DataContext = dataCtx;

            if (_parent != null) _parent.PropertyChanged += (s, e) => { if (e.PropertyName == "Highlight" || e.PropertyName == "HighlightAsync") OnHighlightChanged(); };
            dataCtx.IsElevatedModeChanged += new EventHandler(dataCtx_IsElevatedModeChanged);

            _dependencies.PerfCounter.IncrementViewModel();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dependencies.PerfCounter.DecrementViewModel();
            }
        }

        void dataCtx_IsElevatedModeChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("IsEnabled");
            OnHighlightChanged();
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
            set { _RequestedKind = value; OnPropertyChanged("RequestedKind"); }
        }

        private int _isBusy = 0;
        public bool IsBusy
        {
            get
            {
                return _isBusy > 0;
            }
        }

        public void SetBusy(string message = null)
        {
            _isBusy++;
            OnPropertyChanged("IsBusy");

            CurrentBusyMessage = message ?? ViewModelResources.DefaultBusyMessage;

            if (_isBusy == 1)
                OnPropertyChanged("IsEnabled");
        }

        public void ClearBusy()
        {
            _isBusy--;
            if (_isBusy == 0)
                OnPropertyChanged("IsEnabled");

            if (_isBusy < 0)
            {
                _isBusy = 0;
                Logging.Log.Warn("ClearBusy called too often or without a prev. SetBusy call");
            }
            OnPropertyChanged("IsBusy");
        }

        private string _currentBusyMessage;
        public string CurrentBusyMessage
        {
            get
            {
                return _currentBusyMessage;
            }
            set
            {
                if (_currentBusyMessage != value)
                {
                    _currentBusyMessage = value;
                    OnPropertyChanged("CurrentBusyMessage");
                }
            }
        }

        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get
            {
                if (IsBusy) return false;
                if (DataContext.IsElevatedMode) return true;
                return _isEnabled;
            }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    OnPropertyChanged("IsEnabled");
                    OnHighlightChanged();
                }
            }
        }

        /// <summary>
        /// A common "name" of this Model. May be used for generic filtering or displaying.
        /// </summary>
        public abstract string Name { get; }

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
            if (_PropertyChangedEvent == null) return;

            Task.CompletedTask.ContinueWith(t =>_PropertyChangedEvent(this, new PropertyChangedEventArgs(propertyName)), TaskScheduler.Default);
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
        /// No <see cref="IZetboxContext"/>s is available.
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

        /// <summary>
        /// Override this to create commands associated with this view model. The view is responsible to display them.
        /// </summary>
        /// <returns></returns>
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
                if (!IsEnabled) return Highlight.Deactivated;
                if (Parent != null && Parent.Highlight != Highlight.None) return Parent.Highlight;
                return Highlight.None;
            }
        }

        public virtual Highlight HighlightAsync
        {
            get
            {
                if (!IsEnabled) return Highlight.Deactivated;
                if (Parent != null && Parent.HighlightAsync != Highlight.None) return Parent.HighlightAsync;
                return Highlight.None;
            }
        }

        public void OnHighlightChanged()
        {
            OnPropertyChanged("Highlight");
            OnPropertyChanged("HighlightAsync");
        }

        private System.Drawing.Image _icon;
        public virtual System.Drawing.Image Icon
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
                    OnIconChanged();
                }
            }
        }
        public virtual System.Drawing.Image IconAsync
        {
            get
            {
                return Icon;
            }
        }

        public void OnIconChanged()
        {
            OnPropertyChanged("Icon");
            OnPropertyChanged("IconAsync");
        }
        #endregion

        #region Help command
        private ICommandViewModel _HelpCommand = null;

        /// <summary>
        /// It's not the question _if_ you should display a help command, the question is _where_!
        /// </summary>
        public ICommandViewModel HelpCommand
        {
            get
            {
                if (_HelpCommand == null)
                {
                    _HelpCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, ViewModelResources.HelpCommand, ViewModelResources.HelpCommand_Tooltip, Help, CanHelp, null);
                    Task.Run(async () => _HelpCommand.Icon = await IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.tip_png.Find(FrozenContext)));
                }
                return _HelpCommand;
            }
        }

        public bool CanHelp()
        {
            return HasHelpText;
        }

        public void Help()
        {
            if (!CanHelp()) return;

            // The basic implementation - a toolkit might implement it better
            var dlg = ViewModelFactory.CreateDialog(DataContext, ViewModelResources.HelpCommand)
                .AddTextBlock("help", ViewModelResources.HelpCommand, HelpText);
            dlg.Show();
        }

        public bool HasHelpText
        {
            get
            {
                return !string.IsNullOrWhiteSpace(HelpText);
            }
        }

        private string _helpText;
        public string HelpText
        {
            get
            {
                return _helpText ?? GetHelpText();
            }
            set
            {
                if (_helpText != value)
                {
                    _helpText = value;
                    OnPropertyChanged("HelpText");
                    OnPropertyChanged("HasHelpText");
                }
            }
        }

        protected virtual string GetHelpText()
        {
            return string.Empty;
        }
        #endregion

        #region Error management
        private ValidationError _errorCache;
        public ValidationError ValidationError
        {
            get
            {
                return _errorCache;
            }
        }

        public virtual bool IsValid
        {
            get
            {
                return !_errorCache.HasErrors;
            }
        }

        /// <summary>
        /// If there are errors, futher validation is not needed
        /// If there are no errors, another validation yield return errors
        /// </summary>
        protected virtual bool NeedsValidation
        {
            get
            {
                return IsEnabled;
            }
        }

        public ValidationError Validate()
        {
            // Reset error cache
            _errorCache.Errors.Clear();
            _errorCache.Children.Clear();

            DoValidate();

            if (_errorCache == null)
            {
                _errorCache = new ValidationError(this);
            }

            // notify
            ValidationManager.Notify(this);
            OnErrorChanged();

            // return error object for convenience
            return _errorCache;
        }

        /// <summary>
        /// Validates this ViewModel. If implemented, this method should update add a validation error
        /// </summary>
        protected virtual void DoValidate()
        {
        }

        private void OnErrorChanged()
        {
            OnPropertyChanged("Error");
            OnPropertyChanged("IsValid");
            OnPropertyChanged("ValidationError");
        }
        #endregion
    }

    public struct Highlight
    {
        #region Static Highlight States
        public static readonly Highlight None = new Highlight(HighlightState.None);
        public static readonly Highlight Good = new Highlight(HighlightState.Good);
        public static readonly Highlight Neutral = new Highlight(HighlightState.Neutral);
        public static readonly Highlight Bad = new Highlight(HighlightState.Bad);
        public static readonly Highlight Archived = new Highlight(HighlightState.Archived);
        public static readonly Highlight Deactivated = new Highlight(HighlightState.Deactivated);
        public static readonly Highlight Active = new Highlight(HighlightState.Active);
        public static readonly Highlight Output = new Highlight(HighlightState.Output);
        public static readonly Highlight Input = new Highlight(HighlightState.Input);
        public static readonly Highlight Calculation = new Highlight(HighlightState.Calculation);
        public static readonly Highlight Info = new Highlight(HighlightState.Info);
        public static readonly Highlight Warning = new Highlight(HighlightState.Warning);
        public static readonly Highlight Error = new Highlight(HighlightState.Error);
        public static readonly Highlight Fatal = new Highlight(HighlightState.Fatal);
        public static readonly Highlight ActionRequired = new Highlight(HighlightState.ActionRequired);
        public static readonly Highlight Note = new Highlight(HighlightState.Note);
        #endregion

        public Highlight(HighlightState state)
            : this(state, null, null, System.Drawing.FontStyle.Regular, null)
        {
        }

        public Highlight(string gridBackground, string gridForeground, System.Drawing.FontStyle gridFontStyle, string panelBackground)
            : this(HighlightState.None, gridBackground, gridForeground, gridFontStyle, panelBackground)
        {
        }

        public Highlight(HighlightState state, string gridBackground, string gridForeground, System.Drawing.FontStyle gridFontStyle, string panelBackground)
            : this()
        {
            this.State = state;
            this.GridBackground = gridBackground;
            this.GridForeground = gridForeground;
            this.GridFontStyle = gridFontStyle;
            this.PanelBackground = panelBackground;
        }

        public HighlightState State { get; private set; }
        public string GridBackground { get; private set; }
        public string GridForeground { get; private set; }
        public System.Drawing.FontStyle GridFontStyle { get; private set; }
        public string PanelBackground { get; private set; }

        public override int GetHashCode()
        {
            return State.GetHashCode() +
                (GridBackground ?? string.Empty).GetHashCode() +
                (GridForeground ?? string.Empty).GetHashCode() +
                GridFontStyle.GetHashCode() +
                (PanelBackground ?? string.Empty).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            // If parameter cannot be cast to ThreeDPoint return false:
            var b = obj as Highlight?;
            if (b.HasValue == false)
            {
                return false;
            }

            // Return true if the fields match:
            return this.State == b.Value.State &&
                this.GridBackground == b.Value.GridBackground &&
                this.GridForeground == b.Value.GridForeground &&
                this.GridFontStyle == b.Value.GridFontStyle &&
                this.PanelBackground == b.Value.PanelBackground;
        }

        public static bool operator ==(Highlight a, Highlight b)
        {
            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.State == b.State &&
                a.GridBackground == b.GridBackground &&
                a.GridForeground == b.GridForeground &&
                a.GridFontStyle == b.GridFontStyle &&
                a.PanelBackground == b.PanelBackground;
        }

        public static bool operator !=(Highlight a, Highlight b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return string.Format("{0} ({1}/{2}/{3}/{4})", State, GridBackground, GridForeground, GridFontStyle, PanelBackground);
        }
    }

    internal class DesignerDependencies : IViewModelDependencies
    {
        public IViewModelFactory Factory
        {
            get { throw new NotImplementedException(); }
        }

        public IFrozenContext FrozenContext
        {
            get { throw new NotImplementedException(); }
        }

        public IPrincipalResolver PrincipalResolver
        {
            get { throw new NotImplementedException(); }
        }

        public IIconConverter IconConverter
        {
            get { throw new NotImplementedException(); }
        }

        public IAssetsManager Assets
        {
            get { throw new NotImplementedException(); }
        }

        public IValidationManager ValidationManager
        {
            get { throw new NotImplementedException(); }
        }


        public API.Client.PerfCounter.IPerfCounter PerfCounter
        {
            get { throw new NotImplementedException(); }
        }
    }
}
