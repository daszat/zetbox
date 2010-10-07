
namespace Kistl.Client.Presentables.Relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.App.Base;
    using Kistl.Client.Presentables.ValueViewModels;
    
    public class RelationEndViewModel
        : DataObjectViewModel
    {
        public new delegate RelationEndViewModel Factory(IKistlContext dataCtx, RelationEnd relEnd);

        private RelationEnd _relationEnd;

        public RelationEndViewModel(
            IViewModelDependencies appCtx, KistlConfig config, IKistlContext dataCtx,
            RelationEnd relEnd)
            : base(appCtx, config, dataCtx, relEnd)
        {
            _relationEnd = relEnd;
        }

        #region Public interface

        public ValueViewModel<string, string> RoleName
        {
            get
            {
                return (ValueViewModel<string, string>)this.PropertyModelsByName["RoleName"];
            }
        }

        public DataObjectViewModel Navigator { get; private set; }

        public ICommandViewModel CreateNavigatorCommand { get; private set; }

        public ObjectReferenceViewModel RelatedClass { get; private set; }

        public NullableStructValueViewModel<bool> HasOrderPersisted { get; private set; }

        #endregion

        #region Utilities and UI callbacks
        #endregion

        #region Event handlers
        #endregion

    }
}
