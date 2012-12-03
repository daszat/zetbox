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

namespace Zetbox.Client.Presentables.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Common;
    using Zetbox.App.GUI;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Presentables.ZetboxBase;
    using Zetbox.API.Utils;

    [ViewModelDescriptor]
    public abstract class NavigationEntryViewModel
        : DataObjectViewModel
    {
        public new delegate NavigationEntryViewModel Factory(IZetboxContext dataCtx, ViewModel parent, NavigationEntry screen);

        public static NavigationEntryViewModel Fetch(IViewModelFactory ModelFactory, IZetboxContext dataCtx, ViewModel parent, NavigationEntry screen)
        {
            if (ModelFactory == null) throw new ArgumentNullException("ModelFactory");
            if (screen == null) throw new ArgumentNullException("screen");

            return (NavigationEntryViewModel)dataCtx.GetViewModelCache(ModelFactory.PerfCounter).LookupOrCreate(screen, () =>
            {
                if (screen.ViewModelDescriptor != null)
                {
                    try
                    {
                        var t = screen.ViewModelDescriptor.ViewModelRef.AsType(true);
                        return ModelFactory.CreateViewModel<NavigationEntryViewModel.Factory>(t).Invoke(dataCtx, parent, screen);
                    }
                    catch (Exception ex)
                    {
                        Logging.Client.WarnFormat("Unable to create ViewModel from Descriptor: {0}", ex);
                    }
                }
                return (NavigationEntryViewModel)screen.GetDefaultViewModel(dataCtx, parent);
            });
        }

        private readonly NavigationEntry _screen;
        private NavigationEntryViewModel _parent;

        private NavigatorViewModel _displayer = null;

        public NavigationEntryViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, NavigationEntry screen)
            : base(dependencies, dataCtx, parent, screen)
        {
            if (screen == null) throw new ArgumentNullException("screen");

            _screen = screen;
            _screen.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_screen_PropertyChanged);
        }

        private bool? _hasAccess = null;
        public bool HasAccess
        {
            get
            {
                if (_hasAccess == null)
                {
                    if (CurrentIdentity == null)
                    {
                        _hasAccess = false;
                    }
                    else if (_screen.Groups.Count != 0 && !CurrentIdentity.IsAdmininistrator() && !_screen.Groups.Any(g => CurrentIdentity.Groups.Any(grp => grp.ExportGuid == g.ExportGuid)))
                    {
                        _hasAccess = false;
                    }
                    else
                    {
                        _hasAccess = true;
                    }
                }
                return _hasAccess.Value;
            }
        }

        public override ControlKind RequestedKind
        {
            get
            {
                if (!HasAccess) return NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_AccessDeniedDataObjectKind.Find(FrozenContext);
                return _screen.RequestedKind ?? base.RequestedKind;
            }
            set
            {
                base.RequestedKind = value;
            }
        }

        void _screen_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Title":
                    OnPropertyChanged("Name");
                    OnPropertyChanged("Title");
                    break;
                case "Parent":
                    OnPropertyChanged("Parent");
                    break;
                case "Children":
                    OnPropertyChanged("Children");
                    break;
            }
        }

        public abstract ICommandViewModel ExecuteCommand { get; }

        public override string Name
        {
            get { return _screen.Title; }
        }

        public string Title
        {
            get { return _screen.Title; }
        }

        public override Highlight Highlight
        {
            get
            {
                // Don't call base, since it's readonly, deactivated would be returned
                // Deactivated on !IsEnabled is OK
                if (!IsEnabled) return Highlight.Deactivated;
                return Highlight.None;
            }
        }

        public override Highlight HighlightAsync
        {
            get
            {
                // Don't call base, since it's readonly, deactivated would be returned
                // Deactivated on !IsEnabled is OK
                if (!IsEnabled) return Highlight.Deactivated;
                return Highlight.None;
            }
        }

        public NavigationEntry Screen { get { return _screen; } }

        public Guid ExportGuid { get { return _screen.ExportGuid; } }

        public NavigatorViewModel Displayer
        {
            get
            {
                return _displayer;
            }
            set
            {
                if (_displayer != value)
                {
                    _displayer = value;
                    foreach (var c in Children)
                    {
                        c.Displayer = value;
                    }
                    OnPropertyChanged("Displayer");
                }
            }
        }

        public NavigationEntryViewModel ParentScreen
        {
            get
            {
                if (_parent == null && _screen.Parent != null)
                {
                    _parent = Fetch(ViewModelFactory, DataContext, this._displayer, _screen.Parent);
                }
                return _parent;
            }
        }

        private ObservableCollection<NavigationEntryViewModel> _children;
        private ReadOnlyObservableCollection<NavigationEntryViewModel> _childrenRO;
        public ReadOnlyObservableCollection<NavigationEntryViewModel> Children
        {
            get
            {
                if (HasAccess && _childrenRO == null)
                {
                    _children = new ObservableCollection<NavigationEntryViewModel>();
                    foreach (var s in _screen.Children.Where(c => c.Groups.Count == 0 || CurrentIdentity.IsAdmininistrator() || c.Groups.Any(g => CurrentIdentity.Groups.Select(grp => grp.ExportGuid).Contains(g.ExportGuid))))
                    {
                        _children.Add(NavigationEntryViewModel.Fetch(ViewModelFactory, DataContext, this, s));
                    }
                    _childrenRO = new ReadOnlyObservableCollection<NavigationEntryViewModel>(_children);
                }
                return _childrenRO;
            }
        }

        private ObservableCollection<CommandViewModel> _additionalCommandsRW;
        protected ObservableCollection<CommandViewModel> AdditionalCommandsRW
        {
            get
            {
                if (HasAccess && _additionalCommandsRW == null)
                {
                    _additionalCommandsRW = new ObservableCollection<CommandViewModel>(CreateAdditionalCommands());
                }
                return _additionalCommandsRW;
            }
        }

        private ReadOnlyObservableCollection<CommandViewModel> _additionalCommands;
        public ReadOnlyObservableCollection<CommandViewModel> AdditionalCommands
        {
            get
            {
                if (_additionalCommands == null)
                {
                    _additionalCommands = new ReadOnlyObservableCollection<CommandViewModel>(AdditionalCommandsRW);
                }
                return _additionalCommands;
            }
        }

        protected virtual List<CommandViewModel> CreateAdditionalCommands()
        {
            return new List<CommandViewModel>();
        }

        public string Color
        {
            get
            {
                var tmp = _screen;
                while (tmp != null)
                {
                    if (!string.IsNullOrEmpty(tmp.Color)) return tmp.Color;
                    tmp = tmp.Parent;
                }
                return null;
            }
        }

        /// <summary>
        /// Indicates that a Nav-Entry can be displayed. A Search screen would return true, a Action would return false
        /// </summary>
        public abstract bool IsScreen
        {
            get;
        }

        /// <summary>
        /// Indicates that a navigation entry should stay visible, contains, and "renders" its children itself.
        /// </summary>
        /// <remarks>
        /// Tabbed screens are examples for Containers.
        /// </remarks>
        public abstract bool IsContainer { get; }

        /// <summary>
        /// The currently selected child entry on this NavigationEntry.
        /// </summary>
        public abstract NavigationEntryViewModel SelectedEntry
        {
            get;
            set;
        }

        private NavigationEntryViewModel _current;
        /// <summary>
        /// The currently displayed entry in this part of the hierarchy.
        /// </summary>
        public NavigationEntryViewModel CurrentScreen
        {
            get
            {
                if (_current == null)
                    _current = GetInitialScreen();
                return _current;
            }
            set
            {
                if (_current != value)
                {
                    _current = value;
                    UpdateContainer();
                    OnPropertyChanged("CurrentScreen");
                }
            }
        }

        protected virtual NavigationEntryViewModel GetInitialScreen()
        {
            return this;
        }

        private void UpdateContainer()
        {
            // select the top-most container that is within this container, or the current screen
            ContainerScreen = CurrentScreen
                .AndParents(s => s.ParentScreen)
                .TakeWhile(s => s != this.ParentScreen)
                .Where(s => s.IsContainer)
                .LastOrDefault()
                ?? CurrentScreen;
        }

        private NavigationEntryViewModel _container;
        /// <summary>
        /// The top-most container of the CurrentScreen
        /// </summary>
        public NavigationEntryViewModel ContainerScreen
        {
            get
            {
                if (_container == null) UpdateContainer();
                return _container;
            }
            private set
            {
                if (_container != value)
                {
                    _container = value;
                    OnPropertyChanged("ContainerScreen");
                }
            }
        }

        #region ReportProblemCommand
        private ICommandViewModel _ReportProblemCommand = null;
        public ICommandViewModel ReportProblemCommand
        {
            get
            {
                if (_ReportProblemCommand == null)
                {
                    _ReportProblemCommand = ViewModelFactory.CreateViewModel<ReportProblemCommand.Factory>().Invoke(DataContext, this);
                }
                return _ReportProblemCommand;
            }
        }
        #endregion

    }
}
