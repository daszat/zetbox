
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
                    _ChooseCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Choose", "Select the current destination property",
                        Choose, () => SelectedItem != null);
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
                    _CancelCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, null, "Cancel", "Abort selecting a destination property", Cancel, null);
                }
                return _CancelCommand;
            }
        }
        #endregion

        private List<SelectedPropertyViewModel> _PossibleValues = null;
        public IEnumerable<SelectedPropertyViewModel> PossibleValues
        {
            get
            {
                if (_PossibleValues == null)
                {
                    _PossibleValues = new List<SelectedPropertyViewModel>();
                    foreach (var prop in _objClass.GetAllProperties())
                    {
                        _PossibleValues.Add(ViewModelFactory.CreateViewModel<SelectedPropertyViewModel.Factory>().Invoke(DataContext, this, prop, null));
                    }
                }
                return _PossibleValues;
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
            get { return "Select a destination property"; }
        }
    }

    public class SelectedPropertyViewModel : DataObjectViewModel
    {
        public new delegate SelectedPropertyViewModel Factory(IKistlContext dataCtx, PropertySelectionTaskViewModel parent, Property obj, SelectedPropertyViewModel parentProp);

        private readonly Property _prop;
        private readonly SelectedPropertyViewModel _parent;

        public SelectedPropertyViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx, PropertySelectionTaskViewModel parent,
            Property obj, SelectedPropertyViewModel parentProp)
            : base(appCtx, dataCtx, parent, obj)
        {
            this._prop = obj;
            this._parent = parentProp;
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
                            _PossibleValues.Add(ViewModelFactory.CreateViewModel<SelectedPropertyViewModel.Factory>().Invoke(DataContext, Parent, prop, this));
                        }
                    }
                    if (Parent.FollowRelations && _prop is ObjectReferenceProperty)
                    {
                        foreach (var prop in ((ObjectReferenceProperty)_prop).GetReferencedObjectClass().Properties)
                        {
                            _PossibleValues.Add(ViewModelFactory.CreateViewModel<SelectedPropertyViewModel.Factory>().Invoke(DataContext, Parent, prop, this));
                        }
                    }
                }
                return _PossibleValues;
            }
        }
    }
}
