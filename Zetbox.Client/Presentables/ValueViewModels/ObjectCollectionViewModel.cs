
namespace Zetbox.Client.Presentables.ValueViewModels
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
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Models;

    /// <summary>
    /// </summary>
    public class ObjectCollectionViewModel
        : BaseObjectCollectionViewModel<ICollection<IDataObject>>, IValueCollectionViewModel<DataObjectViewModel, IReadOnlyObservableList<DataObjectViewModel>>, ISortableViewModel
    {
        public new delegate ObjectCollectionViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public ObjectCollectionViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
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
