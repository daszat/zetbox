
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
        public new delegate ObjectReferenceModel Factory(IKistlContext dataCtx, IDataObject obj, Property prop);

        public ObjectReferenceModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            IDataObject obj, ObjectReferenceProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            AllowNullInput = prop.IsNullable();
            ReferencedClass = prop.GetReferencedObjectClass();
        }

        public ObjectReferenceModel(
           IViewModelDependencies appCtx, IKistlContext dataCtx,
           IDataObject referenceHolder, CalculatedObjectReferenceProperty prop)
            : base(appCtx, dataCtx, referenceHolder, prop)
        {
            AllowNullInput = prop.IsNullable();
            ReferencedClass = prop.ReferencedClass;
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

        public IList<DataObjectModel> GetDomain()
        {
            List<DataObjectModel> result = new List<DataObjectModel>();

            foreach (var obj in DataContext
                .GetQuery(DataContext.GetInterfaceType(Property.GetPropertyType()))
                .ToList() // TODO: remove this
                .OrderBy(obj => obj.ToString()).ToList())
            {
                result.Add(ModelFactory.CreateViewModel<DataObjectModel.Factory>(obj).Invoke(DataContext, obj));
            }
            return result;
        }

        private void CollectChildClasses(int id, List<ObjectClass> children)
        {
            var nextChildren = FrozenContext.Single
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
