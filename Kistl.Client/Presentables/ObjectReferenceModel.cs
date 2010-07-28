
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
        : PropertyModel<DataObjectModel>, IValueModel<DataObjectModel>
    {
        public new delegate ObjectReferenceModel Factory(IKistlContext dataCtx, INotifyingObject obj, Property prop);

        public ObjectReferenceModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            INotifyingObject obj, Property prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            AllowNullInput = prop.IsNullable();
            if (prop is ObjectReferenceProperty)
            {
                ReferencedClass = ((ObjectReferenceProperty)prop).GetReferencedObjectClass();
            }
            else if (prop is CalculatedObjectReferenceProperty)
            {
                ReferencedClass = ((CalculatedObjectReferenceProperty)prop).ReferencedClass;
            }
            else
            {
                throw new ArgumentOutOfRangeException("prop", "prop must be ObjectReferenceProperty or CalculatedObjectReferenceProperty");
            }
        }

        #region Public Interface

        public ObjectClass ReferencedClass
        {
            get;
            protected set;
        }

        public bool HasValue
        {
            get
            {
                return _valueCache != null;
            }
            set
            {
                if (!value)
                    Value = null;
            }
        }

        public bool IsNull
        {
            get
            {
                return _valueCache == null;
            }
            set
            {
                if (value)
                    Value = null;
            }
        }

        public bool AllowNullInput { get; private set; }

        public void ClearValue()
        {
            if (AllowNullInput) Value = null;
            else throw new InvalidOperationException();
        }
        private ICommand _ClearValueCommand = null;
        public ICommand ClearValueCommand
        {
            get
            {
                if (_ClearValueCommand == null)
                {
                    _ClearValueCommand = ModelFactory.CreateViewModel<SimpleCommandModel.Factory>()
                        .Invoke(DataContext, "Clear value", "Sets the value to nothing", () => ClearValue(), () => AllowNullInput);
                }
                return _ClearValueCommand;
            }
        }

        private DataObjectModel _valueCache;
        /// <summary>
        /// The value of the property presented by this model
        /// </summary>
        public DataObjectModel Value
        {
            get { return _valueCache; }
            set
            {
                _valueCache = value;

                var newPropertyValue = _valueCache == null ? null : _valueCache.Object;

                if (!object.Equals(Object.GetPropertyValue<IDataObject>(Property.Name), newPropertyValue))
                {
                    Object.SetPropertyValue<IDataObject>(Property.Name, newPropertyValue);
                    CheckConstraints();

                    OnPropertyChanged("Value");
                    OnPropertyChanged("HasValue");
                    OnPropertyChanged("IsNull");
                }
            }
        }

        public override string Name
        {
            get { return Value == null ? "(null)" : "Reference to " + Value.Name; }
        }
        #endregion

        #region Utilities and UI callbacks

        protected override void UpdatePropertyValue()
        {
            IDataObject newValue = Object.GetPropertyValue<IDataObject>(Property.Name);
            var newModel = newValue == null ? null : ModelFactory.CreateViewModel<DataObjectModel.Factory>(newValue).Invoke(DataContext, newValue);
            if (Value != newModel)
            {
                Value = newModel;
            }
        }

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

    }
}
