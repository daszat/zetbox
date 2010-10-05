
namespace Kistl.Client.Presentables.ValueViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Client.Presentables.ValueViewModels;
    using Kistl.Client.Models;

    public partial class ObjectReferenceViewModel
        : ClassValueViewModel<DataObjectModel>
    {
        public new delegate ObjectReferenceModel Factory(IKistlContext dataCtx, IValueModel mdl);

        public ObjectReferenceViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            INotifyingObject obj, IValueModel mdl)
            : base(appCtx, dataCtx, mdl)
        {
            ObjectReferenceModel = (IObjectReferenceValueModel)mdl;
        }

        #region Public Interface

        public IObjectReferenceValueModel ObjectReferenceModel { get; private set; }

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
        #region OpenReference
        public void OpenReference()
        {
            if (Value != null)
                ModelFactory.ShowModel(Value, true);
        }

        private ICommand _openReferenceCommand;
        public ICommand OpenReferenceCommand
        {
            get
            {
                if (_openReferenceCommand == null)
                {
                    _openReferenceCommand = ModelFactory.CreateViewModel<SimpleCommandModel.Factory>()
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
        public void CreateNewItemAndSetValue(Action<DataObjectModel> onCreated)
        {
            ObjectClass baseclass = ObjectReferenceModel.ReferencedClass;

            var children = new List<ObjectClass>() { baseclass };
            CollectChildClasses(baseclass.ID, children);

            if (children.Count == 1)
            {
                var targetType = baseclass.GetDescribedInterfaceType();
                var item = this.DataContext.Create(targetType);
                var model = ModelFactory.CreateViewModel<DataObjectModel.Factory>().Invoke(DataContext, item);

                Value = model;

                if (onCreated != null)
                    onCreated(model);
            }
            else
            {
                ModelFactory.ShowModel(
                    ModelFactory.CreateViewModel<DataObjectSelectionTaskModel.Factory>().Invoke(
                        DataContext,
                        null,
                        children.AsQueryable(),
                        new Action<DataObjectModel>(delegate(DataObjectModel chosen)
                        {
                            if (chosen != null)
                            {
                                var targetType = ((ObjectClass)chosen.Object).GetDescribedInterfaceType();
                                var item = this.DataContext.Create(targetType);
                                var model = ModelFactory.CreateViewModel<DataObjectModel.Factory>().Invoke(DataContext, item);

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

        private ICommand _createNewItemAndSetValueCommand;
        public ICommand CreateNewItemAndSetValueCommand
        {
            get
            {
                if (_createNewItemAndSetValueCommand == null)
                {
                    _createNewItemAndSetValueCommand = ModelFactory.CreateViewModel<SimpleCommandModel.Factory>()
                        .Invoke(DataContext, "Create new item", "Create new item", () => CreateNewItemAndSetValue(null), () => !DataContext.IsReadonly);
                }
                return _createNewItemAndSetValueCommand;
            }
        }
        #endregion

        #region SelectValue

        public void SelectValue()
        {
            var ifType = DataContext.GetInterfaceType(ObjectReferenceModel.ReferencedClass);
            var selectionTask = ModelFactory.CreateViewModel<DataObjectSelectionTaskModel.Factory>().Invoke(
                DataContext,
                ifType.GetObjectClass(FrozenContext),
                null,
                new Action<DataObjectModel>(delegate(DataObjectModel chosen)
                {
                    if (chosen != null)
                    {
                        Value = chosen;
                    }
                }),
                null);
            ModelFactory.ShowModel(selectionTask, true);
        }

        private ICommand _SelectValueCommand;

        public ICommand SelectValueCommand
        {
            get
            {
                if (_SelectValueCommand == null)
                {
                    _SelectValueCommand = ModelFactory.CreateViewModel<SimpleCommandModel.Factory>()
                        .Invoke(DataContext, "Select", "Selects another reference", () => SelectValue(), () => !DataContext.IsReadonly);
                }
                return _SelectValueCommand;
            }
        }
        #endregion
        #endregion
    }
}
