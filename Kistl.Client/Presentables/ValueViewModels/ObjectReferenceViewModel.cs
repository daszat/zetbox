
namespace Kistl.Client.Presentables.ValueViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables.ValueViewModels;

    public partial class ObjectReferenceViewModel
        : ValueViewModel<DataObjectViewModel, IDataObject>
    {
        public new delegate ObjectReferenceViewModel Factory(IKistlContext dataCtx, IValueModel mdl);

        public ObjectReferenceViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            IValueModel mdl)
            : base(appCtx, dataCtx, mdl)
        {
            ObjectReferenceModel = (IObjectReferenceValueModel)mdl;
            _allowCreateNewItem = !dataCtx.IsReadonly;
        }

        #region Public Interface

        public IObjectReferenceValueModel ObjectReferenceModel { get; private set; }
        public ObjectClass ReferencedClass { get { return ObjectReferenceModel.ReferencedClass; } }

        private bool _allowCreateNewItem = true;
        public bool AllowCreateNewItem
        {
            get
            {
                return _allowCreateNewItem;
            }
            set
            {
                if (_allowCreateNewItem != value)
                {
                    _allowCreateNewItem = value;
                    OnPropertyChanged("AllowCreateNewItem");
                }
            }
        }

        private bool _allowSelectValue = true;
        public bool AllowSelectValue
        {
            get
            {
                return _allowSelectValue;
            }
            set
            {
                if (_allowSelectValue != value)
                {
                    _allowSelectValue = value;
                    OnPropertyChanged("AllowSelectValue");
                }
            }
        }

        private bool _allowClear = true;
        public bool AllowClear
        {
            get
            {
                return _allowClear;
            }
            set
            {
                if (_allowClear != value)
                {
                    _allowClear = value;
                    OnPropertyChanged("Clear");
                }
            }
        }

        // Not supported by any command yet
        private bool _allowDelete = false;
        public bool AllowDelete
        {
            get
            {
                return _allowDelete;
            }
            set
            {
                if (_allowDelete != value)
                {
                    _allowDelete = value;
                    OnPropertyChanged("AllowDelete");
                }
            }
        }

        public override string Name
        {
            get { return Value == null ? "(null)" : "Reference to " + Value.Name; }
        }
        #endregion

        #region Utilities and UI callbacks

        private void CollectChildClasses(int id, List<ObjectClass> children)
        {
            var nextChildren = FrozenContext
                .GetQuery<ObjectClass>()
                .Where(oc => oc.BaseObjectClass != null && oc.BaseObjectClass.ID == id)
                .ToList();

            if (nextChildren.Count() > 0)
            {
                foreach (ObjectClass oc in nextChildren)
                {
                    children.Add(oc);
                    CollectChildClasses(oc.ID, children);
                };
            }
        }

        #endregion

        #region Commands
        private ObservableCollection<ICommandViewModel> _Commands;
        public ObservableCollection<ICommandViewModel> Commands
        {
            get
            {
                if (_Commands == null)
                {
                    _Commands = CreateCommands();
                }
                return _Commands;
            }
        }

        protected virtual ObservableCollection<ICommandViewModel> CreateCommands()
        {
            var cmds = new ObservableCollection<ICommandViewModel>();
            cmds.Add(SelectValueCommand);
            cmds.Add(CreateNewItemAndSetValueCommand);
            cmds.Add(OpenReferenceCommand);
            cmds.Add(ClearValueCommand);

            return cmds;
        }

        #region OpenReference
        public void OpenReference()
        {
            if (Value != null)
                ViewModelFactory.ShowModel(Value, true);
        }

        private ICommandViewModel _openReferenceCommand;
        public ICommandViewModel OpenReferenceCommand
        {
            get
            {
                if (_openReferenceCommand == null)
                {
                    _openReferenceCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>()
                        .Invoke(DataContext, "Open", "Open the referenced object", () => OpenReference(), () => Value != null);
                }
                return _openReferenceCommand;
            }
        }
        #endregion

        #region CreateNewItemAndSetValue

        /// <summary>
        /// creates a new target and references it
        /// </summary>
        public void CreateNewItemAndSetValue(Action<DataObjectViewModel> onCreated)
        {
            ObjectClass baseclass = ObjectReferenceModel.ReferencedClass;

            var children = new List<ObjectClass>() { baseclass };
            CollectChildClasses(baseclass.ID, children);

            if (children.Count == 1)
            {
                var targetType = baseclass.GetDescribedInterfaceType();
                var item = this.DataContext.Create(targetType);
                var model = ViewModelFactory.CreateViewModel<DataObjectViewModel.Factory>(item).Invoke(DataContext, item);

                Value = model;

                if (onCreated != null)
                    onCreated(model);
            }
            else
            {
                ViewModelFactory.ShowModel(
                    ViewModelFactory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(
                        DataContext,
                        null,
                        children.AsQueryable(),
                        new Action<DataObjectViewModel>(delegate(DataObjectViewModel chosen)
                        {
                            if (chosen != null)
                            {
                                var targetType = ((ObjectClass)chosen.Object).GetDescribedInterfaceType();
                                var item = this.DataContext.Create(targetType);
                                var model = ViewModelFactory.CreateViewModel<DataObjectViewModel.Factory>(item).Invoke(DataContext, item);

                                Value = model;
                                if (onCreated != null)
                                    onCreated(model);
                            }
                            else
                            {
                                if (onCreated != null)
                                    onCreated(null);
                            }
                        }), null), true);
            }
        }

        private ICommandViewModel _createNewItemAndSetValueCommand;
        public ICommandViewModel CreateNewItemAndSetValueCommand
        {
            get
            {
                if (_createNewItemAndSetValueCommand == null)
                {
                    _createNewItemAndSetValueCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>()
                        .Invoke(DataContext, "Create new item", "Create new item", () => CreateNewItemAndSetValue(null), () => AllowCreateNewItem);
                }
                return _createNewItemAndSetValueCommand;
            }
        }
        #endregion

        #region SelectValue

        public void SelectValue()
        {
            var ifType = ObjectReferenceModel.ReferencedClass.GetDescribedInterfaceType();
            var selectionTask = ViewModelFactory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(
                DataContext,
                ifType.GetObjectClass(FrozenContext),
                null,
                new Action<DataObjectViewModel>(delegate(DataObjectViewModel chosen)
                {
                    if (chosen != null)
                    {
                        Value = chosen;
                    }
                }),
                null);
            ViewModelFactory.ShowModel(selectionTask, true);
        }

        private ICommandViewModel _SelectValueCommand;

        public ICommandViewModel SelectValueCommand
        {
            get
            {
                if (_SelectValueCommand == null)
                {
                    _SelectValueCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>()
                        .Invoke(DataContext, "Select", "Selects another reference", () => SelectValue(), () => !DataContext.IsReadonly);
                }
                return _SelectValueCommand;
            }
        }
        #endregion
        #endregion

        #region Value
        protected override void ParseValue(string str)
        {
            throw new NotImplementedException();
        }

        private bool _valueCacheInititalized = false;
        private DataObjectViewModel _valueCache;

        /// <summary>
        /// Gets or sets the value of the property presented by this model
        /// </summary>
        public override DataObjectViewModel Value
        {
            get
            {
                if (!_valueCacheInititalized)
                {
                    UpdateValueCache();
                }
                return _valueCache;
            }
            set
            {
                _valueCache = value;
                _valueCacheInititalized = true;
                ValueModel.Value = value != null ? value.Object : null;
            }
        }

        private void UpdateValueCache()
        {
            var obj = ValueModel.Value;
            if (obj != null)
            {
                _valueCache = ViewModelFactory.CreateViewModel<DataObjectViewModel.Factory>(ValueModel.Value).Invoke(DataContext, ValueModel.Value);
            }
            _valueCacheInititalized = true;
        }
        #endregion
    }
}
