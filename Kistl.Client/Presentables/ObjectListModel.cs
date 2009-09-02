using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Utils;
using Kistl.App.Base;
using Kistl.App.Extensions;

namespace Kistl.Client.Presentables
{
    /// <summary>
    /// Actually only models a Collection.
    /// </summary>
    public class ObjectListModel
        : PropertyModel<ICollection<DataObjectModel>>, IValueListModel<DataObjectModel>
    {

        public ObjectListModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject referenceHolder, ObjectReferenceProperty prop)
            : base(appCtx, dataCtx, referenceHolder, prop)
        {
            if (!prop.IsList)
                throw new ArgumentOutOfRangeException("prop", "ObjectReferenceProperty must be a list");

            ReferencedClass = prop.ReferenceObjectClass;
        }

        #region Public interface and IReadOnlyValueModel<IReadOnlyObservableCollection<DataObjectModel>> Members

        public bool HasValue
        {
            get { return true; }
        }

        public bool IsNull
        {
            get { return false; }
        }

        private ReadOnlyObservableProjectedCollection<IDataObject, DataObjectModel> _valueCache;
        public IReadOnlyObservableCollection<DataObjectModel> Value
        {
            get
            {
                if (_valueCache == null)
                {
                    _valueCache = new ReadOnlyObservableProjectedCollection<IDataObject, DataObjectModel>(
                        Object.GetPropertyValue<INotifyCollectionChanged>(Property.PropertyName),
                        obj => (DataObjectModel)Factory.CreateDefaultModel(DataContext, obj),
                        mdl => mdl.Object);
                }
                return _valueCache;
            }
        }

        public ObjectClass ReferencedClass
        {
            get;
            protected set;
        }

        public GridDisplayConfiguration DisplayedColumns
        {
            get
            {
                GridDisplayConfiguration result = new GridDisplayConfiguration()
                {
                    ShowIcon = ReferencedClass.ShowIconInLists,
                    ShowId = ReferencedClass.ShowIdInLists,
                    ShowName = ReferencedClass.ShowNameInLists
                };

                var group = this.ReferencedClass.GetAllProperties()
                    .Where(p => (p.CategoryTags ?? String.Empty).Split(',', ' ').Contains("Summary"));
                if (group.Count() == 0)
                {
                    group = this.ReferencedClass.GetAllProperties();
                }

                result.Columns = group
                    .Select(p => new ColumnDisplayModel() { Header = p.PropertyName, PropertyName = p.PropertyName, ControlKind = p.ValueModelDescriptor.GetDefaultGridCellKind() })
                    .ToList();
                return result;
            }
        }

        private DataObjectModel _selectedItem;
        public DataObjectModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        /// <summary>
        /// Creates a new Item suitable for adding to the list. This may prompt 
        /// the user to choose a type of item to add or enter an initial value.
        /// </summary>
        /// <param name="onCreated">this callback will be called with the newly created item on the UI thread</param>
        /// 
        /// This example creates a new item and activates it for the user to edit:
        /// <example><![CDATA[
        /// model.CreateNewElement(newitem =>
        /// {
        ///     if (newitem != null)
        ///     {
        ///         model.AddItem(newitem);
        ///         model.SelectedItem = newitem;
        ///         model.ActivateItem(model.SelectedItem, true);
        ///     }
        /// });]]>
        /// </example>
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

        public void AddItem(DataObjectModel item)
        {
            Object.AddToCollectionQuick(Property.PropertyName, item.Object);
        }

        /// <summary>
        /// Adds an existing item into this ObjectList. Asks the User which should be added.
        /// </summary>
        public void AddExistingItem()
        {
            var baseclass = ((ObjectReferenceProperty)this.Property).ReferenceObjectClass.GetDescribedInterfaceType();
            var instances = DataContext.GetQuery(baseclass).ToList(); // TODO: remove superfluous ToList
            var instanceModels = instances
                .OrderBy(i => i.ToString())
                .Select(i => (DataObjectModel)Factory.CreateDefaultModel(DataContext, i))
                .ToList();

            Factory.ShowModel(
                Factory.CreateSpecificModel<DataObjectSelectionTaskModel>(
                    DataContext,
                    instanceModels,
                    new Action<DataObjectModel>(delegate(DataObjectModel chosen)
                    {
                        if (chosen != null)
                        {
                            AddItem(chosen);
                            SelectedItem = chosen;
                        }
                    })), true);
        }

        public void RemoveItem(DataObjectModel item)
        {
            Object.RemoveFromCollectionQuick(Property.PropertyName, item.Object);
        }

        public void DeleteItem(DataObjectModel item)
        {
            RemoveItem(item);
            item.Delete();
        }

        public void ActivateItem(DataObjectModel item, bool activate)
        {
            Factory.ShowModel(item, activate);
        }

        #endregion

        #region Utilities and UI callbacks

        protected override void UpdatePropertyValue()
        {
        }

        private void CollectChildClasses(int id, List<ObjectClass> children)
        {
            var nextChildren = MetaContext.GetQuery<ObjectClass>()
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
