
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
    using Kistl.API.Client;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Client.Models;

    /// <summary>
    /// </summary>
    public class ObjectCollectionViewModel
        : BaseObjectCollectionViewModel<ICollection<IDataObject>>, IValueCollectionViewModel<DataObjectViewModel, IReadOnlyObservableList<DataObjectViewModel>>, ISortableViewModel
    {
        public new delegate ObjectCollectionViewModel Factory(IKistlContext dataCtx, ViewModel parent, IValueModel mdl);

        public ObjectCollectionViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent,
            IObjectCollectionValueModel<ICollection<IDataObject>> mdl)
            : base(appCtx, dataCtx, parent, mdl)
        {
        }

        #region Public interface and IReadOnlyValueModel<IReadOnlyObservableCollection<DataObjectViewModel>> Members

        protected override string InitialSortProperty {get { return "ID";}}

        public bool HasPersistentOrder
        {
            get
            {
                return false;
            }
        }

        #endregion

        
    }
}
