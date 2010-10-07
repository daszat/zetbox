
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
        : BaseObjectCollectionViewModel<IReadOnlyObservableList<DataObjectModel>, IList<IDataObject>>, IValueListViewModel<DataObjectModel, IReadOnlyObservableList<DataObjectModel>>
    {
        public new delegate ObjectListViewModel Factory(IKistlContext dataCtx, IValueModel mdl);

        public ObjectListViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            IObjectCollectionValueModel<IList<IDataObject>> mdl)
            : base(appCtx, dataCtx, mdl)
        {
        }

        #region Public interface and IReadOnlyValueModel<IReadOnlyObservableCollection<DataObjectModel>> Members

        private ReadOnlyObservableProjectedList<IDataObject, DataObjectModel> _valueCache;
        public override IReadOnlyObservableList<DataObjectModel> Value
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
                    ObjectCollectionModel, ObjectCollectionModel.Value,
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
            ValueModel.Value.Add(item.Object);

            SelectedItem = item;
        }


        public override void RemoveItem(DataObjectModel item)
        {
            if (item == null) { throw new ArgumentNullException("item"); }

            EnsureValueCache();
            ValueModel.Value.Remove(item.Object);
        }

        public override void DeleteItem(DataObjectModel item)
        {
            if (item == null) { throw new ArgumentNullException("item"); }

            EnsureValueCache();
            ValueModel.Value.Remove(item.Object);
            item.Delete();
        }
        #endregion

        #endregion

        public void MoveItemUp(DataObjectModel item)
        {
            if (item == null) { return; }

            var idx = ValueModel.Value.IndexOf(item.Object);
            if (idx > 0)
            {
                ValueModel.Value.RemoveAt(idx);
                ValueModel.Value.Insert(idx - 1, item.Object);
                SelectedItem = item;
            }
        }

        public void MoveItemDown(DataObjectModel item)
        {
            if (item == null) { return; }

            var idx = ValueModel.Value.IndexOf(item.Object);
            if (idx != -1 && idx + 1 < Value.Count)
            {
                ValueModel.Value.RemoveAt(idx);
                ValueModel.Value.Insert(idx + 1, item.Object);
                SelectedItem = item;
            }
        }
    }
}
