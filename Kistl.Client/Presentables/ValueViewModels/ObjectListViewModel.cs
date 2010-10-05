
namespace Kistl.Client.Presentables.ValueViewModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Client.Models;

    /// <summary>
    /// </summary>
    public class ObjectListViewModel
        : BaseObjectCollectionViewModel<IList<DataObjectModel>>, IValueListViewModel<DataObjectModel, IList<DataObjectModel>>
    {
        public new delegate ObjectCollectionModel Factory(IKistlContext dataCtx, IValueModel mdl);

        public IObjectListValueModel ObjectListModel { get; private set; }

        public ObjectListViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            IValueModel mdl)
            : base(appCtx, dataCtx, mdl)
        {
            ObjectListModel = (IObjectListValueModel)mdl;
        }

        #region Public interface and IReadOnlyValueModel<IReadOnlyObservableCollection<DataObjectModel>> Members

        private ReadOnlyObservableProjectedList<IDataObject, DataObjectModel> _valueCache;
        public override IList<DataObjectModel> Value
        {
            get
            {
                EnsureValueCache();
                return _valueCache;
            }
            set
            {
                throw new NotSupportedException();
            }

        }

        private void EnsureValueCache()
        {
            if (_valueCache == null)
            {
                _valueCache = new ReadOnlyObservableProjectedList<IDataObject, DataObjectModel>(
                    ObjectListModel,
                    obj => ModelFactory.CreateViewModel<DataObjectModel.Factory>(obj).Invoke(DataContext, obj),
                    mdl => mdl.Object);
            }
        }

        public bool HasPersistentOrder
        {
            get
            {
                return true;
            }
        }

        #region Commands

        public override void AddItem(DataObjectModel item)
        {
            if (item == null) { throw new ArgumentNullException("item"); }

            EnsureValueCache();
            Value.Add(item);

            SelectedItem = item;
        }


        public override void RemoveItem(DataObjectModel item)
        {
            if (item == null) { throw new ArgumentNullException("item"); }

            EnsureValueCache();
            Value.Remove(item);
        }

        public override void DeleteItem(DataObjectModel item)
        {
            if (item == null) { throw new ArgumentNullException("item"); }

            EnsureValueCache();
            Value.Remove(item);
            item.Delete();
        }
        #endregion

        #endregion

        public void MoveItemUp(DataObjectModel item)
        {
            if (item == null) { return; }

            var idx = Value.IndexOf(item);
            if (idx > 0)
            {
                Value.RemoveAt(idx);
                Value.Insert(idx - 1, item);
                SelectedItem = item;
            }
        }

        public void MoveItemDown(DataObjectModel item)
        {
            if (item == null) { return; }

            var idx = Value.IndexOf(item);
            if (idx != -1 && idx + 1 < Value.Count)
            {
                Value.RemoveAt(idx);
                Value.Insert(idx + 1, item);
                SelectedItem = item;
            }
        }
    }
}
