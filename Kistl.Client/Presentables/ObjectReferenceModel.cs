using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;

namespace Kistl.Client.Presentables
{

    public class ObjectReferenceModel
        : PropertyModel<DataObjectModel>, IValueModel<DataObjectModel>
    {
        public ObjectReferenceModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject referenceHolder, ObjectReferenceProperty prop)
            : base(appCtx, dataCtx, referenceHolder, prop)
        {
            AllowNullInput = prop.IsNullable;
        }

        #region Public Interface

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

                if (!object.Equals(Object.GetPropertyValue<IDataObject>(Property.PropertyName), newPropertyValue))
                {
                    Object.SetPropertyValue<IDataObject>(Property.PropertyName, newPropertyValue);
                    CheckConstraints();

                    OnPropertyChanged("Value");
                    OnPropertyChanged("HasValue");
                    OnPropertyChanged("IsNull");
                }
            }
        }

        // TODO: make readonly for the view
        private ObservableCollection<DataObjectModel> _domain;
        public ObservableCollection<DataObjectModel> Domain
        {
            get
            {
                if (_domain == null)
                {
                    _domain = new ObservableCollection<DataObjectModel>();
                    FetchDomain();
                }
                return _domain;
            }
        }

        public void OpenReference()
        {
            if (Value != null)
                Factory.ShowModel(Value, true);
        }

        /// <summary>
        /// creates a new target and references it
        /// </summary>
        public void CreateNewItem(Action<DataObjectModel> onCreated)
        {
            ObjectClass baseclass = ((ObjectReferenceProperty)this.Property).ReferenceObjectClass;

            var children = new List<ObjectClass>() { baseclass };
            CollectChildClasses(baseclass.ID, children);

            if (children.Count == 1)
            {
                var targetType = baseclass.GetDescribedInterfaceType();
                var item = this.DataContext.Create(targetType);
                onCreated(Factory.CreateSpecificModel<DataObjectModel>(DataContext, item));
            }
            else
            {
                // sort by name, create models
                // TODO: filter non-instantiable classes
                var childModels = children
                    .OrderBy(oc => oc.ClassName)
                    .Select(oc => (DataObjectModel)Factory.CreateSpecificModel<ObjectClassModel>(DataContext, oc))
                    .ToList();

                Factory.ShowModel(
                    Factory.CreateSpecificModel<DataObjectSelectionTaskModel>(
                        DataContext,
                        childModels,
                        new Action<DataObjectModel>(delegate(DataObjectModel chosen)
                        {
                            if (chosen != null)
                            {
                                var targetType = ((ObjectClass)chosen.Object).GetDescribedInterfaceType();
                                var item = this.DataContext.Create(targetType);
                                onCreated(Factory.CreateSpecificModel<DataObjectModel>(DataContext, item));
                            }
                            else
                            {
                                onCreated(null);
                            }
                        })), true);
            }
        }

        #endregion

        #region Utilities and UI callbacks

        protected override void GetPropertyValue()
        {
            IDataObject newValue = Object.GetPropertyValue<IDataObject>(Property.PropertyName);
            var newModel = newValue == null ? null : (DataObjectModel)Factory.CreateDefaultModel(DataContext, newValue);
            if (Value != newModel)
            {
                Value = newModel;
            }
        }

        private void FetchDomain()
        {
            Debug.Assert(_domain != null);

            foreach (var obj in DataContext
                .GetQuery(new InterfaceType(Property.GetPropertyType()))
                .ToList() // TODO: remove this
                .OrderBy(obj => obj.ToString()).ToList())
            {
                _domain.Add((DataObjectModel)Factory.CreateDefaultModel(DataContext, obj));
            }

        }

        private void CollectChildClasses(int id, List<ObjectClass> children)
        {
            var nextChildren = MetaContext
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
