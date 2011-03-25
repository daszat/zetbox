
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
        protected override IReadOnlyObservableList<DataObjectViewModel> GetValue()
        {
            EnsureValueCache();
            return _valueCache;
        }

        protected override void SetValue(IReadOnlyObservableList<DataObjectViewModel> value)
        {
            throw new NotSupportedException();
        }

        protected override void EnsureValueCache()
        {
            if (_valueCache == null)
            {
                _valueCache = new ReadOnlyObservableProjectedList<IDataObject, DataObjectViewModel>(
                    ObjectCollectionModel, ObjectCollectionModel.Value,
                    obj => DataObjectViewModel.Fetch(ViewModelFactory, DataContext, obj),
                    mdl => mdl.Object);
                _valueCache.CollectionChanged += ValueListChanged;
            }
        }

        public bool HasPersistentOrder
        {
            get
            {
                return true;
            }
        }

        #endregion

        private ICommandViewModel _MoveItemUpCommand = null;
        public ICommandViewModel MoveItemUpCommand
        {
            get
            {
                if (_MoveItemUpCommand == null)
                {
                    _MoveItemUpCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, "Up", "Moves the item up", MoveItemUp, () => SelectedItem != null);
                }
                return _MoveItemUpCommand;
            }
        }

        public void MoveItemUp()
        {
            var memories = SelectedItems.ToList();
            memories.ForEach(i => MoveItemUp(i));
            SelectedItems.Clear();
            memories.ForEach(i => SelectedItems.Add(i));
        }

        public void MoveItemUp(DataObjectViewModel item)
        {
            if (item == null) { return; }

            var idx = ValueModel.Value.IndexOf(item.Object);
            if (idx > 0)
            {
                ValueModel.Value.RemoveAt(idx);
                ValueModel.Value.Insert(idx - 1, item.Object);
                //SelectedItem = item;
            }
        }

        private ICommandViewModel _MoveItemDownCommand = null;
        public ICommandViewModel MoveItemDownCommand
        {
            get
            {
                if (_MoveItemDownCommand == null)
                {
                    _MoveItemDownCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, "Down", "Moves the item down", MoveItemDown, () => SelectedItem != null);
                }
                return _MoveItemDownCommand;
            }
        }

        public void MoveItemDown()
        {
            var memories = SelectedItems.ToList();
            memories.ForEach(i => MoveItemDown(i));
            SelectedItems.Clear();
            memories.ForEach(i => SelectedItems.Add(i));
        }

        public void MoveItemDown(DataObjectViewModel item)
        {
            if (item == null) { return; }

            var idx = ValueModel.Value.IndexOf(item.Object);
            if (idx != -1 && idx + 1 < Value.Count)
            {
                ValueModel.Value.RemoveAt(idx);
                ValueModel.Value.Insert(idx + 1, item.Object);
                //SelectedItem = item;
            }
        }
    }
}
