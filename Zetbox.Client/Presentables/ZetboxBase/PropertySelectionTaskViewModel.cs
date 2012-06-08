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

namespace Zetbox.Client.Presentables.ZetboxBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.SchemaMigration;
    using Zetbox.API.Configuration;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.API.Utils;

    [ViewModelDescriptor]
    public class PropertySelectionTaskViewModel : WindowViewModel
    {
        public new delegate PropertySelectionTaskViewModel Factory(IZetboxContext dataCtx, ViewModel parent, ObjectClass objClass, Action<IEnumerable<Property>> callback);

        private readonly Action<IEnumerable<Property>> _callback;
        private readonly ObjectClass _objClass;

        public PropertySelectionTaskViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            ObjectClass objClass,
            Action<IEnumerable<Property>> callback)
            : base(appCtx, dataCtx, parent)
        {
            if (objClass == null) throw new ArgumentNullException("objClass");
            if (callback == null) throw new ArgumentNullException("callback");

            _callback = callback;
            _objClass = objClass;
        }

        #region Configuration
        private bool _followCompundObjects = false;

        public bool FollowCompundObjects
        {
            get { return _followCompundObjects; }
            set
            {
                _followCompundObjects = value;
                OnPropertyChanged("FollowCompundObjects");
            }
        }
        private bool _followRelationsOne = false;
        public bool FollowRelationsOne
        {
            get { return _followRelationsOne; }
            set
            {
                _followRelationsOne = value;
                OnPropertyChanged("FollowRelationsOne");
            }
        }

        private bool _showCalculated = true;
        public bool ShowCalculated
        {
            get { return _showCalculated; }
            set
            {
                _showCalculated = value;
                OnPropertyChanged("ShowCalculated");
            }
        }

        private bool _followRelationsMany = false;
        public bool FollowRelationsMany
        {
            get { return _followRelationsMany; }
            set
            {
                _followRelationsMany = value;
                OnPropertyChanged("FollowRelationsMany");
                if (value == true)
                {
                    Logging.Client.Warn("PropertySelectionTaskViewModel.FollowRelationsMany was set to true, this is not supported by the linq predicate builder yet");
                }
            }
        }
        #endregion

        #region Commands
        public bool CanChoose()
        {
            return SelectedItem != null || MultiSelect;
        }

        public void Choose()
        {
            if (!CanChoose()) return;

            IList<Property> result = new List<Property>();
            var prop = SelectedItem;
            while (prop != null)
            {
                result.Add(prop.Property);
                prop = prop.ParentProperty;
            }

            _callback(result.Reverse());
            Show = false;
        }

        private ICommandViewModel _ChooseCommand = null;
        public ICommandViewModel ChooseCommand
        {
            get
            {
                if (_ChooseCommand == null)
                {
                    _ChooseCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        PropertySelectionTaskViewModelResources.ChooseCommand,
                        PropertySelectionTaskViewModelResources.ChooseCommand_Tooltip,
                        Choose, CanChoose, null);
                }
                return _ChooseCommand;
            }
        }

        public void Cancel()
        {
            _callback(null);
            Show = false;
        }

        private ICommandViewModel _CancelCommand = null;
        public ICommandViewModel CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, null,
                        PropertySelectionTaskViewModelResources.CancelCommand,
                        PropertySelectionTaskViewModelResources.CancelCommand_Tooltip,
                        Cancel, null, null);
                }
                return _CancelCommand;
            }
        }
        #endregion

        private ClassValueModel<string> _filterMdl;
        private ViewModel _filter;
        public ViewModel Filter
        {
            get
            {
                if (_filter == null)
                {
                    _filterMdl = new ClassValueModel<string>(
                        PropertySelectionTaskViewModelResources.Filter,
                        PropertySelectionTaskViewModelResources.Filter_Description,
                        true, false);
                    _filterMdl.PropertyChanged += (s, e) => { if (e.PropertyName == "Value") OnPropertyChanged("PossibleValues"); };
                    _filter = ViewModelFactory.CreateViewModel<ClassValueViewModel<string>.Factory>().Invoke(DataContext, this, _filterMdl);
                }
                return _filter;
            }
        }

        private List<SelectedPropertyViewModel> _PossibleValues = null;
        public IEnumerable<SelectedPropertyViewModel> PossibleValues
        {
            get
            {
                if (_PossibleValues == null)
                {
                    _PossibleValues = _objClass.GetAllProperties()
                        .Where(i => IsPropAvailable(i))
                        .Select(prop => ViewModelFactory.CreateViewModel<SelectedPropertyViewModel.Factory>().Invoke(DataContext, this, prop, null))
                        .OrderBy(i => i.Name)
                        .ToList();
                }
                if (_filterMdl != null && !string.IsNullOrEmpty(_filterMdl.Value))
                {
                    var str = _filterMdl.Value.ToLowerInvariant();
                    return _PossibleValues.Where(i => i.Name.ToLowerInvariant().Contains(str));
                }
                else
                {
                    return _PossibleValues;
                }
            }
        }

        private bool IsPropAvailable(Property prop)
        {
            if ((FollowRelationsOne || FollowRelationsMany) && prop is ObjectReferenceProperty)
            {
                var objRefProp = (ObjectReferenceProperty)prop;
                return
                    (FollowRelationsOne && !objRefProp.GetIsList()) ||
                    (FollowRelationsMany && objRefProp.GetIsList());
            }
            else if (prop.IsCalculated() && !ShowCalculated)
            {
                return false;
            }
            return true;
        }

        private SelectedPropertyViewModel _SelectedItem = null;
        public SelectedPropertyViewModel SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        public override string Name
        {
            get { return PropertySelectionTaskViewModelResources.Name; }
        }

        private bool _MultiSelect = false;
        public bool MultiSelect
        {
            get
            {
                return _MultiSelect;
            }
            set
            {
                if (_MultiSelect != value)
                {
                    _MultiSelect = value;
                    OnPropertyChanged("MultiSelect");
                }
            }
        }

        public void OnSelectedPropertySelectionChanged(SelectedPropertyViewModel i)
        {
            var temp = SelectedPropertySelectionChanged;
            if (temp != null)
            {
                temp(this, new SelectedPropertySelectionChangedEventArgs(i));
            }
        }

        public event SelectedPropertySelectionChangedHandler SelectedPropertySelectionChanged;


        private ILookup<Property[], bool> _initialSelectedProps = null;
        public void UpdateInitialSelectedProperties(IEnumerable<Property[]> props)
        {
            _initialSelectedProps = props.Where(i => i != null).ToLookup(k => k, v => true);
        }
        public bool IsInitialSelected(Property[] p)
        {
            if (_initialSelectedProps == null) return false;
            return _initialSelectedProps.Any(i => i.Key.SequenceEqual(p));
        }
    }

    public class SelectedPropertySelectionChangedEventArgs : EventArgs
    {
        public SelectedPropertySelectionChangedEventArgs(SelectedPropertyViewModel i)
        {
            this.Item = i;
        }
        public SelectedPropertyViewModel Item { get; private set; }
    }

    public delegate void SelectedPropertySelectionChangedHandler(object sender, SelectedPropertySelectionChangedEventArgs e);

    public class SelectedPropertyViewModel : DataObjectViewModel
    {
        public new delegate SelectedPropertyViewModel Factory(IZetboxContext dataCtx, PropertySelectionTaskViewModel parent, Property obj, SelectedPropertyViewModel parentProp);

        private readonly Property _prop;
        private readonly SelectedPropertyViewModel _parent;

        public SelectedPropertyViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, PropertySelectionTaskViewModel parent,
            Property obj, SelectedPropertyViewModel parentProp)
            : base(appCtx, dataCtx, parent, obj)
        {
            this._prop = obj;
            this._parent = parentProp;
            this._IsSelected = parent.IsInitialSelected(Properties);
        }

        public override string Name
        {
            get
            {
                return _prop.Name;
            }
        }

        public Property Property
        {
            get
            {
                return _prop;
            }
        }

        public Property[] Properties
        {
            get
            {
                return this
                    .AndParents(i => new[] { i.Property }, p => p.ParentProperty)
                    .AsEnumerable()
                    .Reverse()
                    .ToArray();
            }
        }


        public new PropertySelectionTaskViewModel Parent
        {
            get
            {
                return (PropertySelectionTaskViewModel)base.Parent;
            }
        }

        public SelectedPropertyViewModel ParentProperty
        {
            get
            {
                return _parent;
            }
        }

        private List<SelectedPropertyViewModel> _PossibleValues = null;
        public IEnumerable<SelectedPropertyViewModel> PossibleValues
        {
            get
            {
                if (_PossibleValues == null)
                {
                    _PossibleValues = new List<SelectedPropertyViewModel>();
                    if (Parent.FollowCompundObjects && _prop is CompoundObjectProperty)
                    {
                        foreach (var prop in ((CompoundObjectProperty)_prop).CompoundObjectDefinition.Properties)
                        {
                            _PossibleValues.Add(ViewModelFactory.CreateViewModel<SelectedPropertyViewModel.Factory>().Invoke(DataContext, Parent, prop, this));
                        }
                    }
                    if ((Parent.FollowRelationsOne || Parent.FollowRelationsMany) && _prop is ObjectReferenceProperty)
                    {
                        var objRefProp = (ObjectReferenceProperty)_prop;
                        if ((Parent.FollowRelationsOne && !objRefProp.GetIsList()) ||
                            (Parent.FollowRelationsMany && objRefProp.GetIsList()))
                        {
                            foreach (var prop in objRefProp.GetReferencedObjectClass().Properties)
                            {
                                _PossibleValues.Add(ViewModelFactory.CreateViewModel<SelectedPropertyViewModel.Factory>().Invoke(DataContext, Parent, prop, this));
                            }
                        }
                    }

                    _PossibleValues = _PossibleValues.OrderBy(i => i.Name).ToList();
                }
                return _PossibleValues;
            }
        }

        private bool _IsSelected = false;
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                if (_IsSelected != value)
                {
                    _IsSelected = value;
                    Parent.OnSelectedPropertySelectionChanged(this);
                    OnPropertyChanged("IsSelected");
                }
            }
        }
    }
}
