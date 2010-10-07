using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.API.Configuration;
using Kistl.Client.Presentables.ValueViewModels;

namespace Kistl.Client.Presentables.Relations
{
    public class RelationEndModel
        : DataObjectViewModel
    {
        public new delegate RelationEndModel Factory(IKistlContext dataCtx, RelationEnd relEnd);

        private RelationEnd _relationEnd;

        public RelationEndModel(
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

        public ICommand CreateNavigatorCommand { get; private set; }

        public ObjectReferenceViewModel RelatedClass { get; private set; }

        public NullableStructValueViewModel<bool> HasOrderPersisted { get; private set; }

        #endregion

        #region Utilities and UI callbacks
        #endregion

        #region Event handlers
        #endregion

    }
}
