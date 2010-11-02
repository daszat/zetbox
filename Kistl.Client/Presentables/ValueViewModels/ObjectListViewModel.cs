
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
        : BaseObjectCollectionViewModel<IReadOnlyObservableList<DataObjectViewModel>, IList<IDataObject>>, IValueListViewModel<DataObjectViewModel, IReadOnlyObservableList<DataObjectViewModel>>
    {
        public new delegate ObjectListViewModel Factory(IKistlContext dataCtx, IValueModel mdl);

        public ObjectListViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            IObjectCollectionValueModel<IList<IDataObject>> mdl)
            : base(appCtx, dataCtx, mdl)
        {
        }

        #region Public interface and IReadOnlyValueModel<IReadOnlyObservableCollection<DataObjectViewModel>> Members

        private ReadOnlyObservableProjectedList<IDataObject, DataObjectViewModel> _valueCache;
        public override IReadOnlyObservableList<DataObjectViewModel> Value
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

        protected override void EnsureValueCache()
        {
            if (_valueCache == null)
            {
                _valueCache = new ReadOnlyObservableProjectedList<IDataObject, DataObjectViewModel>(
                    ObjectCollectionModel, ObjectCollectionModel.Value,
                    obj => ViewModelFactory.CreateViewModel<DataObjectViewModel.Factory>(obj).Invoke(DataContext, obj),
                    mdl => mdl.Object);
                _valueCache.CollectionChanged += _valueCache_CollectionChanged;
            }
        }

        void _valueCache_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyValueChanged();
        }

        public bool HasPersistentOrder
        {
            get
            {
                return true;
            }
        }

        #endregion

        public void MoveItemUp(DataObjectViewModel item)
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

        public void MoveItemDown(DataObjectViewModel item)
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
