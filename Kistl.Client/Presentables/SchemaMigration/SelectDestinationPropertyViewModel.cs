using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;
using ZBox.App.SchemaMigration;
using Kistl.API.Configuration;
using Kistl.App.Extensions;

namespace Kistl.Client.Presentables.SchemaMigration
{
    [ViewModelDescriptor]
    public class SelectDestinationPropertyViewModel : WindowViewModel
    {
#if MONO
        // See https://bugzilla.novell.com/show_bug.cgi?id=660553
        public delegate SelectDestinationPropertyViewModel Factory(IKistlContext dataCtx,
            SourceColumn srcCol,
            Action<IEnumerable<Property>> callback);
#else
        public new delegate SelectDestinationPropertyViewModel Factory(IKistlContext dataCtx,
            SourceColumn srcCol,
            Action<IEnumerable<Property>> callback);
#endif
        
        private readonly Action<IEnumerable<Property>> _callback;
        private readonly SourceColumn _srcCol;

        public SelectDestinationPropertyViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            SourceColumn srcCol,
            Action<IEnumerable<Property>> callback)
            : base(appCtx, dataCtx)
        {
            _callback = callback;
            _srcCol = srcCol;
        }

        #region Commands
        public void Choose()
        {
            IList<Property> result = new List<Property>();
            var prop = SelectedItem;
            while (prop != null)
            {
                result.Add(prop.Property);
                prop = prop.Parent;
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
                    _ChooseCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, "Choose", "Select the current destination property",
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
                    _CancelCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, "Cancel", "Abort selecting a destination property", Cancel, null);
                }
                return _CancelCommand;
            }
        }
        #endregion

        private List<PossibleDestPropertyViewModel> _PossibleValues = null;
        public IEnumerable<PossibleDestPropertyViewModel> PossibleValues
        {
            get
            {
                if (_PossibleValues == null)
                {
                    _PossibleValues = new List<PossibleDestPropertyViewModel>();
                    foreach (var prop in _srcCol.SourceTable.DestinationObjectClass.GetAllProperties())
                    {
                        _PossibleValues.Add(ViewModelFactory.CreateViewModel<PossibleDestPropertyViewModel.Factory>().Invoke(DataContext, prop, null));
                    }
                }
                return _PossibleValues;
            }
        }

        private PossibleDestPropertyViewModel _SelectedItem = null;
        public PossibleDestPropertyViewModel SelectedItem
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

    public class PossibleDestPropertyViewModel : DataObjectViewModel
    {
#if MONO
        // See https://bugzilla.novell.com/show_bug.cgi?id=660553
        public delegate PossibleDestPropertyViewModel Factory(IKistlContext dataCtx, Property obj, PossibleDestPropertyViewModel parent);
#else
        public new delegate PossibleDestPropertyViewModel Factory(IKistlContext dataCtx, Property obj, PossibleDestPropertyViewModel parent);
#endif

        private readonly Property _prop;
        private readonly PossibleDestPropertyViewModel _parent;

        public PossibleDestPropertyViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            Property obj, PossibleDestPropertyViewModel parent)
            : base(appCtx, dataCtx, obj)
        {
            this._prop = obj;
            this._parent = parent;
        }

        public Property Property
        {
            get
            {
                return _prop;
            }
        }

        public PossibleDestPropertyViewModel Parent
        {
            get
            {
                return _parent;
            }
        }

        private List<PossibleDestPropertyViewModel> _PossibleValues = null;
        public IEnumerable<PossibleDestPropertyViewModel> PossibleValues
        {
            get
            {
                if (_PossibleValues == null)
                {
                    _PossibleValues = new List<PossibleDestPropertyViewModel>();
                    if (_prop is CompoundObjectProperty)
                    {
                        foreach (var prop in ((CompoundObjectProperty)_prop).CompoundObjectDefinition.Properties)
                        {
                            _PossibleValues.Add(ViewModelFactory.CreateViewModel<PossibleDestPropertyViewModel.Factory>().Invoke(DataContext, prop, this));
                        }
                    }
                }
                return _PossibleValues;
            }
        }
    }
}
