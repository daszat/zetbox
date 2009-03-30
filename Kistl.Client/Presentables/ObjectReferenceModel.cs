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
                UI.Verify();
                return _valueCache != null;
            }
            set
            {
                UI.Verify();
                if (!value)
                    Value = null;
            }
        }

        public bool IsNull
        {
            get
            {
                UI.Verify();
                return _valueCache == null;
            }
            set
            {
                UI.Verify();
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
            get { UI.Verify(); return _valueCache; }
            set
            {
                UI.Verify();

                _valueCache = value;
                State = ModelState.Loading;
                Async.Queue(DataContext, () =>
                {
                    Object.SetPropertyValue<IDataObject>(Property.PropertyName, _valueCache == null ? null : _valueCache.Object);
                    AsyncCheckConstraints();
                    UI.Queue(UI, () => this.State = ModelState.Active);
                });
                OnPropertyChanged("Value");
                OnPropertyChanged("HasValue");
                OnPropertyChanged("IsNull");
            }
        }

        // TODO: make readonly for the view
        private ObservableCollection<DataObjectModel> _domain;
        public ObservableCollection<DataObjectModel> Domain
        {
            get
            {
                UI.Verify();
                if (_domain == null)
                {
                    _domain = new ObservableCollection<DataObjectModel>();
                    Async.Queue(DataContext, AsyncFetchDomain);
                }
                return _domain;
            }
        }

        public void OpenReference()
        {
            UI.Verify();
            if (Value != null)
                Factory.ShowModel(Value, true);
        }

        /// <summary>
        /// creates a new target and references it
        /// </summary>
        public void CreateNewItem(Action<DataObjectModel> onCreated)
        {
            UI.Verify();

            Async.Queue(DataContext, () =>
            {
                ObjectClass baseclass;

                baseclass = ((ObjectReferenceProperty)this.Property).ReferenceObjectClass;

                var children = new List<ObjectClass>() { baseclass };
                AsyncCollectChildClasses(baseclass.ID, children);

                if (children.Count == 1)
                {
                    var targetType = baseclass.GetDescribedInterfaceType();
                    var item = this.DataContext.Create(targetType);
                    UI.Queue(UI, () => onCreated(Factory.CreateSpecificModel<DataObjectModel>(DataContext, item)));
                }
                else
                {
                    UI.Queue(UI, () =>
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
                                    UI.Verify();
                                    Async.Queue(DataContext, () =>
                                    {
                                        if (chosen != null)
                                        {
                                            var targetType = ((ObjectClass)chosen.Object).GetDescribedInterfaceType();
                                            var item = this.DataContext.Create(targetType);
                                            UI.Queue(UI, () => onCreated(Factory.CreateSpecificModel<DataObjectModel>(DataContext, item)));
                                        }
                                        else
                                        {
                                            UI.Queue(UI, () => onCreated(null));
                                        }
                                    });
                                })), true);
                    });
                }
            });
        }

        #endregion

        #region Async handlers and UI callbacks

        protected override void AsyncGetPropertyValue()
        {
            Async.Verify();
            IDataObject newValue = Object.GetPropertyValue<IDataObject>(Property.PropertyName);
            UI.Queue(UI, () => Value = newValue == null ? null : Factory.CreateSpecificModel<DataObjectModel>(DataContext, newValue));
        }

        private void AsyncFetchDomain()
        {
            Async.Verify();
            Debug.Assert(_domain != null);

            UI.Queue(UI, () => State = ModelState.Loading);

            var objs = DataContext.GetQuery(new InterfaceType(Property.GetPropertyType()))
                .ToList().OrderBy(obj => obj.ToString()).ToList();

            UI.Queue(UI, () =>
            {
                foreach (var obj in objs)
                {
                    _domain.Add(Factory.CreateSpecificModel<DataObjectModel>(DataContext, obj));
                }
                State = ModelState.Active;
            });
        }

        private void AsyncCollectChildClasses(int id, List<ObjectClass> children)
        {
            Async.Verify();

            var nextChildren = MetaContext.GetQuery<ObjectClass>()
                .Where(oc => oc.BaseObjectClass != null && oc.BaseObjectClass.ID == id)
                .ToList();

            if (nextChildren.Count() > 0)
            {
                foreach (ObjectClass oc in nextChildren)
                {
                    children.Add(oc);
                    AsyncCollectChildClasses(oc.ID, children);
                };
            }
        }

        #endregion

    }
}
