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
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.SchemaMigration;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.API.Common;
    using System.Threading.Tasks;

    [ViewModelDescriptor]
    public class PropertySelectionTaskViewModel : WindowViewModel
    {
        public new delegate PropertySelectionTaskViewModel Factory(IZetboxContext dataCtx, ViewModel parent, ObjectClass objClass, Func<IEnumerable<Property>, Task> callback);

        private readonly Func<IEnumerable<Property>, Task> _callback;
        private readonly ObjectClass _objClass;

        public PropertySelectionTaskViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            ObjectClass objClass,
            Func<IEnumerable<Property>, Task> callback)
            : base(appCtx, dataCtx, parent)
        {
            if (objClass == null) throw new ArgumentNullException("objClass");
            if (callback == null) throw new ArgumentNullException("callback");

            _callback = callback;
            _objClass = objClass;
        }

        #region Configuration
        private bool _followCompoundObjects = false;
        public bool FollowCompoundObjects
        {
            get { return _followCompoundObjects; }
            set
            {
                _followCompoundObjects = value;
                OnPropertyChanged("FollowCompoundObjects");
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

        private bool _followRelationsMany = false;
        public bool FollowRelationsMany
        {
            get { return _followRelationsMany; }
            set
            {
                _followRelationsMany = value;
                OnPropertyChanged("FollowRelationsMany");
            }
        }

        private bool _followRelationsManyDeep = true;
        /// <summary>
        /// If true and FollowRelationsMany is set to true, n:m relations are followed unlimited.  If false, only the first level is shown.
        /// </summary>
        public bool FollowRelationsManyDeep
        {
            get { return _followRelationsManyDeep; }
            set
            {
                _followRelationsManyDeep = value;
                OnPropertyChanged("FollowRelationsManyDeep");
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
        #endregion

        #region Commands
        public Task<bool> CanChoose()
        {
            return Task.FromResult(SelectedItem != null || MultiSelect);
        }

        public async Task Choose()
        {
            if (!(await CanChoose())) return;

            IList<Property> result = new List<Property>();
            var prop = SelectedItem;
            while (prop != null)
            {
                result.Add(prop.Property);
                prop = prop.ParentProperty;
            }

            await _callback(result.Reverse());
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

        public Task Cancel()
        {
            _callback(null);
            Show = false;

            return Task.CompletedTask;
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
                    _filter = ViewModelFactory.CreateViewModel<StringValueViewModel.Factory>().Invoke(DataContext, this, _filterMdl);
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
                    // TODO: .Result
                    _PossibleValues = _objClass.GetAllProperties()
                        .Result
                        .Where(async i => await IsPropAvailable(i))
                        .Result
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

        private async Task<bool> IsPropAvailable(Property prop)
        {
            if ((FollowRelationsOne || FollowRelationsMany) && prop is ObjectReferenceProperty)
            {
                var objRefProp = (ObjectReferenceProperty)prop;
                return
                    (FollowRelationsOne && !await objRefProp.GetIsList()) ||
                    (FollowRelationsMany && await objRefProp.GetIsList());
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

    public delegate System.Threading.Tasks.Task SelectedPropertySelectionChangedHandler(object sender, SelectedPropertySelectionChangedEventArgs e);

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

        private string _name;
        public override string Name
        {
            get
            {
                Task.Run(async () => await GetName());
                return _name;
            }
        }

        public async Task<string> GetName()
        {
            _name = await _prop.GetLabel();

            OnPropertyChanged(nameof(Name));
            return _name;
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
                    if (Parent.FollowCompoundObjects && _prop is CompoundObjectProperty)
                    {
                        foreach (var prop in ((CompoundObjectProperty)_prop).CompoundObjectDefinition.Properties)
                        {
                            _PossibleValues.Add(ViewModelFactory.CreateViewModel<SelectedPropertyViewModel.Factory>().Invoke(DataContext, Parent, prop, this));
                        }
                    }
                    if ((Parent.FollowRelationsOne || (Parent.FollowRelationsMany && Parent.FollowRelationsManyDeep)) && _prop is ObjectReferenceProperty)
                    {
                        // TODO: .Result calls
                        var objRefProp = (ObjectReferenceProperty)_prop;
                        if ((Parent.FollowRelationsOne && !objRefProp.GetIsList().Result)
                         || (Parent.FollowRelationsMany && Parent.FollowRelationsManyDeep && objRefProp.GetIsList().Result))
                        {
                            foreach (var prop in objRefProp.GetReferencedObjectClass().Result.GetAllProperties().Result)
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
