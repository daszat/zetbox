using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Client.Presentables.Relations
{
    public class RelationEndModel
        : DataObjectModel
    {
        private RelationEnd _relationEnd;

        public RelationEndModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            RelationEnd relEnd)
            : base(appCtx, dataCtx, relEnd)
        {
            _relationEnd = relEnd;
        }

        #region Public interface

        public PropertyModel<string> RoleName
        {
            get
            {
                return (PropertyModel<string>)this.PropertyModelsByName["RoleName"];
            }
        }

        public DataObjectModel Navigator { get; private set; }

        public ICommand CreateNavigatorCommand { get; private set; }

        public ObjectReferenceModel RelatedClass { get; private set; }

        public PropertyModel<bool> HasOrderPersisted { get; private set; }

        #endregion

        #region Utilities and UI callbacks
        #endregion

        #region Event handlers
        #endregion

    }
}
