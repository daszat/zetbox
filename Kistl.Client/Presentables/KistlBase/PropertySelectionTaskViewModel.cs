
namespace Kistl.Client.Presentables.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using ZBox.App.SchemaMigration;
    using Kistl.API.Configuration;
    using Kistl.App.Extensions;
using Kistl.Client.Models;
    using Kistl.Client.Presentables.ValueViewModels;

    [ViewModelDescriptor]
    public class PropertySelectionTaskViewModel : WindowViewModel
    {
        public new delegate PropertySelectionTaskViewModel Factory(IKistlContext dataCtx, ViewModel parent, ObjectClass objClass, Action<IEnumerable<Property>> callback);

        private readonly Action<IEnumerable<Property>> _callback;
        private readonly ObjectClass _objClass;

        public PropertySelectionTaskViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent,
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
        private bool _followRelations = false;

        public bool FollowRelations
        {
            get { return _followRelations; }
            set
            {
                _followRelations = value;
                OnPropertyChanged("FollowRelations");
            }
        }
        #endregion

        #region Commands
        public void Choose()
        {
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
                        Choose, () => SelectedItem != null || MultiSelect);
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
                        Cancel, null);
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
                        .Select(prop => ViewModelFactory.CreateViewModel<SelectedPropertyViewModel.Factory>().Invoke(DataContext, this, prop, null, IsInitialSelected(prop)))
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


        private ILookup<Property, bool> _initialSelectedProps = null;
        public void UpdateInitialSelectedProperties(IEnumerable<Property> props)
        {
            _initialSelectedProps = props.ToLookup(k => k, v => true);
        }
        public bool IsInitialSelected(Property p)
        {
            if (_initialSelectedProps == null) return false;
            return _initialSelectedProps.Contains(p);
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
        public new delegate SelectedPropertyViewModel Factory(IKistlContext dataCtx, PropertySelectionTaskViewModel parent, Property obj, SelectedPropertyViewModel parentProp, bool isSelected);

        private readonly Property _prop;
        private readonly SelectedPropertyViewModel _parent;

        public SelectedPropertyViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx, PropertySelectionTaskViewModel parent,
            Property obj, SelectedPropertyViewModel parentProp, bool isSelected)
            : base(appCtx, dataCtx, parent, obj)
        {
            this._prop = obj;
            this._parent = parentProp;
            this._IsSelected = isSelected;
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
                            _PossibleValues.Add(ViewModelFactory.CreateViewModel<SelectedPropertyViewModel.Factory>().Invoke(DataContext, Parent, prop, this, Parent.IsInitialSelected(prop)));
                        }
                    }
                    if (Parent.FollowRelations && _prop is ObjectReferenceProperty)
                    {
                        foreach (var prop in ((ObjectReferenceProperty)_prop).GetReferencedObjectClass().Properties)
                        {
                            _PossibleValues.Add(ViewModelFactory.CreateViewModel<SelectedPropertyViewModel.Factory>().Invoke(DataContext, Parent, prop, this, Parent.IsInitialSelected(prop)));
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
