
namespace Kistl.Client.Presentables
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

    public partial class ObjectReferenceModel
    {
        #region OpenReference

        public void OpenReference()
        {
            if (Value != null)
                ModelFactory.ShowModel(Value, true);
        }

        private OpenReferenceCommandModel _openReferenceCommand;

        public ICommand OpenReferenceCommand
        {
            get
            {
                if (_openReferenceCommand == null)
                {
                    _openReferenceCommand = ModelFactory.CreateViewModel<OpenReferenceCommandModel.Factory>().Invoke(DataContext, this);
                }
                return _openReferenceCommand;
            }
        }

        private class OpenReferenceCommandModel
            : CommandModel
        {
            public new delegate OpenReferenceCommandModel Factory(IKistlContext dataCtx, ObjectReferenceModel parent);

            public OpenReferenceCommandModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ObjectReferenceModel parent)
                : base(appCtx, dataCtx, "Open reference", "Open the referenced object")
            {
                _parent = parent;
            }
            private ObjectReferenceModel _parent;

            public override bool CanExecute(object data)
            {
                return (_parent.Value != null);
            }

            protected override void DoExecute(object data)
            {
                _parent.OpenReference();
            }
        }

        #endregion

        #region CreateNewItemAndSetValue

        /// <summary>
        /// creates a new target and references it
        /// </summary>
        public void CreateNewItemAndSetValue(Action<DataObjectModel> onCreated)
        {
            ObjectClass baseclass = ((ObjectReferenceProperty)this.Property).GetReferencedObjectClass();

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
                // sort by name, create models
                // TODO: filter non-instantiable classes
                var childModels = children
                    .OrderBy(oc => oc.Name)
                    .Select(oc => (DataObjectModel)ModelFactory.CreateViewModel<ObjectClassModel.Factory>().Invoke(DataContext, oc))
                    .ToList();

                ModelFactory.ShowModel(
                    ModelFactory.CreateViewModel<DataObjectSelectionTaskModel.Factory>().Invoke(
                        DataContext,
                        childModels,
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

        private CreateNewItemAndSetValueCommandModel _createNewItemAndSetValueCommand;

        public ICommand CreateNewItemAndSetValueCommand
        {
            get
            {
                if (_createNewItemAndSetValueCommand == null)
                {
                    _createNewItemAndSetValueCommand = ModelFactory.CreateViewModel<CreateNewItemAndSetValueCommandModel.Factory>().Invoke(DataContext, this);
                }
                return _createNewItemAndSetValueCommand;
            }
        }

        private class CreateNewItemAndSetValueCommandModel
            : CommandModel
        {
            public new delegate CreateNewItemAndSetValueCommandModel Factory(IKistlContext dataCtx, ObjectReferenceModel parent);

            public CreateNewItemAndSetValueCommandModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ObjectReferenceModel parent)
                : base(appCtx, dataCtx, "Create new item", "Create new item")
            {
                _parent = parent;
            }
            private ObjectReferenceModel _parent;

            public override bool CanExecute(object data)
            {
                return !DataContext.IsReadonly;
            }

            protected override void DoExecute(object data)
            {
                _parent.CreateNewItemAndSetValue(null);
            }
        }

        #endregion

        #region SelectValue

        public void SelectValue()
        {
            var selectionTask = ModelFactory.CreateViewModel<DataObjectSelectionTaskModel.Factory>().Invoke(
                DataContext,
                GetDomain(),
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

        private SelectValueCommandModel _SelectValueCommand;

        public ICommand SelectValueCommand
        {
            get
            {
                if (_SelectValueCommand == null)
                {
                    _SelectValueCommand = ModelFactory.CreateViewModel<SelectValueCommandModel.Factory>().Invoke(DataContext, this);
                }
                return _SelectValueCommand;
            }
        }

        private class SelectValueCommandModel
            : CommandModel
        {
            public new delegate SelectValueCommandModel Factory(IKistlContext dataCtx, ObjectReferenceModel parent);

            public SelectValueCommandModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ObjectReferenceModel parent)
                : base(appCtx, dataCtx, "Open reference", "Open reference")
            {
                _parent = parent;
            }
            private ObjectReferenceModel _parent;

            public override bool CanExecute(object data)
            {
                return !DataContext.IsReadonly;
            }

            protected override void DoExecute(object data)
            {
                _parent.SelectValue();
            }
        }

        #endregion
    }
}
