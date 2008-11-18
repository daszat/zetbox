using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using System.Collections.ObjectModel;
using Kistl.App.Base;

namespace Kistl.Client.Presentables
{
    public class ObjectListModel
        : PropertyModel<ICollection<DataObjectModel>>, IValueModel<ObservableCollection<DataObjectModel>>
    {

        public ObjectListModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory,
            IDataObject referenceHolder, ObjectReferenceProperty prop)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory, referenceHolder, prop)
        {
            if (!prop.IsList)
                throw new ArgumentOutOfRangeException("prop", "ObjectReferenceProperty must be a list");
        }

        public ObjectListModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory,
            IDataObject referenceHolder, BackReferenceProperty prop)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory, referenceHolder, prop)
        {
        }

        #region Public interface: IValueModel<ObservableCollection<DataObjectModel>> Members

        public bool HasValue { get { UI.Verify(); return true; } }
        public bool IsNull { get { UI.Verify(); return false; } }

        private ObservableCollection<DataObjectModel> _value;
        public ObservableCollection<DataObjectModel> Value
        {
            get
            {
                UI.Verify();
                if (_value == null)
                {
                    _value = new ObservableCollection<DataObjectModel>();
                    _value.CollectionChanged += _value_CollectionChanged;
                }
                return _value;
            }
            private set
            {
                UI.Verify();
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged("Value");
                }
            }
        }

        private void _value_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UI.Verify();

            // uarg!

            throw new NotImplementedException();
        }

        private DataObjectModel _selectedItem;
        public DataObjectModel SelectedItem
        {
            get
            {
                UI.Verify();
                return _selectedItem;
            }
            set
            {
                UI.Verify();
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        public void CreateNewElement(Action<DataObjectModel> onCreated)
        {
            UI.Verify();

            Async.Queue(DataContext, () =>
            {
                DataType baseclass;
                if (this.Property is BackReferenceProperty)
                {
                    baseclass = ((BackReferenceProperty)this.Property).ReferenceProperty.ObjectClass;
                }
                else
                {
                    baseclass = ((ObjectReferenceProperty)this.Property).ReferenceObjectClass;
                }
                var children = new List<ObjectClass>();

                AsyncCollectChildClasses(baseclass.ID, children);

                UI.Queue(UI, () =>
                {

                    var childModels = children
                        .OrderBy(oc => oc.ClassName)
                        .Select(oc => (DataObjectModel)Factory.CreateModel<ObjectClassModel>(oc))
                        .ToList();

                    Factory.ShowModel(
                        Factory.CreateModel<DataObjectSelectionTaskModel>(
                            childModels,
                            new Action<DataObjectModel>(delegate(DataObjectModel chosen)
                            {
                                UI.Verify();
                                Async.Queue(DataContext, () =>
                                {
                                    if (chosen != null)
                                    {
                                        Type targetType = ((ObjectClass)chosen.Object).GetDataType();
                                        var item = this.DataContext.Create(targetType);
                                        UI.Queue(UI, () => onCreated(Factory.CreateModel<DataObjectModel>(item)));
                                    }
                                    else
                                    {
                                        UI.Queue(UI, () => onCreated(null));
                                    }
                                });
                            })));
                });
            });
        }

        #endregion

        #region Async handlers and UI callbacks

        protected override void AsyncGetPropertyValue()
        {
            Async.Verify();
            IEnumerable newValue = Object.GetPropertyValue<IEnumerable>(Property.PropertyName);
            UI.Queue(UI, () => SyncValues(newValue));
        }

        private void SyncValues(IEnumerable elements)
        {
            UI.Verify();
            ObservableCollection<DataObjectModel> newValue = new ObservableCollection<DataObjectModel>();
            foreach (IDataObject obj in elements.Cast<IDataObject>())
            {
                newValue.Add(Factory.CreateModel<DataObjectModel>(obj));
            }
            // almost optimal atomic update
            Value = newValue;
        }

        private void AsyncCollectChildClasses(int id, List<ObjectClass> children)
        {
            Async.Verify();

            var nextChildren = GuiContext.GetQuery<ObjectClass>().Where(oc =>
                oc.BaseObjectClass != null && oc.BaseObjectClass.ID == id);

            if (nextChildren.Count() > 0)
                foreach (ObjectClass oc in nextChildren)
                {
                    children.Add(oc);
                    AsyncCollectChildClasses(oc.ID, children);
                };
        }

        #endregion

    }
}
